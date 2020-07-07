<!--#include file="../../../funciones.asp"-->
<%
modo=request.querystring("modo")
codigo_cac=request.querystring("codigo_cac")
codigo_amb=request.querystring("codigo_amb")
codigo_cup=request.querystring("codigo_cup")
codigo_per=request.querystring("codigo_per")
codigo_cpf=request.querystring("codigo_cpf")
dia=request.querystring("dia")

codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")

Select case modo
	case "CU"
		modo=2
		param1=codigo_cup
		param2=codigo_per
		titulo="Horario registrado por Curso [día " & ucase(Convdia(dia)) & "]"
	case "AU"
		modo=3
		param1=codigo_amb
		param2=codigo_cac
		titulo="Horario registrado por Ambiente [día " & ucase(Convdia(dia)) & "]"
	case "PR"
		modo=4
		param1=codigo_per
		param2=codigo_cac
		titulo="Horario registrado por Profesor [día " & ucase(Convdia(dia)) & "]"
End select

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsHorario=obj.Consultar("ConsultarHorariosAmbiente","FO",modo,param1,param2,dia,codigo_usu)
		
		set rsPermisos=obj.Consultar("ValidarPermisoAccionesEnProcesoMatricula","FO","0",codigo_cac,session("codigo_usu"),"lineahorario")
		
		if not(rsPermisos.BOF and rsPermisos.EOF) then
			Agregar=cbool(rsPermisos("agregar_acr"))
			Modificar=cbool(rsPermisos("modificar_acr"))
			Eliminar=cbool(rsPermisos("eliminar_acr"))
		end if			
				
		'Asignar permisos a Evaluación y Registros, Administrador,Programas especiales y si el ciclo >ciclo actual
		if (session("codigo_tfu")=1 OR codigo_cpf=25) or int(codigo_cac)>int(session("codigo_cac")) then
			Agregar=true
			Modificar=true
			Eliminar=true
			'response.write("Sin Permisos")
		end if
	obj.CerrarConexion
Set obj=nothing

	if Not(rsHorario.BOF and rsHorario.EOF) then
		HayReg=true
	end if
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es" >
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<title>Horario</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
	function EliminarHorario(codigo_lho)
	{
		if (confirm("¿Está seguro que desea eliminar el horario seleccionado?")==true){
			location.href="procesar.asp?accion=eliminarhorario&codigo_lho=" + codigo_lho
		}
	}	
</script>
</head>
<body>
<%if HayReg=true then%>
<p class="usattitulo"><%=titulo%></p>

<table width="100%" border="1" bordercolor="gray" cellpadding="3" style="border-collapse: collapse;">
	<tr class="etabla">
		<td>Eliminar</td>
		<td>Horas</td>
		<td>Asignatura</td>
		<td>Ciclo</td>
		<td>Escuela Profesional</td>
		<td>Ambiente</td>
		<td>Editar</td>
	</tr>
	<%Do while not rsHorario.EOF%>
	<tr onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)">
		<td align="center" class="rojo">
		
		
		<%if (Eliminar=true) and (cint(rsHorario("codigo_cpf")) = cint(codigo_cpf)) then%>
		<img alt="Eliminar horario de la escuela profesional" src="../../../images/Eliminar.gif" class="imagen" onClick="EliminarHorario('<%=rsHorario("codigo_lho")%>')">
		
		<%else%>
			[Bloqueado para eliminar horarios]
		
		<%end if%>
		
		
		</td>
		<td>
		INICIO:<%=rsHorario("nombre_hor")%><br>
		FIN: <%=rsHorario("horafin_lho")%>
		</td>
		<td><%=rsHorario("nombre_cur")%><br>
		<em><b>Duración:</b> <%=rsHorario("fechainicio_cup")%> - <%=rsHorario("fechafin_cup")%></em><br>
		<em><b>Profesor:</b> <%=rsHorario("docente")%></em>

		</td>
		<td><%=rsHorario("ciclo_cur")%>&nbsp;</td>		
		<td><%=rsHorario("nombre_cpf")%>&nbsp;</td>		
		<td class="rojo"><%=rsHorario("ambiente")%>&nbsp;</td>
		
	</tr>
		<%rsHorario.movenext
	Loop
	set horario=nothing
	%>
</table>
<%else%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp; No se encontró horario registrado, según el criterio seleccionado</h5>
<%end if%>
</body>

</html>
