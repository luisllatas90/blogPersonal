<%
if session("codigo_Usu2")<>"" then
'==============================================================================
'Permie generar una conexión entre sesiones de Aula Virtual y Campus Virtual
'==============================================================================
    session("tipo_usu")=session("tipo_usu2")
	session("descripciontipo_usu")=session("descripciontipo_usu2")
	session("codigo_Usu")=session("codigo_Usu2")
	session("Ident_Usu")=session("Ident_Usu2")
	session("Nombre_Usu")=session("Nombre_Usu2")
	session("Usuario_bit")=session("Usuario_bit2")
	'Almacenar datos del ciclo académico
	session("codigo_cac")=session("Codigo_Cac2")
	session("descripcion_cac")=session("descripcion_Cac2")
	session("tipo_cac")=session("tipo_Cac2")
	session("notaminima_cac")=session("notaminima_cac2")
		
	'p=replace(request.QueryString("p"),".asp&",".asp?")
	p=request.QueryString("p")
	id=request.QueryString("id")
	ctf=request.QueryString("ctf")
	
	response.redirect(p & "?id=" & id & "&ctf=" & ctf)
	'response.Write(p & "?id=" & id & "&ctf=" & ctf)
else
    response.Write("<h2>Debe iniciar sesi&oacute;n!</h2>")
end if
 %>