<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmsubirarchivo.aspx.vb" Inherits="Investigador_frmsubirarchivo" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Investigaciones :: Subir Avances</title>
     <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../../../private/funciones.js"></script>
    <script language="JavaScript" src="../../../../private/tooltip.js"></script>
    
    <script type="text/javascript">
        function OcultarTabla()
        {
            if (document.form1.FileArchivo.value!="")
            {
                document.all.tblDatos.style.display="none"
                document.all.tblmensaje.style.display=""                
            }
        }
     </script>
    
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
    
        <table id="tblDatos" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td align="left" colspan="3" style="height: 34px; background-color: #f0f0f0">
                    &nbsp;
                    <asp:Button ID="CmdGuardar" runat="server" CssClass="guardar3" Text="     Guardar"
                        ToolTip="Registrar Avances de Investigacion" Width="72px" /></td>
            </tr>
            <tr>
                <td align="center" colspan="3" rowspan="2">
                    <table class="contornotabla" style="width: 317px; height: 154px">
                        <tr>
                            <td colspan="3" style="font-weight: bold; font-size: 11pt; color: navy; font-family: verdana;
                                height: 28px; text-align: center">
                                <asp:Label ID="LblTituloFrm" runat="server"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 112px; height: 27px">
                                &nbsp;Titulo</td>
                            <td align="left" style="width: 257px; height: 27px">
                                <asp:Label ID="LblTitulo" runat="server" ForeColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 112px; height: 6px">
                                &nbsp;Fechas</td>
                            <td align="left" style="width: 257px; height: 6px">
                                Inicio :<asp:Label ID="LblFecIni" runat="server" ForeColor="Black"></asp:Label>&nbsp;
                                Termino :<asp:Label ID="LblFecFin" runat="server" ForeColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="left" style="width: 112px; height: 13px">
                                &nbsp;Avance</td>
                            <td align="left" style="width: 257px; height: 13px">
                                <asp:FileUpload ID="FileArchivo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Height="18px" Width="423px" /><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                        runat="server" ControlToValidate="FileArchivo" ErrorMessage="Solo puede subir archivos con extension *.zip, *.rar, *.pdf, *.doc"
                                        SetFocusOnError="True" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.rar|.RAR|.zip|.ZIP|.doc|.DOC|.pdf|.PDF)$">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                            ID="RequiredFieldValidator4" runat="server" ControlToValidate="FileArchivo" ErrorMessage="Debe seleccionar un archivo para subir."
                                            SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3" style="height: 22px">
                                <asp:Label ID="LblMensaje" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
        
        <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
	    <tr>
	    <td width="100%" align="center" class="usatTitulo" bgcolor="#FEFFE1">
	    Procesando<br>
	    Por favor espere un momento...<br>
	    <img alt='imagen' border="0" src="../../../../images/cargando.gif" width="209" height="20">
	    </td>
	    </tr>
    </table>
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
  

    </form>
</body>
</html>
