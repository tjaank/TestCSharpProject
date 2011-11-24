<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SPManagement.aspx.cs" Inherits="StoreProcedureManagement.SPManagement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stored Procedure Management</title>
</head>
<body>
    <form id="form1" runat="server">
<div class="container">
    <table align="left" border="0" cellpadding="0" cellspacing="0" style="width: 900px;">
        <tr style="height: 30px;">
            <td class="fv16 bold" align="center">
                Stored Procedure Management
            </td>
        </tr>
        <tr>
            <td valign="top">
                <asp:ScriptManager AsyncPostBackTimeout="216000" ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
            </td>
        </tr>
        <tr>
            <td align="center" valign="top">
                <asp:UpdatePanel ChildrenAsTriggers="true" ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="TxtAdmin" runat="server" TextMode="Password"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="h-shad1">
                                    <td class="bo">
                                        <strong>
                                            <asp:CheckBoxList Height="150px" ID="chklstDatabaseServers" RepeatColumns="5" RepeatDirection="Horizontal"
                                                runat="server">
                                            </asp:CheckBoxList>
                                            <asp:CheckBox ID="chkAll" onclick="return checkAllClients();" runat="server" Text="Select All" />
                                        </strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                            <ProgressTemplate>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:RadioButtonList ID="rdobtnlstExecutionMode" RepeatDirection="Horizontal" runat="server">
                                            <asp:ListItem Text="Create" Value="1"></asp:ListItem>
                                            <asp:ListItem Selected="True" Text="Alter" Value="2"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b style="font-size: large;">Stored Procedure Name: </b>
                                        <asp:TextBox ID="txtSPName" runat="server" Width="99%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b style="font-size: large;">SP Parameters: </b>(comma separated parameters with
                                        data type, just copt paste only SP Parameters from SQL Server Management Studio)
                                        <b style="color: red;">Note: Nullable parameters are not allowed in this version of
                                            SP Management i.e. (@Student_Code INT = NULL) is not allowed.</b>
                                        <asp:TextBox Height="200px" ID="txtSPParams" runat="server" TextMode="MultiLine"
                                            Width="99%"></asp:TextBox>>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b style="font-size: large;">SP Body: </b>(Do not include parameters or create/alter
                                        statement, just write body of your stored procedure)
                                        <asp:TextBox Height="600px" ID="txtSPBody" runat="server" TextMode="MultiLine" Width="99%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnExecute" OnClick="btnExecute_Click" runat="server" Style="font-size: medium;
                                            font-weight: bold;" Text="Execute" type="submit" Width="150px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                            <ProgressTemplate>
                                                <img align="middle" alt="" src="http://www.blogger.com/images/Loading-Text-Animation.gif" /></ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Label Font-Bold="true" ID="totalResultsCount" runat="server"></asp:Label>
                                        <div style="overflow: auto; width: 900px;">
                                            <table align="left" border="0" cellpadding="0" cellspacing="0" id="dynamicTable"
                                                runat="server">
                                                <tbody>
                                                </tbody>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <div id="divSuccess" runat="server" style="color: green;">
                                        </div>
                                        <div id="divError" runat="server" style="color: red;">
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</div>
    </form>
</body>
</html>
