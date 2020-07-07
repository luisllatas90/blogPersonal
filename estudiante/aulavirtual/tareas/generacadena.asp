<%
pagina=request.querystring("pagina")

'****************************************************
'Validar que las variables sesion esten activas
'****************************************************

if session("idcursovirtual")="" then
	response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"
end if

'****************************************************
'Datos del cursovirtual y del usuario logeado
'****************************************************
idusuario=session("codigo_usu")
ficursovirtual=session("iniciocursovirtual")
ffcursovirtual=session("fincursovirtual")
idvisita=session("idvisita_sistema")
idcursovirtual = session("idcursovirtual")

accion=request.querystring("accion")
numfila=request.querystring("numfila")

'****************************************************
'Datos del DOCUMENTO
'****************************************************
idcarpeta=request.querystring("idcarpeta")
idDocumento=request.querystring("idDocumento")
tipodoc=request.querystring("tipodoc")

'****************************************************
'Datos de la TAREA
'****************************************************
idtarea=request.querystring("idtarea")
idtareausuario=request.querystring("idtareausuario")
if idtareausuario="" then idtareausuario=0

cadena="idusuario=" & idusuario & "&ficursovirtual=" & ficursovirtual & "&ffcursovirtual=" & ffcursovirtual & "&idvisita=" & idvisita & "&accion=" & accion & "&numfila=" & numfila & "&idcursovirtual=" & idcursovirtual & "&refidtareausuario=0"
if pagina="frmdocumento.aspx" then
	cadena=cadena & "&idcarpeta=" & idcarpeta & "&iddocumento=" & iddocumento 
	response.redirect("../../../libreriaNET/aulavirtual/" & pagina & "?" & cadena)
else
	cadena=cadena & "&idtarea=" & idtarea & "&idtareausuario=" & idtareausuario
	response.redirect("../../../libreriaNET/aulavirtual/" & pagina & "?" & cadena)
end if
%>