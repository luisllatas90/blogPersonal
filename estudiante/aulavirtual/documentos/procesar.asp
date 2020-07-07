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

			end select
		end with
	Set documento=nothing
%>