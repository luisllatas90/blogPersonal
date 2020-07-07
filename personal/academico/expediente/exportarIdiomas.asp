<%
'*****************************
intcodigo_dac= request.querystring("id") 'session("codigo_Dac")
'*****************************
'request.querystring("idDepartamento")
nameHoja="Lista de Idiomas por Docente"
'idcicloacademico=request.querystring("idcicloacademico")
'descripcion_cpf=request.querystring("descripcion_cpf")
'descripcion_cac=request.querystring("descripcion_cac")

'if idescuela="" then idescuela="TO"
'if idcicloacademico="" then idcicloacademico=0

Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=" & nameHoja & ".xls"
Dim objIdiomas,rsIdiomas
	'if idcicloacademico<>0 then
	  	Set objIdiomas=Server.CreateObject("PryUSAT.clsDatIdiomas")
		Set rsIdiomas=Server.CreateObject("ADODB.Recordset")
		Set rsIdiomas= objIdiomas.ConsultarIdiomas ("RS","TH",intcodigo_dac)
			NumReg=rsIdiomas.recordcount
	  	Set objIdiomas=Nothing
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
<td height="30" colspan="8" align="center"><font size="+1" face="Arial, Helvetica, sans-serif"><strong>Listado de Idiomas por docente</strong></font></td></tr>    
  </tr>
</table>

<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">

<tr class="etabla"> 
		  <td rowspan="3">Item</td>
		  <td rowspan="3">Docente</td>
		  <td rowspan="3">Idioma</td>
		  <td rowspan="3">Centro de estudios</td>
		  <td rowspan="3">Año de graduación</td>
		  <td align="center" colspan="9">Nivel de conocimiento del idioma</td>
	    </tr>
		<tr class="etabla">
			<td align="center" colspan="3">Lee</td>
			<td align="center" colspan="3">Habla</td>
			<td align="center" colspan="3">Escribe</td>
		</tr>
		<tr class="etabla">
			<td align="center">Alto</td>
			<td align="center">Medio</td>
			<td align="center">Bajo</td>
			<td align="center">Alto</td>
			<td align="center">Medio</td>
			<td align="center">Bajo</td>
			<td align="center">Alto</td>
			<td align="center">Medio</td>
			<td align="center">Bajo</td>
		</tr>

<%	Dim intItem
	intItem=0 %>
<%do while not rsIdiomas.eof
		intItem=intItem+1%>
 
 
 <tr class="Nivel0"> 
			<td align="center" width="1%"><%=intItem%></td>
			<td width="45%"><%=rsIdiomas("docente")%></td>
			<td width="25%"><%=rsIdiomas("descripcion_Idi")%></td>
			<%if rsIdiomas("codigo_Ins")=1 then%>
				<td width="35%" align="center"><%=rsIdiomas("centroestudios")%></td>
			<%else%>
				<td width="35%" align="center"><%=rsIdiomas("nombre_Ins")%></td>
			<%end if%>
			<td width="10%" align="center"><%=rsIdiomas("aniograduacion")%></td>
			<%select case rsIdiomas("lee")
				case "0"%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center">X</td>
				<%case "1"%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center">X</td>
					<td width="10%" align="center"></td>
				<%case "2"%>
					<td width="10%" align="center">X</td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
				<%case else%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
			<%end select
			select case rsIdiomas("habla")
				case "0"%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center">X</td>
				<%case "1"%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center">X</td>
					<td width="10%" align="center"></td>
				<%case "2"%>
					<td width="10%" align="center">X</td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
					<%case else%>
				<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
			<%end select
			select case rsIdiomas("escribe")
				case "0"%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center">X</td>
				<%case "1"%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center">X</td>
					<td width="10%" align="center"></td>
				<%case "2"%>
					<td width="10%" align="center">X</td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
				<%case else%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
			<%end select%>

		</tr>
 
 
<%
    'response.write "<td align=""center"" width=""1%"">"&intItem&"</td>"
	'response.write "<td width=""55%"" align=""center"">"&trim(rsIdiomas("docente"))&"</td>"
	'response.write "<td width=""55%"" align=""center"">"&trim(rsIdiomas("titulo_Pub"))&"</td>"
    'response.write "<td width=""25%"" align=""center"">" & trim(rsIdiomas("descripcion_Aco")) & "</td>"
    'response.write "<td width=""10%"" align=""center"">" & trim(rsIdiomas("descripcion_Tpu")) & "</td>"
    'response.write "<td width=""10%"" align=""center"">" & trim(rsIdiomas("fecha_Pub")) & "</td>"
    'response.write "<td width=""10%"" align=""center"">" & trim(rsIdiomas("descripcion_Mpu")) & "</td>"
    'response.write "<td width=""10%"" align=""center"">" & trim(rsIdiomas("observaciones_Pub")) & "</td>"
    %>
 


  	
  <%rsIdiomas.movenext
  Loop%>
</table>
</body>
</html>
