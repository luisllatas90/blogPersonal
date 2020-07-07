<!--#include file="../../../funciones.asp"-->
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de estudiantes que actualizaron su Ficha Personal</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
</head>

<body>

<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1" height="98%">
  <tr>
    <td width="100%" class="usattitulo" height="5%" valign="top">Lista de estudiantes que actualizaron su Ficha Personal&nbsp;</td>
  </tr>
  <tr>
    <td width="100%" height="95%" valign="top">
    <%
	Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		set rsAlumnos=Obj.Consultar("ConsultarAlumno","FO","LR",0)
	Obj.CerrarConexion
	Set obj=nothing

	Dim ArrCampos,ArrEncabezados,ArrCeldas

	ArrEncabezados=Array("Ciclo de Ingreso","Código Universitario","Apellidos y Nombres","Escuela Profesional")
	ArrCampos=Array("cicloIng_alu","codigouniver_alu","alumno","nombre_cpf")
	ArrCeldas=Array("10%","15%","40%","30%")
	
	call CrearRpteTabla(ArrEncabezados,rsAlumnos,"",ArrCampos,ArrCeldas,"S","V",pagina,"S","","")
	%>

    </td>
  </tr>
</table>
</body>
</html>