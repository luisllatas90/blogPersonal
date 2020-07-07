<%
Function ObtenerParticipacion(ByVal ID)
	Set Obj= Server.CreateObject("AulaVirtual.clscursovirtual")
		ObtenerParticipacion=Obj.CargarDesempenoUsuario("consultar",ID,ID,session("codigo_usu"))
	Set Obj=nothing
	If ObtenerParticipacion="" then
		ObtenerParticipacion="<font color=""#FF0000"">No ha participado de este criterio de evaluación</font>"
	Else
		ObtenerParticipacion="<font color=""#0000FF"">" & ObtenerParticipacion & "</font>"
	End if
End Function

Set Obj= Server.CreateObject("AulaVirtual.clscursovirtual")
	ArrCriterio=Obj.ListaCriteriosEvaluacion(session("idcursovirtual"))		
Set Obj=nothing
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Ver el desempeño de los usuarios</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
</head>

<body>
<%
if IsEmpty(ArrCriterio) then
	response.write "<h3>Ud. no tiene registrado ningún criterio de evaluación</h3>"
else
%>
<p class="e1">Resultados de su desempeño en la actividad Académica</p>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="50%" id="AutoNumber3">
  <tr class="etabla">
    <td width="7%">#</td>
    <td width="43%">Criterio de evaluación</td>
    <td width="50%">Observaciones</td>
  </tr>
  <%for i=lbound(ArrCriterio,2) to Ubound(ArrCriterio,2)%>
  <tr>
    <td width="7%"><%=(i+1)%>&nbsp;</td>
    <td width="43%"><%=ArrCriterio(1,i)%>&nbsp;</td>
    <td width="50%"><%=ObtenerParticipacion(ArrCriterio(0,i))%>&nbsp;</td>
  </tr>
  <%next%>
</table>
<%end if%>
<p><input name="cmdRegresar" type="button" class="salir" onclick="location.href='listaevaluacion.asp'" value="Regresar"></p>
</body>
</html>