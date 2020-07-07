<!--#include file="../../../NoCache.asp"-->
<!--#include file="../../../funciones.asp"-->
<%
call Enviarfin(session("codigo_usu"),"../")

accion=Request.querystring("accion")

'on error resume next
	
		codigo_alu=request.querystring("codigo_alu")
		if codigo_alu="" then codigo_alu=session("codigo_alu")		
		'call Enviarfin(codigo_alu,"../")

		fechanacimiento_alu=request.form("dia") & "/" & request.form("mes") & "/" & request.form("anio")
		fechanacimiento_alu=cdate(fechanacimiento_alu)
		codigo_col=request.form("codigo_col")
		
		if codigo_col=0 then codigo_col=1
		if codigo_col="" then codigo_col=1
		
		bautismo=iif(request.form("bautismo")<>"",1,0)
		eucaristia=iif(request.form("eucaristia")<>"",1,0)
		confirmacion=iif(request.form("confirmacion")<>"",1,0)
		matrimonio=iif(request.form("matrimonio")<>"",1,0)
		orden=iif(request.form("orden")<>"",1,0)

		codigo_dfa=null
		centrotrabajo_dal=request.form("centrotrabajo_dal")
		telefonoTrabajo_Dal=request.form("telefonoTrabajo_Dal") & " " & request.form("tipoanexo")
		if request.form("tipoanexo")<>"" then
			telefonoTrabajo_Dal=telefonoTrabajo_Dal & " " & request.form("anexo")
		end if
		
		telefonoTrabajo_Dal=trim(telefonoTrabajo_Dal)
		pensionunivinstant=0
		codigo_ins=0
		operador=session("Usuario_bit")
		incluirDatos=request.querystring("incluirDatos")
		
		select case incluirDatos
			case "N","S":pag=";top.location.href='principal.asp'"
			case "A":pag=";window.opener.location.reload();window.close()"
		end select	
		
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			Obj.AbrirConexion
				mensaje=Obj.Ejecutar("Agregardatosalumno",true,codigo_alu, _
						fechanacimiento_alu, _
						request.form("sexo_alu"), _
						request.form("tipodocident_alu"), _
						request.form("nrodocident_alu"), _
						request.form("email_alu"), _
						request.form("email2_alu"), _
						request.form("PersonaFam_Dal"), _
						request.form("direccionfam_dal"), _
						request.form("urbanizacionfam_dal"), _
						request.form("distritofam_dal"), _
						request.form("telefonofam_dal"), _
						request.form("direccion_dal"), _
						request.form("urbanizacion_dal"), _
						request.form("distrito_dal"), _
						request.form("telefonoCasa_Dal"), _
						request.form("telefonoMovil_Dal"), _
						telefonoTrabajo_Dal, _
						request.form("religion_dal"), _
						request.form("estadocivil_dal"), _
						codigo_col, _
						request.form("nombrecolegio_dal"), _
						request.form("tipocolegio_dal"), _
						request.form("anioegresosec_dal"), _
						codigo_ins, _
						pensionunivinstant, _
						centrotrabajo_dal, _
						codigo_dfa, _
						operador,"S", _
						bautismo,eucaristia,confirmacion,matrimonio,orden,null)
				Obj.CerrarConexion
		Set obj=nothing
		'response.Write(mensaje)
		if mensaje <> "" then
		    response.write "<script>" & "alert('Se actualizaron correctamente los datos.')" & pag & "</script>"
		end if
		'for each control in Request.form
		'	response.write control & ":" & control.value & "<br>"
		'	i=i+1
		'next
		
If Err.Number<>0 then
    session("pagerror")="estudiante/procesardatosalumnope.asp"
    session("nroerror")=err.number
    session("descripcionerror")=err.description    
	response.write("<script>top.location.href='../error.asp'</script>")
End If
%>