using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        bool isMember = AuthenticateUser(LoginUser.UserName, LoginUser.Password, LoginUser.RememberMeSet);

        if (isMember)
        {
            FormsAuthentication.RedirectFromLoginPage(LoginUser.UserName, LoginUser.RememberMeSet);
        }
    }
    private bool AuthenticateUser(string userName, string password, bool rememberUserName)
    {
        string userName1 = "code";
        string password1 = "shode";

        if (userName.Equals(userName1) && password.Equals(password1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // Handles LoginUser.LoginError
    protected void Login1_LoginError(object sender, System.EventArgs e)
    {
        if (LoginUser.UserName == string.Empty & LoginUser.Password == string.Empty)
        {
            lblLoginErrorDetails.Text = "Please enter your username and password.";
        }
        else if (LoginUser.UserName == string.Empty)
        {
            lblLoginErrorDetails.Text = "Please enter your username.";
        }
        else if (LoginUser.Password == string.Empty)
        {
            lblLoginErrorDetails.Text = "Please enter your password.";
        }
        else
        {
            MembershipUser userInfo = Membership.GetUser(LoginUser.UserName);
            //LoginError.Visible = "True";
            if (userInfo == null)
            {
                lblLoginErrorDetails.Text = "There is no user in the database " + 
                    "with the username " + LoginUser.UserName + ". Please try again.";
            }
            else
            {
                if (!userInfo.IsApproved)
                {
                    lblLoginErrorDetails.Text = "Your account has not yet been " + 
                        "approved. Please try again later.";
                }
                else if (userInfo.IsLockedOut)
                {
                    lblLoginErrorDetails.Text = "Your account has been locked " + 
                        "out due to maximum incorrect login attempts. Please " + 
                        "contact the site administrator.";
                }
                else
                {
                    lblLoginErrorDetails.Text = "Your password is incorrect, " + 
                        "please try again.";
                }
            }
        }
    }
}
