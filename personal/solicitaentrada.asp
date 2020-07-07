<% 
set cn=server.createobject("pryusat.clsaccesodatos")
dim personal
dim cantidad
personal = session("codigo_usu")
cantidad = request.form("cantidad")

'response.Write personal & " - " & cantidad
		cn.abrirconexion	
    		CN.Ejecutar "EVE_InscripcionAlmuerzo2012",FALSE,personal,cantidad
		cn.cerrarconexion

'response.write (session("codigo_Usu"))
response.redirect "listaaplicaciones.asp"
%>