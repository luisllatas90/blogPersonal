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
	
	if request.querystring("tipodoc")="P" then
	    ruta = "http://www.usat.edu.pe/campusvirtual/librerianet/aulavirtual/vistapaginaweb.aspx?iddocumento=" & doc
	else
	    ruta = "http://www.usat.edu.pe/campusvirtual/archivoscv/" & ruta
	end if
else
    if request.querystring("tipodoc")="P" then
	    ruta = "http://www.usat.edu.pe/campusvirtual/librerianet/aulavirtual/vistapaginaweb.aspx?iddocumento=" & doc
	else
	    ruta = "http://www.usat.edu.pe/campusvirtual/archivoscv/" & codigo_acceso
	end if
end if

response.redirect Ruta
%>