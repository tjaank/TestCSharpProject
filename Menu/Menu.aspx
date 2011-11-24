<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="TestApplication.Menu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="jqueryslidemenu.css" />
    <!--[if lte IE 7]>
    <style type="text/css">
    html .jqueryslidemenu{height: 1%;} /*Holly Hack for IE7 and below*/
    </style>
    <![endif]-->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.2.6/jquery.min.js"></script>
    <script type="text/javascript" src="jqueryslidemenu.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            Plain Menu Example:</div>
        <asp:Menu ID="Menu1" DataSourceID="XmlDataSource1" runat="server" BackColor="#336699"
            DynamicHorizontalOffset="2" Font-Names="Verdana" ForeColor="#CCFFFF" StaticSubMenuIndent="10px"
            StaticDisplayLevels="1" Orientation="Horizontal">
            <DataBindings>
                <asp:MenuItemBinding DataMember="MenuItem" NavigateUrlField="NavigateUrl" TextField="Text"
                    ToolTipField="ToolTip" />
            </DataBindings>
            <StaticSelectedStyle BackColor="#300000" HorizontalPadding="5px" VerticalPadding="2px" />
            <DynamicMenuStyle BackColor="#300000" />
            <DynamicSelectedStyle BackColor="#FF66CC" />
            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <DynamicHoverStyle BackColor="#336000" Font-Bold="False" ForeColor="White" />
            <StaticHoverStyle BackColor="#336000" Font-Bold="False" ForeColor="White" />
        </asp:Menu>
        <br />
        <br />
        <br />
        <div>
            Menu With CSS Example:</div>
        <asp:Menu ID="myslidemenu" DataSourceID="XmlDataSource1" runat="server" DynamicHorizontalOffset="2"
            StaticSubMenuIndent="10px" StaticDisplayLevels="1" Orientation="Horizontal" CssClass="jqueryslidemenu">
            <DataBindings>
                <asp:MenuItemBinding DataMember="MenuItem" NavigateUrlField="NavigateUrl" TextField="Text"
                    ToolTipField="ToolTip" />
            </DataBindings>
            <StaticSelectedStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <DynamicMenuStyle />
            <DynamicSelectedStyle />
            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
            <DynamicHoverStyle Font-Bold="False" />
            <StaticHoverStyle Font-Bold="False" />
        </asp:Menu>
        <asp:XmlDataSource ID="XmlDataSource1" TransformFile="~/Menu/TransformXSLT.xslt"
            XPath="MenuItems/MenuItem" runat="server"></asp:XmlDataSource>
    </div>
    </form>
</body>
</html>
