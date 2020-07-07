<%
codigo_cup=request.querystring("codigo_cup")
codigo_cur=request.querystring("codigo_cur")
nombre_cur=request.querystring("nombre_cur")
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Autorizar permisos para Llenado de Notas</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/calendario.js"></script>
<script language="JavaScript" src="../private/validarnotas.js"></script>
</head>
<body style="background-color: #CCCCCC">

<table border="0" cellpadding="4" cellspacing="0" style="border-style: solid; border-collapse: collapse;background-color: #FFFFFF" bordercolor="#111111" width="100%" class="contornotabla">
	<tr>
		<td style="width: 20%">Código</td>
		<td width="458"><%=codigo_cur%></td>
	</tr>
	<tr>
		<td style="width: 20%">Descripción</td>
		<td width="80%"><%=nombre_cur%>&nbsp;</td>
	</tr>
	<tr>
		<td style="width: 20%">Fecha Inicio</td>
		<td style="width: 24%">
		<input type="text" name="txtfechaini_aut" size="12" class="Cajas" readonly value="<%=date%>"><input type="button" class="cunia" onClick="MostrarCalendario('txtfechaini_aut')">
		</td>
	</tr>
	<tr>
		<td style="width: 20%">Fecha Fin</td>
		<td style="width: 24%">
		<input type="text" name="txtfechafin_aut" size="12" class="Cajas" readonly value="<%=dateAdd("d",date,1)%>"><input type="button" class="cunia" onClick="MostrarCalendario('txtfechafin_aut')">
		</td>
	</tr>
	<tr>
		<td style="width: 20%">Motivo de autorización</td>
		<td width="80%">
		<input type="text" name="txtmotivo_aut" value="" class="cajas2" size="50" maxlength="50"></td>
	</tr>
	<tr>
		<td style="width: 20%">&nbsp;</td>
		<td width="100%">
		<input name="cmdGuardar" type="submit" value="Guardar" class="usatGuardar" onclick="EnviarAutorizacionNotas('<%=codigo_cup%>')">
		<input name="cmdCancelar" type="button" value="Cancelar" class="usatSalir" onclick="window.close()"/>
		</td>
	</tr>
</table>

</body>
</html>