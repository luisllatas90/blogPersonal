<!--#include file="../../../NoCache.asp"-->
<!--#include file="../asignarvalores.asp"-->
<!--#include file="clstarea.asp"-->
<%
Dim accion,numfila,idtarea,idtarearecurso,rutaarchivo,idtareausuario

	accion=request.querystring("accion")
	numfila=request.querystring("numfila")
	idtarea=request.querystring("idtarea")
	idtarearecurso=request.querystring("idtarearecurso")
	idusuario=session("codigo_usu")
		
	set contenido=new clstarea
		with contenido
			.restringir=session("idcursovirtual")
			Select case accion
				case "agregartarea"
					Controlestarea
					idtarea=.agregar(session("idcursovirtual"),titulotarea,descripcion,fechainicio,fechafin,idusuario,idtipopublicacion,1,permitirreenvio,calificacion,idtipotarea)
					if idtarea>0 then
						.codigo_tarea=idtarea
						if recurso="" then
							if idtipopublicacion=2 then
								.Cerrar="PL"
							else
								.Cerrar="M"
							end if
						else
							response.redirect recurso & "?idtarea=" & idtarea & "&idtipopublicacion=" &  idtipopublicacion
						end if
					else
						.cerrar="R"
					end if
				case "modificartarea"
					Controlestarea
					call .modificar(idtarea,titulotarea,descripcion,fechainicio,fechafin,permitirreenvio,calificacion)
					.codigo_tarea=idtarea
					.codigo_fila=numfila
					.Cerrar="MT"
				
				case "eliminartarea"
					mensaje=.eliminar(idtarea)
					if mensaje="" then
						.Cerrar="ET"			
					else
						response.write "<script>alert('" & mensaje & "');history.back(-1)</script>"
					end if
					
				case "asignarrecurso"
					txtArrArchivos=request.form("ArrArchivos")
					ArrArchivos=split(txtArrArchivos,",")
					call .AgregarTareaRecurso(idtarea,nombretabla,ArrArchivos)
					if idtipopublicacion=2 then
						.codigo_tarea=idtarea
						.Cerrar="PL"
					else
						.Cerrar="AT"
					end if
				
				case "eliminartarearecurso"
					call .EliminarTareaRecurso(idtarearecurso)
					.codigo_tarea=idtarea
					.Cerrar="ER"
					
				case "revisardocumento"
					archivo=request.querystring("archivo")
'**                 

            
                    '---------------------------------------------------------------------------------------------------------------
                    'Fecha: 29.10.2012
                    'Usuario: dguevara
                    'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                    '---------------------------------------------------------------------------------------------------------------
                    
					rutaarchivo="../../../archivoscv/" & archivo
					Set Obj= Server.CreateObject("AulaVirtual.clstarea")
						call Obj.AgregarTareaUsuario(idtarea,idusuario,archivo,0,idtarearecurso,0,"",session("idvisita_sistema"),session("idcursovirtual"))
					Set Obj=Nothing
					'Abrir documento marcado
					response.redirect(rutaarchivo)

				case "modificartareausuario"
					idtareausuario=request.querystring("idtareausuario")				
					obs=request.querystring("obs")
					bloqueada=request.querystring("bloqueada")
					call .Modificartareausuario(idtareausuario,obs,bloqueada)
					response.redirect "detalleversion.asp?idtareausuario=" & idtareausuario
			end select
		end with
	Set contenido=nothing
%>