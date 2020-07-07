<%
Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
Obj.AbrirConexion
	set rsMatricula=Obj.Consultar("VerificarAccesoMatriculaEstudiante","FO",1, 12606,33,138)
Obj.CerrarConexion
Set Obj=nothing

response.write(rsMatricula(0))
%>