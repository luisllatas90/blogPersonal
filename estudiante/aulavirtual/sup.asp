<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Menú superior</title>
<link rel="stylesheet" type="text/css" href="../../private/estiloaulavirtual.css">
<base target="contenido">
<style type="text/css">
.titulocurso {
	font-weight: bold;
	color: #FFFFFF;
	font-family: Arial;
	font-size: 20px;
	text-indent: 100px;
}
</style>
</head>

<body topmargin="0" leftmargin="0">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%" bgcolor="#990000" background="../../images/banner2.gif" height="125">
  <tr>
    <td width="100%" height="82" colspan="2" valign="middle" class="titulocurso"><%=session("nombrecursovirtual")%>&nbsp;</td>
  </tr>
  <tr>
    <td width="50%" height="20" valign="middle" style="text-align: left"><font color="#666633"><b>&nbsp; USUARIO: <%=session("nombre_usu")%> </b></font>&nbsp;</td>
    <td width="50%" height="20" align="center"><font color="#FFFFFF"><%=formatdatetime(now,1)%></font></td>
  </tr>
</table>

</body>

</html>