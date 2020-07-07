<%
rutapagina=request.querystring("pagina")

if instr(rutapagina,"agregarcursomatricula")>0 then
	rutapagina="frmmatricula2015.asp?accion=agregarcursomatricula&codigo_mat=" & request.querystring("codigo_mat")
end if
%>
<html>
<head>
<title>Procesando...</title>
<link rel="stylesheet" type="text/css" href="../private/estilo.css">

</head>
<body Onload="location.href='<%=rutapagina%>'">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
  <tr>
    <td width="100%" align="center" class="usatTitulo" bgcolor="#FEFFE1">
Procesando<br>
Por favor espere un momento...<br>
<img border="0" src="../images/cargando.gif" width="209" height="20">
    </td>
  </tr>
</table>
<script type="text/javascript" language="JavaScript" src="private/analyticsEstudiante.js?x=1"></script>
</body>
</html>