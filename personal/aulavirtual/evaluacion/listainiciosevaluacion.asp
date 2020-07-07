<%
IdEval=request.querystring("IdEval")
respuestacorrecta=Request.querystring("respuestacorrecta")

	Set Obj= Server.CreateObject("AulaVirtual.clsEvaluacion")
		ArrDatos=Obj.ListaAccesos(IdEval,session("codigo_usu"))
	Set Obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de inicios de evaluación</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
</head>

<body>

<p class="e1">Lista de inicios de sesión</p>

<%If IsEmpty(Arrdatos) then%>
	<h5>No se han registrado resultados en esta evaluación</h5>
<%else%>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="90%" id="AutoNumber1">
  <tr class="etabla">
    <td width="22">&nbsp;</td>
    <td width="204">Fecha de Entrada</td>
    <td width="327">Usuario</td>
    <td width="121">Fecha de Salida</td>
    <td width="91">IP</td>
  </tr>
  <%for i=Lbound(Arrdatos,2) to Ubound(arrDatos,2)%>
  <tr style="cursor:hand" onClick="location.href='evaluar/verresultados.asp?criterio=consultar&idEval=<%=idEval%>&IdIni=<%=ArrDatos(0,I)%>&respuestacorrecta=<%=respuestacorrecta%>'">
    <td width="22"><img border="0" src="../../../images/der.gif"></td>
    <td width="204"><%=ArrDatos(1,i)%>&nbsp;</td>
    <td width="327"><%=ArrDatos(3,i)%>&nbsp;</td>
    <td width="121"><%=ArrDatos(4,i)%>&nbsp;</td>
    <td width="91"><%=ArrDatos(5,i)%>&nbsp;</td>
  </tr>
  <%next%>
</table>
<%end if%>
<p>Haga clic en la fecha que Ud. ha registrado su tarea</p>
</body>
</html>