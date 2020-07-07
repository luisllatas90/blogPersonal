<!--#include file="../../../../funciones.asp"-->
<%
codigo_cup=request.querystring("codigo_cup")
codigo_tfu=session("codigo_tfu")
tipo=request.querystring("tipo")
if tipo="C" then
	tipo="readonly='true' style=""background:#CCCCCC"""
end if
'9--director de escuela
'32-asistente de escuela
'15-director de dpto
Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion
	Set rsCurso=obj.Consultar("ConsultarCursoProgramado","FO",10,codigo_cup,0,0,0)
	Set rsDpto=obj.Consultar("ConsultarCargaAcademicaDpto","FO","5",0,0,0)
	
	numeroDoc_cup=rsCurso("numeroDoc_cup")
	grupohor_cup=rsCurso("grupohor_cup")
	vacantes_cup=rsCurso("vacantes_cup")
	estado_cup=rsCurso("estado_cup")
	fechaini_cup=Formatdatetime(rsCurso("fechainicio_cup"),2)
	fechafin_cup=Formatdatetime(rsCurso("fechafin_cup"),2)
	fecharetiro_cup=Formatdatetime(rsCurso("fecharetiro_cup"),2)
	usuario_cup=rsCurso("usuario_cup")
	obs_cup=rsCurso("obs_cup")
	codigoDac_cup=rsCurso("codigoDac_cup")
	SoloPrimerCiclo=rsCurso("SoloPrimerCiclo_cup")


	If trim(rsCurso("mododesarrollo_pcu"))="M" then
		mostrar=""		
	end if
obj.CerrarConexion
Set obj=nothing

'if (codigo_tfu=1 or codigo_tfu=7 or codigo_tfu=16) or _
'	cdbl(usuario_cup)=cdbl(session("codigo_usu")) then
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<title>Modificar Grupo Horario</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/calendario.js"></script>
<script type="text/javascript" language="JavaScript" src="../private/validarprogramacion.js"></script>
<style type="text/css">
.lblcurso {
	background-color: #E7E6C9;
}
</style>
</head>
<body style="background-color: #F2F2F2">

<form name="frmlistacursos" method="POST" action="procesar.asp?accion=modificarcursoprogramado&amp;codigo_cup=<%=codigo_cup%>">
<table width="100%" class="contornotabla" cellpadding="3">
<tr>
	<td style="width: 25%" class="lblcurso">Programación</td>
	<td style="width: 35%" class="rojo"><%=numeroDoc_cup%>&nbsp;</td>
	<td style="width: 20%" class="azul" align="right">Estado actual</td>
	<td style="width: 20%" class="rojo">
	<select name="cboEstado" onChange="HabilitarVacantes(this)" <%=tipo%>>
	<option value="1" <%if estado_cup=true then response.write "SELECTED"%>>Abierto</option>
	<option value="0" <%if estado_cup=false then response.write "SELECTED"%>>Cerrado</option>
	</select></td>
</tr>
<tr>
	<td style="width: 25%" class="lblcurso">Asignatura</td>
	<td style="width: 75%" colspan="3"><%=rsCurso("nombre_cur")%>(<%=rsCurso("identificador_cur")%>)</td>
</tr>
<tr>
	<td style="width: 25%; " class="lblcurso">Grupo Horario</td>
	<td style="width: 75%; " colspan="3">
	<input name="txtGrupos" type="text" value="<%=Grupohor_cup%>" maxlength="15" class="Cajas" size="15" <%=tipo%> />	</td>
</tr>
<tr id="trVacantes">
	<td style="width: 25%; " class="lblcurso">Vacantes</td>
	<td style="width: 25%; ">
	<input name="txtVacantes" type="text" value="<%=vacantes_cup%>" class="Cajas" size="3" onKeyPress="validarnumero()" maxlength="3" <%=tipo%> /></td>
	<td style="width: 25%; " align="right">&nbsp;</td>
	<td style="width: 25%; ">&nbsp;</td>
</tr>
<tr <%=mostrar%>>
	<td style="width: 25%" class="lblcurso">Fecha de Inicio</td>
	<td style="width: 75%" colspan="3">
	<input name="txtInicio" type="text" value="<%=fechaini_cup%>" class="Cajas" size="10" readonly="true" ><%if tipo="" then%><input name="cmdinicio" type="button" class="cunia" onClick="MostrarCalendario('txtInicio<%=i%>')" ><%end if%></td>
</tr>
<tr <%=mostrar%>>
	<td style="width: 25%" class="lblcurso">Fecha de Culminación</td>
	<td style="width: 75%" colspan="3">
	<input name="txtFin" type="text" value="<%=fechafin_cup%>" class="Cajas" size="10" ><%if tipo="" then%><input name="cmdinicio1" type="button" class="cunia" onClick="MostrarCalendario('txtFin<%=i%>');" ><%end if%></td>
</tr>
<tr <%=mostrar%>>
	<td style="width: 25%" class="lblcurso">Fecha de Retiro</td>
	<td style="width: 75%" colspan="3">
	<input name="txtRetiro" type="text" value="<%=fecharetiro_cup%>" class="Cajas" size="10" ><%if tipo="" then%><input name="cmdinicio0" type="button" class="cunia" onClick="MostrarCalendario('txtRetiro<%=i%>')" ><%end if%></td>
</tr>
<tr valign="top">
  <td style="width: 25%" class="lblcurso">Observaciones</td>
  <td style="width: 75%" colspan="3"><textarea name="txtobs_cup" cols="" rows="3" class="Cajas2"><%=obs_cup%></textarea></td>
</tr>
<tr valign="top">
  <td style="width: 25%" class="lblcurso">Solo Primer Ciclo</td>
  <td style="width: 75%" colspan="3">
      <input id="chkPrimerCiclo" type="checkbox" checked="<%=SoloPrimerCiclo %>" /></td>
</tr>
<tr>
  <td style="width: 25%" class="lblcurso">Dpto. Acad. al cual solicita Profesor </td>
  <td style="width: 75%" colspan="3">
	<%
	call llenarlista("cbocodigodac_cup","",rsDpto,"codigo_dac","nombre_dac",codigoDac_cup,"","","")
	if tipo="" then response.Write("<script>document.all.cbocodigodac_cup.readonly='true'</script>")
	%>  
  </td>
</tr>
</table>
<p align="right">
<input name="cmdRegresar" type="button" value="       Cancelar" class="noconforme1" onClick="window.close()">
<input name="cmdGuardar" type="button" value="        Guardar" class="guardar_prp" onClick="ValidarGrupoHorario()">
</p>
</form>
</body>
</html>
<%'else
'	response.write "<h4>No puede modificar un grupo horario que ha sido registrado por otro usuario</h4>"
'end if%>