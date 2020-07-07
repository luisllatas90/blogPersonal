<!--#include file="clsreportes.asp"-->
<%
Dim NumNodoAbierto

Set reporte=new clsReportes
	with reporte
		.restringir=session("idcursovirtual")
		NumNodoAbierto =request.querystring("NumNodoAbierto")
%>
<html>
<HEAD>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validaraula.js"></script>
<STYLE>
<!--
	img 	{border:0px none;align:absbottom}
	tr      {top: 0;cursor:hand }
-->
</STYLE>
<base target="_self">
</HEAD>
<body>
<table cellpadding=0 cellspacing=0 border=0 width="100%" heigth="100%">
<%		response.write .CargarRpteEncuestas(session("idcursovirtual"))
	end with
Set reporte=nothing
%>
</table>
</body>
</html>