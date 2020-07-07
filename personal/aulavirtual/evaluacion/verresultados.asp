<!--#include file="clsevaluacion.asp"-->
<%
Dim IdEvaluacion
Dim codigo_acceso
Dim respuestacorrecta

'Verificar que se han respondido todas la preguntas en caso contrario colocar en vacio
session("EstadoActual")="TerminoRespuestas"
IdEvaluacion=request.querystring("IdEvaluacion")
respuestacorrecta=Request.querystring("respuestacorrecta")
codigo_acceso=request.QueryString("codigo_acceso")
exportar=request.querystring("exportar")
modo=3

If idEvaluacion="" then
	IdEvaluacion=session("idEvaluacion")
	respuestacorrecta=session("respuestacorrecta")
	codigo_acceso=session("codigo_acceso")
	modo=1
end if

if exportar="S" then
	Response.ContentType = "application/msword"
	Response.AddHeader "Content-Disposition","attachment;filename=encuesta" & codigo_acceso & ".doc"
end if
	'-------------------------------------------
	'Variables de pagina web
	'-------------------------------------------
	sURL = Request.ServerVariables("SCRIPT_NAME")
	if Request.ServerVariables("QUERY_STRING") <> "" Then
		sURL = sURL & "?" & Request.ServerVariables("QUERY_STRING")
		session("Pagina")=sURL
	else
		sURL=sURL
		session("Pagina")=sURL
	End if
	'-------------------------------------------

Set evaluacion=new clsevaluacion
	with evaluacion
		.Restringir=Idevaluacion
		ArrDatos=.Consultar("9",idEvaluacion,codigo_acceso,"")
		
		If IsEmpty(Arrdatos)=false then
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css" media="print"/>
<style>
<!--
.e4          { font-family: Arial; color: #800000; font-size: 12pt; font-weight: bold }
.imprimir  {border:1px solid #C0C0C0; background:#FEFFE1 url('../../../images/imprimir.gif') no-repeat 0% 80%; width:100; font-family:Tahoma; font-size:8pt; font-weight:bold; height:25}
td           { font-family: Arial; font-size: 8pt }
-->
</style>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="79%" class="e4">Resultados de la Evaluación</td>
    <td width="21%" align="right" class="NoImprimir"><input onClick="ImprimirPagina('<%=modo%>','1')" type="button" id="Imprimir" value="  Imprimir" class="imprimir"/></td>
  </tr>
</table>
<br>
<%for I=Lbound(Arrdatos,2) to Ubound(Arrdatos,2)%>
<center>
<table border="0" cellpadding="5" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="80%">
  <tr>
    <td width="11%" bgcolor="#F7EEB3" height="22" valign="top"><b>Pregunta <%=Arrdatos(7,I)%></b></td>
    <td width="51%" bgcolor="#F7EEB3" height="22" valign="top"><b><%=PreparaMemo(Arrdatos(5,I))%></b>&nbsp;</td>
  </tr>
  <%if (Arrdatos(12,I)=1 or Arrdatos(12,I)=5) then
  else%>
  <tr>
    <td width="11%" height="14" valign="top"></td>
    <td width="51%" height="14" valign="top">
    	<%call .RptaAlternativaElegida(Arrdatos(4,I),Arrdatos(6,I))%>
   	</td>
  </tr>
  <%end if%>
  <tr>
    <td width="11%" height="14" valign="top"><i>Respuesta</i></td>
    <td width="51%" height="14" valign="top">
    <%if Arrdatos(12,I)=1 or Arrdatos(12,I)=5 then
    	response.write PreparaMemo(Arrdatos(6,I))
    else
    	response.write .RecuperaRespuestas(Arrdatos(4,I),Arrdatos(6,I),respuestacorrecta)
    end if
    %>
    </td>
  </tr>
  </table>
  </center>
<br>
<%next%>
</body>
</html>
		<%else
			response.Write "<h3>No se han registrado respuestas en esta evaluación</h3>"
		end if
	end with
Set evaluacion=nothing

%>