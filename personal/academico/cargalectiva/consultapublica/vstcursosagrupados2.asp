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
	
	if codigo_cac<>"-2" then
		Set rsCursoPlan= Obj.Consultar("ConsultarCursoProgramadoAgrupado","FO",1,codigo_cac,"","")

		if Not(rsCursoPlan.BOF and rsCursoPlan.EOF) then
			activo=true
			alto="height=""98%"""
		end if
	end if
%>
<html>
<head>
<title>Cursos Agrupados</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../private/validarprogramacion.js"></script>
<style type="text/css">
.bloque {
	border-style: solid none solid none;
	border-width: 1px;
	border-color: #C0C0C0;
}
</style>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
<tr>
	<td height="3%" colspan="5" class="usattitulo">Lista de asignaturas 
	agrupadas</td>
</tr>
<tr>
	<td height="3%" style="width: 20%">Ciclo Académico:</td>
	<td height="3%" style="width: 15%">
	<%call llenarlista("cbocodigo_cac","",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>			
	</td>
	<td height="3%" style="width: 20%" align="right">Escuela Profesional</td>
	<td height="3%" style="width: 65%">
	[TODAS]
	</td>
	<td height="3%" style="width: 10%" align="right">
    <img alt="Buscar cursos programados" src="../../../../images/menus/buscar_small.gif" class="imagen" onclick="location.href='vstcursosagrupados.asp?codigo_cac='+cbocodigo_cac.value">
	</td>
</tr>
<%if activo=true then%>
  <tr height="90%" valign="top">
    <td width="100%" colspan="5" id="trCursos" class="contornotabla">
    <div id="listadiv" style="height:100%;width:100%">
    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="100%">
      <tr>
        <td width="100%" colspan="6">
		<%	Do while not rsCursoPlan.eof
				'**********************************
				'Imprime el curso PADRE
				'**********************************
		%>
			<table width="100%" border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="gray">
			<tr class="etabla">
				<td width="5%" height="3%">Ciclo</td>
				<td width="10%" height="3%">Código</td>
				<td width="35%" height="3%">Descripcion</td>
				<td width="5%" height="3%">Créd.</td>
				<td width="30%" height="3%">Escuela Profesional / Plan estudio</td>
				<td width="10%" height="3%">Grupo Horario</td>
			</tr>		
			<tr class="rojo" height="15px" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
				<td align="center" width="5%"><%=ConvRomano(rsCursoPlan("ciclo_Cur"))%>&nbsp;</td>
				<td width="10%"><%=rsCursoPlan("identificador_Cur")%></td>
				<td width="35%"><%=rsCursoPlan("nombre_Cur")%>(<i>Principal</i>)</td>
				<td width="5%" align="center"><%=rsCursoPlan("creditos_Cur")%></td>
				<td width="30%"><%=rsCursoPlan("nombre_cpf")%></td>
				<td align="center" width="10%" style="color:blue">
				<%=rsCursoPlan("grupohor_cup")%>
				</td>
			</tr>
			<%
				'**********************************
				'Imprime cursos HIJO
				'**********************************
			
			'Set rsHijos= Obj.Consultar("ConsultarCursoProgramadoAgrupado","FO",2,rsCursoPlan("codigo_cup"),"","")
			
			'If Not(rsHijos.BOF and rsHijos.EOF) then
			If Isnull(rsCursoPlan("codigoHijo"))=false  then
				i=i+1
			%>
			<tr valign="top">
				<td align="center" width="5%"><%=ConvRomano(rsCursoPlan("cicloHijo"))%>&nbsp;</td>
				<td width="10%"><%=rsCursoPlan("codigoHijo")%></td>
				<td width="35%"><%=rsCursoPlan("cursoHijo")%> (<i>Secundario</i>)</td>
				<td width="5%" align="center"><%=rsCursoPlan("creditosHijo")%></td>
				<td width="30%"><i><%=rsCursoPlan("escuelaHijo")%></i></td>
				<td width="10%">&nbsp;</td>
			</tr>
			<%end if%>
			</table>
			<br>
				<%rsCursoPlan.movenext
			loop
			set rsCursoPlan=nothing
		%>
	    </td>
      	</tr>
      </table>
      </div>
      </td>
      </tr>
	<tr bgcolor="#F0F0F0">
      	<td height="5%" width="100%" colspan="5" class="pestanarevez">
      	<table width="100%">
			<tr class="etiqueta">
				<td width="65%">
				TOTAL:<%=I%>&nbsp;
				</td>
				<td width="15%" align="right" class="rojo">
				...
				</td>
			</tr>
		</table>
      	</td>
      </tr>      
	<%end if%>
	
	</table>	
</body>
</html>
<%

    obj.CerrarConexion
Set obj=nothing
%>