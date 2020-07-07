<%
	session("idanio")=Request.querystring("idanio")
	session("nombreanio")=Request.querystring("nombreanio")
	
	response.redirect "bentrada.asp"
%>