<%
usuarioaut_acr=request.querystring("usuarioaut_acr")
nombretbl_acr=request.querystring("nombretbl_acr")
modo=1
if nombretbl_acr="ambiente" then modo=2
if nombretbl_acr="servicioconcepto" then modo=5
if nombretbl_acr="centrocostos" then modo=8
if nombretbl_acr="departamentoacademico" then modo=10
'HCANO 05-01-2017
if nombretbl_acr="Programa/Proyecto" then modo=12
'Falta Colocar el Ejercicio Presupuestaall por el momento esta fijo en la consulta
'FIN HCANO
Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsRecurso=obj.Consultar("ConsultarAccesoRecurso","FO",modo,usuarioaut_acr,0,0)
	obj.CerrarConexion
Set Obj=nothing

%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de recursos</title>
<link rel="stylesheet" type="text/css" href="../../private/estilo.css">
<script language="JavaScript" src="../../private/funciones.js"></script>
<script language="JavaScript">

function Marcar(obj)
{
	if (obj.checked==true){
		SeleccionarTodos(document.all.rec)
	}
	else{
		QuitarTodos(document.all.rec)
	}
}

</script>


</head>

<body>
<form name="frmrecursos" method="post" action="procesar.asp?Accion=agregarpermisorecurso&usuarioaut_acr=<%=usuarioaut_acr%>&nombretbl_acr=<%=nombretbl_acr%>">
<table width="100%" cellpadding="0">
	<%Do while Not rsRecurso.EOF
		i=i+1
	%>
	<tr>
		<td style="width: 20px">
		<input id = "rec" name="chk<%=i%>" type="checkbox" <%=rsRecurso("Marca")%>>
		<input name="txtcodigotbl_acr<%=i%>" type="hidden" value="<%=rsRecurso(0)%>" />
		</td>
		<td><%=rsRecurso(1)%></td>
	</tr>
	<%
		rsRecurso.movenext
	Loop

	Set rsRecurso=nothing
	%>
</table>
<input name="nocheck" type="hidden" value="<%=i%>">

<input name="chkTodos" type="checkbox" onClick="Marcar(this)">--MARCAR TODOS LOS ITEMS--
</form>
</body>

</html>
