<%
codigo_cup=request.querystring("codigo_cup")
if codigo_cup="" then codigo_cup=0

Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsHistorial=Obj.Consultar("ConsultarNotas","FO","BI",codigo_cup,0,0)
	obj.CerrarConexion
Set obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Bitácora de Registro de Notas</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" />
</head>
<body>
<p class="usatTitulo">Bitácora de registro de notas</p>
<%if (rsHistorial.BOf and rsHistorial.EOF) then%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp; No se ha registrado bitácora 
	de registro de notas</h5>
<%else%>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="silver" width="100%">
	<tr class="etabla">
		<td style="width: 15%">Acción</td>
		<td style="width: 20%">Fecha Reg.</td>
		<td style="width: 30%">Operador</td>
		<td style="width: 30%">Observación</td>		
	</tr>
	<%do while not rsHistorial.EOF
		if isnull(rsHistorial("fecha_fin"))=true then
	%>
	<tr>
		<td style="width: 20%" class="rojo"><%=rsHistorial("accion")%>&nbsp;</td>
		<td style="width: 20%"><%=rsHistorial("fecha_ini")%>&nbsp;</td>
		<td style="width: 30%"><%=rsHistorial("operador_bin")%>&nbsp;</td>
		<td style="width: 30%">&nbsp;</td>
	</tr>
	<tr>
		<td colspan="4">
		<blockquote>
		<%=rsHistorial("motivo_bin")%>
		</blockquote>
		</td>
	</tr>		
	<%else%>
	<tr>
		<td style="width: 15%" class="rojo"><%=rsHistorial("accion")%>&nbsp;</td>		
		<td style="width: 20%"><%=rsHistorial("fecha_ini")%>&nbsp;</td>
		<td style="width: 30%"><%=rsHistorial("operador_bin")%>&nbsp;</td>
		<td style="width: 30%"><%=rsHistorial("motivo_bin")%>&nbsp;</td>
	</tr>
	<%end if
	rsHistorial.movenext
	Loop
	set rsHistorial=nothing
	%>
</table>
<%end if%>
</body>
</html>
