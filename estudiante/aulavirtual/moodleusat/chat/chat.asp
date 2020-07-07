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
<p class="e4">Sesiones de Chat por realizar</p>
<%
Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
obj.AbrirConexion
	Set rsContenido=obj.Consultar("DI_ConsultarTodoChat","FO",session("idcursovirtual"),session("codigo_tfu"),session("codigo_usu"),0)
obj.CerrarConexion
Set Obj=nothing

If Not(rsContenido.BOF and rsContenido.EOF) then
%>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" width="100%" bordercolor="#808080">
  <tr class="etabla">
    <td width="5%">#</td>
    <td width="20%">Duración</td>
    <td width="70%">Título</td>
    <td width="5%">Total de Sesiones</td>
  </tr>
  <%
  Do While Not rsContenido.EOF
		i=i+1
  %>
  <tr>
    <td width="5%" valign="top"><%=i%>.</td>
    <td width="20%" valign="top">
    <i><b>Desde:</b></i> <%=rsContenido("fechainicio")%><br><i><b>Hasta:</b></i> <%=rsContenido("fechafin")%>
    
    </td>
    <td width="70%" valign="top">
    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
      <tr>
        <td width="3%%"><img src="../../../../images/<%=rsContenido("icono")%>" border=0></td>
        <td width="97%"> <%=rsContenido("titulo")%></td>
      </tr>
      <tr>
        <td width="3%">&nbsp;</td>
        <td width="97%"><%=rsContenido("descripcion")%></td>
      </tr>
    </table>
	</td>
    <td width="5%" valign="top" align="center">
    <%=rsContenido("total")%>&nbsp;</td>
  </tr>
  <%
  		rsContenido.movenext
		Loop
  %>
</table>
<%else
response.write "<h5 class=""sugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado sesiones de chat por realizar</h5>"
end if

Set rsContenido=nothing
%>
</body>
</html>