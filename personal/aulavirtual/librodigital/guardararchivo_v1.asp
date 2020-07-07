<!--#include file="../../../NoCache.asp"-->
<!-- #include file="../../../funcionesaulavirtual.asp" -->
<!-- #include file="../clssubir.asp" -->
<%
if session("idcursovirtual")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

	Response.Expires = -1
	Server.ScriptTimeout = 600

	dim directorio,idtabla,archivo,tipofuncion,idusuario,icursovirtual,ficursovirtual,ffcursovirtual,narchivooriginal

	directorio=Server.MapPath("../../../archivoscv/" & session("idcursovirtual") & "/documentos")
	tipofuncion=session("tipofuncion")
	idusuario=session("codigo_usu")
	icursovirtual=session("idcursovirtual")
	ficursovirtual=session("iniciocursovirtual")
	ffcursovirtual=session("fincursovirtual")
	archivo=GenerarNombreArchivo(session("codigo_usu"))
	idtabla=request.querystring("idtabla")

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
			  		Call Obj.AgregarArchivosMultimedia(idusuario,narchivooriginal,"contenido",idtabla)
  				Set Obj=Nothing
			%>
				<script>
					window.returnValue="../../../archivoscv/<%=session("idcursovirtual") & "/documentos/" & narchivooriginal%>"
					window.close()
				</script>
		<%else
			response.write cerrarventanaerror
		end if
Set Cargar=nothing
%>