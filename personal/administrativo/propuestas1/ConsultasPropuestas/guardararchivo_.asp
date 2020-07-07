<!--#include  file="../../../../NoCache.asp"-->
<!-- #include file="../../../../funciones.asp" -->
<!-- #include file="../../../../clssubir.asp" -->
<%

Response.Expires = -1
''On error resume next

Dim narchivooriginal,usuario,directorio,RutaSilabo
	codigo_cop=session("comentario_prp")
	codigo_prp=Request.QueryString("codigo_prp")
	usuario=session("Usuario_bit")
	RutaInv=server.MapPath("../../../../filespropuestas/" & codigo_prp & "/")
	fechahora=replace(now,"/","")
	fechahora=replace(fechahora,":","")
	fechahora=replace(fechahora," ","")
	fechahora=replace(fechahora,".","")
	nusuario=replace(usuario,"USAT\","")	
	if Key="filename" then ext=rigth(Cargar.Elementosfrm.Item(Key),4)
	narchivogenerado= nusuario& fechahora
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
				''response.redirect("registrapropuesta3.asp")
			case "1"
				cerrarventana="<script>alert(""El archivo sobrepasa el límite recomendado de 300 kb. Por favor trate de reducir el tamaño del archivo"");history.back(-1)</script>"
			case "2"
				cerrarventana="<script>alert(""Se ha registrado el archivo satisfactoriamente"");</script>"
				''response.redirect("registrapropuesta3.asp")
				
		end select
	End function
	Set Cargar = New SubirArchivo
		directorio=CrearCarpeta(RutaInv)	
			Cargar.Guardar directorio,narchivogenerado,narchivooriginal
		  	For each fileKey in Cargar.ProcesarArchivos.keys
				fname = Cargar.ProcesarArchivos(fileKey).FileName 
			Next
			for each Key in Cargar.Elementosfrm.Keys
				if (lcase(Key) = "docreate" or lcase(Cargar.Elementosfrm.Item(Key)) = "true") then
					doCreate = true
				end if
				if (lcase(Key) = "title" ) then
					newTitle = Cargar.Elementosfrm.Item(Key)
				end if
			if Key="txtdescripcion_prp" then descripcion_prp=Cargar.Elementosfrm.Item(Key)				
			next
		RESPONSE.WRITE(newTitle)
			if (fname = "" or newTitle = "") then
				doCreate = false
				response.write cerrarventana(0)
			end if
					
		If docreate then
				narchivogenerado=narchivogenerado & right(fname,4)
				Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
		            ObjCC.AbrirConexion				
  					Call objCC.ejecutar("RegistraArchivoPropuesta",false,codigo_cop,narchivogenerado,descripcion_prp)
					ObjCC.CerrarConexion					
			  	Set ObjCC=Nothing
				response.write cerrarventana(2)
		else
				response.write cerrarventana(0)
		end if
			

			

'		If Err.Number<>0 then
'			response.write cerrarventana(0)
'		end if
	Set Cargar=nothing
%>