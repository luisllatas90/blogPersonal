<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<html>
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
	<link rel="stylesheet" type="text/css" href="../private/estilo.css">
	<script language="JavaScript" src="../private/funciones.js"></script>
	<script language="JavaScript">
	
		function validaSubmite(){ 
		    if (document.frmParametro.txtAno.value == "") 
			{
       		alert("Debe Ingresar El Año del Presupuesto") 
				frmParametro.txtAno.focus();
				return (false);	

   			}

			if (document.frmParametro.txtFecIni.value == "") 
			{
       		alert("Debe Ingresar La Fecha de Inicio del Presupuesto") 
				frmParametro.txtFecIni.focus();
				return (false);	

   			}

			if (document.frmParametro.txtFecFin.value == "") 
			{
       		alert("Debe Ingresar La Fecha de Fin del Presupuesto") 
				frmParametro.txtFecFin.focus();
				return (false);	

   			}


		    if (document.frmParametro.cboForDet.value == "0") 
			{
       		alert("Debe Seleccionar La Forma de Detalle ") 
				frmParametro.cboForDet.focus();
				return (false);	

		   }
   
		    if (document.frmParametro.cboMon.value == "0") 
			{
       		alert("Debe Seleccionar La Moneda") 
				frmParametro.cboMon.focus();
				return (false);	

		   }
   
   
			 if (document.frmParametro.txtProMor.value == "") 
				{
       			alert("Debe Ingresar el procentaje de provision de morosidad") 
				frmParametro.txtProMor.focus();
				return (false);	

   			}

   
			 if (document.frmParametro.txtRecMor.value == "") 
				{
       			alert("Debe Ingresar el procentaje de recuperacion de morosidad") 
				frmParametro.txtRecMor.focus();
				return (false);	

   			}
   			
   			 if (document.frmParametro.txtSal.value == "") 
				{
       			alert("Debe Ingresar el Saldo de Caja Año Anterior") 
				frmParametro.txtSal.focus();
				return (false);	

   			}
   
   
   	      	return (true);	
   	      	
   	      	
   	      	
   	      	

		} 

    </script>


</head>
<body>

<%

Dim strTipo
Dim strAno_Ppr
Dim strFormaDetalle_Pto
Dim strMoneda_Pto
Dim dblProvisionMorosidad_Pto
Dim dblRecuperacionMorosidad_Pto
Dim dtmFechaInicio_Pto
Dim dtmFechaFin_Pto
Dim  dblSaldoCajaAnoAnterior_Pto
Dim strEstado_Pto
Dim objParametro
Dim rsParametro

strTipo=trim(Request.QueryString("tip")) ' Para Indicar Si E: Edita o N: Nuevo C: Consulta
strAno_Ppr=trim(Request.QueryString("ano"))

'Response.Write(strTipo)
'Response.Write(strAno_Ppr)

if strTipo="E" THEN

	Set objParametro= Server.CreateObject("PryUSAT.clsDatPresupuesto")
	Set rsParametro= Server.CreateObject("ADODB.RecordSet")
	set rsParametro = objParametro.ConsultarParametroPresupuesto("RS",strAno_Ppr) 	
			
	if rsParametro.recordcount >0 then

		strFormaDetalle_Pto=trim(rsParametro("FormaDetalle_Pto"))
		strMoneda_Pto=trim(rsParametro("Moneda_Pto"))
		dblProvisionMorosidad_Pto= trim(rsParametro("ProvisionMorosidad_Pto"))
		dblRecuperacionMorosidad_Pto= trim(rsParametro("RecuperacionMorosidad_Pto"))
		dtmFechaInicio_Pto=trim(rsParametro("FechaInicio_Pto"))
		dtmFechaFin_Pto=trim(rsParametro("FechaFin_Pto") )
		dblSaldoCajaAnoAnterior_Pto=trim(rsParametro("SaldoCajaAñoAnterior_Pto"))
		strEstado_Pto=Trim(rsParametro("Estado_Pto"))
		
	else

		strFormaDetalle_Pto=""
		strMoneda_Pto=""
		dblProvisionMorosidad_Pto= ""
		dblRecuperacionMorosidad_Pto= ""
		dtmFechaInicio_Pto=""
		dtmFechaFin_Pto=""
		dblSaldoCajaAnoAnterior_Pto=""
		strEstado_Pto=""


	end if
	rsParametro.Close 
	Set rsParametro=Nothing
	set	objParametro=Nothing

