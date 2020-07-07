<!--#include file="../NoCache.asp"-->
<%
on error resume next
Dim tipo_uap,Login,Clave, ExisteReg, Egresado
Login = request.form("Login")
Clave = request.form("Clave")
pagina="about:blank"
ExisteReg=false
tipo_uap = "G"
session("tipo_usu")=tipo_uap
Set ObjUsuario= Server.CreateObject("PryUSAT.clsAccesoDatos")
ObjUsuario.AbrirConexion	    
Set rsAlumno=ObjUsuario.Consultar("Alumni_ConsultarAcceso","FO",Login,Clave,1)
session("EgresadoAlumni")= 0
		If Not(rsAlumno.BOF and rsAlumno.EOF) then
            session("EgresadoAlumni") = 1        
            session("codigo_alu")  = rsAlumno("codigo_Alu") 
            session("codigo_usu") =  rsAlumno("codigo_Alu") 
            session("nombre_usu") =  rsAlumno("nombre_usu") 
            session("nombre_Cpf") =  rsAlumno("nombre_Cpf") 
            session("codigo_pso") =  rsAlumno("codigo_pso") 
            'session("hp") =  rsAlumno("hp") 
            ObjUsuario.Ejecutar "ALUMNI_InsertaBitacoraUpdateDatos",true,session("codigo_pso"),"LO" 
            ObjUsuario.CerrarConexion
            Set ObjUsuario=nothing
            Set rsAlumno=nothing    
            'pagina="avisos.asp"            
            response.redirect "abriraplicacion.asp?codigo_tfu=133&codigo_apl=8&descripcion_apl=Campus Virtual USAT&enlace_apl=" & pagina & "&estilo_apl=N"
        Else
            ObjUsuario.CerrarConexion
            Set ObjUsuario=nothing
            Set rsAlumno=nothing    
            response.Write "<script>alert('Estimado Egresado USAT, los datos ingresados son incorrectos.');top.location.href=""egresado.asp"";</script>"            
            
		End if		
if Err.number <> 0 then    
   response.Write "<br/>" & (err.Description)
end if
%>