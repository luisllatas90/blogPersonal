<!--#include file="clstarea.asp"-->
<%
idtarea=request.querystring("idtarea")
idtipotarea=request.querystring("idtipotarea")

dim tarea
	Set tarea=new clstarea
		tarea.restringir=session("idcursovirtual")
		ArrDatos=tarea.Consultar("3",idtarea,"","")
	Set tarea=nothing
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>recursos de tarea</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validartarea.js"></script>
</head>
<body>
<p>Integra tarea con:
    <select size="1" id="cbxrecurso" onChange="IntegrarRecurso('A','<%=idtarea%>')">
    <option value="">--Seleccione el recurso--</option>
    <%if idtipotarea=3 or idtipotarea=5 then%>
    <option value="frmasignardocumentos.asp">Documentos publicados</option>
    <%else%>
    <option value="frmasignarencuestas.asp">Encuestas publicadas</option>
    <option value="frmasignartemaforo.asp">Temas de foro publicados</option>
    <%end if%>
    </select></p>
<%if IsEmpty(Arrdatos) then%>
	<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se han registrados recursos relacionados a la tarea</p>
<%else%>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%" height="80%">
  <tr class="etabla2">
    <td width="9%" height="10%">Nº</td>
    <td width="25%" height="10%">Tipo</td>
    <td width="66%" height="10%">Descripción</td>
  </tr>
  <tr><td colspan="3" height="90%">
  <div id="listadiv" style="height:100%;">
  <%for i=lbound(Arrdatos,2) to Ubound(Arrdatos,2)%>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%">
  <tr>
    <td width="9%"><img border="0" src="../../../images/eliminar.gif" class="imagen" onclick="IntegrarRecurso('E','<%=idtarea%>','<%=Arrdatos(0,i)%>')"><%=i+1%></td>
    <td width="25%"><%=Arrdatos(2,i)%>&nbsp;</td>
    <td width="66%"><%=Arrdatos(4,i)%>&nbsp;</td>
  </tr>
  </table>
  <%next%>
  </div>
  </td></tr>
</table>
<%end if%>
</body>
</html>