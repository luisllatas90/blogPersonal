<!--#include file="../../../../NoCache.asp"-->
<!--#include file="../../../../funciones.asp"-->
<%
ValidarSesion session("codigo_usu"),"../../../../"

accion=request.querystring("accion")
codigo_cup=request.QueryString("codigo_cup")
codigo_pes=request.querystring("codigo_pes")
codigo_cac=request.querystring("codigo_cac")
codigo_cur=request.querystring("codigo_cur")
codigo_test=request.querystring("mod")
usuario=session("Usuario_bit")
SoloPrimerCiclo=request.Form("chkPrimerCiclo")

	if accion="agregarcursoprogramado" then
		on error resume next
		'response.write replace(request.form,"&","<br>")
		'response.write replace(request.querystring,"&","<br>")
		codigoElegido_pes=request.querystring("codigoElegido_pes")
		arrCodigo_cur=split(request.form("txtcodigo_cur"),",")
		arrCodigo_pes=split(request.form("txtcodigo_pes"),",")
		
		Set objProgramar=Server.CreateObject("PryUSAT.clsAccesoDatos")
			objProgramar.AbrirConexion
			for i=0 to ubound(arrCodigo_cur)
	 			numGrupos=Request.Form("txtGrupos" & i)
	 			codigo_Cur=trim(arrCodigo_Cur(i))
	 			codigo_pes=trim(arrCodigo_pes(i))
	 			vacantes_Cup=Request.Form("txtVacantes" & i)
	 			fechainicio_cup=Request.Form("txtInicio" & i)
				fechafin_cup=Request.Form("txtFin" & i)
	 			fecharetiro_cup=Request.Form("txtRetiro" & i)
	 			arrcodigo_ceq=trim(Request.Form("chkEq" & i))
				obs_cup=request.Form("txtobs_cup" & i)
				codigodac_cup=request.Form("cbocodigodac_cup" & i)
	 			
	 			if arrcodigo_ceq="" then arrcodigo_ceq=null
				if obs_cup="" then obs_cup=null
	 			if numGrupos="" then numGrupos=1
	 			if vacantes_Cup="" then vacantes_Cup=30
	 			
				mensaje=objProgramar.Ejecutar("AgregarCursoProgramado",true,numGrupos,codigo_cac,codigo_pes,codigo_cur,"F",vacantes_cup,"C",fechainicio_cup,fechafin_cup,fecharetiro_cup,arrcodigo_ceq,session("codigo_usu"),obs_cup,codigodac_cup,SoloPrimerCiclo,null)
			next
			objProgramar.CerrarConexion
		Set objProgramar=nothing
		
		if err.number>0 then
			mensaje="Ha ocurrido un error al Grabar la Programación \n" & err.description
			pagina="history.back(-1)"
		else
			pagina="location.href='frmnuevaprogramacion.asp?codigo_pes=" & codigoElegido_pes & "&codigo_cac=" & codigo_cac & "'"
		end if
		response.write "<script>alert('" & mensaje & "');" & pagina & "</script>"
	end if
	
	if accion="modificarcursoprogramado" then
	
		grupohor_cup=request.form("txtGrupos")
		estado_cup=request.form("cboEstado")
		vacantes_cup=request.form("txtVacantes")
		fechainicio_cup=request.form("txtInicio")
		fechafin_cup=request.form("txtFin")
		fecharetiro_cup=request.form("txtRetiro")
		obs_cup=request.form("txtobs_cup")
		codigodac_cup=request.form("cbocodigodac_cup")

		Set objProgramar=Server.CreateObject("PryUSAT.clsAccesoDatos")
			objProgramar.AbrirConexion
							
			mensaje=objProgramar.Ejecutar("ModificarCursoProgramado",true,codigo_cup,grupohor_cup,estado_cup,vacantes_cup,fechainicio_cup,fechafin_cup,fecharetiro_cup,session("codigo_usu"),obs_cup,codigodac_cup, soloPrimerCiclo,null)
			
			objProgramar.CerrarConexion
		Set objProgramar=nothing
		response.write "<script>alert('" & mensaje & "');window.opener.location.reload();window.close()</script>"
	end if
	
	if accion="eliminarcursoprogramado" then
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			obj.AbrirConexion
				mensaje=obj.Ejecutar("EliminarCursoProgramado",true,codigo_cup,session("codigo_usu"),null)
			obj.CerrarConexion
		Set obj=nothing
		codigo_cupPadre=request.querystring("codigo_cupPadre")
				
		pagina="detallecursoprogramado.asp?codigo_cup=" & codigo_cupPadre & "&codigo_pes=" & codigo_pes & "&codigo_cac=" & codigo_cac & "&codigo_cur=" & codigo_cur
		
		response.write "<script>alert('" & mensaje & "');location.href='" & pagina & "'</script>"
	end if
	
	if accion="agruparcursos" then
	
		codigo_cpf=request.querystring("codigo_cpf")
		codigo_cac=request.querystring("codigo_cac")
		Hijos=verificacomaAlfinal(Request.querystring("Hijos"))
		Padre=request.querystring("Padre")
	
		arrHijos=split(Hijos,",")			
	
		Set ObjCarga= Server.CreateObject("PryUSAT.clsAccesoDatos")
			ObjCarga.AbrirConexion
			for i=0 to Ubound(arrHijos)
				codigo_cupHijo=trim(arrHijos(i))
				
				if codigo_cupHijo<>trim(Padre) then
		
    				Call ObjCarga.Ejecutar("AgruparCursosProgramados",true,Padre,codigo_cupHijo,codigo_cac,usuario,null)
					
				end if
			next
			ObjCarga.CerrarConexion
			Set ObjCarga=nothing			
	
			response.redirect "frmagruparcursosprof.asp?codigo_cpf=" & codigo_cpf & "&codigo_cac=" & codigo_cac & "&mod=" & codigo_test
       
	end if

	if accion="desagruparcursos" then
		codigo_cpf=request.querystring("codigo_cpf")
		codigo_cac=request.querystring("codigo_cac")
		Hijos=verificacomaAlfinal(Request.querystring("Hijos"))
	
		arrHijos=split(Hijos,",")			
		Set ObjCarga= Server.CreateObject("PryUSAT.clsAccesoDatos")
			ObjCarga.AbrirConexion
				for i=0 to Ubound(arrHijos)
					codigo_cupHijo=trim(arrHijos(i))
					if codigo_cupHijo<>"" then
						Call ObjCarga.Ejecutar("DesAgruparCursosProgramados",false,codigo_cupHijo,codigo_cupHijo,codigo_cac,usuario,null)
					end if
				next
			ObjCarga.CerrarConexion
		Set ObjCarga=nothing
		
		response.redirect "frmagruparcursos.asp?codigo_cpf=" & codigo_cpf & "&codigo_cac=" & codigo_cac & "&mod=" & codigo_test
	end if
%>