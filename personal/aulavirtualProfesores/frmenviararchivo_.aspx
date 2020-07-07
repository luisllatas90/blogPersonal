<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmenviararchivo.aspx.vb" Inherits="aulavirtual_frmenviararchivo"%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Enviar tareas</title>
     <link rel="STYLESHEET"  href="../../private/estilo.css" />
	<script>
        function OcultarTabla()
        {
            if (document.form1.FileArchivo.value!="")
            {
                document.all.tblDatos.style.display="none"
                document.all.tblmensaje.style.display=""                
           }
        }
     </script>


</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%" id="tblDatos">
            <tr>
                <td colspan="3" rowspan="3" style="font-size: 11pt; color: #330000; font-family: verdana; font-variant: normal; font-weight: bold;">
                    &nbsp;Enviar archivo de tarea</td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
            <tr>
                <td colspan="3" rowspan="1" align="center">
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
                                &nbsp;<asp:TextBox ID="TxtComentario" runat="server" Height="48px" TextMode="MultiLine"
                                    Width="435px" Font-Size="9pt" MaxLength="255"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <asp:Label ID="LblMensaje" runat="server" Font-Size="11pt" ForeColor="Red"></asp:Label>&nbsp;</td>
                        </tr>
                    </table>
                    </td>
            </tr>
            <tr id="mensaje">
                <td align="center" colspan="3" rowspan="1">
                    <asp:Button ID="CmdGuardar" runat="server" CssClass="guardar" Text="     Enviar tarea"
                        Width="100px" EnableViewState="False" OnClientClick="OcultarTabla()"/>&nbsp;
                    <asp:Button ID="CmdCancelar" runat="server" CssClass="salir" OnClientClick="javascript: window.close(); return false;"
                        Text="    Cancelar" Width="78px" /></td>
            </tr>
        </table>
	<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
    </div>
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
