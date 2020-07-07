<%
intcodigo_dac= request.querystring("id") 'session("codigo_Dac")
intcodigo_ctf= request.querystring("ctf") 'treyes

nameHoja="Lista de Titulo Profesional por Docente"

Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=" & nameHoja & ".xls"
Dim objTitulos,rsTitulos
	'if idcicloacademico<>0 then
	  	'Set objTitulos=Server.CreateObject("PryUSAT.clsDatTituloProfesional")
		'Set rsTitulos=Server.CreateObject("ADODB.Recordset")
		'Set rsTitulos= objTitulos.ConsultarTituloProfesional ("RS","TH",intcodigo_dac)
		
		Set objTitulos=Server.CreateObject("PryUSAT.clsAccesoDatos") 'treyes 09/01/2018
		objTitulos.AbrirConexion 'treyes 09/01/2018 
        Set rsTitulos=Server.CreateObject("ADODB.Recordset") 'treyes 09/01/2018
        Set rsTitulos=objTitulos.Consultar("ConsultarTituloProfesional_V2","FO",intcodigo_dac,intcodigo_ctf) 'treyes 09/01/2018
		
		NumReg=rsTitulos.recordcount
	  	Set objTitulos=Nothing
  	'end if
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
 <tr><td colspan="8" align="center"><font size="+1" face="Arial, Helvetica, sans-serif"><strong><%=session("Descripcion_Dac")%></strong></font></td>
<tr>
<td height="30" colspan="8" align="center"><font size="+1" face="Arial, Helvetica, sans-serif"><strong>Listado de Títulos profesional por docente</strong></font></td></tr>    
  </tr>
</table>

<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">

<tr class="etabla"> 
		  <td width="5%" rowspan="2">Item</td>
		  <td rowspan="2">Profesor</td>
		  <td rowspan="2">Título</td>
		  <td rowspan="2">Institución</td>
		  <td rowspan="2">Situación</td>
		  <td align="center" colspan="3">Años</td>
</tr>
<tr class="etabla">
		  <td>Ingreso</td>
		  <td>Egreso</td>
		  <td>Graduación</td>
</tr>
<%	Dim intItem
	intItem=0 %>
<%do while not rsTitulos.eof
		intItem=intItem+1%>
  <tr>
  	<%
    response.write "<td align=""center"" width=""1%"">"&intItem&"</td>"
	response.write "<td width=""80%"" align=""center"">"&trim(rsTitulos("docente"))&"</td>"
	response.write "<td width=""20%"" align=""center"">"&trim(rsTitulos("descripcion_Tpf"))&"</td>"
    response.write "<td width=""10%"" align=""center"">" & trim(rsTitulos("nombre_ins")) & "</td>"
    response.write "<td width=""20%"" align=""center"">" & trim(rsTitulos("descripcion_Sit")) & "</td>"
    response.write "<td width=""10%"" align=""center"">" & trim(rsTitulos("anioIngreso_Tpr")) & "</td>"
    response.write "<td width=""20%"" align=""center"">" & trim(rsTitulos("anioEgreso_Tpr")) & "</td>"
    response.write "<td width=""10%"" align=""center"">" & trim(rsTitulos("anioGrad_Tpr")) & "</td>"
    %>
  </tr>
  <%rsTitulos.movenext
  Loop%>
</table>
</body>
</html>