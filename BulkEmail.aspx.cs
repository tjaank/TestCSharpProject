using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Net.Mail;
using System.Data.SqlClient;

namespace TestApplication
{
    public partial class BulkEmail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                chklstRecipientsFromDB.Visible = false;
                btnAddSelected.Visible = false;
            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                #region Direct Email to db Recipients
                //if you want to send emails directly to recipient from database, use code below
                //DataSet dsClients = GetClientDataSet();
                //if (dsClients.Tables["Users"].Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dsClients.Tables["Users"].Rows)
                //    {
                //        SendEmail(dr["Email"].ToString());
                //    }
                //}
                #endregion

                char[] splitter = { ';' };
                foreach (string emailAdd in txtRecipient.Text.Split(splitter))
                {
                    SendEmail(emailAdd);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnRecipientFromDB_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsClients = GetClientDataSet();

                chklstRecipientsFromDB.DataSource = dsClients.Tables[0];
                chklstRecipientsFromDB.DataTextField = "Email";
                chklstRecipientsFromDB.DataValueField = "Email";
                chklstRecipientsFromDB.DataBind();

                chklstRecipientsFromDB.Visible = true;
                btnAddSelected.Visible = true;
                btnRecipientFromDB.Visible = false;
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }
        /// 
        ///  Creates and returns a DataSet using Ms Access OleDBConnection and an OleDBDataAdapter
        /// 
        /// 
        /// A DataSet from Ms Access using an OleDBConnection and an OleDBDataAdapter
        /// 
        private System.Data.DataSet GetClientDataSet()
        {

            //retrieve the connection string from the ConnectionString Key in Web.Config
            //string connectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Talha\Documents\Database2.accdb;Persist Security Info=False;";

            //create a new OleDB connection
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connectionString);

            //pass the Select statement and connection information to the initializxation constructor for the OleDBDataAdapter
            System.Data.OleDb.OleDbDataAdapter myDataAdapter = new System.Data.OleDb.OleDbDataAdapter("SELECT Email FROM Employees", conn);

            //Create a new dataset with a table : CLIENTS
            System.Data.DataSet myDataSet = new System.Data.DataSet("Users");

            //Fill the dataset and table with the data retrieved by the select command
            myDataAdapter.Fill(myDataSet, "Users");

            //return the new dataset created 
            return myDataSet;
        }
        DataTable ExecuteDataTable(string sp, SqlParameter[] splParams)
        {
            DataTable dt = new DataTable();

            // 1
            // Open connection
            using (SqlConnection c = new SqlConnection("YourConnectionString"))
            {
                c.Open();
                using (SqlCommand sqlCmd = new SqlCommand(sp, c))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    foreach (SqlParameter prm in splParams)
                    {
                        sqlCmd.Parameters.Add(prm);
                    }
                    // 2
                    // Create new DataAdapter
                    using (SqlDataAdapter a = new SqlDataAdapter(sqlCmd))
                    {
                        // 3
                        // Use DataAdapter to fill DataTable
                        a.Fill(dt);

                    }
                }
            }
            return dt;
        }
        private void SendEmail(string EmailAddress)
        {
            //Send Email Functionality here
            MailMessage mail = new MailMessage();
            mail.To.Add(EmailAddress);
            mail.From = new MailAddress("admin@codeshode.com");
            mail.Subject = txtSubject.Text;
            mail.Body = txtEmailBody.Text;

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; //Or Your SMTP Server Address
            smtp.Credentials = new System.Net.NetworkCredential("YourUserName@gmail.com", "YourGmailPassword");//Or your Smtp Email ID and Password
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }

        protected void btnAddSelected_Click(object sender, EventArgs e)
        {
            StringBuilder strRecipientEmails = new StringBuilder();
            foreach (ListItem chk in chklstRecipientsFromDB.Items)
            {
                if (chk.Selected)
                {
                    strRecipientEmails.Append(chk.Value);
                    strRecipientEmails.Append(";");
                }
            }
            txtRecipient.Text = strRecipientEmails.ToString();
            chklstRecipientsFromDB.Visible = false;
            btnAddSelected.Visible = false;
        }
    }
}