<!--#include file="clsagenda.asp"-->
<%
	Mes=request.querystring("Mes")
	Anio=request.querystring("Anio")
	fecha=request.querystring("fecha")
	idcategoria=request.querystring("idcategoria")
	modo=Request.querystring("modo")

	if idcategoria="" then idcategoria=0
	if modo="" then modo=1
	if mes="" then mes=Month(date)
	if anio="" then anio=year(date)

	Dim agenda
		Set agenda=new clsagenda
			agenda.restringir=session("idcursovirtual")
			ArrDatos=agenda.Consultar(modo,Mes,Anio,fecha,session("Idcursovirtual"),session("codigo_usu"))
		Set agenda=nothing
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de cursovirtuals</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<base target="listaagenda">
<style>
<!--
td           { border: 1px solid #FFFFFF }
-->
</style>
</head>
<body>
<%If IsEmpty(Arrdatos)=false then%>
<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Para elegir el evento de la 
agenda haga clic en la opción <img border="0" src="../../../images/radio.gif" align="middle"></p>
<%end if%>
<p class="e1">Eventos para el mes de <%=MonthName(mes)%></p>
<input type="hidden" id="txtelegido">
<%
If IsEmpty(Arrdatos)=false then
	for i=lbound(Arrdatos,2) to ubound(Arrdatos,2)%>
<table class="bloque" border="1" cellpadding="4" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber3">
  <tr>
    <td width="21" rowspan="5" valign="top"><b><font color="#800000"># <%=(i+1)%></font></b>&nbsp;<br>
    <%if arrdatos(7,i)=session("nombre_usu") and session("idestadocursovirtual")=1 then%>
	    <input type="radio" name="opt" value="<%=arrdatos(0,i)%>" onClick="HabilitarEleccion(this,'N')"></a>
   <%end if%>
    <img src="../../../images/<%=iif(arrdatos(9,i)=3,"p1","todos")%>.gif" ALT="<%="Recurso compartido para " & arrdatos(10,i)%>"/>
    </td>
    <td><b>Duración</b></td>
    <td><%=arrdatos(2,i)%> hasta <%=arrdatos(3,i)%>&nbsp;</td>
  </tr>
  <tr>
    <td width="92"><b>Tipo</b></td>
    <td width="341" colspan="4"><b><%=ucase(arrdatos(6,i))%></b>&nbsp;</td>
  </tr>
  <tr>
    <td width="92"><b>Evento</b></td>
    <td width="341" id="nombreevento<%=arrdatos(0,i)%>" colspan="4"><%=arrdatos(1,i)%>&nbsp;</td>
  </tr>
  <tr>
    <td width="92" valign="top"><b>Descripción</b></td>
    <td width="341" colspan="4" valign="top"><%=preparamemo(arrdatos(5,i))%>&nbsp;</td>
  </tr>
  <tr>
    <td width="92"><b>Lugar</b></td>
    <td width="341" colspan="4"><%=arrdatos(4,i)%>&nbsp;</td>
  </tr>
  <tr>
    <td width="100%" colspan="5" align="right" class="variable"><b>Organizado por:&nbsp;</b><%=arrdatos(7,i)%>&nbsp;/&nbsp; <%=arrdatos(8,I)%>&nbsp;</td>
  </tr>
</table>
<br>
<%next
else%>
	<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se han registrado eventos en la Agenda</p>
<%end if%>
</body>
</html>