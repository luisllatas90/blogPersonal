<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmenviararchivo.aspx.vb" Inherits="aulavirtual_frmenviararchivo"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Enviar trabajos</title>
     <link rel="STYLESHEET"  href="../../private/estilo.css" />
	<script>
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
<body>
    <form id="form1" runat="server">
    <h4>Enviar trabajos</h4>
    <table width="100%" id="tblDatos">
            <tr>
                <td width="30%">
                    <asp:Label ID="LblUbicacion" runat="server" Text="Ubicacion de Archivo<br>(5 megas máx.)"></asp:Label>
                </td>
                    <td colspan="2" width="70%">
                        <asp:FileUpload ID="FileArchivo" runat="server" Font-Size="9pt" Width="95%" />
                        <asp:RequiredFieldValidator ID="ValidarSubir" runat="server" ControlToValidate="FileArchivo"
                            ErrorMessage="Seleccione el documento a publicar">*</asp:RequiredFieldValidator>
                    </td>
            </tr>
            <tr>
                <td colspan="2" width="100%">&nbsp;Comentario</td>
             </tr>
             <tr>
                <td colspan="2" width="100%">
                    <asp:TextBox ID="TxtComentario" runat="server" Height="70px" TextMode="MultiLine"
                        Width="100%" Font-Size="9pt" MaxLength="255"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" width="100%">
                    <asp:Label ID="LblMensaje" runat="server" Font-Size="11pt" ForeColor="Red"></asp:Label>&nbsp;</td>
            </tr>

            <tr id="mensaje">
                <td align="center" colspan="2" width="100%">
                    <asp:Button ID="CmdGuardar" runat="server" CssClass="guardar" Text="     Enviar tarea"
                        Width="100px" EnableViewState="False" OnClientClick="OcultarTabla()"/>&nbsp;
                    <asp:Button ID="CmdCancelar" runat="server" CssClass="salir" OnClientClick="javascript: window.close(); return false;"
                        Text="    Cancelar" Width="78px" />
                </td>
            </tr>
        </table>
	<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </form>
    <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
	    <tr>
	    <td width="100%" align="center" class="usatTitulo" bgcolor="#FEFFE1">
	    Procesando<br>
	    Por favor espere un momento...<br>
	    <img border="0" src="../../images/cargando.gif" width="209" height="20">
	    </td>
	    </tr>
    </table>
</body>
</html>
