using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace TestApplication
{
    public partial class test2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GenerateGrid(2, 3);
        }
        private void GenerateGrid(int plotNumber, int roadNumber)
        {

            ViewState["plotNumber"] = plotNumber;

            Table t = new Table();
            t.EnableViewState = true;

            t.ID = "tblPlot";
            TableRow tr;
            TableRow trh = new TableRow();

            Label plotNo = new Label();
            plotNo.Text = "Plot No";
            TableCell hc = new TableCell();
            hc.Controls.Add(plotNo);
            trh.Cells.Add(hc);

            Literal RoadNo = new Literal();
            RoadNo.Text = "Road No";
            TableCell hc1 = new TableCell();
            hc1.Controls.Add(RoadNo);
            trh.Cells.Add(hc1);

            Literal size = new Literal();
            size.Text = "Plot Size";
            TableCell hc2 = new TableCell();
            hc2.Controls.Add(size);
            trh.Cells.Add(hc2);

            Literal facing = new Literal();
            facing.Text = "Facing";
            TableCell hc3 = new TableCell();
            hc3.Controls.Add(facing);
            trh.Cells.Add(hc3);

            t.Rows.Add(trh);

            for (int i = 0; i < plotNumber; i++)
            {
                tr = new TableRow();

                TableCell tc = new TableCell();
                Label ltlPloatNumber = new Label();
                ltlPloatNumber.ID = "ltl" + i.ToString();
                ltlPloatNumber.Text = hdn.Value + "-" + (i + 1).ToString();

                tc.Controls.Add(ltlPloatNumber);
                tr.Cells.Add(tc);

                DropDownList ddlRoad = new DropDownList();
                ddlRoad.Items.Add(new ListItem("", ""));
                for (int j = 0; j < roadNumber; j++)
                {
                    ddlRoad.Items.Add(new ListItem("R-" + (j + 1).ToString(), "R-" + (j + 1).ToString()));
                }
                TableCell tc1 = new TableCell();

                ddlRoad.ID = "ddl" + i.ToString();

                RequiredFieldValidator rfv = new RequiredFieldValidator();
                rfv.ID = "rfv" + i.ToString();
                rfv.ControlToValidate = "ddl" + i.ToString();
                rfv.ValidationGroup = "plot";
                rfv.ErrorMessage = "*";

                tc1.Controls.Add(ddlRoad);
                tc1.Controls.Add(rfv);
                tr.Cells.Add(tc1);

                TextBox txtSize = new TextBox();
                TableCell tc2 = new TableCell();
                txtSize.ID = "txt" + i.ToString();
                RequiredFieldValidator rfv1 = new RequiredFieldValidator();
                rfv1.ID = "rfvt" + i.ToString();
                rfv1.ControlToValidate = "txt" + i.ToString();
                rfv1.ValidationGroup = "plot";
                rfv1.ErrorMessage = "*";

                tc2.Controls.Add(txtSize);
                tc2.Controls.Add(rfv1);
                tr.Cells.Add(tc2);

                DropDownList ddlFaceing = new DropDownList();
                ddlFaceing.Items.Add(new ListItem("", ""));
                ddlFaceing.Items.Add(new ListItem("South", "S"));
                ddlFaceing.Items.Add(new ListItem("North", "N"));
                ddlFaceing.Items.Add(new ListItem("East", "E"));
                ddlFaceing.Items.Add(new ListItem("West", "W"));
                ddlFaceing.Items.Add(new ListItem("SouthWest", "SW"));
                ddlFaceing.Items.Add(new ListItem("SouthEast", "SE"));
                ddlFaceing.Items.Add(new ListItem("NortWest", "NW"));
                ddlFaceing.Items.Add(new ListItem("NorthEast", "NE"));

                TableCell tc3 = new TableCell();
                ddlFaceing.ID = "ddlf" + i.ToString();


                RequiredFieldValidator rfv2 = new RequiredFieldValidator();
                rfv2.ID = "rfvf" + i.ToString();
                rfv2.ControlToValidate = "ddlf" + i.ToString();
                rfv2.ValidationGroup = "plot";
                rfv2.ErrorMessage = "*";

                tc3.Controls.Add(ddlFaceing);
                tc3.Controls.Add(rfv2);
                tr.Cells.Add(tc3);
                t.Rows.Add(tr);
            }

            pnlPlotEntry.Controls.Add(t);
            hdnTableID.Value = t.ClientID;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Table tbl = (Table)pnlPlotEntry.FindControl(hdnTableID.Value);
        }
    }
}