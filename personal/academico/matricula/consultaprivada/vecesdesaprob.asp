<%
Dim idescuela,idcicloacademico

idescuela=request.querystring("idescuela")
idcicloacademico=request.querystring("idcicloacademico")
descripcion_cpf=request.querystring("descripcion_cpf")
descripcion_cac=request.querystring("descripcion_cac")

if idescuela="" then idescuela=0
if idcicloacademico="" then idcicloacademico=0

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
<script>
	function EnviarDatos(pagina)
	{
		var ncpf=cboescuela.options[cboescuela.selectedIndex].text
		var ncac=cbocicloacademico.options[cbocicloacademico.selectedIndex].text
		
		location.href=pagina + "?idescuela=" + cboescuela.value + "&idcicloacademico=" + cbocicloacademico.value + "&descripcion_cpf=" + ncpf + "&descripcion_cac=" + ncac
	}
</script>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
</head>

<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
  <tr>
    <td width="30%">Escuela Profesional</td>
    <td width="70%">
    <select id="cboescuela" style="width: 100%">
	  	<option value="0">TODAS LAS ESCUELAS PROFESIONALES</option>
				   <%do while not rsCarrProf.eof%>
       				 <option value="<%=rsCarrProf("codigo_cpf") %>" <%if cint(idescuela)=cint(rscarrProf("codigo_cpf")) then response.write "SELECTED"%>>
					<%=rsCarrProf("nombre_Cpf")%>
					</option>
					<% rsCarrProf.movenext
					loop
					%>
        </select></td>
  </tr>
  <tr>
    <td width="30%">Ciclo Académico</td>
    <td width="70%">
    <select id="cbocicloacademico" style="width: 100%">
	<option value ="0">---Seleccione Ciclo Academico---</option>
	<% do while not rsCiclo.eof%>
        <option value="<%=rsCiclo("codigo_cac")%>" <%if cint(idcicloacademico)=cint(rsCiclo("codigo_cac")) then response.write "SELECTED"%>><%=rsCiclo("Descripcion_Cac")%></option>
	<%rsCiclo.movenext
	loop
	%>
      </select></td>
  </tr>
  <tr>
    <td width="30%">&nbsp;</td>
    <td width="70%">
    <input type="button" value="Buscar" name="cmdBuscar" id="cmdbuscar" class="usatbuscar" onClick="EnviarDatos('consultarvecesdesaprobadas.asp')">
    <%if rsMatricula.recordcount>0 then%><input type="button" value="Exportar" name="cmdExportar" id="cmdexportar" class="boton" onClick="EnviarDatos('xlsvecesdesaprob.asp')"><%end if%></td>
  </tr>
</table>
<%if rsMatricula.recordcount>0 then%>
<h5>Lista de Estudiantes Matriculados (<%=rsMatricula.recordcount%>)</h5>
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
    <td width="20%"><%=rsMatricula("nombre_cpf")%>&nbsp;</td>
    <td width="10%"><%=rsMatricula("codigoUniver_alu")%>&nbsp;</td>
    <td width="30%"><%=rsMatricula("alumno")%>&nbsp;</td>
    <td width="5%" align="center"><%=rsMatricula("creditototal")%>&nbsp;</td>
    <td width="5%" align="center"><%=rsMatricula("segunda")%>&nbsp;</td>
    <td width="5%" align="center"><%=rsMatricula("tercera")%>&nbsp;</td>
    <td width="5%" align="center"><%=rsMatricula("cuarta")%>&nbsp;</td>
    <td width="5%" align="center"><%=rsMatricula("quinta_mas")%>&nbsp;</td>
  </tr>
  <%rsMatricula.movenext
  Loop%>
</table>
<%end if%>
</body>
</html>