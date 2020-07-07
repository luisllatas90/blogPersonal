<!--#include file="../clsgrafico.asp"-->
<script>
	function EnviarDatos(pagina)
	{
		location.href=pagina + "?ano=" + document.all.txtParametro.value
	}
</script>

<%

Dim ArrValores, ArrEtiquetas

Dim bytContar
Dim strAno
Dim strTotalGeneral
Dim strTipoConsulta
Dim seleccionar1
Dim seleccionar2
Dim Ap, Ae, Bp, Be, Cp, Ce, Dp, De, Ep, Ee, Fp,Fe, Gp, Ge, Hp, He, Ip, Ie, Jp, Je, Kp, Ke, Lp, Le, Tp,Te
Dim objParametro
Dim rsParametro
Dim valor1,valor2

valor1=50
valor2=80



'------ Obtenemos Parametro de la Consulta Año-------
strAno=Request.form("txtParametro") 

if strAno="" then
   strAno=request.querystring("ano")
end if
'----------------------------------------------------



'----- Con Este Año, Obtenemos Datos de Parametros para ese Año---------

Set objParametro= Server.CreateObject("PryUSAT.clsDatPresupuesto")
Set rsParametro= Server.CreateObject("ADODB.RecordSet")
set rsParametro = objParametro.ConsultarParametroPresupuesto("RS",strAno) 	
		
if rsParametro.recordcount >0 then
	if trim(rsParametro("moneda_Pto"))="S" THEN strMoneda="S/."
	if trim(rsParametro("moneda_Pto"))="D" THEN strMoneda="US$"
	if trim(rsParametro("moneda_Pto"))="E" THEN strMoneda="€"
	PorMor=trim(rsParametro("provisionMorosidad_Pto"))
	PorRec=trim(rsParametro("recuperacionMorosidad_Pto"))
	SaldoCaja=trim(rsParametro("saldoCajaAñoAnterior_Pto"))
		
end if

rsParametro.close
set rsParametro=Nothing
set objParametro=Nothing
'----------------------------------------------------------------------------


'---------------Funcion Para Totales---------------
function TotalPresupuestado(tipo,ano, param1,param2,param3,param4)
 Dim objPresupuesto
 Dim rsPresupuesto
 
	Set objPresupuesto = Server.CreateObject("PryUSAT.clsDatPresupuesto")
	Set rsPresupuesto=Server.CreateObject("ADODB.Recordset")
	Set rsPresupuesto= objPresupuesto.ConsultarTotalPresupuestado("RS",tipo,ano,param1,param2,param3,param4)
		if rsPresupuesto.recordcount >0 then

			TotalPresupuestado= formatNumber(rsPresupuesto("total"))

		else
			TotalPresupuestado="0.00"
		end if
		rsPresupuesto.close
		set rsPresupuesto=Nothing
		set objPresupuesto =Nothing
end function


function TotalEjecutado(tipo,ano, param1,param2,param3,param4)
 
 'Nota: Quitar Comentarios Una Vez Implementado
		 'Dim objPresupuesto
		 'Dim rsPresupuesto
 
			'Set objPresupuesto = Server.CreateObject("PryUSAT.clsDatPresupuesto")
			'Set rsPresupuesto=Server.CreateObject("ADODB.Recordset")
			'Set rsPresupuesto= objPresupuesto.ConsultarTotalEjecutado("RS",tipo,ano,param1,param2,param3,param4)
			'if rsPresupuesto.recordcount >0 then

					'TotalEjecutado= formatNumber(rsPresupuesto("total"))

			'else
				TotalEjecutado="0.00"
			'end if
			'rsPresupuesto.close
			'set rsPresupuesto=Nothing
			'set objPresupuesto =Nothing
end function


'-------------Fin de Fucnion ---------------------------



%>

<html>
<head>
	<title>Consultar Ejecucion de Presupuesto</title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
	<link rel="stylesheet" type="text/css" href="../private/estilo.css">
	<script language="JavaScript" src="../private/funciones.js"></script>
