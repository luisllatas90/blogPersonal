<%
'*****************************
intcodigo_dac=session("codigo_Dac")
'*****************************
'request.querystring("idDepartamento")
nameHoja="Lista de Investigaciones por Docente"
'idcicloacademico=request.querystring("idcicloacademico")
'descripcion_cpf=request.querystring("descripcion_cpf")
'descripcion_cac=request.querystring("descripcion_cac")

'if idescuela="" then idescuela="TO"
'if idcicloacademico="" then idcicloacademico=0

Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=" & nameHoja & ".xls"
Dim objPublicacion,rsPublicacion
	'if idcicloacademico<>0 then
	  Set objInvestigacion=Server.CreateObject("PryUSAT.clsDatInvestigacion")
		Set rsInvestigacion=Server.CreateObject("ADODB.Recordset")
		Set rsInvestigacion= objInvestigacion.ConsultarInvestigacion ("RS","DE",intcodigo_dac)
		NumReg=rsInvestigacion.recordcount
	  '	Set objPublicacion=Nothing
  	'end if
	
Dim objInvPub, rsInvPub
Set objInvPub=Server.CreateObject("PryUSAT.clsDatInvestigacion")
Set rsInvPub=Server.CreateObject("ADODB.Recordset")
Set rsInvPub= objInvPub.ConsultarInvestigacionPublicacion ("RS","TO","")
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
<td height="30" colspan="8" align="center"><font size="+1" face="Arial, Helvetica, sans-serif"><strong>Listado de Investigaciones por docente</strong></font></td></tr>    
  </tr>  
</table>

<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
<tr class="etabla"> 
		  <td>Item</td>
		  <td>Docente</td>
		  <td>Título</td>
		  <td>Tipo</td>
		  <td>Inicio</td>
		  <td>Término</td>
		  <td>Estado</td>
		  <td>Publicación.</td>
		  <td>Fecha Publicación</td>
		  <td>Medio de Publicación</td>
		  <td>Tipo de Publicación</td>
	    </tr>

<%Dim intItem
		intItem=0
		do while not rsInvestigacion.EOF 
		intItem=intItem+1%>
 <tr>
<td align="center" width="1%"><%=intItem%></td>
			<td width="80%"><%=rsInvestigacion("docente")%>&nbsp;</td>
			<td width="20%" align="center"><%=rsInvestigacion("Titulo_Inv")%></td>
			<td width="10%" align="center"><%=rsInvestigacion("descripcion_tin")%></td>
			<td width="20%" align="center"><%=rsInvestigacion("fechaini_inv")%></td>
			<td width="10%" align="center"><%=rsInvestigacion("fechafin_inv")%></td>
			<td width="20%" align="center"><%=rsInvestigacion("descripcion_ein")%></td>
			<%rsInvPub.movefirst
			do while not rsInvPub.eof
				Dim mostrar
				if rsInvestigacion("codigo_Inv")=rsInvPub("codigo_Inv") then
					mostrar="SI"
				else
					mostrar="NO"
				end if
				'response.Write("<td>inv.codigo= "&rsInvestigacion("codigo_Inv")&" invpub.codigo= "&rsInvPub("codigo_Inv")&"</td>")
			rsInvPub.movenext
			loop%>
			<td width="10%" align="center"><%=mostrar%></td>
			<td width="10%" align="center"><%=rsInvestigacion("fecha_pub")%></td>
			<td width="10%" align="center"><%=rsInvestigacion("descripcion_mpu")%></td>
			<td width="10%" align="center"><%=rsInvestigacion("descripcion_tpu")%></td>
	</tr>
	<% rsInvestigacion.movenext
  Loop%>
</table>
</body>
</html>

