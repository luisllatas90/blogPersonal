<!--#include file="../NoCache.asp"-->
<%


'if (session("codigo_vis") <>"" or session("codigo_vis")>0) and session("aula_activa")=""  then
'	response.write("Existe una sesion activa, si deseas iniciar sesion deberás cerrar todas las ventanas de Campus Virtual")
'else

'#Yperez para evitar abrir dos sesiones diferentes en una misma ventana del navegador.

    if Session("ul")="" then 
        'Session("ul")=trim(request.form("Login"))
        Session("ul")=trim(session("textusuario"))
    end if
    if Session("ul") <> trim(session("textusuario")) then
        response.Redirect("mensajelogueo.asp")           
    end if
'#

Dim tipo_uap,Login,Clave, ExisteReg, Egresado
Egresado = "N"

if(session("textusuario") = "" or session("textclave") = "") then
    response.Redirect("index.asp")
end if

'*************************************************************************************
'Asignar valores a las variables login y clave
'*************************************************************************************
'tipo_uap=request.form("cbxtipo")
'Login=trim(request.form("Login"))
'Clave=trim(request.form("Clave"))

tipo_uap = "A"
Login=trim(session("textusuario"))
Clave=trim(session("textclave"))

'session("textusuario") = ""
'session("textclave") = ""

pagina="about:blank"
ExisteReg=false

if Login="" then Login=session("codigo_Usu")
if Clave=""	then Clave=session("clave")

'response.Write "Usuario: " & (session("textusuario")) &  "<br/>"
'response.Write Str(session("textclave")) &  "<br/>"

'Session.Contents.RemoveAll 'Comentado por Yperez

on error resume next

Set ObjUsuario= Server.CreateObject("PryUSAT.clsAccesoDatos")
	ObjUsuario.AbrirConexion	    
    	Set rsAlumno=ObjUsuario.Consultar("consultaracceso","FO","A",Login,Clave)
		Set rsCiclo=ObjUsuario.Consultar("ConsultarCicloAcademico","FO","AUX",1)		
		If Not(rsAlumno.BOF and rsAlumno.EOF) then
			ExisteReg=true						
			
			Set rsEgresado = ObjUsuario.Consultar("ACC_VerificaEgresado","FO",Login)
			If Not(rsEgresado.BOF and rsEgresado.EOF) then
			    Egresado = "S"
			end if			
			'*************************************************************************************
			'Registrar en Bitacora la visita del estudiante en el Campus Virtual
			'*************************************************************************************
			session("codigo_vis")=ObjUsuario.Ejecutar("AgregarSesionesCampus_V3",true,"E",Login,Request.ServerVariables("REMOTE_ADDR"))
		End if
		

	ObjUsuario.CerrarConexion
Set ObjUsuario=nothing

'*************************************************************************************
'Enviar al aula virtual, si de trata de particulares
'*************************************************************************************
if tipo_uap="T" then
	session("Usuario_bit")=Login
	session("clave")=Clave
	rutaPag="aulavirtual/acceder.asp?tipo_uap=T"	
	response.redirect rutaPag
	
	'response.write session("Usuario_bit") & "-" & session("clave")
