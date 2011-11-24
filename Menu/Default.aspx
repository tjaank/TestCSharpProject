<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TestApplication.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
var to; 
$().ready(function() 
{ 
    to = setTimeout("TimedOut()",);
}); 

function TimedOut() 
{ 
    $.post( "refresh_session.aspx", null, 
    function(data)
    { 
        if(data == "success") 
        { 
            to = setTimeout("TimedOut()", ); 
        }
        else 
        { 
            $("#timeout").slideDown('fast');
        } 
    }); 
}
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Decimal 1: <asp:Label ID="lblRandomDecimal1" runat="server"></asp:Label> <br/>
        Decimal 2: <asp:Label ID="lblRandomDecimal2" runat="server"></asp:Label> <br/><br/>
        Subtraction Result: <asp:Label ID="lblSubtractionResult" runat="server"></asp:Label> <br/>
       
       First Number:&nbsp;&nbsp;&nbsp;<asp:TextBox 
            ID="txt1a" runat="server" Width="19px"></asp:TextBox><strong>&nbsp;/
        </strong><asp:TextBox 
            ID="txt1b" runat="server" Width="19px"></asp:TextBox><br />
       Second Number:&nbsp;<asp:TextBox ID="txt2a" runat="server" Width="19px"></asp:TextBox>
        <strong>&nbsp;/ </strong><asp:TextBox ID="txt2b" runat="server" Width="19px"></asp:TextBox><br />

        <asp:DropDownList ID="ddlFunction" runat="server">
        <asp:ListItem Value="+" Selected="True">+</asp:ListItem>
        <asp:ListItem Value="-">-</asp:ListItem>
        <asp:ListItem Value="/">/</asp:ListItem>
        <asp:ListItem Value="*">*</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Button ID="btnDoIt" runat="server" Text="Do It" onclick="btnDoIt_Click" />


        <br />
        <br />
        Result :
        <asp:Label ID="lblResult" runat="server" style="font-weight: 700"></asp:Label>


    </div>
    </form>
</body>
</html>
