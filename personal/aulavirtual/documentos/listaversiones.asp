<!--#include file="clsdocumento.asp"-->
<%
iddocumento=request.querystring("iddocumento")
titulodocumento=request.querystring("titulodocumento")

Dim scriptCarpetaPredeterminada,veces
Dim NumNodoAbierto

if iddocumento<>"" then
	
	Set documento=new clsdocumento
		with documento
			.restringir=session("idcursovirtual")
			NumNodoAbierto =request.querystring("NumNodoAbierto")
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Versiones del documento</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="../../../private/Menu.js"></script>
<script language="JavaScript" src="private/validardocumento.js"></script>
<STYLE>
<!--
	img 	{border:0px none;align:absbottom}
	tr      {top: 0;cursor:hand }
-->
<!--oncontextmenu="return event.ctrlKey"-->
</STYLE>
</head>

<body topmargin="0" leftmargin="0">
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="98%">
  <tr>
    <td width="100%" height="5%" class="etabla2" style="text-align: left">Documento: <%=titulodocumento%>&nbsp;</td>
  </tr>
  <tr>
    <td width="100%" height="55%" valign="top">
    <DIV id="listadiv" style="height:100%">
	<table cellpadding=0 cellspacing=0 border=0 width="100%">
		<%response.write .crearArbolVersiones(iddocumento,titulodocumento,0,"",session("codigo_usu"),session("idcursovirtual"),session("tipofuncion"),usuarioactual)%>
	</table>
  	</DIV>
    </td>
  </tr>
  <tr>
    <td width="100%" height="3%" colspan="3">
  	<table cellSpacing="0" cellPadding="3" width="100%" border="0" style="border-collapse: collapse" bordercolor="#111111" >
  	<tr>
    <td class="paginaDoc" width="53%">Detalle del documento</td>
    <td width="37%" class="azul" background="../../../images/fondopestana2.gif" align="right" valign="top">
    Por revisar <img border="0" src="../../../images/V0.GIF">&nbsp; | Revisado 
    <img border="0" src="../../../images/V1.GIF">
    </td>
  	</tr>
  	</table>
    </td>
  </tr>
  <tr id="detalledoc">
    <td width="100%" height="45%" valign="top">
    <span class="sugerencia" id="mensajedetalledoc">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Actualmente no hay ningún documento seleccionado de la Lista.</span>
    <iframe name="fradetalleversion" height="100%" width="100%" border="0" frameborder="0" src="detalleversion.asp">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe></td>
  </tr>
</table>
</body>
</html>
		<%end with
	Set documento=nothing
end if%>