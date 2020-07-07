<!--#include file="clshorarios.asp"-->
<%
codigo_cpf=request.querystring("codigo_cpf")
codigo_cac=request.querystring("codigo_cac")
ciclo_cur=request.querystring("ciclo_cur")
grupohor_cup=request.querystring("grupohor_cup")
if codigo_cpf="" then codigo_cpf="-2"
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
</head>
<body>
<%
	Set obj= Server.CreateObject("PryUSAT.clsDatCurso")
		Set rsCursos=obj.ConsultarCursoProgramado("RS","9",codigo_cpf,codigo_cac,ciclo_cur,grupohor_cup)
	Set obj=nothing

	if Not(rsCursos.BOF and rsCursos.EOF) then
		response.write(CabezeraTabla)
		Set obj= Server.CreateObject("PryUSAT.clsDathorario")
		response.write vstCursoCiclo(rsCursos)
		'response.write obj.vstCursoCiclo(rsCursos)
		Set Obj=nothing
		response.write(PieTabla)
	else
		response.write "<h3>No se han programado cursos para el semestre académico actual</h3>"
	end if
	
	Set rsCursos=nothing
%>
</body>
</html>