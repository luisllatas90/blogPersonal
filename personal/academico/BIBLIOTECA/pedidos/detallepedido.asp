<!--#include file="../../../../funciones.asp"-->
<%
codigo_ins=cint(request.querystring("codigo_ins"))
codigo_ped=request.querystring("codigo_ped")
codigo_eped=request.querystring("codigo_eped")
codigo_cco=request.querystring("codigo_cco")
tipobandeja=request.querystring("tipobandeja")
tipo=request.querystring("tipo")
cantidad=request.querystring("cantidad")
accion=request.querystring("accion")
codigo_dpe= request.querystring("codigo_dpe")

if accion="guardar" then
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			Obj.Ejecutar "BI_ActualizarCantidadDetalle",false,cantidad,codigo_ins,codigo_ped,codigo_dpe
		Obj.CerrarConexion
	Set obj=nothing
	Response.Write("<script>alert('Se registro el cambio de cantidad satisfactoriamente.')</script>")
end if
%>
<%
	
if codigo_ped<>"" then
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
	
		Obj.AbrirConexion
			Set rsDetalle=Obj.Consultar("BI_ConsultarPedidos","FO","DE",0,codigo_ped)
		Obj.CerrarConexion
		
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
-->
</style>
<script language="JavaScript" src="../../../../private/tooltip.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
	function MarcarItem(item)
	{
		var cant=document.getElementById("cant" + item)
		var mensaje=document.getElementById("mensaje" + item)
	
		VerificaCheckMarcados(frmDetalle.chkcodigo_dpe,frmDetalle.cmdAprobar)
		if (cant.disabled==true){
			mensaje.style.display=""
			cant.disabled=false
		}
		else{
			mensaje.style.display="none"		
			cant.disabled=true
		}
	}
	
	function AprobarPedido(modo)
	{
		if (confirm("Está completamente que desea " + modo)==true){
			frmDetalle.submit()
		}
	}
	
	function MostrarLibros()
	{
		location.href="detallepedido.asp?codigo_ins=<%=codigo_ins%>&codigo_ped=<%=codigo_ped%>&codigo_cco=<%=codigo_cco%>&tipobandeja=<%=tipobandeja%>&codigo_eped=" + frmDetalle.cboestado.value
	}
	
	function GuardarCantidad(codigo_ins,tipo,codigo_ped,cantidad,codigo_dpe){
	//alert (eval("document.all."+cantidad+".value"))	
	document.location.href="detallepedido.asp?accion=guardar&codigo_ins="+codigo_ins+"&codigo_ped="+codigo_ped+"&tipo="+tipo+"&codigo_dpe="+codigo_dpe+"&cantidad="+eval("document.all."+cantidad+".value")
	}
	
	function Aprobar(codigo_ins,tipo,codigo_ped,codigo_dpe,IdLibro,codigo_eped)
	{	if (confirm("Está completamente que desea aprobar el pedido?")==true){
		codigo_eped = codigo_eped +1;
		document.frmDetalle.action="procesar.asp?accion=aprobar&codigo_ped="+codigo_ped+"&codigo_Dpe="+codigo_dpe+"&IdLibro="+IdLibro+"&codigo_eped="+codigo_eped+"&modo=A&codigo_ins="+codigo_ins;
		document.frmDetalle.submit();
		}
	}
	
	function Rechazar(codigo_ins,tipo,codigo_ped,codigo_dpe,IdLibro){
	//alert (eval("document.all."+cantidad+".value"))	
		document.frmDetalle.action="procesar.asp?accion=aprobar&codigo_ped="+codigo_ped+"&codigo_Dpe="+codigo_dpe+"&IdLibro="+IdLibro+"&codigo_eped=6&modo=D&codigo_ins="+codigo_ins; 
		document.frmDetalle.submit();
//document.location.href="procesar.asp?accion=desaprobarpedido&codigo_ins="+codigo_ins+"&codigo_ped="+codigo_ped+"&tipo="+tipo+"&codigo_dpe="+codigo_dpe+"&cantidad="+eval("document.all."+cantidad+".value")
	}	
 	
</script>
</head>

<body style="background-color: #EEEEEE">
<form name="frmDetalle" method="post"  >
<% if tipo = "PE" then 
	  mostrar=0
	  if codigo_ins = 1 then
		'if ucase(rsDetalle("descripcion_Eped")) <> "En Proceso" then 
		if rsDetalle("codigoactual_eped") <> 3 then 
			mostrar =1
		end if
	  else
	  	mostrar=1
	  end if
	  if mostrar =1 then
		%>
