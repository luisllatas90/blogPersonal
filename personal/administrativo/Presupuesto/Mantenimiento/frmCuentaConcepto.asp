<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<html>
<head>
	<title>Amarrar Cuentas a Conceptos</title>
	<link rel="stylesheet" type="text/css" href="../private/estilo.css">
	<script language="JavaScript" src="../private/funciones.js"></script>

	<script language="JavaScript">
		function Buscar(){

		document.frmBuscarCuenta.action = "frmCuentaConcepto.asp?tipo=B" 
		document.frmBuscarCuenta.submit();
		}
		function Mostrar(){

		document.frmBuscarCuenta.action = "frmCuentaConcepto.asp?tipo=M" 
		document.frmBuscarCuenta.submit();
		}

	</script>

</head>
<body>
<%
Dim objPlanContable
Dim rsPlanContable
Dim lngCodigo_Cie
Dim strTipoBus
Dim strParam
Dim strTipo

lngCodigo_Cie= request.form("cboConcepto")
IF lngCodigo_Cie="" THEN
	lngCodigo_Cie=request.QueryString("CodCie")
END IF
strTipoBus= request.form("cboTipoBus")
strParam= request.form("txtParametro")
strTipo=request.QueryString("tipo")

'response.Write(lngCodigo_Cie)
'response.Write("<br>")
'response.Write(strTipoBus)
'response.Write("<br>")
'response.Write(strParam)

%>
<table align="center" width="80%">
<tr>
	<th align="center" class="table" align="center">RELACIONAR CONCEPTOS CON CUENTAS</th>
</tr>
 <tr>
	<td><hr></td>
 </tr>

</table>

<!-- Menu de Opciones -->
<table width="80%" align="center">
<tr>
	  <%if lngCodigo_Cie="" then 
		lngCodigo_Cie="0"
	  end if%>

	<td width="24%" align="left"><A href="frmConceptoIngresoEgreso.asp" style="text-decoration:none" ><img border="0" src="../images/nuevo.gif" align="baseline" width="16" height="16"> Agregar Nuevo Concepto</A>&nbsp; </td>
	<td width="25%" align="left"><A href="frmCuentaConcepto.asp?tipo=M&CodCie=<%=lngCodigo_Cie%>"style="text-decoration:none" ><img border="0" src="../images/previo.gif" align="baseline" width="16" height="16"> Mostrar Cuentas Asignadas</A></td>
	<td width="30%" align="left" valign="bottom"><A href="frmCuentaConcepto.asp?tipo=B&CodCie=<%=lngCodigo_Cie%>" style="text-decoration:none"><img border="0" src="../images/editar.gif" align="baseline" width="18" height="13"> Asignar Cuentas</A></td>
	<td width="5%">&nbsp;</td>

</tr>
<tr>
<td colspan="3" width="663">---------------------------------------------------------------------------------------------------------------------</td>
</tr>
</table>
<!-- Fin de Menu-->


<form name="frmBuscarCuenta" method="post" action="">
		  <table width="80%" align="center">
		    <tr><td>
			<table width="70%" align="left" border=" cellpadding="1" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" 3">
					<tr> 
					  <td width="30%" class="etabla"  >Concepto:</td>
					  <td width="70%" colspan="4"> 
							<%
								Dim objConcepto
								Dim rsConcepto
								
								Set objConcepto= Server.CreateObject("PryUSAT.clsDatConceptoIngresoEgreso")
								Set rsConcepto= Server.CreateObject("ADODB.RecordSet")
								Set rsConcepto= objConcepto.ConsultarConceptoIngresoEgreso ("RS","TO","","")
								%>
							<select name="cboConcepto" id="cboConcepto" onChange="Mostrar()">
							  <option value="0">---Seleccione Concepto Para Cuentas--- </option>
							  <% do while not rsConcepto.eof 
										seleccionar="" 
									if (cint(lngCodigo_Cie)=rsConcepto(0)) then seleccionar="SELECTED " %>
							  <option value= "<%=rsConcepto(0)%>" <%=seleccionar%>> <%=rsConcepto("descripcion_Cie")%></option>
							  <% rsConcepto.movenext
									loop
									rsConcepto.Close
									Set rsConcepto=Nothing 
									set objConcepto= Nothing
								   %>
							</select>
						
					  </td>
					</tr>
			</table>
			</td></tr>
		</table>

  
 <%IF trim(strTipo)="B" THEN%>
 			<br>
			<table align="center" width="80%" border="0">
				<tr>
					<td width="20%" class="etabla2" >Buscar Cuenta Segun:</td>
					<td width="70%" align="left" colspan="4">
						
						<select name="cboTipoBus">
						  <% 
							SLC=""	  
							SDC=""			  
							if trim(strTipoBus)="LC" THEN  SLC="Selected"
								
							if trim(strTipoBus)="DC" THEN  SDC="Selected"
						  %>	
						  <option value="LC" <%=SLC%>>Numero de Cuenta</option>
						  <option value="DC" <%=SDC%>>Descripcion de Cuenta</option>
						</select>
					</td>
					
					<tr>
						<td width="20%" class="etabla2" >Parametro de Consulta:</td>
						<td width="70%"  colsPan="4" align="left">
                        <input type="text" name="txtParametro" value="<%=strParam%>" size="20"><input type="button" class="buscar" name="cmdBuscar" value="" onClick="Buscar()"></td>
					    
					</tr>
					
			</table>
