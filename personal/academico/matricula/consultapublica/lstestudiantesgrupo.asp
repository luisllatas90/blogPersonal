<%
codigo_cup=request.querystring("codigo_cup")
estado_dma=request.querystring("estado_dma")
nombre_cur=request.querystring("nombre_cur")

select case estado_dma
	case "P":
		titulo="Listado de estudiantes pre matriculados en asignatura de " & nombre_cur
	case "M"
		titulo="Listado de estudiantes matriculados en asignaturas de " & nombre_cur
end select

Set obj = Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	set rsAlumnos=Obj.Consultar("ConsultarMatriculaXGrupoHorario","FO",11,codigo_cup,estado_dma,0)
	obj.CerrarConexion
Set obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Listado de estudiantes</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
</head>
<body>
<p class="usatTitulo"><%=titulo%></p>
<input name="cmdregresar" class="regresar2" type="button" value="Regresar" class="usatSalir" onclick="history.back(-1)">
<input name="cmdregresar" class="imprimir2" type="button" value="Imprimir" onclick="imprimir('N','','<%=titulo%>')">
<br>
<br>
<table width="100%" border="1" bordercolor="gray" style="border-collapse: collapse" cellpadding="3" cellspacing="0">
	<tr class="etabla" height="20px">
		<td>Nº</td>
		<td>Fecha Registro</td>
		<td>Código</td>
		<td>Apellidos y Nombres</td>
		<td>Ciclo de Ingreso</td>
				
		<td>Escuela/Plan de Estudio</td>
<td>Estuvo Suspendido</td>
		<td>Email principal</td>
		<td>Email secundario</td>
	</tr>
	<%Do while not rsAlumnos.EOF
		i=i+1
	%>
	<tr>
		<td><%=I%></td>
		<td><%=rsAlumnos("fechareg_dma")%></td>
		<td><%=rsAlumnos("codigouniver_alu")%></td>
		<td><%=rsAlumnos("alumno")%>&nbsp;</td>
		<td><%=rsAlumnos("cicloIng_alu")%>&nbsp;</td>
		<td><%=rsAlumnos("descripcion_pes")%></td>
		<td><%=rsAlumnos("susp")%></td>
		<td><%=rsAlumnos("email_alu")%></td>
		<td><%=rsAlumnos("email2_alu")%></td>
	</tr>
	<%rsAlumnos.movenext
	Loop
	set rsAlumnos=nothing
	%>
</table>

</body>

</html>
