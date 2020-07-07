<%
	idcursovirtual=request.querystring("idcursovirtual")
	Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
	    Set rs= obj.Consultar("ConsultarAulaVirtual","FO","3",idcursovirtual,0,0)
	    obj.CerrarConexion
	Set obj=nothing

	'Variables del cursovirtual abierto
	session("idcursovirtual")=rs("idcursovirtual")
	session("nombrecursovirtual")=rs("titulocursovirtual")
	session("idestadocursovirtual")=rs("idestadorecurso")
	session("iniciocursovirtual")=rs("fechainicio")
	session("fincursovirtual")=rs("fechafin")
	session("creadorcursovirtual")=rs("idusuario")
	session("numusuarios")=0
	session("tipofuncion")=4
	session("descripciontipofuncion")="DOCENTE"
	session("creartemas")=0
	session("temapublico")=0
	session("integrartematarea")=0
	session("integrarrptatarea")=0

	'Variables de la aplicación abierta
	session("tipo_apl")="1"
	session("codigo_tfu")="3"
	session("codigo_apl")="4"
	session("descripcion_apl")=rs("titulocursovirtual")
	session("enlace_apl")="cursos.asp"

	Set Obj= Server.CreateObject("AulaVirtual.clsDatAplicacion")	
		session("idvisita_sistema")=Obj.AgregarVisitasRecurso("I",session("creadorcursovirtual"),"cursovirtual", session("idcursovirtual"),session("Equipo_bit"),0,session("idcursovirtual"))
	Set Obj=nothing
	
	response.redirect "frmrecursos.asp"
%>