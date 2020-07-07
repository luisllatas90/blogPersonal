<%idcursovirtual=request.querystring("idcursovirtual")%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de imágenes para la Página web</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<base target="_self">
</head>
<body>
<form name="frmImagen" enctype="multipart/form-data" method="post" action="guardararchivo.asp?idcursovirtual=<%=idcursovirtual%>">
<input type="hidden" name="doCreate" value="true">
<fieldset style="padding: 2">
  <legend>
  <p class="e1">Copiar imágenes a la página Web</p>
  </legend>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
    <tr>
      <td width="59" height="20"><font color="#800000">Ubicación</font></td>
      <td width="602" height="20">
      <input class="cajas" type="File" name="file" size="50" style="height:20"></td>
    </tr>
    <tr>
      <td width="59" height="26">&nbsp;</td>
      <td width="602" height="26"><input type="submit" value="Guardar" name="cmdGuardar" class="guardar"> <input OnClick="window.close()" type="button" value="Cancelar" name="cmdCancelar" class="salir">
    </td>
    </tr>
  </table>
</fieldset>
</form>
</body>

</html>