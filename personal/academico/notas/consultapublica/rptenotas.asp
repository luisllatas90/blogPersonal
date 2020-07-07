<!--#include file="../../../../funciones.asp"-->
<%
codigo_tfu=session("codigo_tfu")
codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
codigo_acc=session("codigo_usu")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"
if codigo_cpf="" then codigo_cpf="-2"

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		if codigo_tfu=1 OR codigo_tfu=7 OR codigo_tfu=16 or codigo_tfu=18 then
			Set rsEscuela= obj.Consultar("ConsultarCarreraProfesional","ST","MA",0)
		else
			Set rsEscuela= obj.Consultar("ConsultarAcceso","ST","ESC","Silabo",codigo_acc)
		end if
	obj.CerrarConexion
Set obj=nothing

if codigo_cac<>"-2" and codigo_cpf<>"-2" then
	activo=true
	alto="height=""99%"""
end if
%>
<html>
<head>
<title>Administrar sílabos</title>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252"/>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../silabos/private/validarsilabos.js"></script>
</head>
<style>
<!--
body         { font-size: 7px }
-->
</style>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tr class="usattitulo">
    <td width="100%" colspan="2" height="5%">Rendimiento Académico por Escuela Profesional y Asignaturas</td>
  </tr>
  <tr>
    <td width="27%" height="3%">Ciclo Académico</td>
    <td width="73%" height="3%"><%call ciclosAcademicos("actualizarlista('rptenotas.asp?codigo_cac='+ this.value)",codigo_cac,"","")%></td>
  </tr>
  <tr>
    <td width="27%" height="3%">Escuela Profesional</td>
    <td width="73%" height="3%">
    <%call llenarlista("cbocodigo_cpf","actualizarlista('rptenotas.asp?codigo_cac='+ document.all.cbocodigo_cac.value + '&codigo_cpf=' + this.value)",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"-Seleccione la Escuela Profesional-","","")%>
	</td>
  </tr>
  <%if activo=true then
    Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
    	Obj.AbrirConexion
			Set rsCursos=Obj.Consultar("ConsultarRendimientoAcademico","FO","C",codigo_cac,codigo_cpf)
		Obj.CerrarConexion
	Set obj=nothing
  %>
  <tr>
    <td width="100%" colspan="2" height="50%" valign="top">
    <%		
			Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio,pagina

			ArrEncabezados=Array("Descripción de Asignatura","GH","Profesor","Aprobados","Desaprobados","Matriculados","Retirados")
			ArrCampos=Array("nombre_cur","grupohor_cup","docente","total_aprobados","total_desaprobados","matriculados","retirados")
			ArrCeldas=Array("15%","5%","15%","5%","5%","5%","5%")
			ArrCamposEnvio=Array("refcodigo_cup")

			pagina="lstalumnosmatriculados.asp?codigo_cac=" & codigo_cac

			call CrearRpteTabla(ArrEncabezados,rsCursos,"",ArrCampos,ArrCeldas,"S","I",pagina,"S",ArrCamposEnvio,"")
	
			if not(rsCursos.BOF and rsCursos.EOF) then
	%>
    </td>
  </tr>
  <tr>
    <td width="100%" colspan="2" height="40%">
		<span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Elija el curso para visualizar los alumnos matriculados</span>
        <iframe id="fradetalle" name="fradetalle" height="100%" width="100%" border="0" frameborder="0" scrolling="yes" class="contornotabla">
        El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe>
    </td>
  </tr>
  		<%end if
  	end if%>
  </table>
</body>
</html>