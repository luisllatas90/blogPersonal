<%
Dim modo,idTabla,idfecha

if session("codigo_usu")="" then response.redirect "../tiempofinalizado.asp"
modo=request.querystring("modo")
idfecha=request.querystring("idfecha")
IdTabla=session("idcursovirtual")
IF modo="" then modo="Consulta"
if idfecha="" then idfecha=0

Set Obj= Server.CreateObject("AulaVirtual.clscursovirtual")
	ArrDatos=Obj.ListaParticipantes(idtabla,"T")
	ArrFechas=Obj.ListaFechasPresenciales(IdTabla)
Set Obj=nothing

Function MostrarObs(idpresencial,idusuario)
	Set Obj= Server.CreateObject("AulaVirtual.clscursovirtual")
		MostrarObs=Obj.CargarAsistenciaUsuario(modo,idfecha,idpresencial,idusuario)
	Set Obj=nothing
End Function
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Asistencia de cursovirtual</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<style fprolloverstyle>A:hover {color: #FF0000}
</style>
</head>
<body>
<%If IsEmpty(ArrFechas) then%>
<h5>No se ha registrado fechas presenciales para el cursovirtual</h5>
<input name="Button" type="button" class="agregar" onclick="location.href='asistencia.asp'" value="Agregar fechas">
<input name="cmdRegresar" type="button" class="salir" onclick="location.href='../evaluacion/listaevaluacion.asp'" value="Regresar">
<%else%>
<form name="frmLista" method="POST" action="../procesar.asp?Accion=agregarasistencia&idtabla=<%=idtabla%>&idfecha=<%=idfecha%>">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="60%">
    <p class="e1">Asistencia Presencial de Actividad Académica</td>
    <td width="40%" align="right">
    <%if modo="editar" then%><input type="submit" value="Guardar" name="cmdGuardar" class="guardar"><%end if%><input name="Button" type="button" class="agregar" onclick="location.href='asistencia.asp'" value="Agregar fechas"> <input name="cmdRegresar" type="button" class="salir" onclick="location.href='../evaluacion/listaevaluacion.asp'" value="Regresar"></td>
  </tr>
</table>
<br>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr class="etabla">
    <td width="20" rowspan="2">Nº</td>
    <td width="280" rowspan="2">Apellidos y Nombres</td>
    	<td colspan="<%=Ubound(ArrFechas,2)+1%>">Fechas presenciales</td>
  </tr>
  <tr>
    <%for i=lbound(ArrFechas,2) to Ubound(ArrFechas,2)%>
    <td bgcolor="#DFEFFF" align="center"><a href="listaasistencia.asp?modo=editar&idfecha=<%=ArrFechas(0,I)%>&idtabla=<%=idtabla%>"><%=ArrFechas(1,I)%> <img border="0" src="../../../images/editar.gif" width="18" height="13" class="imagen"></a></td>
	<%next%>
  </tr>
  <%for u=lbound(Arrdatos,2) to Ubound(Arrdatos,2)%>
  	<tr>
	   	<td width="20"><%=u+1%>&nbsp;</td>
	   	<td width="280"><input type="hidden" name="txtidusuario" value="<%=Arrdatos(0,u)%>"><%=Arrdatos(1,u)%>&nbsp;</td>
	   	<%for i=lbound(ArrFechas,2) to Ubound(ArrFechas,2)%>
	    	<td><%=MostrarObs(ArrFechas(0,I),ArrDatos(0,u))%>&nbsp;</td>
	   	<%next%>
  	</tr>
  <%next%>
  </table>
</form>
<%end if%>
</body>
</html>