<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmarea.aspx.vb" Inherits="DirectorDepartamento_frmarea" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Investigacion :: Ingresar Area</title>
    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../../../private/funciones.js"></script>
    <script language="JavaScript" src="../../../../private/tooltip.js"></script>
    <script language="JavaScript" src="../private/validainvestigaciones.js"></script>
    <STYLE type="text/css">
BODY {
scrollbar-face-color:#AED9F4;
scrollbar-highlight-color:#FFFFFF;
scrollbar-3dlight-color:#FFFFFF;
scrollbar-darkshadow-color:#FFFFFF;
scrollbar-shadow-color:#FFFFFF;
scrollbar-arrow-color:#000000;

scrollbar-track-color:#FFFFFF;
}
a:link {text-decoration: none; color: #00080; }
a:visited {text-decoration: none; color: #000080; }
a:hover {text-decoration: none; black; }
a:hover{color: black; text-decoration: none; }
</STYLE>
</head>
<body style="margin-top:0px">
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3" align="right" valign="baseline" style="background-color: #f0f0f0">
                    <asp:Button ID="CmdSalir" runat="server" Text="    Finalizar" CssClass="remove_prp" Height="24px" Width="82px" OnClientClick="javascript: window.close(); return false" />
                    <asp:Button ID="CmdGuardar" runat="server" Text="     Guardar" CssClass="guardar3" Width="72px" Height="15px" ValidationGroup="guardar" /></td>
            </tr>
            <tr>
                <td colspan="3" rowspan="2">
                    <table width="100%" class="contornotabla">
                        <tr>
                            <td colspan="3" style="font-weight: bold; font-size: 11pt; color: navy; font-family: verdana; height: 30px" align="center">
                                <asp:Label ID="LblTitulo" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;Nombre de Area</td>
                            <td colspan="2">
                                <asp:TextBox ID="TxtNomArea" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" ForeColor="Navy" Width="413px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtNomArea"
                                    ErrorMessage="Ingrese Nombre de Área" SetFocusOnError="True" ValidationGroup="guardar">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;Propósito del Área</td>
                            <td colspan="2">
                                <asp:TextBox ID="TxtProposito" runat="server" TextMode="MultiLine" Width="413px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" ForeColor="Navy" Height="54px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtProposito"
                                    ErrorMessage="Ingrese un proposito del área" SetFocusOnError="True" ValidationGroup="guardar">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;Buscar Coordinador</td>
                            <td colspan="2">
                                <asp:TextBox ID="TxtBuscar" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Size="9pt" ForeColor="Navy"></asp:TextBox>&nbsp;
                                <asp:Button ID="CmdBuscar" runat="server" Text="      Buscar" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="buscar1" ValidationGroup="buscar" Width="66px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtBuscar"
                                    ErrorMessage="Ingrese criterio de busqueda" SetFocusOnError="True" ValidationGroup="buscar" Enabled="False">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;Coordinador</td>
                            <td colspan="2">
                                <asp:ListBox ID="LstCoordinador" runat="server" Width="413px" Font-Size="8pt" ForeColor="Navy" Rows="6"></asp:ListBox>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validacoordi"
                                    ErrorMessage="Seleccione un coordinador" ValidationGroup="guardar">*</asp:CustomValidator></td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center">
                                &nbsp;<asp:Label ID="LblError" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="buscar" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="guardar" />
    </form>
</body>
</html>
