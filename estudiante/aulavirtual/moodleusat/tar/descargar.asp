<%
if session("codigo_usu")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

idtareausuario=Request.querystring("idtareausuario")

Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
obj.AbrirConexion
	ruta=Obj.Ejecutar("DI_DescargarTarea",true,idtareausuario,session("codigo_usu"),session("idvisita_sistema"),session("Equipo_bit"),session("idcursovirtual"),null)
obj.CerrarConexion
Set Obj=nothing

'response.redirect("../../../../archivoscv/" & ruta)
'response.write("../../../../archivoscv/" & ruta)
response.redirect("http://www.usat.edu.pe/campusvirtual/archivoscv/" & ruta)
%>