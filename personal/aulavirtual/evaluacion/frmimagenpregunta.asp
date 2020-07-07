<%
	idPregunta=Request.querystring("idPregunta")
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Pagina nueva 1</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
</head>

<body>
<form name="frmdocumento" enctype="multipart/form-data" method="post" action="agregarimg.asp?idPregunta=<%=idpregunta%>">
<input type="hidden" name="doCreate" value="true">
<fieldset style="padding: 2">
  <legend>
  <p class="e1">Agregar Referencia a la pregunta</p>
  </legend>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
    <tr>
      <td width="140"><font color="#800000">Buscar ubicación del archivo</font></td>
      <td width="549">
      <input class="cajas" type="File" name="file" size="50" style="height:20"></td>
    </tr>
    <tr>
      <td width="140">Enlace a página web</td>
      <td width="549"><input type="text" name="URL" size="20" class="Cajas" style="width: 400"></td>
    </tr>
    <tr>
      <td width="140" valign="top">&nbsp;</td>
      <td width="549">&nbsp;</td>	
    </tr>
    <tr>
      <td width="140">&nbsp;</td>
      <td width="549">
    <input type="submit" value="Guardar" name="cmdGuardar" class="guardar"> <input OnClick="location.href='listapreguntas.asp?idevaluacion=<%=idEvaluacion%>&tituloEvaluacion=<%=replace(tituloEvaluacion,"'","")%>'" type="button" value="Cancelar" name="cmdCancelar" class="salir"></td>
    </tr>
  </table>
</fieldset>
</form>
</body>

</html>