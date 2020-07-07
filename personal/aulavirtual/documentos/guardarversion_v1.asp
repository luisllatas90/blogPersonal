<!--#include file="../../../NoCache.asp"-->
<!-- #include file="../../../funcionesaulavirtual.asp" -->
<!-- #include file="../../../clssubir.asp" -->
<%
if session("idcursovirtual")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

	dim iddocumento,idversion,tituloversion,obs
  	dim directorio,archivo,narchivooriginal
  	
  	iddocumento=request.querystring("iddocumento")
	'directorio=Server.MapPath("../../../archivoscv" & session("idcursovirtual") & "/documentos/")
	directorio="t:\documentos aula virtual\archivoscv\" & session("idcursovirtual") & "\documentos\"
	tipofuncion=session("tipofuncion")
	idusuario=session("codigo_usu")
	icursovirtual=session("idcursovirtual")
	archivo=GenerarNombreArchivo(session("codigo_usu"))

	Private Function cerrarventanaerror()
		cerrarventanaerror="<script>alert(""No se pudo registrar correctamente el documento. Por favor intente denuevo"");history.back(-1)</script>"
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
			
			if Key="tituloversion" then tituloversion=Cargar.Elementosfrm.Item(Key)
			if Key="obs" then obs=Cargar.Elementosfrm.Item(Key)
			if Key="txtidversion" then idversion=Cargar.Elementosfrm.Item(Key)
		next
		
		if (fname = "" and newTitle = "") then
			doCreate = false
			response.write cerrarventanaerror
		end if
					
		If docreate then			
		    Set Obj= Server.CreateObject("AulaVirtual.clsdocumento")
  				Call Obj.AgregarVersion(narchivooriginal,tituloversion,iddocumento,idusuario,0,0,1,obs,idversion)
		  	Set Obj=Nothing
			
			response.write "<script>window.parent.location.reload();window.close()</script>"
		end if
	Set Cargar=nothing
%>