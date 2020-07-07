<!--#include file="../../funciones.asp"-->
<%
'***************************************************************************************
'CV-USAT
'Archivo		: menuaplicacion.asp
'Autor			: Gerardo Chunga Chinguel
'Fecha de Creación	: 28/11/200503:53:45 p.m.
'Observaciones		: formulario para ingreso/modificación de datos de la tabla menUAPLICACION
'***************************************************************************************

accion=Request.querystring("accion")
codigo_apl=Request.querystring("codigo_apl")
codigo_men=Request.querystring("codigo_men")
codigoRaiz_men=Request.querystring("codigoRaiz_men")
orden_men=Request.querystring("orden_men")

if accion="Modificarmenuaplicacion" then
	Set Obj= Server.CreateObject("PryUSAT.clsDatAplicacion")
		Set rsmenu=Obj.ConsultarAplicacionUsuario("RS","13",codigo_men,"","")
		descripcion_men=rsmenu("descripcion_men")
		enlace_men=rsmenu("enlace_men")
		codigoRaiz_men=rsmenu("codigoRaiz_men")
		icono_men=rsmenu("icono_men")
		iconosel_men=rsmenu("iconosel_men")
		expandible_men=rsmenu("expandible_men")
		nivel_men=rsmenu("nivel_men")
		orden_men=rsmenu("orden_men")
		variable_men=rsmenu("variable_men")
		rsmenu.close
		Set rsmenu=nothing
	Set Obj=nothing
else
	variable_men="menu" & orden_men
end if
%>
<HTML>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Registro de menús de la Aplicación</title>
<link rel="stylesheet" type="text/css" href="../../private/estilo.css">
<script language="JavaScript" src="../../private/funciones.js"></script>
<script language="JavaScript" src="private/validarsistema.js"></script>
</HEAD>
<BODY onload="frmmenuaplicacion.txtdescripcion_men.focus()" style="background-color: #EEEEEE">
<form name="frmmenuaplicacion" method="post" onSubmit="return validarfrmmenuaplicacion()" action="procesar.asp?accion=<%=accion%>&codigo_apl=<%=codigo_apl%>&codigo_men=<%=codigo_men%>&codigoRaiz_men=<%=codigoRaiz_men%>">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" bgcolor="white" class="contornotabla">
<tr>
<td width="15%" class="etiqueta">Tipo de Menú</td>
<td width="80%" colspan="3" class="azul">
<%if codigoraiz_men=0 then
	response.write "Principal"
else
	response.write "Sub Menú"
end if%>
</td>
</tr>
<tr>
<td width="20%" class="etiqueta">Orden</td>
<td width="16%">
<input class="cajas" type="text" onkeypress="validarnumero()" value="<%=orden_men%>" name="txtorden_men" size="4" maxlength="4"></td>
<td width="35%">
Nivel <select size="1" name="cbonivel_men">
<option value="1" <%=SeleccionarItem("cbo",nivel_men,"1")%>>1</option>
<option value="2" <%=SeleccionarItem("cbo",nivel_men,"2")%>>2</option>
<option value="3" <%=SeleccionarItem("cbo",nivel_men,"3")%>>3</option>
</select></td>
<td width="34%">
<input type="checkbox" name="chkexpandible_men" value="true" <%=SeleccionarItem("chk",expandible_men,"true")%>>Menú 
expandible </td>
</tr>
<tr>
<td width="20%" class="etiqueta">Título</td>
<td width="80%" colspan="3">
<input class="cajas2" type="text" value="<%=descripcion_men%>" name="txtdescripcion_men" size="80" maxlength="500"></td>
</tr>
<tr>
<td width="20%" class="etiqueta">Ruta</td>
<td width="80%" colspan="3">
<input class="cajas2" type="text" value="<%=enlace_men%>" name="txtenlace_men" size="80" maxlength="255"></td>
</tr>
<tr>
<td width="20%" class="etiqueta">Icono</td>
<td width="80%" colspan="3">
<input class="cajas2" type="text" value="<%=icono_men%>" name="txticono_men" size="50" maxlength="50"></td>
</tr>
<tr>
<td width="20%" class="etiqueta">Icono resaltado</td>
<td width="80%" colspan="3">
<input class="cajas2" type="text" value="<%=iconosel_men%>" name="txticonosel_men" size="50" maxlength="100"></td>
</tr>
<tr>
<td width="20%" class="etiqueta">Variable</td>
<td width="80%" colspan="3">
<input class="cajas2" type="text" value="<%=variable_men%>" name="txtvariable_men" size="50" maxlength="100"></td>
</tr>
<tr>
<td width="100%" colspan="4" align="right">
<input type="submit" value="Guardar" name="smtGuardar" class="usatguardar"> <input OnClick="window.close()" type="button" value="Cancelar" name="cmdCancelar" class="usatsalir">
</td>
</tr>
</table>
</form>
</BODY>
</HTML>