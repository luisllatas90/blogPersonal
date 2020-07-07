<%
codigouniver_alu=Request.querystring("codigouniver_alu")
pagina=Request.querystring("pagina")
rutaactual=Request.querystring("rutaactual")

		Set ObjEstudiante= Server.CreateObject("PryUSAT.clsDatAplicacion")
			Set rsAlumno=ObjEstudiante.Validar("RS","E",codigouniver_alu,"")
		Set ObjEstudiante=nothing
	
		If Not(rsAlumno.BOF AND rsAlumno.EOF) then
		
			session("rutaactual")=rutaactual
			session("urlpagina")=pagina
			session("codigoUniver_alu")=rsAlumno("codigoUniver_Alu")
			session("alumno")=rsAlumno("alumno")
			session("nombre_cpf")=rsAlumno("nombre_cpf")
			session("codigo_cpf")=rsAlumno("codigo_cpf")
			session("codigo_alu")=rsAlumno("codigo_alu")
			session("Codigo_Pes")=rsAlumno("codigo_pes")
			session("descripcion_Pes")=rsAlumno("descripcion_pes")
			session("nombre_min")=rsAlumno("nombre_min")
	
			session("CicloActual")=rsAlumno("cicloActual_Alu")
			session("nombre_fac")=rsAlumno("nombre_fac")
			session("descripcion_pes")=rsAlumno("descripcion_pes")
			session("TipoPension")=rsAlumno("tipopension_Alu")
			session("PrecioCredito")=rsAlumno("preciocreditoAct_Alu")
			session("MonedaPrecioCredito")=rsAlumno("monedapreccred_Alu")
			session("totalCredAprobados")=rsAlumno("totalCredAprobados")
			session("estadodeuda_alu")=rsAlumno("estadodeuda_alu")
			session("cicloIng_alu")=rsAlumno("cicloIng_alu")
			session("UltimaMatricula")=rsAlumno("UltimaMatricula")
			session("ActualizoDatos")=rsAlumno("ActualizoDatos")
			session("beneficio_alu")=rsAlumno("beneficio_alu")
			
			response.redirect pagina & "?modo=resultado&codigo_alu=" & session("codigo_alu")
		
		End if
		
		Set rsAlumno=nothing
%>