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
	alto="height=""98%"""
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de docentes con Carga Acad�mica</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tr>
    <td width="97%" height="5%" colspan="2" class="usatTitulo">Carga Acad�mica por Semestre Acad�mico</td>
  <tr>
    <td width="22%" height="5%">Semestre Acad�mico</td>
    <td width="75%" height="5%">
    <%call ciclosAcademicos("actualizarlista('rpteprofesorcursos.asp?modo=R&codigo_cac=' + this.value + '&descripcion_cac=' + this.options[this.selectedIndex].text)",codigo_cac,"","")%>
    </td>
    <%if modo="R" then%>
    <tr><td colspan="2" width="100%" height="90%">
<form name="frmlistacursos" method="POST" action="administrarcargalectiva/procesar.asp?accion=agruparcursosprogramados&amp;codigo_cac=<%=codigo_cac%>">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
  <tr>
    <td width="100%" height="100%">
    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="100%" id="tblcursoprogramado">
      <tr class="etabla">
        <td width="5%" height="3%">Ciclo</td>
        <td width="5%" height="3%">Tipo</td>
        <td width="10%" height="3%">C�digo</td>
        <td width="26%" height="3%">Nombre del Curso</td>
        <td width="5%" height="3%">Crd.</td>
        <td width="5%" height="3%">TH</td>
        <td width="5%" height="3%">GH</td>
        <td width="20%" height="3%">Carrera Profesional</td>
        <td width="20%" height="3%">Docente</td>
      </tr>
      <tr>
        <td width="100%" colspan="9" height="100%">
        <div id="listadiv" style="height:100%" class="NoImprimir">
		<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#ccccccc">
		<%
		Set Obj=Server.CreateObject("PryUSAT.clsDatCurso")
			Set rsCursos=Obj.ConsultarCursoProgramado("RS","13",codigo_cac,0,0,0)
		Set Obj=nothing
	
			i=0:n=0:p=0
			Ciclo=1
			Do while not rsCursos.eof
				i=i+1
				bordeciclo=Agrupar(rsCursos("ciclo_cur"),Ciclo)				
		%>
			<tr class="piepagina" height="20px" id="fila<%=i%>" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
			<td <%=bordeciclo%> align="center" width="5%"><%=ConvRomano(rsCursos("ciclo_Cur"))%>&nbsp;</td>
			<td <%=bordeciclo%> width="5%"><%=rsCursos("tipo_Cur")%>&nbsp;</td>
			<td <%=bordeciclo%> width="12%"><%=rsCursos("identificador_Cur")%>&nbsp;</td>
			<td <%=bordeciclo%> width="24%"><%=rsCursos("nombre_Cur")%>&nbsp;</td>
			<td <%=bordeciclo%> align="center" width="5%"><%=rsCursos("creditos_cur")%>&nbsp;</td>
			<td <%=bordeciclo%> align="center" width="5%"><%=rsCursos("totalhoras_cur")%>&nbsp;</td>
			<td <%=bordeciclo%> align="center" width="8%"><%=rsCursos("grupohor_cup")%>&nbsp;</td>
			<td <%=bordeciclo%> width="20%"><%=rsCursos("nombre_cpf")%>&nbsp;</td>
			<td <%=bordeciclo%> width="20%"><%=ConvertirTitulo(rsCursos("profesor_cup"))%>&nbsp;</td>
			</tr>
				<%rsCursos.movenext
			loop
			set rsCursos=nothing
		%>
		</table>
		</div>
	    </td>
      </tr>
      <tr>
    	<td width="100%" colspan="9" height="5%" bgcolor="#E6E6FA" align="right"><span class="azul">&nbsp;&nbsp;&nbsp;&nbsp; Cursos Programados: <%=i%></span>&nbsp;
    	</td>
	  </tr>
      </table>
  </td>
  </tr>
</table>
</form>
</td></tr>
  <%end if%>
  </table>
</body>
</html>