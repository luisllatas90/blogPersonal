<%

Response.ContentType = "application/vnd.ms-excel"
'Response.AddHeader "Content-Disposition","attachment;filename=" & "Presupuesto" & ".xls"

Dim bytContar
Dim lngCodigo_Pto
Dim strMoneda
Dim strTipo_Pto
Dim strEstado_Pto
Dim lngCodigo_Cco


    lngCodigo_Pto=Request.QueryString("id") 

    Set objPresupuesto=Server.CreateObject("PryUSAT.clsDatPresupuesto")
	Set rsPresupuesto=Server.CreateObject("ADODB.Recordset")
	Set rsPresupuesto= objPresupuesto.ConsultarPresupuesto ("RS","CB",lngCodigo_Pto,"")
	
	
	if	rsPresupuesto("moneda_Pto")="S" Then
		strMoneda_Pto="Soles"
		simbolo="S/."
	end if

	if	rsPresupuesto("moneda_Pto")="D" Then
		strMoneda_Pto="Dolares"
		simbolo="US$"
	end if
	
	if	rsPresupuesto("moneda_Pto")="E" Then
		strMoneda_Pto="Euros"
		simbolo="€"
	end if

	lngCodigo_Cco=trim(rsPresupuesto("codigo_Cco"))
	strTipo_Pto=trim(rsPresupuesto("tipo_Pto"))
	strEstado_Pto=trim(rsPresupuesto("estado_Pto"))

	if strEstado_Pto="P" then
		estado= "NO APROBADO"
    else
		estado="APROBADO"
    end if
    
    Response.AddHeader "Content-Disposition","attachment;filename=" & rsPresupuesto("descripcion_Cco") & ".xls"
    
    
%>

<html>
<head>
	<meta http-equiv="Content-Language" content="es">
	<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
	<meta name="ProgId" content="FrontPage.Editor.Document">
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
	<title>Consultar Consolidado de Presupuesto</title>
	<script language="JavaScript" src="../private/funciones.js"></script>
	
	
    <style>
