<%
Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=" & "Presupuesto" & ".xls"
%>
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
   strAno=request.querystring("ano")
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
	<script language="JavaScript" src="../private/funciones.js"></script>
    <style>
<!--
.verde       { background-color: #00FF00 }
.letraPequena { font-size: 10pt }
.etabla      { font-weight: bold; text-align: center; background-color: #C0C0C0 }
.cabezera    { font-size: 12pt; color: #000080; font-weight: bold; text-align:center }
.nivel1      { color: #000080; text-align: left }
.nivel2      { text-align: left; color: #800000 }
.rojo        { background-color: #FF0000 }
.amarillo    { background-color: #FFFF00 }
-->
    </style>
</head>

<body>
<!--Empezamos a Construir la Estructura Jerarquica  -->
<%
valor1=formatNumber(valor1)
valor2=formatNumber(valor2)
%>

<%IF trim(strAno)<>"" then %>
    <!--I1: Creamos Tabla que Contendra Toda la Estructura --> 
	<table width="100%" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111">
		  
		  <!-- Cabecera de la Tabla de Estructura-->
		  <tr>
		  	<td colspan="7" height="32" class="cabezera">EJECUCIÓN PRESUPUESTAL &nbsp;-&nbsp;<%=strAno%></td>
		  </tr>
		  <THEAD>
		  <tr> 
		  	<td width="20" height="65" bgcolor="#C0C0C0"><b>ID</b></td>
		    <td width="400" height="65" bgcolor="#C0C0C0"><b>Descripción</b></td>
		    <td width="100" height="65" bgcolor="#C0C0C0"><b>Total Presupuesto (<%=strMoneda%>)</b></td>
		    <td width="100" height="65" bgcolor="#C0C0C0"><b>Total Ejecutado (<%=strMoneda%>)</b></td>
		    <td width="100" height="65" bgcolor="#C0C0C0"><b>Diferencia (<%=strMoneda%>)</b></td>
			<td width="80" height="65" bgcolor="#C0C0C0"  ><b>Avance Presup.(%)</b></td>
			<td width="20" height="65" bgcolor="#C0C0C0"  ><b>E</b></td>
		  </tr>
		  </THEAD>
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
							 <tr>
							     <td width="20" align="center" ><b><%=strLetra_Cge%></b>&nbsp;</td>
								 <td width="400" ><b><%=strConGen%></b>&nbsp;</td>
								 <td width="100" align="right" ><b><%=FormatNumber(totalPre)%></b>&nbsp;</td>
								 <td width="100" align="right" ><b><%=FormatNumber(totalEje)%></b>&nbsp;</td>
								 <td width="100" align="right" ><b><%=FormatNumber(totalDif)%></b>&nbsp;</td>
								 <td width="80" align="right" ><b><%=FormatNumber(totalAvance)%></b>&nbsp;</td>
								 <%if cdbl(totalAvance) <= cdbl(valor1) then %>
									 <td align="right" width="20" class="verde" ></td>
								  <%end if%>	 
								 <%if cdbl(totalAvance) > cdbl(valor1)  and cdbl(totalAvance) < cdbl(valor2) then%>
									 <td align="right" width="20" class="amarillo" ></td>
							  	<%end if%>													
								<%if cdbl(totalAvance) >= cdbl(valor2 ) then%>
									<td align="right" width="20" class="rojo" ></td>
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
														
														<tr class="nivel1">
															<td width="20" ></td>
															<td width="400" ><%=trim(rsConceptoIngresoEgreso("descripcion_Cie"))%>&nbsp;</td>
															<td width="100" align="right" ><%=formatNumber(totalPre)%>&nbsp;</td>	
															<td width="100" align="right" ><%=formatNumber(totalEje)%>&nbsp;</td>	
															<td width="100" align="right" ><%=FormatNumber(totalDif)%>&nbsp;</td>
															<td width="80" align="right" ><%=FormatNumber(totalAvance)%>&nbsp;</td>
															
															<%if cdbl(totalAvance) <= cdbl(valor1) then %>
																 <td align="right" width="20" class="verde" ></td>
														    <%end if%>	 

															 <%if cdbl(totalAvance) > cdbl(valor1)  and cdbl(totalAvance) < cdbl(valor2) then%>
																<td align="right" width="20"  class="amarillo" ></td>
															 <%end if%>	 
													
															 <%if cdbl(totalAvance) >= cdbl(valor2) then%>
																<td align="right" width="20" class="rojo" ></td>
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
																		
																		%>	
																			<tr class="nivel2"> 
																			
																				 <td width="20" ></td>
																			     <td width="400" >&nbsp;&nbsp;&nbsp;<%=trim(rsCentroCosto("descripcion_Cco"))%></td>
 																			     <td width="100" align="right" ><%=formatNumber(totalPre)%>&nbsp;</td>
																			   	 <td width="100" align="right" ><%=formatNumber(totalEje)%>&nbsp;</td>
																				 <td width="100" align="right" ><%=formatNumber(totalDif)%>&nbsp;</td>
																				 <td width="80" align="right" ><%=formatNumber(totalAvance)%>&nbsp;</td>
																				  <%if cdbl(totalAvance) <= cdbl(valor1) then %>
																				 <td align="center" width="20" class="verde" ></td>
										   									      <%end if%>	 

				  															   <%if cdbl(totalAvance) > cdbl(valor1)  and cdbl(totalAvance) < cdbl(valor2) then%>
										  										  <td align="center" width="20" class="amarillo" ></td>
																				<%end if%>	 
													
																				 <%if cdbl(totalAvance) >= cdbl(valor2) then%>
																					<td align="center" width="20" class="rojo" ></td>
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
																								

																									%>
																									
																									 <tr>
																									
																											
																											<td width="20" ></td>
																											<td width="400" align="left" >&nbsp;&nbsp;&nbsp;&nbsp;<%=trim(rsPlan("descripcionCuenta_Pco"))%></td>
																											<td align="right" width="100" ><%=formatNumber(totalPre)%>&nbsp;</td>
																											<td width="100" align="right" ><%=formatNumber(totalEje)%>&nbsp;</td>
																											<td width="100" align="right" ><%=formatNumber(totalDif)%>&nbsp;</td>
																											<td width="80" align="right" ><%=formatNumber(totalAvance)%>&nbsp;</td>
																											<%if cdbl(totalAvance) <= cdbl(valor1) then %>
																												<td align="left" width="20"  class="verde" ></td>
															    											<%end if%>	 

																 											 <%if cdbl(totalAvance) > cdbl(valor1)  and cdbl(totalAvance) < cdbl(valor2) then%>
																												<td align="left" width="20" class="amarillo" ></td>
																 											 <%end if%>	 
													
																 											 <%if cdbl(totalAvance) >= cdbl(valor2) then%>
																	 											<td align="left" width="20" class="rojo" ></td>
															   	 											 <%end if%>

																									</tr>
																									
																										
																								<%rsPlan.movenext
																							Loop%>		
																						
																				<%end if
																				  rsPlan.close
																				  Set rsPlan=Nothing
																				  Set ObjPlan=Nothing%>
																				<!-- Fin de Cuentas-->
																		 
																		 
																			  <%rsCentroCosto.movenext
																			loop%>
															 
														  <%end If	
															rsCentroCosto.close
															set rsCentroCosto= Nothing
															set objCentroCosto= Nothing%>
														
														<!-- Fin de Centro de Costos--> 
											
														<%rsConceptoIngresoEgreso.MoveNext 
													loop%>
																					 
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
					  		
							 <tr >
							 <td width="20" align="center" ><b><%=strLetra_Cge%></b>&nbsp;</td>
							  <td width="400" ><b><%=strConGen%>&nbsp;</b></td>
							  <td width="100" align="right"  ><b><%=FormatNumber(Tp)%>&nbsp;</b></td>
							  <td width="100" align="right" ><b><%=FormatNumber(Te)%>&nbsp;</b></td>
							  <td width="100" align="right" ><b><%=FormatNumber(Td)%>&nbsp;</b></td>
							  <td width="80" align="right" ><b><%=FormatNumber(Ta)%>&nbsp;</b></td>
							
												
			 				<%if cdbl(Ta)<= cdbl(valor1) then %>
								 <td align="right" width="20" class="verde" ></td>
						    <%end if%>	 
 							 <%if cdbl(Ta) > cdbl(valor1)  and cdbl(Ta) < cdbl(valor2) then%>
								  <td align="right" width="20" class="amarillo" ></td>
							 <%end if%>	 
													
							 <%if cdbl(Ta) >= cdbl(valor2) then%>
								<td align="right" width="20" class="rojo" >
							 <%end if%>

				  <%end if
			    rsConceptoGeneral.movenext
		      Loop 
		 
		      rsConceptoGeneral.close
		      set rsConceptoGeneral=Nothing
		      set objCentroCostoGeneral=Nothing 
		      %>	
		<!-- Fin  del Bucle de Conceptos Generales   --> 
   &nbsp;</table>
   <!--F1: Cerramos Tabla que Contiene Toda la Estructura  -->
 <%  
End if
%>


</body>
</html>