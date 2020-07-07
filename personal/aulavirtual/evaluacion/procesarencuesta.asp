<!--#include file="../../../NoCache.asp"-->
<!--#include file="clsevaluacion.asp"-->
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<html>
<body oncontextmenu="return false">
<%
'on error resume next

Dim Accion
Dim IdEvaluacion
Dim IdPregunta
Dim IdTipoPregunta
Dim IrAPagina
Dim PagActual
Dim OrdenPregunta
Dim ColeccionAlternativa
Dim ObjDiccionario ' Como Diccionario
Dim Respuestas
Dim modificarRpta

	Accion=request.querystring("Accion")
	IdEvaluacion=Request.querystring("IdEvaluacion")
	PagActual=Request.querystring("PagActual")
	tPaginas=Request.querystring("tPaginas")
	PaginaTemporal=PagActual + 1
	IrAPagina=IIF(PaginaTemporal>=tPaginas,tPaginas,PaginaTemporal)
	
	'------------------------------------------
	'Controles de las Preguntas y Respuestas
	'------------------------------------------
	idPregunta=request.form("cIdPregunta")
	idTipoPregunta=request.form("cIdTipoPregunta")
	descripcionrpta=request("descripcionrpta")
	modificarRpta=Request.querystring("modificarRpta")
	'If modificarRpta="" then modificarRpta="No"
	'---------------------------------------------------------
	'Verificar si el usuario ya ha ingresado a la evaluación
	'---------------------------------------------------------
	sURL = Request.ServerVariables("SCRIPT_NAME")
	if Request.ServerVariables("QUERY_STRING") <> "" Then
		sURL = sURL & "?" & Request.ServerVariables("QUERY_STRING")
		session("Pagina")=sURL
		'response.write sURL
	else
		sURL=sURL
		session("Pagina")=sURL
		'response.write sURL
	End if
	
	Sub AgregarItemAlDiccionario(ByVal vIdPregunta,ByVal vRespuesta)
	 	If ObjDiccionario.Exists(vIdPregunta) Then
	 		'Si se encontró el item
			ObjDiccionario.Remove vIdPregunta
			ObjDiccionario.Add vIdPregunta, vRespuesta
		Else
			ObjDiccionario.Add vIdPregunta,vRespuesta
		End If
	End Sub

	Sub EliminarItemDelDiccionario(vIdPregunta)
 		If ObjDiccionario.Exists(vIdPregunta) Then
			ObjDiccionario.Remove vIdPregunta
		Else
			Response.Write "No se ha encontrado la Pregunta " & vIdPregunta
		End If
	End Sub

	Sub AlmacenarTemporalmente(ByVal vIdPregunta,ByVal vRespuesta)
		Dim ContadorItem
		
		'Verifica y Crear el Objeto Diccionario
		If IsObject(Session("Respuestas")) Then
			Set ObjDiccionario = Session("Respuestas")
		Else
			'Si no existe el objeto lo crea e inicializa contador
			Set ObjDiccionario = Server.CreateObject("Scripting.Dictionary")
			Session("ContadorItem") = 0
		End If
	
		'Número de veces que se ha almacenado la pregunta
      	ContadorItem= Session("ContadorItem")
      
		AgregarItemAlDiccionario vIdPregunta, vRespuesta

      	'Incrementar contador a la variable sesión
      	Session("ContadorItem") = Session("ContadorItem") + 1
      	Set Session("Respuestas") = ObjDiccionario 	
	end Sub
		
