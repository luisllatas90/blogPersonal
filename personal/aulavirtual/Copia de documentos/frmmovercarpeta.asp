<!--#include file="../../../NoCache.asp"-->
<!--#include file="clsdocumento.asp"-->
<%
idcarpeta=request.querystring("idcarpeta")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Seleccionar ubicación de la Carpeta...</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validardocumento.js"></script>
<style>
<!--
li	{cursor:hand;list-style-image: url('../../../images/cerrado.gif'); margin-left:-15}
-->
</style>
</head>
<body class="colorbarra" leftmargin="0" topmargin="0">
<input type="hidden" id="txtCarpetaElegida" value="0">
<input type="button" value="Guardar" onClick="MoverCarpeta('<%=idcarpeta%>')" name="cmdGuardar" id="cmdGuardar" class="guardar"> <input OnClick="window.close()" type="button" value="Cancelar" name="cmdCancelar" id="cmdCancelar" class="salir">
<br><br>
<DIV id="listadiv" style="height:80%" class="contornotabla">
   	<table border="0" cellpadding="2" cellspacing="0" width="100%" height="319" bgcolor="#FFFFFF">
   	<tr><td valign="top" height="315">
		<img border="0" src="../../../images/abierto.gif"><span class="nodoRaiz" id="doc0" onClick="ResaltarCarpeta(this)">&nbsp;Carpeta Principal</span>
		<ul style="margin-top: 3">
			<%Dim documento
			Set documento=new clsdocumento
				call documento.CargarCarpeta(session("codigo_usu"),0,session("Idcursovirtual"),idcarpeta)
			Set documento=nothing%>
		</ul>
   		</td></tr>
	</table>
</DIV>
<b>&nbsp;Carpetas creadas por:&nbsp;<%=session("nombre_usu")%></b>
</body>
</html>