else
	'*************************************************************************************
	'Consultar en BDatos datos del alumno, ciclo y cronograma de matricula
	'*************************************************************************************
	Set ObjUsuario= Server.CreateObject("PryUSAT.clsAccesoDatos")
	ObjUsuario.AbrirConexion
		Set rsAlumno=ObjUsuario.Consultar("consultaracceso","FO","A",Login,Clave)		
		Set rsCiclo=ObjUsuario.Consultar("ConsultarCicloAcademico","FO","AUX",1)

		If Not(rsAlumno.BOF and rsAlumno.EOF) then
			ExisteReg=true						
			
			Set rsEgresado = ObjUsuario.Consultar("ACC_VerificaEgresado","FO",Login)
			If Not(rsEgresado.BOF and rsEgresado.EOF) then
				Egresado = "S"
			end if			
			'*************************************************************************************
			'Registrar en Bitacora la visita del estudiante en el Campus Virtual
			'*************************************************************************************
			'session("codigo_vis")=ObjUsuario.Ejecutar("AgregarSesionesCampusAlumno",true,"E",Login,0,Request.ServerVariables("REMOTE_ADDR"),null)
		End if		
	ObjUsuario.CerrarConexion
	Set ObjUsuario=nothing



	dim mensaje
	'*************************************************************************************
	'Validar mensajes según opciones
	'*************************************************************************************
	if ExisteReg=False then 'Error de clave o código
		mensaje="Lo sentimos, su Código Universitario o contraseña son incorrectos\nSi ha olvidado su contraseña, acudir en forma personal\na la Oficina de Desarrollo de Sistemas USAT para solicitarla con un\ndocumento de identidad."
		'response.write("<script>alert('***" & tipo_uap & " - " & Login & " - " & Clave & " - " & ExisteReg & " - " & session("codigo_vis") & "')</script>")
	else	
		if rsAlumno("condicion_alu")="I" and rsAlumno("codigo_Pes")="129" and rsAlumno("codigo_cpf")="25" then 'Bloqueo para programa especial de sistemas
		   
		   'if int(rsAlumno("estadoActual_alu"))=0 and (Egresado = "N")  then
		   
		   if int(rsAlumno("estadoActual_alu"))=0  then			
				mensaje="Estimado estudiante, usted ha sido puesto como inactivo en el programa que esta llevando.\nSi desea retomar el programa por favor regularizar su situación."  '& Login '& " - " & Clave
		   'else
		   
				'Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
			'    Obj.AbrirConexion
			'	  set rsPS =Obj.consultar("BloquearAccesoProgramasEspeciales","FO",rsAlumno("edicionProgramaEspecial_alu"),rsAlumno("codigo_alu"))
			'	  if rsPS("acceso")="N" then
			'	     mensaje="Estimado estudiante su Matricula en el Programa ha sido suspendida:\n - Por no estar al día en los pagos acordados.\n y/o\n - DEBE DOCUMENTACION \n\n Por favor regularizar esta situación lo más pronto posible con la Sra. Giovanna (gvillalobos@usat.edu.pe)." 
			'	  end if
			'    Obj.CerrarConexion
				'Set Obj=nothing	
				'set rsPE = nothing
		   end if
	 
		else
			if int(rsAlumno("estadoActual_alu"))=0 and rsAlumno("condicion_alu")="I" AND (Egresado = "N") then 'Mensaje de inactivo	        
			'if int(rsAlumno("estadoActual_alu"))=0 and rsAlumno("condicion_alu")="I"  then 'Mensaje de inactivo	        
				mensaje="Ud. no se ha matriculado en el semestre anterior.\nAcudir a la Oficina de Pensiones de la USAT para regularizar su estado."		    
			end if

			'comentado por yperez 03/01/12, para que los alumnos de escuela pre, ingresen al campus.
			'if rsAlumno("condicion_alu")="P" then 'Mensaje de postulante
			   ' mensaje="Ud. está registrado como POSTULANTE, por tanto no puede ingresar al campus virtual\nSólo si es INGRESANTE acudir a la Escuela Pre de la USAT para regularizar de estado."
			'end if
			
			'*******************************
			'Mensaje para censados
			'*******************************
			if int(rsAlumno("censado"))=0 and (rsAlumno("codigo_cpf")<>"35" and rsAlumno("codigo_cpf")<>"37" and rsAlumno("codigo_cpf")<>"19") then
				response.redirect("alertacenso2010.htm")
			end if
			
		end if
	end if

if mensaje<>"" then%>
	<script>
	    alert("<%=mensaje%>")
	    //top.location.href = "../index.asp"
	    
        /*'----------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: yperez
        'Motivo: Cambio de URL del servidor de la WebUSAT [www.usat.edu.pe->intranet.edu.pe]
        '----------------------------------------------------------------------*/
	    top.location.href = "https://intranet.usat.edu.pe/campusvirtual/estudiante/index.asp"
	    
	</script>
<%elseif ExisteReg=true then

