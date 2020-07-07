<!--#include file="../../../funciones.asp"-->
<%
Dim codigo_cpf,codigo_pes

codigo_cpf=request.querystring("codigo_cpf")
codigo_pes=request.querystring("codigo_pes")
nombre_cpf=request.querystring("nombre_cpf")
'descripcion_pes=request.querystring("descripcion_pes")

if codigo_cpf="" then codigo_cpf="-2"
if codigo_pes="" then codigo_pes="-2"
codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
if codigo_tfu=1 or codigo_tfu=7 or codigo_tfu=16 then
	Set rsEscuela= obj.Consultar("ConsultarCarreraProfesional","FO","MA",0)
else
	Set rsEscuela= obj.Consultar("consultaracceso","FO","ESC","Silabo",codigo_usu)
end if
    obj.CerrarConexion
Set obj=nothing

if codigo_cpf<>"-2" and codigo_pes<>"-2" then
	alto="height=""98%"""
end if

'nombre_cpf=session("nombre_cpf")
'codigo_cpf=session("codigo_cpf")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Seleccione el curso que desea agregar a la matrícula</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validarplan.js"></script>
</head>
<body>
<table <%=alto%> cellpadding="2" cellspacing="0" style="border-collapse: collapse; border: 0px solid #C0C0C0; " bordercolor="#111111" width="100%">
  <tr>
    <td width="100%" height="3%" colspan="3" class="usatTitulo">Administrar Planes de Estudio</td>
    </tr>
  <tr>
    <td width="15%" height="3%">Escuela Profesional</td>
    <td width="85%" height="3%" colspan="2"><%call llenarlista("cboEscuela","ActualizarlistaPlan('C')",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccionar la Escuela Profesional","","")%>
    </td>
  </tr>
  <%
  if codigo_cpf<>"-2" then
	Set objPlan=Server.CreateObject("PryUSAT.clsDatPlanestudio")
		Set rsPlan= objPlan.ConsultarPlanEstudio("RS","AC",codigo_cpf,"")
  	Set objPlan=nothing
  %>
  <tr>
    <td width="15%" height="3%">Plan de Estudio</td>
    <td width="80%" height="3%"><%call llenarlista("cboPlan","ActualizarlistaPlan('P')",rsPlan,"codigo_pes","descripcion_pes",codigo_pes,"Seleccione el Plan de Estudio","","")%></td>
    <td width="5%" height="3%" style="cursor:hand"><!--<img src="../../../images/nuevo.gif" alt="Agregar nuevo Plan" onClick="AbrirPlan('A')"> <img src="../../../images/editar.gif" alt="Modificar Plan seleccionado" onClick="AbrirPlan('M')">--></td>
  </tr>
  <%end if
  if codigo_pes<>"-2" then%>
  <tr>
    <td width="100%" height="3%" bgcolor="#FFFF66" colspan="3">
    <b>HT</b>=Horas teoría / <b>HP</b>=Horas de práctica / 
<b>HL</b>=Horas de laboratorio / <b>HA</b>=Horas de asesoría / <b>TH</b>=Total horas</font>
    </td>
  </tr>
  <tr>
    <td width="15%" height="3%">&nbsp;</td>
    <td width="85%" height="3%" colspan="2" align="right">    
    <input type="button" value="Guardar" name="cmdGuardar" class="guardar2" id="cmdGuardar" onclick="Procesarcursos('G',fralista.document.all.frmlistacursos,'<%=codigo_pes%>')">    
    <input type="button" value="    Habilitar vigencia" name="cmdHabilitar" class="marcado2" id="cmdHabilitar" style="width: 110" onclick="Procesarcursos('A',fralista.document.all.frmlistacursos,'<%=codigo_pes%>')">
    <input type="button" value="Quitar vigencia"  name="cmdQuitar" class="regresar2" id="cmdQuitar" style="width: 110" onclick="Procesarcursos('D',fralista.document.all.frmlistacursos,'<%=codigo_pes%>')">
	<!--
    <input type="button" value="   Agregar Curso"  name="cmdAgregar" class="agregar2" id="cmdAgregarCurso" style="width: 100" onclick="AccionCursoPlan('A')">
    <input type="button" value="   Quitar Curso"  name="cmdQuitarCurso" class="eliminar2" id="cmdQuitarCurso" onclick="AccionCursoPlan('Q')">
	-->
	</td>
  </tr>
  <tr>
    <td width="100%" height="85%" colspan="3" valign="top">
    <iframe name="fralista" height="100%" width="100%" src="lstcursosplan.asp?codigo_pes=<%=codigo_pes%>" scrolling="no">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe></td>
  </tr>
  </table>
  <%
     end if
  %>
</body>
</html>