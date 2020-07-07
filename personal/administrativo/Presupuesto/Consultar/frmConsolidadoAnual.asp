<script>
	function EnviarDatos(pagina)
	{
		location.href=pagina
	}
	
	function Volver()
	{
		history.back(-1)
	}
	
	
	
</script>

<%
	strAno="2007"
	strTipo="E"
	strEstado="A"
	strMoneda_Pto="Soles"
	simbolo=" S/. "

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
    
obj.AbrirConexion

	'Calcular Total Anual
		Set rsTotal=Server.CreateObject("ADODB.Recordset")
		Set rsTotal= obj.Consultar ("ConsultarTotalPresupuestoAnual", "ST",strAno,strTipo,strEstado)
	    
		if 	rsTotal.eof <> true and rsTotal.bof <>true Then
			total=rsTotal("total")
		else
			total="0.00"
		end if

		rsTotal.close
		set  rsTotal = Nothing

%>
<html>
<head>
	<meta http-equiv="Content-Language" content="es">
	<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
	<meta name="ProgId" content="FrontPage.Editor.Document">
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
	<title>Consultar Detalle de Presupuesto</title>
	<link rel="stylesheet" type="text/css" href="../private/estilo.css">
	<script language="JavaScript" src="../private/funciones.js"></script>
</head>

<body>
<!-- Titulo de la Pagina-->
<table align="center" width="100%">
  <tr>
	<th align="center" class="table">CONSOLIDADO DE PRESUPUESTOS APROBADOS  DE 
    TODA LA USAT-  AÑO: <%=strAno%></th>
  </tr>
  <tr>
  <td><hr></td>
  </tr>
</table>
<!--Fin del Titulo -->


<!-- Botones de Opciones-->
<table align="center" width="100%" border="0" cellpading="0" height="35">
    <tr> 
          <td align="right" height="31" width="271">
	          <input type="button" value="Exportar a Excel" name="cmdExportar" id="cmdexportar" class="boton" onClick="EnviarDatos('ExportarConsolidadoAnual.asp?ano=<%=strAno%>&tipo=<%=strTipo%>&estado=<%=strEstado%>&total=<%=total%>&simbolo=<%=simbolo%>')" style="float: left">
	      </td>
   </tr>

</table>
<!-- Fin de Botones de Opciones-->


