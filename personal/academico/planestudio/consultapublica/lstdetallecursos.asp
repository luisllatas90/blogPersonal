<%
modo=request.querystring("modo")
codigo_cur=request.querystring("codigo_cur")
codigo_pes=request.querystring("codigo_pes")

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	
		if modo="R" then
			Set rsCursos=Obj.Consultar("ConsultarCursoPlan","FO","RQ",codigo_pes,codigo_cur,0)
		else
			Set rsCursos=Obj.Consultar("ConsultarCursoEquivalente","FO","CE",codigo_pes,codigo_cur,0)
		end if
			
		if Not(rscursos.BOF and rsCursos.EOF) then
			HayReg=true
			alto="height=""93%"""
		end if
	obj.CerrarConexion
Set Obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Lista de detalle de cursos</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
</head>
<body bgcolor="#EEEEEE">
<%if modo="R" then%>
<table style="border-collapse: collapse" cellpadding="3" width="100%" bgcolor="white">
	<tr class="etabla">
		<td>Tipo de Requisito</td>
		<td>Requisito de Creditaje</td>
		<td>Código</td>
		<td>Asignatura</td>
		<td>Estado</td>
	</tr>
	<%Do while Not rsCursos.EOF%>
	<tr>
		<td><%=rsCursos(1)%>&nbsp;</td>
		<td><%=rsCursos(2)%>&nbsp;</td>
		<td><%=rsCursos(3)%>&nbsp;</td>
		<td><%=rsCursos(4)%>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<%
		rsCursos.movenext
	Loop
	%>
</table>
<%else%>
<table style="border-collapse: collapse" cellpadding="3" width="100%" bgcolor="white">
	<tr class="etabla">
		<td>Nº</td>
		<td>Asignatura</td>
		<td>Estado</td>
	</tr>
	<%Do while Not rsCursos.EOF
		i=i+1
	%>
	<tr>
		<td><%=i%>&nbsp;</td>
		<td><%=rsCursos("nombre_curE")%>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
	<%
		rsCursos.movenext
	Loop
	%>
</table>
<%end if

Set rsCursos=nothing
%>
</body>
</html>
