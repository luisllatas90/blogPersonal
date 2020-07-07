<%@ Page Language="VB" AutoEventWireup="false" CodeFile="administrarareaslineas.aspx.vb" Inherits="DirectorDepartamento_administrarareaslineas" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
     <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../../../private/funciones.js"></script>
    <script language="JavaScript" src="../../../../private/tooltip.js"></script>
</head>
<body style=" margin:0,0,0,0">
    <form id="form1" runat="server">
        <table style="width: 100%" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" colspan="2" style="font-weight: bold; font-size: 10.5pt; color: midnightblue;
                    height: 34px">
                    &nbsp;ADMINISTRAR ÁREAS - LÍNEAS - TEMÁTICAS</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="PanArea" runat="server" HorizontalAlign="Right"
                        Width="100%" BackColor="#F0F0F0" style="border-top: maroon 1px solid; border-bottom: maroon 1px solid" Enabled="False">
                        <table cellpadding="0" cellspacing="0" style="width: 41px; height: 1px">
                            <tr>
                                <td style="height: 22px">
                                    &nbsp;
                                    <asp:Button ID="CmdAgreLinea" runat="server" Text="     Agregar " CssClass="attach_prp" Height="20px" Width="105px" />&nbsp;
                                </td>
                                <td style="height: 22px">
                                    &nbsp;<asp:Button ID="CmdModArea" runat="server" Text="    Modificar " CssClass="modificar3" Height="33px" Width="112px" />&nbsp;</td>
                                <td style="height: 22px">
                                    &nbsp;<asp:Button ID="CmdElimArea" runat="server" Text="     Eliminar " CssClass="eliminar3" Height="20px" Width="105px" OnClientClick='return confirm("¿Desea Eliminar el Registro?")' /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" rowspan="2">
        <asp:TreeView ID="TreeUnidades" runat="server" ImageSet="Faq" ExpandDepth="0" ShowLines="True" LineImagesFolder="~/TreeLineImages">
            <ParentNodeStyle Font-Bold="False" />
            <HoverNodeStyle Font-Underline="True" ForeColor="Purple" />
            <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" BackColor="LightBlue" Font-Bold="True" />
            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="DarkBlue" HorizontalPadding="5px"
                NodeSpacing="0px" VerticalPadding="0px" />
        </asp:TreeView>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    </form>
</body>
</html>
