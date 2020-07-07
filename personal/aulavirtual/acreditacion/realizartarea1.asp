<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Desarrollar tarea solicitada</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="private/validaracreditacion.js"></script>
</head>
<body>
<fieldset style="padding: 2; width:100%">
<legend class="e1">Paso 1: El documento que se le solicita lo tiene:</legend>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr id="etiqueta">
    <td colspan="3" width="689" height="14">
    <input type="radio" onClick="mensajetipoavance('0')" value="completo" name="tipoavance">Completo</td>
  </tr>
  <tr id="fila">
    <td width="29"></td>
    <td width="104"></td>
    <td width="544"></td>
  </tr>
  <tr id="etiqueta">
    <td colspan="3" width="689" height="22">
    <input type="radio" onClick="mensajetipoavance('1')" value="incompleto" name="tipoavance">Incompleto</td>
  </tr>
  <tr id="fila" style="display:none">
    <td width="29" height="19">&nbsp;</td>
    <td width="104" height="19">Especifique el motivo</td>
    <td width="544" height="19"><input type="text" id="motivoincompleto" class="Cajas" size="20" style="width: 95%"></td>
  </tr>
  <tr id="etiqueta">
    <td colspan="3" width="689" height="22">
    <input type="radio" onClick="mensajetipoavance('2')" value="noexiste" name="tipoavance">No 
    existe (Marque solamente cuando el documento que se le solicita 
    no existe en el área que desempeña)</td>
  </tr>
  <tr id="fila" style="display:none">
    <td width="29" height="19">&nbsp;</td>
    <td width="104" height="19">Especifique el motivo </td>
    <td width="544" height="19"><input type="text" id="motivonoexiste" class="Cajas" size="20" style="width: 95%"></td>
  </tr>
  <tr id="etiqueta">
    <td colspan="3" width="689" height="22"><input type="radio" onClick="mensajetipoavance('3')" value="otro" name="tipoavance">Otra 
    Área (Marque  solamente para delegar la presentación del 
    documento a otra persona )</td>
  </tr>
  <tr id="fila" style="display:none">
    <td width="29" height="28">&nbsp;</td>
    <td height="28" colspan="2">
	<select id="idusuario" style="width: 90%">
		<option value="0">[Especifique que persona presentará el documento]</option>
	<%if IsEmpty(ArrUsuario)=false then
	for j=lbound(ArrUsuario,2) to Ubound(ArrUsuario,2)%>
		<option value="<%=ArrUsuario(0,j)%>"><%=ArrUsuario(1,j)%></option>
	<%next
	end if%>
		</td>
  </tr>
  </table>
</fieldset>
<p align="right">
    <input type="reset" value="Cerrar" name="cmdSalir" class="cerrar">
    <input type="button" onClick="validarmotivoavance()" id="cmdAceptar" disabled="true" value="Aceptar" name="cmdAceptar" class="guardar" style="width: 100">
</p>
</body>
</html>