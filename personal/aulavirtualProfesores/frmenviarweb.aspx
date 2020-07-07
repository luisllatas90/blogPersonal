<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmenviarweb.aspx.vb" Inherits="aulavirtual_frmenviarweb"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Enviar tareas</title>
     <link rel="STYLESHEET"  href="../../private/estilo.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%">
            <tr>
                <td colspan="3" rowspan="3" style="font-size: 11pt; color: #330000; font-family: verdana; font-variant: normal; font-weight: bold;">
                    &nbsp;Enviar archivo de tarea</td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
            <tr>
                <td colspan="3" rowspan="1">
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                &nbsp;Ubicacion de Archivo</td>
                            <td colspan="2">
                                &nbsp;<asp:FileUpload ID="FileArchivo" runat="server" Font-Size="9pt" Width="435px" />
                                <asp:RequiredFieldValidator ID="ValidarSubir" runat="server" ControlToValidate="FileArchivo"
                                    ErrorMessage="Seleccione el documento a publicar">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;Comentario</td>
                            <td colspan="2">
                                &nbsp;<asp:TextBox ID="TxtComentario" runat="server" Height="75px" TextMode="MultiLine"
                                    Width="435px" Font-Size="9pt" MaxLength="255"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <asp:Label ID="LblMensaje" runat="server" Font-Size="11pt" ForeColor="Red"></asp:Label>&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3" rowspan="1">
                    <asp:Button ID="CmdGuardar" runat="server" CssClass="guardar" Text="     Enviar tarea"
                        Width="100px" />&nbsp;
                    <asp:Button ID="CmdCancelar" runat="server" CssClass="salir" OnClientClick="javascript: window.close(); return false;"
                        Text="    Cancelar" Width="78px" /></td>
            </tr>
        </table>
    
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server"
            ShowMessageBox="True" ShowSummary="False" />
        &nbsp;
        <asp:HiddenField ID="HidenArchivo" runat="server" />
    </form>
</body>
</html>
