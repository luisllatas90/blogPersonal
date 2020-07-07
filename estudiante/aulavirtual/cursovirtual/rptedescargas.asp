<!--#include file="clsreportes.asp"-->
<%
Set reporte=new clsReportes
	with reporte
		.restringir=session("idcursovirtual")
		iddocumento =request.querystring("iddocumento")
		ArrDescargas=.CargarDescargas(iddocumento)
	end with
Set reporte=nothing
%>
<html>
<HEAD>
<meta http-equiv="Content-Language" content="es">

<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<base target="_self">
</HEAD>
<body>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr>
    <td width="50%" class="e4">Descargas del documento</td>
    <td width="50%" align="right">
    <input type="button" value="Regresar" name="cmdRegresar" class="salir" onclick="history.back(-1)">
    </td>
  </tr>
</table>
<%if IsEmpty(ArrDescargas)=false then%>
<br>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
  <tr class="etabla">
    <td width="6%">Nº</td>
    <td width="21%">Fecha de descarga</td>
    <td width="73%">Participante</td>
  </tr>
  <%for i=lbound(ArrDescargas,2) to Ubound(ArrDescargas,2)%>
  <tr>
    <td width="6%"><%=i+1%>&nbsp;</td>
    <td width="21%"><%=ArrDescargas(0,i)%>&nbsp;</td>
    <td width="73%"><%=ArrDescargas(1,i)%>&nbsp;</td>
  </tr>
  <%next%>
</table>
<%end if%>
</body>
</html>