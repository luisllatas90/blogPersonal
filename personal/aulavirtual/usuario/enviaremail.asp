<!--#include file="clsusuario.asp"-->
<%
modo=request.querystring("modo")
idusuario=session("codigo_usu")
idtabla=request.querystring("idtabla")
nombretabla=request.querystring("nombretabla")
col=2

set usuario=new clsusuario
	usuario.Restringir=session("idcursovirtual")
	select case modo
		case "CV"
			ArrUsuario=usuario.consultar("6",nombretabla,idtabla,session("idcursovirtual"))
			col=7
		case "1"
			ArrUsuario=usuario.consultar("5",nombretabla,idtabla,session("idcursovirtual"))
		case "2"
			ArrUsuario=usuario.consultar("4",nombretabla,idtabla,session("idcursovirtual"))
			col=5
		case "3"
			ArrUsuario=usuario.consultar("8",session("codigo_usu"),"","")
	end select
Set usuario=nothing
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Envío de mensajes por email</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarusuario.js"></script>
</head>
<body topmargin="0" bgcolor="#E4E4E4" leftmargin="0">
<form name="frmListaCorreos" onsubmit="return validarenviomensajes(this)" method="post" ACTION="procesar.asp?accion=enviarcorreo&idtabla=<%=idtabla%>">
<table cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" id="tblbotones" class="barraherramientas">
  	<tr><td>
	<input type="submit" value=" Enviar" class="guardar3" name="cmdGuardar">
	<input onClick="top.window.close()" type="button" value="   Cancelar" name="cmdCancelar" class="cerrar3">
	<span id="mensaje" style="color:#FF0000"></span>
	</td></tr>
</table>
<center>
<table cellpadding="2" cellspacing="0" border="0" width="98%" style="border-color:#C0C0C0; border-collapse: collapse" bordercolor="#111111" height="90%">
		<tr align="center">
          <td  width="40%"  height="10%">
          <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
            <tr>
              <td width="15%" class="etiqueta">Buscar </td>
              <td width="85%">
              <input  maxLength="100" size="50" name="nombreusuario" class="cajas" ONKEYUP="AutocompletarCombo(this,document.all.ListaDe,'text',true)"></td>
            </tr>
          </table>
          </td>
          <td  width="10%"  height="10%">&nbsp;</td>
          <td width="40%" height="10%" valign="bottom"><b>Usuarios seleccionados</b></td>
          </tr>
		<tr align="center">
          <td  width="40%"  height="50%" valign="top"><select multiple name="ListaDe" size="10" class="cajas" style="height: 100%">
			<%If IsEmpty(ArrUsuario)=False then
				FOR I=Lbound(ArrUsuario,2) to Ubound(ArrUsuario,2)%>
				<option value="<%=ArrUsuario(col,I)%>"><%=ArrUsuario(1,I)%></option>
				<%NEXT
			end if%>
		  </select></td>
          <td  width="10%"  height="50%" valign="top">
			  <input type="button" value="Agregar-&gt;" style="width: 80" onclick="AgregarItem(this.form.ListaDe)" class="cajas">
			  <br>
		      <input type="button" value="&lt;-Quitar" style="width: 80" onclick="QuitarItem(this.form.ListaPara)" class="cajas"></td>
          <td width="40%" height="50%" valign="top">&nbsp;<select multiple name="ListaPara" size="10" style="height:100%" class="cajas">
		  </select></td>
          </tr>
		<tr>
          <td  width="90%"  height="45%" valign="top" colspan="3">
          <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber3" height="100%">
            <tr>
              <td width="100%" height="15%"><font color="#000080"><b>Asunto:</b></font><input type="text" name="txtAsunto" size="20" class="Cajas"></td>
            </tr>
            <tr>
              <td width="100%" height="15%"><font color="#000080"><b>Escriba el cuerpo del mensaje</b></font></td>
            </tr>
            <tr>
              <td width="100%" height="70%">
              <textarea rows="2" name="txtMensaje" cols="20" style="height:100%" class="Cajas"></textarea></td>
            </tr>
          </table>
          </td>
          </tr>
		</table>
</center>
</form>
</body>
</html>