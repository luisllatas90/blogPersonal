<%
idtarea=request.querystring("idtarea")
idtareausuario=request.querystring("idtareausuario")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registrar archivo según tarea asignada</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validartarea.js"></script>
</head>
<body topmargin="0">
<form name="frmdocumento" enctype="multipart/form-data" method="post" onSubmit="return validarenviotarea(this)"  action="guardarversion.asp?idtarea=<%=idtarea%>&idtareausuario=<%=idtareausuario%>">
<input type="hidden" name="doCreate" value="true">
<fieldset style="padding: 2">
  <legend class="e1">Enviar archivo</legend>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
    <tr>
      <td width="23%" valign="top" class="etiqueta">Buscar ubicación del archivo&nbsp;</td>
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
    <input type="submit" value="Enviar" name="cmdGuardar" id="cmdGuardar" class="guardar"> <input OnClick="window.close()" type="button" value="Cancelar" name="cmdCancelar" id="cmdCancelar" class="salir">
    <span id="mensaje" style="color:#FF0000"></span>
    </td>
    </tr>
  </table>
</fieldset>
</form>
</body>
</html>