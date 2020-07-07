<% 
Response.Expires = -1
Server.ScriptTimeout = 600
%>
<!-- #include file="../../../subir.asp" -->
<%
Dim Directorio
Dim ArchivoNuevo
Dim RutaImagen

	directorio="t:\documentos aula virtual\archivoscv\" & idcursovirtual & "\images"
	Logeo=session("codigo_usu")
	FechaG=Replace(now(),"/",""):FechaG=Replace(FechaG,":",""):FechaG=Replace(FechaG," ","")
	FechaG=Replace(FechaG,"A.M.",""):FechaG=Replace(FechaG,"P.M.","")
	FechaG=Replace(FechaG,"a.m.",""):FechaG=Replace(FechaG,"p.m.","")
	ArchivoNuevo= Right(Logeo, Len(Logeo) - 5) & trim(FechaG)	
	idPregunta=Request.querystring("idPregunta")

	Set Cargar = New clsSubir
		'*************************************************************************************
		'Permite extraer el nombre original del archivo que se va a subir
		'*************************************************************************************
		ArchivoOriginal=Cargar.Fields("file").FileName
		Extension=right(ArchivoOriginal,3)
		
		'*************************************************************************************
		'Asignar valores de controles del formulario
		'*************************************************************************************
		URL=Cargar.Fields("URL").Value
	
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
			Call Obj.AgregarArchivosMultimedia(idusuario,ArchivoNuevo,"pregunta",idpregunta,idEvaluacion)
  		Set Obj=Nothing

	Set Cargar=nothing
	
	response.redirect "listapreguntas.asp?idevaluacion=" & idEvaluacion & "&tituloEvaluacion=" & tituloEvaluacion
%>