<%END IF%>
</form> 
<% if trim(strTipo)="B" THEN%>
		<%if trim(strParam)<>"" and trim(strTipoBus)<>"" then 
					
					Set objPlanContable= Server.CreateObject("PryUSAT.clsDatPlanContable")
					Set rsPlanContable= Server.CreateObject("ADODB.RecordSet")
					Set rsPlanContable= objPlanContable.ConsultarPlanContable ("RS",strTipoBus,strParam,"")%>
					
					<table align="center" width="80%">
						<tr><td align="center"> <b> Resultado de la Busqueda</b> </td></tr>
					</table>
		
		
					<%if rsPlanContable.recordcount >0 then
						'Se Encontro registros de la Busqueda%>
						<form name="frmRelacionarCuenta" method="post" action="Procesos.asp?Tipo=U004">
							<input type="hidden" name="txtCodigo_Cie" value="<%=lngCodigo_Cie%>">
						   <table width="80%" align="center" border="1" cellpadding="1" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" >
							<tr class="etabla">	
								
								<td>Numero de Cuenta</td>
								<td>Descripcion de Cuenta</td>
								<td>Concepto</td>
								<td width="20%"><input type="checkbox" name="chkmarcartodo" onClick="if (this.checked) {SeleccionarTodos(chkCuenta)} else {QuitarTodos(chkCuenta)}" value="ON" ><input type="submit" value="Asignar" class=""> </td>
							</tr>
							 <%
								do while not rsPlanContable.eof%>
									<tr onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)">
										
										<td align="center"><font size="1"><%=rsPlanContable("numeroCuenta_Pco")%></font>&nbsp;</td>
										<td align="left"><font size="1"><%=rsPlanContable("descripcionCuenta_Pco")%></font>&nbsp;</td>
										<td align="left"><font size="1"><%=rsPlanContable("descripcion_Cie")%></font>&nbsp;</td>
										<%
											habilitar=""
											
											if trim(rsPlanContable("descripcion_Cie"))<>"" AND  trim(rsPlanContable("descripcion_Cie"))<>"NO DEFINIDO" then 
												habilitar="disabled"
											end if
											
										    if Not isnull(rsPlanContable("descripcion_Cie")) AND  trim(rsPlanContable("descripcion_Cie"))<>"NO DEFINIDO" then 
										       habilitar="disabled"
										       
										    End if %>
										<td align="center"><font size="1"><input type="checkbox"  name="chkCuenta" id="chkCuenta" value="<%=rsPlanContable("codigo_Pco")%>" <%=habilitar%>></font></td>
									
									</tr>
									<% rsPlanContable.movenext
								loop
								rsPlanContable.close
								set rsPlanContable=Nothing
								set objPlanContable=Nothing
							 %>
						 </table>
					
						</form> 
		
					
					<%else
						'No se encontro Registros%>
						<table align="center">
							<tr>
								<td align="center">No se Encontro Ningun Registro para la Consulta especificada</td>
							</tr>
						</table>
					<%end if
		
					%>
					
					
				
		<%end if
 END IF
 IF trim(strTipo)="M" THEN
	Set objPlanContable= Server.CreateObject("PryUSAT.clsDatPlanContable")
	Set rsPlanContable= Server.CreateObject("ADODB.RecordSet")
	Set rsPlanContable= objPlanContable.ConsultarPlanContable ("RS","CO",lngCodigo_Cie,"")
	%>

	<form method="post" name="frmQuitarRelacion" action="Procesos.asp?Tipo=U005">
	
	<table width="80%" align="center" border="1" cellpadding="1" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" >
	<tr class="etabla">	
		
		<td width="20%">Numero de Cuenta</td>
		<td width="60%">Descripcion de Cuenta</td>
		<%if rsPlanContable.recordcount >0 then%>
			<td width="10%"><input type="checkbox" name="chkmarcartodo" onClick="if (this.checked) {SeleccionarTodos(chkCuenta2)} else {QuitarTodos(chkCuenta2)}" value="ON" ><input type="submit" value="Desasignar" class=""> </td>
		<%else%>
			<td width="10%"><input type="checkbox" name="chkmarcartodo"  value="ON" ><input type="submit" value="Desasignar" class="" id=submit1 name=submit1> </td>
		
		<%end if%>
	</tr>
	<%
	do while not rsPlanContable.eof%>
		<tr onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)">
			
			<td align="center"><font size="1"><%=rsPlanContable("numeroCuenta_Pco")%></font>&nbsp;</td>
			<td align="left"><font size="1"><%=rsPlanContable("descripcionCuenta_Pco")%></font>&nbsp;</td>
			<td align="center" ><font size="1"><input type="checkbox"  name="chkCuenta2" id ="chkCuenta2"value="<%=rsPlanContable("codigo_Pco")%>"></font></td>
		
		</tr>
		<% rsPlanContable.movenext
	loop
	rsPlanContable.close
	set rsPlanContable=Nothing
	set objPlanContable=Nothing
    %>
  </table>
  	<input type="hidden" name="txtCodigo_Cie2" value="<%=lngCodigo_Cie%>">
  </form>

<%END IF%>
</body>
</html>