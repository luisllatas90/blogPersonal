<!--#include file="../../NoCache.asp"-->
<%
	codigouniver_alu=Request.querystring("codigouniver_alu")
	codigo_cco=0'Request.querystring("codigo_cco")

	if Request.querystring("mod")<>"" then
		modulo=Request.querystring("mod")
		session("modulo")=modulo
	else
		modulo=session("modulo")
	end if
	
	pagina=Request.querystring("pagina")
	rutaactual=Request.querystring("rutaactual")

	Set objEstudiante=Server.CreateObject("PryUSAT.clsAccesoDatos")	
	objEstudiante.AbrirConexion
		Set rsAlumno=objEstudiante.Consultar("EVE_ConsultarAlumnoParaMatricula","FO",codigouniver_alu,modulo,codigo_cco)
	objEstudiante.CerrarConexion
	Set objEstudiante=nothing
	
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
			
			session("codigo_cco")=rsAlumno("codigo_cco")			
			session("codigo_test")=rsAlumno("codigo_test")
			session("condicion_alu")=rsAlumno("condicion_alu")			
			
			if cdbl(modulo)=-1 then
				response.redirect pagina & "?modo=resultado&codigo_alu=" & session("codigo_alu")		
			else		
				'Validar si es postulante			
				if (rsAlumno("condicion_alu")="P" and cdbl(modulo)<>1) then
					response.write("<script>alert('El POSTULANTE no corresponde al SISTEMA elegido.');history.back(-1)</script>")
				elseif (rsAlumno("condicion_alu")="I" and cdbl(rsAlumno("codigo_test"))<>cdbl(modulo)) then 'Validar si corresponde al módulo					
					response.write("<script>alert('El estudiante no corresponde al SISTEMA elegido.');history.back(-1)</script>")
				else
					if (rsAlumno("estadoactual_alu")=0) then 'Validar estado actual.
						response.write("<script>alert('El estudiante existe en la base de datos, pero está INACTIVO. Coordinar con la Oficina de Pensiones.');history.back(-1)</script>")
					else	
						response.redirect pagina & "?modo=resultado&codigo_alu=" & session("codigo_alu")
					end if
				end if
			end if
	Else%>
	<script>
		alert("El estudiante no existe en la BASE DE DATOS DEL SISTEMA")
		history.back(-1)
	</script>
	<%End if
		
	Set rsAlumno=nothing
%>