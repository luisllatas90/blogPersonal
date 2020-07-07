<!--#include file="../../../funciones.asp"-->
<%
if session("codigo_usu") = "" then
    Response.Redirect("../../../sinacceso.html")
end if

modo=request.querystring("modo")
codigo_cac=request.querystring("codigo_cac")
descripcion_cac=request.querystring("descripcion_cac")
if codigo_cac="" then
	codigo_cac=session("codigo_cac")
	modo="R"
end if

if modo="R" then
	alto="height=""90%"""
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de docentes con Carga Académica</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
</head>

<body>

<p class="usatTitulo">Lista de docentes con Carga Académica</p>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tr>
    <td width="22%" height="10%">Semestre Académico</td>
    <td width="75%" height="10%">
    <%call ciclosAcademicos("actualizarlista('rpteprofesorescarga.asp?modo=R&codigo_cac=' + this.value + '&descripcion_cac=' + this.options[this.selectedIndex].text)",codigo_cac,"","")%>
    </td>
    <td width="15%"><%call botonExportar("../../../","xls","listaprofesores","S","B")%></td>
    <%if modo="R" then%>
    <tr><td colspan="3" width="100%" height="90%">
<%
	Set Obj=Server.CreateObject("PryUSAT.clsDatDocente")
		Set rsDocente=Obj.ConsultarDocente("RS","CL",codigo_cac,param2)
	Set Obj=nothing

	Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio
	
	ArrEncabezados=Array("Docente")
	ArrCampos=Array("docente")
	ArrCeldas=Array("90%")

	titulorpte="Lista de profesores con Carga Académica " & descripcion_cac

	call CrearRpteTabla(ArrEncabezados,rsDocente,"",ArrCampos,ArrCeldas,"S","I",pagina,"S",ArrCamposEnvio,null)
	call ValoresExportacion(titulorpte,ArrEncabezados,rsDocente,Arrcampos,ArrCeldas)

%>
  </td></tr>
  <%end if%>
  </table>
</body>
</html>