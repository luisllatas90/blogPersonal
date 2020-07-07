<script>
	function EnviarDatos(pagina)
	{
		location.href=pagina + "?id=" + txtPto.value
		//alert (txtPto.value);
		
	}
</script>

<%
Dim bytContar
Dim lngCodigo_Pto
Dim strMoneda
Dim strTipo_Pto
Dim strEstado_Pto
Dim lngCodigo_Cco


    lngCodigo_Pto=Request.QueryString("id") 

    Vigencia_Pto=Request.QueryString("Vig") 

	'response.write "Año: "  + Vigencia_Pto

   'En este caso esta regresando de buscar articulo, osea todavia esta vigente ya que puede agregar articulo
	   if trim(Vigencia_Pto)="" then 
		Vigencia_Pto="1"
	    end if
	   	

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
	
	
%>
<html>
<head>
	<meta http-equiv="Content-Language" content="es">
	<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
	<meta name="ProgId" content="FrontPage.Editor.Document">
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
	<title>Consultar Consolidado de Presupuesto</title>
	<link rel="stylesheet" type="text/css" href="../private/estilo.css">
	<script language="JavaScript" src="../private/funciones.js"></script>
	
	<script language="Javascript">
		function eliminartodo()
		{
			if (confirm("¿Está seguro que desea eliminar Todo el presupuesto?")==true){
				return(true)
			}
			else{
				return(false)
			}
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
	</script>
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

<!-- Opcion de Importar Datos-->

<% if trim(strEstado_Pto)="P" and cbool(Vigencia_Pto)=True then%>

	<table align="center" width="100%">
		<tr style="cursor:hand">
		<td width="25%" align="left" onClick="AbrirPopUp('frmBuscarPresupuesto.asp?cpton=<%=trim(rsPresupuesto("codigo_Pto"))%>&anon=<%=Trim(rsPresupuesto("Año_Pto"))%>&cco=<%=Trim(rsPresupuesto("codigo_Cco"))%>&tip=<%=Trim(rsPresupuesto("tipo_Pto"))%>','250','500','YES','YES','YES',200,300)">
	        <img border="0" src="../images/envio.gif" align="baseline" width="20" height="20"> Importar Presupuesto</A></td>
		</tr>
		<!-- <A href="frmBuscarPresupuesto.asp?cpton=<%=trim(rsPresupuesto("codigo_Pto"))%>&anon=<%=Trim(rsPresupuesto("Año_Pto"))%>&cco=<%=Trim(rsPresupuesto("codigo_Cco"))%>&tip=<%=Trim(rsPresupuesto("tipo_Pto"))%>"style="text-decoration:none">-->

	</table>
<%end if%>

<!--Fin de Opcion de Importar Datos --->


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


<!-- lo utilizo para el parametro de exportacion-->
<input type="hidden" name="txtPto" id="txtPto" value="<%=lngCodigo_Pto%>">

<!-- Botones de Opciones-->
<table align="center" width="100%" border="0" cellpading="0">

    <tr> 
          <td><b>Consolidado de Presupuesto</b></td>
          <td align="center" valign="bottom"><input type="button" value="Exportar a Excel" name="cmdExportar" id="cmdexportar" class="boton" onClick="EnviarDatos('../PresupuestarConsultar/ExportarConsolidadoPresupuesto.asp')" style="float: right"></td>
	      
	      <% if strEstado_Pto="P" and cbool(Vigencia_Pto)=True then%>
	  	   <form action="Procesos.asp?Tipo=D001" onSubmit="return eliminartodo()" name="frmEliminar" method="post">
		      <td align="right" width="30%"><input type="hidden" name="txtcodigo_Pto" value="<%=lngCodigo_Pto%>"><input type="Submit" class="eliminar" name="cmdEliminar" value="Borrar Presupuesto" title="Borrar Todo el Presupuesto"></td>
		      <td><input type="hidden" name="txtTipo_Pto" value="<%=strTipo_Pto%>"></td>
    	   	   </form>
      	 
		  <form name="frmNuevo" method ="post" action="frmbuscararticulo.asp">
		     <td width="20%" colspan="2">
		        <input type="hidden" name="txtCodigo_Pto" value="<%=lngCodigo_Pto%>">
		        <input type="hidden" name="txtTipo_Pto" value="<%=strTipo_Pto%>">
		        <input type="hidden" name="txtVig_Pto" value="<%=Vigencia_Pto%>">
		        <input type="Submit" class="nuevo" name="cmdAgregar" value="Agregar Registro" title="Agregar Registros al Presupuesto">
		     </td>
		  </form>
		 
		
	    <%end if%>
 
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
								 	<table border="0" align="center" width="90%" style="display:none" id="tblDetalleCuenta<%=lngCodigo_Pco%>">
							
							    	<% if strEstado_Pto="P" and cbool(Vigencia_Pto)=True then%>
									    <form method="post" action="Procesos.asp?Tipo=D002" name = "form<%=lngCodigo_Pco%>" onSubmit="return eliminaritem()">
							      	<% end if%>
								    
										 <tr class="etabla"> 
											 
										 <% if strEstado_Pto="P" and cbool(Vigencia_Pto)=True then%>
											<td><input type="submit" class="eliminar2" value="" name="cmd<%=lngCodigo_Pco%>"  height="1%"><input type="hidden" name="txtCod_Pto" value="<%=lngCodigo_Pto%>"></td>
											<td align="center">
                                                                                        <input type="checkbox" name="chkmarcartodo<%=lngCodigo_Pco%>" onClick="if (this.checked) {SeleccionarTodos(form<%=lngCodigo_Pco%>.chkEliminar)} else {QuitarTodos(form<%=lngCodigo_Pco%>.chkEliminar)}" value="ON"></td>
										 <%else%>
	  									   <td>&nbsp;</td>
									 
										 <%end if%> 
												
											<td>Item</td>
											<td width="70%">Detalle</td>
											<td>Unidad</td>
											<td>Precio</td>
											<td>Cant</td>
											<td>SubTotal</td>
										 </tr>				
										
										<%do while not rsArticulo.eof
											lngCodigo_Art=rsArticulo("Codigo_Art")
											bytContarArticulo=bytContarArticulo+1%>
										
											<tr align="center" onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)" style="cursor:hand">
												
												
											<%if trim(imgArt)=trim(lngCodigo_Art) then%>
												<td width="1%"><img src="../images/der.gif"></td>
											<%else%>
													
												<td>&nbsp;</td>
											<%end if%>
												
											<% if strEstado_Pto="P" and cbool(Vigencia_Pto)=True  then%>
												<td ><input type="checkbox" name="chkEliminar" value="<%=lngCodigo_Art%>" ></td>
											<%end if%>
											   
												<td onClick="AbrirPopUp('../PresupuestarConsultar/frmEditarDetallePresupuesto.asp?ca=<%=lngCodigo_Art%>&cp=<%=lngCodigo_Pto%>&da=<%=trim(rsArticulo("descripcion_art"))%>&pa=<%=rsArticulo("precioUnitario_Dpr")%>&ua=<%=rsArticulo("unidad_art")%>&ct=<%=rsArticulo("descripcionCuenta_Pco")%>&ta=<%=rsArticulo("funcion_art")%>&tpto=<%=strTipo_Pto%>&epto=<%=strEstado_Pto%>&vig=<%=Vigencia_Pto%>','520','600','no','no','no',100,250)"><%=bytContarArticulo%></td>
												<td align="left" onClick="AbrirPopUp('../PresupuestarConsultar/frmEditarDetallePresupuesto.asp?ca= <%=lngCodigo_Art%>&cp=<%=lngCodigo_Pto%>&da=<%=trim(rsArticulo("descripcion_art"))%>&pa=<%=rsArticulo("precioUnitario_Dpr")%>&ua=<%=rsArticulo("unidad_art")%>&ct=<%=rsArticulo("descripcionCuenta_Pco")%>&ta=<%=rsArticulo("funcion_art")%>&tpto=<%=strTipo_Pto%>&epto=<%=strEstado_Pto%>&vig=<%=Vigencia_Pto%>','520','600','no','no','no',100,250)"><%=rsArticulo("descripcion_art")%></td>
												<td onClick="AbrirPopUp('../PresupuestarConsultar/frmEditarDetallePresupuesto.asp?ca= <%=lngCodigo_Art%>&cp=<%=lngCodigo_Pto%>&da=<%=trim(rsArticulo("descripcion_art"))%>&pa=<%=rsArticulo("precioUnitario_Dpr")%>&ua=<%=rsArticulo("unidad_art")%>&ct=<%=rsArticulo("descripcionCuenta_Pco")%>&ta=<%=rsArticulo("funcion_art")%>&tpto=<%=strTipo_Pto%>&epto=<%=strEstado_Pto%>&vig=<%=Vigencia_Pto%>','500','600','no','no','no',100,250)"><%=rsArticulo("unidad_Art")%></td>
												<td onClick="AbrirPopUp('../PresupuestarConsultar/frmEditarDetallePresupuesto.asp?ca= <%=lngCodigo_Art%>&cp=<%=lngCodigo_Pto%>&da=<%=trim(rsArticulo("descripcion_art"))%>&pa=<%=rsArticulo("precioUnitario_Dpr")%>&ua=<%=rsArticulo("unidad_art")%>&ct=<%=rsArticulo("descripcionCuenta_Pco")%>&ta=<%=rsArticulo("funcion_art")%>&tpto=<%=strTipo_Pto%>&epto=<%=strEstado_Pto%>&vig=<%=Vigencia_Pto%>','520','600','no','no','no',100,250)"><%=formatNumber(rsArticulo("precioUnitario_Dpr"))%></td>
												<td onClick="AbrirPopUp('../PresupuestarConsultar/frmEditarDetallePresupuesto.asp?ca= <%=lngCodigo_Art%>&cp=<%=lngCodigo_Pto%>&da=<%=trim(rsArticulo("descripcion_art"))%>&pa=<%=rsArticulo("precioUnitario_Dpr")%>&ua=<%=rsArticulo("unidad_art")%>&ct=<%=rsArticulo("descripcionCuenta_Pco")%>&ta=<%=rsArticulo("funcion_art")%>&tpto=<%=strTipo_Pto%>&epto=<%=strEstado_Pto%>&vig=<%=Vigencia_Pto%>','520','600','no','no','no',100,250)"><%=rsArticulo("cantidadTotal_Dpr")%></td>
												<td align="right" onClick="AbrirPopUp('../PresupuestarConsultar/frmEditarDetallePresupuesto.asp?ca= <%=lngCodigo_Art%>&cp=<%=lngCodigo_Pto%>&da=<%=trim(rsArticulo("descripcion_art"))%>&pa=<%=rsArticulo("precioUnitario_Dpr")%>&ua=<%=rsArticulo("unidad_art")%>&ct=<%=rsArticulo("descripcionCuenta_Pco")%>&ta=<%=rsArticulo("funcion_art")%>&tpto=<%=strTipo_Pto%>&epto=<%=strEstado_Pto%>&vig=<%=Vigencia_Pto%>','520','600','no','no','no',100,250)"><%=formatNumber(rsArticulo("total_Dpr"))%></td>
											</tr>
											
											<%rsArticulo.movenext
										Loop%>		
							
									    </form>
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