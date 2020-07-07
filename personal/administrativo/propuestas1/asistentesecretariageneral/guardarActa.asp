<!--#include file="../../../../NoCache.asp"-->
<!-- #include file="../../../../funcionesaulavirtual.asp" -->
<!-- #include file="../../../../clssubir.asp" -->
<%
	codigo_prp=Request.QueryString("codigo_prp")
	codigo_rec=Request.QueryString("codigo_rec")


if Request.QueryString("accion")="eliminar" then
		archivo1=server.MapPath("../../../../filespropuestas/actas/" & Request.QueryString("archivo"))


		set obFile=server.createObject("Scripting.FileSystemObject")
			If obFile.FileExists(archivo1) Then
				//response.write(archivo1)
				obFile.DeleteFile(archivo1)
			End If
		set obFile=nothing
		Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
		objPropuesta.AbrirConexionTrans
		//Response.Write(Request.QueryString("archivo"))
	//	ObjPropuesta.ejecutar "EliminarArchivoPropuesta",false,Request.QueryString("archivo")
		//Call ObjPropuesta.ejecutar("ActualizarGrabacionReunion",false,codigo_prp,null,null)		
		objPropuesta.Ejecutar "ActualizarReunionConsejo",false,"AC",codigo_rec,0,0,0,0
		//response.write(archivo1)			
		objPropuesta.CerrarConexionTrans		
		response.redirect "adjuntaracta.asp?codigo_rec=" & codigo_rec
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
	directorio=CrearCarpeta(server.MapPath("../../../../filespropuestas/actas/"))
	
	''response.write directorio
	

	
	//ArchivoNuevo=request.querystring("archivo")
	Private Function cerrarventanaerror()
		cerrarventanaerror="<script>alert(""No se pudo registrar correctamente el archivo. Por favor intente denuevo"");history.back(-1)</script>"
	End function

	Set Cargar = New clsSubir

		''*************************************************************************************
		''Permite extraer el nombre original del archivo que se va a subir
		''*************************************************************************************
		usuario=session("Usuario_bit")
		fechahora=replace(now,"/","")
		fechahora=replace(fechahora,":","")
		fechahora=replace(fechahora," ","")
		nusuario=MID(usuario,6,LEN(usuario))
		archivogenerado="acta" + codigo_rec
		
		ArchivoOriginal=Cargar.Fields("filename").FileName
		
		Extension=right(ArchivoOriginal,3)
		

		''*************************************************************************************
		''Asignar valores de controles del formulario
		''*************************************************************************************
		descripcion_prp=Cargar.Fields("txtdescripcion_prp").Value & "." & Extension
//		finicio=Cargar.Fields("fechainicio").Value
//		ffin=Cargar.Fields("fechafin").Value

		''*************************************************************************************
		''Guardar archivo en el Disco Duro virtual, y tomar en cuenta lo sgte:
		''1. Si se guarda el archivo con el nombre original, entonces llamar a la variable ArchivoOriginal
		''2. Si se asignará un nombre al archivo, entonces llamar a la variable ArchivoNuevo +Extensión
		''*************************************************************************************
//		ArchivoNuevo= directorio & "\" & archivogenerado & "." & Extension
		ArchivoNuevo=directorio & "\" & descripcion_prp
//		Cargar("filename").SaveAs ArchivoNuevo ''directorio & ArchivoNuevo
		Cargar("filename").SaveAs ArchivoNuevo ''directorio & descripcion_prp
		response.write ArchivoNuevo
		//Response.Write Cargar.DebugText
		Set Cargar=nothing
		''*************************************************************************************
		''Guardar la información del archivo en la Base de Datos
		''*************************************************************************************

				Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
		           ObjCC.AbrirConexion				
  					Call objCC.ejecutar("ActualizarReunionConsejo",false,"RA",codigo_rec,descripcion_prp,0,0,0)
					ObjCC.CerrarConexion					
			  	Set ObjCC=Nothing
				response.redirect "adjuntaracta.asp?codigo_rec=" & codigo_rec	
end if				
%>