<!--#include file="../../../NoCache.asp"-->
<!-- #include file="../../../funcionesaulavirtual.asp" -->
<!-- #include file="../../../clssubir.asp" -->
<%
if session("codigo_usu")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

	Response.Expires = -1
	Server.ScriptTimeout = 600

	dim directorio,archivo,idusuario,narchivooriginal

	idusuario=session("codigo_usu")
	idcursovirtual=request.querystring("idcursovirtual")
	'directorio=Server.MapPath("../../../archivoscv/" & idcursovirtual & "/images")
	directorio="t:\documentos aula virtual\archivoscv\" & idcursovirtual & "\images"

	archivo=GenerarNombreArchivo(session("codigo_usu"))

	Private Function cerrarventanaerror()
		cerrarventanaerror="<script>alert(""No se pudo registrar correctamente el archivo. Por favor intente denuevo"");history.back(-1)</script>"
	End function

Set Cargar = New SubirArchivo
		Cargar.Guardar directorio,archivo,narchivooriginal
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
		next
		
		if (fname = "" and newTitle = "") then
			doCreate = false
			response.write cerrarventanaerror
		end if
					
		If docreate then
				Set Obj= Server.CreateObject("AulaVirtual.clsCategoria")
			  		Call Obj.AgregarArchivosMultimedia(idusuario,narchivooriginal,"cursovirtual",idcursovirtual,idcursovirtual)
  				Set Obj=Nothing
			%>
				<script>
					window.returnValue="../../../archivoscv/<%=session("idcursovirtual") & "/images/" & narchivooriginal%>"
					window.close()
				</script>
		<%else
			response.write cerrarventanaerror
		end if
Set Cargar=nothing
%>