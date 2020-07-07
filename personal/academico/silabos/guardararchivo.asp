<!--#include file="../../../NoCache.asp"-->
<!-- #include file="../../../funciones.asp" -->
<!-- #include file="../../../clssubir.asp" -->
<%
'***************************************************************************************
'CV-USAT
'Archivo		: guardararchivo.asp
'Autor			: Gerardo Chunga Chinguel
'Fecha de Creación	: 11/03/2006 08:12 p.m.
'Fecha Modificación	: 13/03/2006 08:55 a.m.
'Observaciones		: Procedimiento para subir el sílabo al servidor
'***************************************************************************************

if session("codigo_usu")="" then response.redirect "../../../tiempofinalizado.asp"

'On error resume next

	function CrearCarpeta(ByVal ruta)
		on error resume next
		dim ObFile
		
		set obFile=server.createObject("Scripting.FileSystemObject")
		if obFile.FolderExists(ruta)=false then
			obFile.CreateFolder ruta
		end if
		CrearCarpeta=ruta
	end function
	
	function EliminarArchivo(ByVal ruta,ByVal narchivo)
		narchivo= ruta & narchivo
		set obFile=server.createObject("Scripting.FileSystemObject")
			If obFile.FileExists(narchivo) Then
				obFile.DeleteFile(narchivo)
			End If
		set obFile=nothing
	end function
	
	Private Function cerrarventana(byVal mensaje)
		select case mensaje
			case "0"
				cerrarventana="<h3>No se pudo registrar correctamente el archivo. Por favor intente denuevo</h3>"
			case "1"
				cerrarventana="<script>alert(""El archivo sobrepasa el límite recomendado de 300 kb. Por favor trate de reducir el tamaño del archivo"");history.back(-1)</script>"
			case "2"
				cerrarventana="<script>window.opener.location.reload();window.resizeTo(-300,-300);alert('El Silabo fue almacenado correctamente en el servidor ');window.close();</script>"
		end select
	End function

	'*************************************************************************************
	'Declarar variables
	'*************************************************************************************
	Dim Cargar
	Dim codigo_cup,descripcion_cac,ArchivoOriginal,usuario,directorio,RutaSilabo

	Set Cargar = New clsSubir
			
		'*************************************************************************************
		'Permite extraer el nombre original del archivo que se va a subir
		'*************************************************************************************
		ArchivoOriginal=Cargar.Fields("File1").FileName
		Extension=right(ArchivoOriginal,3)

		tamanio=Cargar("File1").Length
		'if tamanio>0 then tamanio=int((tamanio/1000))

		'If int(tamanio)>310 Then
		'	'Response.End
		'	response.write cerrarventana(1)
		'End if
		
		'*************************************************************************************
		'Asignar valores de controles del formulario
		'*************************************************************************************
		ArchivoNuevo=Cargar.Fields("hdcodigo_cup").Value
		descripcion_cac=Cargar.Fields("hddescripcion_cac").Value
		usuario=session("Usuario_bit")
		codigo_cup=ArchivoNuevo
		RutaSilabo=server.MapPath("../../../silabos/") & "\" & descripcion_cac & "\"

		'*************************************************************************************
		'Crear Directorio Web
		'*************************************************************************************
		directorio=CrearCarpeta(RutaSilabo)
		
		'*************************************************************************************
		'Guardar archivo en el Disco Duro virtual, y tomar en cuenta lo sgte:
		'1. Si se guarda el archivo con el nombre original, entonces llamar a la variable ArchivoOriginal
		'2. Si se asignará un nombre al archivo, entonces llamar a la variable ArchivoNuevo +Extensión
		'*************************************************************************************
		ArchivoNuevo=ArchivoNuevo & "." & Extension
		Cargar("File1").SaveAs directorio & ArchivoNuevo
		'response.write directorio & "<br>" & ArchivoNuevo & "<br>" & ArchivoOriginal & "<br>" & Tamanio
	Set Cargar=nothing

	'*************************************************************************************
	'Guardar la información del archivo en la Base de Datos
	'*************************************************************************************
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			Call Obj.Ejecutar("AgregarSilabo",false,"A",codigo_cup,session("codigo_usu"))
		Obj.CerrarConexion
	Set Obj=Nothing

	If Err.Number=0 then
		response.write cerrarventana(2)
	else
		response.write cerrarventana(0)
	end if
%>