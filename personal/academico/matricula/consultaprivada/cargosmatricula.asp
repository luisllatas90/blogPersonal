<!--#include file="../../../../funciones.asp"-->
<%
codigo_cpf=request.querystring("codigo_cpf")
codigo_cac=request.querystring("codigo_cac")
codigo_sco=request.querystring("codigo_sco")
nombre_cpf=request.querystring("nombre_cpf")
estado_deu=request.querystring("estado_deu")

resultado=request.querystring("resultado")

if codigo_cpf="" then codigo_cpf="-2"
if codigo_cac="" then codigo_cac="24"

	Set Obj=Server.CreateObject("PryUSAT.clsDatCarreraProfesional")
		Set rsEscuela=Obj.ConsultarCarreraProfesional("RS","RE","")
	Set Obj=nothing
	
	Set Obj=Server.CreateObject("PryUSAT.clsDatCicloAcademico")
		Set rsCiclo=Obj.ConsultarCicloAcademico("RS","TO","")
	Set Obj=nothing
	
	if codigo_cac<>"" then
		Set Obj=Server.CreateObject("PryUSAT.clsDatMatricula")
			Set rsServicio=Obj.ConsultarMatricula("RS","21",codigo_cac,0,0)
		Set Obj=nothing	
	end if

if resultado="S" then
	alto="height=""92%"" "
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Reporte de Ponderado acumulado</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../private/validarreportes.js"></script>
</head>
<body>

<p class="usattitulo">Consulta de Alumnos según Servicio Abonado</p>
<table border="0" <%=alto%> cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="25%" height="5%">Ciclo</td>
    <td width="65%" height="5%" colspan="3"><%call llenarlista("cbociclo","ConsultarAlumnoCargos()",rsCiclo,"codigo_cac","descripcion_Cac",codigo_cac,"","","")%></td>
  </tr>
  <tr>
    <td width="25%" height="5%">Escuela Profesional</td>
    <td width="65%" height="5%" colspan="3"><%call llenarlista("cboescuela","",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccionar Escuela Profesional","S","")%></td>
  </tr>
  <tr>
    <td width="25%" height="5%">Servicio</td>
    <td width="22%" height="5%"><%call llenarlista("cbocodigo_sco","",rsServicio,"codigo_sco","descripcion_sco",codigo_sco,"Seleccionar el servicio","","")%></td>
   </td>
    <td width="22%" height="5%"><span lang="es">Estado del servicio</span></td>
    <td width="21%" height="5%"><select size="1" name="cboestado_deu">
    <option value="T" <%=SeleccionarItem("cbo",estado_deu,"T")%>>--Todos--</option>
    <option value="P" <%=SeleccionarItem("cbo",estado_deu,"P")%>>Pendiente</option>
    <option value="C" <%=SeleccionarItem("cbo",estado_deu,"C")%>>Cancelado</option>
    <option value="O" <%=SeleccionarItem("cbo",estado_deu,"O")%>>Convenio</option>
    </select></td>
  </tr>
  <tr>
    <td width="25%">&nbsp;</td>
    <td width="65%" colspan="3"><input onclick="ConsultarAlumnoCargos()" type="button" value="    Consultar..." name="cmdConsultar" class="usatbuscar">
    <%call botonExportar("../../../","xls","cargoalumnos","S","B")%>
    </td>
  </tr>
  <%
   if resultado="S" then%>
  <tr>
   <td colspan="4" width="100%" height="80%" valign="top">
	<%
	Set Obj= Server.CreateObject("PryUSAT.clsDatCurso")
		Set rsAlumnos=Obj.ConsultarCursoProgramado("RS","11",codigo_sco,codigo_cac,codigo_cpf,estado_deu)
	Set Obj=nothing
	
	Dim ArrCampos,ArrEncabezados,ArrCeldas

	ArrEncabezados=Array("Ciclo de Ingreso","Código Universitario","Apellidos y Nombres","Escuela Profesional","Cargo","Saldo","Estado")
	ArrCampos=Array("cicloIng_alu","codigouniver_alu","alumno","nombre_cpf","montoTotal_deu","Saldo_deu","estado_deu")
	ArrCeldas=Array("10%","15%","25%","20%","10%","10%","10%")
	'pagina="MostrarHistorial(this)"
	
	titulorpte="Cargos de Alumnos " & nombre_cpf

	call CrearRpteTabla(ArrEncabezados,rsAlumnos,"",ArrCampos,ArrCeldas,"S","V",pagina,"S","","")
	call ValoresExportacion(titulorpte,ArrEncabezados,rsAlumnos,Arrcampos,ArrCeldas)
	
	Set rsAlumnos=nothing
	%>	
   </td>
  </tr>
  <%end if%>
</table>

</body>

</html>