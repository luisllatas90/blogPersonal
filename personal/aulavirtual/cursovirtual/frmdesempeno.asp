<%
if session("tipofuncion")=3 then
	response.redirect "detallesituacion.asp?idusuario=" & session("codigo_usu")
else

dim total_men
if session("codigo_usu")="" then response.redirect "../tiempofinalizado.asp"

	Set ObjUsuario= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		objUsuario.AbrirConexion
		set rsAccesos=ObjUsuario.consultar("ConsultarCursoVirtual","FO","9",session("idcursovirtual"),0,0)
		objUsuario.CerrarConexion
	Set ObjUsuario=nothing
	
Sub ImprimirMenu(byVal icono,byval texto,ByVal enlace)
	dim vinculo

		vinculo="window.location.href='" & enlace & "'"
		
		response.write "<table class='Menu' width='100%' border='0' align='center' cellpadding='4' cellspacing='0' onMouseOver=""ResaltarMenuElegido(1,this)"" onMouseOut=""ResaltarMenuElegido(0,this)"" onClick=""" & vinculo & """>" & vbcrlf & vbtab & vbtab
		response.write "<tr>" & vbcrlf & vbtab & vbtab
		response.write "<td height='60' width='5%' rowspan='2'><img src='../../../images/menus/" & icono & "'></td>" & vbcrlf & vbtab & vbtab
		response.write "<td height='30'>" & texto & "</td>" & vbcrlf & vbtab & vbtab
		response.write "</tr>" & vbcrlf & vbtab & vbtab
		response.write "</table>" & vbcrlf & vbtab & vbtab
end Sub
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Curso virtual: <%=titulocurso%></title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<style type="text/css">
<!--
.Menu {
	border-style: solid;
	border-width: 0px;
	border-color: #96965E;
	font-weight: bold;
	font-size: 12pt;
	font-family:Arial Narrow;
}
.menuElegido {
	border-style: solid;
	border-width: 2px;
	border-color: #96965E;
	background-color: #EBE1BF;
	cursor:hand;
	font-weight: bold;
}
.menuNoElegido {
	border-style: solid;
	border-width: 0px;
	border-color: #96965E;
	background-color: #FFFFFF;
	font-weight: bold;
	font-size: 12pt;
}
.MenuDescripcion {
	font-size: xx-small;
	font-weight:normal
}
-->
</style>
<script language="javascript">
function ResaltarMenuElegido(op,fila)
{
	if(op==1)
		{fila.className="menuElegido"}
	else
		{fila.className="menuNoElegido"}
}
</script>
</head>
<body>
<p class="e4"><u>Menú de opciones</u></p>
<table style="border-style:none; width=60%" height="50%" cellspacing="4" cellpadding="4" align="center">
	<%
	codigo_apl=-1
	i=0
	total=rsAccesos.recordcount
	
	Do while not rsAccesos.EOF
		
		'Salir del bucle sin terminaron los registros
		if (rsAccesos.EOF) then
			exit do
		end if

		if (i mod 2 = 0) then
	%>
	<tr>
		<td style="height: 25%; width: 20%;">
		<%
			ImprimirMenu rsAccesos("icono_mper"),rsAccesos("descripcion_mper"),rsAccesos("enlace_mper")
		%>	
		</td>
		<td style="height: 25%; width: 5%;">&nbsp;</td>
		<%else%>
		<td style="height: 25%; width: 20%;">
		<%
			ImprimirMenu rsAccesos("icono_mper"),rsAccesos("descripcion_mper"),rsAccesos("enlace_mper")
		%>
		</td>
	</tr>
		<%end if
		i=i+1
		rsAccesos.movenext
	loop
	Set accesos=nothing
	%>
</table>
</body>
</html>
<%end if%>