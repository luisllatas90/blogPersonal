<script>
	function EnviarDatos(pagina)
	{
		location.href=pagina + "?id=" + txtPto.value
	}
	
	function Volver()
	{
		history.back(-1)
	}
	
	
	
</script>

<%
	strAno=Request.QueryString("ano") 
    lngCodigo_Cco=Request.QueryString("cco") 
    strTipo_Pto=Request.QueryString("tip") 

    
	'Consultar Presupuesto
    Set objPresupuesto=Server.CreateObject("PryUSAT.clsDatPresupuesto")
	Set rsPresupuesto=Server.CreateObject("ADODB.Recordset")
	Set rsPresupuesto= objPresupuesto.ConsultarRubrosPresupuesto ("RS","CB",strAno,lngCodigo_Cco,strTipo_Pto,"")
	
	
	
	lngCodigo_Pto=trim(rsPresupuesto("codigo_Pto"))
	strEstado_Pto=trim(rsPresupuesto("estado_Pto"))
	
	
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

		<%If strTipo_Pto="E" then%>
			<th align="center" class="table">PRESUPUESTO DE EGRESOS - CENTRO DE COSTOS</th>
		<%else%>
			<th align="center" class="table">PRESUPUESTO DE INGRESOS - CENTRO DE COSTOS</th>
		<%end if%>
  </tr>
  <tr>
  <td><hr></td>
  </tr>
</table>
<!--Fin del Titulo -->

<!-- Cabecera de la Pagina-->
<table width="100%" align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" >
  <tr> 
    <td width="21%" class="etabla">Año:</td>
    <td width="17%"><%=rsPresupuesto("año_Pto")%>&nbsp;</td>
    <td width="18%" class="etabla">Nro. Presup. </td>
    <td width="10%"><%=rsPresupuesto("numero_Pto") %>&nbsp;</td>
    <td width="10%" class="etabla">Estado:</td>
    <%if strEstado_Pto="P" then%>
		<td width="10%"><b><font color="#FF0000">NO APROBADO</font></b></td>
    <%else%>
		<td width="10%"><b><font color="#FF0000">APROBADO</font></b></td>
    <%end if%>
  </tr>
  <tr> 
    <td class="etabla" >Fecha Inicio:</td>
    <td><% =rsPresupuesto("fechaInicio_Pto") %>&nbsp;</td>
    <td class="etabla">Fecha Fin:</td>
    <td width="44%" colspan="3" ><%=rsPresupuesto("fechaFin_Pto") %>&nbsp;</td>
  </tr>
  <tr> 
    <td width="21%" class="etabla">Centro de Costos:</td>
    <td colspan="5" bgcolor="#FEFFE1"><% =rsPresupuesto("descripcion_Cco") %>&nbsp;</td>
    
  </tr>
  <tr> 
    <td width="21%" class="etabla">Total General:</td>
    <td width="17%"><%= simbolo & " " & formatNumber(rsPresupuesto("totalGeneral_Pto"))%>&nbsp;</td>
    <td class="etabla">Moneda:</td>
    <td colspan="3"><%=strMoneda_Pto%> &nbsp;</font></td>
   
  </tr>
</table>
   <%
		rsPresupuesto.Close 
		set rsPresupuesto= Nothing

    %>
<!-- Fin de la Cabecera de la Pagina-->


<!-- Lo utilizo para el parametro de exportacion-->
<input type="hidden" name="txtPto" id="txtPto" value="<%=lngCodigo_Pto%>">

<!-- Botones de Opciones-->
<table align="center" width="900" border="0" cellpading="0" height="35">
    <tr> 
          <td height="31" width="402"><b>Consolidado de Presupuesto</b></td>
          <td width="213" height="31">
          <input type="button" value="Regresar" onclick="Volver()" class="salir" style="float: right"></td>
          <td align="center" height="31" width="271">
          <input type="button" value="Exportar a Excel" name="cmdExportar" id="cmdexportar" class="boton" onClick="EnviarDatos('../PresupuestarConsultar/ExportarConsolidadoPresupuesto.asp')" style="float: left"></td>
   </tr>

</table>
<!-- Fin de Botones de Opciones-->


<!-- Tabla que Contendra la Estructura Jerarquica-->
<table width="100%" align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111">

        <!-- Cabecera de la Tabla Jerarquica -->
		<tr class="etabla"> 
		  <td>Item</td>
		  <td>Descripcion</td>
		  <td>Total(<%=simbolo%>)</td>
		</tr>
		<!-- Fin de la Cabecera de la Tabla Jerarquica -->
		
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
			Set objPlan=Server.CreateObject("PryUSAT.clsDatPlanContable")
			Set rsPlan=Server.CreateObject("ADODB.Recordset")
			Set rsPlan= objPlan.ConsultarPlanContable ("RS","PR",lngCodigo_Pto,lngCodigo_Cie)
			
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
							<tr onClick="MostrarTabla(document.all.tblDetalleCuenta<%=lngCodigo_Pco%>,imgDC<%=lngCodigo_Pco%>)" style="cursor:hand">
							    <td width="3%">&nbsp;</td>
								<td width="1%"><img id="imgDC<%=lngCodigo_Pco%>" src="../images/mas.gif" width="9" height="9">
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
											
						Dim ObjArticulo
						Dim rsArticulo
						
						Set ObjArticulo=Server.CreateObject("PryUSAT.clsDatPresupuesto")
						Set rsArticulo=Server.CreateObject("ADODB.Recordset")
						Set rsArticulo= ObjArticulo.ConsultarPresupuesto ("RS","DE",lngCodigo_Pco,lngCodigo_Pto)
						bytContarArticulo=0
						
						if rsArticulo.recordcount>0 then%>
							<tr>
							   <td colspan="4">
								 	<table border="0" align="center" width="90%" style="display:none" id="tblDetalleCuenta<%=lngCodigo_Pco%>" height="18">
										 <tr class="etabla"> 
											<td height="14">Item</td>
											<td width="70%" height="14">Detalle</td>
											<td height="14">Unidad</td>
											<td height="14">Precio</td>
											<td height="14">Cant</td>
											<td height="14">SubTotal</td>
										 </tr>				
										
										<%do while not rsArticulo.eof
											lngCodigo_Art=rsArticulo("Codigo_Art")
											bytContarArticulo=bytContarArticulo+1%>
										
											<tr align="center" onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)" style="cursor:hand" onClick="AbrirPopUp('../PresupuestarConsultar/frmEditarDetallePresupuesto.asp?ca= <%=lngCodigo_Art%>&cp=<%=lngCodigo_Pto%>&da=<%=rsArticulo("descripcion_art")%>&pa=<%=rsArticulo("precioUnitario_Dpr")%>&ua=<%=rsArticulo("unidad_art")%>&ct=<%=rsArticulo("descripcionCuenta_Pco")%>&ta=<%=rsArticulo("funcion_art")%>&tpto=<%=strTipo_Pto%>&epto=A&vig=0','520','600','no','no','no',100,250)">
											   
												<td height="1"><%=bytContarArticulo%></td>
												<td align="left" height="1"><%=rsArticulo("descripcion_art")%></td>
												<td height="1"><%=rsArticulo("unidad_Art")%></td>
												<td align="right" height="1"><%=formatNumber(rsArticulo("precioUnitario_Dpr"))%></td>
												<td height="1"><%=rsArticulo("cantidadTotal_Dpr")%></td>
												<td align="right" height="1"><%=formatNumber(rsArticulo("total_Dpr"))%></td>
											   
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