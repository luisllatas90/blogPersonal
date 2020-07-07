<!--#include file="../../../../funciones.asp"-->
<html>
<head>
<script language="JavaScript">
function enviar(){

	document.frmPresupuesto.action = "frmEjecutado.asp" 
	document.frmPresupuesto.submit();
}
function ActivarCombo() {
	var Ctrl = document.frmPresupuesto.txtAno

	if (Ctrl.value.length == 4)
		{frmPresupuesto.cboCentroCosto.disabled=false}
	else
	        {frmPresupuesto.cboCentroCosto.disabled=true}
}


function validaSubmite(){ 
    if (document.frmPresupuesto.txtAno.value == "" || document.frmPresupuesto.txtAno.value.length < 4 ) 
	{
       alert("Debe Ingresar el año del Presupuesto") 
		frmPresupuesto.txtAno.focus();
		return (false);	
   }

  
    if (document.frmPresupuesto.cboCentroCosto.value == "0") 
	{
       alert("Debe Indicar el Centro de Costos ") 
		frmPresupuesto.cboCentroCosto.focus();
		return (false);	

   }

   
   	      	return (true);	

} 


	function EnviarDatos(pagina)
	{
		location.href=pagina + "?cboCentroCosto=" + frmPresupuesto.cboCentroCosto.value + "&txtAno=" + frmPresupuesto.txtAno.value
		//alert (txtPto.value);

	}

</script>
<title>Pedidos Almacén</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<script language="JavaScript" src="../private/funciones.js"></script>

</head>

<body onload="ActivarCombo()">
<%
call Enviarfin(Session("codigo_Tfu"),"../../../../")

'Obtenemos El Tipo de Funcion del Usuario, Para Determinar si es Adminiatrador o No
lngCodigo_Tfu = Session("codigo_Tfu")


strBandera="0"
lngCodigo_Cco= request.form("cboCentroCosto")
strAno_Pto= request.form("txtAno")
strTipo_Pto="E"
strMsg= ""

