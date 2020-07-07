<!--#include file="clsforo.asp"-->
<%
idforo=request.querystring("idforo")
tituloforo=request.querystring("tituloforo")
idestadorecurso=request.querystring("idestadorecurso")
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>recursos de foro</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarforo.js"></script>
</head>
<body>
<%if idestadorecurso=3 then%>
	<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Actualmente el tema de discusión se encuentra bloqueado por haber finalizado la fecha de diponibilidad</p>
<%end if%>
<table cellspacing="0" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%" height="100%">
  <tr class="etiqueta">
    <td width="10%" valign="top" height="5%">Tema:</td>
    <td width="75%" valign="top" height="5%"><%=tituloforo%>&nbsp;</td>
    <td width="10%" class="e1" valign="top" height="5%">
    <%if idestadorecurso=1 then%>
    <input type="button" value="  Nuevo mensaje" name="cmdresponder" class="boton4" onclick="AbrirMensaje('A','<%=idforo%>','<%=tituloforo%>')">
    <%end if%>
    </td>
  </tr>
  <tr>
    <td width="100%" class="etiqueta" valign="top" colspan="3" height="95%">
   	<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%" height="100%">
  <tr class="etabla2">
    <td width="55%" height="5%">Debate</td>
    <td width="20%" height="5%">Empezado por</td>
    <td width="10%" height="5%">Respuestas</td>
    <td width="15%" height="5%">Último mensaje</td>
  </tr>
  <tr><td colspan="4" height="90%" width="100%" valign="top">
  <div id="listadiv" style="height:100%">
  	<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%">
		<%
		Dim foro
		Set foro=new clsforo
			with foro
				.restringir=session("idcursovirtual")		
				.CargarArbolRptas idforo,tituloforo,idestadorecurso,0,0
			end with
		Set foro=nothing		
		%>
	</table>
  </div>
  </td>
  </tr>
</table>
	</td>
  </tr>
</table>
</body>
</html>