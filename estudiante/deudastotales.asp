<!--#include file="../NoCache.asp"-->
<!--#include file="../funciones.asp"-->
<%
call Enviarfin(session("codigo_usu"),"../")
on error resume next
Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion			
		Set rsDeuda= obj.Consultar("ConsultarDeuda","FO","E",session("codigo_alu"))
	obj.CerrarConexion
Set obj=nothing
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
</style>
</head>
<body>
<%If Not(rsDeuda.BOF and rsDeuda.EOF) then%>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr>
    <td width="50%" class="usatTitulo">Estado de cuenta general</td>
    <td width="50%" class="NoImprimir" align="right">
	<input name="cmdVer" type="button" value="  Regresar" class="salir" onclick="location.href='deudasbanco.asp'">
    <input onclick="imprimir('N',0,'')" type="button" value="    Imprimir" name="cmdImprimir" class="usatimprimir">
    </td>
  </tr>
</table>
<br>
<table border="1" width="100%" cellspacing="0" cellpadding="3" style="border-collapse: collapse;" bordercolor="#808080">
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
<%else%>
<p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se encontraron deudas pendientes por 
cancelar</p>
<%end if

If Err.Number<>0 then
    session("pagerror")="estudiante/deudastotales.asp"
    session("nroerror")=err.number
    session("descripcionerror")=err.description    
	response.write("<script>top.location.href='../error.asp'</script>")
End If
%>
</body>
</html>