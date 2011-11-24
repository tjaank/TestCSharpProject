<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Async.aspx.cs" Inherits="TestApplication.Sysnet.Async" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div><asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
        <asp:UpdatePanel ID="uppnl" runat="server">
            <ContentTemplate>
                <table>
                    <thead>
                        
                        <tr>
                            <th>
                                jQuery BIND
                            </th>
                            <th>
                                jQuery LIVE
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr valign="top">
                            <td style="width: 150px">
                                <asp:Button ID="btnNormal" runat="server" Text="Hover over me" />
                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        $("#<%= btnNormal.ClientID %>").bind("mouseover", function () {
                                            $("#result").append("entered at: " +
                            (new Date()).toLocaleTimeString());
                                        });
                                    });        
                </script>
                            </td>
                            <td>
                                <asp:Button ID="btnLive" runat="server" Text="Hover over me aswell" />
                                <script type="text/javascript">
                                    $(document).ready(function () {
                                        $("#<%= btnLive.ClientID %>").live("mouseover", function () {
                                            $("#resultLive").append("entered at: " +
                            (new Date()).toLocaleTimeString());
                                        });
                                    });        
                </script>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <table>
            <tr valign="top">
                <td style="width: 150px">
                    <div id="result">
                    </div>
                </td>
                <td>
                    <div id="resultLive">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
