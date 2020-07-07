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

'Buscar en la base de datos el id del usuario,según el tipo
Set ObjUsuario= Server.CreateObject("AulaVirtual.clsAccesoDatos")
    ObjUsuario.AbrirConexion
    Set ArrDatos=ObjUsuario.Consultar("consultaracceso","FO",tipo_uap,Login,Clave)
    ObjUsuario.CerrarConexion
Set ObjUsuario=nothing

	If Not(ArrDatos.BOF and ArrDatos.EOF) then
		'Actualiza los cursos que el alumno esté matriculado pero no en Aula virtual
		Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		    obj.AbrirConexion
		    Obj.Ejecutar "ActivarCursosNoHabilitados",false,trim(Login)
		    obj.CerrarConexion
		SEt obj=nothing

		session("tipo_usu")=tipo_uap
		session("descripciontipo_usu")=Arrdatos(0)
		session("codigo_Usu")=Arrdatos(1)
		session("Ident_Usu")=Arrdatos(2)
		session("Nombre_Usu")=Arrdatos(3)
		session("Area_Usu")= Arrdatos(4)
		session("Equipo_bit")=Request.ServerVariables("REMOTE_ADDR")
		session("Usuario_bit")=Login
		session("email")=Arrdatos(5)
		session("Clave")=Clave
		session("actualizodatos")=Arrdatos(6)

		'Ingresar a página del campus virtual
		response.redirect "listaaplicaciones.asp"		
	Else%>
		<script>
			alert('Lo sentimos, Ud. no tiene acceso al Campus Virtual\n\Para cualquier consulta contáctese con el Administrador del Sistema \n email: gchunga@usat.edu.pe')
			top.location.href='../principal.asp'
		</script>
	<%End if


%>