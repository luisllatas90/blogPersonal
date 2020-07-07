<!--#include file="../../../../NoCache.asp"-->
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<html>
<body oncontextmenu="return false">
<%
if session("codigo_usu")="" then response.redirect "../../../../tiempofinalizado.asp"

Dim Accion
Dim IdEvaluacion

	accion=request.querystring("accion")
	IdEvaluacion=Request.querystring("IdEvaluacion")
	idusuario=session("codigo_usu")
	codigoacceso=session("codigo_acceso")
	
	if accion="GuardarTodo" then
		    			
		'if request.form("descripcionrpta")="" then
			'response.write("<script>alert('Debe responder a las preguntas');location.href='frmresponderencuesta1.asp?idevaluacion=" & idevaluacion & "'</script>")
		'else
			On error resume next
			
			Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
			obj.AbrirConexion
			
			Set rsPreguntas=Obj.Consultar("ConsultarEvaluacion","FO",3,idevaluacion,"","")
	
			Do While Not rsPreguntas.EOF
				rpta=request.form("descripcionrpta" & rsPreguntas("IdPregunta"))
				If rpta<>"" then
					call Obj.Ejecutar("AgregarRespuesta",false,codigoacceso,rsPreguntas("idPregunta"),idusuario,rpta)
				end if
				if Err.Number>0 then
					obj.CerrarConexion
	       			Set obj=nothing
					exit do
		        	response.write "<script>" & _
					"alert(""No se pudo registrar corectamente algunas de las respuestas. Verifique Por favor."");" & _
					"location.href=frmresponderencuesta1.asp?idevaluacion=" & idEvaluacion & _
					"</script>"
      			end if
      			rsPreguntas.movenext
			Loop
		
			mensaje=obj.Ejecutar("TerminarEvaluacion",true,idusuario,codigoacceso,null)
			obj.CerrarConexion
	        Set obj=nothing
			
			response.write "<script>alert('" & mensaje & "');window.opener.location.reload();top.window.close()</script>"			
		'end if
	end if
%>
</body>
</html>