<!--#include file="../NoCache.asp"-->
<!--#include file="../funciones.asp"-->
<%
call Enviarfin(session("codigo_usu"),"../")

accion=Request.querystring("accion")

on error resume next
	
	if accion="cambiarclave" then
		claveanterior=request.form("txtclaveanterior")
		clavenueva=request.form("txtclavenueva")
	
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			Obj.AbrirConexion
				mensaje=obj.Ejecutar("CambiarClave",true,session("codigo_alu"),claveanterior,clavenueva,NULL)
			Obj.CerrarConexion
		Set obj=nothing
		pag="location.href='misdatos.asp'"
		
		if instr(mensaje,"coincide")>0 then
			pag="location.href='cambiarclave.asp'"
		end if
		
		response.write "<script>alert('" & mensaje & "');" & pag & "</script>"
	end if

	if accion="agregarsugerencia" then
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			Obj.AbrirConexion
				Call obj.Ejecutar("AgregarSugerencia",false,usuario,request.form("txtdescripcion_sug"))
			Obj.CerrarConexion
		Set obj=nothing
		pag="location.href='comunicado.asp'"
		response.write "<script>alert('Gracias por Registrar su Sugerencia');" & pag & "</script>"
	end if
	
If Err.Number<>0 then
    session("pagerror")="estudiante/procesar.asp"
    session("nroerror")=err.number
    session("descripcionerror")=err.description    
	response.write("<script>top.location.href='../error.asp'</script>")
End If
%>