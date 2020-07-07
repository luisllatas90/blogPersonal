<!--#include file="../../../../NoCache.asp"-->
<!--#include file="../../../../funciones.asp"-->
<%
Dim codigo_dpe

accion=Request.QueryString("accion")
tipoBD=Request.QueryString("tipoBD")
textoBD=Request.QueryString("textoBD")
idIngreso=Request.QueryString("idIngreso")
idLibro= Request.QueryString("IdLibro")
codigo_cco=Request.QueryString("codigo_cco")
codigo_dpe=Request.QueryString("codigo_dpe")
codigo_ped=session("codigo_ped")

codigo_Jpe=2
if codigo_dpe="" then codigo_dpe="0"
if codigo_ped="" then codigo_ped="0"

	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsCentroCosto=Obj.Consultar("ConsultarPedidoBibliografico","FO",1,0,0,0)
			Set rsPedido=Obj.Consultar("ConsultarPedidoBibliografico","FO",6,IdLibro,codigo_dpe,0)
			Set rsJustificacion=Obj.Consultar("ConsultarPedidoBibliografico","FO",7,0,0,0)
		Obj.CerrarConexion
	Set obj=nothing

	if Not(rsPedido.BOF and rsPedido.EOF) then
		codigo_Dpe=rsPedido("codigo_dpe")
		codigo_ped=rsPedido("codigo_ped")
		codigo_Jpe=rsPedido("codigo_Jpe")
		nombre_cur=rsPedido("nombre_cur")
		codigo_cur=rsPedido("codigo_cur")
		Tema_Dpe=rsPedido("tema_Dpe")
		Cantidad_Dpe=rsPedido("Cantidad_Dpe")
		Justificacion_Dpe=rsPedido("Justificacion_Jpe")
		textoBD=rsPedido("textoBD")
		fechapedido=rsPedido("fechareg_dpe")
		modo="M"
	else
		Cantidad_Dpe=1
		modo="A"
		fechapedido=now
	end if
	
	if IsNull(codigo_Jpe)=true or codigo_Jpe="" then
		codigo_Jpe=0
		imprimirscript="<script language='javascript'>MostrarOtro('0')</script>"
	else
		Justificacion_Dpe=""
	end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registrar Datos del Pedido</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../private/validarpedido.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
</head>
<body bgcolor="#F0F0F0">
<form name="frmPedido" AUTOCOMPLETE="OFF" method="post" action="procesar.asp?modo=<%=modo%>&accion=<%=accion%>&idLibro=<%=idLibro%>&Tipo_Dpe=<%=tipoBD%>&codigo_ped=<%=codigo_ped%>&codigo_Dpe=<%=codigo_Dpe%>&codigo_cco=<%=codigo_cco%>&precio=<%=Request.QueryString("precio")%>&moneda=<%=Request.QueryString("moneda")%>">
&nbsp;&nbsp;&nbsp;
<input name="cmdGuardar"  type="button" value="           Guardar" class="guardar_prp" onClick="ValidarRegPedido(frmPedido,'<%=modo%>',document.all.txtLibro.value)">
<input name="cmdCancelar" type="button" value="           Cerrar" class="noconforme1" onClick="window.close()">
<span id="mensaje" class="rojo"></span>
<input type="hidden" id="txtLibro" value="<%=idLibro%>">
<br>
<table width="97%" bgcolor="white" cellpadding="2" class="contornotabla" height="85%" align="center">
	<tr>
		<td style="width: 20%; height: 5px;">Fecha de Pedido:</td>
		<td style="width: 80%; height: 5px;" colspan="2"><%=fechapedido%></td>
	</tr>
	<tr>
		<td style="width: 20%; height: 5px;">Procedencia: </td>
		<td style="width: 80%; height: 5px;" colspan="2"><%=ucase(textoBD)%></td>
	</tr>
	<tr>
		<td style="width: 20%; height: 5px;" valign="top">Justificación
		<span class="rojo">*</span></td>
		<td style="width: 80%; height: 5px;" colspan="2">
		
		<select name="cbocodigo_Jpe" onChange="MostrarOtro(this.value)" class="Cajas2">
		<%Do while Not rsJustificacion.EOF%>
		<option value="<%=rsJustificacion("codigo_jpe")%>" <%=SeleccionarItem("cbo",codigo_jpe,rsJustificacion("codigo_jpe"))%>><%=rsJustificacion("descripcion_jpe")%></option>
		<%
			rsJustificacion.movenext
		Loop
		%>
		<option value="0" <%=SeleccionarItem("cbo",codigo_jpe,"0")%>>Otros</option>
		</select></td>
	</tr>
	<tr id="trOtraJustificacion" style="display:none">
		<td style="width: 20%; height: 5px;" valign="top" align="right">Especifique:</td>
		<td style="width: 80%; height: 5px;" colspan="2">
		<input name="txtJustificacion_Dpe" type="text" class="Cajas2" value="<%=Justificacion_Dpe%>"></td>
	</tr>
			<input name="txtCodigo_cur" type="hidden" value="<%=codigo_cur%>">
	<% 
		Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			EsAcademica=Obj.Ejecutar("BI_ConsultarSiAcademica",true,"VR",codigo_cco,0)
		Obj.CerrarConexion
	Set obj=nothing
	IF EsAcademica="SI" then
	%>
	<tr>
		<td style="width: 20%; height: 5px;">Asignatura <span class="rojo">*</span></td>
		<td style="width: 75%; height: 5px;">
		<input name="txtCurso" type="text" value="<%=nombre_cur%>" class="Cajas2" maxlength="30" onKeyUp="if(event.keyCode==13){BuscarAsignatura()}">
		
		</td>
		<td style="width: 5%; height: 5px;">
		<img src="../../../../images/menus/buscar_small.gif" name="imgBuscar" class="imagen" onClick="BuscarAsignatura()">
		</td>
	</tr>
	<tr id="trListaAsignaturas" style="display:none">
		<td colspan="3" height="40%" valign="top" align="right">
		<span id="lblmensajecurso" class="rojo"></span>
		<iframe name="fraCurso" id="fraCurso" height="100%" width="100%" scrolling="no" border="0" frameborder="0" class="contornotabla">
		El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.
		</iframe>		
		</td>
	</tr>
	
	<%end if%>
	<tr>
		<td style="width: 20%; height: 15%;" valign="top">
		<%
		if EsAcademica="SI" then
		%>
		Tema del Sílabos / 
		Investigación <span class="rojo">*</span>
		<%else%>
			Motivo de Interés 
		<%end if%>
		</td>
		<td style="width: 80%; height: 15%;" colspan="2" valign="top" align="right">
		<textarea name="txtTema_Dpe" cols="20" rows="4" class="Cajas2" onKeyPress="ContarTextArea(this,255)"><%=tema_dpe%></textarea>
		<span id="lblcontador" class="rojo"></span>
		</td>
	</tr>
	<tr>
		<td style="width: 20%; height: 5%;">&nbsp;Ejemplares <span class="rojo">
		*</span></td>
		<td style="width: 80%; height: 5%;" colspan="2">
		<input name="txtCantidad_Dpe" type="text" size="2" maxlength="2" onKeyPress="validarnumero()" value="<%=Cantidad_Dpe%>"></td>
	</tr>
	<tr id="trBlanco">
		<td colspan="3" height="40%" valign="top">&nbsp;</td>
	</tr>
</table>
<%=imprimirscript%>
</form>
</body>
</html>
