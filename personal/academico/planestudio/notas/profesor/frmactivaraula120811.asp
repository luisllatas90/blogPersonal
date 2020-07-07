<!--#include file="../../../../funciones.asp"-->
<%
codigo_cac=session("codigo_cac")
codigo_per=session("codigo_usu")

if codigo_per<>"" then
	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsCarga=Obj.Consultar("ConsultarCargaAcademica","FO","13",codigo_cac,codigo_per)
	Obj.CerrarConexion
	Set Obj=nothing

%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<title>Carga Acad�mica</title>
<script type="text/javascript" language="javascript">
function ConfirmarCurso()
{
	var mensaje=""
	mensaje="�Est� completamente seguro que desea crear Habilitar el aula virtual para las asignaturas seleccionadas?"

	if (confirm(mensaje)==true){
		frmcurso.cmdGuardar.disabled=true
		mensaje.innerHTML="<b>&nbsp;Espere un momento por favor...</b>"
		frmcurso.submit()
	}
}

function AbrirDocsAnteriores(id)
{
	var fila=event.srcElement.parentElement
	fila=fila.parentElement
	var curso=fila.cells[3].innerText + " (Grupo " + fila.cells[4].innerText + ")"
	
	AbrirPopUp("frmimportardocs.asp?idcursovirtualdestino=" + id + "&curso=" + curso,"500","600","yes","yes","yes")
}

function OcultarPreguntas()
{
	if (divpreguntas.style.display=="none"){
		divpreguntas.style.display=""
	}
	else{
		divpreguntas.style.display="none"
	}
}

</script>
</head>

<body bgcolor="#F0F0F0">
<p><span class="usatTitulo">Habilitar asignaturas para uso del Aula Virtual</span></p>
<%if (rsCarga.BOF and rsCarga.EOF) then
  		response.write "<h5 class=""usatsugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado carga acad�mica registrada para el ciclo acad�mico actual</h5>"
  	else
%>
<form name="frmcurso" method="POST" Action="procesaraulavirtual.asp?accion=agregarcursovirtual&amp;codigo_cac=<%=codigo_cac%>&codigo_per=<%=codigo_per%>&login_per=<%=rsCarga("login_per")%>">
<table cellpadding="3" width="100%" class="contornotabla">
	<tr>
		<td width="25%" class="etiqueta">Ciclo Actual</td>
		<td>: <%=session("descripcion_cac")%></td>
	</tr>
	<tr>
		<td width="25%" class="etiqueta">Profesor</td>
		<td>: <%=session("nombre_usu")%>&nbsp;</td>
	</tr>
</table>
<br>	
<!--<input type="button" class="enviaryrecibir1"  value="        Habilitar y actualizar"  disabled=true onClick="ConfirmarCurso()" name="cmdGuardar" style="width: 170px">-->
<input type="button" class="enviaryrecibir1"  value="        Habilitar y actualizar"  disabled=true onClick="alert('Las aulas ser�n activadas esta semana.')" name="cmdGuardar" style="width: 170px">
<br>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%">
  <tr class="etabla">
    <td width="3%" height="26">&nbsp;</td>
    <td width="5%" height="26">#</td>
    <td width="12%" height="26">C�digo</td>
    <td width="30%" height="26">Descripci�n</td>
    <td width="10%" height="26">GH</td>
    <td width="5%" height="26">Ciclo</td>
    <td width="30%" height="26">Escuela Profesional</td>
    <td width="5%" height="26">Matriculados</td>
    <td width="5%" height="26">Importar Documentos</td>
    <td width="5%" height="26">Aula Virtual</td>
  </tr>
  <%
  	do while not rsCarga.EOF 	
 	i=i+1
  %>
  <tr>
    <td width="3%" align="center" bgcolor="#FFFFFF" height="20">
    <input type="checkbox" onClick="VerificaCheckMarcados(chkcursoshabiles,cmdGuardar)" name="chkcursoshabiles" value="<%=rsCarga("codigo_cup")%>">
    </td>
    <td width="5%" align="center" bgcolor="#FFFFFF" height="20"><%=i%>&nbsp;</td>
    <td width="12%" bgcolor="#FFFFFF" height="20"><%=rsCarga("identificador_Cur")%>&nbsp;</td>
    <td width="30%" bgcolor="#FFFFFF" height="20"><%=rsCarga("nombre_Cur")%>&nbsp;
	<input type="hidden" name="C<%=rsCarga("codigo_cup")%>" id="C<%=rsCarga("codigo_cup")%>" value="<%=rsCarga("nombre_Cur")%>"  />
	</td>
    <td width="10%" align="center" bgcolor="#FFFFFF" height="20"><%=rsCarga("grupoHor_Cup")%>&nbsp;</td>
    <td width="5%" align="center" bgcolor="#FFFFFF" height="20"><%=ConvRomano(rsCarga("ciclo_cur"))%>&nbsp;</td>
    <td width="30%" bgcolor="#FFFFFF" height="20"><%=rsCarga("nombre_cpf")%><span style="font-size: 7pt"> (<%=rsCarga("descripcion_pes")%>)</span>&nbsp;</td>
    <td width="10%" align="center" bgcolor="#FFFFFF" height="20"><%=rsCarga("matriculados")%>&nbsp;</td>
    <td width="10%" align="center" bgcolor="#FFFFFF" height="20">
    <%if rsCarga("idcursovirtual")>0 then%>
	<img class="imagen" alt="Importar documentos anteriores" src="../../../../images/menus/attachfiles_small.gif" width="25" height="25"
	onclick="AbrirDocsAnteriores('<%=rsCarga("idcursovirtual")%>')"
	>
	<%end if%>
	</td>
    <td width="10%" align="center" bgcolor="#FFFFFF" height="20" class="azul">
    <%=iif(rsCarga("idcursovirtual")>0,"Activo","Inactivo")%>
    </td>
  </tr>
  	<%
  	rsCarga.movenext
  loop
  Set rsCarga=nothing%>
</table>
</form>
<span id="mensaje" style="color:#FF0000"></span>
<p class="usattitulousuario" style="cursor:hand" onclick="OcultarPreguntas()">
    Las aulas virtuales ser�n activadas esta semana.<br /><br />
Preguntas frecuentes</p>
<div id="divpreguntas">
&nbsp;<p class="usatEtiqOblig">1. �Qu� debo hacer para agrupar asignaturas en 
	el aula virtual?
	</p>
<p>Solicitar por email a evaluaci�n y 
registros, el agrupamiento de asignaturas indicando las asignaturas que se 
agrupar�n y el motivo.</p>
<p class="usatEtiqOblig">2. �C�mo copio documentos de asignaturas 
	anteriores a una asignatura en el ciclo actual?
	</p>
<p>Primero, debe <strong>habilitar el 
	aula virtual</strong> para la asignatura correspondiente, luego debe hacer clic en el 
	�cono
<img src="../../../../images/menus/attachfiles_small.gif" width="25" height="25"> 
	para indicar de qu� asignatura se copiar�n los documentos..</p>
<p class="usatEtiqOblig">3. �Qu� debo hacer si no tengo asignada mi carga acad�mica para el ciclo actual?
	</p>
<p>Solicitar por email a <strong>Evaluaci�n y 
Registros</strong>, el registro de la carga acad�mica en el Sistema.</p>
</div>
<%end if%>
</body>
</html>
<%end if%>