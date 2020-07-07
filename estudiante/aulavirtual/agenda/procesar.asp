<!--#include file="../../../NoCache.asp"-->
<!--#include file="../asignarvalores.asp"-->
<!--#include file="clsagenda.asp"-->
<%
Dim accion,agenda,idagenda
	accion=request.querystring("accion")
	idagenda=request.querystring("idagenda")
	
	set agenda=new clsagenda
		with agenda
			.restringir=session("idcursovirtual")
			Select case accion
				case "agregaragenda"
					ControlesAgenda
					idagenda=.agregar(tituloagenda,fechainicio,fechafin,lugar,descripcion,contactos,idcategoria,session("idcursovirtual"),session("codigo_usu"),prioridad,idtipopublicacion)
					if IdAgenda>0 then
						.codigo_agenda=idagenda
						if idtipopublicacion=2 then
							.Cerrar="A"
						else
							.Cerrar="M"
						end if
					else
						Cerrar="R"
					end if
			end select
		end with
	Set agenda=nothing
%>