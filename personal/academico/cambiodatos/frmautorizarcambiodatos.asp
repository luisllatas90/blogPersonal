<!--#include file="../../../../NoCache.asp"-->
<%
destino=request.querystring("destino")
accion=request.querystring("accion")
estado_acd=request.querystring("estado_acd")
codigo_acd=request.querystring("codigo_acd")
operadorAut_acd=request.querystring("operadorAut_acd")
nombre_tbl=request.querystring("nombre_tbl")
codigo_tbl=request.querystring("codigo_tbl")
accion_acd=request.querystring("accion_acd")
if codigo_acd="" then codigo_acd=0

if accion="registrar" then
	operadorReg_acd=session("codigo_usu")
	fechaini_acd=request.form("fechainicio")
	fechafin_acd=request.form("fechafin")
	motivo_acd=request.form("motivo_acd")
	
	if operadorAut_acd="" then operadorAut_acd=operadorReg_acd

	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
			mensaje=Obj.Ejecutar("AgregarAutorizacionCambioDatos",true,codigo_acd,accion_acd,fechaini_acd,fechafin_acd,operadorReg_acd,operadorAut_acd,nombre_tbl,codigo_tbl,motivo_acd,estado_acd,null)
		obj.CerrarConexion
	set Obj=nothing
	
	response.write "<h3 align='center'>" & mensaje & "</h3>"
else
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Solicitar autorización de cambio de datos</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" />
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/calendario.js"></script>
<script type="text/javascript" language="javascript">
	function GuardarDatos()
	{
		if (frmAutorizar.motivo_acd.value.length<3){
			alert("Debe ingresar el motivo de la autorización")
			return(false)
			frmAutorizar.motivo_acd.focus()
		}
	
		frmAutorizar.submit()
	}
</script>
</head>

<body style="background-color: #F0F0F0">
<%
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
			Set rsAutorizacion=obj.Consultar("ConsultarAutorizacionCambioDatos","FO",2,codigo_tbl,session("codigo_usu"),0,0)

			'Ya tiene respuesta de la solicitud de cambio de datos			
			if Not(rsAutorizacion.BOF and rsAutorizacion.EOF) then
				mensaje=rsAutorizacion("mensaje")
			end if
		obj.CerrarConexion
	Set obj=nothing
	
if mensaje<>"" then
	response.write "<h3 align='center'>" & mensaje & "</h3>"
else
%>	
<form name="frmAutorizar" method="post" action="frmautorizarcambiodatos.asp?accion=registrar&codigo_acd=<%=codigo_acd%>&estado_acd=<%=estado_acd%>&operadorAut_acd=<%=operadorAut_acd%>&nombre_tbl=<%=nombre_tbl%>&codigo_tbl=<%=codigo_tbl%>&accion_acd=<%=accion_acd%>">
<table width="100%" class="contornotabla">
	<tr>
		<td width="99">Acción</td>
		<td width="80%"><strong><%=UCASE(accion_acd)%></strong></td>
	</tr>
	<tr>
		<td width="99">Destino</td>
		<td width="80%"><strong><%=UCASE(destino)%></strong></td>
	</tr>
	<tr>
		<td width="99">Fecha de inicio</td>
		<td width="80%"><input type="text" name="fechainicio" size="12" class="Cajas" readonly value="<%=date%>"><input type="button" class="cunia" onClick="MostrarCalendario('fechainicio')"></td>
	</tr>
	<tr>
		<td width="99">Fecha de término</td>
		<td width="80%">
		<input type="text" name="fechafin" size="12" class="Cajas" readonly value="<%=date%>" style="height: 19px"><input type="button" class="cunia" onClick="MostrarCalendario('fechafin')">
    	</td>
	</tr>
	<tr>
		<td width="99">Motivo</td>
		<td width="80%">&nbsp;</td>
	</tr>
	<tr>
		<td colspan="2">
		<textarea name="motivo_acd" cols="20" rows="3" class="Cajas2" onkeypress="ContarTextArea(this,100)"></textarea>
		</td>
	</tr>
	<tr>
		<td colspan="2" id="lblcontador" class="rojo">&nbsp;</td>
	</tr>
	</table>
<p align="center">
<input name="cmdGuardar" type="button" value="        Guardar" class="guardar_prp" onclick="GuardarDatos()" /><input name="cmdCancelar" type="reset" value="      Cancelar" class="noconforme1" />
</p>
</form>
<%end if%>
</body>
</html>
<%end if%>