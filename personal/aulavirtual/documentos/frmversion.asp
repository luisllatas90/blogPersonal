<!--#include file="clsdocumento.asp"-->
<%
iddocumento=request.querystring("idDocumento")
idversion=request.querystring("idversion")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registrar versiones del documento</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/calendario.js"></script>
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validardocumento.js"></script>
</head>
<body topmargin="0" onload="document.all.tituloversion.focus()">
<form name="frmdocumento" enctype="multipart/form-data" method="post" onSubmit="return validarversiondoc(this)" action="guardarversion.asp?idDocumento=<%=idDocumento%>&idversion=<%=idversion%>">
<input type="hidden" name="doCreate" value="true">
<input type="hidden" name="txtidversion" value="<%=idversion%>">
<fieldset style="padding: 2">
  <legend class="e1">Datos de la versión del documento <%=titulo%></legend>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1" height="100%">
    <tr>
      <td width="23%" valign="top" class="etiqueta">Título de la versión</td>
      <td width="77%" valign="top">
    <input  maxLength="100" size="74" name="tituloversion" class="cajas"></td>
    </tr>
    <tr>
      <td width="23%" valign="top" class="etiqueta">Ubicación del archivo&nbsp;</td>
      <td width="77%" valign="top"><input class="cajas" type="File" name="file" size="50" style="height:20"></td>
    </tr>
   	<tr>
      <td width="23%" valign="top" class="etiqueta">Observaciones</td>
      <td width="77%" valign="top">
      <textarea  name="obs" rows="4" cols="72" class="cajas"></textarea>
    </td>	
    </tr>
    <tr>
      <td width="23%">&nbsp;</td>
      <td width="77%">
    <input type="submit" value="Guardar" name="cmdGuardar" id="cmdGuardar" class="guardar"> <input OnClick="window.close()" type="button" value="Cancelar" name="cmdCancelar" id="cmdCancelar" class="salir">
    </td>
    </tr>
  </table>
</fieldset>
</form>
</body>
</html>