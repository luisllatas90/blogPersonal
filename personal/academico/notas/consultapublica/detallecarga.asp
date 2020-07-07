<!--#include file="administrarconsultar/clsNotas.asp"-->
<%
Dim codigo_per,nombre_usu

codigo_per=request.querystring("codigo_per")
nombre_usu=request.querystring("docente")
codigo_cac=Request.querystring("codigo_cac")
descripcion_cac=Request.querystring("descripcion_cac")

if codigo_per<>"" then
	if codigo_cac="" then codigo_cac=session("codigo_cac")	
	Dim notas

	Set notas=new clsnotas
		with notas
			Set rsDoc=.ConsultarDocente("TD","","")
			Set rsCac=.ConsultarCicloAcademico("TO","")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<link rel="stylesheet" type="text/css" href="../../../../private/estiloimpresion.css" media="print"/>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../private/validarnotas.js"></script>
</head>
<body>
<!--
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="105">Ciclo Académico</td>
    <td width="83%"><%=descripcion_cac%></td>
  </tr>
  <tr>
    <td width="105">Docente</td>
    <td width="83%"><%=nombre_usu%>&nbsp;</td>
  </tr>
  </table>
<br>
-->
	<%call .CargaAcademica("D",codigo_cac,codigo_per,nombre_usu,"")%>
</body>
</html>
<%
		end with
	Set notas=nothing
end if
%>