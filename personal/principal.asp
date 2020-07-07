<%Pagina=Request.querystring("Pagina")%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title><%=session("descripcion_apl")%></title>
</head>
<frameset framespacing="0" border="0" frameborder="0" rows="106,*">
  <frame name="menusuperior" scrolling="no" noresize target="menusuperior" src="sup.asp">
  <frameset cols="19%,81%">
  <frame name="menuizq" src="izq.asp" scrolling="no" target="menuizq">
    <frame name="contenido" src="<%=Pagina%>" scrolling="auto" target="contenido">
  </frameset>
  <noframes>
  <body>
  <p>Esta página usa marcos, pero su explorador no los admite.</p>
  </body>
  </noframes>
</frameset>
</html>