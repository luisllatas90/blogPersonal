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
<p class="e4">Accesos realizados: Hoy <%=formatdatetime(now,1)%></p>
<%
idusuario=""

Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
obj.AbrirConexion
	Set rsContenido=obj.Consultar("ConsultarCursoVirtual","FO",13,session("idcursovirtual"),session("codigo_tfu"),session("codigo_usu"))
i=0

If Not(rsContenido.BOF and rsContenido.EOF) then%>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" width="60%" bordercolor="#808080">
<%	Do While Not rsContenido.EOF
		i=i+1
		if idusuario<>rsContenido("idusuario") then
			j=j+1
			i=1
			idusuario=rsContenido("idusuario")
			%>
			<tr><td colspan="3" class="azul"><%=j & "." & rsContenido("nombreusuario")%>&nbsp;</td></tr>
			  <tr class="etabla">
			    <td width="5%">#</td>
			    <td width="20%">Hora Entada</td>
			    <td width="20%">Hora Salida</td>
			  </tr>
		<%end if'else
			'i=i+1
		%>
		  <tr>
		    <td width="5%" valign="top"><%=i%>.</td>
		    <td width="20%" valign="top"><%=formatdatetime(rsContenido("fechaentrada"),3)%>&nbsp;</td>
		    <td width="20%" valign="top">
		    <%if isnull(rsContenido("fechasalida"))=false then
	   			response.write formatdatetime(rsContenido("fechasalida"),3)
			  end if
    		%>&nbsp;</td>
		  </tr>
		  <%'end if
  		rsContenido.movenext
	Loop%>
</table>
<%
else
	response.write "<h5 class=""sugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado accesos realizados</h5>"
end if

obj.CerrarConexion
Set Obj=nothing

Set rsContenido=nothing
%>
</body>
</html>