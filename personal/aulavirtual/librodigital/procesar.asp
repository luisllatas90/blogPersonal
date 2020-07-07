<!--#include file="../../../NoCache.asp"-->
<!--#include file="../asignarvalores.asp"-->
<!--#include file="clslibrodigital.asp"-->
<%
Dim accion,contenido,idcontenido,idindice,archivo,numfila,idlibrodigital,idglosario
Dim titulolibro

	accion=request.querystring("accion")
	idlibrodigital=request.querystring("idlibrodigital")
	titulolibro=request.querystring("titulolibro")
	idcontenido=request.querystring("idcontenido")
	idglosario=request.querystring("idglosario")
	idindice=request.querystring("idindice")
	idindice=iif(idindice="",0,idindice)
	tipofuncion=session("tipofuncion")
	idusuario=session("codigo_usu")
	icursovirtual=session("iniciocursovirtual")
	fcursovirtual=session("fincursovirtual")
		
	set contenido=new clslibrodigital
		with contenido
			.restringir=session("idcursovirtual")
			Select case accion
				case "agregarlibrodigital"
					titulolibrodigital=request.form("titulolibrodigital")
					idlibrodigital=.agregar(session("idcursovirtual"),titulolibrodigital,descripcion,fechainicio,fechafin,idusuario,idtipopublicacion,0)
					if idlibrodigital>0 then
						.codigo_libro=idlibrodigital
						if idtipopublicacion=2 then
							.Cerrar="PL"
						else
							.Cerrar="M"
						end if
					else
						.cerrar="R"
					end if
				case "modificarlibrodigital"
					titulolibrodigital=request.form("titulolibrodigital")
					call .modificar(idlibrodigital,titulolibrodigital,descripcion,fechainicio,fechafin)
					.Cerrar="M"
				
				case "eliminarlibrodigital"
					mensaje=.eliminar(idlibrodigital)
					if mensaje="" then
						.Cerrar="E"			
					else
						response.write "<script>alert('" & mensaje & "');history.back(-1)</script>"
					end if
					
				case "agregarindice" 'Exclusivo para doc, de tipo enlace web
					Controlescontenido
					call .agregarcontenido("I",ordencontenido,titulocontenido,"",idlibrodigital,fechainicio,fechafin,idindice,idestadorecurso)
					.cerrar="M"
				case "modificarindice"
					Controlescontenido
					call .modificarcontenido(idcontenido,ordencontenido,titulocontenido,"",fechainicio,fechafin,idindice,idestadorecurso)
					.Cerrar="M"
				case "eliminarindice"
					ArrTemp=.consultar("2",idcontenido,"","")
					mensaje=.eliminarcontenido(idcontenido)
					.codigo_libro=idlibrodigital
					.titulo_libro=titulolibro
					if mensaje="" then
						call .eliminararchivosdir(ArrTemp)
						.Cerrar="EI"						
					else
						response.write "<script>alert('" & mensaje & "');history.back(-1)</script>"
					end if
				case "modificarcontenidoweb"
					call .modificarwebcontenido(idcontenido,request.form("web"))					
				
				case "agregarglosario"
					tituloglosario=request.form("tituloglosario")
					call .agregarglosario(idlibrodigital,tituloglosario,descripcion,idusuario)
					.cerrar="G"
				case "modificarglosario"
					tituloglosario=request.form("tituloglosario")
					call .modificarglosario(idglosario,tituloglosario,descripcion)
					.cerrar="G"
				case "eliminarglosario"
					call .eliminarglosario(idglosario)
					.cerrar="G"
			end select
		end with
	Set contenido=nothing
%>