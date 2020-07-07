<%@ Control Language="VB" AutoEventWireup="false" CodeFile="notas2.ascx.vb" Inherits="medicina_notas2" %>
<table cellspacing="0" width="100%">
    <tr id="fila" runat="server">
        <td align="center" style="width: 30px; height: 30px">
            <asp:Label ID="LblNum" runat="server" Style="font-size: 8pt; font-family: verdana"></asp:Label></td>
        <td style="font-size: 12pt; font-family: Times New Roman">
            <asp:Label ID="LblAlumno" runat="server" Style="font-size: 8pt; font-family: verdana"></asp:Label>
            <asp:HiddenField ID="HidenCodAlu" runat="server" />
        </td>
        <td align="center" style="width: 80px">
            <asp:TextBox ID="TxtNota" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" MaxLength="5"
                Style="text-align: center" Width="43px">0</asp:TextBox><asp:CompareValidator ID="CompareValidator1"
                    runat="server" ControlToValidate="TxtNota" Display="Dynamic" ErrorMessage="Nota debe ser menor o igual a 20"
                    Operator="LessThanEqual" SetFocusOnError="True" Type="Double" ValueToCompare="20">*</asp:CompareValidator><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtNota" Display="Dynamic"
                        ErrorMessage="Valor de nota requerido" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        <td align="center" style="width: 80px">
            <asp:TextBox ID="TxtNota2" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" MaxLength="5"
                Style="text-align: center" Width="43px">0</asp:TextBox><asp:CompareValidator ID="CompareValidator2"
                    runat="server" ControlToValidate="TxtNota2" Display="Dynamic" ErrorMessage="Nota debe ser menor o igual a 20"
                    Operator="LessThanEqual" SetFocusOnError="True" Type="Double" ValueToCompare="20">*</asp:CompareValidator><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtNota2" Display="Dynamic"
                        ErrorMessage="Valor de nota requerido" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        <td style="width: 250px">
            <asp:TextBox ID="TxtObservaciones" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" Width="230px"></asp:TextBox></td>
    </tr>
</table>
