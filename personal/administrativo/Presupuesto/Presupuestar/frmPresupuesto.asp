<!--#include file="../../../../funciones.asp"-->
<html>
<head>
<script language="JavaScript">
function enviar(){

	document.frmPresupuesto.action = "frmPresupuesto.asp" 
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

    if (document.frmPresupuesto.txtNroPre.value == "") 
	{
       alert("Debe Ingresar el Numero del Presupuesto en el Año") 
		frmPresupuesto.txtNroPre.focus();
		return (false);	

   }

    if (document.frmPresupuesto.cboCentroCosto.value == "0") 
	{
       alert("Debe Indicar el Centro de Costos ") 
		frmPresupuesto.cboCentroCosto.focus();
		return (false);	

   }

    if (document.frmPresupuesto.txtFechaIni.value == "") 
	{
       alert("Debe Ingresar La Fecha de Inicio") 
		frmPresupuesto.txtFechaIni.focus();
		return (false);	

   }
    if (document.frmPresupuesto.txtFechaFin.value == "") 
	{
       alert("Debe Ingresar La Fecha de FIn") 
		frmPresupuesto.txtFechaFin.focus();
		return (false);	

   }
   
   
   	      	return (true);	

} 
</script>
<title>Elaborar Presupuesto</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<script language="JavaScript" src="../private/funciones.js"></script>

</head>

<body onload="ActivarCombo()">
<%
call Enviarfin(Session("codigo_Tfu"),"../../../../")

'Obtenemos El Tipo de Funcion del Usuario, Para Determinar si es Adminiatrador o No
Dim lngCodigo_Tfu

lngCodigo_Tfu = Session("codigo_Tfu")

Dim lngCodigo_Cco
Dim strAno_Pto
Dim bytNumero_Pto
Dim dtmFechaInicio_Pto
Dim dtmFechaFin_Pto
Dim strFormaDetalle_Pto
Dim strMoneda_Pto
Dim strObservacion_Pto
Dim strTipo_Pto
Dim strBandera
Dim strMsg

strBandera="0"
lngCodigo_Cco= request.form("cboCentroCosto")
strAno_Pto= request.form("txtAno")
bytNumero_Pto= request.form("txtNroPre")
strTipo_Pto = request.form("txtTipo_Pto")


strMsg= request.querystring("msg")

IF strTipo_Pto="" THEN
	strTipo_Pto=request.querystring("tpto")
END IF


if bytNumero_Pto="" then
	bytNumero_Pto="1"
end if


if trim(strAno_Pto) <>"" then


		Set objPresupuesto= Server.CreateObject("PryUSAT.clsDatPresupuesto")
		Set rsPresupuesto= Server.CreateObject("ADODB.RecordSet")
		set rsPresupuesto= objPresupuesto.BuscarPresupuesto("RS",lngCodigo_Cco,strAno_Pto,bytNumero_Pto,strTipo_Pto)
		
		if rsPresupuesto.recordcount >0 then
			'Ese  Presupuesto del Año ya Existe Para Ese Centro de Costos
			lngCodigo_Pto = rsPresupuesto("codigo_Pto")
			dtmFechaInicio_Pto= rsPresupuesto("fechaInicio_Pto")
			dtmFechaFin_Pto= rsPresupuesto("fechaFin_Pto")
			strFormaDetalle_Pto= rsPresupuesto("formaDetalle_Pto")
			strMoneda_Pto= rsPresupuesto("moneda_Pto")
			strObservacion_Pto= rsPresupuesto("observacion_Pto")
			strBandera="1"
		else
			Dim objParametro
			Dim rsParametro
			
			Set objParametro= Server.CreateObject("PryUSAT.clsDatPresupuesto")
			Set rsParametro= Server.CreateObject("ADODB.RecordSet")
			set rsParametro = objParametro.ConsultarParametroPresupuesto("RS",strAno_Pto) 	
		
			if rsParametro.recordcount >0 then
				strFormaDetalle_Pto= rsParametro("formaDetalle_Pto")
				strMoneda_Pto= rsParametro("moneda_Pto")
				dtmFechaInicio_Pto= rsParametro("fechaInicio_Pto")
				dtmFechaFin_Pto= rsParametro("fechaFin_Pto")
				estado_Pto= rsParametro("estado_Pto")
				strBandera="1"	
			else
			
				ruta="frmPresupuesto.asp?tpto=" & strTipo_Pto & "&" & "msg=1"
				response.Redirect(ruta)
			
			
			end if 
				
			rsParametro.close
			Set rsParametro=Nothing
			set objParametro=Nothing
			
			
		end if	

