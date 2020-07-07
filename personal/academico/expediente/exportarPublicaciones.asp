<%
'*****************************
intcodigo_dac=session("codigo_Dac")
'*****************************
'request.querystring("idDepartamento")
nameHoja="Lista de Publicaciones por Docente"
'idcicloacademico=request.querystring("idcicloacademico")
'descripcion_cpf=request.querystring("descripcion_cpf")
'descripcion_cac=request.querystring("descripcion_cac")

'if idescuela="" then idescuela="TO"
'if idcicloacademico="" then idcicloacademico=0

Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=" & nameHoja & ".xls"
Dim objPublicacion,rsPublicacion
	'if idcicloacademico<>0 then
	  Set objPublicacion=Server.CreateObject("PryUSAT.clsDatPublicacion")
		Set rsPublicacion=Server.CreateObject("ADODB.Recordset")
		Set rsPublicacion= objPublicacion.ConsultarPublicacion ("RS","DE",intcodigo_dac)
			NumReg=rsPublicacion.recordcount
	  	Set objPublicacion=Nothing
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
<td height="30" colspan="8" align="center"><font size="+1" face="Arial, Helvetica, sans-serif"><strong>Listado de Publicaciones por docente</strong></font></td></tr>    
  </tr>  
</table>

<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
<tr class="etabla"> 
		  <td>Item</td>
		  <td>Docente</td>
		  <td>Título</td>
		  <td>Area de conocimiento</td>
		  <td>Tipo de publicación</td>
		  <td>Fecha de publicación</td>
		  <td>Medio de publicación</td>
		  <td>Observaciones</td>
	    </tr>

<%	Dim intItem
	intItem=0 %>
<%do while not rsPublicacion.eof
		intItem=intItem+1%>
 <tr>
<%
    response.write "<td align=""center"" width=""1%"">"&intItem&"</td>"
	response.write "<td width=""55%"" align=""center"">"&trim(rsPublicacion("docente"))&"</td>"
	response.write "<td width=""55%"" align=""center"">"&trim(rsPublicacion("titulo_Pub"))&"</td>"
    response.write "<td width=""25%"" align=""center"">" & trim(rsPublicacion("descripcion_Aco")) & "</td>"
    response.write "<td width=""10%"" align=""center"">" & trim(rsPublicacion("descripcion_Tpu")) & "</td>"
    response.write "<td width=""10%"" align=""center"">" & trim(rsPublicacion("fecha_Pub")) & "</td>"
    response.write "<td width=""10%"" align=""center"">" & trim(rsPublicacion("descripcion_Mpu")) & "</td>"
    response.write "<td width=""10%"" align=""center"">" & trim(rsPublicacion("observaciones_Pub")) & "</td>"
    %>
  </tr>
 


  	
  <%rsPublicacion.movenext
  Loop%>
</table>
</body>
</html>
