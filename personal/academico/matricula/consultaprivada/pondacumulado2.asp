<!--#include file="../../../../funciones.asp"-->
<%
Dim resultado

resultado=false
codigo_cpf=request.querystring("codigo_cpf")
codigo_cac=request.querystring("codigo_cac")
cicloIng_Alu=request.querystring("cicloIng_Alu")
nombre_cpf=request.querystring("nombre_cpf")

if codigo_cpf="" then codigo_cpf="-2"
if codigo_cac="" then codigo_cac="-2"
if cicloIng_Alu="" then cicloIng_Alu="-2"

	Set Obj=Server.CreateObject("PryUSAT.clsDatCarreraProfesional")
		Set rsEscuela=Obj.ConsultarCarreraProfesional("RS","RE","")
	Set Obj=nothing

if codigo_cpf<>"-2" then
	Set Obj=Server.CreateObject("PryUSAT.clsDatCicloAcademico")
		Set rsCiclo=Obj.ConsultarCicloAcademico("RS","TO","")
		Set rsCicloIngreso=Obj.ConsultarCicloAcademico("RS","CIN",codigo_cpf)
	Set Obj=nothing
end if

if (trim(codigo_cpf)<>"-2" and trim(codigo_cac)<>"-2" and trim(cicloIng_alu)<>"-2") then
	resultado=true
	alto="height=""92%"" "
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Reporte de Ponderado acumulado</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../private/validarcursomatriculado.js"></script>
</head>
<body>

<p class="usattitulo">Ponderado acumulado</p>
<table border="0" <%=alto%> cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="25%" height="5%">Escuela Profesional</td>
    <td width="65%" height="5%"><%call llenarlista("cboescuela","actualizarlista('pondacumulado2.asp?codigo_cpf=' + this.value)",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccionar Escuela Profesional","","")%></td>
  </tr>
  <%if codigo_cpf<>"-2" then%>
  <tr>
    <td width="25%" height="5%">Ciclo Ingreso del Alumno</td>
    <td width="65%" height="5%"><%call llenarlista("cbocicloingreso","",rsCicloIngreso,"cicloIng_Alu","cicloIng_Alu",cicloIng_Alu,"Seleccionar Ciclo de Ingreso","S","")%></td>
  </tr>
  <tr>
    <td width="25%" height="5%">Incluir sólo los matriculados en el semestre</td>
    <td width="65%" height="5%"><%call llenarlista("cbociclo","",rsCiclo,"codigo_cac","descripcion_Cac",codigo_cac,"Seleccionar el ciclo académico","","")%></td>
  </tr>
  <tr>
    <td width="25%">&nbsp;</td>
    <td width="65%"><input onclick="ConsultarPonderadoAcumulado()" type="button" value="    Consultar..." name="cmdConsultar" class="usatbuscar">
    <%call botonExportar("../../../../","xls","promedioponderado","S","B")%>
    </td>
  </tr>
  <%end if

   if resultado=true then%>
  <tr>
   <td colspan="2" width="100%" height="80%" valign="top">
	<%
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsAlumnos=Obj.Consultar("TC","FO",codigo_cpf,cicloIng_Alu,codigo_Cac)
		Obj.CerrarConexion
	Set Obj=nothing
	
	Dim ArrCampos,ArrEncabezados,ArrCeldas

	ArrEncabezados=Array("Ciclo de Ingreso","Código Universitario","Apellidos y Nombres","Ponderado","Nro. Crd.")
	ArrCampos=Array("cicloIng_alu","codigouniver_alu","alumno","ponderado","totalCreditosMatriculados")
	ArrCeldas=Array("10%","15%","55%","10%","20%")
	pagina="MostrarHistorial(this)"
	
	titulorpte="Promedio Ponderado" & nombre_cpf

	call CrearRpteTabla(ArrEncabezados,rsAlumnos,"",ArrCampos,ArrCeldas,"S","V",pagina,"S","","")
	call ValoresExportacion(titulorpte,ArrEncabezados,rsAlumnos,Arrcampos,ArrCeldas)
	%>	
   </td>
  </tr>
  <%if Not(rsAlumnos.BOF and rsAlumnos.EOF) then%>
  <tr>
  <td colspan="2" width="100%" class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Haga clic en el alumno para visualizar su historial de notas</td></tr>
  <%end if
  end if%>
</table>

</body>

</html>