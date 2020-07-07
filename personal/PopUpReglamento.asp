<%
'codigo_apl=request.querystring("codigo_apl")
'descripcion_apl=request.querystring("descripcion_apl")
'estilo_apl=request.querystring("estilo_apl")

'Set ObjUsuario= Server.CreateObject("PryUSAT.clsAccesoDatos")
'		objUsuario.AbrirConexion
'		set rs=ObjUsuario.consultar("ConsultarAplicacionUsuario","FO","15",session("codigo_usu"),codigo_apl,"")
'		objUsuario.CerrarConexion
'	Set ObjUsuario=nothing-->
%>
<html>
<head>
<META Http-Equiv="Cache-Control" Content="no-cache">
<META Http-Equiv="Pragma" Content="no-cache">
<META Http-Equiv="Expires" Content="0">
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Reglamento de Seguridad y Salud en el Trabajo</title>
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<script language="JavaScript" src="../private/jq/jquery.js"></script>
<script language="JavaScript" src="../private/jq/controles.js"></script>
<style type="text/css">
.titulofuncion {
	font-size: 11px;
	font-family: verdana;
	font-weight: bold;
	color: #000080;
}
</style>
</head>
<script language="javascript">
/*function ProcesarAccion()
{
	var control= $("select[name='cbocodigo_tfu']").getValue();
	
	if (control==""){
		alert("Por favor seleccione el tipo de función con el que desea Ingresar")
		return(false)
	}
 	else{
		var Argumentos = window.dialogArguments
	   	Argumentos.ctfu=control
   		Argumentos.dtfu=$("cbocodigo_tfu option:selected").text() //control.options[control.options.selectedIndex].text
		Argumentos.c_apl='<%=codigo_apl%>'
		Argumentos.d_apl='<%=descripcion_apl%>'
		Argumentos.e_apl='<%=estilo_apl%>'
   		
   		Argumentos.AbrirAplicacion()
		window.close()
	}
}*/
</script>
<body style="margin: 5px">
<p class="titulofuncion">&nbsp; Estimado trabajador, alcanzamos la Versión 2.0 – 2019 del Reglamento Interno de Seguridad y Salud en el Trabajo. </p>
<a href="../librerianet/reglamentos/ReglamentoIntSeguridadSaludTrabajo.pdf" target="_blank" style="font-weight: bold;">Descargar Reglamento</a>
<input name="cmdDescargar" type="button" value="Descargar Reglamento de SST" >
<p align="left">
<input type="checkbox" type="button"  /> He leido y acepto los terminos y condiciones
</p>
<center>
    <input name="cmdAceptar" type="button" value="Acepto" >
	<input name="cmdSalir" type="button" value="Cancelar" />
</center>

</body>
</html>
