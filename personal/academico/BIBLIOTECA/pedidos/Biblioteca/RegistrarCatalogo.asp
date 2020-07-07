<!--#include file="../../../../../funciones.asp"-->
<%

Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsProveedor=Obj.Consultar("PED_ConsultarEmpresaProveedora","FO","TO",0)

	Obj.CerrarConexion
Set obj=nothing


%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Language" content="es" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Registrar nueva bibliografía</title>
<link rel="stylesheet" type="text/css" href="../../../../../private/estilo.css" />
<script type="text/javascript" language="JavaScript" src="../../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../private/validarpedido.js"></script>
<script>
function LlenarListaFechas(idproveedor){
	location.href="RegistrarCatalogo.asp?idproveedor=" + idproveedor
}

function LlenarListaDelCatalogo(fecha,idproveedor){
	location.href="RegistrarCatalogo.asp?idproveedor=" + idproveedor + "&fecha=" + fecha
//alert (fecha)
}


</script>
<style type="text/css">
<!--
.Estilo1 {
	font-size: 10pt;
	font-weight: bold;
}
.Estilo3 {color: #000000}
.Estilo5 {color: #000000; font-weight: bold; }
-->
</style>
</head>

<body style="background-color: #F0F0F0">
<%
idproveedor = Request.QueryString("idproveedor")
fecha = Request.QueryString("fecha")
%>
<form name="frmcatalogo" method="post" action="procesar.asp?accion=agregarnvocatalogo&codigo_cco=<%=codigo_cco%>">
<p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Seleccione un proveedor, luego la fecha en la que se registr&oacute; el cat&aacute;lgo de dicho proveedor a fin de visualizalo.  </p>
<table width="100%" class="contornotabla"  bgcolor="white">
	<tr>
		<td width="83">Proveedor</td>
	  <td><%call llenarlista("cboProveedor_cat","LlenarListaFechas(this.value)",rsProveedor,"Idproveedor","NombreProveedor",Idproveedor,"Seleccione el proveedor","","")%>		</td>
	</tr>
<%
if idproveedor<>"" then


Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsfecha=Obj.Consultar("PED_ConsultarCatalogosAnteriores","FO","FE",idproveedor,"")
	Obj.CerrarConexion
Set obj=nothing
%>
	<tr>
	  <td>Fecha</td>  
	  <td><%call llenarlista("cboFecha_cat","LlenarListaDelCatalogo(this.value,"+idproveedor+")",rsfecha,"fechaCat","fechaCat",fecha,"Seleccione la fecha de envío del catálogo","","")%></td>
    </tr>
<%
end if
%>
	<tr>
		<td colspan="2" id="lblcontador" class="rojo" align="right">&nbsp;</td>
	</tr>
</table>

<% if  fecha<>"" then

Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsCatalogo=Obj.Consultar("PED_ConsultarCatalogosAnteriores","FO","CF",0,fecha)
	Obj.CerrarConexion
Set obj=nothing

%>
<p class="Estilo1">Detalle del Cat&aacute;logo:</p>
<table border="0" cellpadding="4" cellspacing="0" class="contornotabla">
    <tr>
      <td width="31" align="center" bgcolor="#FFFF99" class="bordeinf"><span class="Estilo3"><strong>N&ordm;</strong></span></td>
      <td width="226" height="15" align="center" bgcolor="#FFFF99" class="bordeinf"><span class="Estilo5">T&iacute;tulo</span></td>
      <td width="185" align="center" bgcolor="#FFFF99" class="bordeinf"><span class="Estilo5">Autor</span></td>
    <td width="100" align="center" bgcolor="#FFFF99" class="bordeinf"><span class="Estilo5">Editorial</span></td>
    <td width="56" align="center" bgcolor="#FFFF99" class="bordeinf"><span class="Estilo5">Pa&iacute;s</span></td>
    <td width="42" align="center" bgcolor="#FFFF99" class="bordeinf"><span class="Estilo5">Edicion</span></td>
    <td width="78" align="center" bgcolor="#FFFF99" class="bordeinf"><span class="Estilo5">ISBN</span></td>
    <td width="46" align="center" bgcolor="#FFFF99" class="bordeinf"><span class="Estilo5">Moneda</span></td>
    <td width="50" align="center" bgcolor="#FFFF99" class="bordeinf"><span class="Estilo5">Precio Unitario </span></td>
    <td width="59" align="center" bgcolor="#FFFF99" class="bordeinf"><span class="Estilo5">Precio Total</span></td>
    <td width="52" align="center" bgcolor="#FFFF99" class="bordeinf"><span class="Estilo5">Cantidad</span></td>
    <td width="93" align="center" bgcolor="#FFFF99" class="bordeinf"><span class="Estilo5">Materia</span></td>
<%
rsCatalogo.MoveFirst
i=0
do while not rsCatalogo.EOF
i=i+1
%>
  <tr class="bordederinf">
    <td height="15" align="left" valign="top" bgcolor="#FFFFFF" class="bordeinf"><span class="Estilo3"><%=i%></span></td>
    <td align="left" valign="top" bgcolor="#FFFFFF" class="bordeinf"><span class="Estilo3"><%
if rsCatalogo("Titulo_cat")="" then
Response.Write (" - ")
else
Response.Write (rsCatalogo("Titulo_cat"))
end if
%></span></td>
    <td align="left" valign="top" bgcolor="#FFFFFF" class="bordeinf"><span class="Estilo3">
    <%
	if rsCatalogo("Autor_cat")="" then
		Response.Write (" - ")
	else
		Response.Write (rsCatalogo("Autor_cat"))
	end if
	%>
    </span></td>
    <td align="left" valign="top" bgcolor="#FFFFFF" class="bordeinf"><span class="Estilo3">
    <%
	if rsCatalogo("Editorial_cat")="" then
		Response.Write (" - ")
	else
		Response.Write (rsCatalogo("Editorial_cat"))
	end if
	%>
    </span></td>
    <td align="left" valign="top" bgcolor="#FFFFFF" class="bordeinf"><span class="Estilo3">
    <%
	if rsCatalogo("Pais_cat")="" then
		Response.Write (" - ")
	else
		Response.Write (rsCatalogo("Pais_cat"))
	end if
	%>	</span>
	</td>
    <td align="left" valign="top" bgcolor="#FFFFFF" class="bordeinf"><span class="Estilo3">
	<%
	if rsCatalogo("Edicion_cat")="" then
		Response.Write (" - ")
	else
		Response.Write (rsCatalogo("Edicion_cat"))
	end if
	%>	
	</span></td>
    <td align="left" valign="top" bgcolor="#FFFFFF" class="bordeinf"><span class="Estilo3">

	<%
	if trim(rsCatalogo("ISBN_cat"))="" or rsCatalogo("ISBN_cat") = null then
		Response.Write (" - ")
	else
		Response.Write (rsCatalogo("ISBN_cat"))
	end if
	%>		
	</span></td>
    <td align="left" valign="top" bgcolor="#FFFFFF" class="bordeinf"><span class="Estilo3"><%=rsCatalogo("Moneda_cat")%></span></td>
    <td align="left" valign="top" bgcolor="#FFFFFF" class="bordeinf"><span class="Estilo3"><%=rsCatalogo("Preciounit_cat")%></span></td>
    <td align="left" valign="top" bgcolor="#FFFFFF" class="bordeinf"><span class="Estilo3"><%=rsCatalogo("PrecioTotal_cat")%></span></td>
    <td align="left" valign="top" bgcolor="#FFFFFF" class="bordeinf"><span class="Estilo3"><%=rsCatalogo("Cant_cat")%></span></td>
    <td align="left" valign="top" bgcolor="#FFFFFF" class="bordeinf"><span class="Estilo3"><%=rsCatalogo("Materia_cat")%></span></td>	
								


<%
rsCatalogo.MoveNext
loop
%>
  </table>
  <%end if%>
<p>&nbsp;</p>
</form>
</body>
</html>
