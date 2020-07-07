<!--#include file="../../../NoCache.asp"-->
<%
if session("codigo_usu")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

accion=request.querystring("accion")
codigo_tre=request.querystring("codigo_tre")
idtabla=request.querystring("idtabla")

if accion="MoverContenidoTematico" then
	modo=request.querystring("modo")
	arrRecursos=request.form("cbocodigo_ccv") & ","

	'Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatoss")
		' obj.AbrirConexion
			' mensaje=obj.Ejecutar("DI_MoverContenidoTematico",true,modo,codigo_tre,arrRecursos,idtabla,session("codigo_usu"),null)
			' 'response.write request.form & "<br>"
			' 'response.write request.querystring & "<br>"
		' obj.CerrarConexion
	'Set Obj=nothing
	response.write(mensaje)
end if
%>