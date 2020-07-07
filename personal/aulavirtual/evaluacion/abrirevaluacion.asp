<!--#include file="clsevaluacion.asp"-->
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
			session("Arrdatos")=.Consultar("3",idevaluacion,"","")
			session("codigo_acceso")=codigoacceso
			session("idEvaluacion")=idevaluacion
			session("EstadoActual")="InicioEvaluacion"
			
			'------------------------------------------------
			'Cargar Datos de la evaluación y
			'Almacenar variables sesión sus datos
			'------------------------------------------------
			arrEvaluacion=.Consultar("8",session("idevaluacion"),"","")
			
			session("tipoeval")=arrEvaluacion(0,0)
			session("tituloeval")=arrEvaluacion(1,0)
			session("descripcioneval")=arrEvaluacion(2,0)
			session("intrucciones")=arrEvaluacion(3,0)
			session("minutos")=arrEvaluacion(4,0) 			'mostrar minutos arriba
			session("enlinea")=arrEvaluacion(5,0) 			'mostrar calificación
			session("mostrarresultados")=arrEvaluacion(6,0) 'mostrar los resultados
			session("incluirimagenes")=arrEvaluacion(7,0)	'mostrar imagenes en preguntas
			session("modificarrespuesta")=arrEvaluacion(8,0) 'modificar respuesta
			session("preguntaporpregunta")=arrEvaluacion(9,0)'pagina por pagina
			session("retrocederpaginas")=arrEvaluacion(10,0) 'retroceder paginas
			session("respuestacorrecta")=arrEvaluacion(11,0) 'respuesta correcta
			'------------------------------------------------			

			'Verifica el modo de presentación de preguntas de la evaluación
			session("pagina")="modpxp.asp?PagActual=1"
			
			if session("preguntaporpregunta")="0" then
				session("pagina")="modtp.asp"
			end if
			response.redirect "portada.asp"
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