</head>

<body>

<!-- Titulo de la Pagina-->
<table align="center" width="100%">
  <tr>
      <th align="center" class="table">EJECUCION PRESUPUESTAL</th>
  </tr>
  <tr>
	<td><hr></td>
  </tr>
</table>
<!-- Fin de Titulo de la Pagina -->
<P>

<!-- Formulario Para Ingreso de Parametros de Consulta  -->
<form method="post" action="frmConsultarEjecucion.asp" id=form1 name=form1>

<table align="center" width="774" border="0">
<tr>
<td width="125">
		<table width="98%" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" >
			  <tr>
	   			 <td width="47%" class="etabla">Año:</td>
			    <td width="1%" align="center">
              <input type="text" name="txtParametro" size="4" maxlength="4" value="<%=strAno%>" onKeyPress="validarnumero()"></td>

			  </tr>	
		</table>

</td>
<td width="82"><input type="submit" value="Consultar" name="cmdConsultar" class="boton"></td>
<td>
	<table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" >
		<tr>
			<td class="etabla">&nbsp;Avance &nbsp;&nbsp;Presup.</td>
			<td align="center"><img border="0" src="../images/V.gif" align="baseline"> Menor o Igual a <%=valor1%>%</td>
			<td align="center"><img border="0" src="../images/A.gif" align="baseline"> <%=valor1%>% a <%=valor2%>%</td>
			<td align="center"><img border="0" src="../images/R.gif" align="baseline"> Mayor o Igual a <%=valor2%>%</td>
			<td> <input type="button" value="Exportar a Excel" name="cmdExportar" id="cmdexportar" class="boton" onClick="EnviarDatos('exportarPresupuestoEjecutado.asp')"></td>
	   </tr>
	</table>
</td>
</tr>	
</table>
</form>
	
<!-- Fin de Formulario de Ingreso de Parametros de la Consulta-->


<!--Empezamos a Construir la Estructura Jerarquica  -->
<%
valor1=formatNumber(valor1)
valor2=formatNumber(valor2)
%>

