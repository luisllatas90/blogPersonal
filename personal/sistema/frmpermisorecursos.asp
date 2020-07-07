<!--#include file="../../funciones.asp"-->
<%
codigo_per=request.querystring("codigo_per")
codigo_usu=session("codigo_usu")

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsPersonal=obj.Consultar("ConsultarPersonal","FO","APE",0)
	obj.CerrarConexion
Set Obj=nothing
%>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Personal</title>
<link rel="stylesheet" type="text/css" href="../../private/estilo.css">
<script language="JavaScript" src="../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
	function BuscarPermisos(recurso)
	{
		var titulo="ACCESO PARA ADMINISTRAR / CONSULTAR ESCUELAS PROFESIONALES"
		
		if (window.cborecurso.value=="ambiente"){
			titulo="ACCESO PARA ADMINISTRAR AMBIENTES USAT"
		}
		if (window.cborecurso.value=="departamentoacademico"){
			titulo="ACCESO PARA ADMINISTRAR DEPARTAMENTOS ACADÉMICOS"
		}
		
		if (window.cbocodigo_per.value==""){
			alert("Seleccione el personal USAT")
		}
		else{
			window.tdRecurso.innerHTML=titulo
			window.frarecurso.document.location="tblrecursos.asp?usuarioaut_acr=" + cbocodigo_per.value + "&nombretbl_acr=" + window.cborecurso.value
			window.cmdGuardar.disabled=false
		}
	}
	
	
	function GuardarPermisos()
	{
		window.frarecurso.document.frmrecursos.submit()
	}
</script>
</head>

<body bgcolor="#EAEAEA">

<table width="100%" height="97%">
	<tr>
		<td height="100%" rowspan="2" valign="top" width="40%">
		
		<table width="100%" height="100%">
			<tr>
				<td width="100%" height="3%" class="etiqueta">Buscar personal</td>
			</tr>
			<tr>
				<td width="100%" height="75%">
				<%call llenarlista("cbocodigo_per","",rsPersonal,"codigo_per","personal",codigo_per,"","","multiple")%>
				<script type="text/javascript" language="javascript">cbocodigo_per.style.height="100%"</script>
				</td>
			<tr>
				<td width="100%" height="3%">Buscar permisos en:
				<select name="cborecurso">
				<option value="carreraprofesional">Escuelas Profesionales</option>
				<option value="ambiente">Ambientes USAT</option>
				<option value="servicioconcepto">Concepto de Servicios</option>
				<option value="centrocostos">Centro de Costos</option>
				<option value="departamentoacademico">Departamento Académico</option>
				<!-- HCANO 05-01-2017 -->
				<option value="Programa/Proyecto">Programa/Proyecto</option>
				<!-- FIN HCANO 05-01-2017 -->
				</select>
				</td>
			</tr>
			<tr>
			<td width="10%" height="3%" align="right">
			<img class="imagen" alt="Buscar permisos" onclick="BuscarPermisos()" src="../../images/buscar.gif">
			</td>
			</tr>
		</table>
		
		</td>
	</tr>
	<tr>
		<td valign="top" width="60%" height="100%">
		<table width="100%" height="100%">
			<tr>
				<td	 id="tdRecurso" width="100%" height="5%" class="usatTablaInfo">
				&nbsp;</td>
			</tr>
			<tr>
				<td width="100%" height="95%" class="contornotabla">
				<iframe name="frarecurso" id="frarecurso" height="100%" width="100%" border="0" frameborder="0">
				El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.
				</iframe>
				</td>
			</tr>
			
		</table>		
		
		</td>
	</tr>
	<tr>
		<td valign="top" width="40%" height="5%">&nbsp;</td>
		<td valign="top" width="60%" height="5%" align="right">
		<input name="cmdGuardar" type="button" value="Guardar cambios" disabled="true" onclick="GuardarPermisos()" />
		</td>
	</tr>
	</table>
</body>

</html>
