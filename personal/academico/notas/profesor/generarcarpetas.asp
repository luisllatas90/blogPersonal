<%
raiz="t:\documentos aula virtual\archivoscv\"

for i=1064 to 1069
	Set fso = CreateObject("Scripting.FileSystemObject")
	carpeta=raiz & i
	'Verificar si existe la carpeta con el curso virtual
	If (Not fso.FolderExists(carpeta)) then
		Set fol = fso.CreateFolder(carpeta)
		Set fol = fso.CreateFolder(carpeta & "\documentos")
		Set fol = fso.CreateFolder(carpeta & "\tareas")
		Set fol = fso.CreateFolder(carpeta & "\images")
	End if
	response.write carpeta & "<br>"
next

response.write "Se han creado correctamente las carpetas"
%>