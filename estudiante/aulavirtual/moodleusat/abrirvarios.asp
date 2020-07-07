<%

'314
idcursovirtual=2116 'request.querystring("idcursovirtual")
idusuario="USAT\gchunga" 'request.querystring(idusuario)
refcodigo_ccv=1 'request.querystring("idcursovirtual")
refiddocumento=40705 'request.querystring("idcursovirtual")

response.redirect ("../../../librerianet/aulavirtual/frmpublicardocumentos.aspx?idcursovirtual=" & idcursovirtual & "&usuario=" & idusuario & "&refcodigo_ccv=" & refcodigo_ccv & "&refiddocumento=" & refiddocumento)
%>