<!--#include file="../../../../../funciones.asp"-->
<%
idproveedor =request.querystring("idproveedor")
IdOperador=5
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
<style type="text/css">
<!--
.Estilo1 {font-size: 12pt}
-->
</style>
</head>

<body style="background-color: #F0F0F0">

<form name="frmcatalogo" method="post" action="procesar.asp?accion=agregarnvocatalogo&codigo_cco=<%=codigo_cco%>">
  <div align="center" class="Estilo1">
    <%
IdProveedor=Request.QueryString("IdProveedor")

Set ConexionBD = Server.CreateObject("ADODB.Connection") 
ConexionBD.Open "DRIVER={Microsoft Excel Driver (*.xls)};DBQ=D:/wwwroot/campusvirtual/catalogobiblioteca/catalogo.xls"  
Set rsVac = Server.CreateObject("ADODB.Recordset") 
rsVac.ActiveConnection = ConexionBD 

rsVac.CursorType = 2 
rsvac.LockType = 2 
rsVac.Source = "SELECT Titulo_cat,Autor_cat,Editorial_cat,Pais_cat,Edicion_cat,ISBN_cat,Moneda_cat,Preciounit_cat,PrecioTotal_cat,Cant_cat,Materia_cat FROM [hoja1$A1:L2000]" 

rsVac.Open 
rsVac.MoveFirst 

	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			obj.ejecutar "PED_ActualizarEstadoCatalogos",false,idproveedor
		Obj.CerrarConexion
	Set obj=nothing
			
do while not rsVac.eof
	i=i+1
	''	Response.Write("ID_PROV: " & rsVac("Titulo_cat")&"<BR>")

	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
				Obj.Ejecutar "PED_RegistrarCatalogo",false,idproveedor,rsVac("Titulo_cat"),rsVac("Autor_cat"),rsVac("Editorial_cat"),rsVac("Pais_cat"),rsVac("Edicion_cat"),rsVac("ISBN_cat"),rsVac("Moneda_cat"),rsVac("Preciounit_cat"),rsVac("PrecioTotal_cat"),rsVac("Cant_cat"),rsVac("Materia_cat"),IdOperador
		Obj.CerrarConexion
	Set obj=nothing



	rsVac.MoveNext
loop
rsVac.close
set rsVac= Nothing
ConexionBD.Close
SET ConexionBD = Nothing
Response.Write("Han sido guardados " & i & " registros en el catálogo." )
%>
  </div>
</form>
</body>
</html>
