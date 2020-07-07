<!--#include file="../../funciones.asp"-->
<%
call Enviarfin(session("codigo_usu"),"../../")
%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title>Menú</title>
<link href="../../private/estilo.css" rel="stylesheet" type="text/css">
<script language="JavaScript" src="../../private/funciones.js"></script>
<script language="Javascript">
function OcultarFrame()
{
	var NombreFrame=top.parent.document.getElementById('fraGrupo')
	var TamanioActual=NombreFrame.cols

	if (TamanioActual=="3%,*"){
		NombreFrame.cols="24%,*"
		document.all.tblMnuPrincipal.style.display=""
		document.all.tblTituloMnu.style.display="none"
	}
	else{
		document.all.tblMnuPrincipal.style.display="none"
		document.all.tblTituloMnu.style.display=""
		NombreFrame.cols="3%,*"
	}
}
</script>
<style type="text/css">
<!--
body 
{
	background-color:#F0F0F0}
-->
</style>
</head>
<body topmargin="0" leftmargin="0" rightmargin="0">
<table width="100%" height="100%" border="0" cellpadding="4" cellspacing="0" id="tblMnuPrincipal">
  <tr>
    <td width="97%" class="bordeinf" valign="middle" height="5%" valign="top">
    <b> <font size="3" color="#800000" face="Arial">Opciones de Menú</font></b>
	</td>
	<td width="3%" align="right" style="cursor:hand" class="bordeinf" onclick="OcultarFrame()">
	<img src="../../images/menus/contraer.gif">
	</td>
	</tr>
  <tr width="100%">
    <td class="contornotabla_azul" valign="top" height="95%" colspan="2" valign="top">
    <iframe name="fradetalle" src="carpetas.asp?codigo_men=0&amp;tipoImagen=iconoSel_men" width="100%" height="100%" scrolling="auto" frameborder="0" target="_self">
	</iframe>
    </td>
	</tr>
</table>
<table style="width: 100%;display:none;height:100%;cursor:hand" id="tblTituloMnu" bgcolor="#CCCCCC">
	<tr><td align="left" valign="top">
		<img src="../../images/menus/barramenu.gif" onClick="OcultarFrame()" class="style1"></td>
	</tr>
</table>
</body>
</html>