<html>
<head>
	<title>Consultar Consolidado de Presupuesto</title>	
	<link rel="stylesheet" type="text/css" href="../private/estilo.css">
	<script language="JavaScript" src="../private/funciones.js"></script>
	<script language="JavaScript" src="../../../../private/funciones.js"></script>

	<script>
	function EnviarDatos(pagina)
	{
		//location.href=pagina + "?id=" + txtPto.value

		location.href=pagina
		
		//alert (txtPto.value);
		
	}
	
	function HabilitarBoton(orden)
	{
		if (orden == 1){
			{frmParametro.cmdExportar.disabled=true} }
		else {
	        {frmParametro.cmdExportar.disabled=false}}
		
		
	}
	
	
	
    </script>

</head>

<body>
<%
Dim bytContar
Dim strAno
Dim strTotalGeneral
Dim strTipoConsulta
Dim seleccionar1
Dim seleccionar2


strAno=Request.form("txtParametro") 
if strAno="" then
   strAno=request.querystring("ano")
end if

strTipoConsulta=""
strTipoConsulta=Request.QueryString("tcon")

if strTipoConsulta="2" then seleccionar1="checked" : seleccionar2="checked"
if strTipoConsulta="A" then seleccionar1="checked"
if strTipoConsulta="P" then seleccionar2="checked"


if trim(strTipoConsulta)="" then
		seleccionar1="checked" 
		seleccionar2="checked"
		
		if trim(Request.form("chkAprobado"))="" and trim(Request.form("chkDesaprobado"))="" then
			seleccionar1="checked"
			seleccionar2="checked"
		else

	
			if trim(Request.form("chkAprobado"))="A" THEN
				if trim(Request.form("chkDesaprobado"))="P" THEN
					strTipoConsulta="2"	
					seleccionar1="checked"
					seleccionar2="checked"
				ELSE
					strTipoConsulta="A"	
					seleccionar1="checked"
					seleccionar2=""
				END IF
			ELSE
				if trim(Request.form("chkDesaprobado"))="A" THEN
					strTipoConsulta="2"	
					seleccionar1="checked"
					seleccionar2="checked"
		
				ELSE
					strTipoConsulta="P"	
					seleccionar2="checked"
					seleccionar1=""
		
				END IF
			end if
		end if
end if


'Creamos Objetos
Set objPresupuesto = Server.CreateObject("PryUSAT.clsDatPresupuesto")
Set rsPresupuesto=Server.CreateObject("ADODB.Recordset")
Set rsTotalEmpresa=Server.CreateObject("ADODB.Recordset")
Set rsCentroCosto=Server.CreateObject("ADODB.Recordset")


Set rsTotalEmpresa= objPresupuesto.ConsultarConsolidadoPresupuesto("RS","TO",strAno,strTipoConsulta,"","")

IF rsTotalEmpresa.recordcount >0 then
	strEgresoAnual= formatNumber(rsTotalEmpresa("totalEgreso"))
	strIngresoAnual=formatNumber(rsTotalEmpresa("totalIngreso"))
	strDiferencia=formatNumber( cdbl(strIngresoAnual) - cdbl(strEgresoAnual))
	strMoneda=rsTotalEmpresa("moneda")
else
	strEgresoAnual="S/. 0.00"
	strIngresoAnual="S/. 0.00"
	strMoneda="S/."
end if


%>
<!-- Titulo de la Pagina-->
<table align="center" width="100%">
  <tr>
      <th align="center" class="table">TOTAL PRESUPUESTADO PARA EL AÑO - <%= strAno%> </th>
  </tr>
  <tr>
	<td><hr></td>
  </tr>
</table>
<!-- Fin de Titulo de la Pagina -->
<P>

<!-- Parametros de Consulta  -->
<form name="frmParametro" method="post" action="frmconsolidadopresupuestoTotal.asp">

<table width="900">
 <tr>
   <td width="292"> 
  	  <table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="278">
          <tr>
	       <td  class="etabla" width="34">Año:</td>
	   		<td  align="center" width="48"><input type="text" name="txtParametro" size="4" maxlength="4" value="<%=strAno%>" onKeyPress="validarnumero()"></td>
			<td width="174"><input type="checkbox" name="chkAprobado" value="A"  <%=seleccionar1%>>Aprobados<input type="checkbox" name="chkDesaprobado" value="P"  <%=seleccionar2%>>No Aprobados
           </td>
		</tr>	
	  </table>
   </td>
   <td width="241">
   <table>
   		<!-- onclick="AbrirMensaje()" -->
   		<tr>
		<td > <input type="submit"  value="Consultar" name="cmdConsultar" class="boton"> </td>
		<td align="center" valign="bottom"><input type="button" value="Exportar a Excel" name="cmdExportar" id="cmdexportar" class="boton" onClick="EnviarDatos('ExportarTotalPresupuestado.asp?ano=<%=strAno%>&tc=<%=strTipoConsulta%>')" style="float: right"></td>
		</tr>
	</table>
	</td>
	<td width="353">
      <table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="234">
       <tr>
		  <td class="etabla" width="49">Leyenda</td>
		  <td width="170"><img border="0" src="../images/R.gif" align="baseline"> No Aprobados&nbsp; <img border="0" src="../images/A.gif" align="baseline">&nbsp; Aprobados</td>
	   </tr>
	  </table> 
	 </td> 
   </td>	 
