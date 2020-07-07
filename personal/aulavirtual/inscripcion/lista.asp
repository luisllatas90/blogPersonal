<!--#include file="../../funciones.asp"-->
<%
codigo_dac=request.querystring("codigo_dac")

	Set Obj= Server.CreateObject("PryUSAT.clsDatDepartamentoAcademico")
		Set rsDpto=Obj.ConsultarDepartamentoAcademico("RS","TO",0)
	Set Obj=nothing
	
if codigo_dac="" then codigo_dac="-2"
if codigo_dac<>"-2" then
	alto="height=""98%"""
end if
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de Inscritos al Curso</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
</head>

<body>

<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tr>
    <td width="40%" valign="top" height="10%" class="usatTitulo">Lista de Inscritos al Curso</td>
    <td width="40%" valign="top" height="10%">
    <%call llenarlista("cbxdepartamento","actualizarlista('lista.asp?codigo_dac=' + this.value)",rsDpto,"codigo_dac","nombre_dac",codigo_dac,"Seleccione el Departamento Académico","S","")%>
    </td>
    <td width="20%" align="right" valign="top" height="10%"><%call botonExportar("../../","xls","listainscritos","S","B")%></td>
  </tr>
  <%if codigo_dac<>"-2" then%>
  <tr>
    <td width="100%" colspan="3" height="90%" valign="top">
<%
	Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio
	
		Set Obj= Server.CreateObject("PryUSAT.clsFuncionesADO")
			Set rsInscritos=Obj.ProcedimientoConsulta("ConsultarInscripcionCursoVirtual","FO","2",codigo_dac,0,0)
		Set Obj=nothing
	
		ArrEncabezados=Array("Fecha","Apellidos y Nombres","Asignatura","Escuela Profesional de Asignatura","Dpto. Acad. de asignatura","Eje","Motivo","Tiempo","Obs.")
		ArrCampos=Array("fechareg","docente","nombre_cur","nombre_cpf","nombre_dac","eje","tipomotivo","tiempocurso","obs")
		ArrCeldas=Array("10%","20%","15%","15%","15%","5%","5%","5%","10%")

		call CrearRpteTabla(ArrEncabezados,rsInscritos,"",ArrCampos,ArrCeldas,"S","","","S",null,null)
		call ValoresExportacion(titulorpte,ArrEncabezados,rsInscritos,Arrcampos,ArrCeldas)
%>
	</td>
  </tr>
   	<tr>
   		<td colspan="3" width="100%" height="3%"><b>MOTIVO POR EL CUAL APLICA LA INVESTIGACIÓN EN LA ASIGNATURA REGISTRADA</b><br> 1:La asignatura, está en relación al PDP | 2:Asignatura asignada por el Dpto adscrito | 3:Porque guarda relación con sus investigaciones | 4:Otro motivo </td>
	</tr>
  <%end if%>	
</table>
</body>
</html>