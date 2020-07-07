<html>
<head><title></title></head>
<body>
<%
if session("codigo_cpf") = 11 then
%>
<a href="enfermeria.pdf" ><img src="images/acreditaFlyer.png" alt=""/></a>
<%else %>
<a href="educacion.pdf" ><img src="images/acreditaFlyer.png" alt=""/></a>
<%
end if
%>
 

</body>
</html>