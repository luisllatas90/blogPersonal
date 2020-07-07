<!--#include file="../../../../NoCache.asp"-->
<%
	codigo_alu=request.QueryString("codigo_alu")
	codigo_cac=request.QueryString("codigo_cac")
	codigo_pes=request.QueryString("codigo_pes")
	cursosprogramados=request.QueryString("cursosprogramados")	

	'response.write (	cursosprogramados)
	'response.write (	codigo_pes)
		
	Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
	objMatricula.AbrirConexion
	'************************ primero realizar la validacion de cruces de horario *****************
	set rs_cruceshorario = objMatricula.Consultar("sp_validarcrucesmatriculaweb","FO",codigo_alu,codigo_pes,codigo_cac,cursosprogramados)
		objMatricula.cerrarconexion
 %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" >
<title>Detalle del cruce de horarios</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
</head>
<body bgcolor="#EEEEEE">
<p align="center" class="usattitulousuario">	Se ha producido el siguiente cruce de horario : </p>
<table bgcolor="white" width="100%" border="1" align="center" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="gray">
  <tr class="etabla">
    <td width="160">Nombre del Curso</td>
    <td width="55">Hora Inicio</td>
    <td width="55">Hora Fin</td>
    <td width="40">Dia</td>
    <td width="60">Fecha Inicio</td>
    <td width="60">Fecha fin</td>
    <td width="160">Nombre del Curso</td>
    <td width="55">Hora Inicio</td>
    <td width="55">Hora Fin</td>
    <td width="40">Dia</td>
    <td width="60">Fecha Inicio</td>
    <td width="60">Fecha fin</td>
  </tr>
  <%
  	while (rs_cruceshorario.eof=false)
  %>
  <tr height=20 onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" >
    <td width="160"><%=rs_cruceshorario("xnombre_cur")%></td>
    <td width="55"><%=rs_cruceshorario("xnombre_hor")%></td>	
    <td width="55"><%=rs_cruceshorario("xhorafin_lho")%></td>
	<td width="60"><%=rs_cruceshorario("xdia_lho")%></td>	
	<td width="60"><%=rs_cruceshorario("xfechainicio_cup")%></td>
    <td width="60"><%=rs_cruceshorario("xfechafin_cup")%></td>	
    <td width="160"><%=rs_cruceshorario("ynombre_cur")%></td>
    <td width="55"><%=rs_cruceshorario("ynombre_hor")%></td>	
    <td width="55"><%=rs_cruceshorario("yhorafin_lho")%></td>
	<td width="60"><%=rs_cruceshorario("ydia_lho")%></td>	
	<td width="60"><%=rs_cruceshorario("yfechainicio_cup")%></td>
    <td width="60"><%=rs_cruceshorario("yfechafin_cup")%></td>	
  </tr>
  <% 
  rs_cruceshorario.movenext
  	wend
set rs_cruceshorario=nothing
  %>
</table>
<p align="center" class="azul">	Verifique que en los Cursos a Matricular, no existan cruces de horario. </p>
</body>
</html>