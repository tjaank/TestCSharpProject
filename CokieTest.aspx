<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CokieTest.aspx.cs" Inherits="TestApplication.CokieTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="http://jqueryjs.googlecode.com/files/jquery-1.2.6.min.js"></script>
    <script type="text/javascript">
        function logoutUser() {
            $.post("Logout.aspx", null,
    function (data) {
        if (data == "done") {
            window.location.href = "newpage.aspx";
        }
    });
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="Button1" runat="server" OnClick="ButtonSubmit_Click" Text="Button" />
        <br />
        <br />
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink1" runat="server">Logout</asp:HyperLink>
        <a href="#" id="adeslogueo" runat="server" onclick="javascript:logoutUser;">Cerrar Sesión</a>
        <li><a href="javascript:void(0);" id="cambiaClave" onclick="cargaModClave1()">
            <asp:Label ID="lblaccesousu2" runat="server" Text="Cambiar Clave" CssClass="AccesoUsuarios" /></a></li>
        <li><asp:LinkButton ID="lnkLogoutUser" runat="server" onclick="lnkLogoutUser_Click">Cerrar Sesión</asp:LinkButton></li>
        <!-- Session.Abandon() -->
    </div>
    </form>
</body>
</html>
