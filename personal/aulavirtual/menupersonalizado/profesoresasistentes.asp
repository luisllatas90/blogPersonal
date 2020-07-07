<%
codigo_dac=request.querystring("codigo_dac")
nombre_dac=request.querystring("nombre_dac")

	Set ObjUsuario= Server.CreateObject("PryUSAT.clsAccesoDatos")
		objUsuario.AbrirConexion
		set rsAccesos=ObjUsuario.consultar("CM_ConsultarFacultad","FO",3,codigo_dac,session("idcursovirtual"),session("codigo_usu"))
		objUsuario.CerrarConexion
	Set ObjUsuario=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Profesores asistentes adscritos</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
</head>

<body>
<p class="usatTitulo"><%=nombre_dac%><br><i>Profesores asistentes</i></p>
<p><input type="button" value="Regresar" name="cmdRegresar" class="salir" onclick="history.back(-1)"></p>
<%if (rsAccesos.BOF and rsAccesos.EOF) then%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se encontraron profesores asistentes</h5>
<%else%>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="90%" id="AutoNumber1">
  <tr class="etabla">
    <td width="10%">Nº</td>
    <td width="26%">Fecha de registro</td>
    <td width="114%">Apellidos y Nombres</td>
  </tr>
  <%Do while Not rsAccesos.EOF
  	i=i+1
  	%>
  <tr>
    <td width="10%"><%=i%>&nbsp;</td>
    <td width="26%"><%=rsAccesos("fecha_epar")%>&nbsp;</td>
    <td width="114%"><%=rsAccesos("profesor")%>&nbsp;</td>
  </tr>
  	<%rsAccesos.movenext  
  Loop
  Set rsAccesos=nothing
  %>
</table>
<%end if%>
</body>

</html>