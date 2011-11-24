<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BulkEmail.aspx.cs" Inherits="TestApplication.BulkEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Recipient(s):<br />
    <asp:TextBox ID="txtRecipient" runat="server" Height="50px" Width="525px" 
            TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Button ID="btnRecipientFromDB" runat="server" 
            onclick="btnRecipientFromDB_Click" Text="Recipient From Database" />
        <br />
        <asp:CheckBoxList ID="chklstRecipientsFromDB" runat="server" Visible="False">
        </asp:CheckBoxList><asp:Button ID="btnAddSelected" runat="server" 
            Text="Add Selected" onclick="btnAddSelected_Click" Visible="False" />
        
        <br />
        Subject:<br />
    <asp:TextBox ID="txtSubject" runat="server" Height="18px" Width="522px"></asp:TextBox>
    
        <br />
        <br />
        Email Body:<br />
        <asp:TextBox ID="txtEmailBody" runat="server" Height="290px" Width="520px" 
            TextMode="MultiLine"></asp:TextBox>
        <br />
        <asp:Button ID="btnSend" runat="server" onclick="btnSend_Click" 
            Text="Send Email" />
    
    </div>
    </form>
</body>
</html>
