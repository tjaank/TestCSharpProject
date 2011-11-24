using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;


namespace TestApplication
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //SqlDataAdapter myCommand = new SqlDataAdapter();
            //myCommand.Fill(ds, "VL_Dashboard");
            //DataList1.DataSource = ds.Tables["VL_Dashboard"].DefaultView;
            //DataList1.DataBind();

            //DateTime dtNow = DateTime.Now;

            //string dtCurrent = DateTime.Now.ToShortDateString();
            //string dt1DayBefore = DateTime.Now.AddDays(-1).ToShortDateString();
            //string dt2DaysBefore = DateTime.Now.AddDays(-2).ToShortDateString();
            //string dt3DaysBefore = DateTime.Now.AddDays(-3).ToShortDateString();
            
            string s = "TestMode=0<br>MessageReceived=Dear Tim, The details you entered for eligiblity check for FSM for your childrens Dayton,Nyle,Ameyal were not found in FSM database<br>" +
                        "MessageCount=1<br>From=SolihullMBC<br>CreditsAvailable=128.8<br>MessageLength=1<br>NumberContacts=1<br>CreditsRequired=0.9" +
                        "<br>CreditsRemaining=127.9";

            string creditRemaining = s.Substring(s.IndexOf("CreditsRemaining="), 4);

        }
        protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int str = Convert.ToInt32(e.CommandArgument.ToString());
                //Response.Redirect(Globals.NavigateURL(str));
            }
        }

        protected void Repeater1_ItemDataBound(object source, RepeaterItemEventArgs e)
        {

            // This event is raised for the header, the footer, separators, and items.

            // Execute the following logic for Items and Alternating Items.
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Image thisImage = (Image)e.Item.FindControl("imgNews");
            }
        }
        private DataSet GetClientDataSet()
        {

            //retrieve the connection string from the ConnectionString Key in Web.Config
            //string connectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Talha\Documents\Database2.accdb;Persist Security Info=False;";

            //create a new OleDB connection
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connectionString);

            //pass the Select statement and connection information to the initializxation constructor for the OleDBDataAdapter
            System.Data.OleDb.OleDbDataAdapter myDataAdapter = new System.Data.OleDb.OleDbDataAdapter("SELECT Email As pictID FROM Employees", conn);

            //Create a new dataset with a table : CLIENTS
            System.Data.DataSet myDataSet = new System.Data.DataSet("Users");

            //Fill the dataset and table with the data retrieved by the select command
            myDataAdapter.Fill(myDataSet, "Users");

            //return the new dataset created 
            return myDataSet;
        }
    }
}

