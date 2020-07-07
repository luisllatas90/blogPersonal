<!--#include file="../../../../NoCache.asp"-->
<!-- #include file="../../../../funcionesaulavirtual.asp" -->
<!-- #include file="../../../../clssubir.asp" -->
<%
	codigo_prp=Request.QueryString("codigo_prp")
	codigo_dap=Request.QueryString("codigo_dap")
	if codigo_dap=0 then
		codigo_dap=0
	end if

		codigo_cop=Request.QueryString("codigo_cop")	


if Request.QueryString("accion")="eliminar" then
		archivo1=server.MapPath("../../../../filespropuestas/" & Request.QueryString("codigo_prp") & "/" & Request.QueryString("archivo"))
		''response.write(archivo1)

		set obFile=server.createObject("Scripting.FileSystemObject")
			If obFile.FileExists(archivo1) Then
				response.write(archivo1)
				obFile.DeleteFile(archivo1)
			End If
		set obFile=nothing
		Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
		objPropuesta.AbrirConexionTrans
		//Response.Write(Request.QueryString("archivo"))
		ObjPropuesta.ejecutar "EliminarArchivoPropuesta",false,Request.QueryString("archivo")
		objPropuesta.CerrarConexionTrans		
		response.redirect "adjuntar.asp?codigo_dap=" & codigo_dap & "&codigo_prp=" & codigo_prp  & "&codigo_cop=" & codigo_cop
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
	directorio=CrearCarpeta(server.MapPath("../../../../filespropuestas/") & "\" & Request.QueryString("codigo_prp") & "\") 
	response.write (directorio)
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
		archivogenerado=codigo_prp + nusuario + fechahora		
		ArchivoOriginal=Cargar.Fields("filename").FileName
		
		Extension=right(ArchivoOriginal,3)	

	response.write (archivogenerado)
	
		''*************************************************************************************
		''Asignar valores de controles del formulario
		''*************************************************************************************
		descripcion_prp=Cargar.Fields("txtdescripcion_prp").Value
	//	codigo_cop=Cargar.Fields("txtcodigo_cop").Value
//		finicio=Cargar.Fields("fechainicio").Value
//		ffin=Cargar.Fields("fechafin").Value

	
		''*************************************************************************************
		''Guardar archivo en el Disco Duro virtual, y tomar en cuenta lo sgte:
		''1. Si se guarda el archivo con el nombre original, entonces llamar a la variable ArchivoOriginal
		''2. Si se asignará un nombre al archivo, entonces llamar a la variable ArchivoNuevo +Extensión
		''*************************************************************************************
		ArchivoNuevo=archivogenerado & "." & Extension
		Cargar("filename").SaveAs directorio & ArchivoNuevo
		''Response.Write Cargar.DebugText
		Set Cargar=nothing
		''*************************************************************************************
		''Guardar la información del archivo en la Base de Datos
		''*************************************************************************************

				Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
		           ObjCC.AbrirConexion
			   
				   	if codigo_dap=0 then	
						codigo_dap="C" & codigo_cop
						response.write ("<br>" & codigo_dap)
  						Call objCC.ejecutar("RegistraArchivoPropuesta",false,codigo_dap,ArchivoNuevo,descripcion_prp)
						codigo_dap =""
					else
						Call objCC.ejecutar("RegistraArchivoPropuesta",false,codigo_dap,ArchivoNuevo,descripcion_prp)
					end if
					ObjCC.CerrarConexion					
			  	Set ObjCC=Nothing
				response.redirect "adjuntar.asp?codigo_dap=" & codigo_dap & "&codigo_prp=" & codigo_prp	 & "&codigo_cop=" & codigo_cop	
end if				
%>