<!--#include file="../../NoCache.asp"-->
<!--#include file="../asignarvalores.asp"-->
<%

Accion=request.querystring("Accion")
codigo_apl=request.querystring("codigo_apl")
codigo_men=request.querystring("codigo_men")
	
	Select case Accion
	    case "clonarpermisos"
	        UsuarioAClonar = request.Form("cboPersonal") 
            UsuarioDestino = request.Form("cboPersonalClonar") 
	        Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
				obj.AbrirConexion                
                call Obj.Ejecutar("SEG_ClonarPermisosUsuario",false,UsuarioAClonar,UsuarioDestino)				
				obj.CerrarConexion
			set obj=nothing
			
			response.write "<script>window.opener.location.reload();window.close()</script>"
			
		case "agregarpermisos"
			tipo_uap=request.form("cbocodigo_tpe")
			codigo_tfu=request.form("cbocodigo_tfu")
			Control=Request("ListaPara")
			Coleccion=split(Control,",")

			Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
				obj.AbrirConexion
			
			For I=LBound(Coleccion) to UBound(Coleccion)
				codigo_uap=Trim(Coleccion(I))			
				call Obj.Ejecutar("agregarusuarioaplicacion",false,tipo_uap,codigo_uap,codigo_apl,codigo_tfu,0,0)
			Next
				obj.CerrarConexion
			set obj=nothing
			
			response.write "<script>window.opener.location.reload();window.close()</script>"

		case "agregarfuncionusuario"
		
			'response.write replace(Request.Form,"&","<br>")
			Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
				obj.AbrirConexion
		
			For i=1 to Request.Form("nocheck")
				codigo_tfu=Request.Form("codigo_tfu" & i)
				if codigo_tfu<>"" then
					'---------------------------------------------------------
					'Si SE ha marcado el check
					'---------------------------------------------------------
					If (Request.Form("chkfuncion" & i ) <> "") then
						call Obj.Ejecutar("agregarfuncionusuario",false,codigo_apl,codigo_tfu,0)
					End If
					
					'---------------------------------------------------------
					'Si NO se ha marcado el check
					'---------------------------------------------------------	
					If(Request.Form("chkfuncion" & i ) = "") Then
						Call Obj.Ejecutar("eliminarfuncionusuario",false,codigo_apl,codigo_tfu,0)	
					End If
				end if
			Next
				obj.CerrarConexion
			set obj=nothing		
			
			response.redirect "listafunciones.asp?codigo_apl=" & codigo_apl

		case "agregarmenufuncionusuario"
			codigo_tfu=request.form("cbocodigo_tfu")
			Control=Request("chkmenu")
			Coleccion=split(Control,",")
			'response.write replace(request.form,"&","<br>")
			
			Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
			obj.AbrirConexion	
			
				'Limpiar permisofuncionusuario
				call Obj.Ejecutar("EliminarPermisoFuncionUsuario",false,codigo_apl,codigo_tfu)
				
				For I=LBound(Coleccion) to UBound(Coleccion)
					codigo_men=Trim(Coleccion(I))			
					call Obj.Ejecutar("agregarmenufuncionusuario",false,codigo_apl,codigo_tfu,codigo_men)
				Next
				obj.CerrarConexion
			Set obj=nothing			
			
			response.redirect "listafunciones.asp?codigo_apl=" & codigo_apl & "&codigo_tfu=" & codigo_tfu
	
		'Permite Agregar datos a la Tabla menuaplicacion
		case "Agregarmenuaplicacion"
			asignarcontrolesmenuaplicacion
			codigoraiz_men=request.querystring("codigoRaiz_men")

			Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
				obj.AbrirConexion
					call Obj.Ejecutar("agregarmenuaplicacion",false,descripcion_men,enlace_men,codigo_apl,codigoraiz_men,icono_men,iconosel_men,expandible_men,nivel_men,orden_men,variable_men)
				obj.CerrarConexion
			set obj=nothing			
			response.write "<script>window.opener.location.reload();window.close()</script>"

		'Permite Modificar datos a la Tabla menuaplicacion
		case "Modificarmenuaplicacion"
			asignarcontrolesmenuaplicacion
			codigoraiz_men=request.querystring("codigoRaiz_men")

			Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
				obj.AbrirConexion	
					call Obj.Ejecutar("modificarmenuaplicacion",false,codigo_apl,codigo_men,descripcion_men,enlace_men,codigoraiz_men,icono_men,iconosel_men,expandible_men,nivel_men,orden_men,variable_men)
				obj.CerrarConexion
			set obj=nothing
			response.write "<script>window.opener.location.reload();window.close()</script>"
			
		case "Eliminarmenuaplicacion"
			codigo_men=request.querystring("codigo_men")
			codigo_apl=request.querystring("codigo_apl")

			Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
				obj.AbrirConexion						
					call Obj.Ejecutar("eliminarmenuaplicacion",false,codigo_men)
				obj.CerrarConexion
			set obj=nothing			
		response.redirect "listamenus.asp?codigo_apl=" & codigo_apl
		
		case "agregarpermisorecurso"
		usuarioaut_acr=request.querystring("usuarioaut_acr")
		nombretbl_acr=request.querystring("nombretbl_acr")
		operador=471'session("codigo_usu")
		response.write request.form("nocheck")
		        
		Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")						
				For i=1 to request.form("nocheck")
					codigotbl_acr=trim(Request.form("txtcodigotbl_acr" & i))
					if codigotbl_acr<>"" then
						'---------------------------------------------------------
						'Si SE ha marcado el check
						'---------------------------------------------------------
						If (Request.Form("chk" & i ) <> "") then
						    obj.AbrirConexionTrans
							call Obj.Ejecutar("AgregarAccesoRecurso",false,"S",operador,usuarioaut_acr,nombretbl_acr,codigotbl_acr,null,null,null,null,null,null,null,null)
					        Obj.CerrarConexionTrans
						End If
						
						'---------------------------------------------------------
						'Si NO se ha marcado el check
						'---------------------------------------------------------	
						If(Request.Form("chk" & i ) = "") Then
							obj.AbrirConexionTrans
							call Obj.Ejecutar("AgregarAccesoRecurso",false,"E",operador,usuarioaut_acr,nombretbl_acr,codigotbl_acr,null,null,null,null,null,null, null,null)
						    Obj.CerrarConexionTrans
						End If
					end if
				Next			
		set obj=nothing
		response.redirect("tblrecursos.asp?usuarioaut_acr=" & usuarioaut_acr & "&nombretbl_acr=" & nombretbl_acr)
        
	end select
	
%>