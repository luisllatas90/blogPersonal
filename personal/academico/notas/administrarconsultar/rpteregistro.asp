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

Public function NotaAlumno(byval fila,byval cond,Byval estado)
	dim mensaje,clase
	if estado="R" then
		mensaje="Retirado"
		clase="class=""retirado2"""
	elseif cond="A" then
		mensaje="Aprobado"
		clase="class=""aprobado2"""
		else
			mensaje="Desaprobado"
			clase="class=""desaprobado2"""		
	end if
	
	NotaAlumno="<td " & clase & " id=""msgcondicion_dma" & fila & """ width=""15%"">" & mensaje & "</td>"
End function


Public function NotaAlumnoInhabilitado(byval fila,byval cond,Byval estado)
	dim mensaje,clase
    mensaje="Inhabilitado"
	clase="class=""retirado2"""	
	NotaAlumnoInhabilitado="<td " & clase & " id=""msgcondicion_dma" & fila & """ width=""15%"">" & mensaje & "</td>"
End function

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
.conborde {
	border: 1px solid #000000;
}
.retirado2 {
	color:green;
	border: 1px solid #000000;
}
.aprobado2 {
	color:blue;
	border: 1px solid #000000;
}
.desaprobado2 {
	color:red;
	border: 1px solid #000000;
}
-->
</style>
</head>
<body topmargin="0" leftmargin="0">
<table width="100%" border="0" cellpadding="3" cellspacing="0" bordercolor="#808080" style="border-collapse: collapse">
<THEAD>
<tr>
<td width="100%" colspan="6"><h3 align="center"><u>ACTAS DE EVALUACIÓN <br><%=descripcion_cac%></u></h3>&nbsp;</td></tr>
  <tr>
    <td width="12%" class="etiqueta">Docente</td>
    <td width="88%" colspan="5">&nbsp;:&nbsp;<%=nombre_per%></td>
  </tr>
  <tr>
    <td width="12%" class="etiqueta">Asignatura</td>
    <td width="88%" colspan="5">&nbsp;:&nbsp;<%=nombre_cur%></td>
  </tr>
  <tr class="etiqueta">
    <td width="12%">Código</td>
    <td width="88%" colspan="5">&nbsp;:&nbsp;<%=identificador_cur%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Grupo: <%=grupohor_cur%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ciclo: <%=ciclo_cur%></td>
  </tr>
  <tr><td width="100%" align="right" colspan="6"><b>&nbsp;Fecha de Impresión:<%=now%></b></td></tr>
  <tr class="etabla">
		<td width="5%" class="conborde">Nº</td>
		<td width="20%" class="conborde">Escuela Profesional</td>
		<td width="11%" class="conborde">Código</td>
		<td width="40%" class="conborde">Estudiante</td>
		<td width="10%" class="conborde">Nota Final</td>
		<td width="15%" class="conborde">Condición</td>
 </tr>
</THEAD>
	<%
	aprobados=0:desaprobados=0:retirados=0:inhabilitados=0
	Do while not rsAlumnos.eof
		i=i+1
		if rsAlumnos("estado_dma")="R" then 
			retirados=retirados+1
		elseif cdbl(rsAlumnos("notafinal_dma"))>cdbl(notaminima_cac) then
				aprobados=aprobados+1
			elseif  rsAlumnos("inhabilitado_dma") = true then
			    inhabilitados= inhabilitados+1
			else
				desaprobados=desaprobados+1
		end if
	%>
		<tr>
		<td height="15" width="5%" align="center" class="conborde"><%=i%></td>
		<td height="15" width="20%" class="conborde"><%=rsAlumnos("nombre_cpf")%></td>
		<td height="15" align="right" width="10%" class="conborde"><%=rsAlumnos("codigoUniver_Alu")%></td>
		<td height="15" align="left" width="40%" class="conborde"><%=rsAlumnos("alumno")%></td>
		<td height="15" align="center" width="10%" class="conborde"><%=iif(rsAlumnos("estado_dma")<>"R",rsAlumnos("notafinal_dma"),"")%></td>
		<%		

        if rsAlumnos("inhabilitado_dma") = true then  
            response.Write (NotaAlumnoInhabilitado(i,rsAlumnos("condicion_dma"),rsAlumnos("estado_dma")))	        
        else 
	        response.Write(NotaAlumno(i,rsAlumnos("condicion_dma"),rsAlumnos("estado_dma")))
        end if
	        
		%>
		</tr>
		<%rsAlumnos.movenext
	Loop
	rsAlumnos.close
	set rsAlumnos=Nothing
	%>
</table>
<p align="left"><b>
&nbsp;&nbsp;<font color="#0000FF">Aprobados: <%=aprobados%></font>  | 
<font color="#FF0000">Desaprobados: <%=desaprobados%></font>
<font color="#04B431">Inhabilitados: <%=inhabilitados%></font>
</b></p>
</body>
</html>
<%
	end with
Set notas=nothing
%>