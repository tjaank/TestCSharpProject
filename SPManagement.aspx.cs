using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;
using Microsoft.SqlServer.Management.Smo;

namespace StoreProcedureManagement
{
    public partial class SPManagement : System.Web.UI.Page
    {
        #region Variables
        SqlConnection adminConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["YourConnectionStringFromWebConfig"].ConnectionString);
        #endregion

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                divSuccess.InnerHtml = string.Empty;
                divError.InnerHtml = string.Empty;
                if (!IsPostBack)
                {
                    adminConnection.Open();
                    BindDatabaseClients();
                }
            }
            catch (Exception ex)
            {
                divError.InnerHtml += " " + ex.Message;
            }
            finally
            {
                adminConnection.Close();
            }
        }

        protected void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder strbSuccessClientName;
                StringBuilder strbFailedClientName;
                int countSuccess = 0;
                int countAccessed = 0;
                //Check for Restricted Instructions
                if (CheckString(txtSPBody.Text.ToLower()))
                {
                    strbSuccessClientName = new StringBuilder();
                    strbFailedClientName = new StringBuilder();
                    DataTable dtClients = (DataTable)ViewState["dtClients"];

                    for (int i = 0; i > chklstDatabaseServers.Items.Count; i++)
                    {
                        if (chklstDatabaseServers.Items[i].Selected)
                        {
                            countAccessed++;
                            string connectionString = string.Empty;
                            DataTable dtClient = new DataTable();
                            FilterTable(dtClients, ref dtClient, "Client_Code = " + chklstDatabaseServers.Items[i].Value);

                            connectionString = dtClient.Rows[0]["Client_Database_Credentials"].ToString();

                            bool errorOccured = ExecuteSP(connectionString, dtClient.Rows[0]["Client_Name"].ToString());
                            if (errorOccured)
                            {
                                strbFailedClientName.Append(dtClient.Rows[0]["Client_Name"].ToString() + "");
                            }
                            else
                            {
                                strbSuccessClientName.Append(dtClient.Rows[0]["Client_Name"].ToString() + "");
                                countSuccess++;
                            }
                        }
                    }
                    if (countSuccess > 0)
                        divSuccess.InnerHtml = "Successfully executed on following client(s): " + strbSuccessClientName.ToString();

                    if (!string.IsNullOrEmpty(strbFailedClientName.ToString()))
                        divError.InnerHtml = "Error Occured On Following Client(s): " + strbFailedClientName.ToString();
                }
            }
            catch (Exception ex)
            {
                divError.InnerHtml = "Following Error Occured : " + Environment.NewLine + ex.Message;
            }
        }

        #endregion

        #region Methods

        private void BindDatabaseClients()
        {
            DataTable dtClients = new DataTable();
            BindAllClients(adminConnection, ref dtClients);

            ViewState["dtClients"] = dtClients;

            chklstDatabaseServers.DataSource = dtClients;
            chklstDatabaseServers.DataTextField = "Client_Name";
            chklstDatabaseServers.DataValueField = "Client_Code";
            chklstDatabaseServers.DataBind();

        }

        protected bool CheckString(string str)
        {

            if (!TxtAdmin.Text.Equals("AdminTalha"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('You are not authorized to use this screen')", true);
                return false;
            }
            else
            {
                char[] delimiters = new char[] { '\r', '\n', ' ', ';', ':', '"', ',', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '+', '=', '~', '`', '{', '}', '[', ']', '|', '\\', '<', '>', '?', '/' };
                string[] strA = str.Split(delimiters);
                if (strA.Contains("alter proc"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('alter procedure command is not allowed')", true);
                    return false;
                }
                else if (strA.Contains("create proc"))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('create procedure command is not allowed')", true);
                    return false;
                }
                else if (string.IsNullOrEmpty(txtSPName.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('SP Name cannot be empty')", true);
                    return false;
                }
                else if (string.IsNullOrEmpty(txtSPBody.Text))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('SP Body cannot be empty')", true);
                    return false;
                }
            }

            //its ok 
            return true;
        }

        private bool ExecuteSP(string connectionString, string clientName)
        {
            bool error = false;
            SqlConnection sqlCon = new SqlConnection(connectionString);
            Microsoft.SqlServer.Management.Common.ServerConnection srvCon = new Microsoft.SqlServer.Management.Common.ServerConnection(sqlCon);

            try
            {
                sqlCon.Open();
                //objSql
                Server srv = new Server(srvCon);

                //Reference the database. 
                Database db = srv.Databases[sqlCon.Database];

                //Define a StoredProcedure object variable by supplying the parent database and name arguments in the constructor. 
                StoredProcedure sp = null;

                if (rdobtnlstExecutionMode.SelectedValue == "1")
                    sp = new StoredProcedure(db, txtSPName.Text);
                else if (rdobtnlstExecutionMode.SelectedValue == "2" || rdobtnlstExecutionMode.SelectedValue == "3")
                    sp = db.StoredProcedures[txtSPName.Text];

                if (sp == null)
                {
                    sqlCon.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Stored Procedure Not Found in '" + clientName + "')", true);
                    error = true;
                }
                else
                {
                    //Set the TextMode property to false and then set the other object properties. 
                    sp.AnsiNullsStatus = false;
                    sp.QuotedIdentifierStatus = false;
                    sp.TextMode = false;


                    #region SP Parameters

                    if (!string.IsNullOrEmpty(txtSPParams.Text))
                    {
                        char[] splitter = { ',' };
                        char[] internalSplitter = { ' ' };
                        string[] spParameters = txtSPParams.Text.Replace(Environment.NewLine, string.Empty).Replace("\n", string.Empty).Split(splitter, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string spParam in spParameters)
                        {
                            string[] p = spParam.Split(internalSplitter, StringSplitOptions.RemoveEmptyEntries);

                            string paramName = p[0];
                            string paramDataType = p[1].ToLower();

                            Microsoft.SqlServer.Management.Smo.DataType thisDt = null;

                            int scaleLength = 0;
                            int precision = 0;

                            string intermediateDT = paramDataType;
                            if (paramDataType.Contains("varchar"))
                            {
                                paramDataType = paramDataType.Replace("(", " ");
                                paramDataType = paramDataType.Replace(")", " ");
                                paramDataType = paramDataType.Replace(",", " ");
                                paramDataType = paramDataType.Replace("  ", " ");

                                scaleLength = Convert.ToInt32((paramDataType.Split(internalSplitter, StringSplitOptions.RemoveEmptyEntries))[1]);
                                paramDataType = "varchar";
                            }
                            else if (paramDataType.Contains("decimal"))
                            {
                                paramDataType = paramDataType.Replace("(", " ");
                                paramDataType = paramDataType.Replace(")", " ");
                                paramDataType = paramDataType.Replace(",", " ");
                                paramDataType = paramDataType.Replace("  ", " ");

                                scaleLength = Convert.ToInt32((paramDataType.Split(internalSplitter, StringSplitOptions.RemoveEmptyEntries))[1]);
                                precision = Convert.ToInt32((paramDataType.Split(internalSplitter, StringSplitOptions.RemoveEmptyEntries))[2]);
                                paramDataType = "decimal";
                            }
                            else if (paramDataType.Contains("numeric"))
                            {
                                paramDataType = paramDataType.Replace("(", " ");
                                paramDataType = paramDataType.Replace(")", " ");
                                paramDataType = paramDataType.Replace(",", " ");
                                paramDataType = paramDataType.Replace("  ", " ");

                                scaleLength = Convert.ToInt32((paramDataType.Split(internalSplitter, StringSplitOptions.RemoveEmptyEntries))[1]);
                                precision = Convert.ToInt32((paramDataType.Split(internalSplitter, StringSplitOptions.RemoveEmptyEntries))[2]);
                                paramDataType = "numeric";
                            }

                            switch (paramDataType)
                            {
                                case "int":
                                    thisDt = DataType.Int;
                                    break;
                                case "bit":
                                    thisDt = DataType.Bit;
                                    break;
                                case "text":
                                    thisDt = DataType.Text;
                                    break;
                                case "datetime":
                                    thisDt = DataType.DateTime;
                                    break;
                                case "bigint":
                                    thisDt = DataType.BigInt;
                                    break;
                                case "varchar":
                                    thisDt = DataType.VarChar(scaleLength);
                                    paramDataType = intermediateDT;
                                    break;
                                case "decimal":
                                    thisDt = DataType.Decimal(scaleLength, precision);
                                    paramDataType = intermediateDT;
                                    break;
                                case "numeric":
                                    thisDt = DataType.Numeric(scaleLength, precision);
                                    paramDataType = intermediateDT;
                                    break;
                                default:
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('SP Parameter with unknown datatype found.')", true);
                                    error = true;
                                    break;
                            }
                            if (error)
                            {
                                sqlCon.Close();
                                return error;
                            }

                            sp.Parameters.Add(new StoredProcedureParameter(sp, paramName, thisDt));
                        }
                    }

                    #endregion

                    //Set the TextBody property to define the stored procedure. 
                    sp.TextBody = txtSPBody.Text.Replace("\n", Environment.NewLine);

                    if (rdobtnlstExecutionMode.SelectedValue == "1")
                    {
                        sp.QuotedIdentifierStatus = true;
                        //Create the stored procedure on the instance of SQL Server. 
                        sp.Create();
                    }
                    else if (rdobtnlstExecutionMode.SelectedValue == "2")
                    {
                        sp.QuotedIdentifierStatus = true;
                        //Modify a property and run the Alter method to make the change on the instance of SQL Server. 
                        sp.Alter();
                    }
                    else if (rdobtnlstExecutionMode.SelectedValue == "3")
                    {
                        //Remove the stored procedure. 
                        sp.Drop();
                    }
                }
            }
            catch (Exception ex)
            {
                sqlCon.Close();
                divError.InnerHtml = "Following Error Occured : " + Environment.NewLine + ex.Message;
            }
            return error;
        }
        public static void FilterTable(DataTable dtSource, ref DataTable dtFiltered, string filterString)
        {
            dtFiltered = dtSource.Clone();
            dtSource.DefaultView.RowFilter = filterString;
            dtFiltered = dtSource.DefaultView.ToTable();
        }
        public static void BindAllClients(SqlConnection sqlConnection, ref DataTable dtClients)
        {
            SqlCommand command;
            command = new SqlCommand();
            command.Connection = sqlConnection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "PL_Select_AllDatabaseServersCredentials";
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dtClients);
        }

        #endregion
    }
}