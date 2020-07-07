<!--#include file="../../../../funciones.asp"-->
<%
call Enviarfin(session("codigo_usu"),"../")
modo=request.querystring("modo")
if modo="" then modo="TE"

	Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion			
			if modo="BK" then
				Set rsDeuda= obj.Consultar("ConsultarDeuda","FO","BK",session("codigouniver_alu"))
				titulo="Pagos pendientes que debe realizar en el Banco"
			else
				Set rsDeuda= obj.Consultar("ConsultarDeuda","FO","E",session("codigo_alu"))
				titulo="Estado de cuenta actual (sin incluir mora)"
			end if
		obj.CerrarConexion
	Set obj=nothing
%>
<html>
<head>
<title>Consultar Deuda</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<link rel="stylesheet" type="text/css" href="../../../../private/estiloimpresion.css" media="print">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<style type="text/css">
.totalizar {
	font-size: 11px;
	font-weight: bold;
	font-family: Verdana;
	background-color: #FFFFCC;
}
</style>
</head>
<body bgcolor="#EEEEEE">
<%If Not(rsDeuda.BOF and rsDeuda.EOF) then%>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr>
    <td width="70%" class="usatTitulo"><%=titulo%></td>
    <td width="30%" class="NoImprimir" align="right">
	<%if modo="BK" then%><input name="cmdVer" type="button" value="  Regresar" class="salir" onclick="location.href='estadocuenta.asp?modo=BK'"><%end if%>
    </td>
  </tr>
</table>
<h4 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Deberá acudir al banco 24 horas después de realizada la pre-matricula antes de la 1:00 pm. </h4>
<br>
<%if modo="BK" then%>
	<p><b>Importante</b>: Las deudas pendientes deben cancelarse a partir del día <span class="azul"> <%=formatdatetime(dateadd("d",1,rsDeuda("UltimoProceso")),1)%></span>,
	en cualquiera de las Oficinas del Banco de Crédito a nivel Nacional.	
	<hr>
	</p>
  <table bgcolor="white" border="1" width="100%" cellspacing="0" cellpadding="3" style="border-collapse: collapse" bordercolor="#808080" border="0">
    <tr class="etabla"> 
      <td width="15%"> Fecha de Vencimiento&nbsp;</td>
      <td width="40%">Descripción del concepto</td>
      <td width="20%"> Importe (S/.)</td>
      <td width="10%"> Mora (S/.)</td>
      <td width="20%">Total (S/.)</td>
    </tr>
    <%
    total=0
    subtotal=0
    Do while Not rsDeuda.EOF
		if cdbl(rsDeuda("mora"))>=0 then
			mora=formatNumber(rsDeuda("mora"))
		else
	      	mora=0
		end if

    	subtotal=cdbl(mora)+cdbl(rsDeuda("total"))
    	
    	total=subtotal+total
    
    %>
    <tr> 
      <td width="15%" align="center">
      <%
      'if rsDeuda("generamora_gen")=1 then
		response.write rsDeuda("fechavencimiento_gen")
	  'else
	  '	response.write "--"
	  'end if
      %>&nbsp;</td>
      <td width="40%"><%=rsDeuda("descripcion_sco")%>&nbsp;</td>
      <td width="20%" align="right"><%=formatnumber(rsDeuda("total"))%>&nbsp;</td>
      <td width="10%" align="right">
	  <%=mora%> &nbsp;</td>
      <td width="20%" bgcolor="#DFDBA4" align="right">
      <%=formatnumber(subtotal)%> &nbsp;</td>
    </tr>
    <%
    	rsDeuda.movenext
    Loop
    %>
    <tr class="totalizar">
      <td colspan="4" align="right" width="80%">TOTAL: </td>
      <td width="20%" align="right">S/. <%=formatnumber(total,2)%></td>
    </tr>
    </table>
<p align="right">
<input style="width:200px" name="cmdVer" type="button" value="  Ver todo el estado de cuenta" class="buscar" onclick="location.href='estadocuenta.asp?modo=TE'">
</p>
<%else%>
<table bgcolor="white" border="1" width="100%" cellspacing="0" cellpadding="3" style="border-collapse: collapse;" bordercolor="#808080">
    <tr class="etabla">
      <td width="15%">Fecha de Cargo</td>
      <td width="15%">Estado de deuda</td>
      <td width="40%">Descripción del concepto</td>
      <td width="15%">Importe (S/.)</td>
      <td width="15%" bgcolor="#DFDBA4">Saldo (S/.)</td>
    </tr>
    <% 
	totalS=0
	Do while not rsDeuda.eof
		saldo=IIf(IsNull(rsDeuda("saldo_Deu"))=True,0,rsDeuda("saldo_Deu"))
			
		if (cdbl(saldo)>0) then
			totalS= totalS + cdbl(saldo)
	%>
    <tr <%=iif(rsDeuda("estado_deu")="O","class=rojo","")%>> 
      <td width="15%"><%=rsDeuda("fecha_Deu")%>&nbsp;</td>
      <td width="15%">
      <%if rsDeuda("estado_deu")="O" then
      	response.write "Convenio (*)"
      else
      	response.write "Pendiente"
      end if
      %> &nbsp;</td>
      <td width="40%"><%=rsDeuda("descripcion_Sco")%>&nbsp;</td>
      <td width="15%" align="right"><%=formatNumber(rsDeuda("montoTotal_Deu"))%>&nbsp;</td>
      <td width="15%" bgcolor="#DFDBA4" align="right"><%=formatNumber(saldo)%>&nbsp;</td>
    </tr>
   	<%end if
    	rsDeuda.movenext
	loop
	%>
    <tr class="totalizar"> 
      <td colspan="4" align="right" width="718">TOTAL (S/.)</td>
      <td width="20%" align="right"><%=formatNumber(totalS)%>&nbsp;</td>
    </tr>
    </table>
    <p class="etiqueta">
    (*)Los CONVENIOS se pagarán en Caja y Pensiones de la Universidad, según las fechas acordadas.
    </p>
    
<%end if%>
<%else%>
<p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se encontraron deudas pendientes por realizar</p>
<%end if%>
</body>
</html>