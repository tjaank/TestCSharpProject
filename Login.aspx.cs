using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace TestApplication.Sysnet
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
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
                //LoginUser.LoginError.Visible = "True";

                if (userInfo == null)
                {
                    lblLoginErrorDetails.Text = "There is no user in the database with the username " + LoginUser.UserName + ". Please try again.";
                }
                else
                {
                    if (!userInfo.IsApproved)
                    {

                        lblLoginErrorDetails.Text = "Your account has not yet been approved. Please try again later.";
                    }
                    else if (userInfo.IsLockedOut)
                    {
                        lblLoginErrorDetails.Text = "Your account has been locked out due to maximum incorrect login attempts. Please contact the site administrator.";
                    }
                    else
                    {
                        lblLoginErrorDetails.Text = "Your password is incorrect, please try again.";
                    }
                }
            }
        }
    }
}