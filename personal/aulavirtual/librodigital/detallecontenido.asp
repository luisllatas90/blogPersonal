<!--#include file="clslibrodigital.asp"-->
<%
Dim web

titulolibro=request.querystring("titulolibro")
modo=request.querystring("modo")
idcontenido=Request.querystring("idcontenido")
idlibrodigital=Request.querystring("idlibrodigital")

Dim contenido
	set contenido=new clslibrodigital
		with contenido
			.restringir=session("idcursovirtual")
			ArrDatos=.Consultar("5",idcontenido,"","")
		end with
		if isEmpty(ArrDatos)=true then
			response.write "<script>alert('No se ha registrado contenido temático en el índice seleccionado');history.back(-1)</script>"
		else
			ordencontenido=ArrDatos(3,0)
			titulocontenido=ArrDatos(4,0)
			descripcioncontenido=ArrDatos(5,0)
		end if
	Set contenido=nothing
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Menú de acciones</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<style fprolloverstyle>A:hover {color: #FF0000; text-decoration: underline; font-weight: bold}
</style>
<style>
<!--
a:link       { color: #0000FF; text-decoration: underline }
-->
</style>
</head>
<body>
<table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla" bgcolor="#EEEEEE">
  <tr>
    <td width="31%" align="center" class="e1">
    Menú de acciones</td>
    <td width="11%" align="center">
    <img border="0" src="../../../images/back.gif" align="absbottom">Anterior</td>
    <td width="11%" align="center">
    Siguiente
    <img border="0" src="../../../images/forward.gif" align="absbottom"></td>
    <td width="11%" align="center">
    <a href="listaindice.asp?idlibrodigital=<%=idlibrodigital%>&modo=<%=modo%>&titulolibro=<%=titulolibro%>">
    <img border="0" src="../../../images/html.gif">
    Contenidos</a></td>
    <td width="11%" align="center">
    <a href="Javascript:history.back(-1)">
    <img border="0" src="../../../images/salir.gif">
    Regresar</a></td>
    <%if session("codigo_usu")="USAT\gchunga" then%>
    <td width="11%" align="center" bgcolor="#FFFDD2">
        <img border="0" src="../../../images/propiedades.gif" style="align: absbottom; border: 0px none">
    Anotaciones</td>
    <%end if%>
  </tr>
</table>
<p class="e4"><%=ordencontenido & ".&nbsp;" & titulocontenido%>&nbsp;</p>
<DIV id="listadiv" style="height:82%;">
<p><%=descripcioncontenido%></p>
</DIV>
</body>
</html>