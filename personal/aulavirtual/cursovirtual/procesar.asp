<!--#include file="../../../NoCache.asp"-->
<!--#include file="../asignarvalores.asp"-->
<!--#include file="clscursovirtual.asp"-->
<%
Dim accion,cursovirtual,idcursovirtual
	accion=request.querystring("accion")
	idcursovirtual=request.querystring("idcursovirtual")
	codigo_apl=request.querystring("codigo_apl")
	codigo_tfu=request.querystring("codigo_tfu")
	modo=request.querystring("modo")
	if modo="" then modo="M"

	if accion="agregarusuario" then
		idusuario=request.form("idusuario")
		nombreusuario=request.form("nombreusuario")
		email=request.form("email")
		modo=request.querystring("accion")
		idtabla=request.querystring("idtabla")
		nombretabla=request.querystring("nombretabla")
	
		Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			call obj.Ejecutar("DI_AgregarUsuario",false,idusuario,nombreusuario,email,session("idcursovirtual"))
			'response.write request.form & "<br>"
			'response.write request.querystring & "<br>"
		obj.CerrarConexion
		Set Obj=nothing

		response.redirect "listamatriculadoscurso.asp?idcursovirtual=" & session("idcursovirtual") & "&codigo_apl=" & session("codigo_apl") & "&codigo_tfu=" & session("codigo_tfu")
	end if

	set cursovirtual=new clscursovirtual
		with cursovirtual
			.restringir=session("codigo_usu")
			Select case accion
				case "agregarcurso"
					Controlescursovirtual
					idcursovirtual=.agregar(fechainicio,fechafin,titulocursovirtual,descripcion,modalidad,session("codigo_usu"),codigo_apl,1,0,creartemas,temapublico,integrartematarea,integrarrptatarea)
					if idcursovirtual>0 then
						.codigo_curso=idcursovirtual
						.CrearCarpetaCurso idcursovirtual
						.Cerrar="W"
					else
						.Cerrar="R"						
					end if
					
				case "modificarcurso"
					Controlescursovirtual
					call .modificar(idcursovirtual,fechainicio,fechafin,titulocursovirtual,descripcion,modalidad,creartemas,temapublico,integrartematarea,integrarrptatarea)
					Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		            obj.AbrirConexion
			        call obj.Ejecutar("AgregarBitacora",false,idcursovirtual, "cursovirtual", session("codigo_Usu"), "modificar")
		            obj.CerrarConexion
		            Set Obj=nothing
					.Cerrar="M"
				case "modificarwebcurso"
					web=request.form("web")
					call .modificarweb(idcursovirtual,web)
					.codigo_curso=idcursovirtual
					.Cerrar=modo
				case "eliminaraccesocursovirtual"
					arrCheck=Request("chk")
					arrUsuarios=split(arrCheck,",")
					.eliminaraccesocursovirtual(arrUsuarios)
					.codigo_curso=idcursovirtual
					.codigo_apl=codigo_apl
					.codigo_tfu=codigo_tfu
					.Cerrar="E"

				case "eliminarcurso"
					mensaje=.eliminar(idcursovirtual)
					if mensaje="" then
						call .EliminarCarpetaCurso(idcursovirtual)
						.Cerrar="M"
					else
						response.write "<script>alert('" & mensaje & "');history.back(-1)</script>"
					end if	
			end select
		end with
	Set cursovirtual=nothing
%>