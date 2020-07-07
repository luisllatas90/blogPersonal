<!--#include file="../../../../NoCache.asp"-->
<!--#include file="../../../asignarvalores.asp"-->
<!--#include file="../../../../clsmensajes.asp"-->
<%
'call Enviarfin(session("codigo_usu"),"../../../../")
'response.write session("codigo_usu")

'on error resume next
accion=Request.querystring("accion")
codigo_mat=request.querystring("codigo_mat")
codigo_cup=request.querystring("codigo_cup")
codigo_cac=request.querystring("codigo_cac")
codigo_alu=request.querystring("codigo_alu")

	if accion="matricular" then
		dim alumno
	
		asignarcontrolesmatricula
		
		tipo_cac=session("tipo_cac")
		codigo_cac=session("Codigo_Cac")
		usuario=session("Usuario_bit")
		IP=session("Equipo_bit")
		redim alumno(0)
		alumno(0)=session("codigo_alu")

		'-------------------------------------------------------------------------
		'Grabar Matrícula, Detalle Matricula y Actualizar Pensión en Matrícula
		'-------------------------------------------------------------------------

		Set objmatricula= Server.CreateObject("PryUSAT.clsDatMatricula")	
			mensaje=Objmatricula.MatriculaAutomatica(alumno,tipo_cac,codigo_cac,"P","Matrícula por Administrador",0,0,usuario,IP,arrCP,ArrTC,ArrCR,ArrVD,"MAT","N")
		Set objMatricula=nothing
		
		if mensaje="OK" then
			mensaje="Se ha registrado correctamente la Matrícula"
			pagina="window.opener.location.reload();window.close()"
		else
			pagina="history.back(-1)"
		end if
		
		if mensaje<>"" then
			response.write "<script>alert('" & mensaje &"');" & pagina & "</script>"
		else
			response.write MensajeCliente("5","history.back(-1)")
		end if
	end if

	'Agregar nuevos cursos a la matricula, a través del administrador de matrícula
	if accion="agregarcursomatricula" then
		asignarcontrolesmatricula		
		
		Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
			objMatricula.AbrirConexion
				For I=LBound(ArrCP) to UBound(ArrCP)
					CP=Trim(ArrCP(I))
					TC=Trim(ArrTC(I))
					CR=Trim(ArrCR(I))
					VD=Trim(ArrVD(I))
					Call objmatricula.Ejecutar("AgregarDetalleMatricula",false,codigo_mat,CP,"A",TC,CR,VD,"P",0,"D",0,0,session("Usuario_bit"))
				next
				'------------------------------------------------------------------
				'Actualizar la pensión de los cursos normales y complementarios
				'------------------------------------------------------------------			
				call objmatricula.Ejecutar("ActualizarPensionMatricula",false,"AGR",codigo_cac,codigo_alu,codigo_mat,0)
			objMatricula.CerrarConexion
		Set objmatricula=nothing
		response.write(CerrarPopUp)
	end if
	
	'Retirar cursos de matricula,a través del administrador de matrícula
	if accion="cambiarestadocurso" then
		estado=request("optestado")
		if estado="" then estado="M"
			Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
				objMatricula.AbrirConexion
				call objmatricula.Ejecutar("DesactivarCursoMatriculado",false,estado,codigo_mat,codigo_cup,session("Usuario_bit"))
				objMatricula.CerrarConexion
			Set objmatricula=nothing
		if (estado="A" OR estado="N" OR estado="X") then
			response.redirect "frmadminmatricula.asp?modo=resultado&codigo_alu=" & session("codigo_alu") & "&codigo_cac=" & codigo_cac
		else
			response.redirect "detallematricula.asp?codigo_mat=" & codigo_mat & "&codigo_cac=" & codigo_cac
		end if
	end if
	
	if accion="registrarotramodalidadmatricula" then
		dim mensaje
		set obj =Server.CreateObject("PryUSAT.clsDatMatricula")
			tipo = request.querystring("tipo")
			codigo_cac  = request.querystring("codigo_cac")
			codigo_pes =request.querystring("codigo_pes")
			codigo_cur =request.querystring("codigo_cur")
			codigo_per =request.querystring("codigo_per")
			codigo_alu =request.querystring("codigo_alu")
			notaFinal_Dma = request.querystring("notaFinal_Dma")
			grupohor_cup=request.querystring("grupohor_cup")
			asistencias=request.querystring("asistencias")
			inasistencias=request.querystring("inasistencias")
			
			codigouniver_alu=session("codigoUniver_alu")
			if codigo_per="" then codigo_per=1
			if asistencias="" then asistencias=0
			if inasistencias="" then inasistencias=0
	
			mensaje=obj.GestionarCursoMatriculado(tipo,codigo_Cac,codigo_Pes,codigo_Cur,codigo_Per,codigo_Alu,notaFinal_Dma,grupohor_cup,asistencias,inasistencias,session("usuario_bit"),session("equipo_bit"))
		set obj=nothing
		
		call limpiarsesionalumno
		%>
		<script>
			if (confirm("<%=mensaje%>\n¿Desea visualizar el Historial de Matrícula para comprobar el Registro?\nElija Cancelar para seguir ingresando matrículas")==true)
				{location.href="../../clsbuscaralumno.asp?codigouniver_alu=<%=codigouniver_alu%>&pagina=matricula/consultapublica/historial.asp"}
			else
				{location.href="frmotramatricula.asp?codigo_pes=0"}
		</script>
	<%end if
	
	if accion="actualizartrasladoalumno" then
			codigo_pes =request.querystring("codigo_pes")
			codigo_alu =request.querystring("codigo_alu")
			cicloIng_Alu=session("cicloIng_alu")
			codigouniver_alu=session("codigoUniver_alu")

			'Por el momento va a ejecutar el sp clsfuncionesADO, OK
			set obj =Server.CreateObject("PryUSAT.clsAccesoDatos")
				obj.AbrirConexion
				call Obj.Ejecutar("ActualizarPlanEscuelaAlumno",true,"TI",cicloIng_Alu,codigo_alu,codigo_pes,session("usuario_bit"))
				'response.write cicloIng_Alu & "," & codigo_alu & "," & codigo_pes & "," & session("usuario_bit")
				obj.CerrarConexion
			Set obj=nothing
			
			call limpiarsesionalumno
			response.redirect("location.href='../../clsbuscaralumno.asp?codigouniver_alu=" & codigouniver_alu & "&pagina=matricula/mantenimiento/frmtraslados.asp'")
	end if
	
	if accion="CambiarCicloConvalidacion" then
		Server.ScriptTimeout=600
		asignarcontrolesmatricula
		tipoConvalidacion=request.querystring("tipoConvalidacion")
		codigoOrigen_cac=request.querystring("codigoOrigen_cac")
		codigoDestino_cac=request.querystring("codigoDestino_cac")

		Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
		objmatricula.AbrirConexion
		for i=lbound(ArrAlumnos) to Ubound(ArrAlumnos)
			codigo_alu=trim(ArrAlumnos(i))
			mensaje=Objmatricula.Ejecutar("GestionarConvalidacionAlumnoEnCicloIngreso",true,tipoConvalidacion,codigo_alu,codigoOrigen_cac,codigoDestino_cac,session("Usuario_bit"))
		Next
		objmatricula.CerrarConexion
		Set objMatricula=nothing
		response.write(CerrarPopUp)
	end if

	if accion="cambiaralumnosgrupohorario" then
		codigoDestino_cup=request.form("cbocursodestino")
		codigo_pes=request.querystring("codigo_pes")
		usuario=session("Usuario_bit")
		arrAlumnos=split(request.form("chk"),",")
	
		Set objProgramar=Server.CreateObject("PryUSAT.clsAccesoDatos")
			objProgramar.AbrirConexion
			'Recorrer alumnos seleccionados
			for i=0 to ubound(arrAlumnos)
				codigo_dma=trim(arrAlumnos(i))
				Call objProgramar.Ejecutar("CambiarAlumnosGrupoHorario",false,codigo_dma,codigoDestino_cup,usuario)
			next
			
			'Actualizar vacantes origen y destino
			call objProgramar.Ejecutar("CalcularNroMatriculados",false,"C",codigo_cup)
			call objProgramar.Ejecutar("CalcularNroMatriculados",false,"C",codigoDestino_cup)
			objProgramar.CerrarConexion
		Set objProgramar=nothing

		response.write "<script>alert('Se ha realizado con éxito el cambio de grupo horario a los estudiantes seleccionados');location.href='frmcambiogrupo.asp?codigo_cac=" & codigo_cac &"&codigo_pes=" & codigo_pes & "&codigo_cup=" & codigo_cup & "&codigodestino=" & codigoDestino_cup & "'</script>"
	end if

	if accion="anularmatricula" then
		tipo=request.querystring("tipo")
		codigo_alu=request.querystring("codigo_alu")
		obs=request.querystring("obs")
	
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			obj.AbrirConexion
			mensaje=Obj.Ejecutar("AnularMatricula",true,tipo,codigo_cac,codigo_alu,session("Usuario_bit"),obs,null)
			obj.CerrarConexion
		Set obj=nothing
		
		arrmensaje=split(mensaje,"|")
			
		response.write "<script>alert('" & arrmensaje(1) & "');location.href='" & arrmensaje(0) & "'</script>"
	end if

	'Verifica si hay errores
	If Err.Number<>0 then
		response.redirect "../../../../error.asp?Numero=" & Err.Number & "&Recurso=" & Err.Source & "&Descripcion=" & Err.description & "&rutaPagina=" & Request.ServerVariables("SCRIPT_NAME")
	End If

	'Verifica si hay errores
	If Err.Number<>0 then
		response.redirect "../../../../error.asp?Numero=" & Err.Number & "&Recurso=" & Err.Source & "&Descripcion=" & Err.description & "&rutaPagina=" & Request.ServerVariables("SCRIPT_NAME")
	End If
%>