'**********************************************
' CONEXION A ORACLE
'**********************************************
'--- Tipo 1 de conexion con dll ---
'Set con = Server.CreateObject("ConectarOracle.clsConectarOracleVB")
'Set rsLector =  server.CreateObject("ADODB.Recordset")
'consultaSQL = "SELECT Z308_ID FROM Z308 WHERE Z308_REC_KEY LIKE UPPER('%" & Login & "%') OR Z308_REC_KEY LIKE '%" & rsAlumno("nroDocIdent_Alu") & "%'"
'con.CadenaConexion "UTM00", "UTM00"
'con.AbrirConexion
'set rsLector = con.Consultar("C", cstr(consultaSQL),  "FO", "")
'If Not(rsLector.BOF and rsLector.EOF) then
'	consultaSQL = "SELECT z36_rec_key, z36_number FROM Z36 WHERE to_number(Z36_DUE_DATE) <= to_number(TO_CHAR(SYSDATE, ''YYYYMMDD')) AND to_number(Z36_DUE_HOUR) <= to_number(to_char(CURRENT_TIMESTAMP, 'HH24MI')) AND  Z36_ID='" & rsLector("z308_id") & "'" 
'	con.CerrarConexion
'	con.CadenaConexion "UTM50", "UTM50"
'	con.AbrirConexion()
'	set rsAleph = con.Consultar("C", cstr(consultaSQL), "ST", "")
'	If Not(rsAleph.BOF and rsAleph.EOF) then 
'    	session("DebeLibrosAleph")= "1"
'	else
'		session("DebeLibrosAleph")= "0"
'	end if 
	'cerramos el recordset y la conexión 
