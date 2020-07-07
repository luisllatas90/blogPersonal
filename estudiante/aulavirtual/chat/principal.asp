<!--#include file ="../../../funcionesaulavirtual.asp"-->
<%
if session("codigo_usu")="" then response.redirect("../../../tiempofinalizado.asp")
%>
<html>

<head>
<meta http-equiv="Content-Language" content="en-gb">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Sala de Chat: Usuario actual:&nbsp;<%=session("nombre_usu")%></title>
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script>
var Old_recieved = ""
xmlhttp=false;
/*@cc_on @*/
/*@if (@_jscript_version >= 5)
// JScript gives us Conditional compilation, we can cope with old IE versions.
// and security blocked creation of the objects.
 try
 	{
  		xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
	}
	
	catch (e){
		  try{
		   		xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
		  }
		catch (E){
		   xmlhttp = false;
		}
 	}
@end @*/
if (!xmlhttp && typeof XMLHttpRequest!='undefined') {
  xmlhttp = new XMLHttpRequest();
}

function RecuperarMensajes()
{

Stamp = new Date();
 xmlhttp.open("GET", "actualizar.asp?time=" + Stamp + "&codigo_usu=<%=session("codigo_usu")%>",true);
 xmlhttp.onreadystatechange=function() {
  if (xmlhttp.readyState==4) {
  
	var rText = xmlhttp.responseText
	var rText = rText.split("!#!");
   	OrdenarUsuarios(rText[0])
   	OrdenarMensajes(rText[1])
   
   }
 }
 xmlhttp.send(null)
 xmlhttp.close
 setTimeout("RecuperarMensajes()", 1000);
}

function OrdenarMensajes(mis_mensajes)
{
	
	if ((mis_mensajes=="") || (mis_mensajes==null)) {
  		alert("Error en el inicio de sesión del chat!");
	} else if (mis_mensajes.indexOf("^#^")== -1) {
  		frames[0].document.body.innerHTML = mis_mensajes
  	} else {
  		var temp_message = ""
  		var temp_old = frames[0].document.body.innerHTML
		var array_mensajes=mis_mensajes.split("^#^");
		var part_num=0;
		while (part_num < array_mensajes.length) {
			if (Old_recieved.indexOf(array_mensajes[part_num])== -1) {
				temp_message = temp_message + array_mensajes[part_num] + "<br>"
			}
  			part_num+=1;
  		}
  		if (temp_message!="") {
  			frames[0].document.body.innerHTML = frames[0].document.body.innerHTML + temp_message	
  		}
  	}
  	Old_recieved = mis_mensajes
}

function OrdenarUsuarios(mis_usuarios)
{
	frames[1].document.body.innerHTML = ""
	if ((mis_usuarios=="") || (mis_usuarios==null)) {
  		alert("Error! Los datos no han retornado. Por favor registre denuevo!");
	} else if (mis_usuarios.indexOf("^#^")== -1) {
  		frames[1].document.body.innerHTML = mis_usuarios
  	} else {
		var array_usuarios=mis_usuarios.split("^#^");
		var part_num=0;
		while (part_num < array_usuarios.length) {
			frames[1].document.body.innerHTML = frames[1].document.body.innerHTML + array_usuarios[part_num] + "<br>";
  			part_num+=1;
  		}
	}
}

function EnviarMensajes()
{
Stamp = new Date();
 xmlhttp.open("GET", "actualizar.asp?M_send="+document.getElementById("message").value+"&time=" + Stamp,true);
 document.getElementById("message").value = ""
 document.getElementById("message").focus()
 xmlhttp.onreadystatechange=function() {
  if (xmlhttp.readyState==4) {
  
  }
 }
 xmlhttp.send(null)
 xmlhttp.close
}
</script>

<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">

</head>
<body onload="RecuperarMensajes()" onUnload="location.href='cerrarchat.asp'" topmargin="0" leftmargin="0" bgcolor="#91A9DB" oncontextmenu="return event.ctrlKey">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
  <tr>
    <td width="70%" valign="top" height="85%">
    <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="100%">
      <tr>
        <td width="100%" height="5%" bgcolor="#FEFFE1" class="etiqueta">
        <span lang="es">&nbsp;</span>Mensajes del Chat</td>
      </tr>
      <tr>
        <td width="100%" height="95%" class="contornotabla"><iframe width=100% height="100%" id=mbox name="I1" src="lstmensajes.htm" FRAMEBORDER=0></iframe></td>
      </tr>
    </table>
    </td>
    <td width="30%" valign="top" height="85%" align="center">
    <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="95%" height="100%">
      <tr>
        <td width="100%" height="5%" bgcolor="#FDFCE8" class="etiqueta">Usuarios en línea</td></tr>
      <tr>
        <td width="100%" height="95%" class="contornotabla">
        <iframe width=100% height="100%" id=blist style="border-style: solid; border-width: 0" name="I2" src="lstusuarios.htm" FRAMEBORDER=0></iframe></td>
      </tr>
    </table>
    </td>
  </tr>
  <tr>
    <td width="100%" height="15%" colspan="2" bgcolor="#CCCCCC" valign="top">
    <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
      <tr><td width="100%" colspan="2" class="etiqueta">Digite su mensaje aquí...</td></tr>
      <tr>
        <td width="65%"><input type="text" name="message" id=message size="76" class="cajas"></td>
        <td width="35%">
        <input type="submit" value="Enviar" name="cmdenviar" onclick="EnviarMensajes()" class="boton"><span lang="es">
        </span>
        <input type="button" value="   Salir" name="cmdsalir" onclick="top.location.href='cerrarchat.asp'" class="boton">
        <%=BotonAyuda("B","../../ayuda/chat.asp","../../../")%>
        </td>
      </tr>
    </table>
    </td>
  </tr>
</table>

</body>

</html>