END IF

if strTipo="N" THEN
	strEstado_Pto="1"
END IF	
%>


<!-- Titulo de la Pagina-->
<table align="center" width="80%">
  <tr>
      <th align="center" class="table">PARÁMETROS DE PRESUPUESTO</th>
  </tr>
  <tr>
	<td><hr></td>
  </tr>
</table>
<!-- Fin de Titulo de la Pagina -->

<!-- Menu de Opciones -->
<table width="80%" align="center">
<tr>
	 

	<td width="20%" align="left"><A href="frmParametro.asp?tip=N&ano=" style="text-decoration:none" ><img border="0" src="../images/nuevo.gif" align="baseline" width="16" height="16"> Aperturar Presupuesto</A></td>
	<td width="30%" align="left"><A href="frmParametro.asp?tip=C" style="text-decoration:none" ><img border="0" src="../images/previo.gif" align="baseline" width="16" height="16"> 
    Consultar Parámetros Registrados</A></td>
	<td width="20%">&nbsp;</td>
	

</tr>
<tr>
<td colspan="3" width="463">--------------------------------------------------------------------------------------------</td>
</tr>
</table>
<!-- Fin de Menu-->

<P>

<% if Trim(strTipo)="C" then


	Set objParametro= Server.CreateObject("PryUSAT.clsDatPresupuesto")
	Set rsParametro= Server.CreateObject("ADODB.RecordSet")
	set rsParametro = objParametro.ConsultarParametroPresupuesto("RS","") 	
		
	if rsParametro.recordcount >0 then%>
		<table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="80%">
			<tr class="etabla">
			  <td>Año</td>
			  <td>Fecha Inicio</td>
			  <td>Fecha Fin</td>
			  <td>Forma Detalle</td>
			  <td>Moneda</td>
			  <td>Porc.Prov.Moros.</td>
			  <td>Porc.Recup.Moros.</td>
			  <td>Saldo Caja Año Ant.</td>
			  <td>Vig.</td>
			  <td>Elim.</td>
			  
			</tr>


		<%do while not rsParametro.eof
			strAno = trim(rsParametro("año_Ppr"))
			strMoneda= trim(rsParametro("Moneda_Pto"))
			if strMoneda="S" then strMoneda="S/."
			if strMoneda="D" then strMoneda="US$"
			if strMoneda="E" then strMoneda="€"
			

			strVigencia=Trim(rsParametro("Estado_Pto"))
			if strVigencia="1" then strVigencia="Si"
			if strVigencia="0" then strVigencia="No"
		
			%>
	
		   
		    
		    
		    <tr align="center" onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)" style="cursor:hand">
		      <a href="frmParametro.asp?ano=<%=trim(rsParametro("año_Ppr"))%>&tip=E">
		        <td><%=strAno%>&nbsp;</td>
				<td><%=trim(rsParametro("FechaInicio_Pto"))%>&nbsp;</td>
				<td><%=trim(rsParametro("FechaFin_Pto") )%>&nbsp;</td>
				<td><%=trim(rsParametro("FormaDetalle_Pto"))%>&nbsp;</td>
				<td><%=strMoneda%>&nbsp;</td>
				<td><%=trim(rsParametro("ProvisionMorosidad_Pto"))%>&nbsp;</td>
				<td><%=trim(rsParametro("RecuperacionMorosidad_Pto"))%>&nbsp;</td>
				<td><%=trim(rsParametro("SaldoCajaAñoAnterior_Pto"))%>&nbsp;</td>
				<td><%=strVigencia%>&nbsp;</td>
			  </a>
			
			  <td align="center" ><a href="Procesos.asp?Tipo=D005&ano=<%=strAno%>"><img border="0" src="../images/eliminar.gif" align="baseline" width=10 eigth=12 ></a></td>
		    
		    </tr>
			
			<%rsParametro.movenext
		loop	


	end if

	rsParametro.Close 
	Set rsParametro=Nothing
	set objParametro=Nothing

	


