<%
Dim Mimes 'Month of calendar
Dim Mianio 'Year of calendar
Dim PrimerDia 'First day of the month. 1 = Monday
Dim diaactual 'Used to print dates in calendar
Dim Col 'Calendar column
Dim Row 'Calendar row

Mimes = Request.Querystring("Mes")
Mianio = Request.Querystring("Anio")

If IsEmpty(Mimes) then Mimes = Month(Date)
if IsEmpty(Mianio) then Mianio = Year(Date)

Public Sub mostrarcalendario(ruta,pagina,idcursovirtual,idusuario)

	call MostrarEncabezado (ruta,Mimes, Mianio)

	PrimerDia = WeekDay(DateSerial(Mianio, Mimes, 1)) -1
	diaactual = 1

	'Crear el objeto de búsqueda
	Set Obj= Server.CreateObject("AulaVirtual.clsAgenda")
	'Construir calendario
	For Row = 0 to 5
		For Col = 0 to 6
			If Row = 0 and Col < PrimerDia then
				response.write "<td>&nbsp;</td>"
			elseif diaactual > UltimoDia(Mimes, Mianio) then
				response.write "<td>&nbsp;</td>"
			else
				fechaactual=diaactual & "/" & Mimes & "/" & Mianio
	   			response.write "<td onMouseOver=Resaltar(1,this,'S') onMouseOut=Resaltar(0,this,'S') onClick=""ResaltarCelda(this);" & pagina & "?modo=2&mes=" & Mimes & "&Anio=" & Anio & "&fecha=" & Cdate(fechaactual) & "'"""
	   			response.write(Obj.Pintar(fechaactual,idcursovirtual,idusuario))
	   			
				if Mimes = Month(Date) and diaactual = Day(Date) then 
					response.write " class='encabezadopregunta' align='center'>"
				else
					response.write " align='center'>"
				end if
				
				response.write diaactual & "</td>"
				diaactual = diaactual + 1
			End If
		Next
		response.write "</tr>"
	Next
	response.write "</table>"
	'Borrar el objeto
	Set Obj=nothing
end sub

Private Sub MostrarEncabezado(ruta,Mimes,Mianio)%>
<table align="center" class="contornotabla" border="0" cellspacing="0" cellpadding="2" width="100%" id="tblcalendario">
	<tr class="etabla2"> 
	<td><a target="_self" href="<%=Request.serverVariables("SCRIPT_NAME")%>?<%=AtrazarMes(Mimes,Mianio)%>"><img border="0" src="<%=ruta%>/izq.gif"></a>&nbsp;</td>
	<td colspan="5"><%=MonthName(Mimes) & " " & Mianio%>&nbsp;</td>
	<td><a target="_self" href="<%=Request.serverVariables("SCRIPT_NAME")%>?<%=AdelantarMes(Mimes,Mianio)%>"><img border="0" src="<%=ruta%>/der.gif"></a>&nbsp;</td>
</tr>
<tr class="etabla">
	<td>D</td><td>L</td><td>M</td><td>M</td><td>J</td><td>V</td><td>S</td>
</tr>
<%End Sub

Private Function UltimoDia(Mimes, Mianio)
' Returns the last day of the month. Takes into account leap years
' Usage: UltimoDia(Month, Year)
' Example: UltimoDia(12,2000) or UltimoDia(12) or UltimoDia
	Select Case Mimes
		Case 1, 3, 5, 7, 8, 10, 12
			UltimoDia = 31
		Case 4, 6, 9, 11
			UltimoDia = 30
		Case 2
			If IsDate(Mianio & "-" & Mimes & "-" & "29") Then UltimoDia = 29 Else UltimoDia = 28
		Case Else
			UltimoDia = 0
	End Select
End Function

Private function AtrazarMes(ByVal Mes,ByVal Ano)
	If Mes-1=0 then
		AtrazarMes="Modo=1&Mes=12&Anio=" & Ano -1
	Else
		AtrazarMes="Modo=1&Mes=" & Mes - 1 & "&Anio=" & Ano
	End if
End function

Private function AdelantarMes(ByVal Mes,ByVal Ano)
	If Mes+1=13 then
		AdelantarMes="Modo=1&Mes=1&Anio=" & Ano +1
	Else
		AdelantarMes="Modo=1&Mes=" & Mes + 1 & "&Anio=" & Ano
	End if
End function
%>