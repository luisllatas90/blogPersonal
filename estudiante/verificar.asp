<!--#include file="../funciones.asp"-->
<%
call Enviarfin(session("codigo_usu"),"../")

modo=trim(request.querystring("modo"))
dim mensaje
mensaje = ""
   if modo="" then modo="M"

   'If 	session("estadodeuda_alu")=1 and  session("codigo_test")=2 then 'and _
	'	'session("codigo_cpf")<>"25" then
	'	session("BloqueoCampus_Alu") =1
	'	mensaje = mensaje & "H|"
	'	response.redirect "mensajes.asp?proceso=H"
   'else
		'if (cdbl(session("codigo_cpf"))=4 or cdbl(session("codigo_cpf"))=11 or cdbl(session("codigo_cpf"))=3) and modo="M" then 
		'	response.redirect "mensajes.asp?proceso=B"
		'end if
		'****************************************
		'Bloqueo por separación vigente
		'****************************************
		if modo<>"H" then
			Set objCnx=Server.CreateObject("PryUSAT.clsAccesoDatos")
			objCnx.AbrirConexion
			set rsSeparacion=objCnx.Consultar("ACAD_ConsultarSeparacionVigente","FO",session("codigo_alu"))
			objCnx.CerrarConexion
			Set objCnx=nothing
			If (rsSeparacion.BOF = false AND rsSeparacion.EOF = false) then
				'response.redirect "mensajes.asp?proceso=SEP"
				session("BloqueoCampus_Alu") =1
				mensaje = mensaje & "SEP|"
			end if 
		end if
		'****************************************
		'Bloquear escuelas
		'****************************************
		if cdbl(session("codigo_cpf"))=24 and modo="M" then
			'response.redirect "mensajes.asp?proceso=medicina"
			session("BloqueoCampus_Alu") =1
			mensaje = mensaje & "MEDICINA|"
		end if
		'if cdbl(session("codigo_cpf"))=11 and modo="M" then
		'	response.redirect "mensajes.asp?proceso=enfermeria"
		'end if
		if cdbl(session("codigo_cpf"))=31 and modo="M" then
			'response.redirect "mensajes.asp?proceso=ODO"
			session("BloqueoCampus_Alu") =1
			mensaje = mensaje & "ODO|"
		end if

		'if cdbl(session("codigo_cpf"))=3 and session("cicloing_alu")="2009-I" then
		'	response.redirect "mensajes.asp?proceso=BMG"
		'end if

		'****************************************
		'Verificar acceso a pre-matrcula
		'****************************************
		if modo="M" then
			Dim Obj
			Dim rsMatricula
			Dim pagina
			
			Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
				Obj.AbrirConexion
					set rsMatricula=Obj.Consultar("VerificarAccesoMatriculaEstudiante","FO",session("codigo_alu"),session("Codigo_Cac"),session("Codigo_Pes"))
				Obj.CerrarConexion
			Set Obj=nothing
			pagina=rsMatricula(0)
			
			rsMatricula.close
			Set rsMatricula=nothing
			
			
			'if session("codigo_alu")=11268 then
			'	session("codigo_cac")=35
			'	session("descripcion_cac")="2010-0"
			'	session("tipo_cac")="E"
			'	response.redirect("frmmatricula.asp")
			'end if
			response.redirect(pagina)
		end if
	
		'****************************************
		'Verificar acceso a AGREGADOS-RETIROS
		'****************************************
		if modo="Q" then
			response.redirect("frmagregadoretiro.asp")
			'response.redirect("mensajes.asp?proceso=F")
		end if
	
		'****************************************
		'Verificar acceso a historial acadmico
		'****************************************
		if modo="H" then
			response.redirect("historial.asp")
		end if

		'****************************************
		'Verificar acceso a AULA VIRTUAL
		'****************************************
		if modo="AV" then
			'response.redirect("aulavirtual/acceder.asp?tipo_uap=E")
			response.write("<script language='javascript'>window.top.location.href='aulavirtual/acceder.asp?tipo_uap=E'</script>")
		end if
	
   'end if
	
%>