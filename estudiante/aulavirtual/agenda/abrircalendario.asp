<%
response.redirect("../../../librerianet/aulavirtual/calendario.aspx?idcursovirtual=" & session("idcursovirtual") & "&idusuario=" & session("codigo_Usu") & "&codigo_tfu=" & session("codigo_tfu"))
%>