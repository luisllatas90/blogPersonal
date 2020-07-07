<!--#include file="../../../NoCache.asp"-->
<!--#include file="../../../funcionesaulavirtual.asp"-->
<!--#include file="asignarcontroles.asp"-->

<%
if session("codigo_usu")="" then response.Redirect "../../../tiempofinalizado.asp"
accion=request.QueryString("accion")
idarchivo=request.querystring("idarchivo")
numeroexpediente=request.querystring("numeroexpediente")
idmovimiento=request.querystring("idmovimiento")

if accion="agregararchivo" then
	controlesarchivo
	Set Obj= Server.CreateObject("aulavirtual.clsRecepcion")
			idarchivoNuevo=Obj.agregararchivo(session("idanio"),fechaarchivo,horaarchivo,numeroexpediente,numerotipo,idtipoarchivo,idprocedencia,iddestinatario,asunto,obs,session("codigo_usu"),prioridad)
	Set Obj=nothing
	if idarchivonuevo<>0 then%>
	<script language=javascript>
		var confirmar=confirm("Desea generar movimiento al documento registrado?")
		if (confirmar==true){
			location.href="movimiento.asp?accion=agregarmovimiento&idarchivo=<%=idarchivonuevo%>"
		}
		else{
			window.opener.location.reload()
			window.close()
		}	
	</script>
	<%else%>
		<script language=javascript>
			alert("No se pudo registrar correctamente al documento")
			location.href="documento.asp?accion=agregararchivo"
		</script>
	<%end if
end if

if accion="modificararchivo" then
	controlesarchivo
	Set Obj= Server.CreateObject("aulavirtual.clsRecepcion")
		call Obj.modificararchivo(idarchivo,session("idanio"),fechaarchivo,horaarchivo,numeroexpediente, numerotipo,idtipoarchivo,idprocedencia,iddestinatario,asunto,obs,prioridad,session("codigo_usu"))
	Set obj=nothing%>
	<script language=javascript>
		window.opener.location.reload()
		window.close()
	</script>
<%end if

if accion="eliminararchivo" then
	Set Obj= Server.CreateObject("aulavirtual.clsRecepcion")
		call obj.eliminararchivo(idarchivo)
	Set Obj=nothing%>
	<script language=javascript>
		parent.location.reload()
		window.close()
	</script>	
<%end if

'Permite Agregar datos a la Tabla movimientoarchivo
If accion="agregarmovimiento" Then
	controlesmovimiento
	Set objmovimientoarchivo= Server.CreateObject("aulavirtual.clsRecepcion")
		call objmovimientoarchivo.agregarmovimientoarchivo(fechamovimiento,horamovimiento,idarchivo,idareaarchivo,idareaarchivo2,otrodestino,numcargo,motivo,session("codigo_usu"),ip,confirmacion)
	Set objmovimientoarchivo=Nothing
	'response.write request.form
%>

	<script language=javascript>
		window.opener.location.reload()
		window.close()
	</script>	
<%end if

'Permite Modificar datos a la Tabla movimientoarchivo
If accion="modificarmovimiento" Then
	controlesmovimiento
	Set objmovimientoarchivo= Server.CreateObject("aulavirtual.clsRecepcion")
		call objmovimientoarchivo.modificarmovimientoarchivo(idmovimiento,fechamovimiento,horamovimiento,idarchivo,idareaarchivo,idareaarchivo2,otrodestino,numcargo,motivo,confirmacion)
	Set objmovimientoarchivo=Nothing%>
	<script language=javascript>
		window.opener.location.reload()
		window.close()
	</script>	
<%end if

'Permite Eliminar datos a la Tabla movimientoarchivo
If accion="eliminarmovimiento" Then
	Set objmovimientoarchivo= Server.CreateObject("aulavirtual.clsRecepcion")
		Call objmovimientoarchivo.eliminarmovimientoarchivo(idmovimiento)
	Set objmovimientoarchivo=Nothing
	response.redirect "listamovimientos.asp?idarchivo=" & idarchivo & "&numeroexpediente=" & numeroexpediente
End If
%>