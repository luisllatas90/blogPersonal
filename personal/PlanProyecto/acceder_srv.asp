<!--#include file="../NoCache.asp"-->
<%

'tipo_uap=request.querystring("cbxtipo")

' Dado que el usuario siempre se loguea como personal a una autenticación, entonces le asignamos
' el tipo_uap = P, para que acceda directamente

tipo_uap = "P"
Session.Contents.RemoveAll
if tipo_uap="" then response.redirect "../tiempofinalizado.asp"

'Sirve para recuperar logeo de windows
Login=Request.ServerVariables("LOGON_USER")

' SOLO PARA EL SERVER-TEST
'Login="USAT\" & Login

'Para verificar por usuario
     
'if Login="USAT\hreyes"  then
'	Login="USAT\marnao"  
'end if 


if Login="USAT\cmasias" or Login="USAT\fguerrero" or Login="USAT\rcustodio" then
else
Clave="0"

'Buscar en la base de datos el id del usuario,según el tipo
Set ObjUsuario= Server.CreateObject("PryUSAT.clsAccesoDatos")
	ObjUsuario.AbrirConexion
		Set rsPersonal=ObjUsuario.Consultar("consultaracceso","FO",tipo_uap,Login,Clave)
		Set rsCiclo=ObjUsuario.Consultar("consultarcicloAcademico","FO","CV",1)
	ObjUsuario.CerrarConexion
Set ObjUsuario=nothing

If Not(rsPersonal.BOF and rsPersonal.EOF) then
	session("tipo_usu")=tipo_uap
	session("descripciontipo_usu")=rsPersonal("descripcion_tpe")
	session("codigo_Usu")=rsPersonal("codigo_per")
	session("Ident_Usu")=rsPersonal("nroDocIdentidad_Per")
	session("Nombre_Usu")=rsPersonal("personal")
	session("codigo_Cco")= rsPersonal("codigo_Cco")
	session("codigo_Dac")= rsPersonal("codigo_Dac")
	session("Descripcion_Cco")= rsPersonal("Descripcion_Cco")
	session("Descripcion_Dac")= rsPersonal("nombre_Dac")
	session("Equipo_bit")=Request.ServerVariables("REMOTE_ADDR")
	session("Usuario_bit")=Login

	'Almacenar datos del ciclo académico
	session("Codigo_Cac")=rsCiclo("codigo_cac")
	session("descripcion_Cac")=rsCiclo("descripcion_cac")
	session("tipo_Cac")=rsCiclo("tipo_cac")
	session("notaminima_cac")=rsCiclo("notaminima_cac")
	rsCiclo.close
	Set rsCiclo=nothing

	'=====================================================================	
	'Copia de sesiones temporal hasta el cambio de AulaVirtual
	'=====================================================================
	session("tipo_usu2")=session("tipo_usu")
	session("descripciontipo_usu2")=session("descripciontipo_usu")
	session("codigo_Usu2")=session("codigo_Usu")
	session("Ident_Usu2")=session("Ident_Usu")
	session("Nombre_Usu2")=session("Nombre_Usu")
	session("Usuario_bit2")=session("Usuario_bit")
	'Almacenar datos del ciclo académico
	session("Codigo_Cac2")=session("Codigo_Cac")
	session("descripcion_Cac2")=session("descripcion_Cac")
	session("tipo_Cac2")=session("tipo_Cac")
	session("notaminima_cac2")=session("notaminima_cac")	
	'=====================================================================	
	
	'---------direcciona a la  solicitud -----------------------------------------------------------------------------------
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			MostrarActividad=Obj.Ejecutar("AVI_MostrarAviso",true,session("codigo_Usu"),"ANIVUSAT",session("tipo_usu"),"")
		Obj.CerrarConexion
	Set Obj=nothing		
	if MostrarActividad="NO" then
			rutaPag="listaaplicaciones.asp"
	else
		rutaPag="../kermesusat/index.html"
	end if
	'-----------------------------------------------------------------------------------------------------------------------
	'Ingresar a página del campus virtual
	response.redirect rutaPag
Else%>
	<script>
		alert('Lo sentimos, Ud. no tiene acceso al Campus Virtual\n\Para cualquier consulta contáctese con el Administrador del Sistema')
		top.window.close()
		//top.location.href="../index.asp"
	</script>
<%End if

end if

%>