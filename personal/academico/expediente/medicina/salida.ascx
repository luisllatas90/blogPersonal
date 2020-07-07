<%@ Control Language="VB" AutoEventWireup="false" CodeFile="salida.ascx.vb" Inherits="medicina_salidas" %>
<table cellspacing="0" width="100%">
    <tr id="fila" runat="server">
        <td align="center" style="width: 30px; height: 30px">
            <asp:Label ID="LblNum" runat="server" style="font-size: 8pt; font-family: verdana"></asp:Label></td>
        <td style="font-size: 12pt; font-family: Times New Roman">
            <asp:Label ID="LblAlumno" runat="server" style="font-size: 8pt; font-family: verdana"></asp:Label>
            <asp:HiddenField ID="HidenCodAlu" runat="server" />
        </td>
        <td align="center" style="width: 100px">
            <asp:Label ID="LblIngreso" runat="server" style="font-size: 8pt; font-family: verdana"></asp:Label></td>
        <td align="center" style="width: 100px">
            <asp:Label ID="LblCondicion" runat="server" Style="font-size: 8pt; font-family: verdana"></asp:Label></td>
        <td style="width: 250px">
            <asp:TextBox ID="TxtObservaciones" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" Width="230px"></asp:TextBox></td>
    </tr>
</table>
