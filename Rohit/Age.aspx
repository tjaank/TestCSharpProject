<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Age.aspx.cs" Inherits="TestApplication.Rohit.Age" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div dir="ltr" style="text-align: left;" trbidi="on">
            <div dir="ltr" style="text-align: left;" trbidi="on">
                It always been fun playing with numbers. And when these numbers are about your birth
                date, it becomes even more interesting. So I thought to write a post about having
                fun with your birth date.<br />
                <br />
                Have you ever wondered, how many days old are you? When you will be 10,000 days
                old? or when will be your 11111st day on earth?<br />
                <br />
                Now it become easier, put your birthday in the textbox and desired day in the other,
                you will know the details as you want.<br />
                <br />
                Have fun!<br />
                <br />
                <br />
            </div>
            <script language="JavaScript">
<!--
                function lifetimer() {
                    today = new Date();
                    BirthDay = new Date(document.getElementById('age').value);
                    //alert(BirthDay);
                    kdays = document.getElementById('kdays').value;
                    if (isNaN(kdays)) { kdays = 0; }
                    msPerDay = 24 * 60 * 60 * 1000;
                    timeold = (today.getTime() - BirthDay.getTime());
                    e_daysold = timeold / msPerDay;
                    daysold = Math.floor(e_daysold);
                    //kdays = Math.floor(daysold / 1000) + 1;
                    //nextk = BirthDay.getTime() + (1000 * kdays * msPerDay);
                    nextk = BirthDay.getTime() + (kdays * msPerDay);
                    nextkdate = new Date(nextk);

                    document.getElementById('time1').value = daysold;
                    //document.live.kdays.value = kdays;
                    nextkyear = nextkdate.getYear();
                    if (nextkyear < 1900) nextkyear = nextkyear + 1900; //NS requires this adjustment
                    nextkString = 1 + nextkdate.getMonth() + "/" + nextkdate.getDate() + "/" + nextkyear;
                    if (kdays < 1) nextkString = "";
                    document.getElementById('kdate').value = nextkString;
                    return false;
                }        

//-->
            </script>
            <br />
            <div align="center">
                <table align="center" bgcolor="#CCCCCC" border="0" cellpadding="5" width="100%">
                    <tbody>
                        <tr>
                            <td class="center">
                                Enter date of birth in format mm/dd/yyyy:&nbsp;<input class="form" maxlength="25"
                                    id="age" name="age" size="10" type="text" value="12/31/1999" />
                                <input class="form" name="start1" onclick="javascript:return lifetimer();" type="button"
                                    value="GO!" />
                            </td>
                        </tr>
                        <tr>
                            <td class="center" nowrap="">
                                You are
                                <input class="form" maxlength="256" id="time1" name="time1" size="6" type="text" value="        " />
                                days old today.
                            </td>
                        </tr>
                        <tr>
                            <td class="center" nowrap="">
                                You will be
                                <input id="kdays" name="kdays" size="7" type="text" value="Enter Days" />
                                days old on
                                <input id="kdate" name="kdate" size="10" type="text" /><br />
                                <small>Enter number</small>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
