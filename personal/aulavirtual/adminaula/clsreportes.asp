<!--#include file="../../../funciones.asp"-->
<%
Class clsReportes
	Dim RutaAula
	
	Private Sub Class_Initialize()
    	RutaAula="../../../../../dpdu/"
	End Sub
	
  Public Property LET Restringir(strX)
  	if strX="" then
  		response.redirect "../../../tiempofinalizado.asp"
  	end if
  End Property

	Private Function ObtenerTipoIcono(byval tipo,byval narchivo)
		if tipo="C" then
			ObtenerTipoIcono="cerrado.gif"
		else
			ObtenerTipoIcono="ext/" & right(narchivo,3) & ".gif"
		end if
	End Function

	Public Function CargarRpteDocumentos(ByVal RefIdDocumento,ByVal prefix,ByVal icursovirtual)
		Dim ArrCarpeta, NodoIzquiero, preadd, Resultado, NodoAbierto,tipo
	  	Set Obj= Server.CreateObject("AulaVirtual.clsdocumento")
  			ArrCarpeta=Obj.Consultar("11","",RefIdDocumento,icursovirtual,"")
  		Set Obj= Nothing

		If IsEmpty(ArrCarpeta) then%>
			<tr><td class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han definido Carpetas y/o documentos en el curso virtual</td></tr>
		<%Else
			preadd = ""
			Resultado = ""
	
			FOR C=Lbound(ArrCarpeta,2) to Ubound(ArrCarpeta,2)
				NodoAbierto = 0
				NodoIzquierdo=ArrCarpeta(7,C) 'Número de Nodos
				NodoIzquiero = NodoIzquiero - 1
				Resultado=Resultado & "<tr><td>" & prefix

				if NodoIzquiero then
					Resultado = Resultado & "<img src='../../../images/beforenode.gif' align=absbottom>"
				else
					Resultado = Resultado & "<img src='../../../images/beforelastnode.gif' align=absbottom>"
				end if
		
				if ArrCarpeta(7,C) > 0 then
					NodoAbierto = instr(NumNodoAbierto,"[" & ArrCarpeta(0,C) & "]")
					if NodoAbierto > 0 then
						Resultado = Resultado & enlaceNodo("A","rptedocumento.asp",ArrCarpeta(0,C),"")
					else
						Resultado=Resultado & enlaceNodo("C","rptedocumento.asp",ArrCarpeta(0,C),"")
					end if
				else
					Resultado=Resultado & "<img src='../../../images/nodeleaf.gif' align=absbottom>"
			end if
				tipo="E"
				if ArrCarpeta(3,C)="C" then
					Resultado = Resultado & "&nbsp;<img border=""0"" src=""../../../images/" & ObtenerTipoIcono(ArrCarpeta(3,C),ArrCarpeta(4,C)) & """ align=absbottom ALT=""" & ArrCarpeta(1,C) &  """>&nbsp;" & _
							AgregarSpan(ArrCarpeta(0,C),ArrCarpeta(1,C),"","E") & "</td></tr>" & chr(13) & chr(10)
				else
					if ArrCarpeta(8,C)>0 then
						tipo="rptedescargas.asp?iddocumento=" & ArrCarpeta(0,C)
					end if
					Resultado = Resultado & "&nbsp;<img border=""0"" src=""../../../images/" & ObtenerTipoIcono(ArrCarpeta(3,C),ArrCarpeta(4,C)) & """ align=absbottom ALT=""" & ArrCarpeta(1,C) &  """>&nbsp;" & _
					'---------------------------------------------------------------------------------------------------------------
                    'Fecha: 29.10.2012
                    'Usuario: dguevara
                    'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                    '---------------------------------------------------------------------------------------------------------------
							AgregarSpan(ArrCarpeta(0,C),ArrCarpeta(1,C) & " (" & ArrCarpeta(8,C) & " descargas)","../../../archivoscv/" & ArrCarpeta(4,C),tipo) & "</td></tr>" & chr(13) & chr(10)
				end if
						
			
				if NodoAbierto then
					if NodoIzquiero then
						preadd = "<img src='../../../images/beforechild.gif' align=absbottom>"
					else
						preadd = "<img src='../../../images/beforelastchild.gif' align=absbottom>"
					end if
					Resultado = Resultado & CargarRpteDocumentos(ArrCarpeta(0,C), prefix & preadd,icursovirtual)
				end if
			NEXT
		end if

		CargarRpteDocumentos= Resultado
	end function

	Public Sub CargarRpteEncuestas(ByVal icursovirtual)
		Dim ArrCarpeta
		Set Obj= Server.CreateObject("AulaVirtual.clsEvaluacion")
  			ArrCarpeta=Obj.Consultar("13",icursovirtual,"","")
  		Set Obj= Nothing

		If IsEmpty(ArrCarpeta) then%>
			<tr><td class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han definido Encuestas en el curso virtual</td></tr>
		<%Else%>
		<table align="right" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%">
			<%for j=lbound(ArrCarpeta,2) to Ubound(ArrCarpeta,2)%>
			<tr>
				<td width="3%">
                <img border="0" src="../../../images/mas.gif" id="img<%=ArrCarpeta(0,j)%>" onclick="MostrarTabla(tbl<%=ArrCarpeta(0,j)%>,'../../../images/',this)"></td>
				<td width="3%" align="left"><img border=""0"" src="../../../images/prop.gif" align=absbottom>
				<td width="94%"><%=AgregarSpan(ArrCarpeta(0,j),ArrCarpeta(1,j),"../evaluacion/xlsencuesta.asp?idevaluacion=" & ArrCarpeta(0,j),"E")%>&nbsp;</td>
			</tr>
				<%call CargarEncuestados(ArrCarpeta(0,j))
			next%>
		</table>
		<%end if
	end sub

	Private function CargarEncuestados(ByVal idevaluacion)
		Dim ArrRpta,cadena
	  	Set Obj= Server.CreateObject("AulaVirtual.clsEvaluacion")
  			ArrRpta=Obj.Consultar("14",idevaluacion,"","")
  		Set Obj= Nothing
		
		cadena="<tr style=""display:none"" id=tbl" & idevaluacion & "><td colspan=3 align=right><table width=""95%"">"
		If IsEmpty(ArrRpta) then
			cadena=cadena & "<tr><td class=""sugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han registrado participantes que respondieron a la encuesta</td></tr>"
		else
			for i=lbound(ArrRpta,2) to Ubound(ArrRpta,2)
				encuesta="../evaluacion/verresultados.asp?respuestacorrecta=1&exportar=S&idevaluacion=" & idevaluacion & "&codigo_acceso=" & ArrRpta(2,i)
				cadena=cadena & "<tr><td width=""3%"">" & (i+1) & "</td><td width=""97%"">" & AgregarSpan(ArrRpta(0,i),ArrRpta(1,i),encuesta,"E") & "</td></tr>"
			next
		end if				
		cadena=cadena & "</table></td></tr>"
		response.write(cadena)
	End function
	
	Private function AgregarSpan(byVal id,byval texto,byval pagina,byval tipo)
		if pagina="" then
			AgregarSpan="<span id=""doc" & id & """ onClick=""ElegirRecurso(this);parent.cmdabrirrecurso.style.display='none';parent.cmddescargas.style.display='none'"">" & texto & "</span>"
		else
			AgregarSpan="<span id=""doc" & id & """ onClick=""AbrirRecursoSeleccionado(this,'" & pagina & "','" & tipo & "')"">" & texto & "</span>"
		end if
	End function
	
	Private function enlaceNodo(byval modo,byval pagina,byval id,byval variables)
		if variables<>"" then
			variables=variables & "&"
		end if
	
		if modo="A" then
			enlaceNodo= "<a href='" & pagina & "?" & variables & "NumNodoAbierto=" & _
					replace(NumNodoAbierto, "[" & id & "]", "") & "'>" & _
					"<img src='../../../images/NodoAbierto.gif' align=absbottom></a>"
		else
			enlaceNodo="<a href='" & pagina & "?" & variables & "NumNodoAbierto=" & _
					NumNodoAbierto & "[" & id & "]'>" & _
					"<img src='../../../images/NodoCerrado.gif' align=absbottom></a>"
		end if
	end function
	
	Public function CargarDescargas(ByVal iddocumento)
		Set Obj= Server.CreateObject("AulaVirtual.clsDocumento")
  			CargarDescargas=Obj.Consultar("12",iddocumento,"","","")
  		Set Obj= Nothing
	End function


	Public Sub CargarRpteForos(ByVal icursovirtual,byval iforo)
		Dim ArrForo,j
		Dim estilotbl,imgtbl,rutaimg
		
		Set Obj= Server.CreateObject("AulaVirtual.clsForo")
  			ArrForo=Obj.Consultar("1",icursovirtual,"","")
  		Set Obj= Nothing
		
		If IsEmpty(ArrForo) then%>
			<tr><td class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han definido Foros de discusión en el curso virtual</td></tr>
		<%Else%>
		<table align="right" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%">
			<%for j=lbound(ArrForo,2) to Ubound(ArrForo,2)
				estilotbl="":imgtbl=""
				if trim(iforo)=trim(ArrForo(0,j)) then
					estilotbl=""
					imgtbl="<img border=""0"" src=""../../../images/menos.gif"" id=""img" & ArrForo(0,j) & """ onclick=""MostrarTabla(tbl" & ArrForo(0,j) & ",'../../../images/',this)"">"
				elseif ArrForo(4,j)>0 then
					estilotbl=" style=""display:none"" "
					imgtbl="<img border=""0"" src=""../../../images/mas.gif"" id=""img" & ArrForo(0,j) & """ onclick=""MostrarTabla(tbl" & ArrForo(0,j) & ",'../../../images/',this)"">"
				end if
			%>
			<tr>
				<td width="3%"><%=imgtbl%></td>
				<td width="3%"><img border=""0"" src="../../../images/menu2.gif" align=absbottom>
				<td width="94%"><b><%=AgregarSpan(ArrForo(0,j),ArrForo(3,j),"","E")%></b>&nbsp;</td>
			</tr>
				<%
				if ArrForo(4,j)>0 then
					response.write "<tr><td colspan=3 width=""100%""><table cellpadding=0 cellspacing=0 border=0 width=""100%"" " & estilotbl & " id=""tbl" & ArrForo(0,j) & """>"
					response.write CargarRpteMensajes(0,"",ArrForo(0,j))
					response.write "</td></tr></table>"
				end if
			next%>
		</table>
		<%end if
	end sub

	Public Function CargarRpteMensajes(ByVal RefIdforomensaje,ByVal prefix,ByVal iforo)
		Dim ArrMensaje, NodoIzquiero, preadd, Resultado, NodoAbierto,tipo,C
	  	Set Obj= Server.CreateObject("AulaVirtual.clsForo")
  			ArrMensaje=Obj.Consultar("3",iforo,RefIdforomensaje,"")
  		Set Obj= Nothing

		If IsEmpty(ArrMensaje)=false then
			preadd = ""
			Resultado = ""
	
			FOR C=Lbound(ArrMensaje,2) to Ubound(ArrMensaje,2)
				NodoAbierto = 0
				NodoIzquierdo=ArrMensaje(7,C) 'Número de Nodos
				NodoIzquiero = NodoIzquiero - 1
				Resultado=Resultado & "<tr><td>" & prefix

				if NodoIzquiero then
					Resultado = Resultado & "<img src='../../../images/beforenode.gif' align=absbottom>"
				else
					Resultado = Resultado & "<img src='../../../images/beforelastnode.gif' align=absbottom>"
				end if
		
				if ArrMensaje(7,C) > 0 then
					'Existen más respuestas
					NodoAbierto = instr(NumNodoAbierto,"[" & ArrMensaje(0,C) & "]")
					textorpta=ArrMensaje(3,C) & " (" & ArrMensaje(7,C) & " respuesta-s)"
					pagina="../foro/detallemensaje.asp?vistaprevia=S&idforomensaje=" & ArrMensaje(0,C)
					
					if NodoAbierto > 0 then
						Resultado = Resultado & enlaceNodo("A","rpteforo.asp",ArrMensaje(0,C),"idforo=" & iforo)
					else
						Resultado=Resultado & enlaceNodo("C","rpteforo.asp",ArrMensaje(0,C),"idforo=" & iforo)
					end if
				else
					Resultado=Resultado & "<img src='../../../images/nodeleaf.gif' align=absbottom>"
					textorpta=ArrMensaje(3,C)
					pagina="../foro/detallemensaje.asp?vistaprevia=S&idforomensaje=" & ArrMensaje(0,C)
				end if

				Resultado = Resultado & "&nbsp;<img border=""0"" src=""../../../images/rpta.gif"" align=absbottom ALT=""" & ArrMensaje(3,C) &  """>&nbsp;" & _
							AgregarSpan(ArrMensaje(0,C),textorpta,pagina,"E") & "</td></tr>" & chr(13) & chr(10)
			
				if NodoAbierto then
					if NodoIzquiero then
						preadd = "<img src='../../../images/beforechild.gif' align=absbottom>"
					else
						preadd = "<img src='../../../images/beforelastchild.gif' align=absbottom>"
					end if
					Resultado = Resultado & CargarRpteMensajes(ArrMensaje(0,C), prefix & preadd,iforo)
				end if
			NEXT
		end if

		CargarRpteMensajes= Resultado
	end function
	
	
	Public Sub CargarRpteTareas(ByVal icursovirtual)
		Dim ArrCarpeta
		Set Obj= Server.CreateObject("AulaVirtual.clsTarea")
  			ArrCarpeta=Obj.Consultar("10",icursovirtual,"","")
  		Set Obj= Nothing

		If IsEmpty(ArrCarpeta) then%>
			<tr><td class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han definido Encuestas en el curso virtual</td></tr>
		<%Else%>
		<table align="right" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%">
			<%for j=lbound(ArrCarpeta,2) to Ubound(ArrCarpeta,2)
				imagen="mas"%>
			<tr>
				<td width="3%">
                <img border="0" src="../../../images/<%=imagen%>.gif" id="img<%=ArrCarpeta(0,j)%>" onclick="MostrarTabla(tbl<%=ArrCarpeta(0,j)%>,'../../../images/',this)"></td>
				<td width="3%" align="left"><img border=""0"" src="../../../images/nota.gif" align=absbottom>
				<td width="94%"><%=AgregarSpan(ArrCarpeta(0,j),ArrCarpeta(1,j) & "( " & ArrCarpeta(3,j) & ")","","E")%>&nbsp;</td>
			</tr>
				<%
				call CargarTareaUsuario(ArrCarpeta(0,j),ArrCarpeta(1,j),ArrCarpeta(2,j),ArrCarpeta(3,j),ArrCarpeta(4,j))
			next%>
		</table>
		<%end if
	end sub

	Private function CargarTareaUsuario(ByVal idtarea,byval titulotarea,byval idtipotarea,byval descripciontipotarea,byval permitirreenvio)
		Dim ArrRpta,cadena
	  	Set Obj= Server.CreateObject("AulaVirtual.clsTarea")
  			ArrRpta=Obj.Consultar("11",idtarea,"","")
  		Set Obj= Nothing

		cadena="<tr style=""display:none"" id=tbl" & idtarea & "><td colspan=3 align=right><table width=""95%"">"	
		If IsEmpty(ArrRpta) then
			cadena=cadena & "<tr><td class=""usatsugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han registrado participantes que realizaron la tarea</td></tr>"
		else
			for i=lbound(ArrRpta,2) to Ubound(ArrRpta,2)
				if idtipotarea=2 then
					pagina=""
				elseif idtipotarea=3 then
						pagina="../documentos/descargar.asp?codigo_acceso=" & ArrRpta(3,i)
					elseif permitirreenvio=0 then
							pagina="../tareas/listatareausuario.asp?modo=V&idtarea=" & idtarea & "&titulotarea=" & titulotarea & "&idtipotarea=" & idtipotarea & "&descripciontipotarea=" & descripciontipotarea & "&permitirreenvio=" & permitirreenvio
						else
							pagina="../tareas/listaversiones.asp?modo=V&idtarea=" & idtarea & "&titulotarea=" & titulotarea & "&idtipotarea=" & idtipotarea & "&idusuario=" & replace(ArrRpta(1,i),"\","/")
				end if

				cadena=cadena & "<tr><td width=""3%"">" & (i+1) & "</td><td width=""97%"">" & AgregarSpan(ArrRpta(0,i),ArrRpta(2,i),pagina,"E") & "</td></tr>"
			next
		end if
		cadena=cadena & "</table></td></tr>"
		response.write(cadena)
	End function
	
End class
%>