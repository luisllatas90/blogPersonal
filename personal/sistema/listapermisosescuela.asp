<!--#include file="../../funciones.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<title>Permisos por Escuela</title>
<link rel="stylesheet" type="text/css" href="../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
</head>

<body bgcolor="#EEEEEE">
<table style="width: 100%;height:100%" bgcolor="white">
	<tr height="45%">
		<td>
		<%
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsPersonal=obj.Consultar("ConsultarDocente","FO","TD",0,0)
		Obj.CerrarConexion
		Set objDocente=nothing
		
			Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio,pagina
		
			ArrEncabezados=Array("ID","Personal","Tipo")
			ArrCampos=Array("codigo_per","docente","descripcion_Tpe")
			ArrCeldas=Array("10%","70%","20%")
			ArrCamposEnvio=Array("codigo_per")
			pagina="lstpermisoescuela.asp?aplicacion=Silabo"
			
			call CrearRpteTabla(ArrEncabezados,rsPersonal,"",ArrCampos,ArrCeldas,"S","I",pagina,"S",ArrCamposEnvio,"")
		%>
		</td>
	</tr>
	<tr height="5%">
		<td class="usatCeldaTitulo">Escuelas Profesionales con acceso</td>
	</tr>
	<tr height="50%">
		<td class="contornotabla">
		<span id="mensajedetalle" class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp; Seleccione el Personal, para visualizar los permisos por Escuela Profesional</span>
		<iframe name="fradetalle" id="fradetalle" width="100%" height="100%" bordercolor="white" style="border:0px">
		Your browser does not support inline frames or is currently configured not to display inline frames.
		</iframe></td>
	</tr>
</table>

</body>
</html>
