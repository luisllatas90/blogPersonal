<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Requerimiento.aspx.vb" Inherits="Usuario_Requerimiento" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Datos Requerimiento</title>
    <link href ="../private/estilo.css" rel ="stylesheet" type ="text/css" />
    <link href ="../private/estiloweb.css" rel ="stylesheet" type ="text/css" />
</head>
<body>
    <form id="frmRequerimiento" runat="server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3" style="text-align: center">
                    <table width="100%" cellspacing="2">
                        <tr>
                            <td class="TituloReq" colspan="3" align="center">
                                Datos de Requerimiento</td>
                        </tr>
                        <tr>
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 86px; height: 63px" valign="top" align="left">
                                Requerimiento</td>
                            <td style="width: 8px; height: 63px" valign="top">
                                :</td>
                            <td style="vertical-align: text-top; height: 63px; width: 689px;" valign="top" 
                                align="left">
                                <asp:TextBox ID="TxtRequerimiento" runat="server" EnableTheming="True" Rows="3" TextMode="MultiLine"
                                    Width="466px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtRequerimiento"
                                    ErrorMessage="Debe ingresar el requerimiento" ValidationGroup="Guardar">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 86px; height: 44px" valign="top" align="left">
                                Prioridad</td>
                            <td style="width: 8px; height: 44px" valign="top">
                                :</td>
                            <td style="vertical-align: text-top; height: 44px; width: 689px;" valign="top" 
                                align="left">
                                <asp:DropDownList ID="CboPrioridad" runat="server" Width="265px">
                                    <asp:ListItem Value="-1">--Seleccione Prioridad--</asp:ListItem>
                                    <asp:ListItem Value="1">Muy Baja</asp:ListItem>
                                    <asp:ListItem Value="2">Baja</asp:ListItem>
                                    <asp:ListItem Value="3">Media</asp:ListItem>
                                    <asp:ListItem Value="4">Alta</asp:ListItem>
                                    <asp:ListItem Value="5">Muy Alta</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="CboPrioridad"
                                    ErrorMessage="Seleccione Prioridad" Operator="GreaterThanEqual" Type="Integer"
                                    ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 86px; height: 40px" valign="top" align="left">
                                Tipo</td>
                            <td style="width: 8px; height: 40px" valign="top">
                                :</td>
                            <td style="vertical-align: text-top; height: 40px; width: 689px;" valign="top" 
                                align="left">
                                <asp:DropDownList ID="CboTipo" runat="server" Width="265px">
                                </asp:DropDownList>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="CboTipo"
                                    ErrorMessage="Seleccione Tipo" Operator="GreaterThanEqual" Type="Integer" ValidationGroup="Guardar"
                                    ValueToCompare="0">*</asp:CompareValidator></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 16px; text-align: center">
                                <asp:Button ID="CmdGuardar" runat="server" CssClass="guardar" Height="20px" Text="Guardar"
                                    Width="85px" ValidationGroup="Guardar" />
                                &nbsp; &nbsp; &nbsp; &nbsp;
                                <asp:Button ID="CmdCancelar" runat="server" CssClass="cancelar" Height="20px" Text="Cancelar"
                                    Width="85px" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
    
    </div>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="Guardar" />
    </form>
</body>
</html>
