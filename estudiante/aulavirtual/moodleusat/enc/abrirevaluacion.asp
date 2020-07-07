<!--#include file="../../evaluacion/clsevaluacion.asp"-->
<%
Dim accion,idevaluacion,codigoacceso

accion=request.querystring("accion")
idevaluacion=Request.querystring("idevaluacion")
'-----------------------------------------------------------
'Limpiar todas las variables de inicio de sesión asignadas
'-----------------------------------------------------------
	session("ArrDatos")=""
	session("IdEvaluacion")=""
	Session("Respuestas")=""
	session("Pagina")=""
	session("EstadoActual")=""
	session("minutos")=""
	session("enlinea")=""
	session("mostrarresultados")=""
	session("incluirimagenes")=""
	session("modificarrespuesta")=""
	session("preguntaporpregunta")=""
	session("retrocederpaginas")=""
	session("respuestacorrecta")=""
'-----------------------------------------------------------

Set evaluacion=new clsevaluacion
	with evaluacion
	
	if accion="iniciarencuesta" then
		session("codigo_acceso")=""
		codigoacceso=.Iniciar(session("codigo_usu"),idevaluacion,session("Equipo_bit"))
		
		If codigoacceso>0 then
			'------------------------------------------------
			'Cargar lista de preguntas de la evaluación y
			'Almacenar ARRAY en variable sessión
			'Almacenar valores de inicio de sesión
			'------------------------------------------------
			session("codigo_acceso")=codigoacceso
			session("idEvaluacion")=idevaluacion
			session("EstadoActual")="InicioEvaluacion"
			
			response.redirect "frmresponderencuesta1.asp?idEvaluacion=" & idevaluacion
		else
			.Cerrar="ES"
		end if
	end if


	if accion="terminarencuesta" then
		call .Terminar(session("codigo_usu"),session("codigo_acceso"))
		.Cerrar="CS"
	end if

	end with
Set evaluacion=nothing
%>