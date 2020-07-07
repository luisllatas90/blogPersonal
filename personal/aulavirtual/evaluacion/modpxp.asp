<!--#include file="clsevaluacion.asp"-->
<%
'on error resume next

Dim PagActual 'Página que queremos mostrar
Dim iEstado
Dim IdPregunta

PagActual=CInt(Request.querystring("PagActual"))
If PagActual="" then PagActual=1

modificarRpta=Request.querystring("modificarRpta")
if modificarRpta="" then modificarRpta="No"
IdPregunta=Request.querystring("IdPregunta")
descripcionrpta=Request.querystring("descripcionrpta")
mostrarbotonvr=Request.querystring("mostrarbotonvr")
if mostrarbotonvr="" then mostrarbotonvr="No"

sURL = Request.ServerVariables("SCRIPT_NAME")
if Request.ServerVariables("QUERY_STRING") <> "" Then
	sURL = sURL & "?" & Request.ServerVariables("QUERY_STRING")
	session("Pagina")=sURL
else
	sURL=sURL
	session("Pagina")=sURL
End if

Set evaluacion=new clsevaluacion
	with evaluacion
		.RestringirInicioSesion=session("codigo_usu")
				
		If modificarRpta="Si" then
			ArrDatos=.consultar("5",idpregunta,"","")
			session("Arrdatos")=ArrDatos
		end if
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Ficha de evaluación</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="private/validarpregunta.js"></script>
<base target="preguntasEvaluacion">
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
<style>
<!--
.navegacion  { border: 1px solid #C0C0C0; background-color: #FEFFE1; font-family:Verdana; font-size:8pt; height:22 }
.contorno    { border: 1px solid #C0C0C0 }
-->
</style>
</head>
<body Onload="enfoquepregunta()">
<%
If IsEmpty(session("Arrdatos"))=false then
	iEstado = .PaginarPreguntas(session("idevaluacion"),1,PagActual,session("Arrdatos"),session("retrocederpaginas"),modificarRpta,session("mostrarresultados"),mostrarbotonvr)
end if
%>
</body>
</html>
<%end with
Set evaluacion=nothing
%>