<%
Response.Expires = 0
Response.ExpiresAbsolute = Now() - 1
Response.addHeader "pragma","no-cache"
Response.addHeader "cache-control","private"
Response.CacheControl = "no-cache" 


function ValidarSesion(Byval variablefin,ByVal rutaactual)
	if (variablefin="" or isnull(variablefin)=true) then
		response.write "<script>top.location.href='" & rutaactual & "tiempofinalizado.asp'</script>"
	end if
end function

%>