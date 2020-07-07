<%
pagina=request.querystring("pagina")

'****************************************************
'Validar que las variables sesion esten activas
'****************************************************

if session("idcursovirtual")="" then
	response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"
end if

'****************************************************
'Enviar sesiones por QueryString
'****************************************************
codigo_tfu=session("codigo_tfu")
idusuario=session("codigo_usu")
idvisita=session("idvisita_sistema")
idcursovirtual = session("idcursovirtual")

cadena="idusuario=" & idusuario & "&idvisita=" & idvisita & "&idcursovirtual=" & idcursovirtual & "&codigo_tfu=" & codigo_tfu

response.redirect("../../aulavirtualProfesores/" & pagina & "?" & cadena)
%>