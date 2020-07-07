<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmtareausuario.aspx.vb" Inherits="aulavirtual_frmtareausuario"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Enviar trabajos</title>
     <link rel="STYLESHEET"  href="../../private/estilo.css" />
	<script type="text/javascript" language="javascript">
        function OcultarTabla()
        {
            if (document.form1.FileArchivo.value!="")
            {
                document.all.form1.style.display="none"
                document.all.tblmensaje.style.display=""                
           }
        }
     </script>


</head>
<body style="margin-top: 5px;background-color: #F2F2F2">
<form id="form1" runat="server">
<asp:Button ID="cmdGuardar" runat="server" CssClass="guardar_prp" Text="         Enviar" OnClientClick="OcultarTabla()"/>
<asp:Button ID="cmdCancelar" runat="server" CssClass="noconforme1" OnClientClick="javascript: window.close(); return false;" Text="        Cancelar" />
<br/><br/>
<table style="width: 100%;" class="contornotabla" cellpadding="3" id="tblDatos">
            <tr>
                            <td width="20%">&nbsp;<asp:Label ID="LblUbicacion" runat="server" Text="Ubicacion de Archivo (5 megas máx.)" Width="128px"></asp:Label>
                            <td width="80%">
                                &nbsp;<asp:FileUpload ID="FileArchivo" runat="server" Font-Size="9pt" Width="95%" />
                                <asp:RequiredFieldValidator ID="ValidarSubir" runat="server" ControlToValidate="FileArchivo"
                                    ErrorMessage="Seleccione el documento a publicar">*</asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td width="20%" colspan="2">&nbsp;Observaciones</td>
			</tr>
			<tr>
                            <td width="80%" colspan="2">
                                &nbsp;<asp:TextBox ID="TxtComentario" runat="server" Height="150px" TextMode="MultiLine"
                                    Width="98%" Font-Size="9pt" MaxLength="1000"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td width="100%" align="center" colspan="2">
                                <asp:Label ID="LblMensaje" runat="server" Font-Size="11pt" ForeColor="Red"></asp:Label>&nbsp;</td>
            </tr>
        </table>
	<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </form>
    <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none;height:100%;width:100%;"  class="contornotabla">
	    <tr>
	    <td style="background-color: #FEFFE1" align="center" class="usatTitulo" >
	    Procesando<br/>
	    Por favor espere un momento...<br/>
	    <img src="../../images/cargando.gif" width="209" height="20"/>
	    </td>
	    </tr>
    </table>
</body>
</html>
