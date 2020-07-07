<%
'verifica si es Jimenez
if lcase(session("Usuario_bit"))="usat\cjimenez" then
	response.redirect("frmadminmatricula.asp")
end if
%>