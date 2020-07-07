<%
'Creamos Objetos a Utilizar en toda la Pagina

 Set objPresupuesto = Server.CreateObject("PryUSAT.clsDatPresupuesto")
 Set rsCentroCosto=Server.CreateObject("ADODB.Recordset")
 
 
 set rsTotal =Server.CreateObject("ADODB.Recordset")
 Set rsConcepto=Server.CreateObject("ADODB.Recordset")
 Set rsPlan=Server.CreateObject("ADODB.Recordset")
 Set rsArticulo=Server.CreateObject("ADODB.Recordset")


strmod = Request.QueryString("est")  



'---Recuperar Datos del Año------------
strAno_Pto=Request.QueryString("txtParametro")  
if strAno_Pto="" then
   strAno_Pto=request.QueryString("ano")
   if strAno_Pto="" then
   		strAno_Pto= year(date()) '"2006" 
   end if
end if
'-------------------------------------------



'----Recuperar Estado de Consulta----------------

strTipoConsulta=Request.QueryString("cboEstado")  
if trim(strTipoConsulta)="" then
	strTipoConsulta=Request.QueryString("tcon")
	if trim(strTipoConsulta)="" then
		strTipoConsulta="2"
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
	
	lngCodigo_Cco= codCentroCosto
'---------------------------------------------------
strMoneda="S/."
strTipo_Pto="E"

