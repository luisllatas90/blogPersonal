<%
if session("codigo_usu")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

IdDocumento=Request.querystring("iddocumento")

Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
obj.AbrirConexion
	ruta=Obj.Ejecutar("DI_DescargarDocumento",true,idDocumento,session("codigo_usu"),session("idvisita_sistema"),session("Equipo_bit"),session("idcursovirtual"),null)
obj.CerrarConexion
Set Obj=nothing

'response.redirect("../../../../archivoscv/" & ruta)
'---------------------------------------------------------------------------------------------------------------
'Fecha: 29.10.2012
'Usuario: dguevara
'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
'---------------------------------------------------------------------------------------------------------------
response.redirect("../../../../archivoscv/" & ruta)

%>