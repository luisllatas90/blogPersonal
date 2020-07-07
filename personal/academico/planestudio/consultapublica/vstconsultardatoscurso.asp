<!--#include file="../../../../funciones.asp"-->
<%
if session("codigo_usu") = "" then
    Response.Redirect("../../../../sinacceso.html")
end if

codigo_pes=request.querystring("codigo_pes")
codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")
modulo=request.QueryString("mod")

if codigo_cac="" then codigo_cac=session("codigo_cac")

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCiclo=obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
		Set rsEscuela=obj.Consultar("EVE_ConsultarCarreraProfesional","FO",modulo,codigo_tfu,codigo_usu)
		
		if codigo_pes<>"-2" and codigo_pes<>"" then
			Set rsCursos=Obj.Consultar("ConsultarCursoPlan","FO","6",codigo_pes,0,0)
			
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
<meta http-equiv="Content-Language" content="es" >
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<title>Registro de horarios por Ambiente</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" >
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/tooltip.js"></script>
<script language="JavaScript">
var codigo_cur=0

function ConsultarDetalle(modo)
{	
	if (modo=="DE"){
		fila=event.srcElement.parentElement
		codigo_cur=fila.codigo_cur	
	
		SeleccionarFila()
		ResaltarPestana2('0','','')
		modo="E"
	}
	
		fraDetalle.location.href="lstdetallecursos.asp?modo=" + modo + "&codigo_cur=" + codigo_cur + "&codigo_pes=" + cbocodigo_pes.value
}

function ConsultarCursos()
{
	if (document.all.listadiv!=undefined){
		listadiv.innerHTML="<h5 align=center class=rojo>Procesando b�squeda, espere un momento...</h5>"
}
	location.href="vstconsultardatoscurso.asp?codigo_pes=" + cbocodigo_pes.value + "&mod=" + hddModulo.value
}

</script>
</head>
<body>
<p class="usatTitulo">Consultar equivalencias y requisitos</p>
<table style="border-collapse: collapse" width="100%" <%=alto%>>
	<tr>
		<td height="5%" style="width: 20%" align="right">Escuela Profesional:</td>
		<td height="5%" style="width: 45%"><%call planescuela2("",codigo_pes,rsEscuela)%></td>
		<td height="5%" style="width: 5%">
		<img src="../../../../images/menus/buscar_small12.gif" alt="Abrir formulario de b�squeda" onclick="ConsultarCursos()" class="imagen">
		</td>
	</tr>
	<%if HayReg=true then%>
	<tr height="35%" valign="top">
		<td width="100%" class="contornotabla" colspan="3">
			<table height="100%" width="100%" cellpadding="2" cellspacing="0" style="border-collapse: collapse">
			<tr class="etabla">
				<td width="5%" height="3%">#</td>	
				<td width="5%" height="3%">Ciclo</td>
				<td width="10%" height="3%">C�digo</td>
				<td width="35%" height="3%">Descripci�n</td>
				<td width="5%" height="3%">Crd.</td>
				<td width="5%" height="3%">TH</td>
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
					<tr height="20px" class="piepagina" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" class="Sel" Typ="Sel" onclick="ConsultarDetalle('DE')" codigo_cur="<%=rsCursos("codigo_cur")%>">
						<td <%=bordeciclo%> align="center" width="5%"><%=i%></td>
						<td <%=bordeciclo%> align="center" width="5%"><%=ConvRomano(rsCursos("ciclo_Cur"))%></td>
						<td <%=bordeciclo%> width="10%"><%=rsCursos("identificador_Cur")%></td>
						<td <%=bordeciclo%> width="35%"><%=replace(rsCursos("nombre_Cur"),"<br>","")%></td>
						<td <%=bordeciclo%> align="center" width="5%"><%=rsCursos("creditos_cur")%></td>
						<td <%=bordeciclo%> align="center" width="5%" style="color:red"><%=rsCursos("totalhoras_Cur")%></td>
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
	<td height="60%" colspan="3" width="100%">
	<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
				<tr>
					<td class="pestanaresaltada" id="tab" align="center" width="20%" height="5%" onClick="ResaltarPestana2('0','','');ConsultarDetalle('E')">
					Equi<span class="style1">valencias</span></td>
					<td width="1%" height="5%" class="bordeinf">&nbsp;</td>
					<td class="pestanabloqueada" id="tab" align="center" width="20%" height="5%" onClick="ResaltarPestana2('1','','');ConsultarDetalle('R')">
					Requisitos</td>
					<td width="65%" height="5%" class="bordeinf">&nbsp;</td>
				</tr>
				<tr  height="55%" valign="top">
					<td colspan="5" width="100%"  class="pestanarevez">
					<iframe name="fraDetalle" id="fraDetalle" height="100%" width="100%" border="0" frameborder="0">El 
					explorador no admite los marcos flotantes o no est� configurado actualmente 
					para mostrarlos.
					</iframe>
					</td>
				</tr>
	</table>
	</td>
	</tr>
	<%end if%>	
	</table>
	
    <p>
        <input id="hddModulo" type="hidden" value="<%=request.QueryString("mod") %>" />
    </p>
	
</body>
</html>