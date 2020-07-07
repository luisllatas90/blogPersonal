<%
dim codigo_sesion
	'Registrar uso del chat por el usuario enviado
	Set ObjChat=Server.CreateObject("AulaVirtual.clsChat")
		codigo_sesion=ObjChat.Agregarsesion(session("codigo_usu"),session("idcursovirtual"),session("Equipo_bit"),session("idvisita_sistema"))
	Set ObjChat=nothing
	
	if codigo_sesion>0 then
		session("idsesion")=codigo_sesion
	  	response.redirect("principal.asp")
	end if
%>