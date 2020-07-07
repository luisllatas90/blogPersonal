<!--#include file="../../../../funciones.asp"-->
<%
codigo_ins=request.querystring("codigo_ins")
codigo_ped=request.querystring("codigo_ped")
codigo_eped=request.querystring("codigo_eped")
codigo_cco=request.querystring("codigo_cco")
tipobandeja=request.querystring("tipobandeja")
tipo=request.querystring("tipo")
cantidad=request.querystring("cantidad")
accion=request.querystring("accion")
codigo_dpe = request.querystring("codigo_dpe")

%>
<%
'if codigo_dpe<>"" then
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsDetalle=Obj.Consultar("BI_ConsultarPedidos","FO","VD",0,codigo_dpe)
		Obj.CerrarConexion
	Set obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>DETALLE DE PEDIDO</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<style type="text/css">
<!--
.style1 {
	color: #000000;

}
.style2 {
	color: #000000;
	font-weight: bold;
}
.Estilo2 {
	color: #000000;
	font-size: 14pt;
	font-weight: bold;
}
-->
</style>
<script language="JavaScript" src="../../../../private/tooltip.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">

</script>
</head>

<body style="background-color: #EEEEEE">
<form name="frmDetalle" method="post" action="procesar.asp?accion=aprobarpedido&codigo_ped=<%=codigo_ped%>">
<%
rsDetalle.MoveFirst
'Do while not rsDetalle.EOF
'	i=i+1
%>
<table style="width: 100%" bgcolor="white" class="contornotabla">
	<tr>
	  <td width="2%" rowspan="12" valign="top" bgcolor="#FFFFCC" style="width: 5%">&nbsp;</td>
	  <td colspan="2" class="usatLinkCeldaGris" style="width: 15%"><span class="Estilo2">DATOS BIBLIOGR&Aacute;FICOS DEL PEDIDO </span></td>
    </tr>
	<tr>
	  <td width="22%" class="usatLinkCeldaGris" style="width: 15%">&nbsp;</td>
	  <td width="76%" style="width: 75%">&nbsp;</td>
    </tr>
	<tr>
	  <td style="width: 15%" class="usatLinkCeldaGris">&nbsp;</td>
	  <td style="width: 75%">&nbsp;</td>
    </tr>
	<tr>
		<%if codigo_ins>0 and tipobandeja="E" then%>		
		<%end if%>
		<td style="width: 15%" class="usatLinkCeldaGris">Nº</td>
		<td style="width: 75%"><%=i%></td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Procedencia</td>
		<td style="width: 75%"><%=rsDetalle("tipo_dpe")%>&nbsp;</td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Título</td>
		<td style="width: 75%"><span class="style2"><%=rsDetalle("titulo")%></span></td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Autor</td>
		<td style="width: 75%"><span class="style2"><%=rsDetalle("nombreautor")%></span></td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Editorial</td>
		<td style="width: 75%"><%=rsDetalle("nombreeditorial")%>. edición: <%=rsDetalle("edicion")%>		</td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Asignatura</td>
		<td style="width: 75%" class="bordesup"><%=rsDetalle("nombre_cur")%></td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Justificación</td>
		<td style="width: 75%"><%=rsDetalle("justificacion_jpe")%></td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Cantidad</td>
	  <td style="width: 75%">	
		<input disabled="true"  name="txtcantidad_dpe" id="cant<%=i%>" type="text" value="<%=rsDetalle("cantidad_dpe")%>" class="Cajas" onKeyPress="validarnumero()" size="2" maxlength="3" /></td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Tema de Inv.</td>
		<td style="width: 75%"><%=rsDetalle("tema_dpe")%></td>
	</tr>
</table>
<br>
<%
'	rsDetalle.movenext
'Loop

set rsDetalle=nothing
%>
</form>
</body>

</html>
<%'end if%>