<!--#include file="../../../NoCache.asp"-->
<!--#include file="../asignarvalores.asp"-->
<!--#include file="clsusuario.asp"-->
<%
Dim accion,usuario,idusuario,modo,sURL

	accion=request.querystring("accion")
	modo=request.querystring("modo")
	idusuario=request.querystring("idusuario")
	tipodoc=request.querystring("tipodoc")
	codigo_apl=request.querystring("codigo_apl")
	codigo_tfu=request.querystring("codigo_tfu")
	
	if tipodoc="" then tipodoc="0"
	sURL = Request.ServerVariables("SCRIPT_NAME")
	if Request.ServerVariables("QUERY_STRING") <> "" Then
		sURL = sURL & "?" & Request.ServerVariables("QUERY_STRING")
	end if
	
	set usuario=new clsusuario
		with usuario
			.restringir=session("codigo_usu")
			Select case accion
				case "agregarpermiso"
					Control=Request("ListaPara")
					Coleccion=split(Control,",")
					call .agregarpermiso(modo,nombretabla,idtabla,Coleccion,session("idcursovirtual"),tipodoc)

				case "agregarpermisocursovirtual"
					Control=Request("ListaPara")
					Coleccion=split(Control,",")
					call .agregarpermisocursovirtual(nombretabla,idtabla,Coleccion,idtabla,3)
					if modo="P" then
						response.redirect "../cursovirtual/enviaracceso.asp?idcursovirtual=" & idtabla
					else
						response.redirect "../cursovirtual/listamatriculadoscurso.asp?idcursovirtual=" & idtabla & "&codigo_apl=" & codigo_apl & "&codigo_tfu=" & codigo_tfu						
					end if
													
				case "eliminarpermiso"
					titulo=request.querystring("titulo")
					descripcion=request.querystring("descripcion")
				
					ID=Request.querystring("ID")
					Control=Request("chk")
					Coleccion=split(Control,",")
					call .eliminarpermiso(Coleccion,idtabla,nombretabla,session("idcursovirtual"),titulo,descripcion,modo)
				
				case "actualizartipopublicacion"
					call .actualizartipopublicacion(nombretabla,idtabla,cbxidtipopublicacion)
					.cerrar="M"
					
				case "enviarcorreo"
					asunto=request.form("txtasunto")
					mensaje=request.form("txtmensaje")
					origen=session("email")
					destinos=Request.form("ListaPara")
					arrdestinos=split(destinos,",")
					
					Set Obj= Server.CreateObject("AulaVirtual.clsEmail")
						For I=LBound(arrdestinos) to UBound(arrdestinos)
							destino=Trim(arrdestinos(I))
							if destino<>"" then
								Mensajes=Obj.EnviarCorreo(origen,asunto,mensaje,destino)
							end if
						Next
					Set Obj=nothing
					response.write "<script>alert('Se enviaron satisfactoriamente los correos electrónicos');top.window.close()</script>"

			End Select
		end with
	set usuario=nothing
%>