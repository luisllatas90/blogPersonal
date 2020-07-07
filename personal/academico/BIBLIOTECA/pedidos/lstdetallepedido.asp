<!--#include file="../../../../funciones.asp"-->
<%
codigo_ped=session("codigo_ped")
total=0

'--CONSULTAR TIPO DE CAMBIO
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			TipoCambioDolar = Obj.Ejecutar("BI_CalcularTipoCambio",TRUE,"TC","D",0,0)
		Obj.CerrarConexion
	Set obj=nothing
	
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			TipoCambioEuro = Obj.Ejecutar("BI_CalcularTipoCambio",TRUE,"TC","E",0,0)
		Obj.CerrarConexion
	Set Obj=nothing	
'-----------------------

if codigo_ped<>"" then
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsDetalle=Obj.Consultar("ConsultarPedidoBibliografico","FO",9,codigo_ped,0,0)
		Obj.CerrarConexion
	Set Obj=nothing
	
	if Not(rsDetalle.BOF and rsDetalle.EOF) then
		HayReg=true
		total=rsDetalle.recordcount
	end if
end if
%>

<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Libros del Pedido</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../private/validarpedido.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/validarpagina.js"></script>
<style type="text/css">
<!--
.Estilo1 {
	font-size: 10pt;
	color: #000000;
	font-weight: bold;
}
.Estilo2 {color: #000000}
-->
</style>
</head>
<body style="margin: 0" oncontextmenu="return event.ctrlKey">
<input type="hidden" id="txtelegido" value="0">
Tipo Cambio US$: 
<% Response.Write(TipoCambioDolar) %><br>
Tipo Cambio  Euros: 
<% Response.Write(TipoCambioEuro) %>
<table width="100%" cellpadding="1" cellspacing="0" bgcolor="white" class="contornotabla">
	<tr class="usatCeldaTitulo" height="5%">
		<th width="50" style="width: 5%">#</th>
		<th style="width: 45%">Título</th>
		<th style="width: 30%">Autor</th>
		<th style="width: 8%">Cant.</th>
		<th style="width: 8%">Precio</th>
		<th style="width: 8%">SubTotal</th>
	    <th style="width: 8%">Precio<br>
        S/.</th>
	    <th style="width: 8%">SubTotal<br>
	      S/.</th>
    </tr>
<%
if HayReg=true then
	Do while not rsDetalle.EOF
		i=i+1
		%>	
	<tr class="Sel" Typ="Sel" id="fila<%=rsDetalle("codigo_dpe")%>" class="piepagina" valign="top" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onclick="SeleccionarDetalle(this)" >	
		<td style="width: 8%"><%=i%></td>
		<td style="width: 22%"><%=rsDetalle("titulo")%></td>
		<td style="width: 18%"><%=rsDetalle("nombreautor")%>&nbsp;</td>
		<td align="center" style="width: 10%"><%=rsDetalle("Cantidad_Dpe")%>&nbsp;</td>
		<td align="right" style="width: 7%">
		  <table width="100%">
				<tr>
					<td width="20%" align="left"><%=rsDetalle("moneda")%></td>
					<td width="80%" align="right"><%=formatNumber(rsDetalle("preciounitario"),2)%></td>
				</tr>
		  </table>		</td>
		<td align="right" style="width: 7%">
		  <table width="100%">
				<tr>
					<td width="20%" align="left"><%=rsDetalle("moneda")%></td>
					<td width="80%" align="right"><%=formatNumber(rsDetalle("subtotal"),2)%></td>
				</tr>
		  </table>		
		</td>
	    <td align="right" bgcolor="#FFFFCC" style="width: 7%"><span class="Estilo2">
	      <%
				precioUnit=iif(trim(rsDetalle("preciounitario"))="",0,rsDetalle("preciounitario"))
				if rsDetalle("moneda")="$" then
					Response.Write(formatNumber(cdbl(TipoCambioDolar)*Cdbl(precioUnit),2))
				else
					if rsDetalle("moneda")="E" then
						Response.Write(formatNumber(cdbl(TipoCambioEuro)*Cdbl(precioUnit),2))						
					else
						Response.Write(formatNumber(precioUnit,2))
					end if
				end if
				%>
	    </span></td>
	    <td align="right" bgcolor="#FFFFCC" style="width: 7%"><span class="Estilo2">
	      <%
				subTotal=iif(trim(rsDetalle("subtotal"))="",0,rsDetalle("subtotal"))
				if rsDetalle("moneda")="$" then
					Response.Write(formatNumber(cdbl(TipoCambioDolar)*Cdbl(subTotal),2))
					subtotal_=cdbl(TipoCambioDolar)*Cdbl(subTotal)
				else
					if rsDetalle("moneda")="E" then
						Response.Write(formatNumber(cdbl(TipoCambioEuro)*Cdbl(subTotal),2))
						subtotal_=cdbl(TipoCambioEuro)*Cdbl(subTotal)
					else
						Response.Write(formatNumber(subTotal,2))
						subtotal_=subTotal
					end if
				end if			
				Total=Total+cdbl(subtotal_)
				%>
	    </span></td>
    </tr>
	<%rsDetalle.movenext
	Loop
end if
%>
</table>

<table width="100%" border="0" cellspacing="2" cellpadding="2">
  <tr>
    <td width="91%" align="right"><span class="Estilo1">Total S/. </span></td>
    <td width="9%" align="right" bgcolor="#FFFF99" class="contornotabla_azul">
	<b> <span style="color:#000066; height:14px">
	<%=formatnumber(Total,2)%>
	</span></b>
	</td>
	
  </tr>
</table>
<p>&nbsp; </p>
</body>
</html>
