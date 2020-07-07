<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ingreso.ascx.vb" Inherits="medicina_ingreso" %>
<table cellspacing="0" width="100%">
    <tr id="fila" runat="server">
        <td align="center" style="width: 30px; height: 30px">
            <asp:Label ID="LblNum" runat="server" style="font-size: 8pt; color: black; font-family: verdana"></asp:Label></td>
        <td style="font-size: 12pt; font-family: Times New Roman">
            <asp:Label ID="LblAlumno" runat="server" style="font-size: 8pt; color: black; font-family: verdana"></asp:Label>
            <asp:HiddenField ID="HidenCodAlu" runat="server" />
        </td>
        <td align="center" style="width: 40px">
            <asp:CheckBox ID="ChkAsistio" runat="server" BorderColor="Transparent" ToolTip="Marque la casilla si el alumno asistió a la actividad programada" />
            <asp:Label ID="LblAsistencia" runat="server"></asp:Label>
        </td>
        <td align="center" style="width: 100px">
            <asp:TextBox ID="TxtInicioHora" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" MaxLength="2" Style="text-align: center"
                ToolTip="Ingrese Hora de Inicio entre 0 - 24" Width="20px"></asp:TextBox>
            :
            <asp:TextBox ID="TxtInicioMin" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" MaxLength="2" Style="text-align: center"
                ToolTip="Ingrese Minutos  de Inicio entre 0 - 60" Width="20px"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="TxtInicioHora"
                Display="Dynamic" ErrorMessage="Hora debe estar entre 0 y 22" Operator="LessThanEqual"
                SetFocusOnError="True" Type="Integer" ValueToCompare="23">*</asp:CompareValidator><asp:CompareValidator
                    ID="CompareValidator3" runat="server" ControlToValidate="TxtInicioMin" Display="Dynamic"
                    ErrorMessage="Minuto debe estar entre 0 - 59" Operator="LessThanEqual" SetFocusOnError="True"
                    Type="Integer" ValueToCompare="59">*</asp:CompareValidator>
        </td>
    </tr>
</table>
