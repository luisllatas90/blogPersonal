<!--#include file="../../../../funciones.asp"-->
<%
if(session("codigo_usu") = "") then
    Response.Redirect("../../../../sinacceso.html")
end if

codigo_cac=request.querystring("codigo_cac")
codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	
	Set rsCiclo= Obj.Consultar("ConsultarCicloAcademico","FO","TO",0)

	if codigo_cac<>"-2" and codigo_cpf<>"-2" then
		Set rsAmbiente= Obj.Consultar("ConsultarHorariosAmbiente","FO",8,codigo_cac,codigo_tfu,codigo_usu,0)

		if Not(rsAmbiente.BOF and rsAmbiente.EOF) then
			activo=true
			alto="height=""97%"""
		end if
	end if
    obj.CerrarConexion
Set obj=nothing

'oncontextmenu="return false"
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Asignación de ambientes por carrera profesional</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="Javascript">
var contador=0
	function BuscarAsignacion()
	{
		location.href="frmasignarambiente.asp?codigo_cac=" + cbocodigo_cac.value
	}

	function PintarAmbiente(celda)
	{
		AnteriorFila.Typ = "Sel";
		AnteriorFila.className = AnteriorFila.Typ + "Off";
		AnteriorFila = celda;
	
		if (celda.Typ == "Sel"){
			celda.Typ ="Selected";
			celda.className = celda.Typ;
		}
		
		fradetalle.location.href="lstambientesescuela.asp?codigo_amb=" + celda.codigo_amb + "&codigo_cac=" + cbocodigo_cac.value
	}
	
	function GuardarAsignacion()
	{
		fradetalle.document.all.frmpermisos.submit()
		alert("Se ha guardado correctamente")
		cmdGuardar.disabled=true
	}
</script>
<style type="text/css">
.OES {
	border: 1px solid #808080;
	background-color: #FF9933;
}
.OTE {
	border: 2px solid #808080;
	background-color: #99FFCC;
}
</style>
</head>
<body bgcolor="#F0F0F0">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
<tr>
	<td height="3%" colspan="4" class="usattitulo">Asignación de ambientes para 
	registro de horarios</td>
</tr>
<tr>
	<td height="3%" style="width: 20%">Semestre Académico:</td>
	<td height="3%" style="width: 20%">
	<%call llenarlista("cbocodigo_cac","BuscarAsignacion()",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"Seleccione el semestre académico","","")%>	
	</td>
	<td height="3%" style="width: 90%" class="azul">Carreras profesionales asignadas al 
	ambiente seleccionado</td>
	<td height="3%" style="width: 10%" align="right">	
	<%'if session("codigo_tfu")=25 or session("codigo_tfu")=18 then %>
	<!--
	<input name="cmdGuardar" type="button" value="Guardar" disabled="true" class="guardar2" onclick="GuardarAsignacion()">
	-->
	<%'else%>
	
	<%'end if%>
	</td>
</tr>
<%if activo=true then%>
<tr>
	<td height="97%" style="width: 40%" colspan="2">
	<div id="listadiv" style="width:100%; height:100%" class="contornotabla_azul">
<table width="100%" class="piepagina" cellpadding="3" cellspacing="3" bgcolor="white">
	<%
	Marcas=0
	OtrasMarcas=0
	i=1
	Do while not rsAmbiente.EOF
	
		i=i+1
		if rsAmbiente("codigo_amb")<>32 then
		if (i mod 2= 0) then
			response.write "</tr><tr>"
		elseif i=1 then
			response.write "<tr>"
		end if
		
		if rsAmbiente("Marca")="fr.gif" then
			Marcas=Marcas+1
		elseif rsAmbiente("Marca")="fr.gif" then
			OtrasMarcas=OtrasMarcas+1
		end if
		%>
		<td class="Sel" Typ="Sel"  style="cursor:hand;border: 1px solid #808080" align="center" onclick="PintarAmbiente(this)" id="Ambientes"  codigo_amb="<%=rsAmbiente("codigo_amb")%>" >
		    <table width="100%">
		    <tr>
		    <td align="center"  >
				    <%=rsAmbiente("ambiente") %><br/>
				    <% response.Write(" [<font size='1pt' color='grey'>" & rsAmbiente("ambienteReal") & "</font>]")%><br/>
				    (<%=rsAmbiente("capacidad_amb")%>)
		    </td>
		    <td align="center"  width="10px"  >
				    <%if rsAmbiente("Marca")="fr.gif" then%>
				    <br/><img src="../../../../images/<%=rsAmbiente("Marca")%>" alt="Ambiente asignada"/>
				    <%end if%>
		    </td>
		    </tr>
		    </table>
		</td>
		<%
		else
		    i=i-1
		end if
		rsAmbiente.movenext
		Loop
	Set rsAmbiente=nothing
	%>
</table>
</div>

	
	</td>
	<td height="97%" style="width: 60%" colspan="2">
	<iframe name="fradetalle" class="contornotabla_azul" id="fradetalle" height="100%" width="100%" border="0" frameborder="0">no soporta frames.</iframe>

	</td>
</tr>

<%end if%>
</table>
</body>
</html>