<!--#include file="../NoCache.asp"-->
<%
'************* ANTES **************
'Pagina=Request.querystring("Pagina")
'response.Redirect ("AsignaSesiones.aspx?cac=" & session("Codigo_Cac") & "&alu=" & session("codigo_alu") & "&codUni=" & session("codigoUniver_alu"))

'Enviado desde abriraplicacion.asp
Pagina = session("enlace_apl")
if Pagina="" then Pagina="avisos.asp"
'response.Write session("encuesta")

'****Para encuesta DD yperez***'
if Session("TieneEncuestaDocente")>0 and request.querystring("op") <> "1" then
  'response.Redirect ("AsignaSesiones.aspx?x=" & Session("TieneEncuestaDocente") & "&y=" & session("codigo_alu") & "&z=../librerianet/Encuesta/EvaluacionAlumnoDocente/EvaluacionDocente_Estudiante.aspx")
end if

%>

<% if (session("cicloIng_Alu") = "2012-I") then %>
    <!-- <script>window.open("FrmPopAviso.aspx", "PopUp Aviso", "menubar=1,resizable=1,width=350,height=250")</script>        -->
<% end if %>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title><%=session("descripcion_apl")%></title>
<link href="private/lytebox.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="JavaScript" src="js/jquery-1.4.2.min.js"></script>

</head>
<frameset framespacing="0" border="0" frameborder="0" rows="118,*">
  <frame name="menusuperior" scrolling="no" noresize target="menusuperior" src="sup.asp">
  </frame>
  <% 'if session("EgresadoAlumni")<> 1 then
           cols = "19%,65%,16%"
       ' else
          '  cols = "15%,58%"
       ' end if
  %>
   	
  <frameset name="Grupo" id="Grupo" cols="<%=cols %>">
  <frame name="menuizq" src="izq.asp" scrolling="no" target="menuizq">
 
    <frame name="contenido" src="<%=Pagina%>" scrolling="yes" target="contenido">
   	
   	<% if session("EgresadoAlumni")<> 1 then
	        response.write "<frame id=""eventos"" name=""eventos"" src=""eventos.asp"" scrolling=""auto"" target=""eventos"">"
	        else
	        response.write "<frame id=""eventos"" name=""eventos"" src="""" scrolling=""auto"" target=""eventos"">"
	   end if
	%>
  </frameset>
    <noframes>
  <body>
  <p>Esta página usa marcos, pero su explorador no los admite.</p>
  </body>
  </noframes>
</frameset>
</html>