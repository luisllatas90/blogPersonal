<!--#include file="../../../../funciones.asp"-->
<%
criterio=request.querystring("criterio")

if criterio<>"" then
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsCursos=Obj.Consultar("ConsultarPedidoBibliografico","FO",4,0,0,ReemplazarTildes(criterio))
		Obj.CerrarConexion
	Set obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<title>Buscar Cursos</title>
<script>
function EnviarNombre()
{
	parent.document.all.frmPedido.txtCurso.value=cbocodigo_cur.options[cbocodigo_cur.selectedIndex].text
	parent.document.all.frmPedido.txtCodigo_cur.value=cbocodigo_cur.value
}
</script>
</head>
<body style="margin: 0">
<%
	if Not(rsCursos.BOF and rscursos.EOF) then	
		call llenarlista("cbocodigo_cur","EnviarNombre()",rsCursos,"codigo_cur","nombre_cur","","","","multiple")
		%>
		<script>cbocodigo_cur.style.height="100%"</script>
	<%else
		response.write "<h5>No se han encontrado asignaturas con el criterio que Ud. especifica</h5>"
	end if
	
	set rsCursos=nothing
%>
<script language="javascript">
	parent.document.all.fraCurso.style.display=""
	parent.document.all.lblmensajecurso.style.display="none"
</script>
</body>
</html>
<%end if%>