<%@ Page Language="VB" AutoEventWireup="false" CodeFile="subir.aspx.vb" Inherits="subir" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Investigaciones Alumno :: Subir Archivos</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css">
     <script type="text/javascript" >
     
     function validarsubida(source, arguments)
        {
        if (form1.FileUpload1.value=="" && form1.FileUpload2.value=="" && form1.FileUpload3.value=="" && form1.FileUpload4.value=="" && form1.FileUpload5.value=="")
            arguments.IsValid = false
        else
            arguments.IsValid = true
        }

       function OcultarTabla()
            {
              document.all.tblDatos.style.display="none"
              document.all.tblmensaje.style.display=""              
            }
</script>
</head>
<body>
    <form id="form1" runat="server" target="_self" onsubmit="return OcultarTabla();">
   <center>
    <table id="tblDatos">
        <tr>
            <td rowspan="1" align="right">
                </td>
        </tr>
        <tr>
            <td rowspan="1" align="center"><asp:FileUpload ID="FileUpload1" runat="server" Width="350px" style="border-right: black 1px solid; border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy; border-bottom: black 1px solid; font-family: verdana" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileUpload1"
                    ErrorMessage="Solo puede subir archivos con extension *.pdf" SetFocusOnError="True"
                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.pdf|.PDF)$">*</asp:RegularExpressionValidator>
                </td>
        </tr>
        <tr>
            <td align="center" rowspan="1">
                <asp:FileUpload ID="FileUpload2" runat="server" Width="350px" style="border-right: black 1px solid; border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy; border-bottom: black 1px solid; font-family: verdana" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="FileUpload2"
                    ErrorMessage="Solo puede subir archivos con extension *.pdf" SetFocusOnError="True"
                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.pdf|.PDF)$">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="center" rowspan="1">
                <asp:FileUpload ID="FileUpload3" runat="server" Width="350px" style="border-right: black 1px solid; border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy; border-bottom: black 1px solid; font-family: verdana" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="FileUpload3"
                    ErrorMessage="Solo puede subir archivos con extension *.pdf" SetFocusOnError="True"
                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.pdf|.PDF)$">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="center" rowspan="1">
                <asp:FileUpload ID="FileUpload4" runat="server" Width="350px" style="border-right: black 1px solid; border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy; border-bottom: black 1px solid; font-family: verdana" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="FileUpload4"
                    ErrorMessage="Solo puede subir archivos con extension *.pdf" SetFocusOnError="True"
                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.pdf|.PDF)$">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="center" rowspan="1" style="height: 21px">
                <asp:FileUpload ID="FileUpload5" runat="server" Width="350px" style="border-right: black 1px solid; border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy; border-bottom: black 1px solid; font-family: verdana" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="FileUpload5"
                    ErrorMessage="Solo puede subir archivos con extension *.pdf" SetFocusOnError="True"
                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.pdf|.PDF)$">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="center" rowspan="1" valign="middle">
                <input id="Reset1" type="reset" onclick="window.close();" value="            Cancelar" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; width: 88px; border-bottom: black 1px solid; height: 30px" class="noconforme1" />
                <asp:Button ID="CmdEnviar"  runat="server" Text="            Guardar" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="guardar_prp" Height="30px" Width="94px" />
                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validarsubida"
                    ErrorMessage="Seleccione al menos 1 archivo para registrar.">*</asp:CustomValidator></td>
        </tr>
    </table>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
    </center>

    </form>
    <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
	    <tr>
	    <td width="100%" align="center" class="usatEtiqOblig" bgcolor="#FEFFFF">
	    Procesando<br/>
	    Por favor espere un momento...<br/>
	    <img border="0" src="../../../images/cargando.gif" width="209" height="20"/>
	    </td>
	    </tr>
    </table>
    
</body>
</html>
