using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestApplication
{
    public partial class Chart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Chart1.DataSource = GetProductOrderDataSet();
            //Chart1.Series["Series1"].XValueMember = "ProductName";
            //Chart1.Series["Series1"].YValueMembers = "Quantity";
            //Chart1.DataBind();

            //AccessDataSource1.DataBind();
            //Chart1.DataBind();
        }
        private System.Data.DataSet GetProductOrderDataSet()
        {

            //retrieve the connection string from the ConnectionString Key in Web.Config
            //string connectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
            string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Talha\Documents\Database2.accdb;Persist Security Info=False;";

            //create a new OleDB connection
            System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection(connectionString);

            //pass the Select statement and connection information to the initializxation constructor for the OleDBDataAdapter
            System.Data.OleDb.OleDbDataAdapter myDataAdapter = new System.Data.OleDb.OleDbDataAdapter("SELECT Products.ProductName, Products.ProductCode, OrderDetails.Quantity FROM (Products INNER JOIN OrderDetails ON Products.ID = OrderDetails.ProductID)", conn);

            //Create a new dataset with a table : CLIENTS
            System.Data.DataSet myDataSet = new System.Data.DataSet("ProductOrder");

            //Fill the dataset and table with the data retrieved by the select command
            myDataAdapter.Fill(myDataSet, "ProductOrder");

            //return the new dataset created 
            return myDataSet;
        }
    }
}