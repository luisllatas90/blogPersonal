<%
idcursovirtual=session("idcursovirtual")
titulocurso=session("titulocurso")
codigo_apl=session("codigo_apl")
codigo_tfu=session("codigo_tfu")
accion=request.querystring("accion")

if accion="" then accion="modificarcurso"
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Curso virtual: <%=titulocurso%></title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
</head>
<body onload="ResaltarPestana('0','','frmcurso.asp?accion=<%=accion%>&idcursovirtual=<%=idcursovirtual%>&codigo_apl=<%=codigo_apl%>')">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="100%">
  <tr class="e2" style="cursor:hand">
    <td width="20%" id="tab" bgcolor="#FEFFE1" class="bordepestana" height="6%" onClick="ResaltarPestana('0','','frmcurso.asp?accion=<%=accion%>&idcursovirtual=<%=idcursovirtual%>&codigo_apl=<%=codigo_apl%>&codigo_tfu=<%=codigo_tfu%>')">&nbsp;Información del Curso</td>
    <td width="2%" height="6%">&nbsp;</td>
    <td width="20%" id="tab" bgcolor="#C0C0C0" class="bordepestana" height="6%" onClick="ResaltarPestana('1','','abrircurso.asp?idcursovirtual=<%=idcursovirtual%>')">&nbsp;Diseño de Página Web</td>
    <td width="2%" bgcolor="#FFFFFF" height="6%">&nbsp;</td>
    <td width="36%" id="tab" bgcolor="#C0C0C0" class="bordepestana" height="6%" onClick="ResaltarPestana('2','','listamatriculadoscurso.asp?idcursovirtual=<%=idcursovirtual%>&codigo_apl=<%=codigo_apl%>&codigo_tfu=<%=codigo_tfu%>')">&nbsp;Administrar permisos</td>
  </tr>
  <tr>
    <td width="100%" colspan="5" height="95%" valign="top" class="contornotabla">
    <iframe id="fracontenedor" name="fracontenedor" height="100%" width="100%" border="0" frameborder="0" scrolling="no">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe></td>
  </tr>
</table>

</body>

</html>