<!-- #include file="../../../funcionesaulavirtual.asp" -->
<!-- #include file="../../../clssubir.asp" -->
<%
if session("idcursovirtual")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

	dim idtareausuario,idtarea,obs
  	dim directorio,ArchivoNuevo,ArchivoOriginal
  	
  	idtarea=request.querystring("idtarea")
  	idtareausuario=request.querystring("idtareausuario")
  	if idtareausuario="" then idtareausuario=0


	'*************************************************************************************
	'Especificar la ruta dónde se guardará el archivo (directorio virtual)
	'*************************************************************************************
	directorio="t:\documentos aula virtual\archivoscv\" & session("idcursovirtual") & "\tareas\"

	tipofuncion=session("tipofuncion")
	idusuario=session("codigo_usu")
	icursovirtual=session("idcursovirtual")
	ArchivoNuevo=GenerarNombreArchivo(session("codigo_usu"))

	Private Function cerrarventanaerror()
		cerrarventanaerror="<script>alert(""No se pudo registrar correctamente el documento. Por favor intente denuevo"");history.back(-1)</script>"
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
		obs=Cargar.Fields("obs").Value
	
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

		Set Obj= Server.CreateObject("AulaVirtual.clstarea")
	  		call Obj.AgregarTareaUsuario(idtarea,idusuario,ArchivoNuevo,idtareausuario,0,0,obs,session("idvisita_sistema"),session("idcursovirtual"))
		Set Obj=Nothing
	Set Cargar=nothing

	if idtareausuario=0 then
		response.write "<script>window.opener.location.reload();window.close()</script>"
	else
		response.write "<script>window.parent.location.reload();window.close()</script>"
	end if	
%>