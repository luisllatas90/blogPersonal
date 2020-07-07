<!--#include file="../../../../funciones.asp"-->
<%

if(session("codigo_usu") = "") then
    Response.Redirect("../../../../sinacceso.html")
end if

codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"
if codigo_cpf="" then codigo_cpf="-2"

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	
	Set rsCiclo= Obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
	
	if codigo_tfu=1 or codigo_tfu=7 or codigo_tfu=16 then
		Set rsEscuela= obj.Consultar("ConsultarCarreraProfesional","FO","MA",0)
	else
		Set rsEscuela= obj.Consultar("consultaracceso","FO","ESC","Silabo",codigo_usu)
	end if

	if codigo_cac<>"-2" and codigo_cpf<>"-2" then
		Set rsCursoPlan= Obj.Consultar("ConsultarCursoProgramado","FO",3,codigo_cpf,codigo_cac,"","")

		if Not(rsCursoPlan.BOF and rsCursoPlan.EOF) then
			activo=true
			alto="height=""100%"""
		end if
	end if
    obj.CerrarConexion
Set obj=nothing

'oncontextmenu="return false"
%>
<html>
<head>
<title>Programaci�n de Cursos</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../private/validarprogramacion.js"></script>
</head>
<body >
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
<tr>
	<td height="3%" colspan="5" class="usattitulo">Modificar programaci�n de asignaturas</td>
</tr>
<tr>
	<td height="3%" style="width: 20%">Semestre Acad�mico:</td>
	<td height="3%" style="width: 15%">
	<%call llenarlista("cbocodigo_cac","",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>			
	</td>
	<td height="3%" style="width: 20%" align="right">Carrera Profesional:</td>
	<td height="3%" style="width: 65%">
	<%call llenarlista("cbocodigo_cpf","",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccione la Escuela Profesional","","")%>
	</td>
	<td height="3%" style="width: 10%" align="right">
    <img alt="Buscar cursos programados" src="../../../../images/menus/buscar_small.gif" class="imagen" onclick="BuscarProgramacion('M')">
	</td>
</tr>
  <%if activo=true then%>
  <tr valign="top">
    <td height="44%" width="100%" colspan="5">
    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="100%">
      <tr class="etabla">
        <td width="5%" height="3%">Ciclo</td>
        <td width="10%" height="3%">C�digo</td>
        <td width="30%" height="3%">Descripcion</td>
        <td width="5%" height="3%">Cr�d.</td>
        <td width="10%" height="3%">Grupo Horario</td>
        <td width="10%" height="3%">Vacantes</td>
        <td width="5%" height="3%">Estado</td>        
        <td width="5%" height="3%">Inscritos</td>
        <td width="5%" height="3%">Primer Ciclo</td>
      </tr>
      <tr>
        <td width="100%" colspan="9">
        <div id="listadiv" style="height:100%" class="NoImprimir">
		<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#ccccccc" id="tblcursoprogramado">
		<%	i=0
			ciclo=1
						
			Do while not rsCursoPlan.eof
				i=i+1
				bordeciclo=Agrupar(rsCursoPlan("codigo_cur"),Ciclo)
		%>
			<tr height="20px" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" class="Sel" Typ="Sel" onclick="MarcarCursoProgramado()" codigo_cur="<%=rsCursoPlan("codigo_cur")%>" codigo_pes="<%=rsCursoPlan("codigo_pes")%>" codigo_cup="<%=rsCursoPlan("codigo_cup")%>">
				<td <%=bordeciclo%> align="center" width="5%"><%=ConvRomano(rsCursoPlan("ciclo_Cur"))%></td>
				<td <%=bordeciclo%> width="10%"><%=rsCursoPlan("identificador_Cur")%></td>
				<td <%=bordeciclo%> width="30%"><%=rsCursoPlan("nombre_Cur")%></td>
				<td <%=bordeciclo%> align="center" width="5%"><%=rsCursoPlan("creditos_Cur")%></td>
				<td <%=bordeciclo%> align="center" width="10%"><%=rsCursoPlan("grupohor_Cup")%></td>
				<td <%=bordeciclo%> align="center" width="10%"><%=rsCursoPlan("vacantes_Cup")%></td>
				<td <%=bordeciclo%> align="center" width="5%" style="color:red"><%=iif(rsCursoPlan("estado_cup")=false,"Cerrado","Abierto")%></td>
				<td <%=bordeciclo%> align="center" width="5%" style="color:red"><%=rsCursoPlan("total_mat")%></td>
				<td <%=bordeciclo%> align="center" width="5%" style="color:red"><% if rsCursoPlan("SoloPrimerCiclo_cup") then response.write("Si") else response.write("No") end if %></td>
				
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
      	<td height="5%" width="100%" colspan="9">
      	<table width="100%">
			<tr class="azul">
				<td width="30%">Detalle del Grupo Horario</td>
				<td width="70%" align="right" class="rojo">
				<%if (codigo_tfu=1 or codigo_tfu=7 or codigo_tfu=16) or _
    				int(codigo_cac)>=int(session("codigo_cac")) then
    			%>			
				<input style="width:100px" name="cmdAgregar" type="button" value="   A�adir Grupo" class="agregar2" disabled="true" onclick="AbrirGrupo('A')" />
				<input style="width:100px" name="cmdModificar" type="button" value="    Modificar Grupo" class="modificar2" disabled="true" onclick="AbrirGrupo('M')" />
				<%else%>
				<b>[No se puede modificar datos de ciclos anteriores]</b>
				<%end if%>
				</td>
			</tr>
		</table>
      	</td>
      </tr>      	
		<tr>
      	<td height="50%" width="100%" colspan="10">
      	<span class="usatsugerencia" id="mensajedetalle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Haga clic en la asignatura para visualizar los grupos horarios programados</span>
      	<iframe name="fradetalle" height="100%" width="100%" border="0" frameborder="0" >
		El explorador no admite los marcos flotantes o no est� configurado actualmente para mostrarlos.
		</iframe>
		</td>
      </tr>
	  <tr bgcolor="#F0F0F0">
      	<td height="5%" width="100%" colspan="10" align="right" class="azul">
		Total de asignaturas programadas: <%=i%>
      	</td>
      </tr>             	
      </table>
      </td>
      </tr>    
	<%end if%>		
	</table>
	<%if activo<>true and codigo_cac<>"-2" and codigo_cpf<>"-2" then%>
		<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp; <span><u>Importante</u></span>:<br>
		No se han programado asignaturas en el Plan de Estudio seleccionado, para ello debe ir al men� [Nuevo Programaci�n] y elegir las asignaturas a programar, seg�n el plan de estudio</h5>
	<%end if%>
</body>
</html>