'	rsAleph.close 
'	con.CerrarConexion
'else
'	con.CerrarConexion
'end if 
'liberamos los objetos 
'Set con = Nothing 
'Set rsAleph = Nothing 
'Set rsLector = Nothing 
'--- Tipo 2 de conexion con dll ---
session("DebeLibrosAleph")= "0"
'---------------comentado
'Set con = Server.CreateObject("ADODB.Connection")
'Set rsLector =  server.CreateObject("ADODB.Recordset")
'Set rsAleph =  server.CreateObject("ADODB.Recordset")
'consultaSQL = "SELECT Z308_ID FROM Z308 WHERE Z308_REC_KEY LIKE UPPER('%" & Login & "%') OR Z308_REC_KEY LIKE '%" & rsAlumno("nroDocIdent_Alu") & "%'"
'con.open "Provider=msdaora;user id=UTM00;password=UTM00;data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=172.16.1.8)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=aleph20.aleph.usat.edu.pe)))"
'rsLector.Open cstr(consultaSQL), con, 1
'If Not(rsLector.BOF and rsLector.EOF) then
'	consultaSQL = "SELECT z36_rec_key, z36_number FROM Z36 WHERE to_number(concat(Z36_DUE_DATE, Z36_DUE_HOUR)) <= to_number(concat(TO_CHAR(SYSDATE, 'YYYYMMDD'), to_char(CURRENT_TIMESTAMP, 'HH24MI')))  AND  Z36_ID='" & rsLector("z308_id") & "'" 
'	con.close
'	con.open "Provider=msdaora;user id=UTM50;password=UTM50;data source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=172.16.1.8)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=aleph20.aleph.usat.edu.pe)))"
'	rsAleph.Open cstr(consultaSQL), con, 1
'	If Not(rsAleph.BOF and rsAleph.EOF) then 
'   		session("DebeLibrosAleph")= "1"
'	else
'		session("DebeLibrosAleph")= "0"
'	end if 
'cerramos el recordset y la conexión 
'	rsAleph.close 
'	con.close
'else
'	con.close
'end if 
'liberamos los objetos 
'Set con = Nothing  
'Set rsAleph = Nothing 
'Set rsLector = Nothing 
'---------------comentado
'********************
'Fin conexion oracle
'********************
'*************************************************************************************
'Almacenar en Variables sesion los datos recuperados
'*************************************************************************************
	'Almacenar datos del ciclo académico
	session("Codigo_Cac")=rsCiclo("codigo_cac")
	session("descripcion_Cac")=rsCiclo("descripcion_cac")
	session("tipo_Cac")=rsCiclo("tipo_cac")
	session("notaminima_cac")=rsCiclo("notaminima_cac")
	session("cronograma_mat")=rsCiclo("cronograma_mat")
	rsCiclo.close
	Set rsCiclo=nothing

	session("tipo_usu")=tipo_uap	
	if(rsAlumno("egresado_alu") = true) then
	    session("es_egresado") = 1
	else
	    session("es_egresado") = 0
	end if
	
	session("descripciontipo_usu")=rsAlumno("descripcion_Tpe")
	session("codigo_Usu")=rsAlumno("codigo_alu")
	session("Ident_Usu")=rsAlumno("codigoUniver_Alu")
	session("Nombre_Usu")=rsAlumno("alumno")
	session("Equipo_bit")=Request.ServerVariables("REMOTE_ADDR")
	session("Usuario_bit")=Login
	session("codigoUniver_alu")=rsAlumno("codigoUniver_Alu")
	session("alumno")=rsAlumno("alumno")
	session("nombre_cpf")=rsAlumno("nombre_cpf")
	session("codigo_cpf")=rsAlumno("codigo_cpf")
	session("codigo_alu")=rsAlumno("codigo_alu")
	session("Codigo_Pes")=rsAlumno("codigo_pes")
	session("cicloIng_Alu")=rsAlumno("cicloIng_Alu")
	session("clave")=clave
	session("CicloActual")=rsAlumno("cicloActual_Alu")
	session("descripcion_pes")=rsAlumno("descripcion_pes")
	session("TipoPension")=rsAlumno("tipopension_Alu")
	session("PrecioCredito")=rsAlumno("preciocreditoAct_Alu")
	session("MonedaPrecioCredito")=rsAlumno("monedapreccred_Alu")
	session("estadodeuda_alu")=rsAlumno("estadodeuda_alu")
	session("UltimaMatricula")=rsAlumno("UltimaMatricula")
	session("UltimoEstadoMatricula")=rsAlumno("UltimoEstadoMatricula")
	session("ActualizoDatos")=rsAlumno("ActualizoDatos")
	'session("ActualizoDatos")=1
	session("beneficio_alu")=rsAlumno("beneficio_alu")
	session("nombre_min")=rsAlumno("nombre_min")
	session("DebeLibros")=rsAlumno("DebeLibros")
	
	session("defecto_pes")=rsAlumno("defecto_pes")
	session("cicloalumno")=rsAlumno("CicloAlumno")
	session("nroDocIdent_Alu")=rsAlumno("nroDocIdent_Alu")
	'session("bloqueoDocumento_Alu")=rsAlumno("bloqueoDocumento_Alu")
	session("BloqueoCampus_Alu") =0
	session("aula_activa") =""
	session("codigo_test") = rsAlumno("codigo_test")
	'session("encuesta") = 0     '0. Sin encuestas pendientes    1. Falta Enuesta
	foto_alu=rsAlumno("foto_alu")
	session("foto_alu")=rsAlumno("foto_alu")
	
	if session("estadodeuda_alu") = 1 and session("codigo_test")=2 then

	else

	end if
	idEncuesta=rsAlumno("idEncuesta")
	accesosencuesta=rsAlumno("accesosencuesta") 



	
	
	'---------direcciona a la  solicitud -----------------------------------------------------------------------------------
	'Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	'	Obj.AbrirConexion
	'		MostrarActividad=Obj.Ejecutar("AVI_MostrarAviso",true,session("codigo_Usu"),"ANIVUSAT","A","")
	'	Obj.CerrarConexion
	'Set Obj=nothing	
	'if MostrarActividad="NO" then
	'	rutaPag="listaaplicaciones.asp"
	'------Para acceso de postulantes al campus virtual
	condicion_alu=rsAlumno("condicion_alu")
	if condicion_alu="P" then
	   ' session("encuesta") = 0	    
		response.redirect "abriraplicacion.asp?codigo_tfu=74&codigo_apl=8&descripcion_apl=Campus Virtual USAT&enlace_apl=" & pagina & "&estilo_apl=N"
	end if
	'-------------------------------------------------------------------------------------

	'Ingresar a página del campus virtual
	if session("cronograma_mat")<>"0" then
		pagina="comunicado.asp"
	else
		if session("codigo_cpf")="2" then
			pagina="avisos/derecho/index.html"
		end if			
	end if
	pagina="avisos.asp"

	'Para pruebas con nuevo ciclo
	'if session("codigo_alu")=10793 then
	'	session("codigo_Cac")=32
	'	session("tipo_cac")="E"
	'end if
   	
	if (int(foto_alu)=0 and int(session("codigo_cpf"))<>25 and trim(session("cicloIng_Alu"))<>trim(session("descripcion_Cac")) and rsAlumno("codigo_test") = 2) then
