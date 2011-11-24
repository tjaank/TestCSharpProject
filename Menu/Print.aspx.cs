using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Print : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int[] arr = new int[] { 2, 1, 4, 3, 2 };
        StringBuilder toDisplay = new StringBuilder();
        foreach (int thisInt in arr)
        {
            for (int i = 0; i <= thisInt; i++)
            {
                toDisplay.Append("*");
            }
            toDisplay.Append("<br />");
        }
        divDisplay.InnerHtml = toDisplay.ToString();
    }
}
