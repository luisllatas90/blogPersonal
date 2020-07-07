<%
pagina=request.querystring("pagina")

if session("idanio")="" then
	response.redirect "anio.asp"
else
	response.redirect pagina
end if
%>