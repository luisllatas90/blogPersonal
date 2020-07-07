<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<html>
<head>

	<script language="JavaScript">

		function validaSubmite(ano){ 
		
		
		    if (document.frmBuscarPrespuesto.txtAno_Pto.value == "") 
			{
		       alert("Debe Ingresar el año del Presupuesto") 
				frmBuscarPrespuesto.txtAno_Pto.focus();
				return (false);	

		   }	

   	   	    if (document.frmBuscarPrespuesto.txtAno_Pto.value >= ano ) 
			{
		       alert("El año Ingresado debe ser menor que el del Presupuesto Actual") 
			
				return (false);	

		   }	

   	   	
   	   	
   	   		   	return (true);	

		} 

	</script>
	
	<title>Seleccionar Presupuesto</title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
	<link rel="stylesheet" type="text/css" href="../private/estilo.css">
	<script language="JavaScript" src="../private/funciones.js"></script>
</head>

<body>
<%

Dim lngCodigo_PtoN
Dim strAno_PtoN
Dim lngCodigo_Cco
Dim strTipo_Pto
Dim strMsg

lngCodigo_PtoN=request.querystring("cpton")
strAno_PtoN=request.querystring("anon")
lngCodigo_Cco=request.querystring("cco")
strTipo_Pto=request.querystring("tip")
strMsg=request.querystring("men")

'response.write(strMsg)
'response.write(lngCodigo_PtoN)
'response.write("<br>")
'response.write(strAno_PtoN)
'response.write("<br>")

'response.write(lngCodigo_Cco)
'response.write("<br>")

'response.write(strTipo_Pto)


%>
<table align="center" border="0" width="100%">
    <tr> 
		<th align="center" class="table">IMPORTAR PRESUPUESTO - CENTRO DE COSTOS</th>
    </tr>
    <tr>
		<td><hr></td>
    </tr>

</table>

<form onSubmit="return validaSubmite(<%=cdbl(strAno_PtoN)%>)" action="Procesos.asp?Tipo=I003" method="post" name="frmBuscarPrespuesto" id="frmBuscarPrespuesto" >

<input type="hidden" name="txtCodigo_PtoN" value=<%=lngCodigo_PtoN%> >
<input type="hidden" name="txtAno_PtoN" value=<%=strAno_PtoN%> >
<input type="hidden" name="txtCodigo_Cco" value=<%=lngCodigo_Cco%> >
<input type="hidden" name="txtTipo_Pto" value=<%=strTipo_Pto%> >


<table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
	<tr>
		<td class="etabla" width="50%">Presupuesto del Año:</td>
		<td align="center" width="10%"><input id=txtAno_Pto maxLength=4   name=txtAno_Pto size=4 onKeyPress="validarnumero()"></td>

		<td align="center"><Input Type="Submit" value="     Importar  " class="nuevo"> </td>
		<!--<td align="center"><Input Type="Button" value="Cancelar" class="salir" onClick=location="frmconsolidadopresupuesto.asp?id=<%=lngCodigo_PtoN%>"> </td>-->
		<td align="center"><Input Type="Button" value="Cancelar" class="salir" onClick="window.close()">
	</tr>	

</table>
<p>

<table align="center">
	<tr>
		<td colspan="4" align="center"><font color="#FF0000"><b><font size="2">
        Importante:</font></b></font><font size="2"> Los datos del Presupuesto que esta elaborando, 
        Serán Borrados y reemplazados por los de este.</font></td>
	</tr>
</table>

</form>
<br>
<%if trim(strMsg)="0" then%>
<table width="80%" align="center">
	<tr>
		<td colspan="4" align="center"> <font color="#FF0000"><b>Resultado:</b> No se 
        Encontró Ningún Presupuesto Para El Año Ingresado.</font></td>
	</tr>
</table>
<%end if%>


</body>
</html>