<!--#include file="../../../NoCache.asp"-->
<!-- #include file="../../../funcionesaulavirtual.asp" -->
<!-- #include file="../clssubir.asp" -->
<%
if session("idcursovirtual")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

	Response.Expires = -1
	Server.ScriptTimeout = 600

	dim directorio,idtabla,ArchivoNuevo,tipofuncion,idusuario,icursovirtual,ficursovirtual,ffcursovirtual,ArchivoOriginal

	'*************************************************************************************
	'Especificar la ruta dónde se guardará el archivo (directorio virtual)
	'*************************************************************************************
	directorio="t:\documentos aula virtual\archivoscv\" & session("idcursovirtual") & "/documentos/"

	tipofuncion=session("tipofuncion")
	idusuario=session("codigo_usu")
	icursovirtual=session("idcursovirtual")
	ficursovirtual=session("iniciocursovirtual")
	ffcursovirtual=session("fincursovirtual")
	ArchivoNuevo=GenerarNombreArchivo(session("codigo_usu"))
	idtabla=request.querystring("idtabla")

	Private Function cerrarventanaerror()
		cerrarventanaerror="<script>alert(""No se pudo registrar correctamente el archivo. Por favor intente denuevo"");history.back(-1)</script>"
	End function

	Set Cargar = New clsSubir
		'*************************************************************************************
		'Permite extraer el nombre original del archivo que se va a subir
		'*************************************************************************************
		ArchivoOriginal=Cargar.Fields("file").FileName
		Extension=right(ArchivoOriginal,3)

		'*************************************************************************************
		'Asignar valores de controles del formulario
		'*************************************************************************************
		'No hay ninguno que se use
	
		'*************************************************************************************
		'Guardar archivo en el Disco Duro virtual, y tomar en cuenta lo sgte:
		'1. Si se guarda el archivo con el nombre original, entonces llamar a la variable ArchivoOriginal
		'2. Si se asignará un nombre al archivo, entonces llamar a la variable ArchivoNuevo +Extensión
		'*************************************************************************************
		ArchivoNuevo=ArchivoNuevo & "." & Extension
		Cargar("file").SaveAs directorio & ArchivoNuevo

		'*************************************************************************************
		'Guardar la información del archivo en la Base de Datos
		'*************************************************************************************
		Set Obj= Server.CreateObject("AulaVirtual.clsCategoria")
			Call Obj.AgregarArchivosMultimedia(idusuario,ArchivoNuevo,"contenido",idtabla)
  		Set Obj=Nothing
	Set Cargar=nothing
	%>
	<script>
		window.returnValue="../../../archivoscv/<%=session("idcursovirtual") & "/documentos/" & ArchivoNuevo%>"
		window.close()
	</script>	
%>