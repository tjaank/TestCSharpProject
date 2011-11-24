using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;

public partial class Log : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Random rnd = new Random();
        int itemCount = rnd.Next(1, 3);
        string equation = getRandomLogEquation(itemCount);
        lblRandomEquation.Text = equation;
        lblResult.Text = solveLogEquation(equation, itemCount);
    }
    string getRandomLogEquation(int n)
    {
        StringBuilder equationBuilder = new StringBuilder();
        Random rnd = new Random();
        char[] operators = new char[] { '+', '-', '*', '/' };
        int logItemType;
        string[] logItems = new string[n];

        for (int i = 0; i < n; i++)
        {
            logItemType = rnd.Next(1, 3);
            switch (logItemType)
            {
                case 1: //plain
                    logItems[i] = "log(" + rnd.Next(1, 50).ToString() + ")";
                    break;
                case 2: // multiple
                    logItems[i] = rnd.Next(1, 9).ToString() + ".log(" + rnd.Next(1, 50).ToString() + ")";
                    break;
                default:
                    break;
            }
        }
        char[] appOperator = new char[n - 1];


        for (int j = 0; j < n - 1; j++)
        {
            appOperator[j] = operators[rnd.Next(0, 3)];
        }

        for (int i = 0; i < n - 1; i++)
        {

            equationBuilder.Append(logItems[i]);
            equationBuilder.Append(appOperator[i]);
        }
        equationBuilder.Append(logItems[n - 1]);



        return equationBuilder.ToString();
    }
    //currently working only for 2 items equation
    string solveLogEquation(string equation, int itemCount)
    {
        string result = string.Empty;
        int firstBraceOpenIndex = equation.IndexOf("(");
        int firstBraceCloseIndex = equation.IndexOf(")");
        double item1 = Convert.ToDouble(equation.Substring(firstBraceOpenIndex + 1, firstBraceCloseIndex - firstBraceOpenIndex - 1));
            int dotCount = equation.Count(f => f == '.');

        if (itemCount > 1)
        {
            string operand = equation.Substring(equation.IndexOf(")") + 1, 1);
            int secondBraceOpenIndex = equation.LastIndexOf("(");
            int secondBraceCloseIndex = equation.LastIndexOf(")");

            double item2 = Convert.ToDouble(equation.Substring(secondBraceOpenIndex + 1, secondBraceCloseIndex - secondBraceOpenIndex - 1));


            if (dotCount > 0)
            {
                int firstIndexOfDot = equation.IndexOf('.');
                if (firstIndexOfDot < equation.IndexOf(")"))
                {
                    double multiplier = Convert.ToDouble(equation.Substring(0, firstIndexOfDot));

                    item1 = Math.Pow(item1, multiplier);
                }
                else
                {
                    double multiplier = Convert.ToDouble(equation.Substring(equation.IndexOf(operand) + 1, 1));
                    item2 = Math.Pow(item2, multiplier);
                }

                if (dotCount > 1)
                {
                    int lastIndexOfDot = equation.LastIndexOf('.');
                    if (lastIndexOfDot < equation.IndexOf(")"))
                    {
                        double multiplier = Convert.ToDouble(equation.Substring(0, lastIndexOfDot));

                        item1 = Math.Pow(item1, multiplier);
                    }
                    else
                    {
                        double multiplier = Convert.ToDouble(equation.Substring(equation.IndexOf(operand) + 1, 1));
                        item2 = Math.Pow(item2, multiplier);
                    }
                }
            }

            switch (operand)
            {
                case "-":
                    result = Math.Log10(item1 / item2).ToString();
                    break;
                case "+":
                    result = (Math.Log10(item1) + Math.Log10(item2)).ToString();
                    break;
                case "*":
                case ".":
                    result = (Math.Log10(item1) * Math.Log10(item2)).ToString();
                    break;
                case "/":
                    result = (Math.Log10(item1) / Math.Log10(item2)).ToString();
                    break;
            }
        }
        else
        {
            if (dotCount > 0)
            {
                int firstIndexOfDot = equation.IndexOf('.');
                if (firstIndexOfDot < equation.IndexOf(")"))
                {
                    double multiplier = Convert.ToDouble(equation.Substring(0, firstIndexOfDot));
                    item1 = Math.Pow(item1, multiplier);
                }
            }
            result = Math.Log10(item1).ToString();
        }

        return result;
    }
}
