<!--#include file="../../../../funciones.asp"-->

<%
codigo_usu=session("codigo_usu")

	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsPedidos=Obj.Consultar("ConsultarPedidoBibliografico","FO",11,codigo_usu,0,1)
			''1: estado borrador
		Obj.CerrarConexion
	Set obj=nothing

	if Not(rsPedidos.BOF and rsPedidos.EOF) then
		HayReg=true
	end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Borrador de Pedidos</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/validarpagina.js"></script>
<script language="JavaScript" src="../private/validarpedido.js"></script>
</head>
<body style="background-color:#F0F0F0">
<p class="usatTitulo">Borrador de Pedidos Bibliográficos</p>
<%if HayReg=true then %>
<input type="hidden" id="txtelegido" value="0">
<p>
<input name="cmdDetalle" type="button" value="      Enviar pedido" class="enviaryrecibir1" disabled="TRUE" onClick="AbrirPedido('A',txtelegido.value,'A')">
<input name="cmdModificar" type="button" value="       Ver detalle" class="editar1" disabled="TRUE" onClick="AbrirPedido('M',txtelegido.value,'B')">
<input name="cmdEliminar" type="button" value="       Eliminar" class="noconforme1" disabled="TRUE" onClick="AbrirPedido('E',txtelegido.value)">
</p>

<table cellpadding="3" cellspacing="0" style="border-style: solid; border-width: 1px; border-collapse: collapse;background-color:#FFFFFF;" bordercolor="gray" border="1" width="100%"> 
	<tr class="etabla">
		<td>#</td>
		<td>Fecha</td>
		<td>Escuela Profesional / <br>
		Departamento Académico</td>
		<td>T&iacute;tulo</td>
	    <td>Autor</td>
	    <td>Cantidad</td>
	</tr>
	<%Do while Not rsPedidos.EOF
		i=i+1	 %>
	<tr class="Sel" Typ="Sel" id="fila<%=rsPedidos("codigo_ped")%>"  class="piepagina" valign="top" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onclick="SeleccionarPedido(this)">
		<td style="width: 3%"><%=i%>&nbsp;</td>
		<td style="width: 12%"><%=rsPedidos("fechareg_ped")%>&nbsp;</td>
		<td style="width: 30%"><%=rsPedidos("descripcion_cco")%>&nbsp;</td>
		<td style="width: 30%"><%=rsPedidos("Titulo")%>&nbsp;</td>
	    <td style="width: 20%"><%=rsPedidos("NombreAutor")%>&nbsp;</td>
	    <td style="width: 10%"><%=rsPedidos("Cantidad_Dpe")%>&nbsp;</td>
	</tr>
	<%
		rsPedidos.movenext
	Loop
	%>
</table>
 <%else%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se 
	han encontrado Pedidos Bibliográficos registrados en Borrador</h5>
<%end if%>
</body>
</html>
