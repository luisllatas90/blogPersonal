<!--#include file="../NoCache.asp"-->
<%
if Session("EGRESADOALUMNI")= "1" then 'tgd: revisar cambio de estado
  BANNER="../images/bannerAlumni_v6.png"
ELSE
  'response.Write (Session("es_egresado"))
  BANNER="../images/bannerUSAT_v3.jpg"
end if
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Menú superior</title>
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<base target="contenido">
</head>

<body topmargin="0" leftmargin="0" bgcolor="#F8F8F8">

<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" bgcolor="#999999" height="118">
  <tr>
    <td width="20%" height="82" colspan="2" style="width: 100%">
        <img src=<%=BANNER%> width="100%" height="90px"></td>
  </tr>
  <tr >
    <td class="bordeinf" width="75%" height="21" bgcolor="#F8F8F8" 
          style="text-align: left">
    &nbsp; <strong>USUARIO: <%=session("nombre_usu")%></strong> /<span class="rojo"> <%=session("nombre_cpf")%></span>&nbsp;</td>
    <td class="bordeinf" width="25%" height="21" bgcolor="#F8F8F8" ><%=formatdatetime(now,1)%></td>
  </tr>
</table>

</body>

</html>