<%
idescuela=request.querystring("idescuela")
idcicloacademico=request.querystring("idcicloacademico")
descripcion_cpf=request.querystring("descripcion_cpf")
descripcion_cac=request.querystring("descripcion_cac")

if idescuela="" then idescuela="TO"
if idcicloacademico="" then idcicloacademico=0

Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=" & descripcion_cpf & ".xls"

Dim Obj,rsMatricula,rsCarrProf,rsCiclo
	'if idcicloacademico<>0 then
	  	Set Obj= Server.CreateObject("PryUSAT.clsDatMatricula")
  			set rsMatricula=Server.CreateObject("ADODB.Recordset")
  			set rsMatricula=Obj.ConsultarVecesdesaprobadas("RS",idescuela,idcicloacademico)
	  	Set Obj=Nothing
  	'end if
  	
	set Obj=Server.CreateObject("PryUSAT.clsDatCarreraProfesional")
		set rsCarrProf=Server.CreateObject("ADODB.Recordset")
		set rsCarrProf=Obj.ConsultarCarreraProfesional("RS","TO","")
  	Set Obj=Nothing

	Set Obj=Server.CreateObject("PryUSAT.clsDatCicloAcademico")
		Set rsCiclo=Server.CreateObject("ADODB.Recordset")
		Set rsCiclo=Obj.ConsultarCicloAcademico ("RS","TO","")
	Set Obj=Nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Escuela</title>
<style>
<!--
.etabla      { font-weight: bold; text-align: center; background-color: #CCCCCC }
-->
</style>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
  <tr>
    <td width="30%"><b>Escuela Profesional</b></td>
    <td width="70%"><%=descripcion_cpf%></td>
  </tr>
  <tr>
    <td width="30%"><b>Ciclo Académico</b></td>
    <td width="70%"><%=descripcion_cac%></td>
  </tr>
</table>
<h4>Lista de Estudiantes Matriculados (<%=rsMatricula.recordcount%>)</h4>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr class="etabla">
    <td width="20%" rowspan="2">Escuela</td>
    <td width="10%" rowspan="2">Código</td>
    <td width="30%" rowspan="2">Apellidos y Nombres</td>
    <td width="25%" align="center" colspan="5">Créditos Matriculados</td>
  </tr>
  <tr class="etabla">
    <td width="5%" align="center">Total</td>
    <td width="5%" align="center">2da</td>
    <td width="5%" align="center">3era</td>
    <td width="5%" align="center">4ta</td>
    <td width="5%" align="center">5ta más</td>
  </tr>
  <%do while not rsMatricula.eof%>
  <tr>
  	<%
    response.write "<td width=""20%"">" & trim(rsMatricula("nombre_cpf")) & "</td>"
    response.write "<td width=""10%"">" & trim(rsMatricula("codigoUniver_alu")) & "</td>"
    response.write "<td width=""30%"">" & trim(rsMatricula("alumno")) & "</td>"
    response.write "<td width=""5%"" align=""center"">" & trim(rsMatricula("creditototal")) & "</td>"
    response.write "<td width=""5%"" align=""center"">" & trim(rsMatricula("segunda")) & "</td>"
    response.write "<td width=""5%"" align=""center"">" & trim(rsMatricula("tercera")) & "</td>"
    response.write "<td width=""5%"" align=""center"">" & trim(rsMatricula("cuarta")) & "</td>"
    response.write "<td width=""5%"" align=""center"">" & trim(rsMatricula("quinta_mas")) & "</td>"
    %>
  </tr>
  <%rsMatricula.movenext
  Loop%>
</table>
</body>
</html>