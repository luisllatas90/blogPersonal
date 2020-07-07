<%
dim imagen,codigoencontrado,codigoreal

Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")

'****************************************************
response.write "<h1>PROCESO DE DECODIFICACION</h1><HR>"
'****************************************************

imagen="23A733337H7.jpg"
response.write "IMAGEN CON EXT: " & imagen & "<br>"
'Quita la extensión
imagen=left(imagen,len(imagen)-4)
response.write "IMAGEN SIN EXT: " & imagen & "<br>"

'Decodifica el codigo real		
codigoencontrado=obEnc.DECodifica(imagen)
			
response.write "CODIGO ENCONTRADO: " & codigoencontrado & "<br>"

'Quita el 069 de la imágen
codigoreal=right(codigoencontrado,len(codigoencontrado)-3)

response.write "CODIGO REAL: " & codigoreal & "<br><br>"

'****************************************************
response.write "<h1>PROCESO DE CODIFICACION</h1><HR>"
'****************************************************
codigoreal="0210000171"
response.write "CODIGO REAL: " & codigoreal & "<br>"

'Codifica el codigo real		
Imagen=obEnc.Codifica("069" & codigoreal)
			
response.write "IMAGEN GENERADA SIN EXT: " & imagen & "<br>"
response.write "IMAGEN GENERADA CON EXT: " & imagen & ".JPG<br>"

Set objEnc=nothing
%>