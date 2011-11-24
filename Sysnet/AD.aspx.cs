using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;

namespace TestApplication.Sysnet
{
    public partial class AD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //System.Security.Principal.WindowsIdentity wi = System.Security.Principal.WindowsIdentity.GetCurrent();
            //string[] a = Context.User.Identity.Name.Split('\\');

            //System.DirectoryServices.DirectoryEntry ADEntry = new System.DirectoryServices.DirectoryEntry("WinNT://" + a[0] + "/" + a[1]);
            //string Name = ADEntry.Properties["FullName"].Value.ToString();

            //foreach (string Key in ADEntry.Properties.PropertyNames)
            //{

            //    string sPropertyValues = String.Empty;
            //    foreach (object Value in ADEntry.Properties[Key])
            //        sPropertyValues += Convert.ToString(Value) + ";";

            //    sPropertyValues = sPropertyValues.Substring(0, sPropertyValues.Length - 1);
            //    Response.Write(Key + "=" + sPropertyValues);
            //}
            string defaultNamingContext;
            using (DirectoryEntry rootDSE = new DirectoryEntry("LDAP://KHIADS01.SYSNET.COM"))
            {
                defaultNamingContext = rootDSE.Properties["defaultNamingContext"].Value.ToString();
            }
            Response.Write(defaultNamingContext);
        }
    }
}