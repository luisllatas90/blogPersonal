<!--#INCLUDE FILE="../../../../funciones.asp"-->
<!--#INCLUDE FILE="../../../../clsSubir.asp"-->
<%
call Enviarfin(session("codigo_usu"),"../../../../")

function CrearCarpeta(ByVal ruta)
	on error resume next
	dim ObFile

	'ruta=server.MapPath("../../../../../imgestudiantes/" & ruta)
	ruta="C:\Aplicaciones WEB\Aplicaciones\imgestudiantes\imgestudiantes\" & ruta & "\"
	
	set obFile=server.createObject("Scripting.FileSystemObject")
	if obFile.FolderExists(ruta)=false then
		obFile.CreateFolder ruta
	end if
	set obFile=nothing
	CrearCarpeta=ruta
	if(Err.number <> 0) then
	    response.write "Error al crear carpeta: " & Err.Description
	end if
end function

'*************************************************************************************
'Declarar variables
'*************************************************************************************
On error resume next

Dim Cargar
Dim num
Dim archivo
Dim directorio
'response.write "<script>alert('ok')</script>"
num=request.querystring("num")

Set Cargar = New clsSubir

Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")


Obj.AbrirConexion

response.write "<h3>Reporte de sucesos</h3><hr>"
response.write "<ol>"
	for i=1 to num

		mensaje=""		
		archivo=Cargar.Fields("File" & i).FileName
		
		if archivo<>"" then
			tamanio=int(Cargar.Fields("File" & i).length/1000)

			'****************************************************
			'Validar el tamaño que no sea mayor a 100 KB
			'****************************************************
			if tamanio>100 then
				response.write "<li>El tamaño del archivo " & ucase(archivo) & " excede a 100 KB</li>"
			else
				'****************************************************			
				'Quitar la extensión, y debe incluir el [069]
				'****************************************************				
				codigouniversitario=left(archivo,len(archivo)-4)

				'****************************************************				
				'Quitar 069 para buscar en BDatos de sistema
				'****************************************************				
				codigoreal=right(codigouniversitario,len(codigouniversitario)-3)
					
				'****************************************************
				'Validar si el archivo es alumno en la BDatos
				'****************************************************
				mensaje=obj.Ejecutar("Actualizarestadofoto",true,"S",codigoreal,1,session("codigo_usu"),null)
				arrmensaje=split(mensaje,"|")
				cicloingreso=arrmensaje(0)
				
				if trim(cicloingreso)<>"" then				    					
					directorio=CrearCarpeta(cicloingreso)					
					archivonuevo=obEnc.Codifica(codigouniversitario)
					extension=right(archivo,3)			
				
					'****************************************************
					'Subir el archivo validado
					'****************************************************
					
					'response.write("<script>alert ('" &   directorio & archivonuevo & "." & extension  & "')</script>") ' jmanay                    
					Cargar("File" & i).SaveAs directorio & archivonuevo & "." & extension					

					if err.number<>0 then
						response.write "Ha ocurrido un error al grabar las fotos. intente mas tarde"
						Set Obj=nothing
						set obEnc=Nothing
						Set Cargar= Nothing
						exit for
					end if

				end if
				response.write "<li>" & arrmensaje(1) & "</li>"
				
			end if
		end if
	next
response.write "</ol>"
response.write "<input name='cmdRegresar' type='button' value='Regresar' onclick=""location.href='frmsubirfoto.asp?num=" & num & "'"" />"

Obj.CerrarConexion
Set Obj=nothing
set obEnc=Nothing
Set Cargar= Nothing

if Err.number <> o then
    response.Write "Error: " & Err.Description
end if
%>