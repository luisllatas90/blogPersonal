<%@ Control Language="VB"  AutoEventWireup="false" CodeFile="alumno.ascx.vb" Inherits="medicina_alumno" %>
<table width="100%" cellspacing="0">
    <tr runat="server" id="fila">
        <td style="width: 30px; height: 30px;" align="center">
            <asp:Label ID="LblNum" runat="server" style="font-size: 8pt; font-family: verdana"></asp:Label></td><td>
            <asp:Label ID="LblAlumno" runat="server" style="font-size: 8pt; font-family: verdana"></asp:Label></td>
        <td style="width: 40px" align="center">
            <asp:CheckBox ID="ChkAsistio" runat="server" BorderColor="Transparent" ToolTip="Marque la casilla si el alumno asistió a la actividad programada" /></td>
        <td style="width: 100px" align="center">
            <asp:TextBox ID="TxtInicioHora" runat="server" Width="20px" style="text-align: center" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" MaxLength="2" ToolTip="Ingrese Hora de Inicio entre 0 - 24"></asp:TextBox>
            :
            <asp:TextBox ID="TxtInicioMin" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" MaxLength="2" Style="text-align: center"
                Width="20px" ToolTip="Ingrese Minutos  de Inicio entre 0 - 60"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="TxtInicioHora"
                Display="Dynamic" ErrorMessage="Hora debe estar entre 0 y 22" Operator="LessThanEqual"
                SetFocusOnError="True" Type="Integer" ValueToCompare="23">*</asp:CompareValidator><asp:CompareValidator
                    ID="CompareValidator3" runat="server" ControlToValidate="TxtInicioMin" Display="Dynamic"
                    ErrorMessage="Minuto debe estar entre 0 - 59" Operator="LessThanEqual" SetFocusOnError="True"
                    Type="Integer" ValueToCompare="59">*</asp:CompareValidator>
        </td>
        <td style="width: 100px" align="center">
            <asp:TextBox ID="TxtFinalHora" runat="server" Width="20px" style="text-align: center" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" MaxLength="2" ToolTip="Ingrese Hora de Inicio entre 0 - 24"></asp:TextBox>
            :
            <asp:TextBox ID="TxtFinalMin" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" MaxLength="2" Style="text-align: center"
                Width="20px" ToolTip="Ingrese Minutos de Inicio entre 0 - 60"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="TxtFinalHora"
                Display="Dynamic" ErrorMessage="Hora debe estar entre 0 y 22" Operator="LessThanEqual"
                SetFocusOnError="True" Type="Integer" ValueToCompare="23">*</asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="TxtFinalMin"
                Display="Dynamic" ErrorMessage="Minuto debe estar entre 0 - 59" Operator="LessThanEqual"
                SetFocusOnError="True" Type="Integer" ValueToCompare="59">*</asp:CompareValidator></td>
        <td style="width: 250px">
            <asp:TextBox ID="TxtObservaciones" runat="server" Width="230px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt"></asp:TextBox></td>
    </tr>
</table>
