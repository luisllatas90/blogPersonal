<%
dim codigo_sesion
	'Registrar uso del chat por el usuario enviado
	modo=request.querystring("modo")
	idchat=request.querystring("idchat")
		
if modo="A" then
	Set ObjChat=Server.CreateObject("AulaVirtual.clsAccesoDatos")
	ObjChat.AbrirConexion
		codigo_sesion=ObjChat.Ejecutar("DI_AgregarsesionChat",true,idchat,session("codigo_usu"),session("Equipo_bit"),null)
	ObjChat.CerrarConexion
	Set ObjChat=nothing
	
	if codigo_sesion>0 then
		session("idsesion")=codigo_sesion
		session("idchat")=idchat
	  	response.redirect("principal.asp")
	end if
else
	Set ObjChat=Server.CreateObject("AulaVirtual.clsAccesoDatos")
	ObjChat.AbrirConexion
		call ObjChat.Ejecutar("DI_CerrarSesionChat",false,session("idsesion"))
	ObjChat.CerrarConexion
	Set ObjChat=nothing
	
	response.write "<script>window.opener.location.reload();top.window.close()</script>"
end if
%>