<!-- Tabla que Contendra la Estructura Jerarquica-->
<table width="100%" align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111">

        <!-- Cabecera de la Tabla Jerarquica -->
		<tr class="etabla"> 
		  <td>Item</td>
		  <td>Descripcion</td>
		  <td align =right>Total(<%=simbolo%>) : <%=formatNumber(total)%></td>
		</tr>
		<!-- Fin de la Cabecera de la Tabla Jerarquica -->
		
		<!-- Inicio de Conceptos-->
		<%
		Dim totalConcepto
		Dim objConcepto
		Dim rsConcepto
		Dim lngCodigo_Cie
	
	
		Set rsConcepto=Server.CreateObject("ADODB.Recordset")
		Set rsConcepto= obj.Consultar("ConceptosUtilizadosPresupuesto", "ST",strAno,strTipo,strEstado)

		bytContar=0 
		
		
		do while not rsConcepto.EOF 
			lngCodigo_Cie = rsConcepto("codigo_Cie")
			totalConcepto= formatNumber(rsConcepto("total"))
			%>
			
			<tr class="Nivel0" onClick="MostrarTabla(document.all.tblCuenta<%=lngCodigo_Cie%>,imgCO<%=lngCodigo_Cie%>)" style="cursor:hand"> 
				<td align="center" width="1%"><img id="imgCO<%=lngCodigo_Cie%>" src="../images/mas.gif" width="9" height="9"></td>
				<td width="80%"><%=rsConcepto("descripcion_Cie") %>&nbsp;</td>
				<td width="19%" align="right"><%=TotalConcepto%>&nbsp;</td>
		 	</tr>
			
			
			
			<!-- Inicio de Cuentas-->
			<%
			Dim objPlan
			Dim rsPlan
			Dim TotalCuenta

			Set rsPlan=Server.CreateObject("ADODB.Recordset")
			Set rsPlan= obj.Consultar("CuentasUtilizadasPresupuesto","ST",strAno,strTipo,strEstado,lngCodigo_Cie)
			
			if rsplan.recordcount>0 then%>
				<tr>
				  <td colspan="3">
					<%if trim(idConcepto)=trim(lngCodigo_Cie) then%>
					  <table width="100%" border="0" cellpadding="3" cellspacing="0" align="center"  id="tblCuenta<%=lngCodigo_Cie%>">
					<%else%>
					  <table  width="100%" border="0" cellpadding="3" cellspacing="0" align="center" style="display:none" id="tblCuenta<%=lngCodigo_Cie%>">			
					<%end if
					
					do while not rsPlan.eof
						lngCodigo_Pco=rsPlan("codigo_Pco")
						bytContar=bytContar+1
						TotalCuenta=formatNumber(rsPlan("total"))
							
						if TotalCuenta>0 then%>
							<!-- Esto sirve siempre y cuando sigamos la jerarquia de articulos
							<tr onClick="MostrarTabla(document.all.tblDetalleCuenta<%=lngCodigo_Pco%>,imgDC<%=lngCodigo_Pco%>)" style="cursor:hand">
							    <td width="3%">&nbsp;</td>
								<td width="1%"><img id="imgDC<%=lngCodigo_Pco%>" src="../images/mas.gif" width="9" height="9">
						
							Agregue esto por que no sigo la jerarquia de articulos-->
						   <tr>
 							   <td width="3%">&nbsp;</td>
								<td width="1%">&nbsp;</td>

							
						<%else %>
							<tr>
								<td width="3%">&nbsp;</td>
								<td width="1%">&nbsp;</td>
								
						<%end if%>
							
								<td align="left"><%=trim(rsPlan("descripcionCuenta_Pco"))%></td>
								<td align="right"><%=TotalCuenta%></td>
							</tr>
						
						<!-- Inicio de Articulos-->
						<%
											
						'Dim ObjArticulo
						'Dim rsArticulo
						
						'Set ObjArticulo=Server.CreateObject("PryUSAT.clsDatPresupuesto")
						'Set rsArticulo=Server.CreateObject("ADODB.Recordset")
						'Set rsArticulo= ObjArticulo.ConsultarPresupuesto ("RS","DE",lngCodigo_Pco,lngCodigo_Pto)
						'bytContarArticulo=0
						
						'if rsArticulo.recordcount>0 then%>
							<!--
							<tr>
							   <td colspan="4">
								 	<table border="0" align="center" width="90%" style="display:none" id="tblDetalleCuenta<%'=lngCodigo_Pco%>">
										 <tr class="etabla"> 
											<td>Item</td>
											<td width="70%">Detalle</td>
											<td>Unidad</td>
											<td>Precio</td>
											<td>Cant</td>
											<td>SubTotal</td>
										 </tr>				
										
										<%'do while not rsArticulo.eof
											'lngCodigo_Art=rsArticulo("Codigo_Art")
											'bytContarArticulo=bytContarArticulo+1%>
										
											<tr align="center" onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)" style="cursor:hand" onClick="AbrirPopUp('frmEditarDetallePresupuesto.asp?ca= <%'=lngCodigo_Art%>&cp=<%'=lngCodigo_Pto%>&da=<%'=rsArticulo("descripcion_art")%>&pa=<%'=rsArticulo("precioUnitario_Dpr")%>&ua=<%'=rsArticulo("unidad_art")%>&ct=<%'=rsArticulo("descripcionCuenta_Pco")%>&ta=<%'=rsArticulo("funcion_art")%>&tpto=<%'=strTipo_Pto%>&epto=A&vig=0','520','600','no','no','no',100,250)">
											   
												<td><%'=bytContarArticulo%></td>
												<td align="left"><%'=rsArticulo("descripcion_art")%></td>
												<td><%'=rsArticulo("unidad_Art")%></td>
												<td align="right"><%'=formatNumber(rsArticulo("precioUnitario_Dpr"))%></td>
												<td><%'=rsArticulo("cantidadTotal_Dpr")%></td>
												<td align="right"><%'=formatNumber(rsArticulo("total_Dpr"))%></td>
											   
											</tr>
											
											<%'rsArticulo.movenext
										'Loop%>		
									 </table>
							   </td>
							</tr>
						-->	
						  <%'end if
							'rsArticulo.close
							'set rsArticulo=Nothing
							'set ObjArticulo =Nothing%>
						 <!-- Fin de Articulos-->
						  	
						<%rsPlan.movenext
					Loop%>		
					  </table>
				  </td>
				</tr>
			<%end if
			 rsPlan.close
			 Set rsPlan=Nothing
			 %>
			 
			 <!-- Fin de Cuentas-->
		
		   <%rsConcepto.movenext
		loop
		rsConcepto.close
		set rsConcepto= Nothing
		%>
		
		<!--Fin de Conceptos -->
		<%
		obj.CerrarConexion
		set obj= Nothing
		%>
</table>
<!-- Fin de la Tabla que Contendra la Estructura Jerarquica-->
</body>
</html>