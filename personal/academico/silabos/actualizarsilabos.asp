<!--#include file="../../../NoCache.asp"-->
<%
'***************************************************************************************
'CV-USAT
'Archivo		: Permite actualizar los sílabos subidos en la BDatos
'Autor			: Gerardo Chunga Chinguel
'Fecha de Creación	: 24/12/2006
'Fecha Modificación	: 25/12/2006
'Observaciones		: Procedimiento para subir el sílabo al servidor
'***************************************************************************************
'On error resume next

Server.ScriptTimeout=1000

RutaSilabo=server.MapPath("../../../silabos/")

Dim strPath 
Dim objFSO
Dim objFolder 
Dim objItem 
Set objFSO = Server.CreateObject("Scripting.FileSystemObject")

'Obtener carpetas de la ruta sílabos

Set objFolder = objFSO.GetFolder(RutaSilabo)

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
	Call Obj.Ejecutar("ActualizarMigracionSilabos",false,1,date,0)

	'**********************************************************
	'Recorrer subcarpetas de ciclos
	'**********************************************************	
	For Each objItem In objFolder.SubFolders
		'**********************************************************	
		'Recorrer archivos .ZIP
		'**********************************************************
		response.write("carpeta:" & objItem.Name & "<br>")
		For Each objFiles In objItem.Files
			codigo_cup=trim(objFiles.Name)
			codigo_cup=left(codigo_cup,len(codigo_cup)-4)
			
			fechareg=objFiles.DateCreated	
			'**********************************************************
			'Guardar la información del archivo en la Base de Datos
			'**********************************************************
			Call Obj.Ejecutar("ActualizarMigracionSilabos",false,2,cdate(fechareg),codigo_cup)
			'response.write vbtab & "-" & codigo_cup & " fecha: " & fechareg & "<br>"
		Next
	Next
		
	'Call Obj.Ejecutar("ActualizarMigracionSilabos",false,3,date,0)
	Obj.CerrarConexion
Set Obj=Nothing

'If Err.Number=0 then
	response.write "Se ha actualizado correctamente"
'else
'	response.write "Ha ocurrido un error de actualización"
'end if
%>