<!--#include file="../../../funciones.asp"-->
<%
if session("codigo_usu")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es" >
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<title>Cambiar de Plataforma</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script type="text/javascript" language="Javascript">
function ValidarEleccion()
{
	if (frmTemas.cboidcursovirtual.value==""){
		alert("Debe elegir la actividad académica")
		frmTemas.cboidcursovirtual.focus()
		return(false)
	}
	
	if (frmTemas.cboplataforma.value==""){
		alert("Debe elegir el tipo de cambio")
		frmTemas.cboplataforma.focus()
		return(false)
	}

	frmTemas.submit()
}
</script>
</head>
<body style="background-color: #EEEEEE">
<p class="e4">Cambiar Plataforma de la actividad académica</p>
<form name="frmTemas" method="post" action="procesar_ccv.asp?accion=Cambiardisenio">
<table width="100%">
	<tr valign="top">
	<%
	Set Obj= Server.CreateObject("aulaVirtual.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCurso=obj.Consultar("ConsultarCursoVirtual","FO",12,session("codigo_usu"),0,0)
	obj.CerrarConexion
	Set Obj=nothing
	%>		
	<td class="boton" width="100%">&nbsp; Actividades Académicas</td>
	</tr>
	<tr>
		<td valign="top" width="100%">
		<select id="cboidcursovirtual" name="cboidcursovirtual" class="cajas">
		<option value="">>>Seleccione el curso virtual<< </option>
   <%Do while not rsCurso.eof%>
  		<option value="<%=rsCurso("idcursovirtual")%>" <%if cdbl(rsCurso("idcursovirtual"))=cdbl(idcursovirtual) then response.write("SELECTED") end if%>><%=rsCurso("titulocursovirtual")%></option>
	  	<%rsCurso.movenext
	  loop
  	Set rsCurso=nothing%>
  </select>
		</td>
	</tr>
	<tr>
		<td class="boton" width="100%">
		<b>&nbsp;Seleccione la acción a realizar</b></td>
	</tr>
	<tr>
		<td width="100%">
		<select size="1" name="cboplataforma">
        <option>--Cambiar de Plataforma --</option>
        <option value="1">Plataforma de Investigación (Bloques)</option>
        <option value="2">Plataforma de Investigación (Tareas)</option>
        <option value="3">Plataforma Moodle USAT</option>
        </select>
        <input type="button" name="cmdGuardar" value="Procesar" onclick="ValidarEleccion()">
        </td>
	</tr>
</table>
</form>
</body>
</html>