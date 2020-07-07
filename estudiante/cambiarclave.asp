<!--#include file="../NoCache.asp"-->
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Cambiar Clave</title>
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<script>
function ValidarClave(frm)
{
	if (frm.txtclaveanterior.value==""){
		alert("Por favor ingrese su contraseña anterior")
		frm.txtclaveanterior.focus
		return(false)	
	}

	if (frm.txtclavenueva.value==""){
		alert("Por favor ingrese su constraseña nueva")
		frm.txtclavenueva.focus
		return(false)	
	}

	if (frm.txtclavenueva2.value==""){
		alert("Por favor repita su constraseña nueva")
		frm.txtclavenueva2.focus
		return(false)	
	}
	return(true)
}

</script>
</head>
<body>
<p class="usatTitulo">Cambiar contraseña de Acceso</p>
<p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; El máximo de caracteres que debe contener la contraseña debe ser a 
    20 caracteres</p>
<form name="frmClave" method="POST" onsubmit="return ValidarClave(this)" action="procesar.asp?accion=cambiarclave">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="80%" id="tblclave" class="contornotabla">
  <tr>
    <td width="50%" class="etiqueta">Contraseña Actual</td>
    <td width="50%"><input type="password" name="txtclaveanterior" size="20" maxlength="20"></td>
  </tr>
  <tr>
    <td width="50%" class="etiqueta">Contraseña Nueva</td>
    <td width="50%">
    <input type="password" name="txtclavenueva" size="20" maxlength="20"></td>
  </tr>
  <tr>
    <td width="50%" class="etiqueta">Confirmar Contraseña Nueva</td>
    <td width="50%">
    <input type="password" name="txtclavenueva2" size="20" maxlength="20"></td>
  </tr>
  <tr>
    <td width="100%" colspan="2" align="center">
    <input type="submit" value="Guardar" name="cmdGuardar" class="usatguardar">
    <input type="reset" value="Cancelar" name="cmdCancelar" class="salir"></td>
  </tr>
</table>
</form>
</body>
</html>