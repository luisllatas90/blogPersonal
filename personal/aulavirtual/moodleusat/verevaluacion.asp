<%
cur=session("idcursovirtual")
usu=session("codigo_usu")

'response.redirect("../../../librerianet/aulavirtual/detalleevaluacion.aspx?idcursovirtual=" & cur & "&codigo_usu=" & usu)
response.redirect("../../aulavirtualProfesores/detalleevaluacion.aspx?idcursovirtual=" & cur & "&codigo_usu=" & usu)
%>