%>

	<table align="center" border="0" width="100%">
		<tr> 
		  <!--<td colspan="5" width="708" align="center"><img src="../images/pres-centro.jpg"></td>-->
		 	<th align="center" class="table">SALIDAS DE ALMACÉN POR CENTRO DE 
            COSTO</th>
		</tr>
		<tr>
			<td><hr></td>
		</tr>

	</table>
	<form onSubmit="return validaSubmite()" action="frmEjecutado.asp" method="post" name="frmPresupuesto" id="frmPresupuesto" >
	  <table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
	    <tr> 
	      <td width="40%" align="right" class="etabla">Año a Consultar:</td>
	      <td width="15%"><INPUT id=txtAno maxLength=4   name=txtAno size=4 value = "<% =strAno_Pto%>" onKeyPress="validarnumero()"  onKeyUp="ActivarCombo()"></td>
	    </tr>
	    <tr> 
	      <%

						
			Dim objCentroCosto
			Dim rsCentroCosto
	      
	      
			Set objCentroCosto=Server.CreateObject("PryUSAT.clsDatCentroCosto")
			Set rsCentroCosto=Server.CreateObject("ADODB.Recordset")
		
	
		      if lngCodigo_Tfu ="27" or lngCodigo_Tfu ="1" or lngCodigo_Tfu ="35" or lngCodigo_Tfu ="38" or lngCodigo_Tfu ="6" then '---------Puede Ver todos los Centros de costo
				
				Set rsCentroCosto= objCentroCosto.ConsultarCentroCosto ("RS","TO","")

		      else '-----------------------solo los centro de costo a los que pertenece
				
				
				Set rsCentroCosto= objCentroCosto.ConsultarCentroCosto ("RS","CP",Session("codigo_Usu"))
				

		      end if 	 	

	      
	      %>
          

	      <td width="40%" align="right" class="etabla">Centro de Costos:</td>
	      <td colspan="3" width="45%"> 
	      
	      
	     
		  <!-- <select name="cboCentroCosto" id="cboCentroCosto" onChange="enviar()"> -->
		  <select name="cboCentroCosto" id="cboCentroCosto"> 
	          	<option value="0">---Seleccione Centro de Costos--- </option>
		          <% do while not rsCentroCosto.eof 
			  		seleccionar="" 
			    		if (cint(lngCodigo_Cco)=rsCentroCosto("codigo_Cco")) then seleccionar="SELECTED " %>
		          		<option value= "<%=rsCentroCosto("codigo_Cco")%>" <%=seleccionar%>> <%=rsCentroCosto("descripcion_Cco") & "(" & rsCentroCosto("codigo_Cco") & ")"%></option>
		          		<% rsCentroCosto.movenext
			     loop
					rsCentroCosto.Close
					Set rsCentroCosto=Nothing 
					set objCentroCosto= Nothing
			   %>
	        </select> 

	       </td>
	    </tr>
	    
	    </table>
	   
		
			<table width ="720" align="center">
			<tr> 
			  <!--<td width="75%" colspan="4" align="center"><input type="button" class="nuevo" value="Elaborar Presupuesto" onclick="validaSubmite()"> -->
				  <td width="472" colspan="4" align="right"><input type="submit" class="nuevo" value="Ejecutar Consulta"> </td>
				  <td align="center" valign="bottom" width="238">
                  <input type="button" value="Exportar a Excel" name="cmdExportar" id="cmdexportar" class="boton" onClick="EnviarDatos('ExportarEjecutado.asp')" style="float: left"></td>

			</tr>
		  </table>

	</form>
		  
		  	<%
		  	
		  	if trim(strAno_Pto) <>"" and  lngCodigo_Cco <> "" then
		
				Set objPresupuesto= Server.CreateObject("PryUSAT.clsAccesoDatos")
				objPresupuesto.AbrirConexion
					Set rsPresupuesto= Server.CreateObject("ADODB.RecordSet")
					set rsPresupuesto= objPresupuesto.consultar("AlmacenSQLConsolidadoSalidaPorArea","FO",lngCodigo_Cco,strAno_Pto)
				objPresupuesto.CerrarConexion
	
				if rsPresupuesto.recordcount >0 then
						contar = 0
						total = 0
						
						%>
						 <table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
						 	<tr>
						 		<td colspan=4><%response.write "Area: " & rsPresupuesto("descripciónAre")  & " - " & strAno_Pto %> &nbsp;</td>	
						 	</tr>
							<tr>
								<td width="5%" class="etabla">ITEM</td>
								<td width="65%" class="etabla">DETALLE DE ARTICULO</td>
								<td width="10%" class="etabla">CANTIDAD TOTAL</td>
								<td width="20%" class="etabla">COSTO TOTAL</td>
							 </tr>
							
							<%DO WHILE NOT rsPresupuesto.EOF
								contar = contar +1
								%>
								
								<TR>	
									<td width="5%" align="center"><%=contar%>&nbsp;</td>
									<td width="65%"><%=TRIM(rsPresupuesto("detalleArticulo"))%>&nbsp;</td>
									<td width="10%" align="center"><%=rsPresupuesto("cantidadTotal")%>&nbsp;</td>
									<td width="20%" align="right"><%=formatNumber(TRIM(rsPresupuesto("subTotal")),4)%>&nbsp;</td>
								
								</TR>
							
							  <% total = total + rsPresupuesto("subTotal")
							  	rsPresupuesto.MOVENEXT
							LOOP%>
								<tr>	 
									<td colspan=2> </td>
									<td class="etabla">TOTAL (S/. ) :</td>
									<td class="etabla"><%=formatNumber(total,4)%> &nbsp;</td>
								<tr>	
						<table>

				<%else%>
					
						<p>
						<p>

						<table align="center"> 
						<tr><td><font color="#800000">No se ha encontrado presupuesto ejecutado para su centro de costos. 
                          </font></td></tr>
				<%end if
		
			end if

		  	%>
	    
</body>
</html>