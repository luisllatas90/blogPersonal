<%
accion=request.querystring("accion")
idforo=request.querystring("idforo")
idforomensaje=request.querystring("idforomensaje")
titulomensaje=request.querystring("titulomensaje")
refidforomensaje=request.querystring("refidforomensaje")

rpta=request.querystring("rpta")
if idforomensaje="" then idforomensaje=0
if refidforomensaje="" then refidforomensaje=0
if refidforomensaje<>0 then
	rpta="RE:"
end if

if accion="modificarforomensaje" then
	Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			rsDatos=obj.Consultar("ConsultarForo","FO",5,idforomensaje,"","")
		obj.CerrarConexion
		
		titulomensaje=rsDatos("titulomensaje")
		web=rsDatos("descripcion")
		Set rsDatos=nothing
	Set Obj=nothing
end if
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registro de mensajes</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estiloaulavirtual.css">
<script language="JavaScript">
//Validar datos de ingreso del documento
function validarmensaje(formulario)
{
  if (formulario.titulomensaje.value == ""){
    alert("Ingrese el título de la respuesta.");
    formulario.titulomensaje.focus();
    return (false);
  }

  formulario.web.value = Contenido.document.body.innerHTML

  if (formulario.web.value==""){
    alert("Especifique el contenido del mensaje.");
    Contenido.focus();
    return (false);
  }

  if (formulario.web.value.length>3999){
    alert("El contenido de la respuesta no debe contener más de 4000 caracteres.");
    Contenido.focus();
    return (false);
  }
  return (true);
}
</script>
</head>
<body leftmargin="0" topmargin="0" bgcolor="#E9F3FC">
<form name="frmEditar" onSubmit="return validarmensaje(this);" METHOD="POST" ACTION="procesar.asp?accion=<%=accion%>&idforomensaje=<%=idforomensaje%>&idforo=<%=idforo%>&refidforomensaje=<%=refidforomensaje%>">
<table cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" id="tblbotones" class="barraherramientas">
  	<tr><td>
	<input type="submit" value=" Enviar" class="guardar3" name="cmdGuardar">
	<input onClick="window.close()" type="button" value="   Cancelar" name="cmdCancelar" class="cerrar3">
	</td></tr>
</table>
<center>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#C0C0C0" width="98%">
  <tr><td width="15%" valign="top" class="etiqueta" height="5%">Título&nbsp; <span><%=rpta%></span></td>
    <td valign="top" height="5%" width="85%"><input type="text" class="cajas" name="titulomensaje" value="<%=titulomensaje%>" size="20" maxlength="100"></td>
  </tr>
  <tr>
    <td width="100%" valign="top" colspan="2" class="etiqueta" height="5%">&nbsp;Descripción del mensaje</td>
  </tr>
  <tr id="tblherramientas">
    <td width="100%" valign="top" colspan="2" height="3%" bgcolor="#EEEEEE" class="bordepestana">
   <select onchange="EjecutarComando('fontname',this[this.selectedIndex].value);">
		<option value="Arial">Arial</option>
		<option value="Arial Black">Arial Black</option>
		<option value="Arial Narrow">Arial Narrow</option>
        <option value="Comic Sans MS">Comic Sans MS</option>
        <option value="Courier New">Courier New</option>
		<option value="System">System</option>
		<option value="Tahoma">Tahoma</option>
		<option value="Times New Roman">Times New Roman</option>
		<option value="Verdana">Verdana</option>
		<option value="Wingdings">Wingdings</option>
	</select> <select onchange="EjecutarComando('fontsize',this[this.selectedIndex].value);">
		<%for i=1 to 14%>
			<option value="<%=i%>"><%=i%>pt</option>
		<%next%>
	</select> &nbsp;|<img src="../../../../images/negrita.gif" alt="Negrita" onClick="Negrita()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="absmiddle"><img src="../../../../images/cursiva.gif" alt="Cursiva" onClick="Cursiva()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="absmiddle"><img src="../../../../images/subrayado.gif" alt="Subrayado" onClick="Subrayado()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="absmiddle"><img src="../../../../images/superindice.gif"  onClick="Superindice()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Superíndice">
     <img src="../../../../images/subindice.gif" onClick="Subindice()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Subíndice"> |<img src="../../../../images/izquierda.gif" onClick="AlineaIzquierda()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Alinear a la izquierda"><img src="../../../../images/centrado.gif" onClick="AlineaCentro()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Alinear al centro"><img src="../../../../images/derecha.gif" onClick="AlineaDerecha()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Alinear a la derecha">
    |<img src="../../../../images/numeros.gif" onClick="Numeros()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Numeración"><img src="../../../../images/lista.gif"  onClick="Vinetas()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Viñetas">
     <img src="../../../../images/alinearizq.gif" onClick="SangriaDerecha()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Reducir sangría">
     <img src="../../../../images/alinearder.gif" onClick="SangriaIzquierda()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Aumentar sangría"> 
    |<img src="../../../../images/color.gif" onClick="VentanaPaletaColores('../paleta.html')" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Color de fuente">|<img src="../../../../images/tabla.gif" onClick="VentanaTabla('../tabla.html')" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Insertar tabla"><img border="0" src="../../../../images/vinculo.gif" onClick="Vinculo()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Hipervínculo"></td>
  </tr>
</table>
  <iframe width="98%" id="Contenido" border="0" name="Contenido" src="../private/plantilla.html" target="_self" height="75%"></iframe>
	<script language="JavaScript" src="../private/formato.js"></script>
	<script language="JavaScript">document.all.titulomensaje.focus();CargarPagina();</script>
	
   	<div style="position:absolute;width:200;left:461;top:88;height:25;visibility:hidden">
	<textarea rows="1" name="web" cols="20"><%=web%></textarea>
	</div>
</form>
</body>
</html>