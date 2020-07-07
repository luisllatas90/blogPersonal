<!--#include file="../../../funciones.asp"-->
<%
if session("codigo_usu") = "" then
    Response.Redirect("../../../sinacceso.html")
end if

codigo_cpf=request.querystring("codigo_cpf")
codigo_cac=request.querystring("codigo_cac")

if codigo_Cac="" then codigo_cac=session("codigo_cac")

if codigo_cpf="" then codigo_cpf="-2"
if codigo_cpf<>"-2" then
	alto="height=""95%"""
end if

' 20180913 ENevado ---------------------------------------------------------------
codigo_per=request.querystring("id")
if codigo_per="" then codigo_per=session("codigo_usu")
Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion
Set rsEscuela=obj.Consultar("ConsultarCarreraProfesional","FO","PG",codigo_per)
obj.CerrarConexion
Set Obj=nothing
' ---------------------------------------------------------------------------------
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validarhorarios.js"></script>
</head>

<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tr>
    <td width="100%" colspan="4" class="usatencabezadopagina" height="5%">
    <p class="usattitulo">Consulta de Horarios por Escuela Profesional</td>
  </tr>
  <tr>
    <td width="27%" height="5%">Ciclo Acad�mico:</td>
    <td width="73%" colspan="3" height="5%"><%call ciclosAcademicos("actualizarlista('vsthorarioescuela_clonado.asp?codigo_cac='+this.value)",codigo_cac,"","")%></td>
  </tr>
  <tr>
    <td width="27%" height="5%">Escuela Profesional:</td>
    <td width="73%" colspan="3" height="5%">
        <%'call escuelaprofesional("actualizarlista('vsthorarioescuela_clonado.asp?codigo_cpf='+this.value + '&codigo_cac=' + cbocodigo_cac.value)",codigo_cpf,"Seleccionar Escuela Profesional")
            call llenarlista("cbocodigo_cpf","actualizarlista('vsthorarioescuela_clonado.asp?codigo_cpf='+this.value + '&codigo_cac=' + cbocodigo_cac.value)",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccione la Escuela Profesional","","") ' 20180913 ENevado
        %>
    </td>
  </tr>
  <%if codigo_cpf<>"-2" then
  	Set obj= Server.CreateObject("PryUSAT.clsDatCurso")
  		Set rsGrupos=obj.ConsultarCursoProgramado("RS","6",codigo_cpf,codigo_cac,0,0)
		Set rsCiclos=obj.ConsultarCursoProgramado("RS","7",codigo_cpf,codigo_cac,0,0)
	Set obj=nothing
  %>
  <tr>
    <td width="27%" height="5%">Ciclos:</td>
    <td width="16%" height="5%">
    <%'call llenarlista("cbociclos","",rsCiclos,"ciclo_cur","ciclo_cur","","","","")%>
    <SELECT id="cbociclos">
    	<option value="0">TODOS</option>
    	<%for i=1 to 12%>
    	<option value="<%=i%>"><%=ConvRomano(i)%></option>
    	<%next%>
    </SELECT>
    </td>
    <td width="33%" height="5%" align="right">Grupos Horario:</td>
    <td width="24%" height="5%"><%call llenarlista("cbogrupohor_cup","",rsGrupos,"grupohor_cup","grupohor_cup","","","S","")%></td>
  </tr>
  <!---
  <tr>
    <td width="27%" height="5%" align="right">
    <input type="checkbox" name="chkpagina" value="0"></td>
    <td width="73%" colspan="3" height="5%">
    Mostrar asignaturas en una sola tabla Horario y la leyenda de descripci�n</td>
  </tr>
  -->
  <tr>
    <td width="27%" height="5%">&nbsp;</td>
    <td width="73%" colspan="3" height="5%">
    <input type="button" value="  Ver horarios" name="cmdGenerar" class="horario2" onClick="GenerarVistaHorario_clonado('HE')"/>
    <input type="button" value="  Imprimir" name="cmdImprimir" id="cmdImprimir" class="imprimir2" onClick="ImprimirVista('HE')" style="display:none"/> </td>
  </tr>
  <tr>
    <td width="100%" colspan="4" height="85%">
    <iframe name="frahorario" height="100%" width="100%" class="contornotabla" border="0" frameborder="0">
    El explorador no admite los marcos flotantes o no est� configurado actualmente para mostrarlos.</iframe></td>
  </tr>
  <%end if%>
</table>
</body>
</html>