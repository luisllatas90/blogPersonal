<!--#include file="../../../../funciones.asp"-->
<%
codigo_cac=request.querystring("codigo_cac")
descripcion_cac=request.querystring("descripcion_cac")
estado_mat=request.querystring("estado_mat")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if descripcion_cac="" then descripcion_cac=session("descripcion_cac")

if estado_mat="" then estado_mat="N"
if codigo_cac="" then codigo_cac="-2"
%>
<html>
<head>
<title></title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
</head>
<body>
<table width="100%" height="98%" border="0" style="border-collapse: collapse" bordercolor="#111111" cellspacing="0" cellpadding="2">
   <tr><td colspan="2" width="100%" height="3%">
   		<table cellspacing="0" cellpadding="0" width="100%" border="0" style="border-collapse: collapse" bordercolor="#111111"><tr>
   		<td width="80%" height="3%" class="usattitulo">Matriculados por Cursos 
        Complementarios</td>
      	<td width="20%" height="3%" align="right">&nbsp;<%call botonExportar("../../../../","xls","rptematriculadoscurso","S","B")%></td>
      	</tr></table></td>
    </tr>
      <tr>
        <td width="20%" height="3%" class="etiqueta">Estado de Matrícula</td>
      	<td width="60%" height="3%">
        <select name="cboestado_mat" id="cboestado_mat" onChange="actualizarlista('consultarAluMatriXGrupoHorTodas.asp?codigo_cac=' + cbocodigo_cac.value + '&estado_mat=' + this.value + '&descripcion_cac=' + this.options[this.selectedIndex].text)" style="width: 100%">
			<option value="N" <%=SeleccionarItem("cbo",estado_mat,"N")%>>Matrículas realizadas</option>
			<option value="P" <%=SeleccionarItem("cbo",estado_mat,"P")%>>Pre-matrículas realizadas</option>
			</select>
      	</td>
    </tr>
      <tr>
        <td width="20%" height="3%" class="etiqueta">Ciclo Académico</td>
      	<td width="60%" height="3%"><%call ciclosAcademicos("actualizarlista('consultarAluMatriXGrupoHorTodas.asp?codigo_cac=' + this.value + '&estado_mat=' + cboestado_mat.value + '&descripcion_cac=' + this.options[this.selectedIndex].text)",codigo_cac,"S","")%></td>
    </tr>
    <%if codigo_cac<>"-2" then%>
    <tr><td width="40%" height="40%" colspan="2" valign="top">
        <%
        	Set obj=Server.CreateObject("PryUSAT.clsDatMatricula")
				Set rsMatriculados=obj.consultarNroAlumporAsig("RS","CO",codigo_cac,estado_mat)
			Set Obj=nothing

			Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio
	
		total=rsMatriculados.recordcount
		titulo=iif(estado_mat="P","Pre-Matriculas realizadas","Matrículas realizadas")
	
		ArrEncabezados=Array("Código","Asignatura","Grupo Horario",titulo & "(" & total & ")")
		ArrCampos=Array("identificador_cur","nombre_cur","grupohor_cup","matriculados")
		ArrCeldas=Array("10%","60%","10%","30%")
		ArrCamposEnvio=Array("codigo_cup","identificador_cur","nombre_cur","grupohor_cup")
		pagina="lstalumnosmatriculados.asp?rpte=mat-cur-gh&codigo_cac=" & codigo_cac & "&estado_mat=" & estado_mat
		titulorpte="Reporte de Matrículas por Curso Programado " & descripcion_cac
		call CrearRpteTabla(ArrEncabezados,rsMatriculados,"",ArrCampos,ArrCeldas,"S","I",pagina,"S",ArrCamposEnvio,null)
		call ValoresExportacion(titulorpte,ArrEncabezados,rsMatriculados,Arrcampos,ArrCeldas)
		%>
	</td></tr>
	<%
	if not(rsMatriculados.BOF and rsMatriculados.EOF) then
	%>
    <tr>
  	<td width="100%" colspan="2" height="3%" class="contornotabla" bgcolor="#C7E0CE">
  	<table cellSpacing="0" cellPadding="0" width="100%" border="0" style="border-collapse: collapse" bordercolor="#111111" height="100%">
  	<tr class="usatEtiqOblig"><td width="65%">Listado de Alumnos Matriculados</td>
    <td width="25%" align="right" valign="top"><%call botonExportar2("../../../../","xls","rptealumnosmatriculados","S","I")%>Descarga Listado...&nbsp;</td>
  	</tr>
  	</table>
  	</td>
  </tr>
    <tr><td width="100%" height="48%" colspan="2" valign="top">
	    <span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Elija el curso para visualizar los alumnos matriculados</span>
        <iframe id="fradetalle" name="fradetalle" height="100%" width="100%" border="0" frameborder="0" src="lstalumnosmatriculados.asp" scrolling="no">
        El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe>
	</td></tr>
	<%end if
	end if%>
	</table>
</body>
</html>