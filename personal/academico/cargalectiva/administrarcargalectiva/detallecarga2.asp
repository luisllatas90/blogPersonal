<%
codigo_cup=request.querystring("codigo_cup")
codigo_cac=request.querystring("codigo_cac")
codigo_dac=request.querystring("codigo_dac")
codigo_per=1
EncuentraReg=false
Agregar=false
Modificar=false
Eliminar=false

	Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
		set rsPermisos=obj.Consultar("ValidarPermisoAccionesEnProcesoMatricula","FO","0",codigo_cac,session("codigo_usu"),"cargaacademica")		
		Set rs=obj.Consultar("ConsultarCargaAcademicaDpto","FO",3,codigo_cup,0,0)
		
		if Not(rs.BOF and rs.EOF) then
			EncuentraReg=true		
		end if
		
		if not(rsPermisos.BOF and rsPermisos.EOF) then
			Agregar=rsPermisos("agregar_acr")
			Modificar=rsPermisos("modificar_acr")
			Eliminar=rsPermisos("eliminar_acr")
		end if
		
		obj.CerrarConexion
	Set obj=nothing
'Asignar permisos a Evaluación y Registros, Administrador,Programas especiales y si el ciclo >ciclo actual
if (session("codigo_tfu")=16 OR session("codigo_tfu")=1 OR codigo_cpf=25) or int(codigo_cac)>int(session("codigo_cac")) then
	Agregar=true
	Eliminar=true
	'response.write("Sin Permisos")
end if
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Detalle de Carga Académica</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/tooltip.js"></script>
<script type="text/javascript" language="JavaScript" src="../private/validarcargaacademica.js"></script>

</head>
<body bgcolor="#EEEEEE">
<%if EncuentraReg=true and rs("obs_cup")<>"" then%>
<table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" border="0"  width="100%" class="contornotabla">
<tr><td><b>Observaciones:</b></td></tr>
<tr><td><%=rs("obs_cup")%></td></tr>
</table>
<%end if%>

<p class="rojo">
<%if Agregar=true then%>
<input onClick="AbrirCarga('ACP','<%=codigo_cup%>','<%=codigo_cac%>','<%=codigo_dac%>')" type="button" value="     Agregar" name="cmdAgregar" class="agregar2" tooltip="Permite asignar el curso sólo a profesores del Departamento Académico seleccionado">
&nbsp;
<input onClick="AbrirCarga('DER','<%=codigo_cup%>')" type="button" value="     Derivar a otro Departamento" style="width:180px" name="cmdDerivar" class="guardar2" tooltip="Permite derivar el curso a un Profesor de otro Departamento Académico.<br>De esta manera el otro Departamento podrá visualizarlo y asignarle la carga académica">

<%else%>
[Bloqueado para asignar profesor]
<%end if%>
</p>
<%if EncuentraReg=true and rs("profesor")<>"" then%>
<table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" bgcolor="white">
  <tr class="etabla">
    <td width="5%">
	Eliminar
	</td>
    <td width="50%">Profesor</td>
    <td width="20%">Tipo</td>
    <td width="15%">Total Hrs. Curso</td>
    <td width="15%">Horario</td>	
  </tr>
  <%Do while not rs.EOF%>
  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
    <td width="5%" align="center">
	<%if Modificar=true or Eliminar=true then%>
    <img border="0" src="../../../../images/eliminar.gif" onClick="AbrirCarga('ECP','<%=codigo_cup%>','<%=codigo_cac%>','<%=rs("codigo_per")%>','<%=codigo_dac%>')">
    <%else%>
    [Bloqueado]
    <%end if%>
    </td>
    <td width="50%"><%=rs("profesor")%>&nbsp;</td>
    <td width="20%"><%=rs("descripcion_fun")%>&nbsp;</td>
    <td width="15%"><%=rs("totalhoras_car")%>&nbsp;</td>
	<td width="15%" align="center"><input type="button" value="Ver" onClick="AbrirPopUp('../../horarios/vsthorariodocente.asp?modo=A&codigo_per=<%=rs("codigo_per")%>&codigo_cac=<%=codigo_cac%>','500','600','yes','yes','yes')">&nbsp;</td>
  </tr>
  	<%rs.movenext
  loop
  Set rs=nothing
  %>
  </table>
<%else%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se han registrado Profesores para la asignatura seleccionada.</h5>
<%end if%>
</body>
