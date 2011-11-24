using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Script.Services;
using System.Collections;
using System.Data;
using System.Xml.Linq;
using System.ServiceModel.Syndication;

namespace TestApplication.Sysnet
{
    public class Alert
    {
        public string AlertCode { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
    }
    public partial class Async_Repeater : System.Web.UI.Page
    {
        private static List<Alert> CustomerAlert;

        public static List<Alert> AlertList
        {
            get
            {
                if (CustomerAlert == default(List<Alert>))
                {
                    CustomerAlert = new List<Alert>();
                    DataTable dt = new DataTable();
                    using (SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ToString()))
                    {
                        SqlCommand command = sqlCon.CreateCommand();

                        command.CommandText = "SELECT * FROM Alert";
                        command.CommandType = CommandType.Text;

                        sqlCon.Open();
                        using (SqlDataAdapter a = new SqlDataAdapter("SELECT * FROM Alert", sqlCon))
                            a.Fill(dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        CustomerAlert.Add(new Alert() { AlertCode = dr["Alert_Code"].ToString(), Text = dr["Alert_Text"].ToString(), Url = dr["Alert_Url"].ToString() });
                    }
                }
                return CustomerAlert;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [WebMethod()]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static IEnumerable GetAlerts()
        {
            System.Threading.Thread.Sleep(3000);
            return AlertList.AsEnumerable();
        }
    }
}