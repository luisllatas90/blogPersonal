<%
fechainicio=request.querystring("fechainicio")
fechafin=request.querystring("fechafin")

if fechainicio="" then fechainicio=0
if fechafin="" then fechafin=0

Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsPedidos=Obj.Consultar("ConsultarPedidoBibliografico","FO",10,session("codigo_usu"),fechainicio,fechafin)
		Obj.CerrarConexion
		
		if Not(rsPedidos.BOF and rsPedidos.EOF) then
			HayReg=true
			
			if fechainicio="0" then
				fechainicio=rsPedidos("ultimafecha")
				fechafin=fechainicio
			end if
		end if
Set obj=nothing

%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Mis pedidos</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/calendario.js"></script>
<script type="text/javascript" language="Javascript">
	function BuscarPedidos()
	{
	
		AbrirMensaje('../../../../images/')
		location.href="mispedidos.asp?fechainicio=" + txtFechaInicio.value + "&fechafin=" + txtFechaFin.value
	
	}

	function AbrirListaPedido(codigo_ins,codigo_eped)
	{
		if (eval(event.srcElement.innerText)>0){
			var menu="Pedidos Bibliográficos "
			switch (codigo_eped){
				case 1:menu+="pendientes en "
						break
				case 2:menu+="aprobados en "
						break
				case 3:menu+="desaprobados en "
						break
			}
			menu+=event.srcElement.parentElement.cells[0].innerText
		
			location.href="lstpedidos.asp?menu=" + menu + "&codigo_ins=" + codigo_ins + "&codigo_eped=" + codigo_eped + "&tipobandeja=S"
		}
		else{
			event.srcElement.style.cursor="default"
		}
	}
</script>
</head>
<body>

<p class="usatTitulo">Consultar estado de mis pedidos bibliográficos</p>
<fieldset name="fraCriterio" style="width:100%">
<legend><b>Parámetros de consulta</b></legend>
		
<table width="100%">
	<tr>
		<td width="15%">Fecha Inicio:</td>
		<td width="10%">
		<input readonly name="txtFechaInicio" type="text" class="Cajas" id="txtFechaInicio" value="<%=fechainicio%>" size="10"><input name="cmdinicio" type="button" class="cunia" onClick="MostrarCalendario('txtFechaInicio')" >
		</td>
		<td width="15%" align="right">Fecha Fin:</td>
		<td width="10%">
		<input readonly name="txtFechaFin" type="text" class="Cajas" id="txtFechaFin" value="<%=fechafin%>" size="10"><input name="cmdfin" type="button" class="cunia" onClick="MostrarCalendario('txtFechaFin')" >		
		</td>
		<td width="50%">
		<input name="cmdBuscar" type="button" value="Buscar..." class="buscar2" onclick="BuscarPedidos()"></td>
	</tr>
</table>
</fieldset>
<%if HayReg=true then%>
<table width="100%" border="1" bordercolor="gray" cellpadding="3" style="border-collapse: collapse">
	<tr class="etabla">
		<td rowspan="2">INSTANCIAS</td>
		<td colspan="3">ESTADO DEL PEDIDO BIBLIOGRÁFICO</td>
		<td>&nbsp;</td>
	</tr>
	<tr class="etabla">
		<td bgcolor="yellow">PENDIENTES&nbsp;</td>
		<td class="Selected">APROBADOS</td>
		<td bgcolor="red" style="color: #FFFFFF">DESAPROBADOS</td>
		<td>TOTAL</td>
	</tr>
	<%
	total=0
	i=0
	Do while not rsPedidos.EOF
		i=i+1
		subtotal=rsPedidos("pendientes")+rsPedidos("aprobadas")+rsPedidos("desaprobadas")
		tp=tp+rsPedidos("pendientes")
		ta=ta+rsPedidos("aprobadas")
		td=td+rsPedidos("desaprobadas")
		tt=tt+subtotal
	%>
	<tr>
		<td><%=rsPedidos("descripcion_ins")%>&nbsp;</td>
		<td onmouseover="Resaltar(1,this,'S')" onmouseout="Resaltar(0,this,'S')" align="center" onclick="AbrirListaPedido('<%=rsPedidos("codigo_ins")%>',1)"><%=rsPedidos("pendientes")%>&nbsp;</td>
		<td onmouseover="Resaltar(1,this,'S')" onmouseout="Resaltar(0,this,'S')" align="center" onclick="AbrirListaPedido('<%=rsPedidos("codigo_ins")%>',2)"><%=rsPedidos("aprobadas")%>&nbsp;</td>
		<td onmouseover="Resaltar(1,this,'S')" onmouseout="Resaltar(0,this,'S')" align="center" onclick="AbrirListaPedido('<%=rsPedidos("codigo_ins")%>',3)"><%=rsPedidos("desaprobadas")%>&nbsp;</td>
		<td align="center"><%=subtotal%>&nbsp;</td>
	</tr>
	<%rsPedidos.movenext
	Loop
	
	set rsPedidos=nothing
	%>
	<tr class="etabla">
		<td>&nbsp;</td>
		<td><%=tp%></td>
		<td><%=ta%>&nbsp;</td>
		<td><%=td%>&nbsp;</td>
		<td><%=tt%>&nbsp;</td>
	</tr>
</table>
<%else%>
<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp; No se encontraron pedidos bibliográficos solicitados según las fechas indicadas</h5>
<%end if%>
</body>

</html>
