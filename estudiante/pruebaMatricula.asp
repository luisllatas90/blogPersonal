
<%
Set obj=server.createobject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion
Set rs=obj.Consultar("ConsultarCursosHabilesMatriculav6","FO",6428,133,30)
obj.CerrarConexion
response.write (rs.recordcount)
Set obj=nothing
%>