<!--#include file="../../../NoCache.asp"-->
<!-- #include file="../../../funcionesaulavirtual.asp" -->
<!-- #include file="../../../clssubir.asp" -->
<%
if session("codigo_usu")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

	Response.Expires = -1
	Server.ScriptTimeout = 600

	dim directorio,ArchivoNuevo,idusuario,ArchivoOriginal

	idusuario=session("codigo_usu")
	idcursovirtual=request.querystring("idcursovirtual")

	'*************************************************************************************
	'Especificar la ruta dónde se guardará el archivo (directorio virtual)
	'*************************************************************************************
	directorio="t:\documentos aula virtual\archivoscv\" & idcursovirtual & "\images\"
	ArchivoNuevo=GenerarNombreArchivo(session("codigo_usu"))

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
		'No hay ninguno		
	
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
			Call Obj.AgregarArchivosMultimedia(idusuario,ArchivoNuevo,"cursovirtual",idcursovirtual,idcursovirtual)
  		Set Obj=Nothing
	Set Cargar=nothing
	%>
	<script>
		window.returnValue="../../../archivoscv/<%=session("idcursovirtual") & "/images/" & ArchivoNuevo%>"
		window.close()
	</script>	
%>