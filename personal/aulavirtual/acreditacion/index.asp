<!--#include file="../NoCache.asp"-->
<%
Dim mostrar

Session.contents.Removeall

LoginUsuario="USAT\wruiz" 'Request.ServerVariables("LOGON_USER")
mostrar=request.querystring("mostrar")

Set ObjUsuario= Server.CreateObject("AulaVirtual.clsUsuario")
	EncuentraReg=ObjUsuario.Busca(LoginUsuario,"P",ArrDatos)
Set ObjUsuario=nothing

If  EncuentraReg then
	session("codigo_usu")=Arrdatos(0,0)
	session("nombre_usu")=Arrdatos(1,0)
	session("Area")=Arrdatos(2,0)
	session("Autorizacion")=Arrdatos(3,0)
	
	if session("Idinicio")="" then
		Set Obj=Server.CreateObject("AulaVirtual.clsUsuario")
			
				Activo= Obj.Acceso("I",LoginUsuario,Request.ServerVariables("REMOTE_ADDR"),0,IdVisita,Conectados)
			
		Set Obj=nothing
		If Activo then
			session("IdVisita")=IdVisita
			session("UsuariosActuales")=Conectados
			response.redirect "listacarreras.asp"
		else
			response.write "<h3>Cierre todas las ventanas del Internet Explorer e ingrese denuevo</h3>"
		end if
	else
		response.redirect "listacarreras.asp"
	end if			
Else%>
	<script>
		alert('Lo sentimos, Ud. no tiene acceso a esta página\n\Para cualquier consulta contáctese con gchunga@usat.edu.pe')
		top.window.close()
	</script>
<%End if%>