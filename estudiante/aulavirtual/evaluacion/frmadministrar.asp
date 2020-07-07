<%
idevaluacion=request.querystring("idevaluacion")
tituloevaluacion=request.querystring("tituloevaluacion")
numfila=request.querystring("numfila")
accion=request.querystring("accion")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Administrar Encuesta: <%=tituloevaluacion%></title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
</head>
<body onload="ResaltarPestana('0','','frmevaluacion.asp?accion=<%=accion%>&idevaluacion=<%=idevaluacion%>&numfila=<%=numfila%>')">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="100%">
  <tr class="e2" style="cursor:hand">
    <td width="30%" id="tab" bgcolor="#FEFFE1" class="bordepestana" height="6%" onClick="ResaltarPestana('0','','frmevaluacion.asp?accion=<%=accion%>&idevaluacion=<%=idevaluacion%>')">&nbsp;Información de la 
    Encuesta</td>
    <td width="2%" height="6%">&nbsp;</td>
    <td width="64%" id="tab" bgcolor="#C0C0C0" class="bordepestana" height="6%" onClick="ResaltarPestana('1','','listapreguntas.asp?idevaluacion=<%=idevaluacion%>')">
    &nbsp;  Preguntas asociadas</td>
  </tr>
  <tr>
    <td width="100%" colspan="5" height="95%" valign="top" class="contornotabla">
    <iframe id="fracontenedor" name="fracontenedor" height="100%" width="100%" border="0" frameborder="0" scrolling="no">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe></td>
  </tr>
</table>

</body>

</html>