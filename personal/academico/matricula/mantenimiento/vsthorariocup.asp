<!--#include file="../../../../funciones.asp"-->
<%
codigo_cup=request.querystring("codigo_cup")
nombre_cur=request.querystring("nombre_cur")

Set objCursoProg=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objCursoProg.AbrirConexion
	Set rsHorario= objCursoProg.Consultar("ConsultarHorarios","FO","4",codigo_cup,0,0)
	objCursoProg.CerrarConexion
Set objCursoProg=nothing
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Horario del Curso Programado</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
</head>

<body>
<p class="usatTitulo"><%=ucase(nombre_cur)%></p>
<%if rsHorario.BOF and rsHorario.EOF then%>
<p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se ha registrado del horario del Curso Programado</p>
<%else%>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr class="etabla">
    <td width="10%">Día</td>
    <td width="10%">Hora Inicio</td>
    <td width="10%">Hora Fin</td>
    <td width="15%">Ambiente</td>
    <td width="40%">Docente</td>
  </tr>
  <%Do while Not rsHorario.EOF%>
  <tr>
    <td width="10%"><%=ucase(ConvDia(rsHorario("dia_Lho")))%>&nbsp;</td>
    <td width="10%"><%=rsHorario("nombre_Hor")%>&nbsp;</td>
    <td width="10%"><%=rsHorario("horaFin_Lho")%>&nbsp;</td>
    <td width="15%"><%=rsHorario("ambiente")%>&nbsp;</td>
    <td width="40%"><%=rsHorario("docente")%>&nbsp;</td>
  </tr>
  	<%rsHorario.movenext
  	Loop
  %>
</table>
<%end if
Set rsHorario=nothing
%>
</body>

</html>