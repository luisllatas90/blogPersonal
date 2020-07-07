<!--#include file="../../../../NoCache.asp"-->
<%
'***************************************************************************************
'CV-USAT
'Archivo		: Permite actualizar las fotos de los alumnos
'Autor			: Gerardo Chunga Chinguel
'Fecha de Creación	: 28/12/2006
'Fecha Modificación	: 29/12/2006
'Observaciones		: Procedimiento para subir el sílabo al servidor
'***************************************************************************************
On error resume next

Server.ScriptTimeout=1000

Rutafoto="C:\Aplicaciones WEB\Aplicaciones\imgestudiantes\imgestudiantes\"
'server.MapPath("../../../../imgestudiantes/")

Dim strPath
Dim objFSO
Dim objFolder 
Dim objItem 
Set objFSO = Server.CreateObject("Scripting.FileSystemObject")

'Obtener carpetas de la ruta sílabos
Set objFolder = objFSO.GetFolder(Rutafoto)

Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
	Call Obj.Ejecutar("ActualizarEstadoFoto",false,"T",0,0,0,null)

	'**********************************************************
	'Recorrer subcarpetas de ciclos
	'**********************************************************	
	For Each objItem In objFolder.SubFolders
		'**********************************************************	
		'Recorrer archivos .JPG
		'**********************************************************
		'response.write("carpeta:" & objItem.Name & "<br>")
		For Each objFiles In objItem.Files
			codigouniver_alu=trim(objFiles.Name)
			'Quita la extensión del archivo
			codigouniver_alu=left(codigouniver_alu,len(codigouniver_alu)-4)

			'Decodifica el codigo real
			codigouniver_alu=obEnc.DeCodifica(codigouniver_alu)

			'Quita el 069 de la imágen
			codigouniver_alu=right(codigouniver_alu,len(codigouniver_alu)-3)

			'**********************************************************
			'Guardar la información del archivo en la Base de Datos
			'**********************************************************
			Call Obj.Ejecutar("ActualizarEstadoFoto",false,"B",codigouniver_alu,1,0,null)
			'response.write vbtab & "-" & codigouniver_alu & "<br>"
		Next
	Next
		
	Obj.CerrarConexion
Set Obj=Nothing
set obEnc=Nothing

If Err.Number=0 then
	response.write "Se ha actualizado correctamente"
else
	response.write "Ha ocurrido un error de actualización"
end if
%>