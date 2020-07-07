<!--#include file="../../../../funciones.asp"-->
<%
codigo_ins=request.querystring("codigo_ins")
codigo_eped=request.querystring("codigo_eped")
codigo_usu= session("codigo_usu")' Request.QueryString("id") '
tipobandeja=request.querystring("tipobandeja")
menu=request.querystring("menu")
tipo=Request.QueryString("tipo")
Idproveedor=Request.QueryString("Idproveedor")
accion=Request.QueryString("accion")
codigo_oco= Request.QueryString("codigo_oco")

if Idproveedor="" then Idproveedor=-1
fecha = Request.QueryString("fecha")
%>
<script>
function Actualizar(Idproveedor,tipo){
	document.location.href="lstcompras.asp?Idproveedor="+Idproveedor+"&tipo="+tipo
} 

function LlenarListaFechas(idproveedor){
	location.href="lstOrdenCompra.asp?idproveedor=" + idproveedor
}

function LlenarListaDelCatalogo(fecha,idproveedor){
	location.href="lstOrdenCompra.asp?idproveedor=" + idproveedor + "&fecha=" + fecha
//alert (fecha)
}
function LlenarOrdenesCompra(codigo_oco,idproveedor,fecha){
	location.href="lstOrdenCompra.asp?idproveedor=" + idproveedor + "&fecha=" + fecha+"&codigo_oco="+codigo_oco
//alert (fecha)
}

</script>
<%
	Response.ContentType = "application/msexcel"
	Response.AddHeader "Content-Disposition","attachment;filename=ordencompra.xls"
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<title>Lista de Pedidos</title>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/validarpagina.js"></script>
<script language="JavaScript" src="../private/validarpedido.js"></script>

<script>

function VerDatos(codigo_dpe){
var pagina ='datosdepedido.asp?codigo_dpe='+codigo_dpe
AbrirPopUp(pagina,'300','500')
}

function Comprar(codigo_dpe){
var pagina ='ordendecompradePedido.asp?codigo_dpe='+codigo_dpe
AbrirPopUp(pagina,'500','700')
}

