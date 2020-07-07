<%

Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		call obj.Ejecutar("ActualizarEstadoCandado",false,request.querystring("codigo_deu"),request.querystring("estado"),session("codigo_usu"))

	obj.CerrarConexion
Set obj=nothing

response.redirect ("vstdeudascobrarcandado.asp?codigo_sco=" & request.querystring("codigo_sco") & "&fechainicio=" & request.querystring("fechainicio") & "&fechafin=" & request.querystring("fechafin"))

%>		