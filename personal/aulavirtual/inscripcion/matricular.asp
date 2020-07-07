<!--#include file="../../NoCache.asp"-->
<%
'On error Resume next
if session("codigo_usu")="" then response.redirect "../../tiempofinalizado.asp"

dia=request.form("optDia")

Set ObjUsuario= Server.CreateObject("PryUSAT.clsAccesoDatos")
	ObjUsuario.AbrirConexion
		mensaje=ObjUsuario.Ejecutar("MatricularCapacitacionAulaVirtual",true,session("codigo_usu"),dia,null)
	ObjUsuario.CerrarConexion
Set ObjUsuario=nothing

if mensaje="OK" then
%>
<script>
	alert("Se ha registrado correctamente su Matrícula\nGracias por registrarse")
	top.window.close()
</script>
<%else%>
<script>
	alert("<%=mensaje%>")
	history.back(-1)
</script>
<%end if%>