</script>
<style type="text/css">
<!--
.Estilo2 {
	color: #000000;
	font-weight: bold;
	font-size: 12px;
}
.Estilo3 {color: #000000}
.Estilo4 {
	color: #FFFFFF;
	font-weight: bold;
}
.Estilo5 {
	font-size: 10pt;
	font-weight: bold;
}
.Estilo6 {color: #990000}
.Estilo7 {font-size: 12px}
-->
</style>
</head>
<body>

<%
Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsProveedor=Obj.Consultar("PED_ConsultarEmpresaProveedora","FO","TO",0)

	Obj.CerrarConexion
Set obj=nothing
  
%>
<br>
 <%
'--CONSULTAR TIPO DE CAMBIO
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			TipoCambioDolar=Obj.Ejecutar("BI_CalcularTipoCambio",TRUE,"TC","D",0,0)
		Obj.CerrarConexion
	Set obj=nothing

	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			TipoCambioEuros=Obj.Ejecutar("BI_CalcularTipoCambio",TRUE,"TC","E",0,0)
		Obj.CerrarConexion
	Set obj=nothing	
'-----------------------
%>

<% if codigo_oco>0 then
Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsOComp=Obj.Consultar("PED_ConsultarOrdenesDeCompra","FO","TC",codigo_oco,"")
	Obj.CerrarConexion
Set obj=nothing

%>
<table width="100%" border="0">
  <tr>
    <td colspan="3"><span class="Estilo2">Orden de Compra </span></td>
  </tr>
  <tr>
    <td width="9%"><span class="Estilo7"></span></td>
    <td width="1%"><span class="Estilo7"></span></td>
    <td width="90%"><span class="Estilo7"></span></td>
  </tr>
  <tr>
    <td colspan="3"><span class="Estilo2">Proveedor:&nbsp;<%=rsOComp("nombreproveedor")%>&nbsp;&nbsp;&nbsp;&nbsp;Fecha:&nbsp;<%=rsOComp("fecha_oco")%></span></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td colspan="3"><span class="Estilo3">Tipo Cambio US$: S/.<%=formatnumber(TipoCambioDolar,2)%><br>
Tipo Cambio Euros: S/.<%=formatnumber(TipoCambioEuros,2)%></span></td>
  </tr>
  <tr>
    <td colspan="3"><table width="100%" border="0" cellpadding="3" cellspacing="0" class="contornotabla">
      <tr>
        <td width="3%" align="center" bgcolor="#26758c" class="bordeizqinf"><span class="Estilo4">N&ordm;</span></td>
        <td width="52%" align="center" bgcolor="#26758c" class="bordeizqinf"><span class="Estilo4">T&iacute;tulo</span></td>
        <td width="17%" align="center" bgcolor="#26758c" class="bordeizqinf"><span class="Estilo4">Precio
          Unitario</span></td>
        <td width="14%" align="center" bgcolor="#26758c" class="bordeizqinf"><span class="Estilo4">Cantidad</span></td>
        <td width="14%" align="center" bgcolor="#26758c" class="bordeizqinf"><p class="Estilo4">Precio Unit.
          S/.</p></td>
        <td width="14%" align="center" bgcolor="#26758c" class="bordeizqinf"><span class="Estilo4">Subtotal 
          S/.</span></td>
      </tr>
      <%
		Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
			Obj.AbrirConexion
				Set rsOComp=Obj.Consultar("PED_ConsultarOrdenesDeCompra","FO","DC",codigo_oco,"")
			Obj.CerrarConexion
		Set obj=nothing	  
	  k=0
	  do while not rsOComp.EOF
	  k=k+1
	  %>
      <tr>
        <td valign="top" class="bordeizqinf Estilo3"><%=k%></td>
        <td align="left" valign="top" class="bordeizqinf Estilo3"><%=rsOComp("TITULO")%><br>
              <span class="cursoM">
              <%Response.Write(rsOComp("NombreAutor"))%>
              <br>
              <span class="Estilo6">
              <%
			  
			  Response.Write(rsOComp("NombreEditorial") &", "&rsOComp("Edic") &" "&rsOComp("Abreviatura") &"; "&rsOComp("FechaPublicacion") &"; "&rsOComp("NombreLugar") )
			  %>
            </span></span></td>
        <td align="right" valign="top" class="bordeizqinf Estilo3"><%=rsOComp("moneda_doc")%>&nbsp;&nbsp;
              <%Response.Write(formatnumber(cdbl(rsOComp("precio_doc")),2))
			moneda=rsOComp("moneda_doc")
			precio=cdbl(rsOComp("precio_doc"))
			%>
          &nbsp;&nbsp;&nbsp;</td>
        <td align="center" valign="top" class="bordeizqinf Estilo3"><%=rsOComp("cantidad_doc")%></td>
        <td align="right" valign="top" bgcolor="#FFFFCC" class="bordeizqinf Estilo3"><%
				if moneda="$" then
					Response.Write(formatNumber(cdbl(TipoCambioDolar)*Cdbl(precio),2))
				else
					if moneda="E" then					
						Response.Write(formatNumber(cdbl(TipoCambioEuros)*Cdbl(precio),2))
					else
						Response.Write(formatNumber(precio,2))
					end if
				end if
				%>
        </td>
        <td align="right" valign="top" bgcolor="#FFFFCC" class="bordeizqinf Estilo3"><%
				if moneda="$" then
					Response.Write(formatNumber(cdbl(TipoCambioDolar)*Cdbl(precio)*(rsOComp("cantidad_doc")),2))
					subtotal=cdbl(TipoCambioDolar)*Cdbl(precio)* rsOComp("cantidad_doc")
				else
					if moneda="E" then
						Response.Write(formatNumber(cdbl(TipoCambioEuros)*Cdbl(precio)*(rsOComp("cantidad_doc")),2))
						subtotal=cdbl(TipoCambioEuros)*Cdbl(precio)* rsOComp("cantidad_doc")
					else
						Response.Write(formatNumber(precio* rsOComp("cantidad_doc"),2))
						subtotal=precio * rsOComp("cantidad_doc")
					end if
				end if			
				Total=Total+subtotal		
				%></td>
        <%subtotal=formatnumber(cdbl(rsOComp("precio_doc"))*cdbl(rsOComp("cantidad_doc")),2)%>
        </tr>
      <%
	  rsOComp.MoveNext
	  loop%>
      <tr>
        <td colspan="5" bgcolor="#FFFFCC" class="Estilo3 bordeizqinf"><span class="Estilo5">Total S/. </span></td>
        <td align="right" bgcolor="#FFFFCC" class="bordeizqinf Estilo3 Estilo7"><div align="right" class="cursoM"><strong><%=formatnumber(total,2)%></strong></div></td>
      </tr>
    </table></td>
  </tr>
</table>
<%end if%>
</body>
</html>
