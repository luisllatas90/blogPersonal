<!--#include file="../../../../funciones.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title>Lista de Pedidos</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/validarpagina.js"></script>
<script language="JavaScript" src="../private/validarpedido.js"></script>
<%
codigo_ins=request.querystring("codigo_ins")
codigo_eped=request.querystring("codigo_eped")
codigo_usu= session("codigo_usu")' Request.QueryString("id") '
tipobandeja=request.querystring("tipobandeja")
menu=request.querystring("menu")
tipo=Request.QueryString("tipo")
Idproveedor=Request.QueryString("Idproveedor")
accion=Request.QueryString("accion")
if Idproveedor="" then Idproveedor=-1
%>
<script>
function Actualizar(Idproveedor,tipo){
	document.location.href="lstcompras.asp?Idproveedor="+Idproveedor+"&tipo="+tipo
} 
</script>
<script>

function VerDatos(codigo_dpe){
var pagina ='datosdepedido.asp?codigo_dpe='+codigo_dpe
AbrirPopUp(pagina,'300','500')
}

function Comprar(codigo_dpe){
var pagina ='ordendecompradePedido.asp?codigo_dpe='+codigo_dpe
AbrirPopUp(pagina,'500','700')
}

function GenerarOrdenDeCompra(proveedor,tipo){
	if (confirm("¿Desea generar la orden de compra?")==true){
	location.href="lstCompras.asp?accion=generar&idproveedor="+proveedor+"&tipo="+tipo
	}
}
</script>
<style type="text/css">
<!--
.Estilo1 {
	font-size: 14pt;
	font-weight: bold;
}
.Estilo2 {
	color: #000000;
	font-weight: bold;
	font-size: 10pt;
}
.Estilo3 {color: #000000}
.Estilo4 {
	color: #000066;
	font-size: 12px;
	font-weight: bold;
}
.Estilo5 {
	color: #FFFFFF;
	font-weight: bold;
}
.Estilo6 {color: #990000}
-->
</style>
</head>
<span class="Estilo1">COMPRA DE MATERIAL BIBLIOGRÁFICO <BR></span> 
<% if tipo="BO" then%>

<table width="50%" border="0">
  <tr>
    <td width="30%">Proveedor:</td>
	<td width="70%">
	<%
	if accion="generar" then
		Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")	
		Obj.AbrirConexion
			Obj.Ejecutar "BI_RegistrarOrdenDeCompra",false,Idproveedor
		Obj.CerrarConexion		
	end if
	%>
	<%
		Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
			Obj.AbrirConexion
				Set rsProveedor=Obj.Consultar("PED_ConsultarEmpresaProveedora","FO","TO",0)		
			Obj.CerrarConexion
		Set obj=nothing
	call llenarlista("cboProveedor_cat","Actualizar(this.value,'"&tipo&"')",rsProveedor,"Idproveedor","NombreProveedor",Idproveedor,"Seleccione el proveedor","","") %>
&nbsp;
	</td>
  </tr>
</table>
<%end if%>
<%
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
		if tipo="SC" then
			Set rsPedidos=Obj.Consultar("BI_ConsultarCompras","FO",tipo,0)
		else	
			Set rsPedidos=Obj.Consultar("BI_ConsultarCompras","FO",tipo,Idproveedor)
		end if
		Obj.CerrarConexion
	Set obj=nothing

	if Not(rsPedidos.BOF and rsPedidos.EOF) then
		HayReg=true
	end if
%>


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

<body>
<% if idproveedor>0 then %>
<table width="100%" border="0" cellpadding="4" cellspacing="0" class="contornotabla">
  <tr>
    <td bordercolor="#000000" bgcolor="#F0F0F0"><input name="cmdGenerar" type="button" class="guardar_prp" id="cmdGenerar" value="    Generar Orden Compra" onClick="GenerarOrdenDeCompra(<%=idproveedor%>,'<%=tipo%>')"></td>
  </tr>
</table>
<%end if%>
<label></label>
<br>
<%if HayReg=true then%>
<input type="hidden" id="txtelegido" value="0">
<br>
Tipo Cambio US$: S/.<%=formatnumber(TipoCambioDolar,2)%> <br>
Tipo Cambio Euros: S/.<%=formatnumber(TipoCambioEuros,2)%>
<table width="100%" height="88%">
	<tr class="contornotabla" id="trLista" >
		<TD width="100%" valign="top">
		<table width="100%" border="0" cellpadding="2" cellspacing="0" class="contornotabla">
          <tr align="center" bgcolor="#26758c">
            <td width="2%" ><span class="Estilo5">N&ordm;</span></td>
            <td width="3%" ><span class="Estilo5">ID</span></td>
            <td width="11%" ><span class="Estilo5">Fecha Registro </span></td>
            <td width="30%" ><span class="Estilo5">T&iacute;tulo /Autor</span></td>
            <td width="31%" ><span class="Estilo5">&Aacute;rea/Solicitante</span></td>
            <td width="8%"><span class="Estilo5">Precio</span></td>
            <td width="6%"><span class="Estilo5">Cantidad</span></td>
            <td width="6%"><span class="Estilo5">Precio<br>
            S/.</span></td>
            <td width="6%" ><span class="Estilo5">Subtotal<br>
            S/.</span></td>
            <td width="9%"><span class="Estilo5">Acci&oacute;n</span></td>
          </tr>
         <% 
		 i=0
		 do while not rsPedidos.EOF
		 i=i+1
		 %>
		  
            <tr>
			<td align="right" class="bordederinf Estilo3" ><%=i%></td>
            <td align="center"  class="bordederinf Estilo3"><%=RsPedidos("Codigo_Dpe")%></td>
            <td align="center" class="bordederinf Estilo3"><%=RsPedidos("FechaReg_Ped")%></td> 
            <td class="bordederinf Estilo3"><%Response.Write(ucase(RsPedidos("Titulo")))%>
              <br>
             
              <span class="cursoM">
              <%Response.Write(RsPedidos("NombreAutor"))%>             
              <br>
              <span class="Estilo6">
              <%
			  
			  Response.Write(RsPedidos("NombreEditorial") &", "&RsPedidos("Edic") &" "&RsPedidos("Abreviatura") &"; "&RsPedidos("FechaPublicacion") &"; "&RsPedidos("NombreLugar") )
			  %>
              </span></span></td>
            <td class="bordederinf Estilo3"><%Response.Write(RsPedidos("descripcion_Cco"))%>
              <br>
              <%Response.Write(RsPedidos("Nombres"))%></td>
            <td align="center" class="bordederinf Estilo3">
			<%
			IF tipo="SO" then
				Response.Write(RsPedidos("moneda_Ped") &" "& RsPedidos("precio_Ped"))
				moneda=RsPedidos("moneda_Ped")
				precio=RsPedidos("precio_Ped")
			end if
			
			IF tipo="BO" then
				Response.Write(RsPedidos("MONEDA_TCO") &" "& RsPedidos("precio_tco"))
				moneda=RsPedidos("MONEDA_TCO")					
				precio=	cdbl(RsPedidos("precio_tco"))
			end if
						
			%></td>  
            <td align="center" class="bordederinf Estilo3">
			<%
			IF tipo="SO" then			
				Response.Write(RsPedidos("Cantidad_Dpe"))
			end if
			IF tipo="BO" then			
				Response.Write(RsPedidos("Cantidad_tco"))
			end if			
			%>			</td>
            <td align="center" bgcolor="#FFFFCC" class="bordederinf Estilo3">
				<%
				if moneda="$" then
					Response.Write(formatNumber(cdbl(TipoCambioDolar)*Cdbl(precio),2))
				else
					if moneda="E" Then
						Response.Write(formatNumber(cdbl(TipoCambioEuros)*Cdbl(precio),2))
					else
						Response.Write(formatNumber(cdbl(precio),2))
					end if
				end if
				%>
			</td>
            <td align="center" bgcolor="#FFFFCC" class="bordederinf Estilo3">
				<%
				if moneda="$" then
					Response.Write(formatNumber(cdbl(TipoCambioDolar)*Cdbl(precio)*(RsPedidos("Cantidad_Dpe")),2))
					subtotal=cdbl(TipoCambioDolar)*Cdbl(precio)* RsPedidos("Cantidad_Dpe")
				else
					if moneda="E" Then
						Response.Write(formatNumber(cdbl(TipoCambioEuros)*Cdbl(precio)*(RsPedidos("Cantidad_Dpe")),2))
						subtotal=cdbl(TipoCambioEuros)*Cdbl(precio)* RsPedidos("Cantidad_Dpe")
					else					
						Response.Write(formatNumber(cdbl(precio)* RsPedidos("Cantidad_Dpe"),2))
						subtotal=cdbl(precio) * RsPedidos("Cantidad_Dpe")
					end if
				end if			
				Total=Total+subtotal		
				%>			</td>
            <td align="center" valign="top" class="bordederinf"><table width="100%" border="0" cellpadding="2" cellspacing="2">
              <tr style="cursor:hand"  id="<%=RsPedidos("Codigo_Dpe")%>" onClick="VerDatos(<%=RsPedidos("Codigo_Dpe")%>)">
                <td width="30%" class="Estilo3"><img src="../../../../images/credito.gif" width="15" height="12"></td>
                <td width="70%" class="Estilo3"><strong>Datos</strong></td>
              </tr>
			  <%if tipo = "SO" then%>
              <tr  style="cursor:hand"  id="<%=RsPedidos("Codigo_Dpe")%>" onClick="Comprar(<%=RsPedidos("Codigo_Dpe")%>)">
                <td class="Estilo3"><img src="../../../../images/editar.gif" width="18" height="13"></td>
                <td class="Estilo3"><strong>Comprar</strong></td>
              </tr>
			  <%end if%>
            </table></td>
          </tr>
	  
		  <%
		  rsPedidos.MoveNext
		  loop%>
            <tr>
              <td colspan="8" align="left" class="bordederinf Estilo2" >Total S/. </td>
              <td align="center" bgcolor="#FFFFCC" class="bordederinf Estilo4"><%=formatnumber(Total,2)%></td>
              <td align="center" valign="top" class="bordederinf">&nbsp;</td>
            </tr>			  
      </table>	  </TD>
	</tr>
</table>
<%else%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se 
	han encontrado Pedidos Bibliográficos registrados</h5>
<%end if%>
</body>
</html>
