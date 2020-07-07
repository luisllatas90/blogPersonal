<% 
Response.Expires = -1
Server.ScriptTimeout = 600
%>
<!-- #include file="../subir.asp" -->
<!-- #include file="../funcionesaulavirtual.asp" -->
<%
Dim idtareaevaluacion
Dim nombrearchivoavance,tituloarchivoavance,pjeavance,idevaluacionindicador
Dim GuardarNombrearchivo

	idtareaevaluacion=Request.querystring("idtareaevaluacion")
	nombrearchivoavance= Request.querystring("nombrearchivoavance")
	tituloarchivoavance=Request.querystring("tituloarchivoavance")
	pjeavance=Request.querystring("pjeavance")
	autorizado=Request.querystring("autorizado")
	idevaluacionindicador=request.querystring("idevaluacionindicador")
	idvariable=request.querystring("idvariable")
	idseccion=request.querystring("idseccion")
	
	Directorio = Server.MapPath("archivos")
	
	
	Set Upload = New SubirArchivo
	
		with Upload
			.Save Directorio,nombrearchivoavance,GuardarNombreArchivo
    		For each fileKey in .UploadedFiles.keys
    	       fname = .UploadedFiles(fileKey).FileName 
    		Next

		    ks = .FormElements.Keys
	    	for each Key in Upload.FormElements.Keys
				if (lcase(Key) = "docreate" and lcase(.FormElements.Item(Key)) = "true") then
					doCreate = true
				end if
				if (lcase(Key) = "title" ) then
					newTitle = .FormElements.Item(Key)
				end if
				if Key="tituloarchivoavance" then titulodocumento=.FormElements.Item(Key)
		    next
			if (fname = "" and newTitle = "") then
				doCreate = false
			end if
		end with
		
		If docreate then
			Set Obj= Server.CreateObject("AulaVirtual.clsAcreditacion")
				call Obj.agregaravancetareaevaluacion(idtareaevaluacion,pjeavance,GuardarNombreArchivo,titulodocumento,session("codigo_usu"),idevaluacionindicador,session("idacreditacion"),idvariable,idseccion)
			Set Obj=nothing%>
			<script>
				window.opener.location.reload()
				window.close()
			</script>
		<%end if
%>