Set evaluacion=new clsevaluacion
	with evaluacion
		.RestringirInicioSesion=session("codigo_usu")
	
	if accion="GuardarTemporalmente" then
		session("EstadoActual")="UsuarioResponde"
		AlmacenarTemporalmente idPregunta,descripcionrpta
		If cint(PagActual)<cint(tPaginas) then
			response.redirect "modpxp.asp?mostrarbotonvr=si&PagActual=" & IrAPagina
		else
			response.redirect "procesarencuesta.asp?accion=VerRespuestas&PagActual" & PagActual & "&tPaginas=" & tPaginas
		end if
	end if
	
	if accion="GuardarRespuesta" then
		'On error resume next
		session("EstadoActual")="TerminoRespuestas"
	    'Verifica si la variable sesión esta vacio
		If IsObject(Session("Respuestas")) Then
			Set ObjDiccionario = Session("Respuestas")
		End if

	    'Recorre y guardar las preguntas respondidas de la variable session
	    
	    For Each Key in ObjDiccionario
           if key <> "" then
           		call .AgregarRespuesta(session("codigo_acceso"),Key,session("codigo_usu"),ObjDiccionario(Key))
           	end if
      	Next
        if Err.Number>0 then
        	.Cerrar="ERR"
      	else
      		ObjDiccionario.RemoveAll
			Session("ContadorItem")=0
			if session("enlinea")=1 then
				response.redirect "verresultados.asp"
			else
				session("EstadoActual")="TerminoRespuestas"
				.Cerrar="TE"
			end if
      	end if
	end if
	
	if accion="GuardarTodo" then
	On error resume next
	Dim ColeccionIdPregunta,ColeccionRptas
	Dim aIdTipo,aIdPreg,aRpta
		session("EstadoActual")="TerminoRespuestas"    
	    ColeccionIdPregunta=split(request.form("cIdPregunta"),",")
	    ColeccionRptas=split(request.form("descripcionrpta"),",")
	
		if request.form("descripcionrpta")<>"" then
			For a=LBound(ColeccionIdPregunta) to UBound(ColeccionIdPregunta)
				aIdPreg=Trim(ColeccionIdPregunta(a))
				aRpta=trim(ColeccionRptas(a))
			
				If aRpta<>"" then
					call .AgregarRespuesta(session("codigo_acceso"),aIdPreg,session("codigo_usu"),aRpta)
				end if
			Next
		End if
	    
        if Err.Number>0 then
        	.Cerrar="ERR"
		elseif session("enlinea")=1 then
				response.redirect "verresultados.asp"
			else
				.Cerrar="L"
      	end if
	end if
	
	if accion="GuardarEncuesta" then
		On error resume next
		
		arrPgta=.Consultar("3",session("idevaluacion"),"","")
		set ObjPregunta=server.CreateObject ("AulaVirtual.clsEvaluacion")
		for i=lbound(arrPgta,2) to ubound(arrPgta,2)
			rpta=request.form("descripcionrpta" & arrPgta(1,i))
			if rpta<>"" then
				call ObjPregunta.AgregarRespuesta(session("codigo_acceso"),arrPgta(1,i),session("codigo_usu"),rpta)
			end if
		next
		set ObjPregunta=nothing
		
		if Err.Number>0 then
			.Cerrar="ERR"
      	elseif session("enlinea")=1 then
				session("EstadoActual")="TerminoRespuestas"
				response.redirect "verresultados.asp"
			else
				.Cerrar="T"
		end if
	end if
	
	if accion="VerRespuestas" then
		AlmacenarTemporalmente idPregunta,descripcionrpta
		call .MostrarTablaTemporalRptas(ObjDiccionario,session("modificarrespuesta"))
	end if
	
	
	if accion="GuardarEncuestaLibre" then
		
		set obj=server.CreateObject("AulaVirtual.clsAccesoDatos")
			obj.AbrirConexion
				Set rsPregunta=Obj.Consultar("ConsultarEvaluacion","FO",3,session("idEvaluacion"),0,0)
	
				Do While Not rsPregunta.EOF
					rpta=request.form("descripcionrpta" & rsPregunta("idPregunta"))
					if rpta<>"" then
						call obj.Ejecutar("AgregarRespuesta",false,session("codigo_acceso"),rsPregunta("idPregunta"),session("Usuario_bit"),rpta)
					end if	
					rsPregunta.movenext
				Loop
	
			if Err.Number>0 then
				mensaje="Ha ocurrido un error al Grabar la Encuesta"
				pagina="../../../index.asp"
		    else
				mensaje=Obj.Ejecutar("TerminarEvaluacion",true,session("Usuario_bit"),session("codigo_acceso"),null)
				pagina="../../../index.asp"
			end if
		Obj.CerrarConexion
		set obj=nothing	
	
		response.write "<script language='Javascript'>alert('" & mensaje & "');top.location.href='" & pagina & "'</script>"
	end if
	
end with
Set evaluacion=nothing
%>
</body>
</html>