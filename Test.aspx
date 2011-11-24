<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="TestApplication.Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script type="text/javaScript">
        function disableselect(e) {
            return false
        }
        function reEnable() {
            return true
        }
        document.onselectstart = new Function("return false")
        if (window.sidebar) {
            document.onmousedown = disableselect
            document.onclick = reEnable
        }
    </script>
    <script type="text/javascript">
         var validFilesTypes = ["bmp", "gif", "png", "jpg", "jpeg", "doc", "xls"];
         function ValidateFile() {
             var file = document.getElementById("<%=FileUpload1.ClientID%>");
             var label = document.getElementById("<%=Label1.ClientID%>");
             var path = file.value;
             var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
             var isValidFile = false;
             for (var i = 0; i < validFilesTypes.length; i++) {
                 if (ext == validFilesTypes[i]) {
                     isValidFile = true;
                     break;
                 }
             }
             if (!isValidFile) {
                 label.style.color = "red";
                 label.innerHTML = "Invalid File. Please upload a File with" +
    " extension:\n\n" + validFilesTypes.join(", ");
             }
             return isValidFile;
         }
    </script>
<script type="text/javascript">
    function extractNumber(obj, decimalPlaces, allowNegative) {
        var temp = obj.value;

        // avoid changing things if already formatted correctly
        var reg0Str = '[0-9]*';
        if (decimalPlaces > 0) {
            reg0Str += '\\.?[0-9]{0,' + decimalPlaces + '}';
        } else if (decimalPlaces < 0) {
            reg0Str += '\\.?[0-9]*';
        }
        reg0Str = allowNegative ? '^-?' + reg0Str : '^' + reg0Str;
        reg0Str = reg0Str + '$';
        var reg0 = new RegExp(reg0Str);
        if (reg0.test(temp)) return true;

        // first replace all non numbers
        var reg1Str = '[^0-9' + (decimalPlaces != 0 ? '.' : '') + (allowNegative ? '-' : '') + ']';
        var reg1 = new RegExp(reg1Str, 'g');
        temp = temp.replace(reg1, '');

        if (allowNegative) {
            // replace extra negative
            var hasNegative = temp.length > 0 && temp.charAt(0) == '-';
            var reg2 = /-/g;
            temp = temp.replace(reg2, '');
            if (hasNegative) temp = '-' + temp;
        }

        if (decimalPlaces != 0) {
            var reg3 = /\./g;
            var reg3Array = reg3.exec(temp);
            if (reg3Array != null) {
                // keep only first occurrence of .
                //  and the number of places specified by decimalPlaces or the entire string if decimalPlaces < 0
                var reg3Right = temp.substring(reg3Array.index + reg3Array[0].length);
                reg3Right = reg3Right.replace(reg3, '');
                reg3Right = decimalPlaces > 0 ? reg3Right.substring(0, decimalPlaces) : reg3Right;
                temp = temp.substring(0, reg3Array.index) + '.' + reg3Right;
            }
        }

        obj.value = temp;
    }
    function blockNonNumbers(obj, e, allowDecimal, allowNegative) {
        var key;
        var isCtrl = false;
        var keychar;
        var reg;

        if (window.event) {
            key = e.keyCode;
            isCtrl = window.event.ctrlKey
        }
        else if (e.which) {
            key = e.which;
            isCtrl = e.ctrlKey;
        }

        if (isNaN(key)) return true;

        keychar = String.fromCharCode(key);

        // check for backspace or delete, or if Ctrl was pressed
        if (key == 8 || isCtrl) {
            return true;
        }

        reg = /\d/;
        var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
        var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false;

        return isFirstN || isFirstD || reg.test(keychar);
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DataList ID="DataList1" runat="server" OnItemCommand="DataList1_ItemCommand"
            RepeatColumns="5" RepeatDirection="Horizontal">
            <ItemTemplate>
                <table width="100px">
                    <tr>
                        <td height="100px" style="text-align: center">
                            <asp:ImageButton ID="ImageButton1" runat="server" Height="100px" ImageUrl='<%# Eval("imagepath") %>'
                                Width="100px" CommandArgument='<%# Eval("IteamTabId") %>' CommandName="select" />
                        </td>
                    </tr>
                    <tr>
                        <td height="20px">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
        <asp:Repeater ID="Repeater2" runat="server">
            <HeaderTemplate>
            </HeaderTemplate>
            <ItemTemplate>
                <table>
                    <tr>
                        <td rowspan="3">
                            <asp:Image ID="imgNews" runat="server" ImageUrl='<%# Eval("pictID","~/pict.aspx?imgId={0}") %>'>
                            </asp:Image>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%#Eval("pictID")%>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <asp:FileUpload ID="FileUpload1" runat="server"></asp:FileUpload>
    <asp:Button ID="btnUpload" runat="server" Text="Upload"
        OnClientClick="javascript:return ValidateFile()"></asp:Button>
    <asp:DetailsView ID="DetailsView1" runat="server" Height="50px" Width="125px" AutoGenerateRows="False"
        HeaderText="Flight Details">
        <Fields>
            <asp:BoundField DataField="FlightNumber" HeaderText="Flight Number" HeaderStyle-Font-Bold="true"
                ReadOnly="True" SortExpression="FlightNumber" />
            <asp:BoundField DataField="FlightOrigin" HeaderText="Origin" HeaderStyle-Font-Bold="true"
                ReadOnly="True" SortExpression="FlightOrigin" />
            <asp:BoundField DataField="FlightDestination" HeaderText="Destination" HeaderStyle-Font-Bold="true"
                ReadOnly="True" SortExpression="FlightDestination" />
            <asp:BoundField DataField="FlightTakeOffDate" HeaderText="Take Off Date" HeaderStyle-Font-Bold="true"
                ReadOnly="True" SortExpression="FlightTakeOffDate" />
        </Fields>
    </asp:DetailsView>
<%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="TextBox1"
    ValidationExpression="\d+" Display="Static" EnableClientScript="true" ErrorMessage="Please enter numbers only"
    runat="server">
    </asp:RegularExpressionValidator>
<asp:TextBox ID="txtAge" MaxLength="3" onblur="extractNumber(this,2,false);"
    onkeypress="return blockNonNumbers(this, event, true, false);"
    onkeyup="extractNumber(this,2,false);" runat="server">
</asp:TextBox>
<asp:RequiredFieldValidator ControlToValidate="txtAge" ErrorMessage="Please Enter Age"
    ID="rfvtxtAge" runat="server">*
</asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ControlToValidate="txtAge" 
    ErrorMessage="Please Enter Valid Age" ID="revtxtAge" runat="server" 
    SetFocusOnError="true" ValidationExpression="^\d+$">*
</asp:RegularExpressionValidator>--%>
    </form>
</body>
</html>
