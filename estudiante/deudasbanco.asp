<!--#include file="../NoCache.asp"-->
<!--#include file="../funciones.asp"-->
<%
call Enviarfin(session("codigo_usu"),"../")
on error resume next
codigouniversitario=session("Ident_Usu")
HayReg=false

	Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion			
			Set rsDeuda=obj.Consultar("ConsultarDeuda","FO","BK",codigouniversitario)
			Set rsCuotas=obj.Consultar("ConsultarDeuda","FO","AV",codigouniversitario)
		obj.CerrarConexion
	Set obj=nothing
	
	If Not(rsDeuda.BOF and rsDeuda.EOF) then
		if cdbl(rsDeuda("saldo_deuda"))>0 then
			HayReg=true
		end if
	end if
	
	'formatdatetime(dateadd("d",1,rsDeuda("UltimoProceso")),1)
%>
<html>
<head>
<title>Consultar Deuda</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<link rel="stylesheet" type="text/css" href="../private/estiloimpresion.css" media="print"/>
<script language="JavaScript" src="../private/funciones.js"></script>
<style type="text/css">
.totalizar {
	font-size: 11px;
	font-weight: bold;
	font-family: Verdana;
	background-color: #FFFFCC;
}
.fechavencimiento {
	font-size:14px;
}
</style>
</head>
<body>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr>
    <td width="70%" class="usatTitulo">Estado de cuenta</td>
    <td width="30%" class="NoImprimir" align="right">
    <%if HayReg=true then%>
	<input name="cmdVer" type="button" value="  Ver todo" class="buscar2" onclick="location.href='deudastotales.asp'">
    <input onclick="imprimir('N',0,'')" type="button" value="    Imprimir" name="cmdImprimir" class="imprimir2">
    <%end if%>
    </td>
  </tr>
</table>
<br>
	<!--#include file="fradatos.asp"-->
