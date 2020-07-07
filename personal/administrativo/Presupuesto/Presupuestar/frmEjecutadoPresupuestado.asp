<!--#include file="../../../../funciones.asp"-->
<html>
<head>
<script language="JavaScript">
function enviar(){

	document.frmPresupuesto.action = "frmEjecutadoPresupuestado.asp" 
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
		location.href=pagina + "?cboCentroCosto=" + frmPresupuesto.cboCentroCosto.value + "&txtAno=" + frmPresupuesto.txtAno.value  + "&cboNivel=" + frmPresupuesto.cboNivel.value
		//alert (txtPto.value);

	}

</script>
<title>Presupuestado Vs. Ejecutado</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<script language="JavaScript" src="../private/funciones.js"></script>

<style>
<!--
.negativo    { color: #7C2128;}
//.negativo    { color: #7C2128; font-weight: bold }
//.negativo    { color: #000000; font-weight: bold }
.negativoMonto  { color: #FF0000; font-weight: bold}
.negativoTotal 	{ color:#FF0000; font-weight: bold; background-color: #FFFDD2; text-align:right }
.positivoTotal 	{ color:#0000FF; font-weight: bold; background-color: #FFFDD2; text-align:right }
-->
</style>

</head>

<body onload="ActivarCombo()">
<%
call Enviarfin(Session("codigo_Tfu"),"../../../../")

'Obtenemos El Tipo de Funcion del Usuario, Para Determinar si es Adminiatrador o No
lngCodigo_Tfu = Session("codigo_Tfu")


strBandera="0"
lngCodigo_Cco= request.form("cboCentroCosto")
strAno_Pto= request.form("txtAno")
strNivel = request.form("cboNivel")
strTipo_Pto="E"
strMsg= ""
%>

	<table align="center" border="0" width="100%">
		<tr> 
		  <!--<td colspan="5" width="708" align="center"><img src="../images/pres-centro.jpg"></td>-->
		 	<th align="center" class="table">PRESUPUESTADO VS.&nbsp; EJECUTADO</th>
		</tr>
		<tr>
			<td><hr></td>
		</tr>

	</table>
	<form onSubmit="return validaSubmite()" action="frmEjecutadoPresupuestado.asp" method="post" name="frmPresupuesto" id="frmPresupuesto" >
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
		
	
		      if lngCodigo_Tfu ="27"  or lngCodigo_Tfu ="1" or lngCodigo_Tfu ="35" or lngCodigo_Tfu ="38" or lngCodigo_Tfu ="6" then '---------Puede Ver todos los Centros de costo
				'Alta Gerencia, Administrador, consultor presupuesto, contador y Adm. General
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
	    <tr>
	    	 <td width="40%" align="right" class="etabla">Tipo de Consulta:</td>
    	       <td>
	       		<Select name="cboNivel" id="cboNivel">
	       			<%
	       			if strNivel="2" then
	       				sel1=""
	       				sel2="SELECTED"
	       			else
	       				sel2=""
	       				sel1="SELECTED"
	       			
	       			End if
	       			%>
	       			<option value="1" <%=sel1%>> Detallada
	       			<option value="2" <%=sel2%>> Resumida
	       		</Select>
		       </td>

	    </tr>
	    
	    </table>
	   
		
			<table width ="961" align="center">
			<tr> 
			  	  
				  <td align="right" ><input type="submit" class="nuevo" value="Consultar Ejecutado"> </td>
				  <td align="center" valign="bottom" width="150"><input type="button" value="Exportar a Excel" name="cmdExportar" id="cmdexportar" class="boton" onClick="EnviarDatos('ExportarEjecutadoPresupuestado.asp')" style="float: right"></td>
			</tr>
			<tr>
				  <td colspan=2><font color="#800000"><b>&nbsp;</b><b>Nota:</b></font> 
                  El presupuesto ejecutado se esta calculando en base a todas 
                  las salidas de almacén registradas para el centro de costo.</td>			
            </tr>
		  </table>

	</form>
		  
		  	<%
		  	
		  	if trim(strAno_Pto) <>"" and  lngCodigo_Cco <> "" then
		
				Set objPresupuesto= Server.CreateObject("PryUSAT.clsAccesoDatos")
				objPresupuesto.AbrirConexion
					Set rsPresupuesto= Server.CreateObject("ADODB.RecordSet")
					set rsPresupuesto= objPresupuesto.consultar("CompararPresupuestadoEjecutado","FO", strNivel, strAno_Pto,lngCodigo_Cco)
				objPresupuesto.CerrarConexion
	
				if rsPresupuesto.recordcount >0 then
						contar = 0
						totalPre = 0
						totalEje = 0

						%>
						 <table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="117">
						 	<tr>
						 		<td colspan=7 width="914" height="14"><%response.write "Area: " & rsPresupuesto("descripciónAre") & " - " & strAno_Pto%> </td>	
						 	</tr>
							<tr>
								<td width="39" class="etabla" rowspan="2" height="49">ITEM</td>
								<td width="652" class="etabla" rowspan="2" height="49">DETALLE DE ARTICULO</td>
								<td width="202" class="etabla" colspan="2" height="14">
                                PRESUPUESTADO</td>
								<td width="178" class="etabla" colspan="2" height="14">
                                EJECUTADO</td>
								<td width="112" class="etabla" rowspan="2" height="49">
                                DIFERENCIA</td>
							 </tr>
							
							<tr>
								<td width="90" class="etabla" height="28">CANT. TOTAL</td>
								<td width="112" class="etabla" height="28">TOTAL PRESUPUEST. 
                                (S/.)</td>
								<td width="78" class="etabla" height="28">CANT. TOTAL</td>
								<td width="100" class="etabla" height="28">TOTAL EJECUTADO 
                                (S/.)</td>
							 </tr>
							
							<%DO WHILE NOT rsPresupuesto.EOF
								contar = contar +1
								if cdbl(rsPresupuesto("diferencia")) < 0 then
									tipo ="negativo"
									tipo2="negativoMonto"
								else
									tipo =""
									tipo2=""
								end if
								
								%>
								
								<TR onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)" style="cursor:hand">	
									<td width="39" align="center" height="13" class="<%=tipo%>"><%=contar%></td>
									<td width="652" align="left" height="13" class="<%=tipo%>"><%=trim(rsPresupuesto("descripcion_Art"))%></td>
									<td width="90" align="center" height="13" class="<%=tipo%>"><%=trim(rsPresupuesto("cantPre"))%></td>
									<td width="112" align="center" height="13" class="<%=tipo%>"><%=formatNumber(trim(rsPresupuesto("totalPre")),4)%></td>
									<td width="78" align="center" height="13" class="<%=tipo%>"><%=trim(rsPresupuesto("cantEje"))%></td>
									<td width="112" align="center" height="13" class="<%=tipo%>"><%=formatNumber(trim(rsPresupuesto("totalEje")),4)%></td>
									<td width="100" align="right" height="13" class="<%=tipo2%>"><%=formatNumber(trim(rsPresupuesto("diferencia")),4)%></td>
								</TR>
							
							  <% totalPre = totalPre + cdbl(rsPresupuesto("totalPre"))
							     totalEje = totalEje + cdbl(rsPresupuesto("totalEje"))

							  	rsPresupuesto.MOVENEXT
							LOOP
							
								if (totalPre - totalEje) < 0 then
									tipo ="negativoTotal"
								else
									tipo ="positivoTotal"
								end if

							%>
								<tr>	 
									<td colspan=2 width="698" height="14"> </td>
									<td class="etabla" width="90" height="14">TOTAL (S/. ) :</td>
									<td class="etabla" width="112" height="14"><%=formatNumber(totalPre,4)%>
                                    </td>
									<td class="etabla" width="78" height="14">TOTAL (S/.)</td>
									<td class="etabla" width="100" height="14"><%=formatNumber(totalEje,4)%> </td>
									<td class="<%=tipo%>" width="112" height="14" align="right">
                                    <%=formatNumber((totalPre - totalEje),4)%></td>
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