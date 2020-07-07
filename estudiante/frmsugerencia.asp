<!--#include file="../NoCache.asp"-->
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registro de Sugerencias</title>
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<script language="Javascript">
	function ValidarSugerencia()
	{
		if (frmsugerencia.txtdescripcion_sug.value.length<11){
			alert("Por favor especifique la descripción de la sugerencia")
			frmsugerencia.txtdescripcion_sug.focus()
			return(false)
		}
		return(true)
	}
</script>
</head>

<body onLoad="document.all.txtdescripcion_sug.focus()">
<p class="usatTitulo">Registro de Sugerencias</p>
<p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; A continuación registre 
su consulta o sugerencia</p>
<form name="frmsugerencia" method="POST" onSubmit="return ValidarSugerencia()" action="procesar.asp?accion=agregarsugerencia">
 <p>
  <textarea rows="10" name="txtdescripcion_sug" cols="20" class="cajas2" maxlength="1000"></textarea></p>
  <p><input type="submit" value="Enviar" name="cmdEnviar">
  <input type="button" value="Cancelar" name="cmdCancelar"></p>
</form>
</body>
</html>