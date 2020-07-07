<%@ Page Language="VB" AutoEventWireup="false" CodeFile="subirfactura.aspx.vb" Inherits="logistica_subirfactura" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Logistica</title>
    <link rel="STYLESHEET"  href="../../private/estilo.css" />
    <script src="../../private/funciones.js"></script>
	<script>
        function OcultarTabla()
        {
            if (document.form1.FileImagen.value!="")
            {
                document.all.tblDatos.style.display="none"
                document.all.tblmensaje.style.display=""                
           }
        }
     </script>
</head>
<body style="background-color:#D4D0C8">
    <form id="form1" runat="server">
    <center>
        <table runat="server" id="tblDatos">
            <tr>
                <td>
                    Tipo Documento</td>
                <td colspan="2" style="font-weight: bold; background-color: white">
                    :
                    <asp:Label ID="LblTipDoc" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    Numero de Documento</td>
                <td colspan="2" style="font-weight: bold; background-color: white">
                    :
                    <asp:Label ID="LblNumDoc" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    Proveedor</td>
                <td colspan="2" style="font-weight: bold; background-color: white">
                    :
                    <asp:Label ID="LblPro" runat="server" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    Fecha Documento</td>
                <td colspan="2" style="font-weight: bold; background-color: white">
                    :
                    <asp:Label ID="LblFecDoc" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    Fecha Registro</td>
                <td colspan="2" style="font-weight: bold; background-color: white">
                    :
                    <asp:Label ID="LblFecReg" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    Destino</td>
                <td colspan="2" style="font-weight: bold; background-color: white">
                    :
                    <asp:Label ID="LblDes" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    Adjuntar Imagen</td>
                <td style="width: 326px">
                    :
                    <asp:FileUpload ID="FileImagen" runat="server" Width="301px" /><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Debe seleccionar solo archivos JPG"
                        ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpg|.JPG)$" ControlToValidate="FileImagen" SetFocusOnError="True">*</asp:RegularExpressionValidator><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileImagen"
                        ErrorMessage="Debe de seleccionar un archivo de imagen." SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                <td>
                    <asp:Button ID="CmdGuardar" runat="server" Text="Guardar" /></td>
                    
            </tr>
            <tr>
                <td align="right" colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="3" >
                    &nbsp;<asp:Image ID="ImgFactura"  runat="server" Width="426px" /></td>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" />
    
    
     <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
	    <tr>
	    <td width="100%" align="center" class="usatTitulo" bgcolor="#FEFFE1">
	    Procesando<br>
	    Por favor espere un momento...<br>
	    <img border="0" src="../../images/cargando.gif" width="209" height="20">
	    </td>
	    </tr>
    </table>
    
</center>    
    </form>
    
    
</body>
</html>
