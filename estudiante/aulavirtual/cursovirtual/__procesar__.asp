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

	set cursovirtual=new clscursovirtual
		with cursovirtual
			.restringir=session("codigo_usu")
			' Select case accion
				' case "agregarcurso"
					' Controlescursovirtual
					' idcursovirtual=.agregar(fechainicio,fechafin,titulocursovirtual,descripcion,modalidad,session("codigo_usu"),codigo_apl,1,0,creartemas,temapublico,integrartematarea,integrarrptatarea)
					' if idcursovirtual>0 then
						' .codigo_curso=idcursovirtual
						' .CrearCarpetaCurso idcursovirtual
						' .Cerrar="W"
					' else
						' .Cerrar="R"						
					' end if
					

			' end select
		end with
	Set cursovirtual=nothing
%>