<%@ Page Language="VB" AutoEventWireup="false" CodeFile="agregacomentario.aspx.vb" Inherits="DirectorDepartamento_agregacomentario" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Investigaciones :: Agregar Comentario</title>
     <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../../../private/funciones.js"></script>
    <script language="JavaScript" src="../../../../private/tooltip.js"></script>
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
<body style="margin-top:0px" >
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td align="left" colspan="3" style="background-color: #f0f0f0; height: 30px;">
                    &nbsp;
                    <asp:Button ID="CmdGuardar" runat="server" Text="     Guardar" CssClass="guardar3" Width="72px" ToolTip="Guardar los comentarios realizados a una investigación" /></td>
            </tr>
            <tr>
                <td colspan="3" rowspan="2" align="center">
                    <table class="contornotabla">
                        <tr>
                            <td colspan="3" style="font-weight: bold; font-size: 11pt; color: navy; font-family: verdana; height: 28px; text-align: center">
                                <asp:Label ID="LblTitulo" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;Asunto</td>
                            <td colspan="2">
                                <asp:TextBox ID="TxtAsunto" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Size="8pt" ForeColor="Navy" Width="400px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtAsunto"
                                    ErrorMessage="Ingrese un asunto al comentario" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;Observación</td>
                            <td colspan="2">
                                <asp:TextBox ID="TxtObservacion" runat="server" Rows="6" TextMode="MultiLine" Width="400px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Font-Size="8pt" ForeColor="Navy" Height="94px" MaxLength="2000"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtObservacion"
                                    ErrorMessage="Ingrese un texto en observacion" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <asp:Label ID="LblMensaje" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
