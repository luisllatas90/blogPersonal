<!--#include file="../NoCache.asp"-->
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Pagina nueva 1</title>
</head>

<body topmargin="0" leftmargin="0">
<%
on error resume next
Codigo_Cup=request.querystring("codigo_cup")

		Dim rsHorario,Cadena,Filas,inicio,fin
		Dim totalHrs
			Set Obj= Server.CreateObject("PryUSAT.clsDatHorario")
				Set rsHorario=Obj.ConsultarHorarios("RS","4",Codigo_Cup,0,0)
			Set Obj=nothing
		
		EventoFila=""
		ControlImg=""
	
		If Not(rsHorario.BOF AND rsHorario.EOF) then
			EventoFila="onclick=""MostrarTabla(tblHorario" & Codigo_Cup & ",img" & Codigo_Cup & ",'../images')"" "
			ControlImg="<img id=""img" & Codigo_Cup & """ border=""0"" src=""../images/mas.gif"">"
			
			Cadena="<tr><td width=""100%"" colspan=""10"">"
			Cadena=Cadena & "<table id=""tblHorario" & Codigo_Cup & """ align=""center"" cellpadding=""2"" cellspacing=""0"" style=""display: none; border-collapse: collapse; border: 1px solid #808080"" bordercolor=""#111111"" width=""95%"">"
			Cadena=Cadena & "<tr class=""etabla"">"
			Cadena=Cadena & "<td width=""10%"">Día</td>"
			Cadena=Cadena & "<td width=""15%"">Inicio - Fin</td>"
			Cadena=Cadena & "<td width=""20%"">Ambiente</td>"
			Cadena=Cadena & "<td width=""35%"">Docente</td>"
			Cadena=Cadena & "<td width=""5%"">Tipo</td>"
			Cadena=Cadena & "<td width=""20%"">Observaciones</td>"
			Cadena=Cadena & "</tr>"
  			Do while Not rsHorario.EOF
  				inicio=Extraercaracter(1,2,rsHorario("nombre_hor"))
  				fin=Extraercaracter(1,2,rsHorario("horafin_Lho"))
  				if (IsNull(rsHorario("fechainicio_cup"))=false and IsNull(rsHorario("fechafin_cup"))=false) then
  					obs="Inicio: " & rsHorario("fechainicio_cup") & " Fin " & rsHorario("fechafin_cup")
  				end if
  				
				Filas=Filas & "<tr>"
				Filas=Filas & "<td width=""10%"">" & ConvDia(rsHorario("dia_Lho")) & ""
				Filas=Filas & "<input type=""hidden"" name=""txthorario" & Codigo_Cup & """ value=""" & rsHorario("dia_Lho") & inicio & fin & """>"
				Filas=Filas & "<input type=""hidden"" name=""txtambiente" & Codigo_Cup & """ value=""" & rsHorario("ambiente") & """>"
				Filas=Filas & "</td>"
				Filas=Filas & "<td width=""15%"">" & rsHorario("nombre_hor") & "-" & rsHorario("horafin_Lho") & "</td>"
				Filas=Filas & "<td width=""20%"">" & ConvertirTitulo(rsHorario("ambiente")) & "</td>"
				Filas=Filas & "<td width=""35%"">" & ConvertirTitulo(rsHorario("docente")) & "</td>"
				Filas=Filas & "<td width=""5%"">" & rsHorario("tipohoracur_lho") & "</td>"
				Filas=Filas & "<td width=""20%"">" & obs & "</td>"
				Filas=Filas & "</tr>"
				rsHorario.movenext
	  		Loop
	  		Cadena=Cadena & Filas
			Cadena=Cadena & "</table>"
			Cadena=Cadena & "</td></tr>"
			HorarioCursoProgramado=Cadena
		End If
		Set horario=nothing
		Set obj=nothing
	End Function
	
	If Err.Number<>0 then
    session("pagerror")="estudiante/tblhorario.asp"
    session("nroerror")=err.number
    session("descripcionerror")=err.description    
	response.write("<script>top.location.href='../error.asp'</script>")
End If
%>
</body>

</html>