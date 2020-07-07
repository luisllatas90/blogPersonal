<!--#include file="../../../funciones.asp"-->
<%
codigo_usu=session("codigo_usu")
codigo_tfu=session("codigo_tfu")
codigo_sco= 950 ' request.querystring("codigo_sco")
fechainicio=request.querystring("fechainicio")
fechafin=request.querystring("fechafin")



if codigo_sco="" then codigo_sco="-2"
if fechainicio="" then fechainicio= "01/01/2008"
if fechafin="" then fechafin=date

Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		
		set rsServicio= obj.Consultar("ConsultarServicioConcepto","FO","CO",codigo_sco)
		
		if codigo_sco<>"-2" then
			Set rsMoneda=obj.Consultar("ConsultarServicioConcepto","FO","CO",codigo_sco)
			Set rsClientes=obj.Consultar("consultarDeudasAlumnos","FO","1",codigo_sco,0,fechainicio,fechafin)		
			
			if Not(rsClientes.BOF and rsClientes.EOF) then
				moneda=ucase(rsMoneda("descripcion_tip"))
				HayReg=true
				
			end if
		end if
	obj.CerrarConexion
set obj= Nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Deudas por cobrar</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../private/calendario.js"></script>
<script type="text/javascript" language="javascript">
	function BuscarDeudas()
	{
		pagina="../administrativo/controlpagos/vstdeudascobrarCandado.asp?codigo_sco=" + cbocodigo_sco.value + "&fechainicio=" + txtFechaInicio.value + "&fechafin=" + txtFechaFin.value + "&moneda=S"
		location.href="../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
	}

</script>
</head>

<body bgcolor="#EEEEEE">
<p class="usatTitulo">Deudas por cobrar</p>
<%if (rsServicio.BOF and rsServicio.EOF) then%>
	<h5>UD. no tiene permiso para acceder a los servicios registrados</h5>
<%else%>
<table cellpadding="3" style="width: 100%" class="contornotabla">
	<tr>
		<td class="etiqueta" width="15%">Servicio</td>
		<td colspan="5">
		<% call llenarlista("cbocodigo_sco","",rsServicio,"codigo_sco","descripcion_sco",codigo_sco,"Seleccione el servicio","","")%>
		</td>
	</tr>
	<tr>
		<td style="width: 15%">&nbsp;</td>
		<td style="width: 5%; text-align: right;">Desde</td>
		<td style="width: 10%">
		<input readonly name="txtFechaInicio" type="text" class="Cajas" id="txtFechaInicio" value="<%=fechainicio%>" size="10"><input name="cmdinicio" type="button" class="cunia" onClick="MostrarCalendario('txtFechaInicio')" ></td>
		<td style="width: 5%">
		Hasta</td>
		<td style="width: 10%">
		<input readonly name="txtFechaFin" type="text" class="Cajas" id="txtFechaFin" value="<%=fechafin%>" size="10"><input name="cmdfin" type="button" class="cunia" onClick="MostrarCalendario('txtFechaFin')" ></td>
		<td style="width: 55%">
		Moneda:<%=moneda%></td>
	</tr>
	<tr>
		<td colspan="6" align="right">
		<input name="cmdBuscar" type="button" value="Consultar" class="buscar" onclick="BuscarDeudas()">
		<%call botonExportar("../../../","xls","deudasporcobrar","E","B")%>
		</td>
	</tr>
	</table>
<%
if codigo_sco<>"-2" then
	if HayReg=false then%>
		<h5>No se han encontrado clientes que con deudas registradas en el sistema</h5>
	<%else
		ArrEncabezados=Array("Recibido","Fecha","Código","Cliente","Area/Escuela","Cargos","Abonos","Saldos")
		ArrCampos=Array("recibiocandado_deu","fecha","cod. resp.","cliente","carrera","cargos","abonos","diferencia")
		ArrCeldas=Array("15%", "15%","10%","30%","15%","10%","10%","10%")
	
		call ValoresExportacion("Deudas por Cobrar",ArrEncabezados,rsClientes,Arrcampos,ArrCeldas)
	%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Los participantes con letra azul han cancelado 
	todo el cargo asignado y a los de color rojo tienen deuda pendiente</h5>
	<table bgcolor="white" bordercolor="silver" border="1" cellpadding="3" style="width: 100%;border-collapse:collapse">
		<tr class="usatCeldaTitulo">
			<td align="center">#</td>
			<td align="center">Entregado</td>	
			<td align="center">Fecha</td>	
			<td align="center">Código</td>
			<td align="center">Cliente</td>
			<td align="center">Área/Escuela</td>
			<td align="center">Cargos</td>
			<td align="center">Abonos<br />
                (Efectivo + Notas de Credito)</td>
			<td align="center">Saldos</td>
		</tr>
		<%
		tc=0:ta=0:ts=0
		a=0:f=0
		Do while not rsClientes.EOF
			i=i+1
			tc=cdbl(tc) + cdbl(rsClientes("cargos"))
			ta=cdbl(ta) + cdbl(rsClientes("abonos"))
			ts=cdbl(ts) + cdbl(rsClientes("diferencia"))
			
			clase=""
			if cdbl(rsClientes("diferencia"))=0 then
				clase="class=azul"
				a=a+1
			else
				clase="class=rojo"
				f=f+1
			end if
		%>
		<tr <%=clase%> onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
			<td align="right"><%=i%>&nbsp;</td>
			
			<%
			    if rsClientes("recibiocandado_deu")=True then
			        %> <td>"SI"</td> 
			    <%else%>
			            <td><a href="entregaCandado.asp?codigo_Deu=<%=rsClientes("codigo_Deu")%>&estado=1&fechainicio=<%=fechainicio%>&fechafin=<%=fechafin%>&codigo_sco=<%=codigo_sco%>">
			             Entregar</a> </td>  
			    <%end if
			 %>
			
			
			<td><%=rsClientes("fecha")%>&nbsp;</td>
			<td><%=rsClientes("cod. resp.")%>&nbsp;</td>
			<td><%=rsClientes("cliente")%>&nbsp;</td>
			<td><%=rsClientes("carrera")%>&nbsp;</td>
			<td align="right"><%=formatnumber(trim(rsClientes("cargos")),2)%>&nbsp;</td>
			<td align="right"><%=formatnumber(trim(rsClientes("abonos")),2)%>&nbsp;</td>
			<td align="right"><%=formatnumber(trim(rsClientes("diferencia")),2)%>&nbsp;</td>
		</tr>
		<%rsClientes.movenext
		Loop
		'rsClientes.close
		'Set rsClientes=nothing
		%>
		<tr class="usatTablaInfo">
			<td colspan="6" style="text-align: right; font-weight: 700;">TOTAL</td>
			<td align="right" style="font-weight: 700"><%=formatnumber(tc,2)%>&nbsp;</td>
			<td align="right" style="font-weight: 700"><%=formatnumber(ta,2)%>&nbsp;</td>
			<td align="right" style="font-weight: 700"><%=formatnumber(ts,2)%>&nbsp;</td>		
		</tr>
	</table>
<p class="usattitulousuario">Resumen</p>
<table style="width: 50%" class="contornotabla">
	<tr>
		<td style="width: 90%">Participantes que han cancelado todo el cargo asignado</td>
		<td width="10%">: <%=a%></td>
	</tr>
	<tr>
		<td style="width: 90%">Participantes que les falta cancelar el cargo asignados</td>
		<td width="10%">: <%=f%>&nbsp;</td>
	</tr>
	<tr>
		<td style="width: 311px">&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
</table>
	<%end if
	end if
end if
%>
</body>
</html>