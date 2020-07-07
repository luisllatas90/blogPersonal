<!--#include file="../funcionesaulavirtual.asp"-->
<%
Dim nombrearchivoavance,Logeo

if session("codigo_usu")="" then response.redirect "../tiempofinalizado.asp"

idtareaevaluacion=request.querystring("idtareaevaluacion")
idevaluacionindicador=request.querystring("idevaluacionindicador")
autorizado=request.querystring("autorizado")
maxpje=request.querystring("maxpje")
idvariable=request.querystring("idvariable")
idseccion=request.querystring("idseccion")

Logeo=session("codigo_usu")
FechaG=Replace(now(),"/",""):FechaG=Replace(FechaG,":",""):FechaG=Replace(FechaG," ","")
FechaG=Replace(FechaG,"A.M.",""):FechaG=Replace(FechaG,"P.M.","")
FechaG=Replace(FechaG,"AM",""):FechaG=Replace(FechaG,"PM","")
nombrearchivoavance= Right(Logeo, Len(Logeo) - 5) & trim(FechaG)

%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registrar documento</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="private/validaracreditacion.js"></script>
</head>
<body>
<form name="frmavance" enctype="multipart/form-data" method="post" onSubmit="return validaravance(this,'<%=maxpje%>')" action="agregar.asp?idtareaevaluacion=<%=idtareaevaluacion%>&autorizado=<%=autorizado%>&nombrearchivoavance=<%=nombrearchivoavance%>&idevaluacionindicador=<%=idevaluacionindicador%>&idvariable=<%=idvariable%>&idseccion=<%=idseccion%>">
<input type="hidden" name="doCreate" value="true">
<fieldset style="padding: 2">
  <legend class="e1">Paso 2: Elegir documento</legend>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
    <tr>
      <td width="135">Descripción del archivo</td>
      <td width="549">
    <input  maxLength="100" size="74" name="tituloarchivoavance" class="Cajas" style="width: 100%"></td>
    </tr>
    <tr>
      <td width="135" class="azul">Buscar ubicación del archivo</td>
      <td width="549">
      <input class="cajas" type="File" name="file" size="50" style="height:20"></td>
    </tr>
    <tr>
      <td width="135">
    <input type="submit" value="Atras" name="cmdAtras" class="atras"></td>
      <td width="549">
    &nbsp;<input type="submit" value="Guardar" name="cmdGuardar" class="guardar"> <input OnClick="window.close()" type="button" value="Cancelar" name="cmdCancelar" class="salir"></td>
    </tr>
  </table>
</fieldset>
</form>
</body>
</html>