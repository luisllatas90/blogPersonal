<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Buscar asignaturas programadas</title>
<script type="text/javascript" language="Javascript">
	function RetornarParametro()
	{
		var Argumentos = window.dialogArguments;
	   	Argumentos.termino=document.all.txtcurso.value
	   	Argumentos.ConsultarCursos(document.all.hdpagina.value);
		window.close();
	}
	
	function HabilitarBusqueda(obj)
	{
		if(obj.value.length>3){
			cmdBuscar.disabled=false
		}
		else{
			cmdBuscar.disabled=true
		}
	}
	
	function ElegirCriterio(opt)
	{
		trBuscar.style.display="none"
		cmdBuscar.disabled=false
		if (opt.value==2){
			cmdBuscar.disabled=true
			trBuscar.style.display=""
			txtcurso.focus()
		}
	}
</script>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
</head>
<body style="background-color: #f0f0f0; margin-top: 15px; margin-bottom: 15px;">
<input type="hidden" value="<%=request.querystring("pagina")%>" name="hdpagina">
<center>
<table width="80%">
	<tr>
		<td>
		<input name="optA" onClick="ElegirCriterio(this)" type="radio" style="width: 20px" checked="checked" value="1"></td>
		<td>Todas las asignaturas programadas</td>
	</tr>
	<tr>
		<td><input name="optA" onClick="ElegirCriterio(this)" type="radio" value="2"></td>
		<td>Buscar asignatura por descripción o código</td>
	</tr>
	<tr id="trBuscar" style="display:none">
		<td>&nbsp;</td>
		<td> <input name="txtcurso" type="text" class="Cajas2" maxlength="15" onKeyUp="HabilitarBusqueda(this)"></td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td align="right">&nbsp;
			</td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td align="right">
			<input name="cmdBuscar" type="button" value="   Búsqueda" class="buscar1" onClick="RetornarParametro()">
		</td>
	</tr>
</table>
</center>
</body>
</html>
