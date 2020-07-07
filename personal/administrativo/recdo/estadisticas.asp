<!--#include file="clsrecepcion.asp"-->
<!--#include file="../../../clsgrafico2.asp"-->
<%
Dim arrEtiquetas
Dim arrValores
Dim grafico

	set recepcion=new clsrecepcion
		ArrDatos=recepcion.ConsultarArchivosRegistrados("8",session("idanio"),0,0)
		totalReg=Ubound(ArrDatos,2)+1
	Set recepcion=nothing
%>
<html>
<head>
<title>estadisticas</title>
<meta name=vs_defaultClientScript content="JavaScript">
<meta name=vs_targetSchema content="http://schemas.microsoft.com/intellisense/ie3-2nav3-0">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name=ProgId content=FrontPage.Editor.Document>
<meta name=Originator content="Microsoft Visual Studio.NET 7.0">
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
</head>
<body>

<%if IsEmpty(ArrDatos)=true then%>
	<h3>No se han registrado documentos en la base de datos</h3>
<%else
	redim arrEtiquetas(totalreg)
	redim arrValores(totalreg)
	
	for i=lbound(Arrdatos,2) to Ubound(ArrDatos,2)
		arrEtiquetas(i)=Arrdatos(0,i)
		arrValores(i)= int(Arrdatos(1,i))
	next

	set grafico = new HorizontalBarChart
	with grafico
		.outerborder="1"
		.title = "Estadística de recepción de documentos en el año " & session("nombreanio")
		.titlefontsize = "4"
		.text = arrEtiquetas
		.values = arrValores
		.SumIs100 = true
		.colors = "orange,black,green"
		.Order = "TBV"
		.DataAlign = "left"
		.ScaleColor = "green"
		.ShowPercentage = false
		.ShowTotal = true
		.BarHeight = "18"
		.buildgraph
		TT = .Total
	end with
	set grafico = nothing
end if
%>
</body>
</html>