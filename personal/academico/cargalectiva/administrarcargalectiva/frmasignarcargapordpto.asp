<!--#include file="../../../../funciones.asp"-->
<%
codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
curso=request.querystring("curso")
codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")

if curso="" then curso="%"
if codigo_cac="" then codigo_cac=session("codigo_cac")

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCiclo=obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
		if codigo_tfu=1 then
			'tipo="S"
		    Set rsEscuela=obj.Consultar("ConsultarDepartamentoAcademico","FO","TO",0)
			tiposel="Seleccione el Dpto. Académico"
		else
			Set rsEscuela=obj.Consultar("ConsultarCentroCosto","FO","AC",codigo_usu)
			if Not(rsEscuela.BOF and rsEscuela.EOF) then
				codigo_cpf=rsEscuela("codigo_dac")
			end if
		end if
		if codigo_cpf<>"-2" and codigo_cpf<>"" then
			curso=ReemplazarTildes(curso)
			Set rsCursos=Obj.Consultar("ConsultarCargaAcademicaDpto","FO","2",codigo_cac,codigo_cpf,curso)
			
			if Not(rscursos.BOF and rsCursos.EOF) then
				HayReg=true
				alto="height=""93%"""
			end if
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
<script language="JavaScript" src="../private/validarcargaacademica.js"></script>
</head>
<body>

<p class="usatTitulo">Asignación de Carga Académica por Dpto. Académico</p>
<table style="border-collapse: collapse" width="100%" <%=alto%>>
	<tr>
		<td height="5%" style="width: 10%">Ciclo:</td>
		<td height="5%" style="width: 15%">
		<%call llenarlista("cbocodigo_cac","",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>			
		</td>
		<td height="5%" style="width: 20%" align="right">Dpto. Académico:</td>
		<td height="5%" style="width: 55%">
		<%call llenarlista("cbocodigo_cpf","",rsEscuela,"codigo_dac","nombre_dac",codigo_cpf,tiposel,tipo,"")%>		
		</td>
		<td height="5%" style="width: 5%" align="right">
		<img src="../../../../images/menus/buscar_small12.gif" alt="Abrir formulario de búsqueda" onClick="AbrirBusqueda('frmasignarcargapordpto.asp')" class="imagen">
		</td>
	</tr>
	<%if HayReg=true then%>
	<tr height="35%" valign="top">
		<td width="100%" class="contornotabla" colspan="5">
			<table height="100%" width="100%" cellpadding="2" cellspacing="0" style="border-collapse: collapse">
			<tr class="etabla">
				<td width="5%" height="3%">#</td>	
				<td width="5%" height="3%">Ciclo</td>
				<td width="10%" height="3%">Código</td>
				<td width="35%" height="3%">Descripción</td>
				<td width="5%" height="3%">Crd.</td>
				<td width="5%" height="3%">TH</td>
				<td width="5%" height="3%">GH</td>
				<td width="25%" height="3%">Profesor</td>
				<td width="10%" height="3%">Matric.</td>				
			</tr>
			<tr>
				<td width="100%" colspan="13">
				<div id="listadiv" style="height:100%" class="NoImprimir">
				<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#ccccccc" id="tblcursosprogramados">
					<%
					codigo=0
					i=0
					Do while not rsCursos.EOF
						i=i+1
						'bordeciclo=Agrupar(rsCursos("identificador_cur"),codigo)
						curso=rsCursos("nombre_Cur")
						bordeciclo=""
						if int(codigo_cpf)<>int(rsCursos("codigo_dac")) then
							bordeciclo="class='cursoU'"
							curso=curso & " / " & rsCursos("abreviatura_dac")
						end if
					%>
					<tr height="20px" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" class="Sel" Typ="Sel" onclick="MostrarAsigProfesor('<%=rsCursos("codigo_cup")%>')">
						<td <%=bordeciclo%> align="center" width="5%"><%=i%></td>
						<td <%=bordeciclo%> align="center" width="5%"><%=ConvRomano(rsCursos("ciclo_Cur"))%></td>
						<td <%=bordeciclo%> width="10%"><%=rsCursos("identificador_Cur")%></td>
						<td <%=bordeciclo%> width="35%"><%=curso%></td>
						<td <%=bordeciclo%> align="center" width="5%"><%=rsCursos("creditos_cur")%></td>
						<td <%=bordeciclo%> align="center" width="5%"><%=rsCursos("totalhoras_Cur")%></td>
						<td <%=bordeciclo%> width="5%"><%=rsCursos("grupohor_Cup")%></td>
						<td <%=bordeciclo%> width="25%"><%=rsCursos("profesor")%></td>
						<td <%=bordeciclo%> width="25%"><%=rsCursos("matriculados")%></td>
					</tr>
					<%
					rscursos.movenext
					Loop
					Set rscursos=nothing
					%>
				</table>
				</div>
				</td>
			</tr>
			</table>
			</td>
	</tr>
	<tr>
	<td height="60%" colspan="5" width="100%">
	<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
				<tr>
					<td class="pestanabloqueada" width="100%" height="5%">&nbsp;<b>Profesor(es) responsables de la asignatura</b></td>
				</tr>
				<tr  height="55%" valign="top">
					<td colspan="5" width="100%"  class="pestanarevez">
					<iframe name="fraHorario" id="fraHorario" height="100%" width="100%" border="0" frameborder="0">El 
					explorador no admite los marcos flotantes o no está configurado actualmente 
					para mostrarlos.
					</iframe>
					</td>
				</tr>
	</table>
	</td>
	</tr>
	<%else%>
		<tr><td colspan="5" width="100%" class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado cursos programados solicitados al Departamento Académico seleccionado</td></tr>
	<%end if%>	
	</table>
</body>

</html>
