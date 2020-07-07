<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmetapainvestigaciontesis.aspx.vb" Inherits="frmetapainvestigaciontesis" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Registro de Etapas de la tesis </title>
    <script type="text/javascript" language="JavaScript" src="private/PopCalendar.js"></script>
	<script type="text/javascript" language="javascript">
        function OcultarTabla()
        {    
            document.all.form1.style.display="none"
            document.all.tblmensaje.style.display=""
        }
     </script>
     <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin-top: 5px;background-color: #F2F2F2">
<form id="form1" runat="server">
<%Response.Write(ClsFunciones.CargaCalendario)%>    
<asp:Button ID="cmdGuardar" runat="server" CssClass="guardar_prp" Text="         Enviar" OnClientClick="OcultarTabla()"/>
<asp:Button ID="cmdCancelar" runat="server" CssClass="noconforme1" OnClientClick="javascript: window.close(); return false;" Text="        Cancelar" />
<br/><br/>
<table style="width: 100%;" class="contornotabla" cellpadding="3" id="tblDatos">
            <tr>
                            <td width="30%">Fecha de Aprobación</td>
                            <td width="70%">
                <asp:TextBox ID="txtFechaAprobacion" runat="server" Width="100px" ForeColor="Navy" 
                    style="text-align: right" BackColor="#CCCCCC" MaxLength="12"></asp:TextBox>
                <asp:Button ID="cmdInicio" runat="server" 
                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaAprobacion,'dd/mm/yyyy');return(false)" 
                    Text="..." CausesValidation="False" UseSubmitBehavior="False" Font-Size="10px" />
                <asp:RequiredFieldValidator ID="ValidarFecha" runat="server" 
                    ControlToValidate="txtFechaAprobacion" 
                    ErrorMessage="Debe especificar la fecha de aprobación">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
            <tr>
                            <td width="30%">Fase</td>
                            <td width="70%">
                                <asp:Label ID="lblFase" runat="server" Font-Bold="True" Font-Size="11px" 
                                    ForeColor="Blue"></asp:Label>
&nbsp;<asp:HiddenField ID="hdFase" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="100%" colspan="2">Comentarios</td>
			</tr>
			<tr>
                            <td width="100%" colspan="2">
                                <asp:TextBox ID="TxtComentario" runat="server" Height="150px" TextMode="MultiLine"
                                    Width="98%" Font-Size="9pt" MaxLength="1000"></asp:TextBox></td>
                        </tr>
			<tr>
                            <td width="100%" colspan="2">
                                <asp:CheckBox ID="chkBloquear" runat="server" 
                                    Text="Bloquear registro de asesorías a las tesis seleccionadas, en esta nueva etapa." />
                            </td>
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
	    <img src="../../../images/cargando.gif" width="209" height="20"/>
	    </td>
	    </tr>
    </table>
</body>
</html>
