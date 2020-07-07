<!--#include file="funciones.asp"-->
<%
Decision=Request.querystring("Decision")
IdVisita=IIF(session("IdVisita")="",0,session("IdVisita"))
If Decision="Si" then
	'Set Obj=Server.CreateObject("PryUSAT.clsUsuario")
		'Obj.IniciaTransaccion
			'Activo= Obj.Acceso("S",0,Request.ServerVariables("REMOTE_ADDR"),IdVisita,0,0)
		'Obj.TerminaTransaccion
	'Set Obj=nothing
	'session.abandon
%>
<html>
<HEAD>
<meta http-equiv="Content-Language" content="es">
<link rel="stylesheet" type="text/css" href="private/estilo.css">
<title>Advertencia</title>
<script language="Javascript">
	function cerrarventana()
	{
	//resizeTo(400, 200)
	top.window.opener=self
	top.window.close()
	}
</script>
</HEAD>
<body Onload="cerrarventana()">
 <h4 align="center">
 <img border="0" src="images/cerrar.gif" width="26" align="absmiddle"> Cerrando Sistema</td>
 </h4>
</body>
</html>
<%end if%>