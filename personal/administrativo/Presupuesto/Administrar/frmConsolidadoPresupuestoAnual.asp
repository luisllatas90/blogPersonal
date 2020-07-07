<%
'Creamos Objetos a Utilizar en toda la Pagina

 Set objPresupuesto = Server.CreateObject("PryUSAT.clsDatPresupuesto")
 Set rsPresupuesto=Server.CreateObject("ADODB.Recordset")
 Set rsCentroCosto=Server.CreateObject("ADODB.Recordset")
 Set rsTipo=Server.CreateObject("ADODB.Recordset")
 Set rsConcepto=Server.CreateObject("ADODB.Recordset")
 Set rsPlan=Server.CreateObject("ADODB.Recordset")
 Set rsArticulo=Server.CreateObject("ADODB.Recordset")


strmod = Request.QueryString("est")  



'---Recuperar Datos del Año------------
strAno=Request.QueryString("txtParametro")  
if strAno="" then
   strAno=request.QueryString("ano")
   if strAno="" then
   		strAno="2006" 
   end if
end if
'-------------------------------------------



'----Recuperar Estado de Consulta----------------

strTipoConsulta=Request.QueryString("cboEstado")  
if trim(strTipoConsulta)="" then
	strTipoConsulta=Request.QueryString("tcon")
	if trim(strTipoConsulta)="" then
		strTipoConsulta="P"
	end if
end if

if strTipoConsulta="2" then seleccionar2="selected"
if strTipoConsulta="A" then seleccionarA="selected"
if strTipoConsulta="P" then seleccionarP="selected"
'----------------------------------------------------


 '-- Recuperar Centro de Costo----------------------
	codCentroCosto= Request.QueryString("cboCentroCosto")  
	if codCentroCosto="" then
		codCentroCosto=Request.QueryString("tCC")
	end if 
'---------------------------------------------------

%>

<html>
<head>
	<title>Aprobar o Desaprobar Presupuestos</title>	
	<link rel="stylesheet" type="text/css" href="../private/estilo.css">
	<script language="JavaScript" src="../private/funciones.js"></script>
	<script language="Javascript">	
	
		function validaSubmite(){ 
			var cboparametro=document.frmParametro.cboCentroCosto
		
		    if (document.frmParametro.txtParametro.value == "") 
			{
       		alert("Debe Ingresar al año a cosnultar") 
				frmParametro.txtParametro.focus();
				return (false);	
   			}

	      if (document.frmParametro.txtParametro.value.length < 4) 

			{
       		alert("Debe Ingresar al año a cosnultar correctamente") 
				frmParametro.txtParametro.focus();
				return (false);	
   			}
			
			if (cboparametro!=undefined){
			    if (cboparametro.value == "0"){
       				alert("Debe seleccionar el centro de costos") 
					frmParametro.cboCentroCosto.focus();
					return (false)
				}
		   }
   
   	      	return (true);	
   	      	
   	      	
		} 

	
		function eliminaritem()
		{
			if (confirm("¿Está seguro que desea eliminar los items seleccionados?")==true){
				return(true)
			}
			else{
				return(false)
			}			
		}
		
		function enviar(){
			var cboparametro=document.frmParametro.cboCentroCosto
			
			if (cboparametro!=undefined){
				document.frmParametro.cboCentroCosto.value="0"
			}	
				document.frmParametro.action = "frmConsolidadoPresupuestoAnual.asp" 
				document.frmParametro.submit();
			
		}
		
		
		function Refrescar() {
			var cboparametro=document.frmParametro.cboCentroCosto
			
			Ctrl = frmParametro.txtParametro;

			if (Ctrl.value.length == 4) {
				if (event.keyCode!=37 && event.keyCode!=38 && event.keyCode!=39 && event.keyCode!=40)  {
					if (cboparametro!=undefined){
						document.frmParametro.cboCentroCosto.value="0"
					}
					    document.frmParametro.action = "frmConsolidadoPresupuestoAnual.asp" 
				    	document.frmParametro.submit();
				    
				} 
			}
		}


	</script>
</head>

<body>
<!-- Titulo de la Pagina-->
<table align="center" width="100%">
  <tr>
      <th align="center" class="table">APROBAR O DESAPROBAR PRESUPUESTOS</th>
  </tr>
  <tr>
	<td><hr></td>
  </tr>
</table>
<!-- Fin de Titulo de la Pagina -->
<P>

<!-- Parametros de Consulta  -->
<form onSubmit="return validaSubmite()" name="frmParametro" method="get" action="frmconsolidadopresupuestoAnual.asp">

