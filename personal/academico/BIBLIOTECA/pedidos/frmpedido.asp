<!--#include file="../../../../funciones.asp"-->

<script>
function AbrirDetallePedido(modo,param1)
{
	var codigo_cco=0
	var pagina=""	
	switch(modo)
		{
		case "A":
				var codigo_cco=document.all.cboCodigo_cco.value		
				var tipoBD=document.all.cbotipoBD
				var textoBD=tipoBD.options[tipoBD.selectedIndex].text
				var Libro=null
				pagina="frmdetallepedido.asp?accion=agregardetallepedido&tipoBD=" + tipoBD.value + "&textoBD=" + textoBD + "&idLibro=" + param1 + "&codigo_cco=" + codigo_cco				
				showModalDialog(pagina,window,"dialogWidth:590px;dialogHeight:400px;status:no;help:no;center:yes")
				break				
		case "M":
				var codigo_cco=document.all.cboCodigo_cco.value
				pagina="frmdetallepedido.asp?accion=agregardetallepedido&idLibro=0&codigo_dpe=" + fraDetalle.document.all.txtelegido.value + "&codigo_cco=" + codigo_cco
				//showModalDialog(pagina,window,"dialogWidth:590px;dialogHeight:400px;status:no;help:no;center:yes")
				AbrirPopUp(pagina,'400','590')
				break
		
		case "E":
				if (confirm(Mensaje[0])==true){
					pagina="procesar.asp?accion=eliminardetallepedido&codigo_dpe=" + fraDetalle.document.all.txtelegido.value
					HabilitarBotones('D',true)
					fraDetalle.location.href=pagina
				}
				break				
		case "B":
		codigo_cco=document.all.cboCodigo_cco.value

			if (codigo_cco!=-2){				
				pagina="frmbuscarbibliografia.asp?codigo_cco=" + codigo_cco
				location.href=pagina
				break
			}else{
				alert ("Seleccione el área por la cual va a elevar el pedido.")
				break			
			}
			
		
		case "N":
				pagina="frmagregarbibliografia.asp?accion=agregarcatalogo&codigo_cco=" + param1
				AbrirPopUp(pagina,'450','590')
				break
		}
}

</script>
<%
call Enviarfin(session("codigo_usu"),"../../../../")

modo=request.querystring("modo")
codigo_cco=request.querystring("codigo_cco")
codigo_usu=session("codigo_usu")
codigo_ped=request.querystring("codigo_ped")

if codigo_ped<>"" then session("codigo_ped")=codigo_ped
if session("codigo_ped")<>"" then codigo_ped=session("codigo_ped")
if codigo_ped="" then codigo_ped=0

if modo="" then modo="A"

	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsCentroCosto=Obj.Consultar("ConsultarPedidoBibliografico","FO",1,0,0,0)
			
			if modo="M" then
				Set rsPedidos=Obj.Consultar("ConsultarPedidoBibliografico","FO",12,codigo_ped,0,0)
				
				if Not(rsPedidos.BOF and rsPedidos.EOF) then
					codigo_cco=rsPedidos("codigo_cco")
					fecha=rsPedidos("fechareg_ped")
				end if		
			else
				fecha=now
				session("codigo_ped")=0
			end if
		Obj.CerrarConexion
		
	Set obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registro de Pedidos</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../private/validarpedido.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/tooltip.js"></script>
<style type="text/css">
p            { margin-top: 0; margin-bottom: 3}
</style>
</head>
<body style="background-color: #F2F2F2">
<%if modo="M" then %>
<!--<input name="cmdAgregar" type="button"  value="        Buscar Libro" class="buscar" onClick="AbrirDetallePedido('B')">-->
<%'else%>

<input name="cmdAgregar" type="button"  value="        Nuevo" class="nuevo1" onClick="AbrirDetallePedido('B')" 
disabled="disabled">
<%end if%>
<input name="cmdCancelar" type="button" value="      Cerrar" class="noconforme1" onClick="history.back(-1)">
<input name="cmdGuardar" type="button" style="visibility:hidden" value="         Guardar" class="guardar_prp" onClick="AbrirPedido('A','<%=session("codigo_ped")%>','B')" tooltip="<b>Guardar en Borrador</b><br><br>Registra la solicitud de pedido bibliográfico como Borrador<br>para enviarlo en otro momento">
<%if modo="M" then %>
<input name="cmdEnviar" type="button" value="        Enviar" class="enviarpropuesta" onClick="AbrirPedido('A','<%=session("codigo_ped")%>','A')" tooltip="<b>Enviar Pedido</b><br><br>Envia toda la solicitud de pedido bibliográfico<br>para la Aprobación según la Escuela o Departamento Académico</b>">
<%end if%>


<table width="100%" cellpadding="2" height="90%" class="bordesup">
	<tr>
		<td style="width: 25%">Fecha de Pedido</td>
		<td style="width: 75%"><%=fecha%>&nbsp;</td>
	</tr>
	<tr>
		<td style="width: 25%; height: 5%;" height="100%">Solicitante</td>
		<td style="width: 75%; height: 5%;"><%=session("Nombre_Usu")%></td>
	</tr>
	<tr>
		<td style="width: 25%; height: 5%;">Direcci&oacute;n</td>
		<td style="width: 75%; height: 5%;">

		<%call llenarlista("cboCodigo_cco","ValidarCentroCosto(this,'" & modo & "')",rsCentroCosto,"codigo_cco","descripcion_cco",codigo_cco,"Seleccione la Dirección","","")%>
		<%if codigo_cco<>"" then
			Response.Write("<script>document.all.cboCodigo_cco.disabled=true </script>")
		end if
		%>
		</td>
	</tr>
	<tr>
		<td style="width: 25%; height: 5%;">Detalle del Pedido</td>
	  <td style="width: 75%; height: 5%;">
		<input name="cmdModificar" type="button" value="   Modificar" class="modificar2" onClick="AbrirDetallePedido('M')" disabled="true">
		<%if modo<>"M" then %>
<input name="cmdAgregar" type="button" style="height:20px"  value="        Buscar Libro" class="buscar" onClick="AbrirDetallePedido('B')">
<%end if%>
		<input name="cmdEliminar" type="hidden" value="Eliminar" class="eliminar2" onClick="AbrirDetallePedido('E')" disabled="true"></td>
	</tr>
	<tr>
		<td style="height: 80%;" colspan="2" valign="top" class="contornotabla">
		<iframe name="fraDetalle" id="fraDetalle" width="100%" height="100%" frameborder="0" src="lstdetallepedido.asp">
		Your browser does not support inline frames or is currently configured not to display inline frames.
		</iframe>
		</td>
	</tr>

</table>
</body>
</html>
