<!--#include file="../../../../funciones.asp"-->
<script >
function Cerrar(){
	alert('Se ha registrado el libro solicitado en la orden de compra temporal.')
	window.opener.location.reload()
	window.close()
}
</script>
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
titulo=left(Request.Form("txtTitulo"),20)
'---------------------------
accion=request.querystring("accion")
idproveedor=request.querystring("idproveedor")
moneda=request.querystring("moneda")
precio=request.querystring("precio")
codigo_per=session("codigo_usu")
'cantidad =request.querystring("cantidad")



if accion="guardar" then
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			Response.Write("Registrando orden de compra ...")
			Obj.Ejecutar "BI_RegistrarTmpCompra",false,codigo_dpe,idproveedor,moneda,precio,cantidad,codigo_per
		Obj.CerrarConexion
	Set obj=nothing	
	Response.Write("<SCRIPT>Cerrar()</SCRIPT>")
end if
%>
<%
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsDetalle=Obj.Consultar("BI_ConsultarPedidos","FO","VD",0,codigo_dpe)
		Obj.CerrarConexion
	Set obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>SOLICITAR COMPRA DE PEDIDO</title>
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
function ComprarLibro(codigo_dpe){
	var proveedor=document.all.cboProveedor_cat.value
	var moneda=document.all.cbomoneda_ped.value
	var precio=document.all.txtimporte.value
	var cantidad= document.all.txtCantidad.value
//	alert(moneda)
	if (proveedor==-2){
		alert("Por favor seleccione un proveedor.")
		document.all.cboProveedor_cat.focus()
		return(false)		
	}
location.href="ordendecompradePedido.asp?accion=guardar&idproveedor="+proveedor+"&moneda="+moneda+"&precio="+precio+"&codigo_dpe="+codigo_dpe+"&cantidad="+cantidad
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
	  <td style="width: 5%" rowspan="15" valign="top" bgcolor="#FFFFCC">&nbsp;</td>
	  <td colspan="2" class="usatLinkCeldaGris" style="width: 15%"><span class="Estilo2">SOLICITAR COMPRA DEL PEDIDO<br>
      </span><span class="Estilo4"><span class="Estilo3"><%=rsDetalle("titulo")%></span></span></td>

	<%
if  titulo ="" then
	titulo=left(rsDetalle("titulo"),20)
end if
%>
	<tr>
	  <td style="width: 15%" class="usatLinkCeldaGris"><p class="style1">&nbsp;</p>      </td>
	  <td style="width: 75%"><label></label></td>
    </tr>
	<tr>
	  <td class="style1 usatLinkCeldaGris" style="width: 15%"><strong>Proveedor</strong></td>
	  <td style="width: 75%">
	<%

		Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
			Obj.AbrirConexion
				Set rsProveedor=Obj.Consultar("PED_ConsultarEmpresaProveedora","FO","TO",0)		
			Obj.CerrarConexion
		Set obj=nothing
	call llenarlista("cboProveedor_cat","",rsProveedor,"Idproveedor","NombreProveedor",Idproveedor,"Seleccione el proveedor","","") %></td>
	</tr>
	<tr>
		<%if codigo_ins>0 and tipobandeja="E" then%>		
		<%end if%>
		<td class="style1 usatLinkCeldaGris" style="width: 15%"><strong>Cantidad</strong></td>
		<td style="width: 75%"><strong>
		  <input name="txtCantidad" type="text" id="txtCantidad" value="<%=rsDetalle("cantidad_dpe")%>" size="5" maxlength="2">
		</strong></td>
	</tr>
	<tr>
	  <td class="style1 usatLinkCeldaGris" style="width: 15%"><strong>Precio</strong></td>
	  <td style="width: 75%">
	  <%
	  cbomoneda_ped=rsDetalle("moneda_Ped")
	  %>
	    <select name="cbomoneda_ped" class="Cajas">
          <option value="S" <%if cbomoneda_ped="S" then %> selected="selected" <%end if%>>S/.</option>
          <option value="$" <%if cbomoneda_ped="$" then %> selected="selected" <%end if%>>$</option>
          <option value="E" <%if cbomoneda_ped="E" then %> selected="selected" <%end if%>>E</option>
        </select>


	    <input name="txtimporte" type="text" id="txtimporte" value="<%=rsDetalle("precio_Ped")%>" size="6">
      (Importe)</td>
    </tr>
	<tr>
	  <td style="width: 15%" class="usatLinkCeldaGris">&nbsp;</td>
	  <td style="width: 75%">
	    <input  name="TxtCotizar" type="button" class="agregar2" id="TxtCotizar"  onClick="ComprarLibro(<%=codigo_dpe%>)" value="    Comprar" >
	 </td>
    </tr>
</table>
<p>
<%
set rsDetalle=nothing
%>
</p>
</form>
</body>

</html>
