using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Delegate_WebUserControl : System.Web.UI.UserControl
{
    private string menuId = string.Empty;

    #region Property
    public string MenuID
    {
        set
        {
            menuId = value;
        }
    }

    #endregion 

    // This function is to be written in the user control
    public void BindMenuData()
    {
        SqlCommand sqlCmd = new SqlCommand();
        sqlCmd.CommandText = "select * from [YourTableName] Where MenuID = @MenuID";
        sqlCmd.Parameters.AddWithValue("@MenuID", menuId);
        sqlCmd.CommandType = CommandType.Text;

        SqlConnection sqlCOn = new SqlConnection("Connection String.. bla bla");
        sqlCOn.Open();
        SqlDataAdapter adapter = new SqlDataAdapter(sqlCmd);
        DataSet ds = new DataSet();
        adapter.Fill(ds);
        sqlCOn.Close();
        Menu1.DataSource = ds;
        Menu1.DataBind();
    }  

}
