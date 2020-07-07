<!--#include file="../../../NoCache.asp"-->
<!--#include file="../asignarvalores.asp"-->
<!--#include file="clsevaluacion.asp"-->
<%
Dim accion,evaluacion,idevaluacion,idpregunta,idtipopregunta

	accion=request.querystring("accion")
	idevaluacion=request.querystring("idevaluacion")
	idpregunta=request.querystring("idpregunta")
	idtipopregunta=request.querystring("idtipopregunta")
	
	set evaluacion=new clsevaluacion
		with evaluacion
			.restringir=session("idcursovirtual")
			' Select case accion
				' case "agregarevaluacion"
					' Controlesevaluacion
					' idEvaluacion=.agregar(idcategoria, tituloevaluacion, fechainicio, fechafin, descripcion, instrucciones, session("idcursovirtual"),session("codigo_usu"), enlinea, mostrarresultados, incluirimagenes, modificarrespuesta, preguntaporpregunta, retrocederpaginas, respuestacorrecta, vecesacceso, minutos,1)
					' if Idevaluacion>0 then
						' .codigo_eval=idevaluacion
						' response.redirect "frmpregunta.asp?accion=agregarpregunta&idevaluacion=" & idevaluacion
					' else
						' Cerrar="R"
					' end if
				' case "modificarevaluacion"
					' Controlesevaluacion
					' call .modificar(idevaluacion,idcategoria, tituloevaluacion, fechainicio, fechafin, descripcion, instrucciones,enlinea, mostrarresultados, incluirimagenes, modificarrespuesta, preguntaporpregunta, retrocederpaginas, respuestacorrecta, vecesacceso, minutos)
					' .Cerrar="M"
				' case "eliminarevaluacion"
					' 'ArrTemp=.Consultar("8",idevaluacion,"","")
					' mensaje=.eliminar(idevaluacion)
					' if mensaje="" then
						' 'call .eliminararchivosdir(ArrTemp)
						' .Cerrar="EE"
					' else
						' response.write "<script>alert('" & mensaje & "');history.back(-1)</script>"
					' end if
					
				' case "agregarpregunta"
					' Controlespregunta
					' IdPregunta=.agregarpregunta(idevaluacion, IdTipoPregunta, ordenpregunta, titulopregunta, pjebueno, pjemalo, pjeblanco, obligatoria, duracion,URL,valorpredeterminado)
					' .codigo_eval=idevaluacion
					' if IdPregunta>0 then
						' if (IdTipoPregunta=1 or IdTipoPregunta=5) then
							' .Cerrar="Q"
						' else
							' 'Para agregar alternativas
							' response.redirect "frmpregunta.asp?accion=modificarpregunta&idevaluacion=" & idevaluacion & "&IdPregunta=" & IdPregunta
						' end if
					' else
						' .Cerrar="R"
					' end if
					
				' case "modificarpregunta"
					' Controlespregunta
					' call .modificarpregunta(idpregunta, idtipopregunta, ordenpregunta, titulopregunta, pjebueno, pjemalo, pjeblanco, obligatoria, duracion, URL,valorpredeterminado)
					' .codigo_eval=idevaluacion
					' .Cerrar="Q"
		
				' case "eliminarpregunta"
					' 'ArrTemp=.Consultar("8",idpregunta,"","")
					' mensaje=.eliminarpregunta(idpregunta)
					' if mensaje="" then
						' 'call .eliminararchivosdir(ArrTemp)
						' .codigo_eval=idevaluacion
						' .Cerrar="Q"
					' else
						' response.write "<script>alert('" & mensaje & "');history.back(-1)</script>"
					' end if
			' end select
		end with
	Set evaluacion=nothing
%>