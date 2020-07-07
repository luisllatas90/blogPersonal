<!--#include file="../../../../funciones.asp"-->
<%
Dim resultado

resultado=false
codigo_cpf=request.querystring("codigo_cpf")
codigo_cac=request.querystring("codigo_cac")
nombre_cpf=request.querystring("nombre_cpf")

if codigo_cpf="" then codigo_cpf="-2"
if codigo_cac="" then codigo_cac=session("codigo_cac")

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

<p class="usattitulo">Promedio ponderado por Escuela Profesional y ciclo académico</p>
<table border="0" <%=alto%> cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="25%" height="5%" class="etiqueta">Escuela Profesional</td>
    <td width="65%" height="5%"><%Call escuelaprofesional("actualizarlista('pondescuelaciclo.asp?codigo_cpf=' + this.value)",codigo_cpf,">> Seleccionar Escuela Profesional <<")%></td>
  </tr>
  <tr>
    <td width="25%" height="5%" class="etiqueta">Ciclo Académico</td>
    <td width="65%" height="5%"><%call ciclosAcademicos("",codigo_cac,"","")%></td>
  </tr>
  <tr>
    <td width="25%">&nbsp;</td>
    <td width="65%"><input type="button" value="    Consultar..." name="cmdConsultar" class="usatbuscar" onclick="ConsultarPonderadoEscuela2()">
    <%call botonExportar("../../../","xls","promedioponderado","S","B")%>
    </td>
  </tr>
  <%

   if resultado=true then%>
  <tr>
   <td colspan="2" width="100%" height="80%" valign="top">
	<%
	Set Obj= Server.CreateObject("PryUSAT.clsDatMatricula")
		Set rsAlumnos=Obj.ConsultarMatricula("RS","17",codigo_Cac,0,codigo_cpf)
	Set Obj=nothing
	
	Dim ArrCampos,ArrEncabezados,ArrCeldas

	ArrEncabezados=Array("Código Universitario","Apellidos y Nombres","Ponderado","Nro. Crd.")
	ArrCampos=Array("codigouniver_alu","alumno","ponderado","totalCreditosMatriculados")
	ArrCeldas=Array("15%","55%","10%","20%")
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