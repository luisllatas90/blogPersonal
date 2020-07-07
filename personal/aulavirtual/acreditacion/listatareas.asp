<!--#include file="clsacreditacion.asp"-->
<%
if session("codigo_usu")="" then response.redirect "../tiempofinalizado.asp"

	dim tarea,idOpt
	
	idOpt=request.querystring("idOpt")
	idOpt=iif(idOpt="",0,idOpt)
	
function cargaravances(id,autorizado,idevalind,idvar,idsecc)
	response.write "<tr id='tbltarea" & id & "' style='display:none'>"
	response.write "<td colspan=""7"" align=""right"">"
	%>
	<img border="0" src="../../../images/beforelastnode.gif" align="top">
	<iframe name="fratarea<%=id%>" src="listaavancestarea.asp?idtareaevaluacion=<%=id%>&autorizado=<%=autorizado%>&idevaluacionindicador=<%=idevalind%>&idvariable=<%=idvar%>&idseccion=<%=idsecc%>" style="border:1px solid #FFFFFF; width:98%" border="0" frameborder="0" height="100">
El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe>
	<%response.write "</tr></td>"
end function

	Set tarea=new clsacreditacion
					
		with tarea
			Arrdatos=.ConsultarEvaluacionModeloAcreditacion("7",session("codigo_usu"),session("idmodelo"),0)
			If IsEmpty(Arrdatos)=false then
				totalreg=Ubound(ArrDatos,2)+1
			end if
		end with
	Set acreditacion=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Listado de tareas asignadas para evaluar indicadores</title>
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
</head>
<body>
<%if IsEmpty(ArrDatos)=true then%>
	<h3>No se han asignado tareas a realizar</h3>
<%else%>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr>
    <td class="e1" width="404">Lista de tareas asignadas (<%=totalreg%>)</td>
    <td align="right">&nbsp;</td>
  </tr>
</table>
<br>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" id="AutoNumber2">
  <tr class="etabla2">
  	<td>&nbsp;</td>
    <td>#</td>
    <td>Descripción de la tarea</td>
    <td>Inicio - Fin</td>
    <td>Indicador a evaluar</td>
    <td>Estado</td>
    <td>Avance</td>
  </tr>
  <%for i=lbound(Arrdatos,2) to Ubound(Arrdatos,2)%>
  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onClick="MostrarTabla(tbltarea<%=Arrdatos(0,I)%>,'../images/',imgtarea<%=Arrdatos(0,I)%>)">
  	<td width="2%" align="right">
    <img id="imgtarea<%=Arrdatos(0,I)%>" src="../../../images/mas.gif"></td>
    <td><%=i+1%>&nbsp;</td>
    <td><%=Arrdatos(1,I)%>&nbsp;</td>
    <td><%=Arrdatos(2,I)%> - <%=Arrdatos(3,I)%>&nbsp;</td>
    <td><%=Arrdatos(5,I)%>&nbsp;</td>
    <td <%=iif(Arrdatos(7,I)="P","class=rojo","class=azul")%>><%=iif(Arrdatos(7,I)="P","Pendiente","Realizada")%>&nbsp;</td>
    <td id="estadotarea<%=Arrdatos(0,I)%>"><%=Arrdatos(4,I)%>%&nbsp;</td>
  </tr>
  	<%cargaravances Arrdatos(0,I),Arrdatos(6,I),Arrdatos(8,I),Arrdatos(9,I),ArrDatos(10,I)
  next%>
</table>
<%end if%>
</body>
</html>