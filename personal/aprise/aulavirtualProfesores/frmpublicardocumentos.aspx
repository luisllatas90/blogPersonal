<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmpublicardocumentos.aspx.vb" Inherits="frmpublicardocumentos" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Publicar documentos</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css">
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
<body style="margin-top: 5px;background-color: #F2F2F2">
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
                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.pdf|.PDF|.rar|.zip|.doc|.xls|.pps|.ppt|.RAR|.ZIP|.DOC|.XLS|.PPS|.PPT|.swf|.SWF)$">*</asp:RegularExpressionValidator>
                </td>
        </tr>
        <tr>
            <td align="center" rowspan="1">
                <asp:FileUpload ID="FileUpload2" runat="server" Width="350px" style="border-right: black 1px solid; border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy; border-bottom: black 1px solid; font-family: verdana" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="FileUpload2"
                    ErrorMessage="Solo puede subir archivos con extension *.pdf" SetFocusOnError="True"
                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.pdf|.PDF|.rar|.zip|.doc|.xls|.pps|.ppt|.RAR|.ZIP|.DOC|.XLS|.PPS|.PPT|.swf|.SWF)$">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="center" rowspan="1">
                <asp:FileUpload ID="FileUpload3" runat="server" Width="350px" style="border-right: black 1px solid; border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy; border-bottom: black 1px solid; font-family: verdana" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="FileUpload3"
                    ErrorMessage="Solo puede subir archivos con extension *.pdf" SetFocusOnError="True"
                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.pdf|.PDF|.rar|.zip|.doc|.xls|.pps|.ppt|.RAR|.ZIP|.DOC|.XLS|.PPS|.PPT|.swf|.SWF)$">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="center" rowspan="1">
                <asp:FileUpload ID="FileUpload4" runat="server" Width="350px" style="border-right: black 1px solid; border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy; border-bottom: black 1px solid; font-family: verdana" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="FileUpload4"
                    ErrorMessage="Solo puede subir archivos con extension *.pdf" SetFocusOnError="True"
                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.pdf|.PDF|.rar|.zip|.doc|.xls|.pps|.ppt|.RAR|.ZIP|.DOC|.XLS|.PPS|.PPT|.swf|.SWF)$">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="center" rowspan="1" style="height: 21px">
                <asp:FileUpload ID="FileUpload5" runat="server" Width="350px" style="border-right: black 1px solid; border-top: black 1px solid; font-size: 8pt; border-left: black 1px solid; color: navy; border-bottom: black 1px solid; font-family: verdana" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="FileUpload5"
                    ErrorMessage="Solo puede subir archivos con extension *.pdf" SetFocusOnError="True"
                    ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.pdf|.PDF|.rar|.zip|.doc|.xls|.pps|.ppt|.RAR|.ZIP|.DOC|.XLS|.PPS|.PPT|.swf|.SWF)$">*</asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="center" rowspan="1" valign="middle">           
                <asp:Button ID="CmdEnviar" runat="server" CssClass="guardar_prp" Text="        Guardar"/>
                <asp:Button ID="CmdCancelar" runat="server" CssClass="noconforme1" OnClientClick="javascript: window.close(); return false;" Text="        Cancelar" />
                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validarsubida" ErrorMessage="Seleccione al menos 1 archivo para registrar.">*</asp:CustomValidator></td>
        </tr>
    </table>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
	<asp:Label ID="LblMensaje" runat="server" Font-Size="12pt" ForeColor="Red" Font-Bold="True"></asp:Label>
    </center>
    </form>
     <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none;height:100%;width:100%;"  class="contornotabla">
	    <tr>
	    <td style="background-color: #FEFFE1" align="center" class="usatTitulo" >
	    Procesando<br/>
	    Por favor espere un momento...<br/>
	    <img alt="cargando..." src="../../images/cargando.gif" width="209" height="20"/>
	    </td>
	    </tr>
    </table>
</body>
</html>
