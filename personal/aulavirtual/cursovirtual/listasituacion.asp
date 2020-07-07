<!--#include file="clscursovirtual.asp"-->
<%
	dim curso
	Set curso=new clscursovirtual
		with curso
			.restringir=session("idcursovirtual")
			Arrdatos=.Consultar("4",session("idcursovirtual"),session("codigo_usu"),"")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css" media="print"/>
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarcurso.js"></script>
<style fprolloverstyle>A:hover {color: #FF0000; font-weight: bold}
</style>
<style>
<!--
.0           { color: #FF0000;}
-->
</style>
</head>
<body>
<form name="frmLista" METHOD="POST" Action="procesar.asp?accion=enviaremail&modo=CV">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
  <tr>
    <td width="55%" class="e4">Desempeño de los participantes del Curso</td>
    <td width="35%" align="right" class="NoImprimir"><%'call enviaremail("cursovirtual",session("idcursovirtual"),"CV")%> |
    <span style="cursor:hand" onclick="ImprimirPagina('3')"> <img border="0" src="../../../images/imprimir.gif"> Imprimir</span></td>
  </tr>
</table>
<p class="sugerencia"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; P&nbsp;= Recurso publicado&nbsp; 
| D = Recurso descargado o realizado</b></p>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%">
  <thead>
  <tr class="etabla">
    <td width="7%" rowspan="2">Nº</td>
    <td width="13%" rowspan="2">Tipo</td>
    <td width="40%" rowspan="2">Apellidos y Nombres</td>
    <td width="6%" colspan="2">Documentos</td>
    <td width="6%" colspan="2">Encuestas</td>
    <td width="6%" colspan="2">Tareas</td>
    <td width="6%" colspan="2">Foro</td>
  </tr>
  <tr class="etabla">
    <td width="3%">P</td>
    <td width="3%">D</td>
    <td width="3%">P</td>
    <td width="3%">D</td>
    <td width="3%">P</td>
    <td width="3%">D</td>
    <td width="3%">P</td>
    <td width="3%">D</td>
  </tr>
  </thead>
  <%for i=lbound(Arrdatos,2) to Ubound(Arrdatos,2)
  		if ArrDatos(16,I)="0" then
  			nombre=Arrdatos(1,I) & "</a><span class=0>(Retirado)</span>"
  		else
  			nombre=Arrdatos(1,I) & "</a>"
  		end if
  %>
  <tr>
    <td width="7%"><%=i+1%>&nbsp;</td>
    <td width="13%"><%=Arrdatos(4,I)%>&nbsp;</td>
    <td width="40%"><a href="detallesituacion.asp?modo=3&idusuario=<%=Arrdatos(3,I)%>"><%=nombre%>&nbsp;</td>
    <%call .AlertaRecursoPtje(Arrdatos(8,I),ArrDatos(9,I))%>
    <%call .AlertaRecursoPtje(Arrdatos(10,I),ArrDatos(11,I))%>
    <%call .AlertaRecursoPtje(Arrdatos(12,I),ArrDatos(13,I))%>
    <%call .AlertaRecursoPtje(Arrdatos(14,I),ArrDatos(15,I))%>
  </tr>
  <%next%>
</table>
</form>
</body>
</html>
	<%end with
Set curso=nothing

%>