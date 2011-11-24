<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Async-Repeater.aspx.cs" Inherits="TestApplication.Sysnet.Async_Repeater" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="revolver.js" type="text/javascript"></script>
    <link href="Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="rpAlerts">
        <div class="row">
            <div class="left">
                
                <a href="{D:Url}">{D:Text}</a>
            </div>
            <div class="middle">
                {D:Text}
            </div>
            <div class="right">
                {D:Url}
            </div>
        </div>
    </div>
    </table>
    <script type="text/javascript">
        $(document).ready(function () {
            rpItemsDataBind('#rpAlerts', 'Async-Repeater.aspx/GetAlerts');
        });
    </script>
    </form>
</body>
</html>