<table width="100%" height="48">
 <tr>
   <td width="40%" height="16"> 
  	  <table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="491">
          <tr>
	       <td width="37" class="etabla">Año:</td>
	   		<td width="42" align="center"><input type="text" name="txtParametro" size="4" maxlength="4" value="<%=strAno%>" onKeyPress="validarnumero()" onKeyUp="Refrescar()"></td>
			<td width="129">Estado:
			 <select name="cboEstado" id="cboEstado" onChange="enviar()">
	          	<option value="P" <%=seleccionarP%> >No Aprobados</option>
	          	<option value="A" <%=seleccionarA%>>Aprobados</option>
	          	<option value="2" <%=seleccionar2%>>Ambos Estados</option>
		    </select> 
			</td>
			<td width="254">
			       
			    <%
			 	
				Set rsCentroCosto= objPresupuesto.ConsultarRubrosEjecucionPresupuesto ("RS","AP",strAno,strTipoConsulta,"","")
			
				if rsCentroCosto.recordCount >0 then
			    %>   
					Centro de Costo:  
						<select name="cboCentroCosto" id="cboCentroCosto">
	          				<option value="0">---Seleccione Centro de Costos--- </option>
							<% do while not rsCentroCosto.eof 
			  						seleccionar="" 
			    					if (cint(codCentroCosto)=rsCentroCosto(0)) then seleccionar="SELECTED " %>
		          					<option value= "<%=rsCentroCosto(0)%>" <%=seleccionar%>> <%=rsCentroCosto("descripcion_Cco")%></option>
		          					<% rsCentroCosto.movenext
							  loop
					 
					  %>
						</select>	
				<%else
				%>
					 No existe ningún centro de costos con ese estado
					 
				<%	
				end if
			   %>
	        			       
            </td>
		</tr>	
		<tr>
		<td width="21%" height="16" colspan=4 align=right> <input type="submit" value="Consultar" name="cmdConsultar" class="boton"> </td>	
		</tr>
	  </table>
   </td>
   
  
</tr>   
  
</table>
</form>
	
<!-- Fin de Parametros de la Consulta-->



<!--Empezamos a Construir la Estructura Jerarquica  -->

