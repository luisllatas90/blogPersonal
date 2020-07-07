<!--#include file="../../../../funciones.asp"-->



<%
if(session("codigo_usu") = "") then
    Response.Redirect("../../../../sinacceso.html")
end if

codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
curso=request.querystring("curso")
codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")
modulo=request.querystring("mod")

if curso="" then curso="%"
if codigo_cac="" then codigo_cac=session("codigo_cac")

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCiclo=obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
		Set rsEscuela=obj.Consultar("EVE_ConsultarCarreraProfesional","FO",modulo,codigo_tfu,codigo_usu)
		
		if codigo_cpf<>"-2" and codigo_cpf<>"" then
			curso=ReemplazarTildes(curso)
			'Set rsCursos=Obj.Consultar("ConsultarHorarios","FO","24",codigo_cac,codigo_cpf,curso)
			Set rsCursos=Obj.Consultar("ACAD_ConsultarHorarios","FO",codigo_cac,codigo_cpf,curso)
			
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

<p class="usatTitulo">Asignaci�n de Carga Acad�mica y horarios</p>
<table style="border-collapse: collapse" width="100%" <%=alto%>>
	<tr>
		<td height="5%" style="width: 15%">Semestre Acad�mico:</td>
		<td height="5%" style="width: 15%">
		<%call llenarlista("cbocodigo_cac","",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>			
		</td>
		<td height="5%" style="width: 20%" align="right">Carrera Profesional:</td>
		<td height="5%" style="width: 45%">
		<%call llenarlista("cbocodigo_cpf","",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccione la Carrera Profesional",tipo,"")%>		
		</td>
		<td height="5%" style="width: 5%" align="right">
		<img src="../../../../images/menus/buscar_small12.gif" alt="Abrir formulario de b�squeda" onClick="AbrirBusqueda('<%=modulo%>')" class="imagen">
		</td>
	</tr>
	<%if HayReg=true then%>
	<tr height="35%" valign="top">
		<td width="100%" class="contornotabla" colspan="5">
			<table height="100%" width="100%" cellpadding="2" cellspacing="0" style="border-collapse: collapse">
			<tr class="etabla">
				<td width="1%" height="1%">#</td>	
				<td width="5%" height="3%">Ciclo</td>
				<td width="10%" height="3%">C�digo</td>
				<td width="27%" height="1%">Descripci�n</td>
				<td width="5%" height="3%">Crd.</td>
				<td width="3%" height="3%">TH</td>
				<td width="3%" height="3%">GH</td>
				<td width="25%" height="3%">Docente</td>
				<td  align="left" width="10%" height="3%">Matric.</td>
				<td width="5%" height="3%">HM</td>
				<td align="left" width="5%" height="3%">&nbsp; DF</td>
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
						bordeciclo=Agrupar(rsCursos("identificador_cur"),codigo)
					%>
					<tr height="20px" class="piepagina" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" class="Sel" Typ="Sel" onclick="ConsultarHorarios('C','<%=rsCursos("codigo_cup")%>')">
						<td <%=bordeciclo%> align="center" width="1%"><%=i%></td>
						<td <%=bordeciclo%> align="center" width="5%"><%=ConvRomano(rsCursos("ciclo_Cur"))%></td>
						<td <%=bordeciclo%> width="10%"><%=rsCursos("identificador_Cur")%></td>
						<td <%=bordeciclo%> width="28%"><%=replace(rsCursos("nombre_Cur"),"<br>","")%></td>
						<td <%=bordeciclo%> align="center" width="5%"><%=rsCursos("creditos_cur")%></td>
						<td <%=bordeciclo%> align="center" width="3%" style="color:red"><%=rsCursos("totalhoras_Cur")%></td>
						<td <%=bordeciclo%> width="5%"><%=rsCursos("grupohor_Cup")%></td>
						<td <%=bordeciclo%> width="25%"><%=rsCursos("profesor")%></td>
						<td <%=bordeciclo%> width="10%"><%=rsCursos("matriculados")%></td>
						<td <%=bordeciclo%> width="3%"><%=rsCursos("HorMarcada")%></td>    
						<td <%=bordeciclo%> width="3%"><%=rsCursos("HorFalta")%></td>
						
						
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
					<td class="pestanaresaltada" id="tab" align="center" width="20%" height="5%" onClick="ResaltarPestana2('0','','');ConsultarHorarios('P')">Profesor(es)</td>
					<td width="1%" height="5%" class="bordeinf">&nbsp;</td>
					<td class="pestanabloqueada" id="tab" align="center" width="20%" height="5%" onClick="ResaltarPestana2('1','','');ConsultarHorarios('H')">Horario(s)</td>
					<td width="65%" height="5%" class="bordeinf">&nbsp;</td>
				</tr>
				<tr  height="55%" valign="top">
					<td colspan="5" width="100%"  class="pestanarevez">
					<iframe name="fraHorario" id="fraHorario" height="100%" width="100%" border="0" frameborder="0">
                    El 
					explorador no admite los marcos flotantes o no est� configurado actualmente 
					para mostrarlos.</iframe>
					</td>
				</tr>
	</table>
	</td>
	</tr>
	<%end if%>	
	</table>
</body>

</html>