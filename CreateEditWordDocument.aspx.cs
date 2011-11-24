using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestApplication
{
    public partial class CreateEditWordDocument : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                //creating object for missing value
                object missing = System.Reflection.Missing.Value;
                //object for end of file
                object endofdoc = "\\endofdoc";

                //creating instance of word application
                Microsoft.Office.Interop.Word._Application w = new Microsoft.Office.Interop.Word.Application();
                //creating instance of word document
                Microsoft.Office.Interop.Word._Document doc;
                //setting status of application to visible
                w.Visible = true;
                //creating new document
                doc = w.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                //adding paragraph to document
                Microsoft.Office.Interop.Word.Paragraph para1;
                para1 = doc.Content.Paragraphs.Add(ref missing);
                object styleHeading1 = "Heading 1";
                para1.Range.set_Style(ref styleHeading1);
                para1.Range.Text = "Heading One";
                para1.Range.Font.Bold = 1;
                para1.Format.SpaceAfter = 24;
                para1.Range.InsertParagraphAfter();
                //creating second paragraph
                Microsoft.Office.Interop.Word.Paragraph para2;
                para2 = doc.Content.Paragraphs.Add(ref missing);
                para2.Range.Text = "Heading OneHeading OneHeading OneHeading OneHeading OneHeading OneHeading" + '\n' + "OneHeading OneHeading OneHeading OneHeading OneHeading OneHeading One";
                para2.Range.Font.Bold = 1;
                para2.Format.SpaceAfter = 24;
                para2.Range.InsertParagraphAfter();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "error", "javascript:;alert('" + ex.Message + "')");
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //creating instance of word application
            Microsoft.Office.Interop.Word._Application w = new Microsoft.Office.Interop.Word.Application();
            object path = @"C:\table.doc";
            object read = "ReadWrite";
            object readOnly = false;
            object o = System.Reflection.Missing.Value;
            //opening document
            Microsoft.Office.Interop.Word._Document oDoc = w.Documents.Open(ref path, ref o, ref readOnly, ref o, ref o, ref o, ref o, ref o, ref o, ref o, ref o, ref o, ref o, ref o, ref o, ref o);

            try
            {
                //loop for each paragraph in document
                foreach (Microsoft.Office.Interop.Word.Paragraph p in oDoc.Paragraphs)
                {
                    Microsoft.Office.Interop.Word.Range rng = p.Range;
                    Microsoft.Office.Interop.Word.Style styl = rng.get_Style() as Microsoft.Office.Interop.Word.Style;
                    //checking if document containg table
                    if ((bool)rng.get_Information(Microsoft.Office.Interop.Word.WdInformation.wdWithInTable)
                                        == true)
                    {
                        //loop for each cell in table
                        foreach (Microsoft.Office.Interop.Word.Cell c in rng.Cells)
                        {
                            if (rng.Cells.Count > 0)
                            {
                                //checking for desired field in table
                                if (c.Range.Text.ToString().Contains("ID"))
                                    //editing values in tables.
                                    c.Next.Range.Text = "1";
                                if (c.Range.Text.ToString().Contains("Name"))
                                    c.Next.Range.Text = "Haider";
                                if (c.Range.Text.ToString().Contains("Address"))
                                    c.Next.Range.Text = "Allahabad";
                            }
                        }
                        //saving document
                        oDoc.Save();
                    }
                }
                //closing document
                oDoc.Close(ref o, ref o, ref o);
            }
            catch (Exception ex)
            {
                oDoc.Close(ref o, ref o, ref o);
                ClientScript.RegisterStartupScript(this.GetType(), "error", "javascript:;alert('" + ex.Message + "')");
            }

        }
    }
}