<!--#include file="clsevaluacion.asp"-->
<%
	idevaluacion=request.querystring("idevaluacion")

	set evaluacion=new clsevaluacion
	with evaluacion
		.restringir=session("idcursovirtual")
		ArrDatos=.Consultar("3",idevaluacion,"","")
	end with
	set evaluacion=nothing
	totalreg=ubound(ArrDatos,2)+1
	
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Preguntas de la evaluación</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarevaluacion.js"></script>
</head>
<body>
<input type="button" onClick="location.href='frmpregunta.asp?accion=agregarpregunta&idevaluacion=<%=idevaluacion%>&totalpreg=<%=totalreg%>'" value="Crear nueva pregunta" name="cmdnueva" class="nuevo">
<br><br>
<%if IsEmpty(ArrDatos) then%>
	<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se ha registrado preguntas para la evaluación</p>
<%else%>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%" height="80%">
  <tr class="etabla">
    <td width="5%" height="5%">Orden</td>
    <td width="10%" height="5%">Tipo</td>
    <td width="60%" height="5%">Enunciado</td>
    <td width="10%" height="5%">Obligatoria</td>
    <td width="10%" height="5%">&nbsp;</td>
  </tr>
  <tr><td colspan="5" width="100%" height="95%">
  <DIV id="listadiv" style="height:100%">
  <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#EEEEEE" width="100%" id="tblpreguntas">
  <%for i=Lbound(Arrdatos,2) to ubound(Arrdatos,2)%>
  <tr>
    <td width="5%"><%=Arrdatos(4,I)%>&nbsp;</td>
    <td width="10%"><%=Arrdatos(3,I)%>&nbsp;</td>
    <td width="60%"><%=Arrdatos(5,I)%>&nbsp;</td>
    <td width="10%"><%=IIF(Arrdatos(11,I)=1,"Sí","No")%>&nbsp;</td>
    <td width="10%" align="right">
    	<a TARGET="_self" href="frmpregunta.asp?accion=modificarpregunta&idpregunta=<%=Arrdatos(1,I)%>&idevaluacion=<%=idevaluacion%>&totalpreg=<%=totalreg%>">
    	<img border="0" src="../../../images/editar.gif"></a>
    	<img border="0" src="../../../images/eliminar.gif" onclick="EliminarPregunta('<%=Arrdatos(1,I)%>','<%=idevaluacion%>');" class="imagen"></td>
  </tr>
  <%next%>
  </table>
  </td></table>
  </DIV>
</table>
<%end if%>
</body>

</html>