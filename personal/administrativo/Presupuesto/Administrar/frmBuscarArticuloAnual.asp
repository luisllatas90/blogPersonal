<html>
<head>
	<meta http-equiv="Content-Language" content="es-mx">
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
	<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
	<meta name="ProgId" content="FrontPage.Editor.Document">
	<title>Buscar Concepto</title>
	<link rel="stylesheet" type="text/css" href="../private/estilo.css">
	<script language="JavaScript" src="../private/funciones.js"></script>
</head>

<body>

<table width="90%" align="center">
	<tr align="center">
		<!--<td><img src="../images/busartconce.jpg"></td>-->
      <th align="center" class="table">BUSCAR ARTICULO - CONCEPTO</th>
    </tr>
    <tr>
		<td><hr></td>
    </tr>

</table>
<%
Dim lngCodigo_Pto
Dim objArticulo
Dim rsArticulo
Dim strParametro

Dim strTipo_Pto

strTipo_Pto= request.form("txtTipo_Pto")

if strTipo_Pto="" then
	strTipo_Pto= request.querystring("tpto")
end if

'Response.Write("tipo pto=")
'Response.Write(strTipo_Pto)


lngCodigo_Pto=request.form("txtCodigo_Pto")
if lngCodigo_Pto="" then
   lngCodigo_Pto=request.querystring("cp")
end if

strParametro=request.form("txtParametro")
%>

<form action="frmbuscararticuloAnual.asp" method="post" name="frmbuscararticulo" id="frmbuscararticulo">
<input type="hidden" name="txtCodigo_Pto" value=<%=lngCodigo_Pto%>>
<input type="hidden" name="txtTipo_Pto" value=<%=strTipo_Pto%>>
<table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="90%">
  <tr>
    <td width="10%" class="etabla">Concepto:</td>
    <td width="35%">
        <p><input type="text" name="txtParametro" size="50">
    </td>
    <td width="5%" align="center"><input type="submit" value=" Buscar " name="cmdBuscar" class="boton"></td>
  </tr>
</table>
</Form>
<%
if strParametro<>"" then

Dim lngCodigo_Art
Dim blnTipo_Art
Dim strUnidad_Art
Dim dblPrecioUnit_Art
Dim strDescripcion_Art
Dim strCuenta_Art

Set objArticulo= Server.CreateObject("PryUSAT.clsDatArticuloConcepto")
Set rsArticulo= Server.CreateObject("ADODB.RecordSet")


Set rsArticulo= objArticulo.ConsultarArticuloConcepto("RS","DE",strParametro,strTipo_pto)

if rsArticulo.recordcount >0 then
%>	
<table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="90%">
      <tr class="etabla">
      		<!--<td width="2%">&nbsp;</td>-->
			<td width="38%">Descripcion</td>
			<td width="6%">Unidad</td>
			<td width="9%" align="right">Precio Unit.</td>
			<td width="20%" align="left">Categoria</td>
      </tr>
	
	<%
    	do while not rsArticulo.eof
        	lngCodigo_Art=rsArticulo("codigo_Art")
			blnTipo_Art = rsArticulo("funcion_Art")
			strDescricpion_Art= rsArticulo("descripcion_Art")
	 		strCuenta_Art=rsArticulo("descripcionCuenta_Pco")
	 		
	 		if isNull(rsArticulo("descripcion_Uni")) then
				strUnidad_Art="-"
			else	 	
				strUnidad_Art= rsArticulo("descripcion_Uni")
			end if
		
			if isNull(rsArticulo("precioUnitario_Art")) then
				dblPrecioUnit_Art	="0.00"	
			else		
			   dblPrecioUnit_Art= rsArticulo("precioUnitario_Art")
			end if
	
	%>
			
			<a href="frmregistrardetallepresupuestoAnual.asp?ca=<%=lngCodigo_Art%>&cp=<%=lngCodigo_Pto%>&da=<%=strDescricpion_Art%>&pa=<%=dblPrecioUnit_Art%>&ua=<%=strUnidad_Art%>&ct=<%=strCuenta_Art%>&ta=<%=blnTipo_Art%>">
    	 	<tr onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)" style="cursor:hand">
 	      		<!--<td width="3%"><img src="../images/nuevo.gif" width="14" height="14"></td>-->
      	 		<td width="15%"><%=strDescricpion_Art%>&nbsp;</td>
				<td width="5%"><%=strUnidad_Art%>&nbsp;</td>
				<td width="5%" align="right"><%=formatNumber(dblPrecioUnit_Art)%>&nbsp;</td>
				<td width="25%" align="left"><%=strCuenta_Art%>&nbsp;</td>
			 </tr>
			</a> 
		<%					
	    rsArticulo.movenext
    	loop%>
	</table>
<%	
else
	%>
	<script language="JavaScript">
		alert("No se encontro ningun registro con la descripcion ingresada") 
		frmbuscararticulo.txtParametro.focus();
	</script>
<%
end if
rsArticulo.close
Set rsArticulo= Nothing
Set objArticulo= Nothing

end if
%>

</body>