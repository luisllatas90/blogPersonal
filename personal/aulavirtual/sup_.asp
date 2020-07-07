<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Menú superior</title>
<link rel="stylesheet" type="text/css" href="../../private/estiloaulavirtual.css">
<base target="contenido">
</head>

<body topmargin="0" leftmargin="0" bgcolor="#D8D8C2">

<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" bgcolor="#EBE1BF" background="../../images/logoaulavirtual.jpg" height="103">
  <tr>
    <td width="20%" height="82">&nbsp;</td>
    <td width="80%" colspan="2" height="82" class="e3" valign="top"><br><%=session("nombrecursovirtual")%>&nbsp;</td>
  </tr>
  <tr>
    <td width="70%" class="franja" height="21" colspan="2" style="text-align: left">
    &nbsp; USUARIO: <%=session("nombre_usu")%> / <%=session("Area_Usu")%>&nbsp;</td>
    <td width="30%" class="franja" height="21" ><%=formatdatetime(now,1)%></td>
  </tr>
</table>

</body>

</html>