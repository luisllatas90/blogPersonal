<!--#include file="clscursovirtual.asp"-->
<%
	Set curso=new clscursovirtual
		with curso
			.Restringir=session("idcursovirtual")
			ArrDatos=.Consultar("3",session("idcursovirtual"),"","")
		end with 
		web=ArrDatos(6,0)
	Set curso=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta http-equiv="imagetoolbar" content="no">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Portada</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<base target="_self">
</head>
<%=web%>
</html>