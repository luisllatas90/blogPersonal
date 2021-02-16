<%
 'tipo_uap=request.querystring("cbxtipo")

' Dado que el usuario siempre se loguea como personal a una autenticaci�n, entonces le asignamos
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
'response.write "<script>alert('" & Login & "')</script>" 
    
     
' serverdev      
'Login = "USAT\scastro"
'Login = "USAT\HMERA"
'Login = "USAT\MCRUZ"  
'Login = "USAT\DOJEDA"  
'Login = "USAT\JFUPUY" 
'Login = "USAT\MVILCHEZ"
'Login = "USAT\MMELENDEZ"  
'LOGIN = "USAT\ovargas"
'Login = "USAT\andy.diaz"
'Login = "USAT\rtimana"
Login = "USAT\nelly.becerra"
Login = "USAT\MGAMARRA"
'Login = "USAT\RSYORENTE"
'Login = "USAT\CCHAVEZ"
Login = "USAT\esther.vasquez"
'Login = "USAT\mticliahuanca"
login = "USAT\ALLATAS"
'Login = "USAT\CCAMA"
'Login = "USAT\ALLATAS"
'Login = "USAT\rasalde" 
'Login = "USAT\wgarcia"
'Login = "USAT\ehernandez"
Login = "USAT\eurpeque"
'Login = "USAT\fmanay"
Login = "USAT\ESAAVEDRA"
'Login = "USAT\arivasplata" 
'Login = "USAT\PDIAZ"
'Login = "USAT\rnunez "
'Login = "USAT\jcampodonico"
'Login = "USAT\pcampos"
'Login = "USAT\cmundaca"
'Login = "USAT\msalas"
'Login = "USAT\ftuesta"
'Login = "USAT\MSALAS"
'Login = "USAT\MFHUIDOBRO"
'Login = "USAT\AGUTIERREZ"
Login = "USAT\JOLIVOS"
'Login = "USAT\GLEON"
'Login = "USAT\CACOSTA"
'Login = "USAT\HZAPATA"
'Login = "USAT\MCUADRA"
Login = "USAT\PDIAZ"  


IF LOGIN = "USAT\MNECIOSUP" THEN    
   LOGIN = "USAT\JOLIVA" 
END IF


if Login="USAT\cmasias" or Login="USAT\fguerrero"  then
else 
Clave="0"
   

'Buscar en la base de datos el id del usuario,seg�n el tipo
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
    
	'Almacenar datos del ciclo acad�mico
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
	'Almacenar datos del ciclo acad�mico
	session("Codigo_Cac2")=session("Codigo_Cac")
	session("descripcion_Cac2")=session("descripcion_Cac")
	session("tipo_Cac2")=session("tipo_Cac")
	session("notaminima_cac2")=session("notaminima_cac")	
	'=====================================================================	
	
  	'rutaPag="listaaplicaciones.asp"  	
  	rutaPag="AsignaSesionNet.aspx?per="&session("codigo_Usu")   	& "&perlogin="& session("Usuario_bit") & "&nombre=" &session("Nombre_Usu")
	response.redirect rutaPag 
	'end if
Else%>
	<script type="text/javascript" language="javascript">
	    alert('Lo sentimos, Ud. no tiene acceso al Campus Virtual\n\Para cualquier consulta contactese con Direccion de Personal o con su Director de Departamento')
	    top.window.close()
	    //top.location.href="../index.asp"
	</script>
<%End if
END IF
if Err.number <> 0 then    
    response.Write (Err.number & ": " &Err.Description & " - "  & Err.helpfile)    
end if
%>