<table  border="0" >
  <tr>
    <td><label>
      <input name="CmdAprobar" type="submit" class="aprobar" id="CmdAceptar" value="Aprobar"  onClick="Aprobar(<%=codigo_ins%>,'<%=tipo%>',<%=codigo_ped%>,<%=rsDetalle("codigo_dpe")%>,<%=rsDetalle("IdLibro")%>,<%=rsDetalle("codigoactual_eped")%>);"  codigo_dpe="<%=rsDetalle("codigo_dpe")%>" style="width:80px; font-size:10px; height:20px">
    </label></td>
    <td><label>
	 <input name="CmdRechazar" type="submit" class="eliminar2" id="CmdRechazar" value="Rechazar" onClick="Rechazar(<%=codigo_ins%>,'<%=tipo%>',<%=codigo_ped%>,<%=rsDetalle("codigo_dpe")%>,<%=rsDetalle("IdLibro")%>);" codigo_dpe="<%=rsDetalle("codigo_dpe")%>" style="width:80px; font-weight:bold; font-size:10px; height:20px">
    </label></td>
  </tr>
</table>
<% 
	end if
  end if
 
'Do while not rsDetalle.EOF
if not rsDetalle.EOF then
	i=i+1
%>
<table style="width: 100%" bgcolor="white" class="contornotabla">
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Nº</td>
	  <td style="width: 75%"><%=i%>
	 </td>

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
	<!--
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Editorial</td>
		<td style="width: 75%"><%'=rsDetalle("nombreeditorial")%>. edición: <%=rsDetalle("edicion")%>		</td>
	</tr>
	-->
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Asignatura</td>
		<td style="width: 75%" class="bordesup">
		<%
		Response.Write(rsDetalle("nombre_cur"))
		if ISNULL(rsDetalle("nombre_cur"))=true then
			Response.Write("Es área administrativa")
		else
			Response.Write(rsDetalle("nombre_cur"))
		end if
		%></td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Justificación</td>
		<td style="width: 75%"><%=rsDetalle("justificacion_jpe")%></td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Cantidad</td>
	  <td style="width: 75%">	
		<input <%if tipo<>"PE" then%>  disabled="true" <%end if%> name="txtcantidad_dpe" id="cant<%=i%>" type="text" value="<%=rsDetalle("cantidad_dpe")%>" class="Cajas" onKeyPress="validarnumero()" size="2" maxlength="3" />
		<input <%if tipo<>"PE" then%>  style="visibility:hidden" <%end if%>  name="Button" type="button" class="guardar2" value="Guardar"  onClick="GuardarCantidad(<%=codigo_ins%>,'<%=tipo%>',<%=codigo_ped%>,'cant<%=i%>',<%=rsDetalle("codigo_dpe")%>)" codigo_dpe="<%=rsDetalle("codigo_dpe")%>" ></td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Tema de Inv.</td>
		<td style="width: 75%"><%=rsDetalle("tema_dpe")%></td>
	</tr>
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Precio Unit</td>
		<td style="width: 75%"><%response.write(rsDetalle("moneda_ped") & " " & rsDetalle("precio_ped"))%></td>
	</tr>	
	<tr>
		<td style="width: 15%" class="usatLinkCeldaGris">Total</td>
		<td style="width: 75%">
		<%		Total=0
				subTotal=iif(trim(rsDetalle("precio_ped"))="",0,rsDetalle("precio_ped"))
				if rsDetalle("moneda_ped")="$" then
					'Response.Write(formatNumber(cdbl(TipoCambioDolar)*Cdbl(subTotal),2))
					subtotal_=cdbl(TipoCambioDolar)*Cdbl(subTotal)
				else
					if rsDetalle("moneda_ped")="E" then
						'Response.Write(formatNumber(cdbl(TipoCambioEuro)*Cdbl(subTotal),2))
						subtotal_=cdbl(TipoCambioEuro)*Cdbl(subTotal)
					else
						'Response.Write(formatNumber(subTotal,2))
						subtotal_=subTotal
					end if
				end if			
				Total=Total+cdbl(subtotal_)
				response.Write(formatnumber(Total,2))
		%>		</td>
	</tr>
	<% if codigo_ins <> 0 then %>
	<tr>
	  <td style="width: 15%" class="usatLinkCeldaGris">Solicitante</td>
	  <td style="width: 75%"><%=rsDetalle("Personal")%></td>
    </tr>
		<tr>
	  <td style="width: 15%" class="usatLinkCeldaGris">Estado</td>
	  <td style="width: 75%; color:#0000FF; font-weight:bold"><%=ucase(rsDetalle("descripcion_Eped"))%></td>
    </tr>
	<% end if %>
</table>
<br>
<%
	'rsDetalle.movenext
'loop
end if

set rsDetalle=nothing
%>
</form>
</body>

</html>
<%end if%>