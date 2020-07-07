<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditarEvaluacion.aspx.vb" Inherits="SisSolicitudes_EditarEvaluacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
</head>
<body>
    <form id="form1" runat="server">

                                            <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                                            </ajaxToolkit:ToolkitScriptManager>

                                            <table align="center" width="100%">
                                                <tr>
                                                    <td align="left">
                                                        Fecha:
                                                        <asp:Label ID="LblFecha" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Ha resuelto:
                                                        <asp:DropDownList ID="CmbHaResuelto" runat="server">
                                                            <asp:ListItem>--Seleccione una opcion--</asp:ListItem>
                                                            <asp:ListItem>Aprobar</asp:ListItem>
                                                            <asp:ListItem>Rechazar</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="LblMensaje" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td height="20" align="left" width="100%">
                                                        Observaciones:</td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        <asp:TextBox ID="TxtObservaciones" runat="server" Rows="4" 
                                        TextMode="MultiLine" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:Button ID="CmdObservar" runat="server" CssClass="boton" Text="Observar" 
                                                            CausesValidation="False" UseSubmitBehavior="False" />
                                                        &nbsp;<asp:Button ID="CmdGuardar" runat="server" 
                                        CssClass="boton" Text="Guardar" />
                                        <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" 
                                            ConfirmText="¿Está seguro que desea guardar?" TargetControlID="CmdGuardar">
                                        </ajaxToolkit:ConfirmButtonExtender>
                                                    </td>
                                                </tr>
                                            </table>

                                            <asp:HiddenField ID="HddCodCco" runat="server" />
                                            <asp:HiddenField ID="HddCodEva" runat="server" />

                                            <asp:HiddenField ID="HddCodEob" runat="server" />

                                            <asp:HiddenField ID="HddNivel" runat="server" />

    </form>
</body>
</html>
