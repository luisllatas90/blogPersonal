<%
	session("idacreditacion")=Request.querystring("idacreditacion")
	session("idmodelo")=Request.querystring("idmodelo")
	session("titulomodelo")=Request.querystring("titulomodelo")
	session("idcarrera")=Request.querystring("idcarrera")
	session("nombrecarrera")=Request.querystring("nombrecarrera")
	'Set Obj= Server.CreateObject("AulaVirtual.clsUsuario")		
	'	session("IdAcceso")=Obj.AgregarVisitasRecurso("Acreditacion",session("idacreditacion"),session("codigo_usu"),session("IdVisita"),0)
	'Set Obj=nothing
	response.redirect "principal.asp"
%>