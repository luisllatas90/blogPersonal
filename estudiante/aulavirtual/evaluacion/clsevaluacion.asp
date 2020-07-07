<!--#include file="../../../funcionesaulavirtual.asp"-->
<%
' Proyecto Cursos Virtuales - USAT
' Autor: Gerardo Chunga Chinguel
' Clase generada: Martes, 07 de Noviembre de 2005 11:58:00 a.m.

Class clsevaluacion
  Dim idevaluacion
  
  Public Property LET Restringir(strX)
  	if strX="" then
  		response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"
  	end if
  End Property
  
  Public Property LET RestringirInicioSesion(strX)
   	if strX="" then
  		response.redirect "cerrarevaluacion.asp?decision=si"
  	end if
  End Property
  
  Public Property LET codigo_eval(strX)
  	idevaluacion=strX
  End Property
  
  Public Property LET Cerrar(strModo)
  	Select case strModo
  		case "A"
  			response.redirect "../usuario/frmagregarusuariosrecurso.asp?accion=agregarpermisos&idtabla=" & idevaluacion & "&nombretabla=evaluacion"
  		case "R"
			response.write "<script>alert(""No se pudo registrar correctamente la encuesta. Por favor intente denuevo"");history.back(-1)</script>"
  		case "M"
  			response.write "<script>top.window.opener.location.reload();top.window.close()</script>"
 		case "EE" 'Eliminar evaluación y actualizara lista
 			response.write "<script>parent.location.reload()</script>"  			
  		case "Q"
  			response.redirect "listapreguntas.asp?idevaluacion=" & idevaluacion
  		case "ES" 'Bloquear evaluación
  			response.write "<script>alert(""Por el momento esta evaluacion está bloqueada. Cierre todas las ventanas del Internet Explorer"");top.window.close()</script>"
  		case "CS" 'Cerrar sesion
  			response.write "<script>window.opener.location.reload();top.window.close()</script>"
  		case "TE" 'Finalizar encuesta sin ver resultados
  			response.write "<script>top.window.close()</script>"
  		case "ERR" 'Error al guardar todas las preguntas
			response.write "<script>" & _
					"alert(""No se pudo registrar corectamente algunas de las respuestas. Verifique Por favor."");" & _
					"top.window.close()" & _
					"</script>"
		case "T" 'Terminar encuesta correctamente
			session("EstadoActual")="TerminoRespuestas"
			response.write "<script>alert(""Ha finalizado correctamente la encuesta"");location.href='abrirevaluacion.asp?Accion=terminarencuesta'</script>"
	
		case "L" 'Terminar encuesta libre, correctamente
			session("EstadoActual")="TerminoRespuestas"
			response.write "<script>alert(""Ha finalizado correctamente la encuesta\n Gracias por responder a las preguntas."");location.href='abrirevaluacionlibre.asp?Accion=terminarencuesta'</script>"

  	End select
  End Property
  
  Public Function Mostrarestado(byval idestado,byval limite,byval accesos,byval idevaluacion)
  
  	if idestado=1 then
     	if accesos<=limite then%>
		  	<img src="../../../images/img5.gif" style="cursor:hand" onClick="AbrirEvaluacion('I','<%=idevaluacion%>')"><br>Iniciar encuesta
		<%else%>
		  	<br><img src="../../../images/img4.gif"><br>Encuesta realizada<br>
	  	<%end if
	else%>
		<img src="../../../images/bloquear.gif"><br>Encuesta bloqueda. Ha finalizado la fecha límite de publicación
	<%end if
  End Function
  
  Public Function Consultar( byVal tipo, byVal param1, byVal param2, byVal param3)
  	Set Obj= Server.CreateObject("AulaVirtual.clsevaluacion")
  		Consultar=Obj.Consultar(tipo,param1,param2,param3)
  	Set Obj= Nothing
  End Function

	Public sub EncuestasDelUsuario(ByVal codigousuario,ByVal codigocursovirtual,codigotarea)
	Dim ArrTemp
	ArrTemp=Consultar("12",codigousuario,codigocursovirtual,"")

	if IsEmpty(ArrTemp)=false then
		for j=lbound(ArrTemp,2) to Ubound(ArrTemp,2)
			response.write "<li><span id=""doc" & ArrTemp(0,j) & """>" & VerificarEncuestaAsignada(ArrTemp(5,j),ArrTemp(0,j)) & " &nbsp;" & ArrTemp(1,j) & "</span></li>" & vbNewLine
		next
	end if
   End Sub

   Private Function VerificarEncuestaAsignada(Byval existe,byval iddoc)
		script=" onClick=""ElegirDocumento(this)"""
		if (clng(existe)=0) then
			VerificarEncuestaAsignada="<input type=checkbox name=ArrArchivos value=""" & iddoc & """ id=""chk" & iddoc & """" & script & ">"
		else
			VerificarEncuestaAsignada="<img src='../../../images/bloquear.gif' ALT='Encuesta asignado a la Tarea'>"
		end if
	End Function

  Public Function Agregar(Byval idcategoria,Byval tituloevaluacion,Byval fechainicio,Byval fechafin,Byval descripcion,Byval instrucciones,Byval idcursovirtual,Byval idusuario,Byval enlinea,Byval mostrarresultados,Byval incluirimagenes,Byval modificarrespuesta,Byval preguntaporpregunta,Byval retrocederpaginas,Byval respuestacorrecta,Byval vecesacceso,Byval minutos,Byval idtipopublicacion)
  	Set Obj= Server.CreateObject("AulaVirtual.clsevaluacion")
  		Agregar=Obj.Agregar(idcategoria, tituloevaluacion, fechainicio, fechafin, descripcion, instrucciones, idcursovirtual,idusuario, enlinea, mostrarresultados, incluirimagenes, modificarrespuesta, preguntaporpregunta, retrocederpaginas, respuestacorrecta, vecesacceso, minutos,idtipopublicacion)
  	Set Obj=Nothing
  End Function

  Public Function Modificar(Byval idevaluacion,Byval idcategoria,Byval tituloevaluacion,Byval fechainicio,Byval fechafin,Byval descripcion,Byval instrucciones,Byval enlinea,Byval mostrarresultados,Byval incluirimagenes,Byval modificarrespuesta,Byval preguntaporpregunta,Byval retrocederpaginas,Byval respuestacorrecta,Byval vecesacceso,Byval minutos)
  	Set Obj= Server.CreateObject("AulaVirtual.clsevaluacion")
  		Call Obj.Modificar(idevaluacion,idcategoria, tituloevaluacion, fechainicio, fechafin, descripcion, instrucciones,enlinea, mostrarresultados, incluirimagenes, modificarrespuesta, preguntaporpregunta, retrocederpaginas, respuestacorrecta, vecesacceso, minutos)
  	Set Obj=Nothing
  End Function

  Public Function Eliminar(byVal Idevaluacion)
	Set Obj= Server.CreateObject("AulaVirtual.clsevaluacion")
  		Eliminar=Obj.Eliminar(Idevaluacion)
  	Set Obj=Nothing
  End Function

  Public Function AgregarPregunta(Byval idevaluacion,Byval IdTipoPregunta,Byval ordenpregunta,Byval titulopregunta,Byval pjebueno,Byval pjemalo,Byval pjeblanco,Byval obligatoria,Byval duracion,Byval URL,Byval valorpredeterminado)
  	Set Obj= Server.CreateObject("AulaVirtual.clsevaluacion")
  		AgregarPregunta=Obj.AgregarPregunta(idevaluacion, IdTipoPregunta, ordenpregunta, titulopregunta, pjebueno, pjemalo, pjeblanco, obligatoria, duracion,URL,valorpredeterminado)
  	Set Obj=Nothing
  End Function

  Public Function ModificarPregunta(Byval idpregunta,Byval IdTipoPregunta,Byval ordenpregunta,Byval titulopregunta,Byval pjebueno,Byval pjemalo,Byval pjeblanco,Byval obligatoria,Byval duracion,Byval URL,Byval valorpredeterminado)
  	Set Obj= Server.CreateObject("AulaVirtual.clsevaluacion")
  		call Obj.ModificarPregunta(idpregunta, idtipopregunta, ordenpregunta, titulopregunta, pjebueno, pjemalo, pjeblanco, obligatoria, duracion, URL,valorpredeterminado)
  	Set Obj=Nothing
  End Function
  
  Public Function EliminarPregunta(byVal idPregunta)
	Set Obj= Server.CreateObject("AulaVirtual.clsevaluacion")
  		EliminarPregunta=Obj.EliminarPregunta(idPregunta)
  	Set Obj=Nothing
  End Function
  
  '---------------------------------------------------------------------------------
  'Métodos para registrar las alternativas
  '---------------------------------------------------------------------------------
 
  Public Sub AgregarAlternativa(byval idpregunta,byval tituloalternativa,byval rptacorrecta,byval orden,byval mensaje)
	set Obj=server.CreateObject ("AulaVirtual.clsEvaluacion")
		call Obj.AgregarAlternativa(idpregunta,tituloalternativa,rptacorrecta,orden,mensaje)
	set Obj=nothing
  End Sub
	
  Public Sub ModificarAlternativa(byval idalternativa,byval idtipopregunta,byval idpregunta,byval tituloalternativa,byval rptacorrecta,byval orden,byval mensaje)
	set Obj=server.CreateObject ("AulaVirtual.clsEvaluacion")	
		call Obj.ModificarAlternativa(idalternativa,idtipopregunta,idpregunta,tituloalternativa,rptacorrecta,orden,mensaje)
	set Obj=nothing
  End Sub
	
  Public Sub EliminarAlternativa(Byval idalternativa)
	set Obj=server.CreateObject ("AulaVirtual.clsEvaluacion")
		call Obj.eliminarAlternativa(idalternativa)
	set Obj=nothing
  End Sub
	
	Function ImprimirControl(idtipo,valormarca)
		Select case idtipo
			case 2,3:ImprimirControl="<input type=""radio"" name=""rptacorrecta"" " & marcar(valormarca,1) & ">"
			case 6	:ImprimirControl="<input type=""checkbox"" name=""rptacorrecta"" " & marcar(valormarca,1) & ">"
	        'case 7	:ImprimirControl="<select name=""rptacorrecta""></select>"
        end select
	End Function
  
  
  '---------------------------------------------------------------------------------
  'Métodos para procesar información de los inicios de sesión en la evaluación
  '---------------------------------------------------------------------------------
  Public Function Iniciar(Byval idusuario,byval ideval,byval ip)
	Set Obj= Server.CreateObject("AulaVirtual.clsEvaluacion")
		Iniciar=Obj.Iniciar(idusuario,ideval,ip)
	Set Obj=nothing
  End Function
  
  Public Function Terminar(ByVal idusuario,Byval idinicio)
  	if idinicio="" then idinicio=0
	Set Obj= Server.CreateObject("AulaVirtual.clsEvaluacion")	
		Call Obj.Terminar(idusuario,idinicio)
	Set Obj=nothing
  End function
  
  Public Sub MostrarTablaTemporalRptas(byVal objdic,byval modrpta)
		Dim RecuperaDatos
		Dim Key%>
		<input type="button" onclick="location.href='procesarencuesta.asp?accion=GuardarRespuesta'" value="    Guardar todo" name="cmdGuardar" class="guardar">
		<p class="e1">Respuestas realizadas</p>
		<table cellspacing="0" cellpadding="3" border="1" style="border-collapse: collapse" bordercolor="#111111" width="90%">
			<tr class="eTabla">
                <td width="56">Pregunta</td>
                <td width="389">Enunciado de la Pregunta</td>
                <td width="506">Respuesta (s)</td>
                <%if modrpta=1 then%>
                <td width="120">&nbsp;</td>
                <%end if%>
            </tr>
      	<%'Mostar el contenido de la variable sesión
           For Each Key in objdic
	           if Key <> "" then
                	RecuperaDatos=BuscarEnBDyObtenerDatos(Key)
                	%>
                     <tr> 
                          <td width="56" valign="top"># <%=RecuperaDatos(0)%>&nbsp;</td>
                          <td width="389" valign="top"><%=PreparaMemo(RecuperaDatos(1))%>&nbsp;</td>
                          <td width="506" valign="top"><%=ProcesarIDRpta(objdic(Key))%>&nbsp;</td>
                          <%if modrpta=1 then%>
                          <td width="120" align="center" valign="top">
                          <a href="modpxp.asp?mostrarbotonvr=si&modificarRpta=Si&IdPregunta=<%=Key%>&descripcionrpta=<%=objdic(Key)%>">
                          <img border="0" src="../../../images/editar.gif">Modificar</a></td>
                          <%end if%>
                     </tr>
      			<%End If
           Next%>
         </table>
   <%End Sub
  
  Public Function BuscarEnBDyObtenerDatos(ByVal cIdPregunta)
	Dim Datos,ArrPregunta
		ArrPregunta=Consultar("5",cIdPregunta,"","")
		Datos = Array(ArrPregunta(0,0),ArrPregunta(1,0))
		BuscarEnBDyObtenerDatos = Datos
  End Function
  
  Private function BuscarAlternativa(ByVal cIdAlternativa,ByVal RptaCorrecta)
	'On error resume next
		Dim ArrTemp
		ArrTemp=Consultar("6",cIdAlternativa,"","")
		If IsEmpty(ArrTemp)=false then
			if RptaCorrecta=1 then
				BuscarAlternativa= ArrTemp(1,0) & IIF(ArrTemp(2,0)=1,"<b>&nbsp;&nbsp;(Respuesta Correcta)</b>","")
			else
				BuscarAlternativa=ArrTemp(1,0)
			end if
		end if
   end function
	
	function ProcesarIDRpta(ByVal cIdAlternativa)
		Dim ColeccionRptas,cadena,Pos
		ColeccionRptas=split(cIdAlternativa,",")
		if cIdAlternativa<>"" then
			For a=LBound(ColeccionRptas) to UBound(ColeccionRptas)
				nAlternativa=Trim(ColeccionRptas(a))
				If IsNumeric(nAlternativa) then
					cadena="- " & BuscarAlternativa(nAlternativa,session("respuestacorrecta")) & "<br>" & cadena
				Else
					cadena=cIdAlternativa
					Exit for
				end if
			Next
			ProcesarIDRpta=PreparaMemo(cadena)
		End if
	end function
	
	function AgregarRespuesta(ByVal idinicio,ByVal tIdPregunta,Byval idusuario,ByVal tDescripcionRpta)
		set ObjPregunta=server.CreateObject ("AulaVirtual.clsEvaluacion")
			call ObjPregunta.AgregarRespuesta(idinicio,tIdPregunta,idusuario,tDescripcionRpta)
		set ObjPregunta=nothing	
	end function

	Private function MarcarAlternativaElejida(ByVal cIdAlternativa)
		Dim ColeccionRptas
		ColeccionRptas=split(descripcionrpta,",")
		'response.write cRespuesta
		if descripcionrpta<>"" then
			For a=LBound(ColeccionRptas) to UBound(ColeccionRptas)
				nRpta=Trim(ColeccionRptas(a))
					if int(cIdAlternativa)=int(nRpta) then
						MarcarAlternativaElejida="checked"
					end if
			Next
		End if
	end function

	Public Sub CargarAlternativas(ByVal IdPta,ByVal Tipo)
		Dim ArrAlternativas
			ArrAlternativas=Consultar("7",IdPta,"","")
   
    	If IsEmpty(ArrAlternativas)=false then%>
	      <table border="0" cellpadding="2" cellspacing="0" style="border-width:0; border-collapse: collapse" bordercolor="#EBEBEB" width="80%">
    	  <%for a=Lbound(ArrAlternativas,2) to Ubound(ArrAlternativas,2)%>
	      <tr>
    	    <td width="5%" align="center">
        		<%if Tipo=2 or Tipo=3 then%>
        			<input type="radio" name="descripcionrpta" value="<%=ArrAlternativas(0,a)%>" idPre="Pregunta<%=IdPta%>" <%=MarcarAlternativaElejida(ArrAlternativas(0,a))%>>
        		<%end if
        		if Tipo=6 then%>
        			<input type="checkbox" name="descripcionrpta" value="<%=ArrAlternativas(0,a)%>" idPre="Pregunta<%=IdPta%>" <%=MarcarAlternativaElejida(ArrAlternativas(0,a))%>>
        		<%end if%>
        	</td>
	        <td width="80%"><%=ArrAlternativas(1,a)%></td>
    	    <td><span id="mensaje<%=int(NumId & a)%>"></span></td>
	      	</tr>
			<%Next%>
    	</table>
    	<%end if
	End Sub

	Public Sub CargarAlternativas2(ByVal IdPta,ByVal Tipo)
		Dim ArrAlternativas
			ArrAlternativas=Consultar("7",IdPta,"","")
   
    	If IsEmpty(ArrAlternativas)=false then%>
	      <table border="0" cellpadding="2" cellspacing="0" style="border-width:0; border-collapse: collapse" bordercolor="#EBEBEB" width="80%">
    	  <%for a=Lbound(ArrAlternativas,2) to Ubound(ArrAlternativas,2)%>
	      <tr>
    	    <td width="5%" align="center">
        		<%if Tipo=2 or Tipo=3 then%>
        			<input type="radio" name="descripcionrpta<%=idPta%>" value="<%=ArrAlternativas(0,a)%>" idPre="Pregunta<%=IdPta%>">
        		<%end if
        		if Tipo=6 then%>
        			<input type="checkbox" name="descripcionrpta<%=idPta%>" value="<%=ArrAlternativas(0,a)%>" idPre="Pregunta<%=IdPta%>">
        		<%end if%>
        	</td>
	        <td width="80%"><%=ArrAlternativas(1,a)%></td>
    	    <td><span id="mensaje<%=int(NumId & a)%>"></span></td>
	      	</tr>
			<%Next%>
    	</table>
    	<%end if
	End Sub


	function MostrarRptaAlernativa(ByVal cIdAlternativa,ByVal Texto,ByVal cRespuestasMarcadas)
		Dim ColeccionRptas,nRpta,t
		ColeccionRptas=split(cRespuestasMarcadas,",")
		if cRespuestasMarcadas<>"" then
			For t=LBound(ColeccionRptas) to UBound(ColeccionRptas)
				nRpta=Trim(ColeccionRptas(t))
				if int(cIdAlternativa)=int(nRpta) then
					MostrarRptaAlernativa="<font color=""#006600""><u>" & Texto & "</u><img border=""0"" src=""../../../images/ok.gif"" width=""10"" height=""10""></font>"
					Exit For
				else
					MostrarRptaAlernativa=Texto
				end if
			Next
		End if
	end function

	function RecuperaRespuestas(ByVal cIdPregunta,ByVal cRespuestasMarcadas,rtacorrecta)
	Dim cadena,r
		ColeccionRptas=split(cRespuestasMarcadas,",")
		if cRespuestasMarcadas<>"" then
			For r=LBound(ColeccionRptas) to UBound(ColeccionRptas)
				nAlternativa=Trim(ColeccionRptas(r))
				cadena=BuscarAlternativa(nAlternativa,rptacorrecta) & "<br>" & cadena
			Next
			RecuperaRespuestas=PreparaMemo(cadena)
		end if
	end function

	Sub RptaAlternativaElegida(IdPta,cRpta)
		Dim ArrAlternativas,a
		ArrAlternativas=Consultar("7",IdPta,"","")
				
		If IsEmpty(ArrAlternativas)=false then
			response.write "<ol>"
			for a=lbound(ArrAlternativas,2) to Ubound(ArrAlternativas,2)
				response.write "<li>" & MostrarRptaAlernativa(trim(ArrAlternativas(0,a)),ArrAlternativas(1,a),cRpta)
				'response.write "<li>" & ArrAlternativas(1,a)
			next
			'response.write "</ol>"
		end if
	end sub
	
	function ConsultarAlternativasPregunta(ByVal idpta)
		Dim ArrAlternativas,a
		ArrAlternativas=Consultar("7",IdPta,"","")
				
		If IsEmpty(ArrAlternativas)=false then
			response.write "<ol>"
			for a=lbound(ArrAlternativas,2) to Ubound(ArrAlternativas,2)
				response.write "<li>" & ArrAlternativas(1,a)
			next
			'response.write "</ol>"
		end if
	end function

	function MostrarRptaUsuario(byVal idPta,byVal idusu)
		Dim arrRpta
		
		ArrRpta=Consultar("15",idpta,idusu,"")
		if IsEmpty(ArrRpta)=false then
			if (ArrRpta(1,0)=1 or ArrRpta(1,0)=5) then
				MostrarRptaUsuario=PreparaMemo(ArrRpta(0,0))
			else
				MostrarRptaUsuario=RecuperaRespuestas(idPta,ArrRpta(0,0),1)				
			end if
		end if
	end function

  	Public Function Detalle(ByVal idevaluacion,ByRef tipoeval,ByRef tituloeval,ByRef descripcioneval,ByRef instrucciones,ByRef minutos,ByRef enlinea,ByRef mostrarresultados,ByRef incluirimagenes,ByRef modificarRespuesta,ByRef preguntaxpregunta,ByRef Retrocederpaginas,ByRef respuestacorrecta)
		Dim ArrEvaluacion
		
		ArrEvaluacion=Consultar("7",idevaluacion,"","")
		tipoeval=arrEvaluacion(0,0)
		tituloeval=arrEvaluacion(1,0)
		descripcioneval=arrEvaluacion(2,0)
		intrucciones=arrEvaluacion(3,0)
		minutos=arrEvaluacion(4,0) 'mostrar minutos arriba
		enlinea=arrEvaluacion(5,0) 'mostrar calificación
		mostrarresultados=arrEvaluacion(6,0) 'mostrar los resultados
		incluirimagenes=arrEvaluacion(7,0)
		modificarrespuesta=arrEvaluacion(8,0) 'modificar respuesta
		preguntaporpregunta=arrEvaluacion(9,0) 'pagina por pagina
		retrocederpaginas=arrEvaluacion(10,0) 'retroceder paginas
		respuestacorrecta=arrEvaluacion(11,0) 'respuesta correcta
	end function

	Public Function PaginarPreguntas(ByVal ideval,Byval iRegsPorPag,byval iPag,Byval vector,Byval retrocederpag,byVal modrpta,ByVal mostrarrpta,byVal mostrarbto)
			Dim I, J 'Se utilizan para recorrer el vector
			Dim iTotal, iComienzo, iFin 'Total de registros, registro en que empezamos y registro en que terminamos
			Dim tPaginas
			Dim Mensaje

			'Hallo el Total de registros devueltos
			iTotal = UBound(vector, 2) + 1
			
			'Calculo el numero de páginas que tenemos
			tPaginas = (iTotal \ iRegsPorPag)
			
			'Si daba decimales, añado una más para mostrar los últimos registros
			If iTotal Mod iRegsPorPag > 0 Then
    			tPaginas = tPaginas + 1
			End If
			
			'Si no es una página válida, comienzo en la primera
			If iPag < 1 Then iPag = 1

			'Si es una página mayor al nº de páginas, comienzo en la última
			If iPag > tPaginas Then iPag = tPaginas
			
			Mensaje= "<font color=""#FF0000""><b>" & iPag & "</b></font> / " & tPaginas & ""

			'Calculo el índice donde comienzo:
			iComienzo = (iPag - 1) * iRegsPorPag

			'y donde termino:
			iFin = iComienzo + (iRegsPorPag - 1)

			'Si no tengo suficientes registros restantes,
			'voy hasta el final
			If iFin > UBound(vector, 2) Then
    			iFin = UBound(vector, 2)
			End If
			'Pinto la tabla
			
			'If (retrocederpag=1 and modrpta="No") then%>
			<!---
		<table class="contorno" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%">
			<tr>
				<td width="50%" align="right" height="25" bgcolor="#FBF5D2">
				<input onclick="location.href='modpxp.asp?idEvaluacion=<%=idEval%>&PagActual=1'" type="button" value="|&lt;" name="cmdPrimero" class="navegacion" alt="Ir a Primera Pregunta">&nbsp;
				<input onclick="location.href='modpxp.asp?idEvaluacion=<%=idEval%>&PagActual=<%=(iPag-1)%>'" type="button" value="&lt;&lt;" name="cmdAnterior" class="navegacion" alt="Ir a anterior Pregunta">
				<input onclick="location.href='modpxp.asp?idEvaluacion=<%=idEval%>&PagActual=<%=(iPag+1)%>'" type="button" value="&gt;&gt;" name="cmdSiguiente" class="navegacion" alt="Ir a siguiente Pregunta">
				<input onclick="location.href='modpxp.asp?idEvaluacion=<%=idEval%>&PagActual=<%=tPaginas%>'" type="button" value="&gt;|" name="cmdUltimo" class="navegacion" alt="Ir a última Pregunta">     
				</td>
			</tr>
			</table>
			-->
			<%'end if%>
		<br>
		<form name="frmPreguntas" method="post" onSubmit="return ValidarFormulario(this)" action="procesarencuesta.asp?Accion=GuardarTemporalmente&PagActual=<%=PagActual%>&tPaginas=<%=tPaginas%>&modificarRpta=<%=modrpta%>">
		<%For I= iComienzo to iFin%>
		<table border="0" cellpadding="5" cellspacing="0" width="100%">
		<tr class="encabezadopregunta">
			<td width="11%" valign="top">Pregunta <%=i+1%> / <b><%=tPaginas%></b>:</td>
			<td width="57%" valign="top" style="text-align: left"><%=PreparaMemo(vector(5,I))%>&nbsp;</td>
			<td width="32%" style="text-align: right" valign="top">
			<%if mostrarrpta=1 and mostrarbto="si" and retrocederpag=1 then%>
			<input onclick="location.href='procesarencuesta.asp?accion=VerRespuestas&PagActual=<%=PagActual%>&tPaginas=<%=tPaginas%>'" type="button" value="Ver respuestas" name="cmdVerRespuestas" class="navegacion" style="width: 100">
			<%end if%>
			</td>
		</tr>
		<tr>
			<td width="100%" colspan="3">
			<%Select case vector(2,I)
    			case 1%>
    				<input type="text" name="descripcionrpta" size="80" value="<%=descripcionrpta%>" class="Cajas" style="width: 500" idPre="Pregunta<%=vector(1,I)%>">
    			<%case 2
    				CargarAlternativas vector(1,I),vector(2,I)
    			case 3
    				CargarAlternativas vector(1,I),vector(2,I)
    			'case 4
    			case 5
    				if modrpta="No" then%>
						<textarea rows="4" name="descripcionrpta" cols="80" class="Cajas" style="width: 500; height:100" idPre="Pregunta<%=vector(1,I)%>"><%=vector(13,I)%></textarea>
    				<%else%>
    					<textarea rows="4" name="descripcionrpta" cols="80" class="Cajas" style="width: 500; height:100" idPre="Pregunta<%=vector(1,I)%>"><%=descripcionrpta%></textarea>
    				<%end if
    			case 6
    				CargarAlternativas vector(1,I),vector(2,I)
    			'case 7
			End select%> &nbsp;
			<!--
			Los valores se almacenan en campos ocultos, ya que se puede Guardar mostrarndo varias preguntas
			-->
			<input type="hidden" name="cIdTipoPregunta" value="<%=vector(2,I)%>">
			<input type="hidden" name="cIdPregunta" value="<%=vector(1,I)%>">
			</td>
		</tr>
		<%if vector(7,I)<>"" then%>
		<tr>
			<td width="100%" colspan="3">
    			<b>Referencias:</b><br>
    			<%if vector(7,I)<>"" then%><br><br><b>Página web:</b> <a target="_blank" href="<%=vector(7,I)%>"><%=vector(7,I)%></a><%end if%>
			&nbsp;</td>
		</tr>
		<%end if%>
		</table>
		<p align="center">
        <input type="submit" value="Siguiente" name="cmdGuardar" class="siguiente" style="width: 100; float:left"></p>
		</form>
		<%Next
		PaginarPreguntas= 0
	End Function

End Class
%>