<!--#include file="../../funciones.asp"-->
<%
if session("codigo_usu")="" then response.redirect "../../tiempofinalizado.asp"

accion="agregarinscripcion"
modalidad="P"

	Set Obj= Server.CreateObject("PryUSAT.clsDatCurso")
		Set rsCurso=Obj.ConsultarCurso("RS","LC",0,0)
	Set Obj=nothing
	
	Set Obj= Server.CreateObject("PryUSAT.clsDatCarreraProfesional")
		Set rsEscuela=Obj.ConsultarCarreraProfesional("RS","MA",0)
	Set Obj=nothing
	
	Set Obj= Server.CreateObject("PryUSAT.clsDatDepartamentoAcademico")
		Set rsDpto=Obj.ConsultarDepartamentoAcademico("RS","TO",0)
	Set Obj=nothing
	
	Set Obj= Server.CreateObject("PryUSAT.clsFuncionesADO")
		Set rsInscripcion=Obj.ProcedimientoConsulta("ConsultarInscripcionCursoVirtual","FO","1",session("codigo_usu"),0,0)
	Set Obj=nothing
	
	If Not (rsInscripcion.BOF and rsInscripcion.EOF) then
		accion="modificarinscripcion"
		codigo_cpf=rsInscripcion("codigo_cpf")
		codigo_dac=rsInscripcion("codigo_dac")
		nombre_cur=rsInscripcion("nombre_cur")
		eje=rsInscripcion("eje")
		tiempo=rsInscripcion("tiempo")
		tipotiempo=rsInscripcion("tipotiempo")
		tipomotivo=rsInscripcion("tipomotivo")
		obs=rsInscripcion("obs")
		modalidad=rsInscripcion("modalidad")
	end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Inscripción de curso</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validarinscripcion.js"></script>
<style>
<!--
.etiquetafrm { color: #FFFFFF; background-color: #26758C }
-->
</style>
</head>
<body topmargin="0" leftmargin="0">
<form name="frminscripcioncursovirtual" method="post" onSubmit="return validarinscripcion()" action="procesar.asp?accion=<%=accion%>">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" bgcolor="#EBE1BF" background="../../../images/sup.jpg" height="103">
  <tr>
    <td width="20%" height="82">&nbsp;</td>
    <td width="80%" colspan="2" height="82" class="usatTituloAplicacion" valign="top"><br><%=ImprimeTitulo%>&nbsp;</td>
  </tr>
  <tr>
    <td width="70%" class="franja" height="21" colspan="2" style="text-align: left">
    &nbsp; USUARIO: <%=session("nombre_usu")%></td>
    <td width="30%" class="franja" height="21" ><%=formatdatetime(now,1)%></td>
  </tr>
</table>

<p class="usatTitulo">&nbsp; Inscripción al curso Diseño de asignaturas basadas en la investigación</p>
<table align="center" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="90%" id="tblinscripcion" class="contornotabla">
  <tr>
    <td width="100%" height="13" colspan="2" class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Por favor complete el formulario de inscripción. Recuerde 
que todos los campos son obligatorios</td>
  </tr>
  <tr>
    <td width="35%" height="13" class="etiquetafrm">Apellidos y Nombres</td>
    <td width="65%" height="13"><%=session("nombre_usu")%></td>
  </tr>
  <tr>
    <td width="35%" height="22" class="etiquetafrm">Elija la asignatura en la que desarrollará su Investigación</td>
    <td width="65%" height="22"><%call llenarlista("cbxasignatura","",rsCurso,"nombre_cur","nombre_cur",nombre_cur,"Seleccionar la asignatura","","")%></td>
  </tr>
  <tr>
    <td width="35%" height="32" class="etiquetafrm">Escuela Profesional de la asignatura elegida</td>
    <td width="65%" height="32"><%call llenarlista("cbxescuela","",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccionar la Escuela Profesional","","")%></td>
  </tr>
  <tr>
    <td width="35%" height="32" class="etiquetafrm">Elija el Departamento Académico al que está adscrito</td>
    <td width="65%" height="32"><%call llenarlista("cbxdepartamento","",rsDpto,"codigo_dac","nombre_dac",codigo_dac,"","","")%></td>
  </tr>
  <tr>
    <td width="35%" height="19" class="etiquetafrm">Seleccione el Eje transversal al que pertenece la asignatura elegida</td>
    <td width="65%" height="19"><select size="1" name="cbxeje">
    <option value="Cultura" <%=SeleccionarItem("cbo",eje,"Cultura")%>>Cultura</option>
    <option value="Persona" <%=SeleccionarItem("cbo",eje,"Persona")%>>Persona</option>
    <option value="Especialidad" <%=SeleccionarItem("cbo",eje,"Especialidad")%>>Especialidad</option>
    </select></td>
  </tr>
  <tr>
    <td width="35%" height="22" class="etiquetafrm">Especifique el tiempo que viene desarrollando la asignatura</td>
    <td width="65%" height="22">
    <input type="text" name="txttiempo" size="3" class="cajas" onkeypress="validarnumero()" value="<%=tiempo%>">
    <select size="1" name="cbxtipotiempo">
    <option value="semestre" <%=SeleccionarItem("cbo",tipotiempo,"semestre")%>>Semestre(s)</option>
    </select></td>
  </tr>
  <tr>
    <td width="35%" height="22" class="etiquetafrm" valign="top">¿Cuál es el motivo por la cual ha elegido la asignatura?</td>
    <td width="65%" height="22" valign="top">
    <input <%=SeleccionarItem("opt",tipomotivo,1)%> type="radio" value="1" checked name="opttipomotivo" onclick="if (this.checked==true){txtobs.disabled=true}">Por tener relación con mi PDP<br>
    <input <%=SeleccionarItem("opt",tipomotivo,2)%> type="radio" value="2" name="opttipomotivo" onclick="if (this.checked==true){txtobs.disabled=true}">Por ser asignada por mi Departamento adscrito<br>
    <input <%=SeleccionarItem("opt",tipomotivo,3)%> type="radio" value="3" name="opttipomotivo" onclick="if (this.checked==true){txtobs.disabled=true}">Porque guarda relación con mis investigaciones<br>
    <input <%=SeleccionarItem("opt",tipomotivo,4)%> type="radio" value="4" name="opttipomotivo" onclick="if (this.checked==true){txtobs.disabled=false}">Otros motivos
    <input type="text" value="<%=obs%>" name="txtobs" size="40" class="cajas" disabled=true></td>
  </tr>
<tr>
    <td width="35%" height="22" class="etiquetafrm" valign="top">¿Marque la modalidad que desea llevar el Curso?</td>
    <td width="65%" height="22" valign="top"><input type="radio" <%=SeleccionarItem("chk",modalidad,"P")%> value="P" name="chkmodalidad">En forma Presencial <input <%=SeleccionarItem("chk",modalidad,"V")%> type="radio" value="V" name="chkmodalidad">En forma virtual</td>
</tr>
  <tr>
    <td width="35%" height="22" class="etiquetafrm">&nbsp;</td>
    <td width="65%" height="22" valign="top">
    <input type="submit" value="Matricular" name="cmdGuardar" class="guardar">
    <input type="button" value="Cancelar" name="cmdCancelar" class="salir"></td>
  </tr>
  </table>
</form>
</body>
</html>