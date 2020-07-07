<!--#include file="../../../../funciones.asp"-->
<%
if session("codigo_usu") = "" then
    Response.Redirect("../../../../sinacceso.html")
end if

codigo_cac=request.querystring("codigo_cac")
codigo_tam=request.querystring("codigo_tam")
codigo_ube=request.querystring("codigo_ube")
codigo_tfu= session("codigo_tfu")
codigo_usu= session("codigo_usu")

if codigo_cac="" then codigo_cac=session("codigo_cac")

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCiclo=obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
		set rsTipoAmbiente = obj.Consultar("ACAD_ListaTipoAmbiente","FO",0,"")
		set rsUbicacion = obj.Consultar("ACAD_ListaUbicacionEdificio","FO",0,"")
		
		if codigo_tfu=1 OR codigo_tfu=7 OR codigo_tfu=16 or codigo_tfu=18 or codigo_tfu=85 or codigo_tfu=181 then
			'tipo="S"
		    Set rsEscuela=obj.Consultar("ConsultarCarreraProfesional","FO","MA",0)
		else
			Set rsEscuela=obj.Consultar("consultaracceso","FO","ESC","Silabo",codigo_usu)
		end if		
	obj.CerrarConexion
Set Obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Registro de horarios por Ambiente</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" />
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="javascript">
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
    var pagina = "../academico/horarios/consultapublica/tbltodohorario.asp?codigo_cac=" + cbocodigo_cac.value + "&ciclo_cur=" + cbociclos.value + "&codigo_cpf=" + cbocodigo_cpf.value + "&codigo_tam=" + cbocodigo_tam.value + "&codigo_ube=" + cbocodigo_ube.value + "&mat=" + chkMatricula.checked
	var paginanueva = "tbltodohorario.asp?codigo_cac=" + cbocodigo_cac.value + "&ciclo_cur=" + cbociclos.value + "&codigo_cpf=" + cbocodigo_cpf.value + "&codigo_tam=" + cbocodigo_tam.value + "&codigo_ube=" + cbocodigo_ube.value + "&mat=" + chkMatricula.checked
	//alert(pagina);
    //var pagina = "../academico/horarios/consultapublica/tbltodohorario_original.asp?codigo_cac=" + cbocodigo_cac.value + "&ciclo_cur=" + cbociclos.value + "&codigo_cpf=" + cbocodigo_cpf.value + "&codigo_tam=" + cbocodigo_tam.value + "&codigo_ube=" + cbocodigo_ube.value
	//fraHorario.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina + "&x=1"
	fraHorario.location.href = paginanueva + "&x=1"
}
function GenerarVistaExcel() {
    var pagina = "../academico/horarios/consultapublica/tbltodohorarioExcel.asp?codigo_cac=" + cbocodigo_cac.value + "&ciclo_cur=" + cbociclos.value + "&codigo_cpf=" + cbocodigo_cpf.value + "&codigo_tam=" + cbocodigo_tam.value + "&codigo_ube=" + cbocodigo_ube.value    
    window.open("../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina)
    //fraHorario.location.href = "../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
}
</script>
</head>
<body bgcolor="#F0F0F0">
<p class="usatTitulo">Consulta general de horarios</p>
<table width="100%" height="92%">
	<tr>
		<td height="5%" style="width: 15%">Ciclo Académico:</td>
		<td height="5%" style="width: 10%">
		<%call llenarlista("cbocodigo_cac","",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>
		</td>
		<td height="5%" style="width: 15%" align="right">Escuela Profesional:</td>
		<td height="5%" style="width: 30%">
		<%call llenarlista("cbocodigo_cpf","",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"","S","")%>
		</td>
		<td width="15%" height="5%">Ciclo:
		<SELECT name="cbociclos">
			<option value="-1">TODOS</option>
			<%for i=1 to 12%>
			<option value="<%=i%>"><%=ConvRomano(i)%></option>
			<%next%>
		</SELECT>
		</td>
		<td height="5%" style="width: 10%" align="right">
		<input style="width:70px" name="cmdBuscar" type="button" value="   Generar" class="horario2" onclick="GenerarVista()" />&nbsp;
		</td>		
	</tr>
	<tr>
		    <td>Tipo Ambiente:&nbsp; </td>
		    <td>
                <%call llenarlista("cbocodigo_tam","",rsTipoAmbiente,"codigo_tam","descripcion_Tam",codigo_tam,"","S","")%>
		    </td>
		    
		    <td align="right">Tipo Ubicación:</td>
		    <td>
		        <%call llenarlista("cbocodigo_ube","",rsUbicacion,"codigo_ube","descripcion_ube",codigo_ube,"","S","")%>
		    </td>
		    <td>
		        <input type="checkbox" name="chkMatricula" value="1" /> Incluir matriculados
		    </td>
		    <td align="right">
		        <input style="width:70px" name="cmdBuscar" type="button" value="   Exportar" class="excel2" onclick="GenerarVistaExcel()" />&nbsp;
		    </td>
		</tr>
	<tr height="95%" valign="top">
		<td colspan="6" width="100%" class="contornotabla">
		<iframe name="fraHorario" id="fraHorario" height="100%" width="100%" border="0" frameborder="0">El 
		explorador no admite los marcos flotantes o no está configurado actualmente 
		para mostrarlos.
		</iframe>
		</td>
	</tr>
	</table>
</body>

</html>
