<!--#include file="../NoCache.asp"-->
<!--#include file="../funciones.asp"-->
<%
call Enviarfin(session("codigo_usu"),"../")
on error resume next

modo=request.querystring("modo")
if modo="" then modo="TE"

	Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion			
			if modo="BK" then
				Set rsDeuda= obj.Consultar("ConsultarDeuda","FO","BK",session("Ident_Usu"))
				titulo="DEUDAS ENVIADAS AL BANCO EL: " & rsDeuda("fecEnvio")
				fechaHoraEnvio = rsDeuda("fecEnvio")

			else
				Set rsDeuda= obj.Consultar("ConsultarDeuda","FO","E",session("codigo_alu"))
				titulo="Estado de cuenta actual"
			end if
		dim rsfechapago
		set rsfechapago= obj.consultar("sp_determinafechapagomatricula","FO",session("codigo_alu"),session("codigo_cac"))
		obj.CerrarConexion
	Set obj=nothing
%>
<html>
<head>
<title>Consultar Deuda</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<link rel="stylesheet" type="text/css" href="../private/estilo.css" />
<link rel="stylesheet" type="text/css" href="../private/estiloimpresion.css" media="print" />
<script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>
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
    <td width="70%" class="usatTitulo"><%=titulo%></td>
    <td width="30%" class="NoImprimir" align="right">
	
    <input onClick="imprimir('N',0,'')" type="button" value="    Imprimir" name="cmdImprimir" class="usatimprimir">
    </td>
  </tr>
</table>
<br>
	<!--#include file="fradatos.asp"-->
<br>


<!--
<h4 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Deberá acudir al banco 24 horas después de realizada la pre-matricula si se matriculó antes de la una de la tarde, caso contrario deberá acudir 48 horas después</h3>
-->
<br>
<%if modo="BK" then%>
	<!--<p><b>Importante</b>: Las deudas pendientes deben cancelarse a partir del día <span class="azul"> <%=formatdatetime(dateadd("d",1,rsDeuda("UltimoProceso")),1)%></span>,
	en cualquiera de las Oficinas del Banco de Crédito, a nivel Nacional.	
	<hr>
	</p>
	-->
  <table border="1" width="100%" cellspacing="0" cellpadding="3" style="border-collapse: collapse" bordercolor="#808080" border="0">
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
      'if rsDeuda("generamora_gen")=true then
      'if 1 = 1 then
		response.write rsDeuda("fechavencimiento_gen")
	  'else
	  	'response.write ""
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
<input style="width:200px" name="cmdVer" type="button" value="  Ver todo el estado de cuenta" class="buscar" onClick="location.href='estadocuenta.asp?modo=TE'">
</p>
<%else%>



<table border="1" width="100%" cellspacing="0" cellpadding="3" style="border-collapse: collapse;" bordercolor="#808080">
    <tr class="etabla">
      <td width="15%">Fecha de Vencimiento</td>
      <td width="15%">Estado de deuda</td>
      <td width="40%">Descripción del concepto</td>
      <td width="15%">Importe (S/.)</td>
      <td width="15%">Saldo (S/.)</td>
      <td width="15%">Mora (S/.)</td>
      <td width="15%" bgcolor="#DFDBA4">Sub Total (S/.)</td>
    </tr>
    <% 
	totalS=0
	Do while not rsDeuda.eof
		saldo=IIf(IsNull(rsDeuda("saldo_Deu"))=True,0,rsDeuda("saldo_Deu"))
		
		if (cdbl(saldo)>0) then
		
			if cdbl(rsDeuda("mora_deu"))>=0 then
				mora=rsDeuda("mora_deu")
			else
	      		mora=0
			end if

    		subtotal=cdbl(mora)+cdbl(rsDeuda("saldo_Deu"))
    		totalS=totalS+subtotal
	%>
    <tr <%=iif(rsDeuda("estado_deu")="O","class=rojo","")%>> 
      <td width="15%">
      
      
      <%
       'if rsDeuda("generaMora_sco")=true then
		response.write rsDeuda("fechavencimiento_Deu")
	   'else
	  	'response.write "" 
	   'end if
      %>
      
      
      
      
      </td>
      <td width="15%">
      <%if rsDeuda("estado_deu")="O" then
      	response.write "Consideracion Especial"
      else
      	response.write "Pendiente"
      end if
      %> &nbsp;</td>
      <td width="40%"><%=rsDeuda("descripcion_Sco")%>&nbsp;</td>
      <td width="15%" align="right"><%=formatNumber(rsDeuda("montoTotal_Deu"))%>&nbsp;</td>
      <td width="15%" align="right"><%=formatNumber(saldo)%>&nbsp;</td>
      <td width="15%" align="right"><%=formatNumber(mora)%>&nbsp;</td>
      <td width="15%" bgcolor="#DFDBA4" align="right"><%=formatNumber(subtotal)%>&nbsp;</td>
    </tr>
   	<%end if
    	rsDeuda.movenext
	loop
	%>
    <tr class="totalizar"> 
      <td width="85%" colspan="4" align="center">TOTAL</td>
      <td width="15%" align="right">&nbsp;</td>
      <td width="15%" align="right">&nbsp;</td>
      <td width="15%" bgcolor="#DFDBA4" align="right"><%=formatNumber(totalS)%>&nbsp;</td>
    </tr>
    </table>

<% if session("codigo_cpf")<> 25 then %>
   <p class="etiqueta"><u>Aviso:</u></p>

 

    <li>
	<p><b><font color="#FF0000">Deberá acudir a cancelar en cualquier 
    agencia del Banco de Crédito a nivel nacional, indicando su número de documento de identidad a partir del&nbsp; <%=rsfechapago.fields("fechapago")%> .</font></b></p></li>

    <!--<li><b><font color="#FF0000">Para estar matriculado deberá efectuar el pago total de las asignaturas (inscripción) y el 50% del costo dela pensión del 26/12/2008 hasta el 05/01/2009, y el último pago del 28/01/2009 hasta el 30/01/2009.</font></b></li>-->
   <%if modo<>"BK" then%>    <p align="center"><input name="cmdVer" type="button" style="width : 180px" value="Ver Deudas enviadas al Banco " class="usatboton" onClick="location.href='estadocuenta.asp?modo=BK'"></p><%end if%>
<%end if%>    
    
    
<%end if%>
<%else%>
<p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se encontraron deudas pendientes por realizar</p>
<%end if

If Err.Number<>0 then
    session("pagerror")="estudiante/estadocuenta.asp"
    session("nroerror")=err.number
    session("descripcionerror")=err.description    
	response.write("<script>top.location.href='../error.asp'</script>")
End If
%>

<% if modo="BK" then

        response.write ("Estas deudas figuran en el Banco al día siguiente del último envío. Fueron enviadas el: " & fechaHoraEnvio )	
    end if
%>


</body>
</html>