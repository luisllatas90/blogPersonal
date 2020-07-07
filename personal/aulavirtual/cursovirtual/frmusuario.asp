<%
accion=request.querystring("accion")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Pagina nueva 1</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
</head>
<body onload="document.all.idusuario.focus()">
<p class="e4">Agregar Usuarios</p>
<form method="POST" action="procesar.asp?accion=<%=accion%>">
<table class="contornotabla" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr>
    <td width="15%">Código</td>
    <td width="75%">
      <input type="text" name="idusuario" size="20" value="<%=idusuario%>" class="cajas">
	</td>
  </tr>
  <tr>
    <td width="15%">Apellidos y Nombres</td>
    <td width="75%">
    <input type="text" name="nombreusuario" size="20" value="<%=nombreusuario%>" class="cajas"></td>
  </tr>
  <tr>
    <td width="15%">Email</td>
    <td width="75%">
    <input type="text" name="email" size="20" value="<%=email%>" class="cajas"></td>
  </tr>
  <tr>
    <td width="100%" colspan="2" align="center">
    <input type="submit" value="Guardar" name="cmdGuardar">
    <input type="reset" value="Cancelar" name="cmdCancelar"></td>
  </tr>
</table>
</body>
</html>