<!--#include file="../../../funcionesaulavirtual.asp"-->
<%
veces=request.querystring("veces")
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Documentos</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validardocumento.js"></script>
</head>
<!--oncontextmenu="return event.ctrlKey"-->
<body>
<input type="hidden" id="txtidcarpeta">
<input type="hidden" id="txttitulocarpeta">
<div id="MenuDoc" style="position:absolute;visibility:hidden">
<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" width="120" class="colorbarra">
  <tr onMouseOver="Resaltar(1,this,'S','#EBEBEB')" onMouseOut="Resaltar(0,this,'S','#EBEBEB')" onClick="AgregarDoc('A')">
    <td width="5%"><img border="0" src="../../../images/nuevo.gif"></td>
    <td width="95%">Documentos&nbsp;</td>
  </tr>
  <tr onMouseOver="Resaltar(1,this,'S','#EBEBEB')" onMouseOut="Resaltar(0,this,'S','#EBEBEB')" onClick="AgregarDoc('L')">
    <td width="5%" valign="top"><img border="0" src="../../../images/librohoja.GIF"></td>
    <td width="95%">Enlaces a web</td>
  </tr>
   <tr onMouseOver="Resaltar(1,this,'S','#EBEBEB')" onMouseOut="Resaltar(0,this,'S','#EBEBEB')" onClick="AgregarDoc('P')">
    <td width="5%" valign="top"><img border="0" src="../../../images/ext/HTM.GIF"></td>
    <td width="95%">Páginas web</td>
  </tr>
  </table>
</div>
<table cellpadding="0" cellspacing="0" width="100%" id="tbldocs" height="99%">
  <tr>
    <td class="bordeizqsup" id="tdcarpeta1" width="20%" height="5%" style="text-align: left;background-color:#D8D8C2"><%=BotonAyuda("I","../../../ayuda/documento.asp","../../../")%>&nbsp;<b>Documentos del Curso</b></td>
    <td class="bordedersup" width="10%" id="tdcarpeta2" height="5%" style="text-align: right; background-color:#D8D8C2"><%if session("idestadocursovirtual")<>3 then%><input type="button" value="   Añadir" id="cmdAgregar" class="agregar5" onclick="MuestraMenuTemp(MenuDoc)" style="display:none"><%end if%>&nbsp;</td>
    <td width="65%" height="100%" rowspan="2" valign="top">
    <iframe name="fralista" id="fralista" height="100%" width="100%" border="0" frameborder="0" target="listacarpetas" src="listadocumentos.asp" scrolling="no">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe>
    </td>
  </tr>
  <tr>
    <td width="35%" colspan="2" height="95%" id="tdcarpeta3" class="contornotabla">
    <iframe name="fracarpetas" id="fracarpetas" src="carpetas.asp?veces=<%=veces%>" height="100%" width="100%" border="0" frameborder="0" target="listacarpetas">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe></td>
  </tr>
</table>
</body>
</html>