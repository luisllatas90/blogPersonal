<!--#include file="../../../funciones.asp"-->
<%
codResp=Request.QueryString("codigo_alu") 
tipoResp=Request.QueryString("tipo")
codigouniver_alu=request.querystring("codigouniver_alu")
alumno=request.querystring("alumno")
nombre_cpf=request.querystring("nombre_cpf")

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
<body bgcolor="#EEEEEE">
	<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla" bgcolor="#FFFFFF">
          <tr>
    <td width="18%">Código Universitario&nbsp;</td>
    <td class="usatsubtitulousuario" width="71%">: <%=codigouniver_alu%></td>
          </tr>
          <tr>
    <td width="18%">Apellidos y Nombres</td>
    <td class="usatsubtitulousuario" width="71%">: <%=alumno%></td>
          </tr>
          <tr>
    <td width="18%">Escuela Profesional&nbsp;</td>
    <td class="usatsubtitulousuario" width="46%">: <%=nombre_cpf%>&nbsp;</td>
     	 </tr>
 	</table>
<BR> 	
<%If Not(rsDeuda.BOF and rsDeuda.EOF) then%>
  <table width="100%" cellspacing="0" cellpadding="3" style="border-collapse: collapse" bordercolor="#C0C0C0" border="1" bgcolor="#FFFFFF">
    <tr class="usatCeldaTitulo"> 
      <th>Fecha Vencimiento</th>
      <th>Concepto</th>
      <th>Descripcion</th>
      <th>Moneda</th>     
      <th>Monto</th>
      <th>Mora</th>
    </tr>
    <% 
	totalS=0
	totalD=0
	totalE=0
	do while not rsDeuda.eof
		mora=0
		saldo=IIf(IsNull(rsDeuda("saldo_Deu"))=True,0,rsDeuda("saldo_Deu"))
		if rsDeuda("moneda_Deu")="S/." then totalS= totalS + cdbl(saldo)
		if rsDeuda("moneda_Deu")="US$" then totalD= totalD + cdbl(saldo)
		if rsDeuda("moneda_Deu")="&#8364;" then totalE= totalE + cdbl(saldo)
		if rsDeuda("mora_Deu")>0 then mora=rsDeuda("moneda_Deu")
	%>
    <tr> 
      <td><%=rsDeuda("fechaVencimiento_Deu")%>&nbsp;</td>
      <td><%=rsDeuda("descripcion_Sco")%>&nbsp;</td>
      <td><%=rsDeuda("observacion_Deu")%>&nbsp;</td>
      <td><%=rsDeuda("moneda_Deu")%>&nbsp;</td>
      <td><%=formatNumber(saldo)%>&nbsp;</td>
      <td><%=formatNumber(mora)%>&nbsp;</td>
    </tr>
    	<%rsDeuda.movenext
	loop
	%>
    <tr> 
      <th colspan="4" align="right">Total (S/.)</th>
      <th><%=formatNumber(totalS)%>&nbsp;</th>
    </tr>
    </table> 
<%else%>
	<p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se encontraron deudas pendientes por realizar</p>
<%end if%>
</body>
</html>
<%end if%>