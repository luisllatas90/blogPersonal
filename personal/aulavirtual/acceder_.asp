<!--#include file="../../NoCache.asp"-->
<%
Dim tipo_uap,Login,Clave
Session.Contents.RemoveAll

tipo_uap="P"
Login=Request.ServerVariables("LOGON_USER")
Clave=request.querystring("Clave")


'Para verificar por usuario
'if Login="USAT\gchunga" then
'	Login="USAT\ccachay"
'end if


if (Login="") then
	response.redirect "../../tiempofinalizado.asp"
end if

'Buscar en la base de datos el id del usuario,según el tipo
Set ObjUsuario= Server.CreateObject("AulaVirtual.clsDatAplicacion")
	ArrDatos=ObjUsuario.Validar(tipo_uap,Login,Clave)
Set ObjUsuario=nothing

	If IsEmpty(ArrDatos)=false then
		session("tipo_usu")=tipo_uap
		session("descripciontipo_usu")=Arrdatos(0,0)
		session("codigo_Usu")=Arrdatos(1,0)
		session("Ident_Usu")=Arrdatos(2,0)
		session("Nombre_Usu")=Arrdatos(3,0)
		session("Area_Usu")= Arrdatos(4,0)
		session("Equipo_bit")=Request.ServerVariables("REMOTE_ADDR")
		session("Usuario_bit")=Login
		session("email")=Arrdatos(5,0)

		'Ingresar a página del campus virtual
		response.redirect "listaaplicaciones.asp"		
	Else%>
		<script>
			alert('Lo sentimos, Ud. no tiene acceso al Campus Virtual\n\Para cualquier consulta contáctese con el Administrador del Sistema \n email: gchunga@usat.edu.pe')
			location.href='../listaaplicaciones.asp'
		</script>
	<%End if
%>