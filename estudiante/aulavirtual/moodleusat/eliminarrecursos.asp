<%
Dim ruta

ruta="t:\documentos aula virtual\archivoscv\"
nombretabla=request.querystring("nombretabla")
idtabla=request.querystring("idtabla")
pagina="tematicacurso.asp"

	'====================================================================
	'Eliminar tareas y archivos publicadas dependientes
	'====================================================================
   	if lcase(nombretabla)="contenidocursovirtual" then num=2
   	if lcase(nombretabla)="documento" then num=3
   	if lcase(nombretabla)="tarea" then num=4
   	if lcase(nombretabla)="tareausuario" then
   		num=5
   		pagina="detallerecursos.asp?codigo_tre=T&idtabla=" & request.querystring("idtarea")
   	end if
	
 	Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			if num<>"" then
				Set rsarchivos=obj.Consultar("ConsultarContenidoCursoVirtual","FO",num,idtabla,0,0)
			end if
			'====================================================================
			'Almacenar variables de campos
			'====================================================================
			mensaje=obj.Ejecutar("DI_Eliminar" & nombretabla,true,idtabla,0)
		obj.CerrarConexion
	Set Obj=nothing

	'====================================================================
	'Direccionar a página
	'====================================================================
	if num<>"" then
		If Not(rsarchivos.BOF and rsarchivos.EOF) then
		    Set fs = Server.CreateObject("Scripting.FileSystemObject")
		    
		    Do While Not rsarchivos.EOF
				ruta=ruta & rsarchivos(0)
        		If fs.FileExists(ruta) then
    				fs.DeleteFile(ruta)
    			end if
    			rsarchivos.movenext
    		Loop
	    	Set fs = Nothing
    	end if
    	set rsarchivos=nothing
    end if
	'====================================================================
	'Direccionar a página
	'====================================================================
   	response.redirect "cargando.asp?rutapagina=" & pagina
%>