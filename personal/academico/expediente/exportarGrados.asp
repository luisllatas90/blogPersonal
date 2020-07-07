<%
intcodigo_dac= request.querystring("id") 'session("codigo_Dac")
intcodigo_ctf = request.querystring("ctf") 'treyes 09/01/2018

'request.querystring("idDepartamento")
nameHoja="Lista de Grados Académicos por Docente"
'idcicloacademico=request.querystring("idcicloacademico")
'descripcion_cpf=request.querystring("descripcion_cpf")
'descripcion_cac=request.querystring("descripcion_cac")

'if idescuela="" then idescuela="TO"
'if idcicloacademico="" then idcicloacademico=0

Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=" & nameHoja & ".xls"
Dim objGrados,rsGrados
	'if idcicloacademico<>0 then
	    'Set objGrados=Server.CreateObject("PryUSAT.clsDatGrados")
	    'Set rsGrados=Server.CreateObject("ADODB.Recordset")
	    'Set rsGrados= objGrados.ConsultarGradoAcademico ("RS","TH",intcodigo_dac)
	    
	    Set objGrados=Server.CreateObject("PryUSAT.clsAccesoDatos") 'treyes 09/01/2018
		objGrados.AbrirConexion 'treyes 09/01/2018 
        Set rsGrados=Server.CreateObject("ADODB.Recordset") 'treyes 09/01/2018
        Set rsGrados=objGrados.Consultar("ConsultarGradoAcademico_V2","FO",intcodigo_dac,intcodigo_ctf) 'treyes 09/01/2018
	    
			NumReg=rsGrados.recordcount
	  	Set objGrados=Nothing
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
<td height="30" colspan="8" align="center"><font size="+1" face="Arial, Helvetica, sans-serif"><strong>Listado de Grados académicosl por docente</strong></font></td></tr>    
  </tr>
</table>

<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
<tr class="etabla"> 
		  <td>Item</td>
		  <td>Profesor</td>
		  <td>Grado</td>
		  <td>TipoGrado</td>
		  <td>Mención</td>
		  <td>Situación</td>
		  <td>Institucion</td>
		  <td>Ing.</td>
		  <td>Egr.</td>
		  <td>Grad.</td>
	    </tr>

<!--  <tr class="etabla">
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
  </tr>-->
<%	Dim intItem
	intItem=0 %>
<%do while not rsGrados.eof
		intItem=intItem+1%>
  <tr>
  	<%
    response.write "<td align=""center"" width=""1%"">"&intItem&"</td>"
	response.write "<td width=""70%"" align=""center"">"&trim(rsGrados("docente"))&"</td>"
	response.write "<td width=""20%"" align=""center"">"&trim(rsGrados("nombre_Gra"))&"</td>"
    response.write "<td width=""10%"" align=""center"">" & trim(rsGrados("descripcion_TGr")) & "</td>"
    response.write "<td width=""20%"" align=""center"">" & trim(rsGrados("mencion_GPr")) & "</td>"
    response.write "<td width=""10%"" align=""center"">" & trim(rsGrados("descripcion_Sit")) & "</td>"
    response.write "<td width=""20%"" align=""center"">" & trim(rsGrados("nombre_Ins")) & "</td>"
    response.write "<td width=""10%"" align=""center"">" & trim(rsGrados("anioIngreso_GPr")) & "</td>"
    response.write "<td width=""10%"" align=""center"">" & trim(rsGrados("anioEgreso_GPr")) & "</td>"
    response.write "<td width=""10%"" align=""center"">" & trim(rsGrados("anioGrad_GPr")) & "</td>"
    %>
  </tr>
  <%rsGrados.movenext
  Loop%>
</table>
</body>
</html>