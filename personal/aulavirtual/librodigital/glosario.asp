<!--#include file="clslibrodigital.asp"-->
<%
modo=request.querystring("modo")
idlibrodigital=request.querystring("idlibrodigital")
letra=request.querystring("letra")
if letra="" then letra="A"
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Glosario de términos</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarlibrodigital.js"></script>
</head>
<body>
<table class="bordeinf" border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="50%" class="e4">Glosario de términos</td>
    <td width="50%" align="right">
    <%if modo="administrar" then%><input type="button" onclick="AbrirGlosario('A','<%=idlibrodigital%>')" value="Nuevo término" name="cmAgregar" class="nuevo"><%end if%>&nbsp; 
<input type="button" onclick="AbrirGlosario('B','<%=idlibrodigital%>','','<%=modo%>')" value="Buscar" name="cmBuscar" class="buscar"> </td>
  </tr>
</table>
<br>
<%
Set contenido=new clslibrodigital
	with contenido
		.restringir=session("idcursovirtual")
		response.write .generarletras(idlibrodigital,modo)
	end with
Set contenido=nothing
%>
<br>
<iframe class="contornotabla" id="fradetalleglosario" name="fradetalleglosario" height="80%" width="100%" src="detalleglosario.asp?modo=<%=modo%>" border="0" frameborder="0">
El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe>
</body>
</html>