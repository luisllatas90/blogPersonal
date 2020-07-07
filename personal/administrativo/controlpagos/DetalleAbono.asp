<!--#include file="../../../funciones.asp"-->
<%

codigo_deu=request.querystring("cc")
datos =request.querystring("dat")

Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion
set rs= obj.Consultar("CAJ_DetalleAbonoDeudas","FO",codigo_deu)
obj.CerrarConexion
set obj= Nothing
%>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Deudas por cobrar</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../private/calendario.js"></script>
</head>

<body bgcolor="#EEEEEE">

<p class="usatTitulo"><%=datos%></p>

<p>Detalle de Abonos</p>

	<table bgcolor="white" bordercolor="silver" border="1" cellpadding="3" style="width: 100%;border-collapse:collapse">
		<tr class="usatCeldaTitulo">
			
			<td align="center">Fecha</td>	
			<td align="center">Tipo Doc</td>
			<td align="center">Nro Doc</td>
			<td align="center">Servicio</td>
			<td align="center">Importe (S/.)</td>
			<td align="center">Importe Transf. a Otra Deuda</td>
			<td align="center">Observacion</td>
		</tr>
		<%
		Do while not rs.EOF
			
		%>
		<tr <%=clase%> onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
			
			<td><%=rs("fecha")%>&nbsp;</td>
			<td><%=rs("descripcion_tdo")%>&nbsp;</td>
			<td><%=rs("nroDocumento_cin")%>&nbsp;</td>
			<td><%=rs("descripcion_sco")%>&nbsp;</td>
			<td align="right"><%=formatnumber(trim(rs("subtotal_dci")),2)%>&nbsp;</td>
			<td align="right"><%=formatnumber(trim(rs("montoTransf_dci")),2)%>&nbsp;</td>
			<td><%=rs("observacion_cin")%>&nbsp;</td>
		</tr>
		<%rs.movenext
		Loop

		%>
		<!--<tr class="usatTablaInfo">
			<td colspan="5" style="text-align: right; font-weight: 700;">TOTAL</td>
			<td align="right" style="font-weight: 700"><%'=formatnumber(tc,2)%>&nbsp;</td>
			<td align="right" style="font-weight: 700"><%'=formatnumber(ta,2)%>&nbsp;</td>
			<td align="right" style="font-weight: 700"><%'=formatnumber(ts,2)%>&nbsp;</td>		
		</tr>-->
	</table>
</body>
</html>