<!--
.nivel1      { color: #000080; font-weight: bold }
.titulodetalle { font-weight: bold }
.nivel2      { color: #800000 }
.titulo      { color: #000080; font-weight: bold; text-align: center }
-->
    </style>
	
	
</head>
<body>


<!-- Cabecera de la Pagina-->
<table width="100%" align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" >
  <tr class="titulo"> 
    <td height ="65" colspan=6><font size="5">PRESUPUESTO <%=trim(rsPresupuesto("año_Pto")) %></font></td>
  </tr>  
  <tr>
    <td colspan="2"><b>Centro de Costo:</b> <%=trim(rsPresupuesto("descripcion_Cco"))%></td>
    <td colspan="2"><b>Estado: </b><%=estado%></td>  
    <td colspan="2"><b>Total General: </b><%=simbolo & " " & formatNumber(rsPresupuesto("totalGeneral_Pto"))%></td>  
   
  </tr>
   <%
		rsPresupuesto.Close 
		set rsPresupuesto= Nothing

    %>
<!-- Fin de la Cabecera de la Pagina-->

	           
         
		<tr> 
		  <td colspan="2" align="left" bgcolor="#C0C0C0"><b>Descripción</b></td>
		  <td colspan="4" align="right" bgcolor="#C0C0C0"><b>Total(<%=simbolo%>)</b></td>
		</tr>
		
		<!-- Inicio de Conceptos-->
		<%
		Dim totalConcepto
		Dim objConcepto
		Dim rsConcepto
		Dim lngCodigo_Cie
	
		Set objConcepto=Server.CreateObject("PryUSAT.clsDatConceptoIngresoEgreso")
		Set rsConcepto=Server.CreateObject("ADODB.Recordset")
		
		' El tipo I/E lo distinguimos en el procedimiento almacenado usando el codigo_Pto 				
		Set rsConcepto= objConcepto.ConsultarConceptoIngresoEgreso ("RS","PR",lngCodigo_Pto,"")


		bytContar=0 
		
		
		do while not rsConcepto.EOF 
			lngCodigo_Cie = rsConcepto("codigo_Cie")
			totalConcepto= formatNumber(rsConcepto("total"))
			%>
			
			<tr class="nivel1"> 
				<td width="80%" colspan="2" align="left"><%=rsConcepto("descripcion_Cie") %>&nbsp;</td>
				<td width="19%" align="right" colspan="4"><%=TotalConcepto%>&nbsp;</td>
		 	</tr>
			
			
			<!-- Inicio de Cuentas-->
			<%
			Dim objPlan
			Dim rsPlan
			Dim TotalCuenta
			Set objPlan=Server.CreateObject("PryUSAT.clsDatPlanContable")
			Set rsPlan=Server.CreateObject("ADODB.Recordset")
			Set rsPlan= objPlan.ConsultarPlanContable ("RS","PR",lngCodigo_Pto,lngCodigo_Cie)
			
			if rsplan.recordcount>0 then
			
					do while not rsPlan.eof
						lngCodigo_Pco=rsPlan("codigo_Pco")
						bytContar=bytContar+1
						TotalCuenta=formatNumber(rsPlan("total"))
							%>
							<tr class="nivel2">
								
								<td align="left" colspan="2"><%=trim(rsPlan("descripcionCuenta_Pco"))%>&nbsp;</td>
								<td align="right" colspan="4"><%=TotalCuenta%>&nbsp;</td>
							</tr>
						
						<!-- Inicio de Articulos-->
						<%
											
						Dim ObjArticulo
						Dim rsArticulo
						
						Set ObjArticulo=Server.CreateObject("PryUSAT.clsDatPresupuesto")
						Set rsArticulo=Server.CreateObject("ADODB.Recordset")
						Set rsArticulo= ObjArticulo.ConsultarPresupuesto ("RS","DE",lngCodigo_Pco,lngCodigo_Pto)
						bytContarArticulo=0
						
						if rsArticulo.recordcount>0 then%>
							<tr class="titulodetalle">
							
											<td align="center">Ítem</td>
											<td width="70%" align="left">Detalle</td>
											<td align="left">Unidad</td>
											<td align="right">Precio</td>
											<td align="center">Cant</td>
											<td align="right">SubTotal</td>
							 </tr>				
										
										<%do while not rsArticulo.eof
											lngCodigo_Art=rsArticulo("Codigo_Art")
											bytContarArticulo=bytContarArticulo+1%>
										
											<tr>
											   
												<td align="center"><%=bytContarArticulo%>&nbsp;</td>
												<td> <%=rsArticulo("descripcion_art")%>&nbsp;</td>
												<td><%=rsArticulo("unidad_Art")%>&nbsp;</td>
												<td align="right"><%=formatNumber(rsArticulo("precioUnitario_Dpr"))%>&nbsp;</td>
												<td align="center"><%=rsArticulo("cantidadTotal_Dpr")%>&nbsp;</td>
												<td align="right"><%=formatNumber(rsArticulo("total_Dpr"))%>&nbsp;</td>

											</tr>
											
											<%rsArticulo.movenext
										Loop%>		
							
						  <%end if
							rsArticulo.close
							set rsArticulo=Nothing
							set ObjArticulo =Nothing%>
						 <!-- Fin de Articulos-->
						  	
						<%rsPlan.movenext
					Loop%>		
			<%end if
			 rsPlan.close
			 Set rsPlan=Nothing
			 Set ObjPlan=Nothing%>
			 
			 <!-- Fin de Cuentas-->
		
		   <%rsConcepto.movenext
		loop
		rsConcepto.close
		set rsConcepto= Nothing
		set objConcepto= Nothing
		%>
		
		<!--Fin de Conceptos -->
		
</table>
<!-- Fin de la Tabla que Contendra la Estructura Jerarquica-->
</body>
</html>