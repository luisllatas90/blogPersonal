
<%
Set ObjUsuario= Server.CreateObject("PryUSAT.clsAccesoDatos")
	ObjUsuario.AbrirConexion
		Set rsPersonal=ObjUsuario.Consultar("consultaracceso","FO","P","usat\csenmache","pellejon")				
		Set rsCiclo=ObjUsuario.Consultar("consultarcicloAcademico","FO","CV",1)
	ObjUsuario.CerrarConexion
Set ObjUsuario=nothing
%>
Test