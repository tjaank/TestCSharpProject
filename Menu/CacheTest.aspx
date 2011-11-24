<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CacheTest.aspx.cs" Inherits="CacheTest" %>
<%@ OutputCache CacheProfile="CacheFor60Seconds" VaryByParam="name" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
     <form id="form1" runat="server">
    <div>
    <b>This page is output cached with a cache profile.</b>
    <br /><br />
    Hello <%= Server.HtmlEncode(name.Text) %>!
    <br /><br /> 
    The time right now is <%= DateTime.Now.ToString() %>
    <br /><br />
    Name: <asp:TextBox Runat="server" Id="name" />
    <asp:Button ID="Button1" Runat="server" Text="Refresh" />
    </div>
    </form>
</body>
</html>
