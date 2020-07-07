<%
	idcursovirtual=request.querystring("idcursovirtual")
	
	Set Obj=Server.CreateObject("AulaVirtual.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsCursos=Obj.Consultar("ConsultarCursoVirtual","FO",0,idcursovirtual,session("codigo_usu"),0)
	Obj.CerrarConexion
	Set Obj=nothing

	'Variables del cursovirtual abierto
	session("idcursovirtual")=Request.querystring("idcursovirtual")
	session("nombrecursovirtual")=rsCursos("titulocursovirtual")
	session("idestadocursovirtual")=rsCursos("idestadorecurso")
	session("iniciocursovirtual")=rsCursos("fechainicio")
	session("fincursovirtual")=rsCursos("fechafin")
	session("creadorcursovirtual")=rsCursos("idusuario")
	session("numusuarios")=rsCursos("numusuarios")
	session("tipofuncion")=rsCursos("codigo_tfu")
	session("descripciontipofuncion")=rsCursos("descripcion_tfu")
	session("creartemas")=rsCursos("creartemas")
	session("temapublico")=rsCursos("temapublico")
	session("integrartematarea")=rsCursos("integrartematarea")
	session("integrarrptatarea")=rsCursos("integrarrptatarea")
	fichaactualizacion=rsCursos("ActualizarDatosParticipante")

	'Variables de la aplicación abierta
	session("tipo_apl")=1'rsCursos("tipo_apl")
	session("codigo_tfu")=rsCursos("codigo_tfu")
	session("codigo_apl")=rsCursos("codigo_apl")
	session("descripcion_apl")=""'rsCursos("descripcion_apl")
	session("enlace_apl")=""'replace(rsCursos("enlace_apl"),"\","/")


	nombreusuario=lcase(trim(session("nombre_usu")))
	nombreusuario=replace(nombreusuario,"á","a")
	nombreusuario=replace(nombreusuario,"é","e")
	nombreusuario=replace(nombreusuario,"í","i")
	nombreusuario=replace(nombreusuario,"ó","o")
	nombreusuario=replace(nombreusuario,"ú","u")
	nombreusuario=ucase(nombreusuario)


	if fichaactualizacion=true and int(session("actualizodatos"))=0 then
		response.redirect "../../librerianet/inscripcioncursos/fichainscripcion.aspx?idusuario=" & session("codigo_Usu") & "&nombreusuario=" & nombreusuario
	else
	
		Set Obj= Server.CreateObject("AulaVirtual.clsDatAplicacion")	
			session("idvisita_sistema")=Obj.AgregarVisitasRecurso("I",session("codigo_usu"),"cursovirtual", session("idcursovirtual"),session("Equipo_bit"),0,session("idcursovirtual"))
		Set Obj=nothing

		'Actualizar permisos
		Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
			obj.AbrirConexion
			call Obj.Ejecutar("ActualizarAulaVirtual",false,session("idcursovirtual"),session("codigo_usu"))
		obj.CerrarConexion
		Set Obj=nothing
		Set rsCursos=nothing

		response.redirect "principal.asp?pagina=cursovirtual/convocatoria.asp"
	end if
%>