<!--#include file="../../../../funciones.asp"-->
<%
codigo_cac=request.querystring("codigo_cac")
codigo_amb=request.querystring("codigo_amb")
dia=request.querystring("dia")
codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCursos= Obj.Consultar("ConsultarHorariosAmbiente","FO",12,codigo_cac,codigo_amb,dia,codigo_usu)
	obj.CerrarConexion
Set obj=nothing

'oncontextmenu="return false"
%>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Lista de asignaturas por ambiente</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="javascript">
	function QuitarHorario(id)
	{
		if (confirm("¿Está seguro que desea quitar la asignación de la escuela?")==true){
			location.href="../procesar.asp?Accion=EliminarAsignacionAmbiente&codigo_cac=<%=codigo_cac%>&codigo_amb=<%=codigo_amb%>&codigo_cpf=" + id + "&ambiente=<%=ambiente%>"
		}
	
	}
</script>
</head>

<body>

<p class="usattitulousuario">Horario para el día [ <%=ucase(ConvDia(rsCursos("dia_lho")))%> ] en <%=rsCursos("ambiente")%></p>
<table bordercolor="gray"  border="1" cellpadding="3" style="width: 100%;border-collapse: collapse">
	<tr class="etabla">
		<td>#</td>
		<td>Inicio-Fin</td>		
		<td>Ciclo</td>
		<td>Asignatura</td>
		<td>Escuela Profesional</td>
		<!--<td>Quitar</td>-->
	</tr>
	<%Do while not rsCursos.EOF
		i=i+1
	%>
	<tr>
		<td><%=i%>&nbsp;</td>
		<td><%=rsCursos("nombre_hor") & " - " & rsCursos("horafin_lho") %>&nbsp;</td>		
		<td><%=rsCursos("ciclo_cur")%></td>
		<td><%=rsCursos("nombre_cur")%><br>
		<em><b>Duración:</b> <%=rsCursos("fechainicio_cup")%> - <%=rsCursos("fechafin_cup")%></em><br>
		<em><b>Profesor:</b> <%=rsCursos("docente")%></em>		
		</td>
		<td><%=rsCursos("nombre_cpf")%>&nbsp;</td>
		<!--
		<td align="center">
		<img class="imagen" src="../../../../images/eliminar.gif" onclick="QuitarHorario('<%=rscursos("codigo_lho")%>')">
		</td>
		-->
	</tr>
	<%
		rsCursos.movenext
	Loop
	
	Set rsCursos=nothing
	%>
</table>

</body>

</html>
