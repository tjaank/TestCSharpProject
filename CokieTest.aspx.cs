using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace TestApplication
{
    public partial class CokieTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string s = "ASE-Association";
            string shortString = s.Substring(0, s.IndexOf("-"));
            Label1.Text = s.Substring(0, s.IndexOf("-"));

            //if (!IsPostBack)
            //{

            //    DataTable dt = getData();

            //    dt.DefaultView.Sort= "UserName DESC";

            //    DropDownList1.DataSource = dt.DefaultView.ToTable();
            //    DropDownList1.DataTextField = "UserName";
            //    DropDownList1.DataValueField = "UserId";
            //    DropDownList1.DataBind();

            //}
            //{
            //    HttpCookie cookieRepData = Request.Cookies["my_cookie_name"];
            //    if (cookieRepData != null)
            //    {
            //        string strCookieVal = (string)cookieRepData.Value;      //strCookieVal is NOT null!
            //        if (strCookieVal != null)
            //        {
            //            //Got cookie OK
            //        }
            //    }
            //}
        }
        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            string strData = "123";


            HttpCookie cookie = new HttpCookie("my_cookie_name", strData);
            cookie.Expires = DateTime.Now.AddYears(1);
            Response.SetCookie(cookie);

            Response.Redirect("~/report.aspx?type=" + DropDownList1.SelectedValue);
        }

        DataTable getData()
        {
            return new DataTable();
        }

        protected void lnkLogoutUser_Click(object sender, EventArgs e)
        {
            Session.Abandon();
        }
    }
}