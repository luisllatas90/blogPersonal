<!--#include file="../../NoCache.asp"-->
<%
Dim tipo_uap,Login,Clave

Session.Contents.RemoveAll

'Sirve para recuperar logeo de windows
Login=Request.ServerVariables("LOGON_USER")
tipo_uap="P"
Clave="0"

'Buscar en la base de datos el id del usuario,según el tipo
Set ObjUsuario= Server.CreateObject("PryUSAT.clsDatAplicacion")
	ArrDatos=ObjUsuario.Validar("AR",tipo_uap,Login,Clave)
Set ObjUsuario=nothing

If IsEmpty(ArrDatos)=false then
	session("tipo_usu")=tipo_uap
	session("descripciontipo_usu")=Arrdatos(0,0)
	session("codigo_Usu")=Arrdatos(1,0)
	session("Ident_Usu")=Arrdatos(2,0)
	session("Nombre_Usu")=Arrdatos(3,0)
	session("codigo_Cco")= Arrdatos(5,0)
	session("codigo_Dac")= Arrdatos(6,0)
	session("Descripcion_Cco")= Arrdatos(7,0)
	session("Descripcion_Dac")= Arrdatos(8,0)
	session("Equipo_bit")=Request.ServerVariables("REMOTE_ADDR")
	session("Usuario_bit")=Login

	'Ingresar a página del campus virtual
	 response.redirect "frminscripcion.asp"
Else%>
	<script>
		alert('Lo sentimos, Ud. no tiene acceso al Campus Virtual\n\Para cualquier consulta contáctese con el Administrador del Sistema')
		top.window.close()
	</script>
<%End if%>