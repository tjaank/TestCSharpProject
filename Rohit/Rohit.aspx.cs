using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

public partial class Rohit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string equation = getRandomEquation(5);
        lblRandomEquation.Text = equation;
        NCalc.Expression ex = new NCalc.Expression(equation);
        lblResult.Text = (ex.Evaluate().ToString());
    }
    string getRandomEquation(int n)
    {
        StringBuilder equationBuilder = new StringBuilder();
        Random rnd = new Random();
        char[] operators = new char[] { '+', '-', '*', '/' };
        bool isBraces = rnd.Next(0, 2) == 1 ? true : false;
        bool isBracesOpened = false;
        int rndBracesLocation = rnd.Next(0, n - 1);

        int[] nums = new int[n];

        for (int i = 0; i < n; i++)
        {
            nums[i] = rnd.Next(1, 99);
        }

        char[] appOperator = new char[n - 1];


        for (int j = 0; j < n - 1; j++)
        {
            appOperator[j] = operators[rnd.Next(0, 3)];
        }

        for (int i = 0; i < n - 1; i++)
        {
            if ((isBraces && i == rndBracesLocation) || (isBracesOpened))
            {
                if (isBracesOpened)
                {
                    equationBuilder.Append(nums[i]);
                    equationBuilder.Append(")");
                    equationBuilder.Append(appOperator[i]);
                    isBracesOpened = false;
                }
                else
                {
                    equationBuilder.Append("(");
                    equationBuilder.Append(nums[i]);
                    equationBuilder.Append(appOperator[i]);
                    isBracesOpened = true;
                }

                isBraces = false;
            }
            else
            {
                equationBuilder.Append(nums[i]);
                equationBuilder.Append(appOperator[i]);
            }
        }
        equationBuilder.Append(nums[n - 1]);

        string equ = equationBuilder.ToString();
        if (equ.Contains("(") && !equ.Contains(")"))
            equ += ")";

        return equ;
    }
}