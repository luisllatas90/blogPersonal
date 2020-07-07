<%
codigo_cac=request.querystring("codigo_cac")
codigo_amb=request.querystring("codigo_amb")
ambiente=request.querystring("ambiente")

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsAmbiente= Obj.Consultar("ConsultarHorariosAmbiente","FO",9,codigo_cac,codigo_amb,0,0)
	obj.CerrarConexion
Set obj=nothing

'oncontextmenu="return false"
%>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Ambientes por escuela</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script type="text/javascript" language="javascript">
	function QuitarEscuela(id)
	{
		if (confirm("¿Está seguro que desea quitar la asignación de la escuela?")==true){
			location.href="procesar.asp?Accion=EliminarAsignacionAmbiente&codigo_cac=<%=codigo_cac%>&codigo_amb=<%=codigo_amb%>&codigo_cpf=" + id + "&ambiente=<%=ambiente%>"
		}
	
	}
	
	function AgregarEnAmbiente()
	{
		location.href="procesar.asp?Accion=AgregarEnAmbiente&codigo_cac=<%=codigo_cac%>&codigo_amb=<%=codigo_amb%>&codigo_cpf=" + parent.document.all.cbocodigo_cpf.value + "&ambiente=<%=ambiente%>"
	}
</script>
</head>

<body>

<p class="usattitulousuario">AMBIENTE: <%=ambiente%></p>
<table bordercolor="gray"  border="1" cellpadding="3" style="width: 100%;border-collapse: collapse">
	<tr class="etabla">
		<td>#</td>
		<td>Escuela Profesional</td>
		<td>Quitar</td>
	</tr>
	<%Do while not rsAmbiente.EOF
		i=i+1
	%>
	<tr>
		<td><%=i%>&nbsp;</td>
		<td><%=rsAmbiente("nombre_cpf")%>&nbsp;</td>
		<td align="center"><img class="imagen" src="../../../images/eliminar.gif" onclick="QuitarEscuela('<%=rsAmbiente("codigo_cpf")%>')"></td>
	</tr>
	<%
		rsAmbiente.movenext
	Loop
	
	Set rsAmbiente=nothing
	%>
</table>

<p align="right" class="rojo" style="cursor:hand"  onclick="AgregarEnAmbiente()">[Agregar en este ambiente a la escuela seleccionada]</p>

</body>

</html>
