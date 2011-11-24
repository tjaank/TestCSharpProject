using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace TestApplication
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string connStr = @"Data Source=Talha-PC\SQLExpress;Initial Catalog=LocalTestDB;User Id=talha;Password=talha123;Trusted_Connection=True;";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "Select MenuID, Text,Description, ParentID from Menu";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.Fill(ds);
                da.Dispose();
            }
            ds.DataSetName = "Menus";
            ds.Tables[0].TableName = "Menu";
            DataRelation relation = new DataRelation("ParentChild",
             ds.Tables["Menu"].Columns["MenuID"],
             ds.Tables["Menu"].Columns["ParentID"], true);

            relation.Nested = true;
            ds.Relations.Add(relation);

            XmlDataSource1.Data = ds.GetXml();

            if (Request.Params["Sel"] != null)
                Page.Controls.Add(new System.Web.UI.LiteralControl("You selected " + Request.Params["Sel"]));
        }
    }
}