<%
'Response.ContentType = "application/vnd.ms-excel"
Response.ContentType = "application/msword"
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Pagina nueva 1</title>
<style>
<!--
td           { font-size: 8pt; font-family: Arial }
.LU          { border:1px solid #808080; background-color: #FF9933; font-size:7pt; text-align:center}
.MA          { border:1px solid #808080; background-color: #34B1BC; font-size:7pt; text-align:center}
.MI          { border:1px solid #808080; background-color: #6699FF; font-size:7pt; text-align:center}
.JU          { border:1px solid #808080; background-color: #99FFCC; font-size:7pt; text-align:center}
.VI          { border:1px solid #808080; background-color: #999966; font-size:7pt; text-align:center}
.SA          { border:1px solid #808080; background-color: #CC0000; font-size:7pt; text-align:center}
.contornotabla { border: 1px solid #808080 }
.etiqueta    { font-weight: bold }
.usatCeldaTitulo {
	font-family: "Arial";
	font-size: 9pt;
	background: #26758C;
	color: #FFFFFF;
	height: 16pt;
	font-weight: bold
}
-->
</style>
</head>

<body>
<%=session("horario")%>
</body>

</html>