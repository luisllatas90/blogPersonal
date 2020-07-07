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
	
		if 	session("mododesarrollo")="N" then
			
			codigo_alu=session("codigo_alu")
			if codigo_alu="" then codigo_alu=0

			Set objCnx=Server.CreateObject("PryUSAT.clsAccesoDatos")
			objCnx.AbrirConexion
			Set rsdatoscurso= ObjCnx.Consultar("EAD_ConsultarCursoEvaluacion","FO",session("codigo_cup"), codigo_alu)
			objCnx.CerrarConexion
			sw = 0
			IF Not(rsdatoscurso.BOF and rsdatoscurso.EOF)=true THEN
				Do while not rsdatoscurso.EOF
					IF (rsdatoscurso("codigo_eed") = 0) then			
						sw =1
						exit do
					END if
					
					rsdatoscurso.movenext
				LOOP 
				
				
			END IF
	
			
			
	
			IF (sw = 1)  THEN
				response.redirect "../../librerianet/encuesta/EvaluacionAlumnoDocente/EvaluacionDocente_Estudiante.aspx?cup=" & session("codigo_cup") & "&id=" & session("codigo_alu") & "&pagina=cursovirtual/convocatoria.asp"
			else
				response.redirect "principal.asp?pagina=cursovirtual/convocatoria.asp"
			END IF 	
			

			
		else
			response.redirect "moodleusat/principal.asp"		
		end if
	end if
%>