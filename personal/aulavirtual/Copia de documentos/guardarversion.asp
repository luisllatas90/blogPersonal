<!--#include file="../../../NoCache.asp"-->
<!-- #include file="../../../funciones.asp" -->
<!-- #include file="../../../clssubir.asp" -->
<%
if session("idcursovirtual")="" then response.redirect "../../../tiempofinalizado.asp"

	dim iddocumento,idversion,tituloversion,obs
  	dim directorio,archivo,ArchivoOriginal
  	
  	iddocumento=request.querystring("iddocumento")
	'directorio=Server.MapPath("../../../archivoscv")
	directorio="t:\documentos aula virtual\archivoscv\" & session("idcursovirtual") & "\documentos\"

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
		tituloversion=Cargar.Fields("tituloversion").Value
		obs=Cargar.Fields("obs").Value
		idversion=Cargar.Fields("txtidversion").Value
	
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
		Set Obj= Server.CreateObject("ControlDeMando.clsdocumento")
  			Call Obj.AgregarVersion(ArchivoOriginal,tituloversion,iddocumento,idusuario,0,0,1,obs,idversion)
		Set Obj=Nothing
	Set Cargar=nothing
	
	response.write "<script>window.parent.location.reload();window.close()</script>"
%>