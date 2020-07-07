<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Buscar curso programado</title>
<script type="text/javascript" language="Javascript">
	function RetornarParametro()
	{
		var Argumentos = window.dialogArguments;
	   	Argumentos.termino=document.all.txtcurso.value
	   	Argumentos.ConsultarCursos();
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
</script>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
</head>
<body style="background-color: #f0f0f0; margin-top: 15px; margin-bottom: 15px;">

<p class="usatEtiqOblig">&nbsp;&nbsp; Ingrese la descripción o Código del curso:
<br>
&nbsp;&nbsp; <input name="txtcurso" type="text" class="Cajas" style="width:95%"  maxlength="15" onkeyup="HabilitarBusqueda(this)">
</p>
<p align="right">
<input name="cmdBuscar" type="button" value="   Búsqueda" class="buscar1" onclick="RetornarParametro()" disabled="true">
</p>
</body>
</html>
