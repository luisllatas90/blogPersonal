<!--#include file="../../../NoCache.asp"-->
<!--#include file="../asignarvalores.asp"-->
<!--#include file="clsforo.asp"-->
<%
Dim accion,idforo,idforomensaje,numfila

	accion=request.querystring("accion")
	idforo=request.querystring("idforo")
	idforomensaje=request.querystring("idforomensaje")
	rpta=request.querystring("rpta")
	numfila=request.querystring("numfila")
	idusuario=session("codigo_usu")
	if idforomensaje="" then idforomensaje=0
	tituloforo=request.querystring("tituloforo")
	idestadorecurso=request.querystring("idestadorecurso")
	
	set contenido=new clsforo
		with contenido
			.restringir=session("idcursovirtual")
			Select case accion
				case "agregarforo"
					Controlesforo
					idforo=.Agregar(session("idcursovirtual"),tituloforo,descripcion,fechainicio,fechafin,permitircalificar,tipocalificacion,numcalificacion,idusuario)
					if idforo>0 then
						.codigo_foro=idforo
						.codigo_fila=numfila
						.cerrar="M"
					else
						.cerrar="R"
					end if
				case "modificarforo"
					Controlesforo
					call .Modificar(idforo,tituloforo,descripcion,fechainicio,fechafin,permitircalificar,tipocalificacion,numcalificacion)
					.codigo_fila=numfila
					.Cerrar="M"
				
				case "eliminarforo"
					mensaje=.eliminar(idforo)
					if mensaje="" then
						.Cerrar="ET"
					else
						response.write "<script>alert('" & mensaje & "');history.back(-1)</script>"
					end if
					
				case "agregarforomensaje"
					ControlesMensajeForo
					titulomensaje=rpta & " " & titulomensaje
					call .Agregarmensaje(idforo,titulomensaje,descripcionmensaje,idusuario,idforomensaje)
					.Cerrar="M"
				
				case "modificarforomensaje"
					ControlesMensajeForo
					call .Modificarmensaje(idforomensaje,titulomensaje,descripcionmensaje)
					.Cerrar="M"
				
				case "eliminarforomensaje"
					mensaje=.eliminarmensaje(idforomensaje)
					if mensaje="" then
						response.redirect "listamensajes.asp?idforo=" & idforo & "&tituloforo=" & tituloforo & "&idestadorecurso=" & idestadorecurso
					else
						response.write "<script>alert('" & mensaje & "');history.back(-1)</script>"
					end if
				case "calificarmensaje"
					Call .calificarmensaje(idforomensaje,idusuario,numcalificacion)
					response.redirect "listamensajes.asp?idforo=" & idforo & "&tituloforo=" & tituloforo & "&idestadorecurso=" & idestadorecurso
			end select
		end with
	Set contenido=nothing
	
%>