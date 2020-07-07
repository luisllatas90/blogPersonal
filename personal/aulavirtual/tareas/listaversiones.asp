<!--#include file="clstarea.asp"-->
<%
idtarea=request.querystring("idtarea")
titulotarea=request.querystring("titulotarea")
idusuario=request.querystring("idusuario")
idusuario=replace(idusuario,"/","\")
idtipotarea=request.querystring("idtipotarea")
modo=request.querystring("modo")

Dim NumNodoAbierto

if idtarea<>"" then
	
	Set tarea=new clstarea
		with tarea
			.restringir=session("idcursovirtual")
			NumNodoAbierto =request.querystring("NumNodoAbierto")
%>
<html xmlns:v="urn:schemas-microsoft-com:vml" xmlns:o="urn:schemas-microsoft-com:office:office">
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Versiones del documento</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validartarea.js"></script>
<STYLE>
<!--
	img 	{border:0px none;align:absbottom}
	tr      {top: 0;cursor:hand }
-->
</STYLE>
</head>
<body >
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="98%">
  <tr>
    <td width="86%" height="5%" class="etabla2" style="text-align: left">Tarea: <%=titulotarea%>&nbsp;</td>
     <%if modo="" then%>
    <td width="14%" height="5%" class="etabla2" style="text-align: left"> <input OnClick="RegresarAListaTareas('<%=idtarea%>','<%=titulotarea%>')" type="button" value="Cancelar" name="cmdCancelar" id="cmdCancelar" class="salir"></td>
    <%end if%>
  </tr>
  <tr>
    <td width="100%" height="50%" valign="top" colspan="2">
    <DIV id="listadiv" style="height:100%">
	<table cellpadding=0 cellspacing=0 border=0 width="100%">
		<%response.write .cargardocumentos(idtarea,titulotarea,0,"",idusuario,modo)%>
	</table>
  	</DIV>
    </td>
  </tr>
  <tr>
    <td width="100%" height="3%" colspan="2">
  	<table cellSpacing="0" cellPadding="3" width="100%" border="0" style="border-collapse: collapse" bordercolor="#111111" >
  	<tr>
    <td class="paginaDoc" width="53%"><img border="0" src="../../../images/menos.gif" onclick="MostrarTabla(detalledoc,'../../../images/',this)"> 
	Detalle de la versión de tarea seleccionada </td>
    <td width="37%" class="azul" background="../../../images/fondopestana2.gif" align="right" valign="top">
    Por revisar <img border="0" src="../../../images/CR.GIF">&nbsp; | Revisado 
    <img border="0" src="../../../images/CV.GIF">
    </td>
  	</tr>
  	</table>
    </td>
  </tr>
  <tr id="detalledoc">
    <td width="90%" height="40%" colspan="2">
    <span class="sugerencia" id="mensajedetalledoc">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Actualmente no hay ningún documento seleccionado de la Lista.</span>
    <iframe name="fradetalleversion" id="fradetalleversion" height="100%" width="100%" border="0" frameborder="0">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe>
    </td>
  	</tr>
</table>
</body>
</html>
		<%end with
	Set documento=nothing
end if%>