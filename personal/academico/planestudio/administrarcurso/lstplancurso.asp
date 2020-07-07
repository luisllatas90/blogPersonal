<!--#include file="../../../../funciones.asp"-->
<%
codigo_cur=request.querystring("codigo_cur")

if codigo_cur<>"" then

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsPlanes=obj.Consultar("ConsultarCursoPlan","FO","2",codigo_cur,0,0)
	Obj.CerrarConexion
	
Set Obj=nothing
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>PlanCurso</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
</head>
<body bgcolor="#EFEFEF">
<%
Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio,pagina

	ArrEncabezados=Array("ID","Plan de Estudio","Escuela Profesional")
	ArrCampos=Array("codigo_pes","descripcion_pes","nombre_cpf")
	ArrCeldas=Array("10%","50%","40%")

	Call CrearRpteTabla(ArrEncabezados,rsPlanes,"",ArrCampos,ArrCeldas,"N","","","N","","")
%>
</body>
</html>
<%end if%>