<!--#include file="../../../funciones.asp"-->
<%
Dim resultado
resultado=false
codigo_cac=request.querystring("codigo_cac")

if codigo_cac="" then codigo_cac=session("codigo_cac")

'if (trim(codigo_cpf)<>"-2" and trim(codigo_cac)<>"-2" and trim(cicloIng_alu)<>"-2") then
	resultado=true
	alto="height=""92%"" "
'end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Reporte de Ponderado acumulado</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validarcursomatriculado.js"></script>
</head>
<body>
<p class="usattitulo">Lista de postulantes por ciclo académico</p>
<table border="0" <%=alto%> cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="15%" height="5%">Ciclo Académico</td>
    <td width="20%" height="5%"><%call ciclosAcademicos("",codigo_cac,"","")%></td>
    <td width="65%" height="5%">
		<input onclick="actualizarlista('rptepostulantes.asp?resultado=true&codigo_cac='+this.value)" type="button" value="    Consultar..." name="cmdConsultar" class="usatbuscar">
		<%call botonExportar("../../../","xls","postulantes","S","B")%>
    </td>
  </tr>
  <%if resultado=true then%>
  <tr>
   <td colspan="3" width="100%" height="80%" valign="top">
	<%
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsAlumnos=Obj.Consultar("ConsultarAlumno","FO","13",codigo_cac)
		Obj.CerrarConexion
	Set Obj=nothing
	
	Dim ArrCampos,ArrEncabezados,ArrCeldas

	ArrEncabezados=Array("Código Universitario","Apellidos y Nombres","Fecha Nac.","Sexo","Modalidad","Dirección","Teléf. Casa","Teléf. Movil","Tipo Colegio","Colegio","Año Egres.","Escuela Profesional","email")
	ArrCampos=Array("codigouniver_alu","alumno","fechanacimiento_alu","sexo_alu","nombre_min","direccion_dal","telefonocasa_dal","telefonomovil_dal","tipoColegio_Dal","nombre_col","añoEgresoSec_Dal","nombre_cpf","email_alu")
	ArrCeldas=Array("15%","30%","10%","5%","10%","20%","5%","5%","10%","15%","5%","15%","10%")
	
	titulorpte="Lista de postulantes. ciclo académico " & codigo_cac

	call CrearRpteTabla(ArrEncabezados,rsAlumnos,"",ArrCampos,ArrCeldas,"S","V",pagina,"S","","")
	call ValoresExportacion(titulorpte,ArrEncabezados,rsAlumnos,Arrcampos,ArrCeldas)
	%>	
   </td>
  </tr>
  <%end if%>
</table>
</body>
</html>