end if

if strFormaDetalle_Pto="M" THEN FormaDetalle_Pto="MENSUAL"
if strFormaDetalle_Pto="B" THEN FormaDetalle_Pto="BIMESTRAL"
if strFormaDetalle_Pto="T" THEN FormaDetalle_Pto="TRIMESTRAL"
if strFormaDetalle_Pto="S" THEN FormaDetalle_Pto="SEMESTRAL"
IF strMoneda_Pto="S" THEN Moneda_Pto="SOLES"
IF strMoneda_Pto="D" THEN Moneda_Pto="DOLARES"
IF strMoneda_Pto="E" THEN Moneda_Pto="EUROS"
%>

	<table align="center" border="0" width="100%">
		<tr> 
		  <!--<td colspan="5" width="708" align="center"><img src="../images/pres-centro.jpg"></td>-->
		  <%if strTipo_Pto="E" then %>
			<th align="center" class="table">PRESUPUESTO DE EGRESOS - CENTRO DE COSTOS</th>
		  <%else%>
			<th align="center" class="table">PRESUPUESTO DE INGRESOS - CENTRO DE COSTOS</th>
		  <%end IF %>
		</tr>
		<tr>
			<td><hr></td>
		</tr>

	</table>
	<form onSubmit="return validaSubmite()" action="Procesos.asp?Tipo=I001" method="post" name="frmPresupuesto" id="frmPresupuesto" >
	  <table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
	    <tr> 
	      <td width="40%" align="right" class="etabla">Año a Presupuestar:</td>
	      <td width="15%"><INPUT id=txtAno maxLength=4   name=txtAno size=4 value = "<% =strAno_Pto%>" onKeyPress="validarnumero()"  onKeyUp="ActivarCombo()"></td>
		  
	      <td width="8%" class="etabla">Nro Presupuesto:</td>
	      <td width="22%"><input type="hidden" name="txtTipo_Pto" value=<%=strTipo_Pto%> id="txtTipo_Pto"><INPUT type = "hidden" id=txtNroPre maxLength="2" name=txtNroPre size="2" value="<%=bytNumero_Pto%>"><%=bytNumero_Pto%>&nbsp;</td>
	    </tr>
	    <tr> 
	      <%

						
			Dim objCentroCosto
			Dim rsCentroCosto
	      
	      
			Set objCentroCosto=Server.CreateObject("PryUSAT.clsDatCentroCosto")
			Set rsCentroCosto=Server.CreateObject("ADODB.Recordset")
		
	
		      if lngCodigo_Tfu ="1" or lngCodigo_Tfu ="6" then '---------Puede Ver todos los Centros de costo
				
				Set rsCentroCosto= objCentroCosto.ConsultarCentroCosto ("RS","TO","")

		      else '-----------------------solo los centro de costo a los que pertenece
		      		
					Set rsCentroCosto= objCentroCosto.ConsultarCentroCosto ("RS","CP",Session("codigo_Usu"))
		      end if 	 	

	      
	      %>
              <% 

	      'Para Activar o desactivar combo de Centro de costo
		'if lngCodigo_Cco="" then 
			'ActCombo = "disabled=true"
			
		'else
			'ActCombo = "disabled=false"
		
		'end if

		%>
	      		


	      <td width="40%" align="right" class="etabla">Centro de Costos:</td>
	      <td colspan="3" width="45%"> 
	      
	      
	     
		  <select name="cboCentroCosto" id="cboCentroCosto" onChange="enviar()">
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
	    <tr> 


	      <%	

	      '-------- Solo el Administrador puede modificar las fechas	
	     'if lngCodigo_Tfu ="1" then
		 'ModFec=""
	     ' else
		 'ModFec="readonly"
	     'end if		
	     
	     
	     
	     
	      '-----------------------------------------------------------				
	      %>   	
		

	      <td width="40%" align="right" class="etabla">Fecha Inicio:</td>
	      <td width="15%"><input name="txtFechaIni" id="txtFechaIni"  <%=ModFec%>  size=10 value="<%=dtmFechaInicio_Pto%>"></td>
	      <td width="8%" class="etabla"> Fecha Fin:</td>
	      <td width="22%" ><input name="txtFechaFin" id="txtFechaFin"  <%=ModFec%> size=10 value="<%=dtmFechaFin_Pto%>"></td>
	      
	    </tr>
	    <tr> 
	      <td width="40%" align="right" class="etabla">Forma de Detalle:</td>
	      <td width="15%"><input type="hidden" name="cboFormaDetalle" id="cboFormaDetalle" value="<%=strFormaDetalle_Pto%>"><%=FormaDetalle_Pto%>
		     <!--<select name="cboFormaDetalle" id="cboFormaDetalle">-->
	          <%
			  	'seleccM=""
			  	'seleccB=""
				'SeleccT=""
				'SeleccS=""

				
			  	'if strFormaDetalle_Pto ="M"		then
					  	'seleccM="Selected"
				'end if

				'if strFormaDetalle_Pto ="B" then		
					  	'seleccB="Selected"
				'end if
				
				'if strFormaDetalle_Pto ="T" then		
					  	'seleccT="Selected"
				'end if
				
				'if strFormaDetalle_Pto ="S" then		
					  	'seleccS="Selected"
				'end if
				
			%>
			<!--
	          <option value="M" <%'=SeleccM%>>Mensual</option>
	          <option value="B" <%'=SeleccB%>>Bimestral</option>
	          <option value="T" <%'=SeleccT%>>Trimestral</option>
	          <option value="S" <%'=SeleccS%>>Semestral</option>
	        </select>--> &nbsp;</td>
	      <td width="8%" class="etabla">Moneda:</td>
	      <td width="22%"><input type="hidden" name="cboMoneda" id="cboMoneda" value="<%=strMoneda_Pto%>"><%=Moneda_Pto%>
	        <%
			  	'seleccS=""
				'SeleccD=""
				'SeleccE=""

				
			  	'if strMoneda_Pto="S" then		
					  '	seleccS="Selected"
				'end if
				
				'if strMoneda_Pto ="D" then		
					  	'seleccD="Selected"
				'end if
				
				'if strMoneda_Pto ="E" then		
					  	'seleccE="Selected"
				'end if
				
			%>
	        
			<!--<select name="cboMoneda" id="cboMoneda">
	          <option value="S" <%'=SeleccS%>>Soles</option>
	          <option value="D" <%'=SeleccD%>>Dolares</option>
	          <option value="E" <%'=SeleccE%>>Euros</option>
	        </select> -->&nbsp;</td>
	    </tr>
	    <tr> 
	      <td width="40%" align="right" class="etabla">Observación:</td>
	      <td width="45%" colspan="3"><input name="txtObservacion" id="txtObservacion" size="80" maxlength="100" value="<%=strObservacion_Pto%>"> 
	      </td>
	    </tr>
	    </table>
	    <%if strBandera="1" then%>
		
			<table width ="75%" align="center">
			<tr> 
			  <!--<td width="75%" colspan="4" align="center"><input type="button" class="nuevo" value="Elaborar Presupuesto" onclick="validaSubmite()"> -->
			
				  <td width="75%" colspan="4" align="center"><input type="submit" class="nuevo" value="Elaborar Presupuesto"> </td>
			</tr>
		  </table>
		 <%else
					if strMsg="1" then%>
						<p>
						<p>

						<table align="center"> 
						<tr><td><font color="#800000">No se han Establecido los 
                          Parámetros Para El Año Indicado. Comuníquese con Contabilidad</font></td></tr>
						
					<%end if
			
	     end if%>	
	</form>
</body>
</html>