<!--#include file="../../../../funciones.asp"-->
<%
codigo_cac=request.querystring("codigo_cac")
codigo_pes=request.querystring("codigo_pes")
codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"
if codigo_pes="" then codigo_pes="-2"

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	
	Set rsCiclo= Obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
	
	if codigo_tfu=1 or codigo_tfu=7 or codigo_tfu=16 then
		Set rsEscuela= obj.Consultar("ConsultarCarreraProfesional","FO","MA",0)
	else
		Set rsEscuela= obj.Consultar("consultaracceso","FO","ESC","Silabo",codigo_usu)
	end if

	if codigo_cac<>"-2" and codigo_Pes<>"-2" then
		Set rsCursoPlan= Obj.Consultar("ConsultarCursoProgramado","FO",1,codigo_pes,codigo_cac,"","")

		if Not(rsCursoPlan.BOF and rsCursoPlan.EOF) then
			activo=true
			alto="height=""98%"""
		end if
	end if
    obj.CerrarConexion
Set obj=nothing
%>
<html>
<head>
<title>Programación de Cursos</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../private/validarprogramacion.js"></script>
</head>
<body oncontextmenu="return false">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
<tr>
	<td height="3%" colspan="5" class="usattitulo">Programación de asignaturas por Planes de Estudio</td>
</tr>
<tr>
	<td height="3%" style="width: 20%">Ciclo Académico:</td>
	<td height="3%" style="width: 15%">
	<%call llenarlista("cbocodigo_cac","",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>			
	</td>
	<td height="3%" style="width: 20%" align="right">Plan de Estudio:</td>
	<td height="3%" style="width: 65%">
	<%call planescuela2("OcultarCursos()",codigo_pes,rsEscuela)%>
	</td>
	<td height="3%" style="width: 10%" align="right">
    <img alt="Buscar cursos programados" src="../../../../images/menus/buscar_small.gif" class="imagen" onclick="BuscarProgramacion('N')">
	</td>
</tr>
<%if activo=true then%>
  <tr height="95%" valign="top">
    <td width="100%" colspan="5" id="trCursos">
    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="100%">
      <tr class="etabla">
        <td width="5%" height="3%">Ciclo</td>
        <td width="10%" height="3%">Código</td>
        <td width="30%" height="3%">Descripcion</td>
        <td width="5%" height="3%">Créd.</td>
        <td width="4%" height="3%">HT</td>
        <td width="4%" height="3%">HP</td>
        <td width="4%" height="3%">HL</td>
        <td width="4%" height="3%">HA</td>
        <td width="4%" height="3%">TH</td>
        <td width="10%" height="3%">Estado</td>
      </tr>
      <tr>
        <td width="100%" colspan="10">
        <div id="listadiv" style="height:100%" class="NoImprimir">
		<form name="frmPlanCurso" method="post" action="frmconfirmarprogramacion.asp?codigoElegido_pes=<%=codigo_pes%>&codigo_cac=<%=codigo_cac%>">
		<input name="arrCursosMarcados" type="hidden" value="0" />
		<input name="arrPlanesMarcados" type="hidden" value="0" />
				
		<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#ccccccc" id="tblcursoprogramado">
		<%	i=0
			ciclo=1
			Do while not rsCursoPlan.eof
				i=i+1
				if rsCursoPlan("grupos")=0 then
					estado=false
					n=n+1
				else
					estado=true
					p=p+1
				end if
				bordeciclo=Agrupar(rsCursoPlan("ciclo_cur"),Ciclo)
		%>
			<tr height="20px" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" class="Sel" Typ="Sel" onclick="MarcarCurso()" codigo_cur="<%=rsCursoPlan("codigo_cur")%>" codigo_pes="<%=rsCursoPlan("codigo_pes")%>">
			<td <%=bordeciclo%> align="center" width="5%"><%=ConvRomano(rsCursoPlan("ciclo_Cur"))%>&nbsp;</td>
			<td <%=bordeciclo%> width="10%"><%=rsCursoPlan("identificador_Cur")%></td>
			<td <%=bordeciclo%> width="35%"><%=rsCursoPlan("nombre_Cur")%></td>
			<td <%=bordeciclo%> align="center" width="5%"><%=rsCursoPlan("creditos_Cur")%></td>			
			<td <%=bordeciclo%> align="center" width="4%"><%=rsCursoPlan("horasteo_Cur")%></td>
			<td <%=bordeciclo%> align="center" width="4%"><%=rsCursoPlan("horaspra_Cur")%></td>
			<td <%=bordeciclo%> align="center" width="4%"><%=rsCursoPlan("horaslab_Cur")%></td>
			<td <%=bordeciclo%> align="center" width="4%"><%=rsCursoPlan("horasase_Cur")%></td>
			<td <%=bordeciclo%> align="center" width="4%"><%=rsCursoPlan("totalhoras_Cur")%></td>
			<td <%=bordeciclo%> align="center" width="10%" style="color:blue">
			<%if rsCursoPlan("grupos")>0 then
				response.write("Programados (" & rsCursoPlan("grupos") & ")")
			end if
			%>
			</td>
			</tr>
				<%rsCursoPlan.movenext
			loop
			set rsCursoPlan=nothing
		%>
		</table>
		</form>
		</div>
	    </td>
      	</tr>
		<tr bgcolor="#F0F0F0">
      	<td height="5%" width="100%" colspan="10">
      	<table width="100%">
			<tr class="etiqueta">
				<td width="10%">Mostrar
				</td>
				<td width="10%">
				<select name="cboFiltro" onchange="FiltrarCursos()">
				<option value="T">Todo</option>
				<option value="N">No Programado</option>
				<option value="P">Programado</option>
				</select>				
				</td>
				<td width="65%">
				Programados:<%=p%> | No Programados: <span class="rojo"><%=n%></span>&nbsp;
				| <span class="usatsubtitulousuario" id="seleccionadas"></span>
				</td>
				<td width="15%" align="right" class="rojo">
				<%if (codigo_tfu=1 or codigo_tfu=7 or codigo_tfu=16) or _
    					int(codigo_cac)>=int(session("codigo_cac")) then
    				%>
				<input disabled="true" style="width:170px" name="cmdGuardar" type="button" value="    Generar Programación" class="attach_prp" onclick="EnviarMarcas()">
				<%else%>
					<b>[Bloqueado]</b>
				<%end if%>
				</td>
			</tr>
		</table>
      	</td>
      </tr>      	
      </table>
      </td>
      </tr>
	<%end if%>		
	</table>
</body>
</html>