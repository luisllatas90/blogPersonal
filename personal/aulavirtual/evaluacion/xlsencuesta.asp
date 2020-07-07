<!--#include file="clsevaluacion.asp"-->
<%
idevaluacion=request.querystring("idevaluacion")

Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=encuesta" & idevaluacion & ".xls"

	set evaluacion=new clsevaluacion
	with evaluacion
		.restringir=session("idcursovirtual")
		ArrDatos=.Consultar("3",idevaluacion,"","")
		ArrEncuestados=.Consultar("14",idevaluacion,"","")		
	
	totalreg=ubound(ArrDatos,2)+1

if IsEmpty(Arrdatos)=false then%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>ver resultados de encuesta</title>
<style>
<!--
.etabla      { background-color: #EBE1BF; text-align:center }
.e1          { font-size: 12pt; font-weight: bold; font-family:Arial }
body         { font-family: Verdana; font-size: 8pt }
td           { font-size: 8pt }
-->
</style>
</head>

<body>
<p class="e1">Resultados de encuesta</p>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
  <tr class="etabla">
    <td width="6%" rowspan="2">&nbsp;</td>
    <td width="44%" rowspan="2">Participante</td>
    <td width="5%" colspan="<%=totalreg%>">Preguntas</td>
  </tr>
  <tr class="etabla">
    <%for i=1 to totalreg%>
    <td width="5%"><%=i%>&nbsp;</td>
    <%next%>
  </tr>
  <%for j=lbound(ArrEncuestados,2) to ubound(ArrEncuestados,2)%>
  <tr>
    <td width="6%" valign="top"><%=j+1%>&nbsp;</td>
    <td width="44%" valign="top"><%=ArrEncuestados(1,j)%>&nbsp;</td>
    <%for i=lbound(Arrdatos,2) to Ubound(Arrdatos,2)%>
    <td width="5%" valign="top"><%=.MostrarRptaUsuario(Arrdatos(1,i),ArrEncuestados(0,j))%>&nbsp;</td>
    <%next%>
  </tr>
  <%next%>
</table>
<h5><u>Leyenda de preguntas</u></h5>

<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="80%" id="AutoNumber3">
  <tr class="etabla">
    <td width="8%">#</td>
    <td width="92%">Descripción de pregunta</td>
  </tr>
  <%for i=lbound(Arrdatos,2) to ubound(Arrdatos,2)%>
  <tr>
    <td width="8%" valign="top"><%=i+1%>&nbsp;</td>
    <td width="92%" valign="top"><%=Arrdatos(5,i)%>
    <%call .ConsultarAlternativasPregunta(Arrdatos(1,i))%>&nbsp;</td>
  </tr>
  <%next%> &nbsp;</table>
</body>
</html>
<%end if
end with
	set evaluacion=nothing
%>