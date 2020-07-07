<!--#include file="../../../funciones.asp"-->
<%
idtabla=request.querystring("idtabla")

if idtabla<>"" then

Set Obj= Server.CreateObject("aulavirtual.clsAccesoDatos")
Obj.AbrirConexion
	Set rsDatos=Obj.Consultar("ConsultarParametrosArchivo","FO",idtabla)
Obj.CerrarConexion
Set Obj=nothing

	if Not (rsDatos.BOF and rsDatos.EOF) then
		HayReg=true	
	end if
end if
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Mover documentos</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script type="text/javascript" language="javascript">
	function ConsultarTablas()
	{
		if (cbotabla.value==""){
			alert("Debe elegir una tabla de consulta")
			return(false)
		}
		location.href="frmmoverarchivo.asp?idtabla=" + cbotabla.value
	}
</script>

</head>

<body bgcolor="#EEEEEE">

<p class="usatTitulo">Mantenimiento de Maestros</p>
<table cellpadding="3" style="width: 100%" class="contornotabla">
	<tr>
		<td>TABLA</td>
		<td><select name="cbotabla">
		<option>>>Selecciona la tabla<<</option>
		<option value="5" <%=SeleccionarItem("cbo",idtabla,5)%>>Procedencia</option>
		<option value="6" <%=SeleccionarItem("cbo",idtabla,6)%>>Destinatario</option>
		<option value="7" <%=SeleccionarItem("cbo",idtabla,7)%>>Áreas Orígen</option>
		<option value="8" <%=SeleccionarItem("cbo",idtabla,8)%>>Áreas Destino</option>		
		</select></td>
		<td>
		<img class="imagen" alt="Buscar" src="../../../images/buscar.gif" onclick="ConsultarTablas()">
		</td>
	</tr>
	<%if HayReg=true then%>	
	<tr>
		<td>Orígen</td>
		<td>
		<%		
		llenarlista "cboOrigen","",rsDatos,0,1,"","Seleccione la TABLA ORÍGEN","",""
		%>
		</td>
		<td>&nbsp;</td>
	</tr>
	<tr>
		<td>Destino</td>
		<td>
		<%
		rsDatos.movefirst
		llenarlista "cboOrigen","",rsDatos,0,1,"","Seleccione la TABLA DESTINO","",""
		%>
		</td>

		<td>&nbsp;</td>
	</tr>
	<tr>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	</table>
	<%end if%>	
	<p align="center">
	<input type="button" name="cmdMover" value="    Mover documentos" class="conforme1">
	</p>
</body>
</html>
