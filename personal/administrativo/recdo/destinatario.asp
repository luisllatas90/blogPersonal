<!--#include file="clsrecepcion.asp"-->
<%
tabla=request.querystring("tabla")
modo=request.QueryString("modo")
iddestinatario=request.QueryString("iddestinatario")

If modo="modificardestinatario" then
	Dim archivo
	set archivo=new clsrecepcion
		ArrDatos=archivo.ConsultarDestinatario("2",iddestinatario)
	set archivo=nothing
		If IsEmpty(ArrDatos)=false then
			Tipo=ArrDatos(2,0)
			nombre=ArrDatos(1,0)
		End If
End If
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registrar Destinatarios para el documento</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="private/validararchivo.js"></script>
</head>

<body>
<form name="frmdestinatario" Method="POST" onSubmit="return validardestinatario()" ACTION="procesaratributos.asp?modo=<%=modo%>&tabla=<%=tabla%>&iddestinatario=<%=iddestinatario%>">
<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Recomendaciones de registro 
según el tipo de destinatario:<br>
- Personal de la Universidad: Ej. Pérez López, Jorge (evitar agregar títulos 
como Mgstr, Dr., etc.) y el orden del registro es [Apellidos] , [Nombres]</p>
<fieldset style="padding: 2; width:90%">
<legend class="e1">Registrar destinatario del documento</legend>
<table border="0" class="Listas" cellpadding="2" cellspacing="0" style="border-collapse: collapse" width="100%" height="81">
  <tr>
    <td width="91" height="1">Tipo</td>
    <td width="85%" height="1">
    <input type="radio" value="0" <%if tipo=0 then response.write "checked" end if%> name="tipodestinatario" checked>Personal
    <input type="radio" value="1" <%if tipo=1 then response.write "checked" end if%> name="tipodestinatario">Área</td>
  </tr>
  <tr>
    <td width="91" height="28">Destinatario</td>
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