<%IF trim(strAno)<>"" then %>
    <!--I1: Creamos Tabla que Contendra Toda la Estructura --> 
	<table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
		  
		  <tr class="etabla"> 
		    <td>Item</td>
		    <td>Descripción</td>
		    <td>Ingresos (<%=strMoneda%>)</td>
		    <td>Egresos (<%=strMoneda%>)</td>
		    <td>Diferencia (<%=strMoneda%>)</td>
			<td width="1%">Est</td>
			<td width="10%">Apr/Des</td>
		  </tr>
		  <!-- Fin de Cabecera de la Tabla de Estructura-->
		 
		 
		 <!-- Datos del Centro de Costos   --> 
			
		   <% 		   
			Set rsCentroCosto= objPresupuesto.ConsultarRubrosPresupuesto("RS","AE",strAno,strTipoConsulta,codCentroCosto,"")
		   %>
            
			  <%
			  if rsCentroCosto.RecordCount >0 then
				lngCodigo_Cco = rsCentroCosto("codigo_Cco")
				strEstado_Cco=TRIM(rsCentroCosto("estado_Pto"))
				strCentro=trim(rsCentroCosto("descripcion_Cco"))
							   
				'Calcular Total Por Centro de Costos
				totalEgreso=formatNumber(rsCentroCosto("TotalEgreso"))
				totalIngreso=formatNumber(rsCentroCosto("TotalIngreso"))
				diferencia=formatNumber(totalIngreso - totalEgreso)
				 %>
			 	 
			 	 <tr class="Nivel0"> 
				   <%if cdbl(totalEgreso)=0 and cdbl(totalIngreso)=0 then %>
 			        <td align="center" width="1%">&nbsp;</td>
					 <td width="50%" ><%=strCentro%>&nbsp;</td>
					 <td width="15%" align="right"><%=totalIngreso%>&nbsp;</td>
					 <td width="15%" align="right"><%=totalEgreso%>&nbsp;</td>
					 <td width="15%" align="right"><%=diferencia%>&nbsp;</td>
					 <%if strEstado_Cco="P" then%>
					 <td align="center"  onclick="MostrarTabla(document.all.tblTipo<%=lngCodigo_Cco%>,imgTI<%=lngCodigo_Cco%>)" style="cursor:hand"><img border="0" src="../images/R.gif" align="baseline"></td>
					 <td align="center" ><a href="Procesos.asp?Tipo=U002&est=A&cco=<%=lngCodigo_Cco%>&ano=<%=strAno%>&tc=<%=strTipoConsulta%>"><img border="0" src="../images/editar.gif" align="baseline">Aprobar</a></td>
										
					 <%else%>
					 <td align="center"  onclick="MostrarTabla(document.all.tblTipo<%=lngCodigo_Cco%>,imgTI<%=lngCodigo_Cco%>)" style="cursor:hand"><img border="0" src="../images/A.gif" align="baseline"></td>
					 <td align="center" ><a href="Procesos.asp?Tipo=U002&est=P&cco=<%=lngCodigo_Cco%>&ano=<%=strAno%>&tc=<%=strTipoConsulta%>"><img border="0" src="../images/eliminar.gif" align="baseline">Desaprobar</a></td>
				     <%end if%>
				   			   
				   <%else%>
				     <td align="center"  width="1%" onClick="MostrarTabla(document.all.tblTipo<%=lngCodigo_Cco%>,imgTI<%=lngCodigo_Cco%>)" style="cursor:hand"><img id="imgTI<%=lngCodigo_Cco%>" src="../images/mas.gif" width="9" height="9"></td>
					 <td width="50%"  onclick="MostrarTabla(document.all.tblTipo<%=lngCodigo_Cco%>,imgTI<%=lngCodigo_Cco%>)" style="cursor:hand"><%=strCentro%>&nbsp;</td>
					 <td width="15%"  align="right" onClick="MostrarTabla(document.all.tblTipo<%=lngCodigo_Cco%>,imgTI<%=lngCodigo_Cco%>)" style="cursor:hand"><%=totalIngreso%>&nbsp;</td>
					 <td width="15%"  align="right" onClick="MostrarTabla(document.all.tblTipo<%=lngCodigo_Cco%>,imgTI<%=lngCodigo_Cco%>)" style="cursor:hand"><%=totalEgreso%>&nbsp;</td>
					 <td width="15%"  align="right" onClick="MostrarTabla(document.all.tblTipo<%=lngCodigo_Cco%>,imgTI<%=lngCodigo_Cco%>)" style="cursor:hand"><%=diferencia%>&nbsp;</td>
					 <%if strEstado_Cco="P" then%>
					 <td align="center"  onclick="MostrarTabla(document.all.tblTipo<%=lngCodigo_Cco%>,imgTI<%=lngCodigo_Cco%>)" style="cursor:hand"><img border="0" src="../images/R.gif" align="baseline"></td>
					 <td align="center" ><a href="Procesos.asp?Tipo=U002&est=A&cco=<%=lngCodigo_Cco%>&ano=<%=strAno%>&tc=<%=strTipoConsulta%>"><img border="0" src="../images/editar.gif" align="baseline">Aprobar</a></td>
										
					 <%else%>
					 <td align="center"  onclick="MostrarTabla(document.all.tblTipo<%=lngCodigo_Cco%>,imgTI<%=lngCodigo_Cco%>)" style="cursor:hand"><img border="0" src="../images/A.gif" align="baseline"></td>
					 <td align="center" ><a href="Procesos.asp?Tipo=U002&est=P&cco=<%=lngCodigo_Cco%>&ano=<%=strAno%>&tc=<%=strTipoConsulta%>"><img border="0" src="../images/eliminar.gif" align="baseline">Desaprobar</a></td>
  					 <%end if%>
                 <%end if%>
				 </tr>			

						  <%
						  Set rsTipo= objPresupuesto.ConsultarRubrosPresupuesto("RS","TI",strAno,lngCodigo_Cco,"","")
						  %>
						  <!-- Inicio de  Tipos-->
						  <%if rsTipo.RecordCount >0 then%>
							
							 <tr>
								<td colspan="7">
								   
								   <table width="100%" border="0" cellpadding="3" cellspacing="0" align="center" style="display:none"  id="tblTipo<%=lngCodigo_Cco%>">			
										
										<!-- Iniciamo el Bucle del Tipo de Presupuesto I/E-->
										<%Do while not rsTipo.EOF 
											strTipo_Pto=trim(rsTipo("tipo_Pto")) %>
													  
											<tr class="Negrita" onClick="MostrarTabla(document.all.tblCentro<%=lngCodigo_Cco%><%=strTipo_Pto%>,imgCC<%=lngCodigo_Cco%><%=strTipo_Pto%>)"> 
											    <td width="3%">&nbsp;</td>
												<td align="center" width="1%"  style="cursor:hand"><img id="imgCC<%=lngCodigo_Cco%><%=strTipo_Pto%>" src="../images/mas.gif" width="9" height="9"></td>
												<td colspan="5" style="cursor:hand"><%=trim(rsTipo("tipo"))%></td>	
											</tr>
											<%
											  
											  Dim lngCodigo_Cie
											  Set rsConcepto= objPresupuesto.ConsultarRubrosPresupuesto("RS","CO",strAno,lngCodigo_Cco,strTipo_Pto,"")
											%>	
											<!--  Inicio de Conceptos -->
													
											  <%if rsConcepto.recordcount >0 then
												bytContar=0 %>
												
												<tr>
												   <td colspan="7">
												      <table   width="100%" border="0" cellpadding="3" cellspacing="0" align="center" style="display:none" id="tblCentro<%=lngCodigo_Cco%><%=strTipo_Pto%>">			
														<%do while not rsConcepto.EOF 
															lngCodigo_Cie = rsConcepto("codigo_Cie")
															TotalPorConcepto=cdbl(rsConcepto("total"))
															
														 	 if TotalPorConcepto >0 then%>
														 		
																<tr class="Nivel2" onClick="MostrarTabla(document.all.tblCuenta<%=lngCodigo_Cco%><%=lngCodigo_Cie%>,imgCO<%=lngCodigo_Cco%><%=lngCodigo_Cie%>)" style="cursor:hand"> 
																	<td width="3%">&nbsp;</td>
																	<td width="2%">&nbsp;</td>    
																	<td align="center" width="1%"><img id="imgCO<%=lngCodigo_Cco%><%=lngCodigo_Cie%>" src="../images/mas.gif" width="9" height="9"></td>
														 	 <%else%>				
																<tr class="Nivel2"> 
																	<td width="3%">&nbsp;</td>
																	<td width="2%">&nbsp;</td>
																	<td width="1%">&nbsp;</td>
															 <%end if%>
															
																  <td width="80%"><%=rsConcepto("descripcion_Cie") %></td>
																  <td width="15%" align="right"><%=formatNumber(TotalPorConcepto)%></td>
																  <td colspan="2">&nbsp;</td>
															 
																</tr>
																 <%
																	Set rsPlan= objPresupuesto.ConsultarRubrosPresupuesto("RS","PC",strAno,lngCodigo_Cco,lngCodigo_Cie,strTipo_Pto)
																  %>
																  <!-- Inicio de Cuentas-->
																
																	<% if rsplan.recordcount>0 then%>
																		<tr>
																		   <td colspan="7">
																		      <table  width="100%" border="0" cellpadding="2" cellspacing="0" align="center" style="display:none"  id="tblCuenta<%=lngCodigo_Cco%><%=lngCodigo_Cie%>">			
																			
																				<%do while not rsPlan.eof
																						lngCodigo_Pco=rsPlan("codigo_Pco")
																						bytContar=bytContar+1
																						
																						TotalPorCuenta=cdbl(rsPlan("total"))
																						strCuenta=trim(rsPlan("descripcionCuenta_Pco"))
																						
																						if TotalPorCuenta>0 then%>
																						 <tr class="Nivel3" onClick="MostrarTabla(document.all.tblDetalleCuenta<%=lngCodigo_Cco%><%=lngCodigo_Pco%>,imgDC<%=lngCodigo_Cco%><%=lngCodigo_Pco%>)" style="cursor:hand">
																								<td width="3%">&nbsp;</td>
																								<td width="3%">&nbsp;</td>
																								<td width="2%">&nbsp;</td>
																							   <td width="1%"><img id="imgDC<%=lngCodigo_Cco%><%=lngCodigo_Pco%>" src="../images/mas.gif" width="9" height="9">
																						 <%else %>
																						 <tr class="Nivel3">
																						 		<td width="3%">&nbsp;</td>
																								<td width="3%">&nbsp;</td>
																								<td width="2%">&nbsp;</td>
																							    <td width="1%">&nbsp;</td>
																						<%end if%>
																					
																							<td width="50%" align="left"><%=strCuenta%></td>
																							<td align="right" width="20%"><%=formatNumber(TotalPorCuenta)%></td>
																							<td width="5%" colspan="3">&nbsp;</td>
																						</tr>
																						
																						<%
																							Set rsArticulo= objPresupuesto.ConsultarRubrosPresupuesto("RS","AR",strAno,lngCodigo_Cco,lngCodigo_Pco,strTipo_Pto)
																							bytContarArticulo=0%>
																							
																							<!-- Inicio de Articulos -->
																							
																							<% if rsArticulo.recordcount>0 then%>
																								<tr>
																								  <td colspan="7">
																									<table width="80%" border="0" cellspacing="0" align="center" style="display:none" id="tblDetalleCuenta<%=lngCodigo_Cco%><%=lngCodigo_Pco%>">
																									
																																														  
																										<form name="form<%=lngCodigo_Cco%><%=lngCodigo_Pco%>" method="post" action="Procesos.asp?Tipo=D003" onSubmit="return eliminaritem()">
																											<tr class="etabla">

																												<input type="hidden" name="txtTipoConsulta" value="<%=strTipoConsulta%>">
																												<input type="hidden" name="txtCC" value="<%=lngCodigo_Cco%>">
																												<input type="hidden" name="txtAno" value="<%=strAno%>">
																											    
																											<td>
																												<input type="submit" class="eliminar2" value="" name="cmd<%=lngCodigo_Cco%><%=lngCodigo_Pco%>" id="cmd<%=lngCodigo_Cco%><%=lngCodigo_Pco%>" height="1%">
																											</td>
																											    <!--<td align="center"> <input type="checkbox" name="chkmarcartodo" onClick="if (this.selected) {SeleccionarTodos(form<%=lngCodigo_Cco%><%=lngCodigo_Pco%>.chkEliminar)} else {QuitarTodos(form<%=lngCodigo_Cco%><%=lngCodigo_Pco%>.chkEliminar)}" value="ON"></td>-->
																											    <td>&nbsp;</td>
																											    <td>Item</td>
																											    <td width="50%">Detalle</td>
																											    <td >Unidad</td>
																											    <td >Precio</td>
																											    <td >Cant</td>
																											    <td >SubTotal</td>
																										    </tr>				
																												
																												<%do while not rsArticulo.eof
																													lngCodigo_Art=rsArticulo("Codigo_Art")
																													lngCodigo_Pto=trim(rsArticulo("Codigo_Pto"))
																													bytContarArticulo=bytContarArticulo+1%>
																													
																													<tr align="center" onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)" style="cursor:hand">
																								
																													   <td>&nbsp;</td>
																													   <td><input type="checkbox" name="chkEliminar" id="chkEliminar" value="<%=lngCodigo_Pto & "-" & trim(lngCodigo_Art)%>"></td>
																													   
																													   
																													<!--<a href="frmregistrardetallepresupuestoAnual.asp?tpto=<%=strTipo_Pto%>&Cent=<%=strCentro%>&ca=<%=lngCodigo_Art%>&cp=<%=lngCodigo_Pto%>&da=<%=rsArticulo("descripcion_art")%>&pa=<%=rsArticulo("precioUnitario_Dpr")%>&ua=<%=rsArticulo("descripcion_Uni")%>&ct=<%=strCuenta%>&ta=<%=rsArticulo("funcion_art")%>&tc=<%=strTipoConsulta%>">-->
																													   <td onClick="AbrirPopUp('frmregistrardetallepresupuestoAnual.asp?tpto=<%=strTipo_Pto%>&Cent=<%=strCentro%>&ca=<%=lngCodigo_Art%>&cp=<%=lngCodigo_Pto%>&da=<%=rsArticulo("descripcion_art")%>&pa=<%=rsArticulo("precioUnitario_Dpr")%>&ua=<%=rsArticulo("descripcion_Uni")%>&ct=<%=strCuenta%>&ta=<%=rsArticulo("funcion_art")%>&tc=<%=strTipoConsulta%>','520','600','no','no','no',100,250)"><%=bytContarArticulo%></td>
																													   <td align="left" onClick="AbrirPopUp('frmregistrardetallepresupuestoAnual.asp?tpto=<%=strTipo_Pto%>&Cent=<%=strCentro%>&ca=<%=lngCodigo_Art%>&cp=<%=lngCodigo_Pto%>&da=<%=rsArticulo("descripcion_art")%>&pa=<%=rsArticulo("precioUnitario_Dpr")%>&ua=<%=rsArticulo("descripcion_Uni")%>&ct=<%=strCuenta%>&ta=<%=rsArticulo("funcion_art")%>&tc=<%=strTipoConsulta%>','520','600','no','no','no',100,250)"><%=rsArticulo("descripcion_art")%></td>
																													   <td onClick="AbrirPopUp('frmregistrardetallepresupuestoAnual.asp?tpto=<%=strTipo_Pto%>&Cent=<%=strCentro%>&ca=<%=lngCodigo_Art%>&cp=<%=lngCodigo_Pto%>&da=<%=rsArticulo("descripcion_art")%>&pa=<%=rsArticulo("precioUnitario_Dpr")%>&ua=<%=rsArticulo("descripcion_Uni")%>&ct=<%=strCuenta%>&ta=<%=rsArticulo("funcion_art")%>&tc=<%=strTipoConsulta%>','520','600','no','no','no',100,250)"><%=rsArticulo("descripcion_Uni")%></td>
																													   <td onClick="AbrirPopUp('frmregistrardetallepresupuestoAnual.asp?tpto=<%=strTipo_Pto%>&Cent=<%=strCentro%>&ca=<%=lngCodigo_Art%>&cp=<%=lngCodigo_Pto%>&da=<%=rsArticulo("descripcion_art")%>&pa=<%=rsArticulo("precioUnitario_Dpr")%>&ua=<%=rsArticulo("descripcion_Uni")%>&ct=<%=strCuenta%>&ta=<%=rsArticulo("funcion_art")%>&tc=<%=strTipoConsulta%>','520','600','no','no','no',100,250)"><%=formatNumber(rsArticulo("precioUnitario_Dpr"))%></td>
																													   <td onClick="AbrirPopUp('frmregistrardetallepresupuestoAnual.asp?tpto=<%=strTipo_Pto%>&Cent=<%=strCentro%>&ca=<%=lngCodigo_Art%>&cp=<%=lngCodigo_Pto%>&da=<%=rsArticulo("descripcion_art")%>&pa=<%=rsArticulo("precioUnitario_Dpr")%>&ua=<%=rsArticulo("descripcion_Uni")%>&ct=<%=strCuenta%>&ta=<%=rsArticulo("funcion_art")%>&tc=<%=strTipoConsulta%>','520','600','no','no','no',100,250)"><%=rsArticulo("cantidadTotal_Dpr")%></td>
																													   <td align="right" onClick="AbrirPopUp('frmregistrardetallepresupuestoAnual.asp?tpto=<%=strTipo_Pto%>&Cent=<%=strCentro%>&ca=<%=lngCodigo_Art%>&cp=<%=lngCodigo_Pto%>&da=<%=rsArticulo("descripcion_art")%>&pa=<%=rsArticulo("precioUnitario_Dpr")%>&ua=<%=rsArticulo("descripcion_Uni")%>&ct=<%=strCuenta%>&ta=<%=rsArticulo("funcion_art")%>&tc=<%=strTipoConsulta%>','520','600','no','no','no',100,250)"><%=formatNumber(rsArticulo("total_Dpr"))%></td>
																													<!--</a>-->
													  
																													</tr>
																													<%rsArticulo.movenext
																												Loop%>		
																					
																									    </form>
																									</table>
																								  </td>
																								</tr>
																							<%end if
																							
																							%>
																							<!-- Fin de Articulos-->
																					<%rsPlan.movenext
																				Loop%>		
																			  </table>
																		   </td>
																		</tr>
																	<%end if
																	  
																	  %>
																	<!-- Fin de Cuentas-->
																  <%rsConcepto.movenext
																loop%>
													  </table>
												   </td>
												</tr>
												 
											  <%end If %> 						
											<!-- Fin de Conceptos--> 
											<%rsTipo.MoveNext 
										loop%>
										<!-- Fin de Bucle de Tipos-->
								 </table>
								</td>
							  </tr>
							 
						 <%end if   %>
						 
				         <!-- Fin de Tipos--> 	
		  	    <%End if %>	
		<!-- Fin  de Datos de Centro de Costos   --> 
   </table>
   <!--F1: Cerramos Tabla que Contiene Toda la Estructura  -->
<%End if

 Set rsPresupuesto=Nothing
 Set rsCentroCosto=Nothing
 Set rsTipo=Nothing
 Set rsConcepto=Nothing
 Set rsPlan=Nothing
 Set rsArticulo=Nothing
 Set objPresupuesto = Nothing



if strmod="A" then%>
			
			<script>
				alert ("EL PRESUPUESTO FUE APROBADO");
			</script>
			

<%end if


if strmod="P" then%>
			
			<script>
				alert ("EL PRESUPUESTO FUE DESAPROBADO");
			</script>
			

<%end if%>

</body>
</html>