<!--#include file="../../../../funciones.asp"-->
<script >
function Cerrar(){
	alert('Se ha registrado la cotización del libro solicitado.')
	window.opener.location.reload()
	window.close()
}
</script>
<%
'Response.Write(session("codigo_usu"))

codigo_ins=request.querystring("codigo_ins")
codigo_ped=request.querystring("codigo_ped")
codigo_eped=request.querystring("codigo_eped")
codigo_cco=request.querystring("codigo_cco")
tipobandeja=request.querystring("tipobandeja")
tipo=request.querystring("tipo")
cantidad=request.querystring("cantidad")
accion=request.querystring("accion")
codigo_dpe = request.querystring("codigo_dpe")
titulo=left(Request.Form("txtTitulo"),20)
'---------------------------
accion=request.querystring("accion")
idproveedor=request.querystring("idproveedor")
moneda=request.querystring("moneda")
precio=request.querystring("precio")
cantidad=Request.QueryString("cantidad")
codigo_per=session("codigo_usu")



if accion="guardar" then
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			Obj.Ejecutar "BI_RegistrarCotizacion",false,codigo_dpe,idproveedor,moneda,precio,cantidad,session("codigo_usu")
		Response.Write(codigo_dpe)
		Obj.CerrarConexion
	Set obj=nothing	
Response.Write("<SCRIPT>Cerrar()</SCRIPT>")
end if
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
<title>COTIZACI&Oacute;N DE PEDIDO</title>
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
.Estilo3 {
	color: #009900;
	font-size: 10px;
	font-weight: bold;
}
.Estilo4 {font-size: 14pt; color: #000000;}
-->
</style>
<script language="JavaScript" src="../../../../private/tooltip.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
function CotizarLibro(proveedor,moneda,codigo_dpe,precio){
	var cantidad=document.all.txtCantidad.value
	location.href="cotizaciondePedido.asp?accion=guardar&idproveedor="+proveedor+"&moneda="+moneda+"&precio="+parseFloat(precio)+"&codigo_dpe="+parseInt(codigo_dpe)+"&cantidad="+cantidad
//	alert(parseFloat(precio))
}
</script>
</head>

<body style="background-color: #EEEEEE">
<form name="frmDetalle" method="post" action="cotizaciondePedido.asp?codigo_dpe=<%=codigo_dpe%>">
<%
rsDetalle.MoveFirst
%>
<table style="width: 100%" bgcolor="white" class="contornotabla">
	<tr>
	  <td style="width: 5%" rowspan="13" valign="top" bgcolor="#FFFFCC">&nbsp;</td>
	  <td colspan="2" class="usatLinkCeldaGris" style="width: 15%"><span class="Estilo2">COTIZACI&Oacute;N DEL PEDIDO<br>
	      </span><span class="Estilo4"><span class="Estilo3"><%=rsDetalle("titulo")%></span></span></td>
    </tr>
	<tr>
	  <td class="usatLinkCeldaGris" style="width: 15%"><span class="style1"><strong>Cantidad:</strong></span></td>
	  <td style="width: 75%"><%=rsDetalle("cantidad_dpe")%> ejemplares pedidos </td>
    </tr>
	<%
if  titulo ="" then
	titulo=left(rsDetalle("titulo"),20)
end if
%>
	<tr>
	  <td style="width: 15%" class="usatLinkCeldaGris"><p class="style1"><strong>T&iacute;tulo:</strong></p>      </td>
	  <td style="width: 75%"><label>
	    <input name="txtTitulo" type="text" id="txtTitulo" value="<%=titulo%>" size="60">
	  </label></td>
    </tr>
	<tr>
	  <td style="width: 15%" class="usatLinkCeldaGris">&nbsp;</td>
	  <td style="width: 75%"><input name="CmdBuscar" type="submit" class="buscar_prp_small" id="CmdBuscar" value="       Buscar"></td>
    </tr>
	<tr>
		<%if codigo_ins>0 and tipobandeja="E" then%>		
		<%end if%>
		<td style="width: 15%" class="usatLinkCeldaGris">&nbsp;</td>
		<td style="width: 75%">&nbsp;</td>
	</tr>
</table>
<p align="right"><br>
    <%


'if codigo_dpe<>"" then
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsCatalogo=Obj.Consultar("PED_ConsultarCatalogosAnteriores","FO","TC",0,titulo,20)
		Obj.CerrarConexion
	Set obj=nothing
%>
    <strong>Cantidad de ejemplares a Cotizar
    <input name="txtCantidad" type="text" id="txtCantidad" value="<%=rsDetalle("cantidad_dpe")%>" size="5" maxlength="2">
  </strong></p>
<table width="100%" border="0" cellpadding="0" cellspacing="0" class="contornotabla">
  <tr>
    <td width="3%" align="center" valign="middle" bgcolor="#FFFF99" class="bordeizqinf"><span class="style2">N&ordm;</span></td>
    <td width="61%" align="center" valign="middle" bgcolor="#FFFF99" class="bordeizqinf"><span class="style2">Proveedor</span></td>
    <td width="8%" align="center" valign="middle" bgcolor="#FFFF99" class="bordeizqinf"><span class="style2">Precio </span></td>
    <td width="8%" align="center" valign="middle" bgcolor="#FFFF99" class="bordeizqinf"><span class="style2"> Descto.</span></td>
    <td width="11%" align="center" valign="middle" bgcolor="#FFFF99" class="bordeizqinf"><span class="style2">Cotizar</span></td>
  </tr>
  <%
  
  for i=0 to rsCatalogo.RecordCount-1
  
  %>
  <tr>
    <td align="right" valign="top" bgcolor="#FFFFFF" class="bordeizqinf"><%=i+1%>.-</td>
    <td bgcolor="#FFFFFF" class="bordeizqinf"><span class="azul">.<%=rsCatalogo("nombreproveedor")%></span><br> <%=rsCatalogo("titulo_cat")%> </td>
    <td bgcolor="#FFFFFF" class="bordeizqinf"><b><%Response.Write(rsCatalogo("moneda_cat") & " " & rsCatalogo("Preciounit_cat"))%></B></td>
    <td bgcolor="#FFFFFF" class="bordeizqinf"><b>
      <%Response.Write(rsCatalogo("moneda_cat") & " " & rsCatalogo("Preciototal_cat"))%>
    </B></td>
    <td align="center" bgcolor="#FFFFFF" class="bordeizqinf">
      <input name="TxtCotizar" id="<%=rsCatalogo("idproveedor")%>" type="button" class="agregar5" id="TxtCotizar"  onClick="CotizarLibro(<%=rsCatalogo("idproveedor")%>,'<%=rsCatalogo("moneda_cat")%>',<%Response.Write(codigo_dpe)%>,'<%=replace(rsCatalogo("Preciounit_cat"),",",".")%>')">    </td>
  </tr>
  <%
  rsCatalogo.Movenext
  next%>
</table>
<strong><span class="Estilo3">T&Iacute;TULO del libro solicitado en letra VERDE <br>
</span></strong><span class="cursoM"><strong>Nombre del PROVEEDOR en letra AZUL </strong>
</span>
<p>
  <%
	set rsDetalle=nothing
  %>
</p>
</form>
</body>

</html>
<%'end if%>