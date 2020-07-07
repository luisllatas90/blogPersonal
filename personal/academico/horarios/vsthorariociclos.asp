<!--#include file="../../../funciones.asp"-->
<%
if session("codigo_usu") = "" then
    Response.Redirect("../../../sinacceso.html")
end if

codigo_cac=request.querystring("codigo_cac")
codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")

if codigo_cac="" then codigo_cac=session("codigo_cac")

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCiclo=obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
		'if codigo_tfu=1 OR codigo_tfu=7 OR codigo_tfu=16 or codigo_tfu=18 then
		'tipo="S"
		'Set rsEscuela=obj.Consultar("ConsultarCarreraProfesional","FO","MA",0)
		Set rsEscuela=obj.Consultar("ConsultarCarreraProfesional","FO","PG",request.QueryString("id")) '20180912 Enevado
		'else
		'	Set rsEscuela=obj.Consultar("consultaracceso","FO","ESC","Silabo",codigo_usu)
		'end if		
	obj.CerrarConexion
Set Obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Registro de horarios por Ambiente</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="javascript">
function ConsultarHorarios()
{
	if (cbocodigo_cpf.value!="-2"){
	   	showModalDialog("frmfiltrohorario.asp?codigo_cpf=" + cbocodigo_cpf.value,window,"dialogWidth:400px;dialogHeight:300px;status:no;help:no;center:yes");
	}
}
function TituloRpte(tipo)
{
	var str=""
	if (tipo=="HE"){ //Horario por escuela
		str="HORARIOS: " + cbocodigo_cpf.options[cbocodigo_cpf.selectedIndex].text
		str+=" (" + cbocodigo_cac.options[cbocodigo_cac.selectedIndex].text + ")"
	}
	window.fraHorario.document.title=str
}

function ImprimirVista(tipo)
{
	TituloRpte(tipo)
	window.fraHorario.focus()
	window.fraHorario.print()	
}

function GenerarVista()
{
	fraHorario.innerHTML="Generando vista horario<br>Por favor espere un momento..."
	fraHorario.location.href="tblhorariociclos.asp?modo=" + modo + "&codigo_cac=" + cbocodigo_cac.value + "&codigo_pes=" + codigo_pes + "&ciclo_cur=" + ciclo_cur + "&descripcion_pes=" + descripcion_pes
}
</script>
</head>
<body bgcolor="#F0F0F0">

<p class="usatTitulo">Consulta de horarios por ciclo de asignaturas</p>
<table width="100%" height="92%">
	<tr>
		<td height="5%" style="width: 15%">Ciclo Acad�mico:</td>
		<td height="5%" style="width: 10%">
		<%call llenarlista("cbocodigo_cac","",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>
		</td>
		<td height="5%" style="width: 15%" align="right">Escuela Profesional:</td>
		<td height="5%" style="width: 35%">
		<%call llenarlista("cbocodigo_cpf","",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccione la Escuela Profesional","","")%>
		</td>
		<td height="5%" style="width: 30%" align="right">
		<input style="width:70px" name="cmdBuscar" type="button" value="   Generar" class="horario2" onclick="ConsultarHorarios()" />
		<input style="width:70px" name="cmdImprimir" type="button" value="  Imprimir" class="imprimir2" onclick="ImprimirVista('HE')" />
		</td>
	</tr>
	<tr height="95%" valign="top">
		<td colspan="5" width="100%" class="contornotabla">
		<iframe name="fraHorario" id="fraHorario" height="100%" width="100%" border="0" frameborder="0">El 
		explorador no admite los marcos flotantes o no est� configurado actualmente 
		para mostrarlos.
		</iframe>
		</td>
	</tr>
	</table>
</body>

</html>
