<html>

<head>
	<meta http-equiv="Content-Language" content="es-mx">
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
	<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
	<meta name="ProgId" content="FrontPage.Editor.Document">
	<title>Buscar Concepto</title>
	<link rel="stylesheet" type="text/css" href="../private/estilo.css">
	<script language="JavaScript" src="../private/funciones.js"></script>
	<script language="JavaScript" >
		function Volver(cp){
			location.href="frmconsolidadopresupuesto.asp?id=" + cp
		}
	</script>
</head>

<body>


<%

Dim lngCodigo_Pto
Dim strTipo_pto
Dim objArticulo
Dim rsArticulo
Dim strParametro

lngCodigo_Pto=request.form("txtCodigo_Pto")
strTipo_pto=request.form("txtTipo_Pto")

if lngCodigo_Pto="" then
   lngCodigo_Pto=request.querystring("cp")
end if

if strTipo_Pto="" then
	strTipo_Pto=request.querystring("tpto")
end if

strParametro=request.form("txtParametro")

%>

<table width="90%" align="center">
	<tr align="center">
		<!--<td><img src="../images/busartconce.jpg"></td>-->
	   <% if strTipo_Pto="E" then %>	
	      <th align="center" class="table">BUSCAR ARTICULO - CONCEPTO</th>
	   <%else%>   
	      <th align="center" class="table">BUSCAR SERVICIO - CONCEPTO</th>

	   <% end if%>
    </tr>
    <tr>
		<td><hr></td>
    </tr>

</table>

<form action="frmbuscararticuloMenu.asp" method="post" name="frmbuscararticuloMenu" id="frmbuscararticuloMenu">

<input type="hidden" name="txtCodigo_Pto" value=<%=lngCodigo_Pto%>>
<input type="hidden" name="txtTipo_Pto" value=<%=strTipo_Pto%>>

<table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="90%">
  <tr>
    <td width="10%" class="etabla">Concepto:</td>
    <td width="20%">
        <p><input type="text" name="txtParametro" value="<%=strParametro%>" size="50">
    </td>
    <td width="5%" align="center"><input type="submit" value=" Buscar " name="cmdBuscar" class="boton" title="Consultar Articulo-Concepto"></td>
    <td>
	<input type="button" value="Cancelar" onclick="Volver('<%=lngCodigo_Pto%>')" class="salir">
	 

    </td>

	
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

strParametro = Trim(Replace(strParametro, " ", "%"))


Set rsArticulo= objArticulo.ConsultarArticuloConcepto("RS","DE",strParametro,strTipo_pto)

if rsArticulo.recordcount >0 then
%>	

<table  align="center" id="tblelegidos" border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="90%">
<!--<table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="90%">-->
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
	 		strCuenta_Art= trim(rsArticulo("descripcionCuenta_Pco"))
	 		
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
	    		<tr onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)" style="cursor:hand" class="table">
      	 			<td width="10%"><%=strDescricpion_Art%>&nbsp;</td>
					<td width="5%"><%=strUnidad_Art%>&nbsp;</td>
					<td width="5%" align="right"><%=formatNumber(dblPrecioUnit_Art)%>&nbsp;</td>
					<td width="35%" align="left"><%=strCuenta_Art %>&nbsp;</td>
			</tr>
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