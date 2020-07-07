<!--#include file="clsrecepcion.asp"-->
<%
tabla=request.querystring("tabla")
modo=request.QueryString("modo")
iddestino=request.QueryString("iddestino")
idorigen=request.QueryString("idorigen")

if iddestino<>"" then idtabla=iddestino
if idorigen<>"" then idtabla=idorigen

If modo="modificarorigen" then
	Dim archivo
	set archivo=new clsrecepcion
		ArrDatos=archivo.ConsultarDestinatario("5",idtabla)
	set archivo=nothing
		If IsEmpty(ArrDatos)=false then
			'Tipo=ArrDatos(2,0)
			nombre=ArrDatos(1,0)
		End If
End If
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registrar Origen de envio</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="private/validararchivo.js"></script>
</head>

<body>
<form name="frmdestinatario" Method="POST" onSubmit="return validardestinatario()" ACTION="procesaratributos.asp?modo=<%=modo%>&tabla=<%=tabla%>&idtabla=<%=idtabla%>">
<fieldset style="padding: 2; width:90%">
<legend class="e1">Registro de áreas</legend>
<table border="0" class="Listas" cellpadding="2" cellspacing="0" style="border-collapse: collapse" width="100%" height="81">
  <tr>
    <td width="91" height="28">Nombre</td>
    <td width="85%" height="28">
    <input type="text" name="nombre" size="61" class="cajas" value="<%=nombre%>" style="width: 90%"></td>
  </tr>
  <tr>
    <td width="91" height="34">&nbsp;</td>
    <td width="85%" height="34">
    <input type="submit" class="guardar" value="Guardar">
    <input type="button" class="salir" onclick="history.back()" value="Cancelar" name="salir"></td>
  </tr>
</table>
</form>
</fieldset>
</body>
</html>