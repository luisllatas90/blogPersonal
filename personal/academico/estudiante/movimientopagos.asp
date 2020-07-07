<!--#include file="../../../funciones.asp"-->
<%
codigo_alu=Request.QueryString("codigo_alu") 
codigouniver_alu=request.querystring("codigouniver_alu")
alumno=request.querystring("alumno")
nombre_cpf=request.querystring("nombre_cpf")

mesini=request.querystring("mesini")
mesfin=request.querystring("mesfin")
anioini=request.querystring("anioini")
aniofin=request.querystring("aniofin")
estado=request.querystring("estado")


if codigo_alu<>"" then
	if mesini="" then mesini=0'month(date)
	if mesfin="" then mesfin=0'month(date)
	if anioini="" then anioini=year(date)
	if aniofin="" then aniofin=year(date)
	if estado="" then estado="P"

	Set objDeuda= Server.CreateObject("PryUSAT.clsAccesoDatos")
		objDeuda.AbrirConexion
		Set rsDeuda= objDeuda.Consultar("ConsultarMovimientosAlumno","FO",codigouniver_alu,mesini,mesfin,anioini,aniofin,estado)
		objDeuda.CerrarConexion
	Set objDeuda=nothing
%>
<html>
<head>
<title>Consultar Deuda</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css" media="print"/>
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script>
	function consultarMov()
	{
		if(txtanioini.value.length>5 && txtaniofin.value.length>5){
			alert("Por favor especifique correctamente los a�os de inicio y fin")
			return(false)
		}
		var otroparam="&codigo_alu=<%=codigo_alu%>&codigouniver_alu=<%=codigouniver_alu%>&tipo=E&alumno=<%=alumno%>&nombre_cpf=<%=nombre_cpf%>"
		location.href="movimientopagos.asp?mesini=" + cbomesini.value + "&anioini=" + txtanioini.value + "&mesfin=" + cbomesfin.value + "&aniofin=" + txtaniofin.value + "&estado=" + cboestado.value + otroparam
	}
	
	function bloquearcbo(cbo1,cbo2){
		if (cbo1.value=="0"){
			cbo2.value=0
			txtanioini.style.display="none"
			txtaniofin.style.display="none"
			cbo2.style.display="none"
		}
		else{
			txtanioini.style.display=""
			txtaniofin.style.display=""
			cbo2.style.display=""
			cbo2.value=cbo1.value
		}
	}
</script>
</head>
<body bgcolor="#EEEEEE">
	<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1" bgcolor="#FFFFFF" class="contornotabla">
      <tr>
        <td width="1148" colspan="7" class="usatceldatitulo">Consultar movimientos por mes y a�o</td>
      </tr>
      <tr>
        <td width="156" class="etiqueta">Mes de Cargo de Inicio:</td>
        <td width="143">
        <select size="1" name="cbomesini" id="cbomesini" onChange="bloquearcbo(this,cbomesfin)">
        <option value="0">TODOS</option>
        <%for i=1 to 12%>
        	<option value="<%=i%>" <%=seleccionarItem("cbo",mesini,i)%>><%=MonthName(i)%></option>
        <%next%>
        </select><input type="text" name="txtanioini" id="txtanioini" style="display:none" onkeypress="validarnumero()" maxlength="4" size="4" class="cajas" value="<%=anioini%>" >&nbsp;
        </td>
        <td width="144" align="right" class="etiqueta">Mes de Cargo de Fin:</td>
        <td width="130">
        <select size="1" name="cbomesfin" id="cbomesfin" style="display:none" onChange="bloquearcbo(this,cbomesini)">
        <%for i=1 to 12%>
        	<option value="<%=i%>" <%=seleccionarItem("cbo",mesfin,i)%>><%=MonthName(i)%></option>
        <%next%>
        </select><input type="text" name="txtaniofin" id="txtaniofin" style="display:none" onkeypress="validarnumero()" maxlength="4" size="4" class="cajas" value="<%=aniofin%>"></td>
        <td width="110" align="right">
        <p align="left">Estado de Deuda</td>
        <td width="95">
        <select size="1" name="cboestado" id="cboestado">
        <option value="T">--TODOS--</option>
        <option value="P" <%=seleccionarItem("cbo",estado,"P")%>>Pendientes</option>
        <option value="C" <%=seleccionarItem("cbo",estado,"C")%>>Cancelados</option>
        </select></td>
        <td width="334"><img class="NoImprimir" style="cursor:hand" src="../../../images/buscar.gif" onclick="consultarMov()"></td>
      </tr>
    </table>
    <br>
	<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla" bgcolor="#FFFFFF">
          <tr>
    <td width="18%">C�digo Universitario&nbsp;</td>
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