else%>


	<form  onSubmit="return validaSubmite()" method="post" action="Procesos.asp?Tipo=I007" id="frmParametro" name="frmParametro">
				
			  <input type="hidden" name="txtTip" value="<%=strTipo%>">
			  <table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="80%">
			  
			  
			
				<tr>
					<td class="etabla">Año del Presupuesto:</td>
					<td><input Type"text" name="txtAno" size="4" maxlength="4" value="<%=strAno_Ppr%>"></td>
					
				</tr>
				
				<tr>
					<td class="etabla">Fecha de Inico:</td>
					<td><input Type"text" name="txtFecIni" size="10" value="<%=dtmFechaInicio_Pto%>"></td>
					
				</tr>
				
				<tr>
					<td class="etabla">Fecha de Fin:</td>
					<td><input Type"text" name="txtFecFin" size="10" value="<%=dtmFechaFin_Pto%>"></td>
					
				</tr>
				
				
				<tr>
					<td class="etabla">Forma de Detalle de Presupuesto:</td>
					<%
						fdM=""
						fdB=""
						fdT=""
						fdS=""
						if strFormaDetalle_Pto="M" then fdM="selected"
						if strFormaDetalle_Pto="B" then fdB="selected"
						if strFormaDetalle_Pto="T" then fdT="selected"
						if strFormaDetalle_Pto="S" then fdS="selected"
					%>
					<td><select name="cboForDet" id="cboForDet">
						 <option value="0">--- Seleccione la Forma de Detalle ---</option>
						 <option value="M"  <%=fdM%>>Mensual</option>
					     <option value="B"  <%=fdB%>>Bimestral</option>
					     <option value="T"  <%=fdT%>>Trimestral</option>
					     <option value="S"  <%=fdS%>>Semestral</option>
					     </select>
					</td>
				</tr>
				
				
				<tr>
					<td class="etabla">Moneda de Presupuesto:</td>
					
					<%
						mS=""
						mD=""
						if strMoneda_Pto="S" then mS="selected"
						if strMoneda_Pto="D" then mD="selected"
					
					%>
					<td><select name="cboMon" id="cboMon">
						 <option value="0">--- Seleccione Moneda ---</option>
						 <option value="S"  <%=mS%>>Soles</option>
					     <option value="D"  <%=mD%>>Dolares</option>
					     </select>
					</td>
				</tr>
				
				<tr>
					<td class="etabla">provisión de Morosidad (%):</td>
					<td><input Type"text" name="txtProMor" size="4" value="<%=dblProvisionMorosidad_Pto%>"></td>
					
				</tr>
				
				<tr>
					<td class="etabla">Recuperacion de Morosidad (%):</td>
					<td><input Type"text" name="txtRecMor" size="4" value="<%=dblRecuperacionMorosidad_Pto%>"></td>
					
				</tr>
				
				<tr>
					<td class="etabla">Saldo de Caja Año Anterior:</td>
					<td><input Type"text" name="txtSal" size="10" value="<%=dblSaldoCajaAnoAnterior_Pto%>"></td>
					
				</tr>
				
				<tr>
				<%
					
					sel=""
					
					if strEstado_Pto="1" then sel="checked"
					
				
				%>
					<td class="etabla">Vigencia:</td>
					<td>
                    <input type="checkbox" name="chkVigencia" <%=sel%> value="ON"></td>
					
				</tr>
				
				
			</table>

			<table align="center" width="80%">
			<tr>
			  <td colspan="2" align="right">
			    <input type="submit" value="Grabar" name="cmdGrabar" class="guardar">
			    <input type="button" value="Cancelar" name="cmdCancelar" class="salir"  onClick=location="frmParametro.asp?tip=C">
			  </td>
			    
			  
			</tr>
			</table>
	</form>
<%end if%>

<%

'strRpta=trim(Request.QueryString("rpta")) ' Para Indicar Si No se Puede Eliminar

'if strRpta <>"" then
'	if strRpta="0" then%>
	<script language="JavaScript">
		//alert("No se Puede Eliminar Por que ya Existen Presupuestos Registrados Para este año") 
	</script>
	
	<%'end if


'end if
%>


</body>
</html>