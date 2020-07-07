<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmtematica.aspx.vb" Inherits="DirectorDepartamento_frmtematica" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Invetigacion :: Actualizar Tematica</title>
     <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../../../private/funciones.js"></script>
    <script language="JavaScript" src="../../../../private/tooltip.js"></script>
    <script language="JavaScript" src="../private/validainvestigaciones.js"></script>
</head>
<body style="margin-top: 0px; padding-top: 0px">
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td align="right" colspan="3" style="background-color: #f0f0f0">
                    <asp:Button ID="CmdSalir" runat="server" CssClass="remove_prp" Height="24px" OnClientClick="javascript: window.close(); return false"
                        Text="    Finalizar" Width="82px" /><asp:Button ID="CmdGuardar" runat="server" CssClass="guardar3"
                            Height="15px" Text="     Guardar" ValidationGroup="guardar" Width="72px" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
        <table style="width: 100%" class="contornotabla">
            <tr>
                <td style="font-weight: bold; font-size: 11pt; color: navy; font-family: verdana; height: 30px" align="center" colspan="2">
                    <asp:Label ID="LblTitulo" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    &nbsp;Nombre de Temática</td>
                <td>
                    &nbsp;<asp:TextBox ID="TxtTematica" runat="server" Style="border-right: black 1px solid;
                        border-top: black 1px solid; font-size: 9pt; border-left: black 1px solid; color: navy;
                        border-bottom: black 1px solid; font-family: verdana" Width="383px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtTematica"
                        ErrorMessage="Ingrese titulo de temática" SetFocusOnError="True" ValidationGroup="guardar">*</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td>
                    &nbsp;Propósito de Temática</td>
                <td>
                    &nbsp;<asp:TextBox ID="TxtProposito" runat="server" BorderColor="Black" BorderStyle="Solid"
                        BorderWidth="1px" Font-Size="9pt" ForeColor="Navy" Height="113px" TextMode="MultiLine"
                        Width="387px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtProposito"
                        ErrorMessage="Ingrese proposito de la temática" SetFocusOnError="True" ValidationGroup="guardar">*</asp:RequiredFieldValidator><br />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="LblError" runat="server"></asp:Label><br />
                </td>
            </tr>
        </table>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="guardar" />
    </form>
</body>
</html>
