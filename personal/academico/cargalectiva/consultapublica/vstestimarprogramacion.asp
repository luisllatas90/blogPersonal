<!--#include file="../../../../funciones.asp"-->
<%
if session("codigo_usu") = "" then
    Response.Redirect("../../../../sinacceso.html")
end if

codigo_cac=request.querystring("codigo_cac")
codigo_pes=request.querystring("codigo_pes")
codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")
modulo=request.QueryString("mod")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"
if codigo_pes="" then codigo_pes="-2"

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	Set rsEscuela=obj.Consultar("EVE_ConsultarCarreraProfesional","FO",modulo,codigo_tfu,codigo_usu)

	if codigo_Pes<>"-2" then
		Set rsCursoPlan= obj.Consultar("ConsultarEstimadoProgramacion","FO",1,codigo_pes,codigo_cac,0)
		
		if Not(rsCursoPlan.BOF and rsCursoPlan.EOF) then
			activo=true
			alto="height=""97%"""
		end if
	end if
    obj.CerrarConexion
Set obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Estimar programaci�n de asignaturas por planes de estudio</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
var nc=""
function BuscarCursos()
{
    location.href = "vstestimarprogramacion.asp?codigo_pes=" + cbocodigo_pes.value + "&mod=<%=modulo%>"
}
	
function MarcarCurso()
{
	var fila=event.srcElement.parentElement
	var condicion=0
	codigo_cur=fila.codigo_cur
	
	if (document.all.chkcondicion.checked==true){
		condicion=1
	}
	if (codigo_cur!=undefined){
		SeleccionarFila()
		//var pagina="../academico/cargalectiva/consultapublica/tblestimarestudiantes.asp?codigo_pes=" + cbocodigo_pes.value + "&codigo_cur=" + codigo_cur + "&condicion=" + condicion + "&mod=<%=modulo%>"
		//var pagina = "../../../rptusat/?/PRIVADOS/ACADEMICO/ACAD_ProyeccionCurso&codigo_pes=" + cbocodigo_pes.value + "&codigo_cur=" + codigo_cur + "&condicion=" + condicion + "&mod=<%=modulo%>"
		var pagina = "../../../reportServer/?/PRIVADOS/ACADEMICO/ACAD_ProyeccionCurso&codigo_pes=" + cbocodigo_pes.value + "&codigo_cur=" + codigo_cur + "&condicion=" + condicion + "&mod=<%=modulo%>"
		nc=fila.nombre_cur
		fradetalle.document.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
		mensajedetalle.style.display="none"
		cmdImprimir.disabled=true
	}
}

function ImprimirLista()
{
	fradetalle.document.title="Estimado de estudiantes para asignatura " + nc
	fradetalle.focus();
	fradetalle.print();
}
</script>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
<tr>
	<td height="3%" colspan="2" class="usattitulo">Estimar programaci�n de 
	asignaturas por planes de estudio</td>
</tr>
<tr>
	<td valign="top" height="3%" width="50%">
	<%call planescuela2("",codigo_pes,rsEscuela)%>
	</td>
	<td valign="top" height="3%" width="50%">
      &nbsp;<img alt="Buscar cursos programados" src="../../../../images/buscar.gif" class="imagen" onclick="BuscarCursos()">
    </td>
	</tr>  
<%if activo=true then%>
<tr>
<td valign="top" height="5%" width="100%" colspan="2" class="rojo">
      <input type="checkbox" name="chkcondicion" value="1">Asumir que todos los estudiante matriculados en el ciclo actual han aprobado todos sus cursos matriculados</td>
	</tr>  
<tr>
<td valign="top" height="44%" width="50%">
<table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="100%">
      <tr class="etabla">
        <td width="5%" height="3%">Ciclo</td>
        <td width="10%" height="3%">C�digo</td>
        <td width="35%" height="3%">Descripcion</td>
        <td width="5%" height="3%">Cr�d.</td>
        <td width="5%" height="3%">TH</td>        
      </tr>
      <tr>
        <td width="100%" colspan="6">
        <div id="listadiv" style="height:100%" class="NoImprimir">
		<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#ccccccc" id="tblcursoprogramado">
		<%	i=0
			ciclo=1
						
			Do while not rsCursoPlan.eof
				i=i+1
				bordeciclo="class=bordeinf"'Agrupar(rsCursoPlan("ciclo_cur"),Ciclo)
		%>
			<tr valign="top" class="piepagina" height="20px" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" class="Sel" Typ="Sel" onclick="MarcarCurso()" codigo_cur="<%=rsCursoPlan("codigo_cur")%>" nombre_cur="<%=rsCursoPlan("nombre_cur")%>">
				<td <%=bordeciclo%> align="center" width="5%"><%=ConvRomano(rsCursoPlan("ciclo_Cur"))%></td>
				<td <%=bordeciclo%> width="10%"><%=rsCursoPlan("identificador_Cur")%></td>
				<td <%=bordeciclo%> width="40%"><%=rsCursoPlan("nombre_Cur")%>
				<%if rsCursoPlan("requisitos_cur")<>"NINGUNO" then%>
				<br><u><em>Requisitos</em></u><br><%=rsCursoPlan("requisitos_cur")%>
				<%end if%>
				</td>
				<td <%=bordeciclo%> align="center" width="5%"><%=rsCursoPlan("creditos_Cur")%></td>
				<td <%=bordeciclo%> align="center" width="5%"><%=rsCursoPlan("totalhoras_cur")%></td>
			</tr>
				<%rsCursoPlan.movenext
			loop
			set rsCursoPlan=nothing
		%>
		</table>
		</div>
	    </td>
      	</tr>
	  <tr bgcolor="#F0F0F0">
      	<td height="5%" width="100%" colspan="6" align="right" class="azul">
		Total de asignaturas: <%=i%>
      	</td>
      </tr>             	
      </table>
      </td>
	<td valign="top" height="44%" width="50%">
		<table style="border-collapse: collapse" class="contornotabla" width="100%" height="100%">
		<tr height="3%">
			<td width="95%" class="etabla">Estudiantes que pueden llevar la asignatura</td>
			<td width="5%" class="etabla">
			<input disabled="true" class="imprimir2" name="cmdImprimir" type="button" value="Imprimir" onclick="ImprimirLista()" >
			</td>
		</tr>
		<tr height="97%">
			<td width="100%" colspan="2">
			<span class="usatsugerencia" id="mensajedetalle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Haga clic en la asignatura para visualizar el estimado de estudiantes que pueden llevar.</span>
			<iframe name="fradetalle" height="100%" width="100%" border="0" frameborder="0" >
            El explorador no admite los marcos flotantes o no est� configurado actualmente para mostrarlos.</iframe>
			</td>
		</tr>
		</table>
	</td>
	</tr>  
<%end if%>
</table>
</body>
</html>