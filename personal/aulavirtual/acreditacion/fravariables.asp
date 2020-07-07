<%
	idseccion=request.QueryString("idseccion")
	nombreseccion=request.QueryString("nombreseccion")
%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>variables de la sección</title>
</head>

<frameset rows="*" framespacing="0" border="0" frameborder="0">
  <frameset cols="243,*">
    <frame name="contenido" target="principal" src="izq.asp?idseccion=<%=idseccion%>&nombreseccion=<%=nombreseccion%>" scrolling="no">
    <frame name="principal" src="listavariables.asp?idseccion=<%=idseccion%>&nombreseccion=<%=nombreseccion%>">
  </frameset>
  <noframes>
  <body>

  <p>Esta página usa marcos, pero su explorador no los admite.</p>

  </body>
  </noframes>
</frameset>

</html>