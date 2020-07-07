<!--#include file="clsdocumento.asp"-->
<%
Dim scriptCarpetaPredeterminada,veces
Dim NumNodoAbierto

Set documento=new clsdocumento
	with documento
		.restringir=session("idcursovirtual")
		NumNodoAbierto =request.querystring("NumNodoAbierto")
		mostrardescargas=request.querystring("mostrardescargas")
		veces=request.querystring("veces")
%>
<html>
<HEAD>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="../../../private/Menu.js"></script>
<script language="JavaScript" src="private/validardocumento.js"></script>
<STYLE>
<!--
	img 	{border:0px none;align:absbottom}
	tr      {top: 0;cursor:hand }
-->
</STYLE>
<base target="_self">
</HEAD>
<body topmargin="0">
<table cellpadding=0 cellspacing=0 border=0 width="100%" heigth="100%">
<%		response.write .CargarTodo(0,"",session("codigo_usu"),session("idcursovirtual"),session("tipofuncion"),session("codigo_usu"),mostrardescargas)
	end with
Set documento=nothing
%>
</table>
</body>
</html>