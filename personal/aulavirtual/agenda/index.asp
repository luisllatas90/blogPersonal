<!--#include file="../clscalendario.asp"-->
<!--#include file="../../../funcionesaulavirtual.asp"-->
<%
Modo=request.querystring("Modo")
Mes=request.querystring("Mes")
Anio=request.querystring("Anio")
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Agenda</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validaragenda.js"></script>
</head>
<body>
<%=BarraHerramientas("AbrirAgenda","N")%>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1" height="90%">
  <tr>
    <td width="70%" valign="top">
    <iframe name="fralista" id="fralista" src="listaagenda.asp?modo=<%=modo%>&Mes=<%=Mes%>&Anio=<%=Anio%>" height="100%" width="100%" class="contornotabla" border="0" frameborder="0" target="listaagenda">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe></td>
    <td width="30%" valign="top"><%call mostrarcalendario("../../../images","fralista.location.href='listaagenda.asp",session("Idcursovirtual"),session("codigo_usu"))%>&nbsp;</td>
  </tr>
</table>
</body>
</html>