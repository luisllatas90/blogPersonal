<!--#include file="../../../NoCache.asp"-->
<!-- #include file="../../../funcionesaulavirtual.asp" -->
<!-- #include file="../../../clssubir.asp" -->
<%
if session("idcursovirtual")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

	dim iddocNuevo,titulodocumento,descripcion,itipopublic,ArchivoOriginal,iversiondoc
  	dim strfechainicio,strfechafin,finicio,ffin,hinicio,minicio,tinicio,hfin,mfin,tfin
  	dim directorio,xidcarpeta,ArchivoNuevo,tipofuncion,idusuario,icursovirtual,ficursovirtual,ffcursovirtual
  	
	

	'*************************************************************************************
	'Especificar la ruta dónde se guardará el archivo (directorio virtual)
	'*************************************************************************************
	directorio="t:\documentos aula virtual\archivoscv\" & session("idcursovirtual") & "\documentos\"
	'response.write directorio

	tipofuncion=session("tipofuncion")
	idusuario=session("codigo_usu")
	icursovirtual=session("idcursovirtual")
	ficursovirtual=session("iniciocursovirtual")
	ffcursovirtual=session("fincursovirtual")
	ArchivoNuevo=request.querystring("archivo")

	Private Function cerrarventanaerror()
		cerrarventanaerror="<script>alert(""No se pudo registrar correctamente el archivo. Por favor intente denuevo"");history.back(-1)</script>"
	End function

	Set Cargar = New clsSubir

		'*************************************************************************************
		'Permite extraer el nombre original del archivo que se va a subir
		'*************************************************************************************
		ArchivoOriginal=Cargar.Fields("file").FileName
		Extension=right(ArchivoOriginal,3)
		

		'*************************************************************************************
		'Asignar valores de controles del formulario
		'*************************************************************************************
		titulodocumento=Cargar.Fields("titulodocumento").Value
		finicio=Cargar.Fields("fechainicio").Value
		ffin=Cargar.Fields("fechafin").Value
		hinicio=Cargar.Fields("horainicio").Value
		minicio=Cargar.Fields("mininicio").Value
		tinicio=Cargar.Fields("turnoinicio").Value
		hfin=Cargar.Fields("horafin").Value
		mfin=Cargar.Fields("minfin").Value
		tfin=Cargar.Fields("turnofin").Value
		descripcion=Cargar.Fields("descripcion").Value
		itipopublic=Cargar.Fields("idtipopublicacion").Value
		xidcarpeta=Cargar.Fields("txtidcarpeta").Value
		iversiondoc=Cargar.Fields("versiondoc").Value
	
		'*************************************************************************************
		'Guardar archivo en el Disco Duro virtual, y tomar en cuenta lo sgte:
		'1. Si se guarda el archivo con el nombre original, entonces llamar a la variable ArchivoOriginal
		'2. Si se asignará un nombre al archivo, entonces llamar a la variable ArchivoNuevo +Extensión
		'*************************************************************************************
		ArchivoNuevo=ArchivoNuevo & "." & Extension
		Cargar("file").SaveAs directorio & ArchivoNuevo
		'Response.Write Cargar.DebugText

		'*************************************************************************************
		'Guardar la información del archivo en la Base de Datos
		'*************************************************************************************

		strfechainicio=finicio & " " & hinicio & ":" & minicio & ":00 " & tinicio
		strfechafin=ffin & " " & hfin & ":" & mfin & ":00 " & tfin
		estado=0
		
		if finicio="" then strfechainicio=now
		if ffin="" then strfechafin=session("fincursovirtual")
	
		Set Obj= Server.CreateObject("AulaVirtual.clsdocumento")
 			iddocNuevo=Obj.Agregar("A",ArchivoNuevo,titulodocumento,idusuario,strfechainicio,strfechafin,descripcion,estado,xidcarpeta,icursovirtual,iversiondoc,0,itipopublic)
	  	Set Obj=Nothing
	Set Cargar=nothing	
		
		if iddocNuevo>0 then
			if itipopublic=2 then
				response.redirect "../usuario/frmagregarusuariosrecurso.asp?accion=agregarpermisos&idtabla=" & iddocNuevo & "&nombretabla=documento&tipodoc=A"
			else%>
				<script language='Javascript'>window.opener.document.frames[0].location.reload();window.close()</script>
			<%end if
		else
			response.write cerrarventanaerror
		end if
	
%>