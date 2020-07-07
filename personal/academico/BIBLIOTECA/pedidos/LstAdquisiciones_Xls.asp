<!--#include file="../../../../funciones.asp"-->
<%
'Response.Write(session("codigo_usu"))
codigo_ins=request.querystring("codigo_ins")
codigo_eped=request.querystring("codigo_eped")
codigo_usu= session("codigo_usu")' Request.QueryString("id") '
tipobandeja=request.querystring("tipobandeja")
menu=request.querystring("menu")
tipo=Request.QueryString("tipo")
Idproveedor=Request.QueryString("Idproveedor")
imprimir=Request.QueryString("Imprimir")
nombre_prov=ucase(Request.QueryString("nombre_prov"))

if Idproveedor="" then Idproveedor=-1
%>
<script>
function Imprimir(tipo, proveedor){
document.location.href="lstAdquisiciones.asp?tipo="+tipo+"&Idproveedor="+proveedor+"&imprimir=SI"
}

function Actualizar(Idproveedor,tipo){
document.location.href="lstAdquisiciones.asp?Idproveedor="+Idproveedor+"&tipo="+tipo
} 
</script>
<% if tipo="CC" then%>
<table width="50%" border="0"">
  <tr>
    <td width="30%">Proveedor:</td>
	<td width="70%">
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
			Set rsPedidos=Obj.Consultar("BI_ConsultarPedidosEnAdqusicion","FO",tipo,0)
		else	
			Set rsPedidos=Obj.Consultar("BI_ConsultarPedidosEnAdqusicion","FO",tipo,Idproveedor)
		end if
		Obj.CerrarConexion
	Set obj=nothing

	if Not(rsPedidos.BOF and rsPedidos.EOF) then
		HayReg=true
	end if
if imprimir="SI" then
	Response.ContentType = "application/msexcel"
	Response.AddHeader "Content-Disposition","attachment;filename=Cotizacion.xls"
end if
%>

<html> 
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de Pedidos</title>

<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/validarpagina.js"></script>
<script language="JavaScript" src="../private/validarpedido.js"></script>

<script>

function VerDatos(codigo_dpe){
var pagina ='datosdepedido.asp?codigo_dpe='+codigo_dpe
AbrirPopUp(pagina,'300','500')
}

