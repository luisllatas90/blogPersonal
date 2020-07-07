<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<html>
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
	<link rel="stylesheet" type="text/css" href="../private/estilo.css">
	<script language="JavaScript" src="../private/funciones.js"></script>
	<script language="JavaScript">
		function validaSubmite(){ 
		    if (document.frmConceptoIngresoEgreso.txtDesCon.value == "") 
			{
       		alert("Debe Ingresar la Descripcion del Concepto") 
				frmConceptoIngresoEgreso.txtDesCon.focus();
				return (false);	

   			}


		    if (document.frmConceptoIngresoEgreso.cboTipCon.value == "0") 
			{
       		alert("Debe Seleccionar el Tipo de Concepto ") 
				frmConceptoIngresoEgreso.cboTipCon.focus();
				return (false);	

		   }
   
		    if (document.frmConceptoIngresoEgreso.cboConGen.value == "0") 
			{
       		alert("Debe Seleccionar el Concepto General") 
				frmConceptoIngresoEgreso.cboConGen.focus();
				return (false);	

		   }
   
   	      	return (true);	

		} 

    </script>




</head>
<body>


<!-- Titulo de la Pagina-->
<table align="center" width="80%">
  <tr>
      <th align="center" class="table">REGISTRAR CONCEPTO DE INGRESO / EGRESO</th>
  </tr>
  <tr>
	<td><hr></td>
  </tr>
</table>
<!-- Fin de Titulo de la Pagina -->
<P>

<form  onSubmit="return validaSubmite()" method="post" action="Procesos.asp?Tipo=I005" id="frmConceptoIngresoEgreso" name="frmConceptoIngresoEgreso">
  <table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="80%">
  
	<tr>
		<td class="etabla">Descripcion del Concepto:</td>
		<td><input Type"text" name="txtDesCon" size="50"></td>
		
	</tr>
    <tr>
		<td class="etabla">Tipo de Concepto:</td>
		<td><select name="cboTipCon" id="cboTipCon">
			 <option value="0">--- Seleccione Tipo ---</option>
			 <option value="I">Ingreso</option>
		     <option value="E">Egreso</option>
		     </select>
		</td>
	</tr>
	<tr>
		<td class="etabla">Concepto General:</td>
		
		<%
			Dim objConGen
			Dim rsConGen
			Set objConGen=Server.CreateObject("PryUSAT.clsDatPresupuesto")
			Set rsConGen=Server.CreateObject("ADODB.Recordset")
			Set rsConGen= objConGen.ConsultarConceptoGeneral("RS","VA","")
		
		%>
		
		<td> <select name="cboConGen" id="cboConGen">
          <option value="0">---Seleccione Concepto General--- </option>
          <% do while not rsConGen.eof %>
		  	
          <option value="<%=rsConGen(0)%>"> <%=rsConGen("descripcion_Cge")%></option>
          <% rsConGen.movenext
			loop
			rsConGen.Close
			Set rsConGen=Nothing 
			set objConGen= Nothing
		   %>
        </select> </td>
		
	</tr>

</table>
<br>
<table align="center" width="80%">
<tr>
  <td colspan="2" align="center">
    <input type="submit" value="Grabar" name="cmdGrabar" class="guardar">
    <input type="button" value="Cancelar" name="cmdCancelar" class="salir" onClick=location="frmCuentaConcepto.asp" >
 
  </td>
</tr>
</table>
</form>
</body>
</html>