<br>
<%if HayReg=false then%>
<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se encontraron deudas pendientes por realizar</h5>	
<%else%>
<table style="width: 100%">
	<tr>
		<td id="tdResumen" width="35%" valign="top">
		<!--
			Totaliza la deuda general y el monto cancelado hasta el momento
		-->
			<p class="usatTituloPagina">Resúmen total</p>
			<table class="contornotabla_azul" cellpadding="3" width="100%" style="border-collapse: collapse" border="1" bordercolor="#808080">
				<tr class="etiqueta">
					<td>Monto de deuda</td>
					<td class="fechavencimiento"><%=formatnumber(rsDeuda("total_deuda"))%>&nbsp;</td>
				</tr>
				<tr class="etiqueta">
					<td>Saldo actual</td>
					<td><%=formatnumber(rsDeuda("saldo_deuda"))%>&nbsp;</td>
				</tr>
				<tr class="etiqueta">
					<td>Moneda</td>
					<td>SOLES</td>
				</tr>
			</table>
		<br>
		<table cellpadding="3" width="100%" class="contornotabla_azul">
			<tr class="azul">
				<td class="bordeinf"><span class="rojo">
				CONTÁCTENOS</span></td>
			</tr>
			<tr>
				<td>Para cualquier información adicional comuníquese con la 
				Oficina de Caja y Pensiones de la Universidad. <br>
				<br>
				Teléfono: 
				201530 anexo 134</td>
			</tr>
		</table>
		<br>
		<br>
		<table cellpadding="3" width="100%" class="contornotabla_azul">
			<tr class="azul">
				<td class="bordeinf">MENSAJE AL CLIENTE</td>
			</tr>
			<tr>
				<td>RECORDAMOS PAGAR SUS CUOTAS A TIEMPO, YA QUE CUALQUIER 
				RETRASO EN SUS PAGOS GENERA UNA MORA ADICIONAL POR CADA SERVICIO 
				ASIGNADO.<br>
				<br>
				EL PAGO DE SUS CUOTAS DEBEN REALIZARSE EN CUALQUIERA DE LAS 
				OFICINAS DEL <strong>BANCO DE CRÉDITO O BANCO INTERBANK,&nbsp;A NIVEL 
				NACIONAL</strong></td>
			</tr>
		</table>
		<br>
		</td>
		<td id="tdSeparador" width="10%" valign="top">
		&nbsp;</td>
		<td id="tdMesActual" width="55%" valign="top">
		<!--
			Resume la deuda del mes actual
		-->
		<%
			rsDeuda.Filter=""
			rsDeuda.Filter="cuotames=" & month(date) & " and cuotaanio=" & year(date) & " "
		%>
		<p class="usatTituloPagina">Cuota del mes actual</p>
		<table class="contornotabla_azul" border="0" width="100%" cellspacing="0" cellpadding="3" style="border-collapse: collapse" bordercolor="#808080">
		    <tr class="etabla"> 
		      <td width="40%" class="bordeinf">SERVICIO</td>
		      <td width="10%" class="bordeinf">IMPORTE</td>
		      <td width="10%" class="bordeinf">MORA</td>
		      <td width="20%" class="bordeinf">SUBTOTAL</td>
		    </tr>
		    <%
		    total=0
		    subtotal=0
		    Do while Not rsDeuda.EOF
		    	UltimoDiaPago=rsDeuda("fechavencimiento_gen")
				if cdbl(rsDeuda("mora"))>=0 then
					mora=formatNumber(rsDeuda("mora"))
				else
			      	mora=0
				end if
		
		    	subtotal=cdbl(mora)+cdbl(rsDeuda("total"))
		    	
		    	total=subtotal+total
		    
		    %>
		    <tr> 
		      <td width="40%"><%=rsDeuda("descripcion_sco")%>&nbsp;</td>
		      <td width="10%" align="right"><%=formatnumber(rsDeuda("total"))%>&nbsp;</td>
		      <td width="10%" align="right">
			  <%=mora%> &nbsp;</td>
		      <td width="20%" align="right" class="etiqueta">
		      <%=formatnumber(subtotal)%> &nbsp;</td>
		    </tr>
		    <%
		    	rsDeuda.movenext
		    Loop
		    %>
		    <tr class="totalizar">
		      <td class="bordesup" colspan="3" align="right" width="80%">Monto a pagar: </td>
		      <td class="bordesup" width="20%" align="right">S/. <%=formatnumber(total,2)%></td>
		    </tr>
		    <tr class="totalizar">
		      <td colspan="3" align="right" width="80%">Último día de pago</td>
		      <td width="20%" align="right" class="fechavencimiento"><%=UltimoDiaPago%></td>
		    </tr>
		    </table>
		<br>
		<br>
		<br>
		<span class="usatTituloPagina">Cuotas de los próximos meses</span><br>
		<table cellpadding="2"  width="100%" border="0" bordercolor="gray">
		<%
		total=rsCuotas.recordcount
		
		Do while not rsCuotas.EOF
			i=i+1
			if (i mod 7= 0) then
				response.write "</tr><tr>"
			elseif i=0 then
				response.write "<tr>"
			end if
		%>
			<td align="center">
				<table cellpadding="3" border="1" bordercolor="gray" width="100%">
					<tr><td align="right" bgcolor="#E7E6C9"><%=rsCuotas(1)%></td></tr>
					<tr><td align="right">S/. <%=rsCuotas(0)%></td></tr>
				</table>
			</td>
		<%	
			rsCuotas.movenext
		Loop%>
		</table>		
		</td>
	</tr>
	</table>
<%end if

set rsDeuda=nothing

If Err.Number<>0 then
    session("pagerror")="estudiante/deudasbanco.asp"
    session("nroerror")=err.number
    session("descripcionerror")=err.description    
	response.write("<script>top.location.href='../error.asp'</script>")
End If
%>
</body>
</html>