function Cotizar(codigo_dpe){
var pagina ='cotizaciondePedido.asp?codigo_dpe='+codigo_dpe
AbrirPopUp(pagina,'500','700')
}
</script>
<style type="text/css">
<!--
.Estilo2 {color: #000000}
.Estilo3 {color: #990000}
.Estilo5 {
	color: #FFFFFF;
	font-weight: bold;
}
.Estilo1 {	font-size: 10pt;
	color: #000000;
	font-weight: bold;
}
.Estilo6 {color: #FFFFCC}
.Estilo7 {
	font-size: 14pt;
	font-weight: bold;
}
.Estilo8 {
	font-size: 12px;
	font-weight: bold;
	color: #FFFFFF;
}
-->
</style>
</head>
<body>
<br>
  <%if HayReg=true then%>
  <input type="hidden" id="txtelegido" value="0">
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
  <label>

<p class="Estilo7" >COTIZACI&Oacute;N <%REsponse.Write(" "& nombre_prov)%> <br></p>
<p>Generada el: 
  <%Response.Write(date) %> 
  <%response.Write(" "&time)%>
&nbsp;&nbsp;&nbsp; | &nbsp;&nbsp;&nbsp; Tipo de cambio US$: S/.
<%Response.Write(formatnumber(TipoCambioDolar))%>
 | &nbsp;&nbsp;&nbsp; Tipo de cambio Euros: S/.
 <%Response.Write(formatnumber(TipoCambioEuros))%>
</p>
<p>&nbsp;</p>
<table width="100%" height="88%">
	<tr class="contornotabla" id="trLista" >
	  <TD width="100%" valign="top">
		<table width="100%" border="0" cellpadding="2" cellspacing="0" class="contornotabla">
          <tr class="bordeinf">
            <td width="5%" align="center" bgcolor="#26758c" class="Estilo2 bordeizqinf"><span class="Estilo5">N&ordm;</span></td>
            <td width="35%" align="center" bgcolor="#26758c" class="Estilo5">Bibliograf&iacute;a</td>
            <td width="10%" align="center" bgcolor="#26758c" class="Estilo2 bordeizqinf"><span class="Estilo5">Precio</span></td>
            <td width="10%" align="center" bgcolor="#26758c" class="Estilo2 bordeizqinf"><span class="Estilo5">Cantidad</span></td>
            <td width="10%" align="center" bgcolor="#26758c" class="Estilo2 bordeizqinf"><span class="Estilo5">Precio 
            S/.</span></td>
            <td width="10%" align="center" bgcolor="#26758c" class="Estilo2 bordeizqinf"><span class="Estilo5">Subtotal 
            S/.</span></td>
          </tr>
         <% 
		 i=0
		 do while not rsPedidos.EOF
		 i=i+1
		 %>
		  
            <tr>
			<td align="right" valign="top" class="bordederinf Estilo2" ><%=i%></td>
            <td valign="top" class="bordederinf"><span class="Estilo2">
              <%Response.Write(ucase(RsPedidos("Titulo")))%>
            </span><br>
             
              <span class="cursoM">
              <%Response.Write(RsPedidos("NombreAutor"))%>             
              <br>
              </span>
			  <span class="Estilo3">
			  <%
			  
			  Response.Write(RsPedidos("NombreEditorial") &", "&RsPedidos("Edic") &" "&RsPedidos("Abreviatura") &"; "&RsPedidos("FechaPublicacion") &"; "&RsPedidos("NombreLugar") )
			  %>
			  </span></td>
            <td width="5%" align="center" valign="top" class="bordederinf Estilo2"><%
			Response.Write(RsPedidos("moneda_Ped") &" "& FormatNumber(cdbl(RsPedidos("precio_Ped")),2))
			%></td>  
            <td width="6%" align="center" valign="top" class="bordederinf Estilo2"><%
			Response.Write(RsPedidos("moneda_Ped") &" "& FormatNumber(CDBL(RsPedidos("precio_Ped")),2))
			%></td>
            <td align="right" valign="top" bgcolor="#FFFFCC" class="bordederinf Estilo2"><%
				if RsPedidos("moneda_Ped")="$" then
					Response.Write(formatNumber(cdbl(TipoCambioDolar)*Cdbl(RsPedidos("precio_Ped")),2))
				else
					if RsPedidos("moneda_Ped")="E" then
						Response.Write(formatNumber(cdbl(TipoCambioEuros)*Cdbl(RsPedidos("precio_Ped")),2))				
					else
						Response.Write(formatNumber(cdbl(RsPedidos("precio_Ped")),2))
					end if
				end if
				%>
            </td>
            <td align="right" valign="top" bgcolor="#FFFFCC" class="bordederinf Estilo2"><%
				if RsPedidos("moneda_Ped")="$" then
					Response.Write(formatNumber(cdbl(TipoCambioDolar)*Cdbl(RsPedidos("precio_Ped"))*(RsPedidos("Cantidad_Dpe")),2))
					subtotal=cdbl(TipoCambioDolar)*Cdbl(RsPedidos("precio_Ped"))* RsPedidos("Cantidad_Dpe")
				else
					if RsPedidos("moneda_Ped")="E" then
						Response.Write(formatNumber(cdbl(TipoCambioEuros)*Cdbl(RsPedidos("precio_Ped"))*(RsPedidos("Cantidad_Dpe")),2))
						subtotal=cdbl(TipoCambioEuros)*Cdbl(RsPedidos("precio_Ped"))* RsPedidos("Cantidad_Dpe")
					else				
						Response.Write(formatNumber(CDBL(RsPedidos("precio_Ped"))* RsPedidos("Cantidad_Dpe"),2))
						subtotal=CDBL(RsPedidos("precio_Ped")) * RsPedidos("Cantidad_Dpe")
					end if
				end if			
				Total=Total+subtotal		
				%>
            </td>
          </tr>		  
		  <%
		  rsPedidos.MoveNext
		  loop%>
            <tr>
              <td colspan="5" align="left" valign="top" bgcolor="#FFFFCC" class="bordederinf Estilo6" ><span class="Estilo1">Total S/. </span></td>
              <td align="right" valign="top" bgcolor="#FFFFCC" class="bordederinf Estilo2"><b><span style="color:#000066; height:14px"><%=formatnumber(Total,2)%></span></b></td>
            </tr>		  		  
      </table>	  
        <p>&nbsp;</p>
        <p>&nbsp;</p></TD>
	</tr>
</table>
<%else%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se 
	han encontrado Pedidos Bibliográficos registrados</h5>
<%end if%>
</body>
</html>
