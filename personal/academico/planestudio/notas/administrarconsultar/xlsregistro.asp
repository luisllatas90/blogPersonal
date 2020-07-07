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
nombre_cur=replace(nombre_cur,"(Principal)","")

Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=Registro_" & nombre_cur & ".xls"

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
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registro Auxiliar</title>
</head>
<body>
<table width="100%" border="0" cellpadding="3" cellspacing="0" 
        style="border-collapse: collapse">
<tr>
<td width="100%" colspan="4" style="border-style: none"><b>REGISTRO DE EVALUACIÓN</b></td></tr>
  <tr>
    <td width="100%" colspan="4" style="width: 98%">Asignatura: <%=nombre_cur%> (Grupo <%=grupohor_cur%>)</tr>
  <tr>
    <td width="100%" colspan="4">&nbsp;</td>
  </tr>
  <tr style="background-color: #FFFFCC; color: #800000; font-weight: bold;">
    <th width="3%" style="border: 1px solid #000000">Nro</th>
    <th width="10%" style="border: 1px solid #000000">Escuela Profesional</th>
    <th width="8%" style="border: 1px solid #000000">Código</th>
    <th width="40%" style="border: 1px solid #000000">Estudiante</th>
  </tr>
  <%
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
    <td width="3%" style="border: 1px solid #000000"><%=i%></td>
    <td width="10%" style="border: 1px solid #000000"><%=rsAlumnos("nombre_cpf")%></td>
    <td width="8%" style="border: 1px solid #000000">&nbsp;<%=rsAlumnos("codigoUniver_Alu")%></td>
    <td width="40%" style="border: 1px solid #000000"><%=rsAlumnos("alumno")%></td>
  </tr>
        <%rsAlumnos.movenext
	Loop
	rsAlumnos.close
	set rsAlumnos=Nothing
	%>
</table>
</body>
</html>
<%
	end with
Set notas=nothing
%>