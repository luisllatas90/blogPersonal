<%
if session("tipo_apl")=1 then
	ImprimeTitulo=session("nombreEvento")
else
	ImprimeTitulo=session("descripcion_apl")
end if
%>
<html>
<head>

<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Menú superior</title>
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<base target="contenido">
</head>

<body topmargin="0" leftmargin="0" bgcolor="#D8D8C2">

<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" bgcolor="#E33439" background="../images/sup2.png" height="103">
  <tr>
    <td width="20%" height="82">&nbsp;</td>
    <td width="80%" colspan="2" height="82" class="usatTituloAplicacion" valign="top"><br></td>
  </tr>
  <tr>
    <td width="70%" class="franja" height="21" colspan="2" style="text-align: left">
    &nbsp; USUARIO: <%=session("nombre_usu")%> / <%=session("Descripcion_Cco")%>&nbsp;</td>
    <td width="30%" class="franja" height="21" ><%=formatdatetime(now,1)%></td>
  </tr>
</table>

</body>

</html>