<!--#include file="../../../../NoCache.asp"-->
<%
Dim accion,idforo,idforomensaje

	accion=request.querystring("accion")
	idforo=request.querystring("idforo")
	idforomensaje=request.querystring("idforomensaje")
	refidforomensaje=request.querystring("refidforomensaje")
	idusuario=session("codigo_usu")
	titulomensaje=Request.form("titulomensaje")
	descripcionmensaje=Request.form("web")
	if idforomensaje="" then idforomensaje=0
	

	if accion="agregarforomensaje" then
		Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			obj.Ejecutar "AgregarForoMensaje",false,idforo,titulomensaje,descripcionmensaje,session("codigo_usu"),refidforomensaje
		obj.CerrarConexion
		
		response.write "<script>window.opener.location.reload();window.close()</script>"
	end if

	if accion="modificarforomensaje" then
		Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			obj.Ejecutar "ModificarForoMensaje",false,idforomensaje,titulomensaje,descripcionmensaje
		obj.CerrarConexion
		
		response.write "<script>window.opener.location.reload();window.close()</script>"
	end if
	
	if accion="eliminarforomensaje" then
		Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			obj.Ejecutar "EliminarForoMensaje",false,idforomensaje
		obj.CerrarConexion
		
		response.redirect "../detallerecurso.asp?codigo_tre=F&idtabla=" & idforo
	end if

	if accion="calificarmensaje" then
		Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			obj.Ejecutar "Agregarforocalificacionmensajes",false,idforomensaje,session("codigo_usu"),numcalificacion
		obj.CerrarConexion
		
		response.redirect "../detallerecurso.asp?codigo_tre=F&idtabla=" & idforo
	end if
%>