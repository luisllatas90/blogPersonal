<!--#include file="../NoCache.asp"-->
<%tipomnu=request.querystring("tipomnu")%>
<html>
<head>
    <meta http-equiv="Content-Language" content="es-Pe">
    <meta name="GENERATOR" content="Microsoft FrontPage 5.0">
    <meta name="ProgId" content="FrontPage.Editor.Document">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <% 
    if session("descripcion_apl") = "CRM USAT" OR session("descripcion_apl") = "GRADOS Y TITULOS" OR  session("descripcion_apl") = "PERSONAL" OR session("codigo_apl") = 11 OR session("codigo_apl") = 50   OR session("codigo_apl") = 53 or session("codigo_apl") =21 then    
        response.Write("<meta http-equiv='X-UA-Compatible' content='IE=edge'>")
    end if
    %>
    <!--<meta http-equiv="X-UA-Compatible" content="IE=edge">-->
    <title>Campus Virtual USAT:<%=session("descripcion_apl")%></title>
</head>
<frameset rows="3%,*" frameborder="0" border="0" framespacing="0">
  <frame src="cabecera.asp" name="fraArriba" scrolling="No" id="fraArriba">
  <frameset rows="*" framespacing="0" frameborder="no" border="0">
    <frameset cols="24%,*" framespacing="0" frameborder="no" border="0" id="fraGrupo">
    <frame src="<%=tipomnu%>" name="fraIzq" scrolling="no" id="fraIzq">
    <frame src="about:blank" name="fraPrincipal" id="fraPrincipal">
  	</frameset>
  </frameset>
</frameset>
<noframes>
    <body>
    </body>
</noframes>
</html>
