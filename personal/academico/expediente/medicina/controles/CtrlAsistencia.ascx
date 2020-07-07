<%@ Control Language="VB" AutoEventWireup="false" CodeFile="CtrlAsistencia.ascx.vb" Inherits="CtrlAsistencia" %>

<table cellspacing="0" width="100%" cellpadding="0"  >
    <tr id="fila" runat="server" OnMouseOver="javascript:this.style.backgroundColor='#DDE2F7'" OnMouseOut="javascript:this.style.backgroundColor='#FFFFFF'">
        <td align="center" height="30" width="30">
            <asp:Label ID="LblNum" runat="server" style="font-size: 8pt; color: black; font-family: verdana"></asp:Label></td>
        <td >
            <asp:Label ID="LblAlumno" runat="server" style="font-size: 8pt; color: black; font-family: verdana"></asp:Label>
            <asp:HiddenField ID="HidenCodAlu" runat="server" />
        </td>
        <td align="center" width="40">
            <asp:CheckBox ID="ChkAsistio" runat="server" BorderColor="Transparent" ToolTip="Marque la casilla si el alumno asistió a la actividad programada" />
            <asp:Label ID="LblAsistencia" runat="server"></asp:Label>
        </td>
        <td align="center" width="80">
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
        <td align="center" width="40">
            <asp:DropDownList ID="DDLEst" runat="server" Font-Names="Verdana" 
                Font-Size="8pt">
                <asp:ListItem>A</asp:ListItem>
                <asp:ListItem>T</asp:ListItem>
                <asp:ListItem Selected="True">F</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td align="center" width="60">
            <asp:TextBox ID="TxtNota" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" MaxLength="5" Style="text-align: center"
                ToolTip="Ingrese Nota entre 0 y 20 " Width="43px"></asp:TextBox>
            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                ControlToValidate="TxtNota" 
                ErrorMessage="Solo puede ingresar valores entre 0 y 20" MaximumValue="20" 
                MinimumValue="0" Type="Double" SetFocusOnError="True">*</asp:RangeValidator>
            <asp:Image ID="ImgBloqueo" runat="server" ImageUrl="~/images/bloquear.gif" 
                ToolTip="No se ha considerado registrar notas para la actividad." />
        </td>
        <td align="center" width="100">
            <asp:TextBox ID="TxtObs" runat="server" BorderColor="Black" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Verdana" Font-Size="8pt" Style="text-align:left"
                ToolTip="Breve observacion" Width="231px"></asp:TextBox>
        </td>
    </tr>
</table>
    <asp:Image ID="ImgFoto" runat="server" Visible="False" />

