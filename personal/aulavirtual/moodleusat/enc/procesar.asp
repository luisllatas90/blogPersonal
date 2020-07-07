<!--#include file="../../../../NoCache.asp"-->
<!--#include file="../../../../funcionesaulavirtual.asp"-->
<%
if session("codigo_usu")="" then response.write "<script>top.location.href='../../../../tiempofinalizado.asp'</script>"

Dim accion,evaluacion,idevaluacion,idpregunta,idtipopregunta

	'=====================================================
	'Datos de la encuesta
	'=====================================================

	accion=request.querystring("accion")
	idevaluacion=request.querystring("idevaluacion")
	codigo_ccv=request.querystring("codigo_ccv")
	idtipopregunta=request.querystring("idtipopregunta")	
	
	tituloevaluacion=request.form("tituloevaluacion")
	instrucciones=request.form("instrucciones")
	fechainicio=request.form("FechaInicio")
	fechafin=request.form("FechaFin")
	enlinea=request.form("enlinea")
	enlinea=IIf(enlinea="",0,enlinea)
	respuestacorrecta=request.form("respuestacorrecta")
	respuestacorrecta=IIf(respuestacorrecta="",0,respuestacorrecta)
	minutos=request.form("minutos")
	minutos=IIf(minutos="",0,minutos)
	mostrarresultados=1
	incluirimagenes=1
	modificarrespuesta=1
	preguntaporpregunta=0
	retrocederpaginas=0
	vecesacceso=1
	
	if accion="agregarevaluacion" then
		Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			idEvaluacion=obj.Ejecutar("DI_AgregarEvaluacion",true,tituloevaluacion, fechainicio, fechafin, instrucciones, session("idcursovirtual"),session("codigo_usu"), enlinea, mostrarresultados, incluirimagenes, modificarrespuesta, preguntaporpregunta, retrocederpaginas, respuestacorrecta, vecesacceso, minutos,1,codigo_ccv,null)
			'response.write (request.form) & "<br><br>" & request.querystring
			
		obj.CerrarConexion
		Set Obj=nothing
		
		response.redirect "frmpregunta.asp?accion=agregarpregunta&codigo_ccv=" & codigo_ccv & "&idevaluacion=" & idevaluacion & "&idtipopregunta=" & idtipopregunta
	end if

	if accion="modificarevaluacion" then
		Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			call obj.Ejecutar("DI_ModificarEvaluacion",false,idevaluacion,tituloevaluacion, fechainicio, fechafin, instrucciones,enlinea, mostrarresultados, incluirimagenes, modificarrespuesta, preguntaporpregunta, retrocederpaginas, respuestacorrecta, vecesacceso, minutos)
		obj.CerrarConexion
		Set Obj=nothing
	
		if request.querystring("modo")="P" then
			response.redirect "frmpregunta.asp?accion=agregarpregunta&codigo_ccv=" & codigo_ccv & "&idevaluacion=" & idevaluacion & "&idtipopregunta=" & idtipopregunta
		else
			response.redirect "../cargando.asp?rutapagina=tematicacurso.asp"
		end if
		'response.write (request.form) & "<br><br>" & request.querystring
	end if

	'=====================================================
	'Datos de la pregunta
	'=====================================================
	idpregunta=request.querystring("idpregunta")
	titulopregunta=request.form("titulopregunta")
	ordenpregunta=request.form("ordenpregunta")
	obligatoria=request.form("obligatoria")
	URL=null
	valorpredeterminado=request.form("valorpredeterminado")
	if obligatoria="" then obligatoria=0
	
	ordenalt=request.querystring("ordenalt")
	tituloalt=request.querystring("tituloalt")
	mensajealt=request.querystring("mensajealt")
	correctoalt=request.querystring("correctoalt")
	
	if ordenalt="" then ordenalt=1

	if accion="agregarpregunta" then
		Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			IdPregunta=obj.Ejecutar("DI_AgregarPregunta",true,idevaluacion, IdTipoPregunta, ordenpregunta, titulopregunta,obligatoria,URL,valorpredeterminado,ordenalt,tituloalt,mensajealt,correctoalt,null)
		obj.CerrarConexion
		Set Obj=nothing
		if IdPregunta>0 then
			if (IdTipoPregunta=1 or IdTipoPregunta=5) then
				response.redirect "frmencuesta.asp?accion=modificarevaluacion&codigo_ccv=" & codigo_ccv & "&idevaluacion=" & idevaluacion
			else
				'Para agregar alternativas
				response.redirect "frmpregunta.asp?modo=A&accion=modificarpregunta&idevaluacion=" & idevaluacion & "&IdPregunta=" & IdPregunta & "&idtipopregunta=" & idtipopregunta & "&codigo_ccv=" & codigo_ccv
			end if
		end if
	end if

	if accion="modificarpregunta" then
		Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			call obj.Ejecutar("DI_modificarpregunta",false,idpregunta, idtipopregunta, ordenpregunta, titulopregunta, obligatoria,URL,valorpredeterminado,ordenalt,tituloalt,mensajealt,correctoalt)
		obj.CerrarConexion
		Set Obj=nothing
		
		if (IdTipoPregunta=1 or IdTipoPregunta=5) then		
			response.redirect "frmencuesta.asp?accion=modificarevaluacion&idevaluacion=" & idevaluacion & "&codigo_ccv=" & codigo_ccv
		else
			response.redirect "frmpregunta.asp?modo=A&accion=modificarpregunta&idevaluacion=" & idevaluacion & "&IdPregunta=" & IdPregunta & "&idtipopregunta=" & idtipopregunta & "&codigo_ccv=" & codigo_ccv
		end if
	end if
		
	if accion="eliminarpregunta" then
		Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			mensaje=obj.Ejecutar("eliminarpregunta",true,idpregunta,null)
		obj.CerrarConexion
		Set Obj=nothing

		if mensaje="" then
			response.redirect "frmencuesta.asp?accion=modificarevaluacion&idevaluacion=" & idevaluacion & "&codigo_ccv=" & codigo_ccv
		else
			response.write "<script>alert('" & mensaje & "');history.back(-1)</script>"
		end if
	end if
	
	if accion="eliminaralternativa" then
		idalternativa=request.querystring("idalternativa")
	
		Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			call obj.Ejecutar("eliminaralternativa",false,idalternativa)
		obj.CerrarConexion
		Set Obj=nothing
		response.redirect "frmpregunta.asp?modo=A&accion=modificarpregunta&idevaluacion=" & idevaluacion & "&IdPregunta=" & IdPregunta & "&idtipopregunta=" & idtipopregunta & "&codigo_ccv=" & codigo_ccv
	end if


%>