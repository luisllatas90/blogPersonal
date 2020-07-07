<!--#include file="../../../NoCache.asp"-->
<%
rutapagina=replace(request.querystring,"rutapagina=","")
%>
<html>
<head>
<title>Procesando...</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<style type="text/css">
.usatcabezera {
	font-size: 13px;
	font-weight: bolder;
	font-family: Tahoma;
	color: #800000;
}
</style>
</head>
<body Onload="location.href='<%=rutapagina%>'">
<table border="0" width="100%" height="100%">
<tr>
<td align="center" width="100%" height="100%">&nbsp;
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="40%" height="100px" class="contornotabla">
  <tr>
    <td width="100%" height="20%" align="center" class="usatcabezera" bgcolor="#FEFFE1">
	Espere un momento por favor</td>
	</tr>
	<tr>
	<td width="100%" height="13%" align="center" bgcolor="#FEFFE1">
	su información se está procesando...
	<img border="0" src="../../../images/cargando.gif" width="209" height="20">
	<br><br>
    </td>
  </tr>
</table>
</td></tr>
</table>
</body>
</html>