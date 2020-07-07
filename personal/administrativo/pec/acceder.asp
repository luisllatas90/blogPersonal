<%
'tipo_uap=request.querystring("cbxtipo")

' Dado que el usuario siempre se loguea como personal a una autenticación, entonces le asignamos
 'el tipo_uap = P', para que acceda directamente
on error resume next

tipo_uap = "P"

Session.Contents.RemoveAll
if tipo_uap="" then response.redirect "../tiempofinalizado.asp"

'Sirve para recuperar logeo de windows
Login=Request.ServerVariables("LOGON_USER")


'Login="USAT\" & Login
'Para verificar por usuario
 
Login=  UCASE(Login)
if Login="USAT\TREYES" then
	Login="USAT\RIMAN" 
end if 


if Login="USAT\cmasias" or Login="USAT\fguerrero"  then
else
Clave="0"

'Buscar en la base de datos el id del usuario,según el tipo
Set ObjUsuario= Server.CreateObject("PryUSAT.clsAccesoDatos")
	ObjUsuario.AbrirConexion
		Set rsPersonal=ObjUsuario.Consultar("consultaracceso","FO",tipo_uap,Login,Clave)				
		Set rsCiclo=ObjUsuario.Consultar("consultarcicloAcademico","FO","CV",1)
	ObjUsuario.CerrarConexion
Set ObjUsuario=nothing
If Not(rsPersonal.BOF and rsPersonal.EOF) then    
	session("tipo_usu")=tipo_uap
	session("descripciontipo_usu")=rsPersonal("descripcion_tpe")
	session("codigo_Usu")=rsPersonal("codigo_per")
	session("Ident_Usu")=rsPersonal("nroDocIdentidad_Per")
	session("Nombre_Usu")=rsPersonal("personal")
	session("codigo_Cco")= rsPersonal("codigo_Cco")
	session("codigo_Dac")= rsPersonal("codigo_Dac")
	session("Descripcion_Cco")= rsPersonal("Descripcion_Cco")
	session("Descripcion_Dac")= rsPersonal("nombre_Dac")
	session("codigo_tpe")= rsPersonal("codigo_Tpe")
	session("Equipo_bit")=Request.ServerVariables("REMOTE_ADDR")
	session("Usuario_bit")=Login
    
	'Almacenar datos del ciclo académico
	session("Codigo_Cac")=rsCiclo("codigo_cac")
	session("descripcion_Cac")=rsCiclo("descripcion_cac")
	session("tipo_Cac")=rsCiclo("tipo_cac")
	session("notaminima_cac")=rsCiclo("notaminima_cac")
	rsCiclo.close
	Set rsCiclo=nothing
    
	'=====================================================================	
	'Copia de sesiones temporal hasta el cambio de AulaVirtual
	'=====================================================================
	session("tipo_usu2")=session("tipo_usu")
	session("descripciontipo_usu2")=session("descripciontipo_usu")
	session("codigo_Usu2")=session("codigo_Usu")
	session("Ident_Usu2")=session("Ident_Usu")
	session("Nombre_Usu2")=session("Nombre_Usu")
	session("Usuario_bit2")=session("Usuario_bit")
	'Almacenar datos del ciclo académico
	session("Codigo_Cac2")=session("Codigo_Cac")
	session("descripcion_Cac2")=session("descripcion_Cac")
	session("tipo_Cac2")=session("tipo_Cac")
	session("notaminima_cac2")=session("notaminima_cac")	
	'=====================================================================	
	
	'---------direcciona a la  solicitud -----------------------------------------------------------------------------------
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")	
		Obj.AbrirConexion
			'MostrarActividad=Obj.Ejecutar("AVI_MostrarAviso",true,session("codigo_Usu"),"ANIVUSAT",session("tipo_usu"),"")
		Obj.CerrarConexion
	Set Obj=nothing		
	

	'****************************************    
	'**** Encuesta de Docente #Yperez ****   
	'****************************************
	dim rsCursos,rsVerifica ,obj            
    'Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
    'obj.AbrirConexion
    'Set rsVerifica = server.CreateObject("ADODB.Recordset")        
    'Set rsVerifica = obj.Consultar("VerificarEncuestaDocentexDia","FO",cint(session("codigo_Usu")))
    'obj.CerrarConexion 
    'obj = nothing
    'if (rsVerifica.RecordCount) then        
        'if rsVerifica("eed") = "-1" then	            
            Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	        obj.AbrirConexion	            
	        Set rsCursos = server.CreateObject("ADODB.Recordset")
	        Set rsCursos = obj.Consultar("ConsultarCursosEncuestaDocente","FO",cint(session("codigo_Usu")))
	        obj.CerrarConexion	
	        obj= nothing            
	        if rsCursos.RecordCount then
	            if rsCursos("codigo_Cup")>0 and rsCursos("codigo_eed") = 0 then	              
	               response.Redirect ("AsignaSesionNetEncuestaDocente.aspx?per=" & session("codigo_Usu") & "&cup=" & rsCursos("codigo_Cup"))
	            end if
            end if	            	            
	      'end if	        	        
	'end if 


	
	'************************************************    
	'**** Encuesta de TIPO ES #Yperez RadioOnline ****   
	'************************************************
	    dim rsVerificaES ,objES
        Set objES=Server.CreateObject("PryUSAT.clsAccesoDatos")
        objES.AbrirConexion
        Set rsVerificaES = server.CreateObject("ADODB.Recordset")        
        Set rsVerificaES = objES.Consultar("VerificarEncuestaTipoES","FO",cint(session("codigo_Usu")))
        objES.CerrarConexion 
        objES = nothing
	    if (rsVerificaES.RecordCount) then
	        if rsVerificaES("eed") = "-1" then	            
	            response.Redirect ("../librerianet/Encuesta/EncuestaES/AsignaSesionNet.aspx?x=P&y=" & session("codigo_Usu")& "&z=EncuestaRadioOnline.aspx") 
	        end if
	    end if	
	
	'****************************************    
	'**** Encuesta de Biblioteca #Yperez ****   
	'****************************************
	    dim objBib,rsDatosBib
        Set objBib = Server.CreateObject("PryUSAT.clsAccesoDatos")
        Set rsDatosBib =  server.CreateObject("ADODB.Recordset")
        objBib.AbrirConexion
        Set rsDatosBib = objBib.Consultar("VerificarEncuestaBibliotecaEstudiante","FO", cint(session("codigo_Usu")),cint(session("codigo_cac")))
        If rsDatosBib.recordcount = 0 then 		
            'response.redirect "academico/encuesta/EncuestaBibliotecaPersonal.aspx?xcxa=" & cint(session("codigo_cac")) & "&xcxu=" & cint(session("codigo_usu"))  	  
        end if	        
        objBib.CerrarConexion		
        set rsDatosBib = nothing     
        set objBib = nothing   
	'********************************
  	rutaPag="listaaplicaciones.asp"  	
	response.redirect rutaPag
	'end if
Else%>
	<script>
	    alert('Lo sentimos, Ud. no tiene acceso al Campus Virtual\n\Para cualquier consulta contáctese con el Administrador del Sistema')
	    top.window.close()
	    //top.location.href="../index.asp"
	</script>
<%End if
END IF
if Err.number <> 0 then    
    response.Write (Err.number & ": " &Err.Description & " - "  & Err.helpfile)
end if
%>