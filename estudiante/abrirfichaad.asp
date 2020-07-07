<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<title>Verificando...</title>
</head>
<body onload="document.frmprocesar.submit()">
<form name="frmprocesar" method="post" action="../librerianet/academico/frmactualizardu.aspx">
<input name="hdident_usu" type="hidden" value=<%=session("ident_usu")%> />
<input name="hdcodigo_usu" type="hidden" value=<%=session("codigo_usu")%> />
</form>
</body>
</html>
