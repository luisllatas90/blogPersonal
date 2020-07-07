<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Todos los documentos</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estiloaulavirtual.css">
<style fprolloverstyle>A:hover {color: #FF0000; text-decoration: underline; font-weight: bold}</style>
</head>
<body>
<p class="e4">Documentos nuevos</p>
<%
Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
obj.AbrirConexion
	Set rsContenido=obj.Consultar("DI_ConsultarTodoDocumento","FO",session("idcursovirtual"),session("codigo_tfu"),session("codigo_usu"),0)
obj.CerrarConexion
Set Obj=nothing

If Not(rsContenido.BOF and rsContenido.EOF) then
%>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" width="100%" bordercolor="#808080">
  <tr class="etabla">
    <td width="5%" class="bordeitem">#</td>
    <td width="20%">Fecha de registro</td>
    <td width="70%">Título de documento</td>
  </tr>
  <%
  Do While Not rsContenido.EOF
		i=i+1
  %>
  <tr>
    <td width="5%" class="bordeitem"><%=i%>.</td>
    <td width="20%"><%=rsContenido("fechareg")%>&nbsp;</td>
    <td width="70%">
    <img src="../../../../images/<%=rsContenido("icono")%>" border=0>&nbsp;
    <%=rsContenido("titulo")%>&nbsp;</td>
  </tr>
  <%
  		rsContenido.movenext
		Loop
  %>
</table>
<%else
response.write "<h5 class=""sugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado documentos nuevos para descargar</h5>"
end if

Set rsContenido=nothing
%>
</body>
</html>