</tr>   
</table>
</form>
	
<!-- Fin de Parametros de la Consulta-->



<!--Empezamos a Construir la Estructura Jerarquica  -->

<%IF trim(strAno)<>"" then %>
	<script>
		HabilitarBoton(0);
	</script>
    <!--I1: Creamos Tabla que Contendra Toda la Estructura --> 
	<table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
		  
		  <!-- Cabecera de la Tabla de Estructura-->
		  <tr><td colspan="7">Haga click sobre el monto del egreso para ver el detalle</td></tr>
		  <tr align="center">
		  	<td colspan="2" align="right"><b>Total (<%=strMoneda%>)</b></td>
			<td><%=strIngresoAnual%>&nbsp;</td>
			<td><%=strEgresoAnual%>&nbsp;</td>
			<td><%=strDiferencia%>&nbsp;</td>
			<td colspan="2" align="center">&nbsp;</td>
		  </tr>
		  
		  <tr class="etabla"> 
		    <td>Item</td>
		    <td>Descripcion</td>
		    <td>Ingresos (<%=strMoneda%>)</td>
		    <td>Egresos (<%=strMoneda%>)</td>
		    <td>Diferencia (<%=strMoneda%>)</td>
			<td width="1%">Est</td>
			
		  </tr>
		  <!-- Fin de Cabecera de la Tabla de Estructura-->
		  

		   <% 		   
			Set rsCentroCosto= objPresupuesto.ConsultarRubrosPresupuesto("RS","CC",strAno,strTipoConsulta,"","")
		   %>

            <!-- Iniciamos el Bucle de Centro de Costos   --> 
			
			  <%
			  contar=0
			  Do while Not rsCentroCosto.eof   
			  	contar = contar + 1
				lngCodigo_Cco = rsCentroCosto("codigo_Cco")
				strEstado_Cco=TRIM(rsCentroCosto("estado_Pto"))
				strCentro=trim(rsCentroCosto("descripcion_Cco"))
							   
				'Calcular Total Por Centro de Costos
				totalEgreso=formatNumber(rsCentroCosto("TotalEgreso"))
				totalIngreso=formatNumber(rsCentroCosto("TotalIngreso"))
				diferencia=formatNumber(totalIngreso - totalEgreso)
				 %>
			 	 
			 	 <tr onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)"> 
 			        <td align="center" width="1%"><%=contar%>&nbsp;</td>
					 <td align="left" width="50%" ><%=trim(strCentro)%>&nbsp;</td>
					 <% if cdbl(totalIngreso)>0 then%>
						<td width="15%" align="right"  onclick="location.href='frmDetallePresupuestoCentroCosto.asp?ano=<%=strAno%>&cco=<%=lngCodigo_Cco%>&tip=I'" style="cursor:hand"><%=totalIngreso%>&nbsp;</td>
					 <% else%>
						<td width="15%" align="right"><%=totalIngreso%>&nbsp;</td>
					 <%end if%>
					 
					 <%if cdbl(totalEgreso)>0 then%>
						<td width="15%" align="right" onclick="location.href='frmDetallePresupuestoCentroCosto.asp?ano=<%=strAno%>&cco=<%=lngCodigo_Cco%>&tip=E'" style="cursor:hand"><%=totalEgreso%>&nbsp;</td>
					 <%else%>
						<td width="15%" align="right"><%=totalEgreso%>&nbsp;</td>
					 <%end if%>
					 
					 <td width="15%" align="right"><%=diferencia%>&nbsp;</td>
					  <%if strEstado_Cco="P" then%>
						 <td align="center"><img border="0" src="../images/R.gif" align="baseline"></td>
					 <%else%>
						 <td align="center"><img border="0" src="../images/A.gif" align="baseline"></td>
				     <%end if%>
				 </tr>			
			    <%rsCentroCosto.movenext
		      Loop 
		      %>	
		<!-- Fin  del Bucle de Centro de Costos   --> 
   </table>
   <!--F1: Cerramos Tabla que Contiene Toda la Estructura  -->
   
  <%else%>
	
		<script>
			HabilitarBoton(1);
		</script>
 <%End if

Set objPresupuesto = Nothing
Set rsPresupuesto=Nothing
Set rsTotalEmpresa=Nothing
Set rsCentroCosto=Nothing

%>

</body>
</html>