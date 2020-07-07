<%
codigo_cpf=request.querystring("codigo_cpf")
cicloingreso=request.querystring("cicloingreso")
codigo_cacini=request.querystring("codigo_cacini")
codigo_cacfin=request.querystring("codigo_cacfin")
incluir=request.querystring("incluir")
nombre_cpf=request.querystring("nombre_cpf")
descripcion_cacfin=request.querystring("descripcion_cacfin")



if codigo_cpf<>"" then
	Response.ContentType = "application/vnd.ms-excel"
	Response.AddHeader "Content-Disposition","attachment;filename=cuadromeritos" & codigo_cpf & ".xls"

	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsAlumnos=Obj.Consultar("ConsultarPonderado_Tercio","FO","TO",codigo_cpf,cicloIngreso,codigo_cacini,codigo_cacfin)
		Obj.CerrarConexion
	Set Obj=nothing
	
	
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Exportar Cuadro de Méritos</title>
</head>
<body>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%" id="cuadromeritos">
  <tr class="usatceldatitulo">
    <td width="105%" align="center" colspan="9"><b>
    <font size="5" color="#0000FF">Reporte de Cuadro de Méritos</font></b></td>
  </tr>
  <tr class="usatceldatitulo">
    <td width="100%" colspan="9">
    <b>Escuela Profesional</b>:&nbsp;<%=nombre_cpf%>
    <br><b>Semestre de Ingreso</b>:&nbsp;<%=cicloingreso%>
    <br><b>Semestre Académico Fin</b>:&nbsp;<%=descripcion_cacfin%><br>
    </td>
  </tr>
  <tr class="usatceldatitulo">
    <td width="5%" align="center" bgcolor="#CCFFCC"><b>Orden</b></td>
    <td width="15%" align="center" bgcolor="#CCFFCC"><b>Código Universitario</b></td>
    <td width="60%" align="center" bgcolor="#CCFFCC"><b>Alumno</b></td>
    <td width="5%" align="center" bgcolor="#CCFFCC"><b>Créditos Matriculados</b></td>
    <td width="5%" align="center" bgcolor="#CCFFCC"><b>Créditos Aprobados</b></td>
    <td width="5%" align="center" bgcolor="#CCFFCC"><b>Créditos Desaprobadas</b></td>
    <td width="5%" align="center" bgcolor="#CCFFCC"><b>Asignaturas Desaprobadas</b></td>    
    <td width="5%" align="center" bgcolor="#CCFFCC"><b>Promedio Ponderado</b></td>
    <td width="5%" align="center" bgcolor="#CCFFCC"><b>Estado</b></td>
  </tr>
 <%
  	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
	i=0
	
	Do While Not rsAlumnos.EOF
		estado="No Apto"
		clase="rojo"
  		NroCredDesaprobados=rsAlumnos("NroCredMatriculados")-rsAlumnos("NroCredAprobados")

		Set rsMerito=Obj.Consultar("DeterminarInclusionEnTercio","FO",rsAlumnos("codigo_alu"),cicloIngreso,codigo_cacfin)
		if int(rsMerito(0))=1 then
			estado="Apto"
			clase="azul"
		end if
		
		'response.write "codigo_alu=" & rsAlumnos("codigo_alu") & "=" & int(rsMerito(0)) & "<br>"
		i=i+1
  %>
  <tr>
  	<%
    response.write "<td width='5%'>" & i & "</td>"
    response.write "<td width='15%'>" & rsAlumnos("codigouniver_alu") & "&nbsp;</td>"
    response.write "<td width='60%'>" & rsAlumnos("alumno") & "</td>"
    response.write "<td width='5%'>" & rsAlumnos("NroCredMatriculados") & "</td>"
    response.write "<td width='5%'>" & rsAlumnos("NroCredAprobados")& "</td>"
    response.write "<td width='5%'>" & NroCredDesaprobados & "</td>"
    response.write "<td width='5%'>" & rsAlumnos("NroAsigDesaprobadas")& "</td>"    
    response.write "<td width='5%'>" & FormatNumber(rsAlumnos("Ponderado"),4) & "</td>"
    response.write "<td width='5%'>" & estado & "</td>"
    %>
  </tr>
  <%	end if
  
	  rsAlumnos.movenext
  Loop
  	'if incluir="1" then
		Obj.CerrarConexion
		Set Obj=nothing
		Set rsMerito=nothing
	'end if
  	Set rsAlumnos=nothing
  %>
</table>
</body>
</html>
<%end if%>