<%IF trim(strAno)<>"" then %>
    <!--I1: Creamos Tabla que Contendra Toda la Estructura --> 
	<table width="100%" align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111">
		  
		  <!-- Cabecera de la Tabla de Estructura-->
		  		  
		  <tr class="etabla"> 
		    <td class="etabla" width="2%">Item</td>
		  	<td class="etabla" width="4%">Id.</td>
		    <td class="etabla" width="472">Descripcion</td>
		    <td class="etabla" width="100">Total Presupuesto (<%=strMoneda%>)</td>
		    <td class="etabla" width="83">Total Ejecutado (<%=strMoneda%>)</td>
		    <td class="etabla" width="58">Diferencia (<%=strMoneda%>)</td>
			<td width="60" class="etabla">Avance Presup.(%)</td>
			<td width="38" class="etabla">Situac.</td>
		  </tr>
		  <!-- Fin de Cabecera de la Tabla de Estructura-->
		  
          <!-- Iniciamos Conceptos Generales   --> 
		   <% 		   
			Dim objConceptoGeneral
			Dim rsConceptoGeneral
			Dim lngCodigo_Cge

			Set objConceptoGeneral=Server.CreateObject("PryUSAT.clsDatPresupuesto")
			Set rsConceptoGeneral=Server.CreateObject("ADODB.Recordset")
			Set rsConceptoGeneral= objConceptoGeneral.ConsultarRubrosEjecucionPresupuesto("RS","CG",strAno,"","","")
		   %>
			
			  <%
			  Do while Not rsConceptoGeneral.eof   
				lngCodigo_Cge = rsConceptoGeneral("codigo_Cge")
				strConGen=trim(rsConceptoGeneral("descripcion_Cge"))
				strLetra_Cge= trim(rsConceptoGeneral("letra_Cge"))
				strTipo_Cge= trim(rsConceptoGeneral("tipo_Cge"))
				strTipoCta_Cge= trim(rsConceptoGeneral("tipoCta_Cge"))
				
				if strTipo_Cge="V" then
				   'Tiene Cuentas Amarradas				
							
							'LLamamos a Funcion TotalPresupuestado
							totalPre=FormatNumber(totalPresupuestado("CG",strAno,lngCodigo_Cge,strTipoCta_Cge,"",""))
							
							'Llamamos a Funcion Total Ejecutado
							totalEje=FormatNumber(totalEjecutado("CG",strAno,lngCodigo_Cge,strTipoCta_Cge,"",""))
							
							
							totalDif=totalPre - totalEje
							
							if totalPre >0 then
								totalAvance=(totalEje*100)/totalPre
							else
							    totalAvance="0.00"
							end if
							
							if strLetra_Cge="A" then Ap=totalPre : Ae=totalEje 
							if strLetra_Cge="B" then Bp=totalPre : Be=totalEje
							if strLetra_Cge="G" then Gp=totalPre : Ge=totalEje
							if strLetra_Cge="I" then Ip=totalPre : Ie=totalEje
							if strLetra_Cge="K" then Kp=totalPre : Ke=totalEje
							

							 %>
							 <%if totalPre >0 then%>
							 <tr class="Nivel0" onClick="MostrarTabla(document.all.tblTipo<%=lngCodigo_Cge%>,imgTI<%=lngCodigo_Cge%>)" style="cursor:hand"> 
							      <td align="center" width="3%" ><img id="imgTI<%=lngCodigo_Cge%>" src="../images/mas.gif" width="9" height="9"></td>
							      <td width="1%" align="center"><%=strLetra_Cge%> &nbsp;</td>
							  <%else%>
							    <tr class="Nivel0">
							      
								  <td width="3%"> 
								  <td width="1%" align="center"><%=strLetra_Cge%> &nbsp;</td>
							  <%end if%>
								 <td width="472"><%=strConGen%>&nbsp;</td>
								 <td width="100" align="right"><%=FormatNumber(totalPre)%>&nbsp;</td>
								 <td width="83" align="right"><%=FormatNumber(totalEje)%>&nbsp;</td>
								 <td width="58" align="right"><%=FormatNumber(totalDif)%>&nbsp;</td>
								 <td width="60" align="right"><%=FormatNumber(totalAvance)%>&nbsp;</td>
								 
								 <%if cdbl(totalAvance) <= cdbl(valor1) then %>
									 <td align="right" width="38"><img border="0" src="../images/V.gif" align="baseline"></td>
								  <%end if%>	 

								 <%if cdbl(totalAvance) > cdbl(valor1)  and cdbl(totalAvance) < cdbl(valor2) then%>
									  <td align="right" width="38"><img border="0" src="../images/A.gif" align="baseline"></td>
								  <%end if%>	 
													
								<%if cdbl(totalAvance) >= cdbl(valor2 ) then%>
									<td align="right" width="38"><img border="0" src="../images/R.gif" align="baseline"></td>
								 <%end if%>
			
							 </tr>			
								  <!-- Inicio de Conceptos de Ingreso y Egresos-->			
									  <%
									  Dim objConceptoIngresoEgreso
									  Dim rsConceptoIngresoEgreso
									  Set objConceptoIngresoEgreso=Server.CreateObject("PryUSAT.clsDatPresupuesto")
									  Set rsConceptoIngresoEgreso=Server.CreateObject("ADODB.Recordset")
									  Set rsConceptoIngresoEgreso= objConceptoIngresoEgreso.ConsultarRubrosEjecucionPresupuesto("RS","CO",strAno,lngCodigo_Cge,"","")
									 
									  if rsConceptoIngresoEgreso.RecordCount >0 then%>
										
										 <tr>
											<td colspan="8">
											   
											   <table width="100%" border="0" cellpadding="3" cellspacing="0" align="center" style="display: none"  id="tblTipo<%=lngCodigo_Cge%>">			
													
													
													<%Do while not rsConceptoIngresoEgreso.EOF 
														lngCodigo_Cie=trim(rsConceptoIngresoEgreso("codigo_Cie")) 
														strTipo_Cie=trim(rsConceptoIngresoEgreso("tipo_Cie")) 

														'LLamamos a Funcion TotalPresupuestado
														totalPre=FormatNumber(totalPresupuestado("CO",strAno,lngCodigo_Cie,strTipo_Cie,"",""))
													
														
														'Llamamos a Funcion Total Ejecutado
														 totalEje=FormatNumber(totalEjecutado("CO",strAno,lngCodigo_Cie,strTipo_Cie,"",""))
											
														totalDif=totalPre - totalEje
														
														if totalPre >0 then
															totalAvance=(totalEje*100)/totalPre
														else
														    totalAvance="0.00"
														end if
														
														%>
														<%if totalPre >0 then%>		  
														<tr  style="cursor:hand" onClick="MostrarTabla(document.all.tblCentro<%=lngCodigo_Cge%><%=lngCodigo_Cie%>,imgCC<%=lngCodigo_Cge%><%=lngCodigo_Cie%>)"> 
															<td width="18">&nbsp;</td>
															<td class="Nivel1" align="center" width="20"><img id="imgCC<%=lngCodigo_Cge%><%=lngCodigo_Cie%>" src="../images/mas.gif" width="9" height="9"></td>
														<%else%>
														<tr >
															<td width="20">&nbsp;</td>
															<td width="20" class="Nivel1">&nbsp;</td>

														<%end if%>
															
															<td width="431" class="Nivel1" ><%=trim(rsConceptoIngresoEgreso("descripcion_Cie"))%></td>
															<td width="95" align="right" class="Nivel1"><%=formatNumber(totalPre)%></td>	
															<td width="82" align="right" class="Nivel1"><%=formatNumber(totalEje)%></td>	
															<td width="64" align="right" class="Nivel1"><%=FormatNumber(totalDif)%></td>
															<td width="60" align="right" class="Nivel1"><%=FormatNumber(totalAvance)%></td>
															<%if cdbl(totalAvance) <= cdbl(valor1) then %>
																 <td align="right" width="43" class="Nivel1"><img border="0" src="../images/V.gif" align="baseline"></td>
														    <%end if%>	 

															 <%if cdbl(totalAvance) > cdbl(valor1)  and cdbl(totalAvance) < cdbl(valor2) then%>
																<td align="right"width="43" class="Nivel1"><img border="0" src="../images/A.gif" align="baseline"></td>
															 <%end if%>	 
													
															 <%if cdbl(totalAvance) >= cdbl(valor2) then%>
																<td align="right" width="43" class="Nivel1"><img border="0" src="../images/R.gif" align="baseline"></td>
														   	 <%end if%>

															
														 </tr>
														 
													<!--  Inicio de Centro de Costos-->
														<%
														  Dim objCentroCosto
														  Dim rsCentroCosto
														  Dim lngCodigo_Cco
																	
														  Set objCentroCosto=Server.CreateObject("PryUSAT.clsDatPresupuesto")
														  Set rsCentroCosto=Server.CreateObject("ADODB.Recordset")
														  Set rsCentroCosto= objCentroCosto.ConsultarRubrosEjecucionPresupuesto("RS","CC",strAno,lngCodigo_Cie,strTipo_Cie,"")
														%>	
														
																
														  <%if rsCentroCosto.recordcount >0 then
															bytContar=0 %>
															
															<tr>
															   <td colspan="8">
																  <table   width="100%" border="0" cellpadding="3" cellspacing="0" align="center" style="display: none" id="tblCentro<%=lngCodigo_Cge%><%=lngCodigo_Cie%>">			
																	<%do while not rsCentroCosto.EOF 
																	
																		lngCodigo_Cco = rsCentroCosto("codigo_Cco")
				
																		'LLamamos a Funcion TotalPresupuestado								
																		totalPre=FormatNumber(totalPresupuestado("CC",strAno,lngCodigo_Cie,lngCodigo_Cco,strTipo_Cie,""))
																		
																		'LLamamos a Funcion TotalEjecutado
																		totalEje=FormatNumber(totalEjecutado("CC",strAno,lngCodigo_Cie,lngCodigo_Cco,strTipo_Cie,""))

																		totalDif=totalPre - totalEje
																		if totalPre >0 then
																			totalAvance=(totalEje*100)/totalPre
																		else
																		    totalAvance="0.00"
																		end if
																
																		
																		 if totalPre >0 then%>
																			
																			<tr  onClick="MostrarTabla(document.all.tblCuenta<%=lngCodigo_Cie%><%=lngCodigo_Cco%>,imgCO<%=lngCodigo_Cie%><%=lngCodigo_Cco%>)" style="cursor:hand"> 
																				<td width="18">&nbsp;</td>
																				<td width="18">&nbsp;</td>    
																				<td align="center" width="20" class="Nivel2"><img id="imgCO<%=lngCodigo_Cie%><%=lngCodigo_Cco%>" src="../images/mas.gif" width="9" height="9"></td>
																		 <%else%>				
																			<tr> 
																				<td width="20" class="Nivel2">&nbsp;</td>
																				<td width="20">&nbsp;</td>
																				<td width="20" class="Nivel2" >&nbsp;</td>
																		 <%end if%>
																				
																		 
																			  <td width="391" class="Nivel2"><%=rsCentroCosto("descripcion_Cco") %></td>
																			  <td width="98" align="right" class="Nivel2"><%=formatNumber(totalPre)%></td>
																			  <td width="80" align="right" class="Nivel2"><%=formatNumber(totalEje)%></td>
																			  <td width="67" align="right" class="Nivel2"><%=formatNumber(totalDif)%></td>
																			  <td width="61" align="right" class="Nivel2"><%=formatNumber(totalAvance)%></td>
																			  <%if cdbl(totalAvance) <= cdbl(valor1) then %>
																				 <td align="center" width="40" class="Nivel2"><img border="0" src="../images/V.gif" align="baseline"></td>
									   									      <%end if%>	 

				  															   <%if cdbl(totalAvance) > cdbl(valor1)  and cdbl(totalAvance) < cdbl(valor2) then%>
										  										  <td align="center" width="40" class="Nivel2"><img border="0" src="../images/A.gif" align="baseline"></td>
																				<%end if%>	 
													
																				 <%if cdbl(totalAvance) >= cdbl(valor2) then%>
																					<td align="center" width="40" class="Nivel2"><img border="0" src="../images/R.gif" align="baseline"></td>
																			   	  <%end if%>

																		 
																			</tr>
																			 <%
																				 
																				Dim objPlan
																				Dim rsPlan
																				Dim TotalCuenta
																				Set objPlan=Server.CreateObject("PryUSAT.clsDatPresupuesto")
																				Set rsPlan=Server.CreateObject("ADODB.Recordset")
																				Set rsPlan= objPlan.ConsultarRubrosEjecucionPresupuesto("RS","PC",strAno,lngCodigo_Cco,lngCodigo_Cie,strTipo_Cie)
																			  %>
																			  <!-- Inicio de Cuentas-->
																			
																				<% if rsplan.recordcount>0 then%>
																					<tr>
																					   <td colspan="9">
																						  <table  width="100%" border="0" cellpadding="3" cellspacing="0" align="center" style="display: none"  id="tblCuenta<%=lngCodigo_Cie%><%=lngCodigo_Cco%>">			
																						
																							<%do while not rsPlan.eof
																									lngCodigo_Pco=rsPlan("codigo_Pco")
																									bytContar=bytContar+1
																									
																													
																									'LLamamos a Funcion TotalPresupuestado								
																										totalPre=FormatNumber(totalPresupuestado("PC",strAno,lngCodigo_Cco,lngCodigo_Cie,lngCodigo_Pco,strTipo_Cie))
																		
																									'LLamamos a Funcion TotalEjecutado
																										totalEje=FormatNumber(totalEjecutado("PC",strAno,lngCodigo_Cco,lngCodigo_Cie,lngCodigo_Pco,strTipo_Cie))
																									
																									
																									totalDif=totalPre - totalEje
																									
																									if totalPre >0 then
																										totalAvance=(totalEje*100)/totalPre
																									else
																										totalAvance="0.00"
																									end if
																									
																									

																									
																									if totalPre>0 then%>
																									 <tr onClick="MostrarTabla(document.all.tblDetalleCuenta<%=lngCodigo_Cco%><%=lngCodigo_Pco%>,imgDC<%=lngCodigo_Cco%><%=lngCodigo_Pco%>)" style="cursor:hand">
																											<td width="22">&nbsp;</td>
																											<td width="22">&nbsp;</td>
																											<td width="22">&nbsp;</td>
																										   <td width="17" class="Nivel3"><img id="imgDC<%=lngCodigo_Cco%><%=lngCodigo_Pco%>" src="../images/mas.gif" width="9" height="9">
																									 <%else %>
																									 <tr class="Nivel3">
																											<td width="18">&nbsp;</td>
																											<td width="18">&nbsp;</td>
																											<td width="18">&nbsp;</td>
																											<td width="17" class="Nivel3">&nbsp;</td>
																									<%end if%>
																								

																										<td width="358" align="left" class="Nivel3"><%=rsPlan("descripcionCuenta_Pco")%></td>
																										<td align="right" width="102" class="Nivel3"><%=formatNumber(totalPre)%></td>
																										<td width="83" align="right" class="Nivel3"><%=formatNumber(totalEje)%></td>
																										<td width="68" align="right" class="Nivel3"><%=formatNumber(totalDif)%></td>
																										<td width="59" align="right" class="Nivel3"><%=formatNumber(totalAvance)%></td>
																										<%if cdbl(totalAvance) <= cdbl(valor1) then %>
																											<td align="left" width="38" class="Nivel3"><img border="0" src="../images/V.gif" align="baseline"></td>
														    											<%end if%>	 

															 											 <%if cdbl(totalAvance) > cdbl(valor1)  and cdbl(totalAvance) < cdbl(valor2) then%>
																											<td align="left" width="38" class="Nivel3"><img border="0" src="../images/A.gif" align="baseline"></td>
															 											 <%end if%>	 
													
															 											 <%if cdbl(totalAvance) >= cdbl(valor2) then%>
																 											<td align="left" width="38" class="Nivel3"><img border="0" src="../images/R.gif" align="baseline"></td>
														   	 											 <%end if%>


																									</tr>
																									
																									<%
																										Dim ObjArticulo
																										Dim rsArticulo
																					
																										Set ObjArticulo=Server.CreateObject("PryUSAT.clsDatPresupuesto")
																										Set rsArticulo=Server.CreateObject("ADODB.Recordset")
																										Set rsArticulo= ObjArticulo.ConsultarRubrosEjecucionPresupuesto("RS","AR",strAno,lngCodigo_Cco,lngCodigo_Pco,strTipo_Cie)
																										bytContarArticulo=0%>
																										
																										<!-- Inicio de Articulos -->
																										
																										<% if rsArticulo.recordcount>0 then%>
																											<tr>
																											  <td colspan="10" width="851">
																												<table width="80%" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" align="center" style="display:none" id="tblDetalleCuenta<%=lngCodigo_Cco%><%=lngCodigo_Pco%>">
																												
																																																	  
																													
																														<tr class="Nivel4"> 
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
																																
																																<tr class="Nivel5" align="center" onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)">
																											
																																   <td><%=bytContarArticulo%></td>
																																   <td align="left"><%=rsArticulo("descripcion_art")%></td>
																																   <td><%=rsArticulo("descripcion_Uni")%></td>
																																   <td><%=formatNumber(rsArticulo("precioUnitario_Dpr"))%></td>
																																   <td><%=rsArticulo("cantidadTotal_Dpr")%></td>
																																   <td align="right"><%=formatNumber(rsArticulo("total_Dpr"))%></td>
																																
																  
																																</tr>
																																<%rsArticulo.movenext
																															Loop%>		
																								

																												</table>
																											  </td>
																											</tr>
																										<%end if
																										rsArticulo.close
																										set rsArticulo=Nothing
																										set ObjArticulo =Nothing%>
																										<!-- Fin de Articulos-->
																								<%rsPlan.movenext
																							Loop%>		
																						  </table>
																					   </td>
																					</tr>
																				<%end if
																				  rsPlan.close
																				  Set rsPlan=Nothing
																				  Set ObjPlan=Nothing%>
																				<!-- Fin de Cuentas-->
																		 
																		 
																			  <%rsCentroCosto.movenext
																			loop%>
																  </table>
															   </td>
															</tr>
															 
														  <%end If	
															rsCentroCosto.close
															set rsCentroCosto= Nothing
															set objCentroCosto= Nothing%>
														
														<!-- Fin de Centro de Costos--> 
											
														<%rsConceptoIngresoEgreso.MoveNext 
													loop%>
													
											 </table>
											</td>
										  </tr>
										 
									 <%end if 
									   
									   rsConceptoIngresoEgreso.close
									   Set rsConceptoIngresoEgreso=Nothing
									   Set objCentroCostoIngresoEgreso=Nothing%>
									
									 <!-- Fin de Concepto de Ingreso y Egresos--> 	
				 <%else
				 	'No Tiene Amarres y es Calculado
				 	
				 	   if strLetra_Cge ="C" THEN
				 	     	Tp= cdbl(Ap) + cdbl(Bp)
				 	   		Te= cdbl(Ae) + cdbl(Be)
				 	   		Td = cdbl(Tp) - cdbl(Te)
				 	   		
				 	   		Cp=Tp
							Ce=Te
				 	   end if
				 	
				 	  if strLetra_Cge ="D" THEN
				 	   		Tp= cdbl(PorMor)/100 * cdbl(Cp)
				 	   		Te= cdbl(PorMor)/100 * cdbl(Ce) 
				 	   		Td= cdbl(Tp)- cdbl(Te)
				 	   		
				 	   		Dp=Tp 
				 	   		De=Te
							strConGen = strConGen & " ("  & PorMor & "%)"
				 	   end if

						if strLetra_Cge ="E" THEN
				 	   		Tp= cdbl(PorRec)/100 * cdbl(Dp)
				 	   		Te= cdbl(PorRec)/100 * cdbl(De)
				 	   		Td=cdbl(Tp)-cdbl(Te)
				 	   		
				 	   		Ep= Tp
				 	   		Ee=Te
							strConGen = strConGen & " ("  & PorRec & "%)"
				 	   end if

						if strLetra_Cge ="F" THEN
				 	   		Tp= cdbl(Cp) - cdbl(Dp) + cdbl(Ep)
				 	   		Te= cdbl(Ce) - cdbl(De) + cdbl(Ee)
				 	   		Td=cdbl(Tp)-cdbl(Te)
				 	   		
				 	   		Fp= Tp
				 	   		Fe=Te

				 	   end if


						if strLetra_Cge ="H" THEN
				 	     	Tp= cdbl(Fp)  - cdbl(Gp)
				 	   		Te= cdbl(Fe)  - cdbl(Ge)
				 	   		Td=cdbl(Tp)-cdbl(Te)
				 	   		
				 	   		Hp= Tp
				 	   		He=Te

				 	   end if

						if strLetra_Cge ="J" THEN
				 	     	Tp= cdbl(Hp)  + cdbl(Ip)
				 	   		Te= cdbl(He)  + cdbl(Ie)
				 	   		Td=cdbl(Tp)-cdbl(Te)
				 	   		
				 	   		Jp= Tp
				 	   		Je=Te

				 	   end if
						if strLetra_Cge ="L" THEN
				 	     	Tp= cdbl(SaldoCaja)
				 	   		Te= cdbl(SaldoCaja)
				 	   		Td=cdbl(Tp)-cdbl(Te)
				 	   		
				 	   		Lp= Tp
				 	   		Le=Te

				 	   end if


						if strLetra_Cge ="M" THEN
				 	     	Tp= cdbl(Jp)  + cdbl(Lp) - cdbl(Kp)
				 	   		Te= cdbl(Je)  + cdbl(Le) - cdbl(Ke)
				 	   		Td=cdbl(Tp)-cdbl(Te)
				 	   		
				 	   		Mpr = Tp
				 	   		Mej = Te

				 	   end if


				 	   
				 	
					 	if Tp >0 then
								Ta=(Te*100)/Tp
						else
							    Ta="0.00"
						end if
				 	    
				        	
				 	
				       %>     
					  		
							 <tr class="Nivel0">
							  
					          
					         <td width="31">&nbsp;</td>
					         <td width="1%" align="center"><b><%=strLetra_Cge%> &nbsp;</b></td>
							  <td width="472"><b><%=strConGen%>&nbsp;</b></td>
							  <td width="100" align="right"><b><%=FormatNumber(Tp)%>&nbsp;</b></td>
							  <td width="83" align="right"><b><%=FormatNumber(Te)%>&nbsp;</b></td>
							  <td width="58" align="right"><b><%=FormatNumber(Td)%>&nbsp;</b></td>
							  <td width="60" align="right"><b><%=FormatNumber(Ta)%>&nbsp;</b></td>
							
												
			 				<%if cdbl(Ta)<= cdbl(valor1) then %>
								 <td align="right" width="38"><img border="0" src="../images/V.gif" align="baseline"></td>
						    <%end if%>	 
 							 <%if cdbl(Ta) > cdbl(valor1)  and cdbl(Ta) < cdbl(valor2) then%>
								  <td align="right" width="38"><img border="0" src="../images/A.gif" align="baseline"></td>
							 <%end if%>	 
													
							 <%if cdbl(Ta) >= cdbl(valor2) then%>
								<td align="right" width="38"><img border="0" src="../images/R.gif" align="baseline"></td>
							 <%end if%>

				  <%end if
			    rsConceptoGeneral.movenext
		      Loop 
		 
		      rsConceptoGeneral.close
		      set rsConceptoGeneral=Nothing
		      set objCentroCostoGeneral=Nothing%>	
		<!-- Fin  del Bucle de Conceptos Generales   --> 
   </table>
   <!--F1: Cerramos Tabla que Contiene Toda la Estructura  -->
   <br>
   <hr>
<%
	'Set grafico=new clsGrafico
	'grafico.mostrargrafico Array(cdbl(Ap), cdbl(Ae), cdbl(Bp), cdbl(Be), cdbl(Cp), cdbl(Ce), cdbl(Dp), cdbl(De), cdbl(Ep), cdbl(Ee), cdbl(Fp), cdbl(Fe), cdbl(Gp),cdbl(Ge), cdbl(Hp), cdbl(He), cdbl(Ip), cdbl(Ie), cdbl(Jp), cdbl(Je), cdbl(Kp),cdbl(Ke), cdbl(Lp), cdbl(Le), cdbl(Mpr), cdbl(Mej) ),Array("Ap", "Ae", "Bp", "Be", "Cp", "Ce", "Dp", "De", "Ep", "Ee", "Fp", "Fe", "Gp","Ge", "Hp", "He", "Ip", "Ie", "Jp", "Je", "Kp", "Ke", "Lp", "Le", "Mp", "Me"),"Presupuestado Vs. Ejecutado", "Conceptos de Ingresos y Egresos", "","../images/bgtabla.gif","S"
	'Set grafico=nothing

End if

%>
<p>&nbsp;</p>

	
	


</body>
</html>