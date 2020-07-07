<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">



<%
session("tipo_apl")=request.querystring("tipo_apl")
session("codigo_tfu")=request.querystring("codigo_tfu")
session("codigo_apl")=request.querystring("codigo_apl")
session("descripcion_apl")=request.querystring("descripcion_apl")
session("enlace_apl")=replace(request.querystring("enlace_apl"),"-","/")

response.redirect "principal.asp?pagina=" & session("enlace_apl")
%>