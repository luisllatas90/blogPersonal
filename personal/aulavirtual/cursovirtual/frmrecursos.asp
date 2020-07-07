<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Seleccione el tipo de recurso</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarcurso.js"></script>
</head>
<body topmargin="0" leftmargin="0" bgcolor="#EEEEEE" onLoad="AbrirRecurso('evaluacion')">
<input type="hidden" id="rutarecurso">
<input type="hidden" id="descargas">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1" height="98%">
  <tr>
    <td width="20%" class="etiqueta" height="6%">&nbsp;Tipo de recurso</td>
    <td width="16%" height="6%">
    <select size="1" id="cbxrecurso" onChange="AbrirRecurso(this.value)">
    <option value="documento">Documentos</option>
    <option value="evaluacion" selected>Encuestas</option>
    <option value="foro">Foros de discusión</option>
 	<option value="tarea">Tareas</option>    
    </select></td>
    <td width="64%" height="6%" align="right">
    <input type="button" onClick="VisualizarDescargas()" style="background-position: left 80%; display:none; background-image:url('../../../images/download.gif'); background-repeat:no-repeat" value=" Ver Descargas" id="cmddescargas" class="boton4">
    <input type="button" onClick="VisualizarRecurso()" style="background-position: left 80%; display:none; background-image:url('../../../images/abierto.gif'); background-repeat:no-repeat" value="  Abrir Recurso" id="cmdabrirrecurso" class="boton4">
    </td>
  </tr>
  <tr>
    <td width="100%" colspan="3" height="94%">
    <iframe name="frarecurso" height="100%" width="100%" id="frarecurso">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe></td>
  </tr>
</table>
</body>

</html>