<table>
<tr>
	<td><font color="#0000FF"><b>N�mero de convenios realizados:</b></font></td>
	<td><b>
	<%
	Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
		Set rs= obj.Consultar("ConsultarConvenioPago","FO","CO", "E", codigo_Alu)
		obj.CerrarConexion
		
		if rs.recordcount>0 then
			response.write (rs("nroconvenios"))
			if cdbl(rs("nroconvenios"))>0 then%>
				<font color="#FF0000">
				<!--
				    '---------------------------------------------------------------------------------------------------------------
                    'Fecha: 29.10.2012
                    'Usuario: dguevara
                    'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                    '---------------------------------------------------------------------------------------------------------------
				-->
	 			<span style="cursor:hand" onclick="AbrirPopUp('../../../librerianet/academico/conveniopensiones.aspx?codigo_alu=<%=codigo_alu%>&codigouniver_alu=<%=codigouniver_alu%>&tipo=E&alumno=<%=alumno%>&nombre_cpf=<%=nombre_cpf%>','550','900','no','no','yes','convenio')" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Haga 
    clic aqu� para ver los convenios</span>
			    </font>
			<%end if
			
		end if
		
		rs.close
		set rs=nothing
	Set obj=nothing
	%>
	 
	
	</b>
 </td>
</table>

<%If Not(rsDeuda.BOF and rsDeuda.EOF) then%>
  <table width="100%" cellspacing="0" cellpadding="3" style="border-collapse: collapse" bordercolor="#C0C0C0" border="1" bgcolor="#FFFFFF">
    <tr class="usatCeldaTitulo"> 
      <th>Fecha de Vencimiento</th>
      <th>Concepto</th>
      <th>Estado</th>
      <th>Cargo</th>
      <th>Pago</th>
      <th>Saldo</th>
      <th>Mora</th>
      <th>Subtotal</th>
    </tr>
    <% 
	totalS=0
	totalD=0
	totalE=0
	do while not rsDeuda.eof
			subtotal_deu=0
			mora_deu=0
			cargo=IIf(IsNull(rsDeuda("cargo"))=True,0,rsDeuda("cargo"))
			pago=IIf(IsNull(rsDeuda("pagos"))=True,0,rsDeuda("pagos"))
			saldo=IIf(IsNull(rsDeuda("saldo"))=True,0,rsDeuda("saldo"))
			mora_deu=rsDeuda("mora_deu")
			subtotal_deu=cdbl(mora_deu)+cdbl(saldo)			

			totalcargo=totalcargo + cdbl(cargo)
			totalpago=totalpago + cdbl(pago)
			totalsaldo=totalsaldo + cdbl(saldo)
			totalmoras=totalmoras + cdbl(mora_deu)
			totalsubtotal=totalsubtotal + cdbl(subtotal_deu)
			
			if rsDeuda("estado_deu")="P" then estado="Pendiente"
			if rsDeuda("estado_deu")="C" then estado="Cancelada"
			if rsDeuda("estado_deu")="O" then estado="Convenio"		
			
			gm = ""
			if rsDeuda("generaMora_sco")= true then
				gm = " <font color='#FF0000'>*</font> "
			end if
	%>
    <tr> 
      <td><%=formatdatetime(rsDeuda("fechaVencimiento_sco"),2)%>&nbsp; <%=gm%> </td>
      <td><%=rsDeuda("servicio")%>&nbsp;</td>
      <td><%=estado%>&nbsp;</td>
      <td align="right"><%=formatNumber(cargo)%>&nbsp;</td>
      <td align="right"><%=formatNumber(pago)%>&nbsp;</td>
      <td align="right"><%=formatNumber(saldo)%>&nbsp;</td>
      <td align="right"><%=formatNumber(mora_deu)%>&nbsp;</td>
      <td align="right"><%=formatNumber(subtotal_deu)%>&nbsp;</td>
    </tr>
    	<%rsDeuda.movenext
	loop
	%>
    <tr bgcolor="#FFFFCC"> 
    <td><p align="left">(<font color="#FF0000"><b>*</b></font>) Genera Mora&nbsp;</td>
      <td colspan="2" align="right" class="etiqueta">Total (S/.)</td>
      <td align="right"><%=formatNumber(totalcargo)%>&nbsp;</td>
      <td align="right"><%=formatNumber(totalpago)%>&nbsp;</td>
      <td align="right"><%=formatNumber(totalsaldo)%>&nbsp;</td>
      <td align="right" class="rojo"><%=formatNumber(totalmoras)%>&nbsp;</td>
      <td align="right" class="usatTitulousat"><%=formatNumber(totalsubtotal)%>&nbsp;</td>
    </tr>
    </table> 
<%else%>
	<p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se encontraron movimientos de pago realizados.</p>
<%end if%>
</body>
</html>
<%end if%>