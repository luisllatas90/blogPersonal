<!--#include file="../../../funciones.asp"-->
<%
codResp=Request.QueryString("codigo_alu") 
tipoResp=Request.QueryString("tipo")

if codResp<>"" and tipoResp<>"" then
	Set objDeuda= Server.CreateObject("PryUSAT.clsDatDeuda")
		Set rsDeuda= objDeuda.ConsultarDeuda("RS",TRIM(tipoResp),codResp)
	Set objDeuda=nothing
%>
<html>
<head>
<title>Consultar Deuda</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
</head>
<body>
<%If Not(rsDeuda.BOF and rsDeuda.EOF) then%>	
  <table width="80%" cellspacing="0" cellpadding="3" style="border-collapse: collapse" bordercolor="#808080" border="1">
    <tr class="usatencabezadotabla"> 
      <th>Fecha Deuda</th>
      <th >Concepto</th>
      <th>Descripcion</th>
      <th> Moneda</th>
      <th> Monto</th>
    </tr>
    <% 
	totalS=0
	totalD=0
	totalE=0
	do while not rsDeuda.eof
			saldo=IIf(IsNull(rsDeuda("saldo_Deu"))=True,0,rsDeuda("saldo_Deu"))
			if rsDeuda("moneda_Deu")="S/." then totalS= totalS + cdbl(saldo)
			if rsDeuda("moneda_Deu")="US$" then totalD= totalD + cdbl(saldo)
			if rsDeuda("moneda_Deu")="€" then totalE= totalE + cdbl(saldo)
	%>
    <tr> 
      <td><%=rsDeuda("fecha_Deu")%>&nbsp;</td>
      <td><%=rsDeuda("descripcion_Sco")%>&nbsp;</td>
      <td><%=rsDeuda("observacion_Deu")%>&nbsp;</td>
      <td><%=rsDeuda("moneda_Deu")%>&nbsp;</td>
      <td><%=formatNumber(saldo)%>&nbsp;</td>
    </tr>
    	<%rsDeuda.movenext
	loop
	%>
    <tr> 
      <th colspan="4" align="right">Total (S/.)</th>
      <th><%=formatNumber(totalS)%>&nbsp;</th>
    </tr>
    <tr> 
      <th colspan="4" align="right">Total ($)</th>
      <th><%=formatNumber(totalD)%>&nbsp;</th>
    </tr>
    <tr> 
      <th colspan="4" align="right">Total (<font face="Verdana">€</font>)</th>
      <th><%=formatNumber(totalE)%>&nbsp;</th>
    </tr>
    </table> 
<%else%>
	<p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se encontraron deudas pendientes por realizar</p>
<%end if%>
</body>
</html>
<%end if%>