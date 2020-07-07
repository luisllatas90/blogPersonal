<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">

<%
session("codigo_apl")=request.querystring("codigo_apl")
session("codigo_tfu") = request.querystring("codigo_tfu")
session("descripcion_apl")=request.querystring("descripcion_apl")
session("descripcion_tfu")=request.querystring("descripcion_tfu")

'if session("codigo_apl")=19 and (session("codigo_tfu")=5 or session("codigo_tfu")=13) then

'	response.redirect "academico/biblioteca/pedidos/suspendido.html"

'else


	estilo_apl=request.querystring("estilo_apl")
	
	'pagina="../librerianet/academico/carpetas.aspx?id=" & session("codigo_usu") & "&ctf=" & session("codigo_tfu") & "&capl=" & session("codigo_apl")
	pagina="menu.asp"
	if estilo_apl="T" then pagina="menu2.asp"

	if estilo_apl="N" then
		response.redirect "principal.asp?pagina=" & session("enlace_apl")
	else	
		response.redirect "index.asp?tipomnu=" & pagina
	end if

'end if
%>