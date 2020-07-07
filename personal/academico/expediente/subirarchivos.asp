<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Documento sin t&iacute;tulo</title>
</head>

<frameset rows="*" cols="450,*" framespacing="0" frameborder="no" border="0">
  <frame src="subirinvalumnos.aspx?codigo_cac=<% response.write(Request.QueryString("codigo_cac")) %>&codigo_per=<% response.write(Request.QueryString("codigo_per")) %>&nombre_per=<%response.write(Request.QueryString("nombre_per")) %>&codigo_cup=<%response.write(Request.QueryString("codigo_cup")) %>&nombre_cur=<%response.write(Request.QueryString("nombre_cur")) %>" name="mainFrame" id="mainFrame" />
  <frame src="vacio.htm" name="rightFrame" scrolling="No" noresize="noresize" id="rightFrame" />
</frameset>
<noframes><body>
</body>
</noframes></html>

