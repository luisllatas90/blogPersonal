<!--#include file="../../NoCache.asp"-->
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">

<%
Dim tipo_uap,Login,Clave
Session.Contents.RemoveAll

tipo_uap="P"
Login=Request.ServerVariables("LOGON_USER")
Clave=0'request.querystring("Clave")



'Para verificar por usuario
'if Login="USAT\hzelada" then
'	Login="USAT\lotake"
'end if

login = "USAT\HREYES"

if (Login="") then
	response.redirect "../../tiempofinalizado.asp"
end if

'Buscar en la base de datos el id del usuario,según el tipo
Set ObjUsuario= Server.CreateObject("AulaVirtual.clsAccesoDatos")
ObjUsuario.AbrirConexion
	Set ArrDatos=ObjUsuario.Consultar("consultaracceso","FO",tipo_uap,Login,Clave)
ObjUsuario.CerrarConexion
Set ObjUsuario=nothing

	If Not(ArrDatos.BOF and ArrDatos.EOF) then
		session("tipo_usu")=tipo_uap
		session("descripciontipo_usu")=Arrdatos(0)
		session("codigo_Usu")=Arrdatos(1)
		session("Ident_Usu")=Arrdatos(2)
		session("Nombre_Usu")=Arrdatos(3)
		session("Area_Usu")= Arrdatos(4)
		session("Equipo_bit")=Request.ServerVariables("REMOTE_ADDR")
		session("Usuario_bit")=Login
		session("email")=Arrdatos(5)

		Set ArrDAtos=nothing

		'Ingresar a página del campus virtual
		'response.redirect "../lebir/acceder.aspx?tipo_uap=P"
		response.redirect "listaaplicaciones.asp"		
	Else
		Set ArrDAtos=nothing
		%>
		<script>
			alert('Lo sentimos, Ud. no tiene acceso al Campus Virtual\n\Para cualquier consulta contáctese con el Administrador del Sistema \n email: gchunga@usat.edu.pe')
			location.href='../listaaplicaciones.asp'
		</script>
	<%End if
%>