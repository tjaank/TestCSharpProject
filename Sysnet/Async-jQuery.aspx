<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Async-jQuery.aspx.cs" Inherits="TestApplication.Sysnet.Async_jQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js"></script>
    <link href="Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "Async-jQuery.aspx/GetFeedburnerItems",
                // Pass the "Count" parameter, via JSON object.
                data: "{'Count':'7'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    BuildTable(msg.d);
                }
            });
        });
        function BuildTable(msg) {
            var table = '<table><thead><tr><th>Date</th><th>Title</th><th>Excerpt</th></thead><tbody>';

            for (var post in msg) {
                var row = '<tr>';

                row += '<td>' + msg[post].Date + '</td>';
                row += '<td><a href="' + msg[post].Link + '">' + msg[post].Title + '</a></td>';
                row += '<td>' + msg[post].Description + '</td>';

                row += '</tr>';

                table += row;
            }

            table += '</tbody></table>';

            $('#Container').html(table);
            $('#Container').removeClass('loading');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Container" class="loading" style="height:200px;">
    &nbsp;
    </div>
    </form>
</body>
</html>
