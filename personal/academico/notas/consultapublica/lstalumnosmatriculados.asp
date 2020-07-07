<!--#include file="../administrarconsultar/clsnotas.asp"-->
<%
Dim activarnotas
codigo_cac=request.querystring("codigo_cac")
codigo_cup=request.querystring("refcodigo_cup")

if codigo_cup<>"" then

	Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
			Set rsAlumnos=Obj.Consultar("ConsultarAlumnosMatriculados","FO","RN",codigo_cup,"","")
		obj.CerrarConexion
	Set obj=nothing
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registro de notas</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estiloimpresion.css">
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../private/validarnotas.js"></script>
<style>
<!--
input        { font-family: Verdana; font-size: 8.5pt;width:70% }
-->
</style>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
</head>
<body topmargin="0" leftmargin="0">
<table width="100%" border="1" cellpadding="3" cellspacing="0" bordercolor="#808080" style="border-collapse: collapse">
	<tr class="usatCeldaTabActivo">
	<td align="center" height="5%"  width="5%">
	Nº</td>
	<td align="center" height="5%" style="width: 16%">
	Escuela Profesional</td>
	<td align="center" height="5%"  width="10%">
	Código</td>
	<td align="center" height="5%" style="width: 17%">
	Apellidos y Nombres</td>
	<td align="center" height="5%"  width="4%">
	Nota final</td>
	<td align="center" height="5%"  width="15%">
	Condición</td>
	</tr>
	<%
	Do while not rsAlumnos.eof
		i=i+1
	%>
		<tr onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)">
		<td width="5%" align="center"><%=i%>&nbsp;</td>
		<td width="30%"> <span style="font-size: 7pt"><%=rsAlumnos("nombre_cpf")%></span>&nbsp;</td>
		<td align="center" width="12%"><%=rsAlumnos("codigoUniver_Alu")%></td>
		<td align="left" width="35%"><%=rsAlumnos("alumno")%>&nbsp;</td>
		<td align="center" width="10%"><%=rsAlumnos("notafinal_dma")%></td>
		<td align="center" width="5%"><%=rsAlumnos("condicion_dma")%></td>
		</tr>
		<%rsAlumnos.movenext
	Loop
	rsAlumnos.close
	set rsAlumnos=Nothing
	%>
</table>
</body>
</html>
<%end if%>