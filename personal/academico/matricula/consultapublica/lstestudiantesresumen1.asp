<%
codigo_cpf=request.querystring("codigo_cpf")
codigo_cac=request.querystring("codigo_cac")
tipo=request.querystring("tipo")
nombre_cpf=request.querystring("nombre_cpf")

select case tipo
	case "PE":
		titulo="pre matriculados en asignaturas de escuela"
		modo=8
		estado="P"
	case "PC":
		titulo="pre matriculados sólo en asignaturas complementarias"
		modo=9
		estado="P"
	case "ME"
		titulo="matriculados en asignaturas de escuela"
		modo=8
		estado="M"
	case "MC"
		titulo="matriculados sólo en asignaturas complementarias"
		modo=9
		estado="M"
end select

Set obj = Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	set rsAlumnos=Obj.Consultar("ConsultarAlumnosMatriculados","FO",modo,codigo_cac,codigo_cpf,estado,0)
	obj.CerrarConexion
Set obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Listado de estudiantes</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
</head>

<body>

<p class="usatTitulo">Listado de estudiantes de la Escuela Profesional de <%=nombre_cpf%><br><%=titulo%></p>

<input name="cmdregresar" class="regresar2" type="button" value="Regresar" class="usatSalir" onclick="location.href='totmatriculaciclo.asp?codigo_cac=<%=codigo_cac%>'">
<br>
<br>
<table width="100%" border="1" bordercolor="gray" style="border-collapse: collapse" cellpadding="3" cellspacing="0">
	<tr class="etabla" height="20px">
		<td>Nº</td>
		<td>Código</td>
		<td>Apellidos y Nombres</td>
		<td>Ciclo de Ingreso</td>
		<td>Modalidad de Ingreso</td>		
		<td>Plan de Estudio</td>		
	</tr>
	<%Do while not rsAlumnos.EOF
		i=i+1
	%>
	<tr>
		<td><%=I%>&nbsp;</td>
		<td><%=rsAlumnos("codigouniver_alu")%>&nbsp;</td>
		<td><%=rsAlumnos("alumno")%>&nbsp;</td>
		<td><%=rsAlumnos("cicloIng_alu")%>&nbsp;</td>
		<td><%=rsAlumnos("nombre_min")%>&nbsp;</td>		
		<td><%=rsAlumnos("descripcion_pes")%>&nbsp;</td>		
	</tr>
	<%rsAlumnos.movenext
	Loop
	set rsAlumnos=nothing
	%>
</table>

</body>

</html>