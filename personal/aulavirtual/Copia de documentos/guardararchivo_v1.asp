<!--#include file="../../../NoCache.asp"-->
<!-- #include file="../../../funcionesaulavirtual.asp" -->
<!-- #include file="../../../clssubir.asp" -->
<%
if session("idcursovirtual")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

	dim iddocNuevo,titulodocumento,descripcion,itipopublic,narchivooriginal,iversiondoc
  	dim strfechainicio,strfechafin,finicio,ffin,hinicio,minicio,tinicio,hfin,mfin,tfin
  	dim directorio,xidcarpeta,archivo,tipofuncion,idusuario,icursovirtual,ficursovirtual,ffcursovirtual
  	
	'directorio=Server.MapPath("../../../archivoscv/") & session("idcursovirtual") & "/documentos/"
	directorio="t:\documentos aula virtual\archivoscv\" & session("idcursovirtual") & "\documentos\"
	'response.write directorio

	tipofuncion=session("tipofuncion")
	idusuario=session("codigo_usu")
	icursovirtual=session("idcursovirtual")
	ficursovirtual=session("iniciocursovirtual")
	ffcursovirtual=session("fincursovirtual")
	archivo=request.querystring("archivo")

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
			
			if Key="titulodocumento" then titulodocumento=Cargar.Elementosfrm.Item(Key)
			if Key="fechainicio" then finicio=Cargar.Elementosfrm.Item(Key)
			if Key="fechafin" then ffin=Cargar.Elementosfrm.Item(Key)
			if Key="horainicio" then hinicio=Cargar.Elementosfrm.Item(Key)
			if Key="mininicio" then minicio=Cargar.Elementosfrm.Item(Key)
			if Key="turnoinicio" then tinicio=Cargar.Elementosfrm.Item(Key)
			if Key="horafin" then hfin=Cargar.Elementosfrm.Item(Key)
			if Key="minfin" then mfin=Cargar.Elementosfrm.Item(Key)
			if Key="turnofin" then tfin=Cargar.Elementosfrm.Item(Key)
			if Key="descripcion" then descripcion=Cargar.Elementosfrm.Item(Key)
			if Key="idtipopublicacion" then itipopublic=Cargar.Elementosfrm.Item(Key)
			if Key="txtidcarpeta" then xidcarpeta=Cargar.Elementosfrm.Item(Key)
			if Key="versiondoc" then iversiondoc=Cargar.Elementosfrm.Item(Key)			
		next
		
		if (fname = "" and newTitle = "") then
			doCreate = false
			response.write cerrarventanaerror
		end if
					
		If docreate then
			strfechainicio=finicio & " " & hinicio & ":" & minicio & ":00 " & tinicio
			strfechafin=ffin & " " & hfin & ":" & mfin & ":00 " & tfin
			estado=0
		
			if finicio="" then strfechainicio=now
			if ffin="" then strfechafin=session("fincursovirtual")
	
			Set Obj= Server.CreateObject("AulaVirtual.clsdocumento")
 				iddocNuevo=Obj.Agregar("A",narchivooriginal,titulodocumento,idusuario,strfechainicio,strfechafin,descripcion,estado,xidcarpeta,icursovirtual,iversiondoc,0,itipopublic)
	  		Set Obj=Nothing
			
			if iddocNuevo>0 then
				if itipopublic=2 then
					response.redirect "../usuario/frmagregarusuariosrecurso.asp?accion=agregarpermisos&idtabla=" & iddocNuevo & "&nombretabla=documento&tipodoc=A"
				else
					response.write "<script>window.opener.document.frames[0].location.reload();window.close()</script>"
				end if
			else
				response.write cerrarventanaerror
			end if		
		end if
	Set Cargar=nothing
%>