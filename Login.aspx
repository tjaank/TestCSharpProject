<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TestApplication.Sysnet.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Login ID="LoginUser" runat="server">
    <LayoutTemplate>
    <p>
        Username: <asp:TextBox runat="server" ID="Username" />
    </p>
    <p>
        Password: <asp:TextBox runat="server" ID="Password" TextMode="Password" />
    </p>
    <p>
        <asp:CheckBox ID="RememberMe" runat="server" />
    </p>
        <asp:Button ID="btnLogin" runat="server" CommandName="Login" Text="Login" />
    </LayoutTemplate>
</asp:Login>
<br />
<asp:Label ID="lblLoginErrorDetails" runat="server" />
    </div>
    </form>
</body>
</html>
