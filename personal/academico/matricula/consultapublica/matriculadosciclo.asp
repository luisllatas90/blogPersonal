<!--#include file="../../../../funciones.asp"-->
<%
Dim resultado

resultado=false
codigo_cac=request.querystring("codigo_cac")
nombre_cac=request.querystring("nombre_cac")

if codigo_cac="" then codigo_cac="-2"

if (trim(codigo_cac)<>"-2") then
	resultado=true
	alto="height=""92%"" "
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de alumnos matriculados</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
</head>
<body>

<p class="usattitulo">Reporte de total de matriculados</p>
<table border="0" <%=alto%> cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="25%" height="5%">Ciclo Académico</td>
    <td width="65%" height="5%"><%call ciclosAcademicos("",codigo_cac,"","")%></td>
  </tr>
  <tr>
    <td width="25%">&nbsp;</td>
    <td width="65%"><input type="button" value="    Consultar..." name="cmdConsultar" class="usatbuscar" onclick="actualizarlista('rptetotalmatriculados.asp?resultado=true&codigo_cac=' + cbocodigo_cac.value + '&nombre_cac=' + cbocodigo_cac.options[cbocodigo_cac.selectedIndex].text)">
    <%call botonExportar("../../../../","xls","totalmatriculados","S","B")%>
    </td>
  </tr>
  <%
   if resultado=true then%>
  <tr>
   <td colspan="2" width="100%" height="90%" valign="top">
	<%
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsAlumnos=Obj.Consultar("ConsultarMatricula","FO",27,codigo_cac,0,0)
		Obj.CerrarConexion
	Set Obj=nothing
	
	Dim ArrCampos,ArrEncabezados,ArrCeldas

	ArrEncabezados=Array("Ciclo de Ingreso","Código Universitario","Apellidos y Nombres","Escuela Profesional")
	ArrCampos=Array("cicloIng_alu","codigouniver_alu","nombresapellidos","nombre_cpf")
	ArrCeldas=Array("10%","15%","40%","30%")
	
	titulorpte="Lista total de matriculados " & nombre_cac

	call CrearRpteTabla(ArrEncabezados,rsAlumnos,"",ArrCampos,ArrCeldas,"S","V",pagina,"S","","")
	call ValoresExportacion(titulorpte,ArrEncabezados,rsAlumnos,Arrcampos,ArrCeldas)
	%>	
   </td>
  </tr>
  <%end if%>
</table>

</body>

</html>