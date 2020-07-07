<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Buscar documentos</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="private/validararchivo.js"></script>
</head>
<body bgcolor=Gainsboro>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%">
  <tr>
    <td width="35%">&nbsp;&nbsp;Término de búsqueda</td>
    <td width="65%"><input name="texto" size="50" style="width:100%" class="cajas"></td>
  </tr>
  <tr>
    <td width="35%" valign="top" height="30%">&nbsp;&nbsp;Buscar en: </td>
    <td width="65%" height="30%">
          <select name="cbxcampo" style="width:100%; height:100px" multiple onChange="desactivarCamposBusqueda()">
          <option value="Asunto" >- Asunto</option>
          <option value="NumeroExpediente" selected>- Nº de Expediente</option>
          <option value="NombreProcedencia">- Procedencia</option>
          <option value="NombreDestino">- Destino</option>
          <option value="Fechaarchivo">- Fecha Recepción</option>
          <option value="%%%%%">- Mostrar todos los documentos</option>
          </select></td>
  </tr>
  <tr valign=top>
    <td width="35%">&nbsp;</td>
    <td width="65%">
    <input type="button" class="buscar" value="Buscar" OnClick="EvaluarCriteriosbusqueda()" NAME="cmdbuscar">
    <input type="button" class="salir" value="Cerrar" OnClick="window.close()" NAME="cmdcerrar"></td>
  </tr>
</table>
</body>
</html>