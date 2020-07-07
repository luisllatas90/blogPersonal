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


			end select
		end with
	Set contenido=nothing
%>