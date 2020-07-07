<%tipomnu=request.querystring("tipomnu")%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title>Campus Virtual USAT</title>
</head>
<frameset rows="4%,*"  frameborder="0" border="0" framespacing="0">
  <frame src="cabecera.asp" name="fraArriba" scrolling="No" id="fraArriba">
  <frameset rows="*" framespacing="0" frameborder="no" border="0">
    <frameset cols="24%,*" framespacing="0" frameborder="no" border="0" id="fraGrupo">
    <frame src="<%=tipomnu%>" name="fraIzq" scrolling="no" id="fraIzq">
    <frame src="about:blank" name="fraPrincipal" id="fraPrincipal">
  	</frameset>
  </frameset>
</frameset>
<noframes>
<body>
</body>
</noframes></html>
