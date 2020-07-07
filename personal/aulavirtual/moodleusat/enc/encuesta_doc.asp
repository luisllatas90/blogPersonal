<!--#include file="../../evaluacion/clsevaluacion.asp"-->
<%
Dim IdEvaluacion
Dim codigo_acceso
Dim respuestacorrecta

idevaluacion=request.querystring("idevaluacion")
idinicio=request.querystring("idinicio")

'Response.ContentType = "application/msword"
'Response.AddHeader "Content-Disposition","attachment;filename=encuesta" & idinicio & ".doc"

Set evaluacion=new clsevaluacion
	with evaluacion
		.Restringir=Idevaluacion
		ArrDatos=.Consultar("9",idevaluacion,idinicio,"")
		
		If IsEmpty(Arrdatos)=false then
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<style>
<!--
.e4          { font-family: Arial; color: #800000; font-size: 12pt; font-weight: bold }
.imprimir  {border:1px solid #C0C0C0; background:#FEFFE1 url('../../../images/imprimir.gif') no-repeat 0% 80%; width:100; font-family:Tahoma; font-size:8pt; font-weight:bold; height:25}
td           { font-family: Arial; font-size: 8pt }
-->
</style>
<title>Encuesta realizada</title></head>
<body>
<h5 align="center" class="e4"><%=(arrdatos(13,0))%></h5>
<p>
<i>Realizado por:</i> <%=arrdatos(16,0)%><br>
<i>Fecha de realización:</i> <%=arrdatos(2,0)%>
</p>
<%for I=Lbound(Arrdatos,2) to Ubound(Arrdatos,2)%>
<center>
<table border="0" cellpadding="5" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="90%">
  <tr>
    <td width="100%" bgcolor="#F7EEB3" valign="top"><b><%=Arrdatos(7,I) & ".&nbsp;" & PreparaMemo(Arrdatos(5,I))%></b></td>
  </tr>
  <%if (Arrdatos(12,I)=1 or Arrdatos(12,I)=5) then
  else%>
  <tr>
    <td width="100%" height="14" valign="top">
    	<%call .RptaAlternativaElegida(Arrdatos(4,I),Arrdatos(6,I))%>
   	</td>
  </tr>
  <%end if%>
  <tr>
    <td width="100%" valign="top">
    <i>Respuesta:<br></i>
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
<p align="right"><input type="button" name="Imprimir" value="Imprimir" class="imprimir" onclick="window.print()"></p>
</body>
</html>
		<%else
			response.Write "<h3>No se han registrado respuestas para la encuesta</h3>"
		end if
	end with
Set evaluacion=nothing

%>