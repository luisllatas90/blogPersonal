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
	
	'Sesiones nuevas para Nuevo Diseño MOODLEUSAT
	session("mododesarrollo")=rsCursos("mododesarrollo_cv")
	session("codigo_cup")=rsCursos("codigo_cup")
	session("nombre_css")=rsCursos("nombre_css")

	'Variables de la aplicación abierta
	session("tipo_apl")=1'rsCursos("tipo_apl")
	session("codigo_tfu")=rsCursos("codigo_tfu")	
	session("codigo_apl")=rsCursos("codigo_apl")
	session("agregar_cv")=rsCursos("agregar_cv")
	session("modificar_cv")=rsCursos("modificar_cv")
	session("eliminar_cv")=rsCursos("eliminar_cv")
	
	Set rsCursos=nothing
	
	'if fichaactualizacion=true and int(session("actualizodatos"))=0 and instr(session("codigo_usu"),"USAT\")=0 then
	'	response.redirect "../../librerianet/inscripcioncursos/fichainscripcion.aspx?idusuario=" & session("codigo_Usu") & "&nombreusuario=" & nombreusuario
	'else
		Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")	
			obj.AbrirConexion
			session("idvisita_sistema")=Obj.Ejecutar("AgregaVisitasRecurso",true,"I",session("codigo_usu"),"cursovirtual", session("idcursovirtual"),session("Equipo_bit"),0,session("idcursovirtual"),null)
			obj.CerrarConexion
		Set Obj=nothing

		'Actualizar permisos
		Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
			obj.AbrirConexion
				call Obj.Ejecutar("ActualizarAulaVirtual",false,session("idcursovirtual"),session("codigo_usu"))
			obj.CerrarConexion
		Set Obj=nothing
	
		if 	session("mododesarrollo")="N" then
			response.redirect "principal.asp?pagina=cursovirtual/convocatoria.asp"
		else
			response.redirect "moodleusat/principal.asp"
		end if
	'end if
%>