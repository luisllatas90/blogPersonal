<%
Pagina=session("Pagina")
%>
<html>

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>Usuario: <%=session("nombre_usu")%></title>
</head>

<frameset rows="100,*" framespacing="0" border="0" frameborder="0">
  <frame name="tituloEvaluacion" scrolling="no" noresize target="tituloEvaluacion" src="sup.asp">
  <frame name="preguntasEvaluacion" src="<%=Pagina%>" target="preguntasEvaluacion" scrolling="auto" noresize>
  <noframes>
  <body>

  <p>Esta página usa marcos, pero su explorador no los admite.</p>

  </body>
  </noframes>
</frameset>

</html>