using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Rohit_Aug_08_a : System.Web.UI.Page
{
    int v1;
    int v2;
    int v3;
    int v4;
    string returnval = "";
    string rndnumber;


    void LeaveFunction()
    {
        DateTime DateOnWhichLeaveIsRequired = Convert.ToDateTime("20/8/2011");
        DateTime DateOfJoining = Convert.ToDateTime("get value from database");
        DateTime TwoWeeksBeforeDateOfLeave = DateOnWhichLeaveIsRequired.AddDays(-14);
        TimeSpan DateDifference = DateOfJoining - TwoWeeksBeforeDateOfLeave;

        int numbersOfWeek = NumberOfWeeks(DateOfJoining, TwoWeeksBeforeDateOfLeave);

        if (numbersOfWeek >= 10)
        {
            //accept
        }

        else
        {

            //reject
        }   

    }
    int NumberOfWeeks(DateTime dateFrom, DateTime dateTo)
    {
        TimeSpan Span = dateTo.Subtract(dateFrom);

        if (Span.Days <= 7)
        {
            if (dateFrom.DayOfWeek > dateTo.DayOfWeek)
            {
                return 2;
            }

            return 1;
        }

        int Days = Span.Days - 7 + (int)dateFrom.DayOfWeek;
        int WeekCount = 1;
        int DayCount = 0;

        for (WeekCount = 1; DayCount < Days; WeekCount++)
        {
            DayCount += 7;
        }

        return WeekCount;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {


        Insert_user();
        System.Threading.Thread.Sleep(50);
        Insert_user2();
        System.Threading.Thread.Sleep(50);
        Insert_user3();
        System.Threading.Thread.Sleep(50);
        Insert_user4();
        System.Threading.Thread.Sleep(50);

        Label1.Text = v1.ToString();

        Label2.Text = v2.ToString();

        Label3.Text = v3.ToString();

        Label4.Text = v4.ToString();
        Label5.Text = Convert.ToInt32(Label1.Text) + "/" + Convert.ToInt32(Label2.Text);
        Label7.Text = Convert.ToInt32(Label3.Text) + "/" + Convert.ToInt32(Label4.Text);


        //////HERE I WANT RANDOM OPERATORS.......

        Random rnd = new Random();
        string[] strOperators = new string[] { "+", "-" };
        string thisOperator = strOperators[rnd.Next(0, 2)];
        switch (thisOperator)
        {
            case "+":
                Label6.Text = "+";
                Label8.Text = Convert.ToString(((double)v1 / (double)v2) + ((double)v3 / (double)v4));
                break;
            case "-":
                Label6.Text = "-";
                Label8.Text = Convert.ToString(((double)v1 / (double)v2) - ((double)v3 / (double)v4));
                break;
            default:
                break;
        }



    }

    public string recieptno(string rec)
    {
        int i = 0;
        int j = 0;
        int r = 0;

        Random ran = null;
        string str = null;
        str = "";
        ran = new Random();
        i = ran.Next(1, 1);
        for (j = 1; j <= i; j++)
        {
            do
            {
                r = ran.Next(-9, 9);
            } while (r == 0);


            if (r < 65)
            {
            }
            str = str + r;
        }
        string recc = str;
        return recc;
    }

    public string recieptno2(string rec2)
    {
        int i = 0;
        int j = 0;
        int r2 = 0;

        Random ran2 = null;
        string str2 = null;
        str2 = "";
        ran2 = new Random();
        i = ran2.Next(1, 1);
        for (j = 1; j <= i; j++)
        {
            do
            {
                r2 = ran2.Next(1, 9);
            } while (r2 == 0);


            if (r2 < 65)
            {
            }
            str2 = str2 + r2;
        }
        string reccc = str2;
        return reccc;
    }

    private void Insert_user()
    {
        rndnumber = recieptno(returnval);
        v1 = Convert.ToInt32(rndnumber);
    }

    private void Insert_user2()
    {
        rndnumber = recieptno2(returnval);
        v2 = Convert.ToInt32(rndnumber);
    }
    private void Insert_user3()
    {
        rndnumber = recieptno2(returnval);
        v3 = Convert.ToInt32(rndnumber);
    }

    private void Insert_user4()
    {
        rndnumber = recieptno2(returnval);
        v4 = Convert.ToInt32(rndnumber);
    }
}
