<!--#include file="clslibrodigital.asp"-->
<%
if Len(Request.form("cmExportar"))>0 then
	Response.ContentType = "application/msword"
	Response.AddHeader "Content-Disposition","attachment;filename=contenidos.doc"
End if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Menú de acciones</title>
<style fprolloverstyle>A:hover {color: #FF0000; text-decoration: underline; font-weight: bold}
</style>
<style>
<!--
body{
	font-family: "Trebuchet MS", "Lucida Console", Arial, san-serif;
	color: #333333;font-size:8pt
	}
a:link       { color: #0000FF; text-decoration: underline }
.tcontenido  { border-bottom:1px solid #C0C0C0; font-family: Arial; color: #800000; font-size: 13pt; font-weight: bold; 
                }
td           { font-size: 8pt }
-->
</style>
</head>
<body>
<%
Dim contenido
	set contenido=new clslibrodigital
	with contenido
		Control=Request("chkidcontenido")
		Coleccion=split(Control,",")
		call .ConstruirDocumento(Coleccion)
	end with
	Set contenido=nothing
%>
</body>
</html>