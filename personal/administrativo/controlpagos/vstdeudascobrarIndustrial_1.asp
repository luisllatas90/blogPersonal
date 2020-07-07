<!--#include file="../../../funciones.asp"-->
<%
codigo_usu=session("codigo_usu")
codigo_tfu=session("codigo_tfu")

edicion = request.querystring("edicion")

sel1=""
sel2=""


if edicion=1 then
    sel1="selected"
    sel2=""
end if

if edicion=2 then
    sel1=""
    sel2="selected"
end if



codigo_sco= 471
codigo_pes= 146


if edicion <> ""  and edicion <> 0 then
Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
			
			Set rsClientes=obj.Consultar("consultarPagosProgramasEspeciales_IngInd","FO",codigo_sco,edicion,codigo_pes)		
			
			if Not(rsClientes.BOF and rsClientes.EOF) then
				HayReg=true
			end if
		
	obj.CerrarConexion
set obj= Nothing

End if


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
	   
		pagina="../administrativo/controlpagos/vstdeudascobrarIndustrial.asp?codigo_sco=" + txtcodigo_sco.value  + "&edicion=" + cboEdicion.value  + "&codigo_pes=" + txtcodigo_pes.value  
		location.href="../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
	}

</script>
</head>

<body bgcolor="#EEEEEE">
<p class="usatTitulo">Control de pagos</p>
<table cellpadding="3" style="width: 100%" class="contornotabla">
	<tr>
	    <input type=hidden name="txtcodigo_sco" value = <%=codigo_sco%> >
		<input type=hidden name="txtcodigo_pes" value = <%=codigo_pes%> >
		
		<td class="etiqueta" width="15%">Edicion</td>
		<td colspan="5">
		    <select name="cboEdicion" id="cboEdicion" >
		           <option value="0"><-- Seleccione --></option>
		           <option value="1" <%=sel1%>> III Edicion</option>
		           <option value="2" <%=sel2%>> IV  Edicion</option>
		    </select>
		</td>
	</tr>
	<tr>
		<td colspan="6" align="Left">
		<input name="cmdBuscar" type="button" value="Consultar" class="buscar" onclick="BuscarDeudas()">
		<% call botonExportar("../../../","xls","deudasporcobrar","E","B")%>
		</td>
	</tr>
	</table>
<%
if edicion<>"" and  edicion<>0 then
	if HayReg=false then%>
		<h5>No se ha encontrado ningun registro para la consulta realizada</h5>
	<%else
		ArrEncabezados=Array("Fecha","Código","Cliente","ultimoPago", "Cargo","P.Efectivo","N.Abono","Devol.","Abono Total","Saldo","Estado","Plan")
		ArrCampos=Array("fecha","codigoUniver_alu","alumno","ultimoPago", "cargo","efectivo","notas","devol","totalAbonos","saldo_Deu","estadoActual_Alu","PlanPrograma")
		ArrCeldas=Array("15%","10%","30%","10%","30%","30%","30%","30%","30%","30%","10%","10%")
	
		call ValoresExportacion("Control de Pagos",ArrEncabezados,rsClientes,Arrcampos,ArrCeldas)
	%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Los participantes con letra azul han cancelado 
	todo el cargo asignado y a los de color rojo tienen deuda pendiente</h5>
	<table bgcolor="white" bordercolor="silver" border="1" cellpadding="3" style="width: 100%;border-collapse:collapse">
		<tr class="usatCeldaTitulo">
			<td align="center">#</td>
			<td align="center">Fecha</td>	
			<td align="center">Código</td>
			<td align="center">Cliente</td>
			<td align="center">Ult.Pago</td>
			<td align="center">Cargo</td>
			<td align="center">P.Efectivo</td>
			<td align="center">N.Abono</td>
			<td align="center">Devol.</td>
			<td align="center">Abono Total</td>
			<td align="center">Saldo</td>
			<td align="center">Estado</td>
			<td align="center">Plan Sist.</td>
		</tr>
		<%
		tc=0:te=0:ta=0:td=0:tt=0:ts=0
		a=0:f=0
		Do while not rsClientes.EOF
			i=i+1
			tc=cdbl(tc) + cdbl(rsClientes("cargo"))
			te= cdbl(te) + cdbl(rsClientes("efectivo"))
			ta=cdbl(ta) + cdbl(rsClientes("notas"))
			td= cdbl(td)  + cdbl(rsClientes("devol"))
			tt= cdbl(tt) + cdbl(rsClientes("totalAbonos"))
			ts=cdbl(ts) + cdbl(rsClientes("saldo_Deu"))
			
			clase=""
			if cdbl(rsClientes("saldo_Deu"))=0 then
				clase="class=azul"
				a=a+1
			else
				clase="class=rojo"
				f=f+1
			end if
		%>
		<tr <%=clase%> onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onclick="document.location.href='../../../librerianet/academico/adminestadocuenta.aspx?id=<%=rsClientes("codigo_Alu") %>&tipo=IIND&codigo_sco=<%=codigo_sco%>&codigo_pes=<%=codigo_pes%>&edicion=<%=edicion%>'">
			<td align="right"><%=i%>&nbsp;</td>
			<td><%=rsClientes("fecha")%>&nbsp;</td>
			<td><%=rsClientes("codigoUniver_alu")%>&nbsp;</td>
			<td><%=rsClientes("alumno")%>&nbsp;</td>
			<td><%=rsClientes("ultimoPago")%>&nbsp;</td>
			<td align="right"><%=formatnumber(trim(rsClientes("cargo")),2)%>&nbsp;</td>
			<td align="right"><%=formatnumber(trim(rsClientes("efectivo")),2)%>&nbsp;</td>
			<td align="right"><%=formatnumber(trim(rsClientes("notas")),2)%>&nbsp;</td>
			<td align="right"><%=formatnumber(trim(rsClientes("devol")),2)%>&nbsp;</td>
			<td align="right"><%=formatnumber(trim(rsClientes("totalAbonos")),2)%>&nbsp;</td>
			<td align="right"><%=formatnumber(trim(rsClientes("saldo_Deu")),2)%>&nbsp;</td>
			<td><%=rsClientes("estadoActual_Alu")%>&nbsp;</td>
			<td><%=rsClientes("PlanPrograma")%>&nbsp;</td>
			
		</tr>
		<%rsClientes.movenext
		Loop
		'rsClientes.close
		'Set rsClientes=nothing
		%>
		<tr class="usatTablaInfo">
			<td colspan="5" style="text-align: right; font-weight: 700;">TOTAL</td>
			<td align="right" style="font-weight: 700"><%=formatnumber(tc,2)%>&nbsp;</td>
			<td align="right" style="font-weight: 700"><%=formatnumber(te,2)%>&nbsp;</td>
			<td align="right" style="font-weight: 700"><%=formatnumber(ta,2)%>&nbsp;</td>
			<td align="right" style="font-weight: 700"><%=formatnumber(td,2)%>&nbsp;</td>
			<td align="right" style="font-weight: 700"><%=formatnumber(tt,2)%>&nbsp;</td>
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

%>
</body>
</html>