'		response.write "<script>alert('OBLIGATORIO: TOMA DE FOTOGRAFÍA PARA CARNET DEL 01 AL 15 DE MARZO EN AULA 104 DE 10:00 AM A 01:00 PM');top.location.href='../index.asp'</script>"
		rutaPag="abriraplicacion.asp?codigo_tfu=3&codigo_apl=8&descripcion_apl=Campus Virtual USAT&enlace_apl=" & pagina & "&estilo_apl=N&foto=0"
		response.write "<script>" & mensaje & "top.location.href='" & rutaPag & "'</script>"
	end if
	'elseif session("codigo_cpf") = "31" then
	'	response.redirect "mensajes.asp?proceso=ODON"
	if trim(session("DebeLibros"))="0" then
		if session("codigo_alu")=19203 then
        'if session("ActualizoDatos")=2 then
		    'session("codigo_test")
			'if session("codigo_cpf")<>25 and session("codigo_cpf")<>19 then
			'	rutaPag="abrirfichaad.asp" ''FICHA PARA PENSIONES
			'else
			'rutaPag="frmactualizardatos.asp"
			'end if
			'' -- Comentado sólo para campaña de dni
            ''rutaPag="frmactualizardatos.asp"
			''mensaje="alert('Estimado estudiante: \n\nSírvase completar la información que le presentaremos a continuación, para actualizar su Ficha Personal\n\Esta información será verificada y utilizada por la Universidad, para fines académicos y administrativos.');"
			Set objEnc=server.CreateObject("EncriptaCodigos.clsEncripta")			
            'response.write("<script>alert('###" & tipo_uap & " - " & Login & " - " & Clave & " - " & ExisteReg & " - " & session("codigo_vis") & "')</script>")
            'response.write("<script>alert('$$$" & pagina & " - "& ExisteReg & "')</script>")   
            response.redirect("../librerianet/academico/frmactualizardni.aspx?accion=A&c=" & objEnc.Codifica("069" & session("codigo_alu")) & "&x=" & objEnc.Codifica("069" & session("codigo_alu")) & "&Tipo=E")			
			'response.Write("<script>location.href='../librerianet/academico/frmactualizardni.aspx?accion=A&c=" & objEnc.Codifica("069" & session("codigo_alu")) & "&x=" & objEnc.Codifica("069" & session("codigo_alu")) & "&Tipo=E'</script>")
	  	else
			if (accesosencuesta="0" and idEncuesta<>"0") then
				'Si no ha respondido la Encuesta con código
				'rutaPag="aulavirtual/evaluacion/abrirevaluacionlibre.asp?idevaluacion=" & idEncuesta & "&accion=iniciarencuesta"
			else
				'Bloquear a escuelas
				'if session("codigo_cpf")=22 then
				'	response.redirect "mensajes.asp?proceso=F"							
				'end if 
				'if session("codigo_alu")=1354 then
					'response.redirect "mensajes.asp?proceso=F"							                    					
					rutaPag="abriraplicacion.asp?codigo_tfu=3&codigo_apl=8&descripcion_apl=Campus Virtual USAT&enlace_apl=" & pagina & "&estilo_apl=N"

				'else
					'	rutaPag="abriraplicacion1.asp?codigo_tfu=3&codigo_apl=8&descripcion_apl=Campus Virtual USAT&enlace_apl=" & pagina & "&estilo_apl=N"
				'end if
				
			end if
		end if			
		response.write "<script>" & mensaje & "top.location.href='" & rutaPag & "'</script>"		
    else        
        
		response.redirect "mensajes.asp?proceso=L"
	end if	
	else	    
		'rutaPag="../kermesusat/index.html"
		'response.redirect (rutaPag)
	end if
	'-----------------------------------------------------------------------------------------------------------------------

'END IF
Set rsAlumno=nothing
end if

'end if
if Err.number <> 0 then    
    response.Write "<br/>" & (err.Description)
end if
%>