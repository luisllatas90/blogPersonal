<%
Dim strcurso,cadena

'****************************************************
'Validar que las variables sesion esten activas
'****************************************************

if session("codigo_usu")="" then
	response.write "<script>top.location.href='../../../../tiempofinalizado.asp'</script>"
end if

'****************************************************
'Datos del cursovirtual y del usuario logeado
'****************************************************
idusuario=session("codigo_usu")
ficursovirtual=session("iniciocursovirtual")
ffcursovirtual=session("fincursovirtual")
idvisita=session("idvisita_sistema")
idcursovirtual = session("idcursovirtual")
mododesarrollo=session("mododesarrollo")

accion=request.querystring("accion")

'****************************************************
'Datos del RECURSO
'****************************************************
codigo_tre=request.querystring("codigo_tre")
refcodigo_ccv=request.querystring("refcodigo_ccv")
if refcodigo_ccv="" then refcodigo_ccv=0

'****************************************************
'Verificar tipo de recurso
'****************************************************
strcurso="idusuario=" & idusuario & "&ficursovirtual=" & ficursovirtual & "&ffcursovirtual=" & ffcursovirtual & _
	"&accion=" & accion & "&idcursovirtual=" & idcursovirtual & _
	"&refcodigo_ccv=" & refcodigo_ccv & "&mododesarrollo_cv=" & mododesarrollo

select case codigo_tre
	case "I": cadena="frmcontenidocursovirtual.aspx?" & strcurso & "&codigo_ccv=" & request.querystring("codigo_ccv")
	case "A": cadena="frmdocumento2.aspx?" & strcurso & "&iddocumento=" & request.querystring("iddocumento")
	case "T": cadena="frmtarea.aspx?" & strcurso & "&idtarea=" & request.querystring("idtarea")
	case "E": cadena="frmevaluacion.aspx?" & strcurso & "&idevaluacion=" & request.querystring("idevaluacion")
	case "F": cadena="frmforo.aspx?" & strcurso & "&idforo=" & request.querystring("idforo")
	case "S": cadena="frmchat.aspx?" & strcurso & "&idchat=" & request.querystring("idchat")
	case "R": cadena="frmagenda.aspx?" & strcurso & "&idagenda=" & request.querystring("idagenda")
	case "TU": cadena="frmtareausuario.aspx?" & strcurso & "&idtarea=" & request.querystring("idtarea") & "&idtareausuario=" & request.querystring("idtareausuario")
end select

response.redirect("../../../librerianet/aulavirtual/" & cadena)
'response.write(cadena)
%>