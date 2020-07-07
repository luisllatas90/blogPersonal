<!--#include file="../../../NoCache.asp"-->
<%
if session("codigo_usu")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

accion=request.querystring("accion")
codigo_tre=request.querystring("codigo_tre")
idtabla=request.querystring("idtabla")

if accion="MoverContenidoTematico" then
	modo=request.querystring("modo")
	arrRecursos=request.form("cbocodigo_ccv") & ","

	Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			mensaje=obj.Ejecutar("DI_MoverContenidoTematico",true,modo,codigo_tre,arrRecursos,idtabla,session("codigo_usu"),null)
			'response.write request.form & "<br>"
			'response.write request.querystring & "<br>"
		obj.CerrarConexion
	Set Obj=nothing
	response.write(mensaje)
end if


if accion="Cambiardisenio" then
	idcursovirtual=request.form("cboidcursovirtual")
	tipo=request.form("cboplataforma")

	Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			if tipo=1 then
				mensaje=obj.Ejecutar("DI_CrearDisenioAsignatura_v1",true,idcursovirtual,null)
			end if
			if tipo=2 then
				mensaje=obj.Ejecutar("DI_CrearDisenioAsignatura_v2",true,idcursovirtual,null)
			end if
			if tipo=3 then
				mensaje=obj.Ejecutar("DI_CambiarDisenioCurso",true,idcursovirtual,"M",1,null)
			end if
		obj.CerrarConexion
	Set Obj=nothing

	response.write(mensaje)
end if
%>