'Obtenemos El Tipo de Funcion del Usuario, Para Determinar si es Adminiatrador o No
lngCodigo_Tfu = Session("codigo_Tfu")


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

	
		
		function enviar(){
			var cboparametro=document.frmParametro.cboCentroCosto
			
			if (cboparametro!=undefined){
				document.frmParametro.cboCentroCosto.value="0"
			}	
				document.frmParametro.action = "frmEjecutadoJerarquico.asp" 
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
					    document.frmParametro.action = "frmEjecutadoJerarquico.asp" 
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
      <th align="center" class="table">PRESUPUESTO EJECUTADO CLASIFICADO</th>
  </tr>
  <tr>
	<td><hr></td>
  </tr>
</table>
<!-- Fin de Titulo de la Pagina -->
<P>

<!-- Parametros de Consulta  -->
<form onSubmit="return validaSubmite()" name="frmParametro" method="get" action="frmEjecutadoJerarquico.asp">

<table width="100%" height="95">
 <tr>
   <td width="34%" height="56"> 
  	  <table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="491">
          <tr>
	       <td width="37" class="etabla">Año:</td>
	   		<td width="42" align="center"><input type="text" name="txtParametro" size="4" maxlength="4" value="<%=strAno_Pto%>" onKeyPress="validarnumero()" onKeyUp="Refrescar()"></td>
			
			<% 'if lngCodigo_Tfu ="1" or lngCodigo_Tfu ="35" or lngCodigo_Tfu ="38" or lngCodigo_Tfu ="6" then '---------Puede Ver todos los Centros de costo%>
			 <!--<td width="129">Estado:
			 <select name="cboEstado" id="cboEstado" onChange="enviar()">
	
	          	<option value="2" <%'=seleccionar2%>>Ambos Estados</option>
   	          	<option value="A" <%'=seleccionarA%>>Aprobados</option>
  	          	<option value="P" <%'=seleccionarP%> >No Aprobados</option>

		    </select> 
			</td> -->
			<%'End if%>			
			
			
			<td width="254">
			     
				<%
						Set objCentroCosto=Server.CreateObject("PryUSAT.clsDatCentroCosto")
	
				      if lngCodigo_Tfu ="27"  or lngCodigo_Tfu ="1" or lngCodigo_Tfu ="35" or lngCodigo_Tfu ="38" or lngCodigo_Tfu ="6" then '---------Puede Ver todos los Centros de costo
						'Administrador, consultor presupuesto, contador y Adm. General
						Set rsCentroCosto= objCentroCosto.ConsultarCentroCosto ("RS","TO","")
						
	      	      	    'Este codigo comentado permite ver de todos		 	
					    'Set rsCentroCosto= objPresupuesto.ConsultarRubrosEjecucionPresupuesto ("RS","AP",strAno_Pto,strTipoConsulta,"","")


				      else 'Solo los centro de costo a los que pertenece
							Set rsCentroCosto= objCentroCosto.ConsultarCentroCosto ("RS","CP",Session("codigo_Usu"))
				      end if 	 	
			
				if rsCentroCosto.recordCount >0 then
			    %>   
					Centro de Costo:  
						<select name="cboCentroCosto" id="cboCentroCosto">
	          				<option value="0">---Seleccione Centro de Costos--- </option>
							<% do while not rsCentroCosto.eof 
			  						seleccionar="" 
			    					if (cint(codCentroCosto)=rsCentroCosto("codigo_Cco")) then seleccionar="SELECTED " %>
		          					<option value= "<%=rsCentroCosto("codigo_Cco")%>" <%=seleccionar%>> <%=rsCentroCosto("descripcion_Cco")%></option>
		          					<% rsCentroCosto.movenext
							  loop
					 
					  %>
						</select>	
				<%else
				%>
					 No existe ningún centro de costos con ese estado
					 
				<%	
				end if
				
				rsCentroCosto.close
				set rsCentroCosto = Nothing
				set objPresupuesto= Nothing
			   %>
	        			       
            </td>
	  </tr>	
	  </table>
   </td> 
   <td width="3%" height="56"> &nbsp;</td>
   </tr> 
   <tr>
   	<td>
    <input type="submit" value="Consultar" name="cmdConsultar" class="boton" style="float: left"></td>
  </tr>  
   <tr>
   <td height="14"><font color="#800000"><b>Nota:</b></font></font> 
    El presupuesto ejecutado se esta calculando en base a todas las salidas de 
    almacén registradas para el centro de costo.</td>
   </tr>
</table>
</form>
	
<!-- Fin de Parametros de la Consulta-->



<!--Empezamos a Construir la Estructura Jerarquica  -->

<%

IF trim(strAno_Pto)<>"" and lngCodigo_Cco<>"" And lngCodigo_Cco <> 0 then %>
    <!--I1: Creamos Tabla que Contendra Toda la Estructura --> 
	<table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
		  
		  <tr class="etabla"> 
		    <td width="25">Item</td>
		    <td width="525">Descripción</td>
		    <td width="132">Presupuestado (<%=strMoneda%>)</td>
		    <td width="114">Ejecutado (<%=strMoneda%>)</td>
		    <td width="128">Diferencia (<%=strMoneda%>)</td>
		  </tr>
		  <!-- Fin de Cabecera de la Tabla de Estructura-->
		 
		
		<%
		Set cn = Server.CreateObject("pryUSAT.clsAccesoDatos")
		cn.AbrirConexion
			set rsTotal = cn.consultar("ConsultarEjecutadoJerarquico","FO","0", lngCodigo_Cco,strAno_Pto,0,0)
		
		%>
		
			<tr class="nivel1">
				<td colspan=2 align="right"> TOTALES (S/.) --></td>
				<td align="right"><%=formatNumber(rstotal("totalPre"),4)%>&nbsp;</td>
				<td align="right"><%=formatNumber(rstotal("totalEje"),4)%>&nbsp;</td>
				
				<%
				if cdbl(rstotal("Diferencia"))<0 then
					color = "red"
				else
					color = ""
				end if
				%>
				<td align="right" bgcolor = <%=color%>><%=formatNumber(rstotal("Diferencia"),4)%>&nbsp;</td>

			</tr>
						<!--  Inicio de Conceptos -->
													
											  <%
											  
											  set rsConcepto = cn.consultar("ConsultarEjecutadoJerarquico","FO","1", lngCodigo_Cco,strAno_Pto,0,0)											  

											  if rsConcepto.recordcount >0 then
											  	
											  
												bytContar=0 %>
												
												<!--<tr>-->
												   <!--<td colspan="5">-->
												      <!--<table   width="100%" border="0" cellpadding="3" cellspacing="0" align="center" style="display:none" id="tblCentro<%'=lngCodigo_Cco%><%'=strTipo_Pto%>">-->			
														<%do while not rsConcepto.EOF 
															lngCodigo_Cie = rsConcepto("codigo_Cie")
															TotalPre=cdbl(rsConcepto("totalPre"))
															TotalEje=cdbl(rsConcepto("totalEje"))

															
														 	 if ((TotalPre >0) or (TotalEje >0)) then
														 	 	
														 	 		%>
														 		  
																<tr class="Nivel2" onClick="MostrarTabla(document.all.tblCuenta<%=lngCodigo_Cco%><%=lngCodigo_Cie%>,imgCO<%=lngCodigo_Cco%><%=lngCodigo_Cie%>)" style="cursor:hand"> 
																	
																	<td align="center" width="25"><img id="imgCO<%=lngCodigo_Cco%><%=lngCodigo_Cie%>" src="../images/mas.gif" width="9" height="9"></td>
														 	 <%else
														 	       
														 	 	%>				
																<tr class="Nivel2"> 
																	<td width="25">&nbsp;</td>
															 <%end if%>
															
																  <td width="525" align="left"><%=rsConcepto("descripcion_Cie") %>&nbsp;</td>
																  <td width="132" align="right"><%=formatNumber(TotalPre,4)%>&nbsp;</td>
																  <td width="114" align="right"><%=formatNumber(TotalEje,4)%>&nbsp;</td>
																  <td width="128" align="right"><%= formatNumber(rsConcepto("Diferencia"),4)%>&nbsp;</td>
															 
																</tr>
																 <%
																	
																	set rsPlan = cn.consultar("ConsultarEjecutadoJerarquico","FO","2", lngCodigo_Cco,strAno_Pto,lngCodigo_Cie,0)											  

																  %>
																  <!-- Inicio de Cuentas-->
																
																	<% if rsplan.recordcount>0 then%>
																		<tr>
																		   <td colspan="5">
																		      <table  width="100%" border="0" cellpadding="2" cellspacing="0" align="center" style="display:none"  id="tblCuenta<%=lngCodigo_Cco%><%=lngCodigo_Cie%>">			
																			
																				<%do while not rsPlan.eof
																						lngCodigo_Pco=rsPlan("codigo_Pco")
																						bytContar=bytContar+1
																						
																						TotalPorCuentaPre=cdbl(rsPlan("totalPre"))
																						TotalPorCuentaEje=cdbl(rsPlan("totalEje"))
																						strCuenta=trim(rsPlan("descripcionCuenta_Pco"))
																						
																						if ((TotalPorCuentaPre>0) or  (TotalPorCuentaEje>0)) then%>
																						 <tr class="Nivel3" onClick="MostrarTabla(document.all.tblDetalleCuenta<%=lngCodigo_Cco%><%=lngCodigo_Pco%>,imgDC<%=lngCodigo_Cco%><%=lngCodigo_Pco%>)" style="cursor:hand">
																								<td width="13">&nbsp;</td>
																								<td width="4">&nbsp;</td>
																								<td width="4">&nbsp;</td>
																							   <td width="9"><img id="imgDC<%=lngCodigo_Cco%><%=lngCodigo_Pco%>" src="../images/mas.gif" width="9" height="9">
																						 <%else %>
																						 <tr class="Nivel3">
																						 		<td width="13">&nbsp;</td>
																								<td width="4">&nbsp;</td>
																								<td width="4">&nbsp;</td>
																							    <td width="9">&nbsp;</td>
																						<%end if%>
																					
																							<td width="498" align="left"><%=strCuenta%>&nbsp;</td>
																							<td width="136"align="right" ><%=formatNumber(TotalPorCuentaPre,4)%>&nbsp;</td>
																							<td width="113" align="right" ><%=formatNumber(TotalPorCuentaEje,4)%>&nbsp;</td>
																							<td width="125" align="right" ><%= formatNumber(rsPlan("Diferencia"),4)%>&nbsp;</td>
																						</tr>
																						
																						<%
																							
																							set rsArticulo= cn.consultar("ConsultarEjecutadoJerarquico","FO","3", lngCodigo_Cco,strAno_Pto,lngCodigo_Cie,lngCodigo_Pco)											  

																							bytContarArticulo=0%>
																							
																							<!-- Inicio de Articulos -->
																							
																							<% if rsArticulo.recordcount>0 then%>
																								<tr>
																								  <td colspan="8" width="944">
																									<table width="80%" border="0" cellspacing="0" align="center" style="display:none" id="tblDetalleCuenta<%=lngCodigo_Cco%><%=lngCodigo_Pco%>">
																										
																											<tr class="etabla">
		    
																											    <td>Item</td>
																											    <td width="50%">Detalle</td>
																											    <td >
                                                                                                                <p style="text-align: right">Total Pre</td>
																											    <td >
                                                                                                                <p style="text-align: right">Total Eje</td>
																											    <td >
                                                                                                                <p style="text-align: right">Diferencia</td>
																											    
																										    </tr>				
																												
																												<%do while not rsArticulo.eof
																													bytContarArticulo=bytContarArticulo+1%>
																													
																													<tr align="center" onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)" style="cursor:hand">
																							
																													   <td><%=bytContarArticulo%></td>
																													   <td align="left"><%=rsArticulo("descripcion_Art")%></td>
																													   <td align="right"><%=formatNumber(rsArticulo("totalPre"),4)%></td>
																													   <td align="right"><%=formatNumber(rsArticulo("totalEje"),4)%></td>
																													   <td align="right"><%=formatNumber(rsArticulo("Diferencia"),4)%></td>
																											
																													</tr>
																													<%rsArticulo.movenext
																												Loop%>		
																					
																									   
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
													  <!--</table>-->
												   <!--</td>-->
												<!--</tr>-->
												 
											  <%end If %> 						
											<!-- Fin de Conceptos--> 
													  	    
   </table>
   <!--F1: Cerramos Tabla que Contiene Toda la Estructura  -->
<%


Cn.CerrarConexion
set cn= Nothing


	rsConcepto.close
	Set rsConcepto=Nothing
	
	IF rsPlan.state <>0 then
		rsPlan.close
	End If
	
	Set rsPlan=Nothing

	IF rsArticulo.state <>0 then
 		rsArticulo.close
 	End if
 	
	Set rsArticulo=Nothing
End if

%>
</body>
</html>