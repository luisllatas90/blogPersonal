<%
idtarea=request.querystring("idtarea")
numfila=request.querystring("numfila")
accion=request.querystring("accion")
idtipotarea=request.querystring("idtipotarea")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Modificar tarea</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
</head>
<body onload="ResaltarPestana('0','','frmtarea.asp?accion=<%=accion%>&idtarea=<%=idtarea%>&numfila=<%=numfila%>')" bgcolor="#EEEEEE">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="100%">
  <tr class="e2" style="cursor:hand">
    <td width="20%" id="tab" bgcolor="#FEFFE1" class="bordepestana" height="8%" onClick="ResaltarPestana('0','','frmtarea.asp?accion=<%=accion%>&idtarea=<%=idtarea%>&numfila=<%=numfila%>')">
    &nbsp;Información de la tarea</td>
    <td width="2%" height="6%">&nbsp;</td>
    <td width="36%" id="tab" bgcolor="#C0C0C0" class="bordepestana" height="8%" onClick="ResaltarPestana('1','','listarecursos.asp?idtarea=<%=idtarea%>&idtipotarea=<%=idtipotarea%>&idtipopublicacion=0')">&nbsp;Recursos 
    relacionados</td>
  </tr>
  <tr>
    <td width="100%" colspan="3" height="92%" valign="top" class="contornotabla">
    <iframe id="fracontenedor" name="fracontenedor" height="100%" width="100%" border="0" frameborder="0" scrolling="no">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe></td>
  </tr>
</table>

</body>

</html>