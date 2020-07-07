<!--#include file="clsNotas.asp"-->
<%
Dim activarnotas
codigo_cac=request.querystring("codigo_cac")
codigo_cup=request.querystring("codigo_cup")
if codigo_cac="" then codigo_cac=0

identificador_cur=request.querystring("identificador_cur")
nombre_cur=request.querystring("nombre_cur")
grupohor_cur=request.querystring("grupohor_cur")
ciclo_cur=request.querystring("ciclo_cur")
codigo_per=request.querystring("codigo_per")
nombre_per=request.querystring("nombre_per")

Response.ContentType = "application/msword"
'Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=" & nombre_per & ".doc"

Set notas=new clsnotas
	with notas
		Set rsAlumnos=.ConsultarRegistroNotas(codigo_cup,"RM")
		Set rsCiclo=.ConsultarCicloAcademico("CO",codigo_cac)
		
		if rsCiclo.recordcount>0 then
			'Cargar datos del ciclo
			descripcion_cac=rsCiclo("descripcion_cac")
			tipo_Cac=rsCiclo("tipo_cac")
			notaminima_cac=rsCiclo("notaminima_cac")
		end if
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registro de notas</title>
<style>
<!--
.etiqueta    { font-weight: bold }
.sinborde    { border: 1px solid #FFFFFF }
td           { font-size: 9pt }
.rojo        { color: #FF0000 }
.azul        { color: #0000FF }
.etabla      { font-weight: bold; text-align: center; background-color: #FFFDD2 }
body         { font-family: Arial Narrow; font-size: 9pt }
-->
</style>
</head>
<body style="margin: 0px">
<table width="100%" border="0" cellpadding="0" cellspacing="0" bordercolor="#808080" style="border-collapse: collapse">
<THEAD>
<tr>
<td width="100%" colspan="5"><h3 align="center"><u>REGISTRO DE EVALUACIÓN <br><%=descripcion_cac%></u></h3>&nbsp;</td></tr>
  <tr>
    <td width="12%" class="etiqueta">Docente</td>
    <td width="88%" colspan="4">&nbsp;:&nbsp;<%=nombre_per%></td>
  </tr>
  <tr>
    <td width="12%" class="etiqueta">Asignatura</td>
    <td width="88%" colspan="4">&nbsp;<%=nombre_cur%></td>
  </tr>
  <tr class="etiqueta">
    <td width="12%">Código</td>
    <td width="20%">&nbsp;<%=identificador_cur%></td>
    <td width="15%">&nbsp;<%=grupohor_cur%></td>
    <td width="15%">&nbsp;<%=ciclo_cur%></td>
    <td width="30%" align="right">&nbsp;Fecha de Impresión:<%=now%></td>
  </tr>
  <tr>
    <td width="100%" colspan="5">&nbsp;</td>
  </tr>
  <tr>
    <td width="100%" colspan="5" height="20%">
    <table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#000000" width="100%" height="100%">
		<tr class="etabla">
		<td width="5%">Nº</td>
		<td width="20%">Escuela Profesional</td>
		<td width="11%">Código</td>
		<td width="40%">Estudiante</td>
		<td width="10%">Nota Final</td>
		<td width="15%">Condición</td>
		</tr>
	</table>
	</td>
  </tr>
  </td></tr>
</THEAD>
	<tr>
	<td width="100%" colspan="5" valign="top">
	<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="100%">
	<%
	aprobados=0:desaprobados=0:retirados=0
	Do while not rsAlumnos.eof
		i=i+1
		if rsAlumnos("estado_dma")="R" then 
			retirados=retirados+1
		elseif cdbl(rsAlumnos("notafinal_dma"))>cdbl(notaminima_cac) then
				aprobados=aprobados+1
			else
				desaprobados=desaprobados+1
		end if
	%>
		<tr>
		<td height="15" width="5%" align="center"><%=i%>&nbsp;</td>
		<td height="15" width="20%"><%=rsAlumnos("nombre_cpf")%></td>
		<td height="15" align="right" width="10%"><%=rsAlumnos("codigoUniver_Alu")%>&nbsp;</td>
		<td height="15" align="left" width="40%"><%=rsAlumnos("alumno")%>&nbsp;</td>
		<td height="15" align="center" width="10%"><%=iif(rsAlumnos("estado_dma")<>"R",rsAlumnos("notafinal_dma"),"")%>&nbsp;</td>
		<%=.condicionnota(i,rsAlumnos("condicion_dma"),rsAlumnos("estado_dma"))%>
		</tr>
		<%rsAlumnos.movenext
	Loop
	rsAlumnos.close
	set rsAlumnos=Nothing
	%>
	</table>
	</td>
	</tr>
</table>
<p align="left">
<b>&nbsp;&nbsp;<font color="#0000FF">Aprobados: <%=aprobados%></font>  | 
<font color="#FF0000">Desaprobados: <%=desaprobados%></b></p>
</font>
</body>
</html>
<%
	end with
Set notas=nothing
%>