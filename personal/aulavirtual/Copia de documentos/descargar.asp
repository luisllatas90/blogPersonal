<%
Dim idUsuario

tblrecurso=Request.querystring("tblrecurso")
Ruta=Request.querystring("Ruta")
Doc=Request.querystring("Doc")
idusuario=session("codigo_usu")
codigo_acceso=request.querystring("codigo_acceso")

if codigo_acceso="" then
	Set Obj= Server.CreateObject("AulaVirtual.clsDatAplicacion")
		a=Obj.AgregarVisitasRecurso("I",session("codigo_usu"),tblrecurso,Doc,session("Equipo_bit"),session("idvisita_sistema"),session("idcursovirtual"))
	Set Obj=nothing
	
	response.redirect "http://www.usat.edu.pe/campusvirtual/archivoscv/" & ruta
	'response.redirect Ruta
else
	'Ruta="../../../archivoscv/" & codigo_acceso
	ruta = "http://www.usat.edu.pe/campusvirtual/archivosccv/" & codigo_acceso
	response.redirect Ruta
end if

'response.write(ruta)
%>