<%
if session("tipofuncion")=3 then
	response.redirect "detallesituacion.asp"	
else
	response.redirect ""	
end if%>