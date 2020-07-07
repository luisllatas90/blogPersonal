<!--#include file="clslibrodigital.asp"-->
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Recopilar contenido temático</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarlibrodigital.js"></script>
<style fprolloverstyle>A:hover {color: #FF0000; text-decoration: underline; font-weight: bold}
</style>
<style>
<!--
a:link       { color: #0000FF; text-decoration: underline }
.linea	{cursor:hand;margin-top:0; margin-bottom:0}
-->
</style>
</head>
<body>
<form name="frmrecopilar" METHOD="POST" onSubmit="return validarrecopilacion(this)" ACTION="resultadorecopilar.asp">
<table class="bordeinf" border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="50%" class="e4">Recopilador de contenidos&nbsp;</td>
    <td width="50%" align="right">
<input type="submit" value="Recopilar" name="cmdRecopilar" class="buscar">
<input type="submit" value="  Exportar..." name="cmExportar" class="exportar"></td>
  </tr>
</table>
<br>
<DIV id="listadiv" style="height:82%;">
<%
Dim contenido
	set contenido=new clslibrodigital
	with contenido
		.restringir=session("idcursovirtual")
		idlibrodigital=request.querystring("idlibrodigital")
		call .Cargarcontenido(idlibrodigital,0)
	end with
	Set contenido=nothing
%>
</DIV>
</form>
</body>
</html>