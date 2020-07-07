<!--#include file="../../../../../NoCache.asp"-->
<!-- #include file="../../../../../funcionesaulavirtual.asp" -->
<!-- #include file="../../../../../clssubir.asp" -->
<%
	codigo_cni=Request.QueryString("codigo_cni")

if Request.QueryString("accion")="eliminar" then
//		archivo1=server.MapPath("../../../../../convenios/" & Request.QueryString("codigo_cni") & "/" & Request.QueryString("archivo"))
		archivo1=server.MapPath("../../../../../convenios/" & codigo_cni & ".pdf")		
		response.write(archivo1)

		set obFile=server.createObject("Scripting.FileSystemObject")
			If obFile.FileExists(archivo1) Then
				//response.write(archivo1)
				obFile.DeleteFile(archivo1)
			End If
		set obFile=nothing
		Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
		objPropuesta.AbrirConexionTrans
		//Response.Write(Request.QueryString("archivo"))

		Call ObjPropuesta.ejecutar("actualizaPDFConvenio",false,codigo_cni,null)		
		objPropuesta.CerrarConexionTrans		
		response.redirect "adjuntarconvenio.asp?codigo_cni=" & codigo_cni
else
  	
	function CrearCarpeta(ByVal ruta)
		on error resume next
		dim ObFile
		set obFile=server.createObject("Scripting.FileSystemObject")
		obFile.CreateFolder ruta
		CrearCarpeta=ruta
	end function	

	''*************************************************************************************
	''Especificar la ruta dónde se guardará el archivo (directorio virtual)
	''*************************************************************************************
	//directorio=
	//directorio=CrearCarpeta(server.MapPath("../../../../../convenios/") & "\" & Request.QueryString("codigo_cni") & "\")
	directorio=CrearCarpeta(server.MapPath("../../../../../convenios/") & "\")	
	''response.write directorio
	

	
	//ArchivoNuevo=request.querystring("archivo")
	Private Function cerrarventanaerror()
		cerrarventanaerror="<script>alert(""No se pudo registrar correctamente el archivo. Por favor intente denuevo"");history.back(-1)</script>"
	End function

	Set Cargar = New clsSubir

		''*************************************************************************************
		''Permite extraer el nombre original del archivo que se va a subir
		''*************************************************************************************
	
		ArchivoOriginal=Cargar.Fields("filename").FileName
		
		Extension=right(ArchivoOriginal,3)
		

		''*************************************************************************************
		''Asignar valores de controles del formulario
		''*************************************************************************************
		descripcion_prp=Cargar.Fields("txtdescripcion_prp").Value


	
		''*************************************************************************************
		''Guardar archivo en el Disco Duro virtual, y tomar en cuenta lo sgte:
		''1. Si se guarda el archivo con el nombre original, entonces llamar a la variable ArchivoOriginal
		''2. Si se asignará un nombre al archivo, entonces llamar a la variable ArchivoNuevo +Extensión
		''*************************************************************************************
		ArchivoNuevo=descripcion_prp & "." & Extension
		Cargar("filename").SaveAs directorio & codigo_cni & "." & Extension
		''Response.Write Cargar.DebugText
		Set Cargar=nothing
		''*************************************************************************************
		''Guardar la información del archivo en la Base de Datos
		''*************************************************************************************

				Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
		           ObjCC.AbrirConexion				
  					Call objCC.ejecutar("actualizaPDFConvenio",false,codigo_cni,ArchivoNuevo)
					ObjCC.CerrarConexion					
			  	Set ObjCC=Nothing
				response.redirect "adjuntarconvenio.asp?codigo_cni=" & codigo_cni	
end if				
%>