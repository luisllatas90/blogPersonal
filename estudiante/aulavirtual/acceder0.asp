<!--#include file="../../NoCache.asp"-->
<%
Dim tipo_uap,Login,Clave
'Session.Contents.RemoveAll

tipo_uap=request.querystring("tipo_uap")
Login=session("Usuario_bit")
Clave=session("Clave")

if (Login="") then
	response.redirect "../../tiempofinalizado.asp"
end if

'Buscar en la base de datos el id del usuario,seg�n el tipo
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
		session("Clave")=Clave
		session("actualizodatos")=Arrdatos(6,0)

		'Ingresar a p�gina del campus virtual
		response.redirect "listaaplicaciones.asp"		
	Else%>
		<script>
			alert('Lo sentimos, Ud. no tiene acceso al Campus Virtual\n\Para cualquier consulta cont�ctese con el Administrador del Sistema \n email: gchunga@usat.edu.pe')
			top.location.href='../principal.asp'
		</script>
	<%End if
%>