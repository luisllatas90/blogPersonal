<!--#include file="../../../../NoCache.asp"-->
<%

if session("codigo_usu")="" then response.redirect "../../../../tiempofinalizado.asp"

accion=request.querystring("accion")

if accion="agregarcursovirtual" then 'Matricular en curso de la usat
	dim mensaje,raiz,carpeta
	
		tipo=request.form("chkagrupar")
		if tipo="" then tipo="I"
		ArrCP=split(request.form("chkcursoshabiles"),",")
		codigo_per=request.querystring("codigo_per")
		codigo_cac=request.querystring("codigo_cac")
		login_per=request.querystring("login_per")
		'raiz=Server.MapPath("../../../archivoscv/")
		raiz="t:\documentos aula virtual\archivoscv\"
		
		Set fso = CreateObject("Scripting.FileSystemObject")
		Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		Set objCurso=Server.CreateObject("AulaVirtual.clsCursoVirtual")
		
		obj.AbrirConexion
		redim mensaje(Ubound(arrCP))
		for i=lbound(ArrCP) to Ubound(ArrCP)
			codigo_cup=trim(ArrCP(i))
			if codigo_cup="" then codigo_cup=0
			mensaje(i)=Obj.Ejecutar("AgregarCursoVirtual",true,tipo,codigo_per,codigo_cup,codigo_cac,login_per,0,null)
			arrCurso=ObjCurso.Consultar(5,codigo_cup,0,0)
	
			carpeta=raiz & arrCurso(0,0)

				'Verificar si existe la carpeta con el curso virtual
				If (Not fso.FolderExists(carpeta)) then
					Set fol = fso.CreateFolder(carpeta)
					Set fol = fso.CreateFolder(carpeta & "\documentos")
					Set fol = fso.CreateFolder(carpeta & "\tareas")
					Set fol = fso.CreateFolder(carpeta & "\images")
				End if
			'response.write carpeta
		next
		Obj.CerrarConexion
		Set Obj=nothing
		Set ObjCurso=nothing
		Set fso=nothing
		Set fol=nothing

		%>
		<html>
		<head>
		<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
		</head>
		<body bgcolor="#F0F0F0">
		<table border='0' cellpadding='3' class='contornotabla' cellspacing='0' style='border-collapse: collapse' width='100%'>
		<tr><td align='center' class='usattitulousuario'><u>Reporte de cursos habilitados</u></td></tr>
		<tr><td align='right' style='cursor:hand' onClick='history.back(-1)'><i>Haga click aquí para Regresar a la Página anterior</i></td></tr>
		<tr><td width='100%'>
		<ul>
		<%
		for j=lbound(mensaje) to ubound(mensaje)
			response.write "<li>" & mensaje(j) & "</li>"
		next
		%>
		</ul>
		</td></tr>
		</table>
		</body>
		</html>
		<%
end if


if accion="copiardocumentosanteriores" then
	idcursovirtualorigen=request.querystring("idcursovirtualorigen")
	idcursovirtualdestino=request.querystring("idcursovirtualdestino")

	Set Obj=Server.CreateObject("AulaVirtual.clsAccesoDatos")
	obj.AbrirConexion
		mensaje=Obj.Ejecutar("CopiarDocumentosAnteriores",true,idcursovirtualorigen,idcursovirtualdestino,session("codigo_usu"),null)
	Obj.CerrarConexion
	Set Obj=nothing

	response.write "<script language='javascript'>alert('" & mensaje & "');window.opener.location.reload();window.close()</script>"
end if
%>