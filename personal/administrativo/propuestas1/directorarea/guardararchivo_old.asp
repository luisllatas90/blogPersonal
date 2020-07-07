<!--#include file="../../../../NoCache.asp"-->
<!-- #include file="../../../../funciones.asp" -->
<!-- #include file="../../../../clssubir.asp" -->

 <%
'***************************************************************************************
'CV-USAT
'Archivo			: guardararchivo.asp
'Autor				: Gerardo Chunga Chinguel
'Fecha de Creación	: 11/03/2006 08:12 p.m.
'Fecha Modificación	: 13/03/2006 08:55 a.m.
'Observaciones		: Procedimiento para subir el sílabo al servidor
'***************************************************************************************
Response.Expires = -1
''On error resume next
	codigo_prp=Request.QueryString("codigo_prp")
	codigo_cop=Request.QueryString("codigo_cop")
''if session("codigo_usu")="" then response.redirect "../../../tiempofinalizado.asp"
''por hugo para eliminar files de propuestas
Dim codigo_cup,descripcion_cac,narchivooriginal,usuario,directorio,RutaSilabo
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
		response.redirect "adjuntar.asp?codigo_cop=" & codigo_cop & "&codigo_prp=" & codigo_prp
	else
	

	response.write codigo_cop
	usuario=session("Usuario_bit")
	RutaInv=server.MapPath("../../../../filespropuestas/" & codigo_prp & "/")
	fechahora=replace(now,"/","")
	fechahora=replace(fechahora,":","")
	fechahora=replace(fechahora," ","")
	nusuario=replace(usuario,"USAT\","") 
	nusuario= replace(usuario,"usat\","")
	
	nusuario=codigo_prp + nusuario + fechahora
	
		function CrearCarpeta(ByVal ruta)
		on error resume next
		dim ObFile
		set obFile=server.createObject("Scripting.FileSystemObject")
		obFile.CreateFolder ruta
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
		SELECT case mensaje
			case "0"
				cerrarventana="<script>alert(""No se pudo registrar correctamente el archivo. Por favor intente denuevo"");history.back(-1)</script>"
			case "1"
				cerrarventana="<script>alert(""El archivo sobrepasa el límite recomendado de 300 kb. Por favor trate de reducir el tamaño del archivo"");history.back(-1)</script>"
			case "2"
				''cerrarventana="<script>window.opener.location.reload();window.resizeTo(-200,-200);alert('El Silabo fue almacenado correctamente en el servidor ');window.close();
				cerrarventana="<script>location.href=''</script>"
				
		end select
	
	End function
	Set Cargar = New SubirArchivo
		directorio=CrearCarpeta(RutaInv)
	RESPONSE.WRITE narchivogenerado
''		If Cargar.ValidarTamanio(directorio)>310 then
''			response.write cerrarventana(1)
''		else
			Cargar.Guardar directorio,nusuario,narchivooriginal
		  	For each fileKey in Cargar.ProcesarArchivos.keys
				fname = Cargar.ProcesarArchivos(fileKey).FileName 
			Next
		
			for each Key in Cargar.Elementosfrm.Keys
				if (lcase(Key) = "docreate" and lcase(Cargar.Elementosfrm.Item(Key)) = "true") then
					doCreate = true
				end if
				if (lcase(Key) = "title" ) then
					newTitle = Cargar.Elementosfrm.Item(Key)
				end if
				if Key="txtdescripcion_prp" then descripcion_prp=Cargar.Elementosfrm.Item(Key)				
				//response.write descripcion_prp
			next
		
			if (fname = "" and newTitle = "") then
				doCreate = false
				response.write cerrarventana(0)
			end if

			If docreate then
			response.write narchivooriginal
			  //narchivogenerado=narchivogenerado & right(fname,4)
				Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
		           ObjCC.AbrirConexion				
  					Call objCC.ejecutar("RegistraArchivoPropuesta",false,codigo_cop,narchivooriginal,descripcion_prp)
					ObjCC.CerrarConexion					
			  	Set ObjCC=Nothing
			//	response.write cerrarventana(2)
				response.redirect "adjuntar.asp?codigo_cop=" & codigo_cop & "&codigo_prp=" & codigo_prp

			else
				response.write cerrarventana(0)
			end if
			
''		end if

''		If Err.Number<>0 then
''			response.write cerrarventana(0)
'		end if
	Set Cargar=nothing
	end if

	
%>