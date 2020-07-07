<!--#include file="../../../../funciones.asp"-->
<%
total=0
codigo_aam=request.querystring("codigo_aam")
codigo_cac=request.querystring("codigo_cac")
codigo_amb=request.querystring("codigo_amb")
codigo_cpf=request.querystring("codigo_cpf")

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsFechas=Obj.Consultar("Consultarhorariosambiente","FO",14,codigo_aam,0,0,0)
	obj.CerrarConexion
	
	if not(rsFechas.BOF and rsFechas.EOF) then
		total=rsFechas.recordcount
	end if
set Obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Especificar fechas y horas de asignación</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/calendario.js"></script>
<script type="text/javascript" language="JavaScript">
	function LlenarFin()
	{
		var inicio=eval(frmDetalle.horainicio.value)
		var items=eval(frmDetalle.horafin.length)

		//Quitar nuevos items
		for(var i=0;i<items;i++){
			frmDetalle.horafin[0]=null
		}

		//Agregar nuevos items
		for(var i=inicio;i<=21;i++){
			frmDetalle.horafin[frmDetalle.horafin.length] = new Option(i+1,i+1);
		}
	}
	

	function GuardarDatos()
	{
	
		if (frmDetalle.fechainicio.value==""){
			alert("Especifique la fecha de inicio de asignación de ambiente")
			return(false)
			frmDetalle.fechainicio.focus()
		}
		
			if (frmDetalle.fechainicio.value==""){
			alert("Especifique la fecha de inicio de asignación de ambiente")
			return(false)
			frmDetalle.fechafin.focus()
		}

		frmDetalle.submit()
	}
	
	function QuitarFecha(codigo_daa)
	{
		location.href="../procesar.asp?Accion=QuitarFechaAsignacion&codigo_daa=" + codigo_daa;
	}
</script>
</head>

<body bgcolor="#F0F0F0" onload="LlenarFin()">
<form name="frmDetalle" method="post" action="../procesar.asp?Accion=AgregarDetalleAsignacion&codigo_aam=<%=codigo_aam%>&codigo_amb=<%=codigo_amb%>&codigo_cac=<%=codigo_cac%>&codigo_cpf=<%=codigo_cpf%>">
<table width="100%" class="contornotabla" >
  <tr style="zindex:-1">
    <td class="etiqueta" colspan="2">Especificar nuevos datos de asignación de 
	ambiente:</td>
  </tr>
  <tr style="zindex:-1">
    <td  width="50">Desde</td>
    <td>
    <select  name="horainicio" onchange="LlenarFin()">
    <%for i=7 to 21%>
		<option value="<%=i%>"><%=i%></option>
	<%next%>
    </select> <select  name="mininicio">
    <%for i=0 to 50 step 10%>
		<option value="<%=iif(len(i)=1,"0" & i,i)%>"><%=iif(len(i)=1,"0" & i,i)%></option>
	<%next%>
    </select><input type="text" name="fechainicio" size="12" class="Cajas" readonly value="<%=fechainicio%>"><input type="button" class="cunia" onClick="MostrarCalendario('fechainicio')"></td>
  </tr>
  <tr>
    <td  width="50">Hasta</td>
    <td>
    <select  name="horafin">
   
    </select> <select  name="minfin">
    <%for i=0 to 50 step 10%>
		<option value="<%=iif(len(i)=1,"0" & i,i)%>"><%=iif(len(i)=1,"0" & i,i)%></option>
	<%next%>
    </select><input type="text" name="fechafin" size="12" class="Cajas" readonly value="<%=fechafin%>"><input type="button" class="cunia" onClick="MostrarCalendario('fechafin')">
    </td>
  </tr>
  <tr>
    <td  width="50">&nbsp;</td>
    <td>
    <input onclick="GuardarDatos()" name="cmdGuardar" type="button" value="Guardar" class="guardar2"></td>
  </tr>
</table>
</form>
<p class="etiqueta">Fechas de asignación registradas</p>
<table bgcolor="white" width="100%" cellpadding="3" style="border-collapse: collapse" bordercolor="gray" border="1">
	<tr class="etabla">
		<td>&nbsp;</td>
		<td>Fecha de inicio</td>
		<td>Fecha fin</td>
	</tr>
	<%Do while not rsFechas.EOF%>
	<tr>
		<td>
		<%if total>=1 then%>
		<img alt="Quitar fechas" class="imagen" src="../../../../images/eliminar.gif" onclick="QuitarFecha('<%=rsFechas("codigo_daa")%>')">
		<%end if%>
		</td>
		<td><%=rsFechas(0)%>&nbsp;</td>
		<td><%=rsFechas(1)%>&nbsp;</td>
	</tr>
	<%
		rsFechas.movenext
	Loop
	
	Set rsFechas=nothing
	%>
</table>

</body>

</html>