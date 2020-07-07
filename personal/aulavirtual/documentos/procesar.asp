<!--#include file="../../../NoCache.asp"-->
<!--#include file="../asignarvalores.asp"-->
<!--#include file="clsdocumento.asp"-->
<%
Dim accion,documento,iddocumento,idcarpeta,archivo,numfila,idversion

	accion=request.querystring("accion")
	iddocumento=request.querystring("iddocumento")
	idcarpeta=request.querystring("idcarpeta")
	tipodoc=Request.querystring("tipodoc")
	archivo= Request.querystring("archivo")
	numfila=request.querystring("numfila")
	idversion=request.querystring("idversion")
	
	tipofuncion=session("tipofuncion")
	idusuario=session("codigo_usu")
	idcursovirtual=session("idcursovirtual")
	icursovirtual=session("iniciocursovirtual")
	fcursovirtual=session("fincursovirtual")
		
	set documento=new clsdocumento
		with documento
			.restringir=session("idcursovirtual")
			Select case accion
				case "agregardocumento" 'Exclusivo para doc, de tipo enlace web
					ControlesDocumento
					iddocumento=.agregar("L","enlace.htm",titulodocumento,idusuario,fechainicio,fechafin,descripcion,0,idcarpeta,idcursovirtual,0,0,1)
					if iddocumento>0 then
						.codigo_doc=iddocumento
						response.redirect "link.asp?iddocumento=" & iddocumento
					else
						.cerrar="R"
					end if
				case "modificardocumento"
					Controlesdocumento
					call .modificar(iddocumento,archivo,titulodocumento,idusuario,fechainicio,fechafin,descripcion,0,0)
					.codigo_dir=idcarpeta
					.codigo_doc=iddocumento
					.codigo_fila=numfila
					if tipodoc="L" then
						response.redirect "link.asp?iddocumento=" & iddocumento
					else
						.Cerrar="MD"
					end if
				case "eliminardocumento"
					mensaje=.eliminar("A",iddocumento,archivo)
					if mensaje="" then
						.Cerrar="ED"
					else
						response.write "<script>alert('" & mensaje & "');history.back(-1)</script>"
					end if
				case "modificarversion"
					tituloversion=request.querystring("tituloversion")
					publica=request.querystring("publica")
					bloqueada=request.querystring("bloqueada")
					call .modificarversion(idversion,tituloversion,publica,bloqueada)
					.codigo_ver=idversion
					.codigo_doc=iddocumento
					.Cerrar="MV"
				case "eliminarversion"
					call .EliminarVersion(idversion,archivo)
				
				case "agregarcarpeta" 'Exclusivo para crear carpetas
					ControlesDocumento
					iddocumento=.agregar("C","",titulodocumento,idusuario,fechainicio,fechafin,descripcion,0,idcarpeta,idcursovirtual,0,escritura,idtipopublicacion)
					if iddocumento>0 then
						.codigo_doc=iddocumento
						if idtipopublicacion=2 then
							.Cerrar="PL"
						else
							.Cerrar="DIR"
						end if
					else
						.cerrar="R"
					end if
				case "modificarcarpeta"
					Controlesdocumento
					call .modificar(iddocumento,archivo,titulodocumento,idusuario,fechainicio,fechafin,descripcion,0,escritura)
					.Cerrar="DIR"
				case "eliminarcarpeta"
			  		ArrTemp=.Consultar("8",iddocumento,"","","")
					mensaje=.eliminar("C",iddocumento,"")
					if mensaje="" then
						call .eliminararchivosdir(ArrTemp)
						.Cerrar="ED"
					else
						response.write "<script>alert('" & mensaje & "');history.back(-1)</script>"
					end if
				
				case "moverdocumento"
					.mover tipodoc,iddocumento,idcarpeta
					.Cerrar="MOV"
			end select
		end with
	Set documento=nothing
%>