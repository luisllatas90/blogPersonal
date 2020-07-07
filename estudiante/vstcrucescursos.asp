<!--#include file="../NoCache.asp"-->
<%
	codigo_alu=session("codigo_alu")
	codigo_cac=session("codigo_cac")
	codigo_pes=session("codigo_pes")
	cursosprogramados="0,"

	'response.write (	cursosprogramados)
	'response.write (	codigo_pes)
		
	Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
	objMatricula.AbrirConexion
	'************************ primero realizar la validacion de cruces de horario *****************
	set rs_cruceshorario = objMatricula.Consultar("sp_validarcrucesmatriculaweb","FO",codigo_alu,codigo_pes,codigo_cac,cursosprogramados)
	objMatricula.cerrarconexion

	if Not (rs_cruceshorario.BOF and rs_cruceshorario.EOF) then
 %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" >
<title>Detalle del cruce de horarios</title>
<style type="text/css">
<!--
.Estilo22 {font-family: Verdana, Arial, Helvetica, sans-serif; font-size: 9px; }
.Estilo27 {font-size: 10px}
.Estilo28 {font-weight: bold; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;}
.Estilo31 {font-size: 14pt}
.Estilo32 {
	font-size: 9pt;
	font-weight: bold;
}
-->
</style>	
			<script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>				
            <link href="../private/estilo.css" rel="stylesheet" type="text/css" >
</head>
<body>
<p align="center" class="usatTablaSubTitulo Estilo31">	Se ha producido el siguiente cruces de horario : </p>
<table width="100%" height="57" border="0" align="center" cellpadding="0" cellspacing="0" class="contornotabla" alt="cruces de horas">
  <tr>
    <td width="160" height="27" bordercolor="#767C4A" bgcolor="#FBF9C8" class="bordeinf"><div align="center" class="Estilo27"><span class="Estilo28">Nombre del Curso</span></div></td>
    <td width="55" bordercolor="#767C4A" bgcolor="#FBF9C8" class="bordeinf"><div align="center" class="Estilo27"><span class="Estilo28">Hora Inicio</span></div></td>
    <td width="55" bordercolor="#767C4A" bgcolor="#FBF9C8" class="bordeinf"><div align="center" class="Estilo27"><span class="Estilo28">Hora Fin</span></div></td>
    <td width="40" bordercolor="#767C4A" bgcolor="#FBF9C8" class="bordeinf"><div align="center" class="Estilo27"><span class="Estilo28">Dia</span></div></td>
     <td width="60" height="27" bordercolor="#767C4A" bgcolor="#FBF9C8" class="bordeinf"><div align="center" class="Estilo27">
       <span class="Estilo28">Fecha Inicio</span></div></td>
    <td width="60" bordercolor="#767C4A" bgcolor="#FBF9C8" class="bordeinf">
    <div align="center" class="Estilo27" style="width: 92; height: 12"><span class="Estilo28">
      Fecha fin</span></div></td>
    <td width="160" height="27" bordercolor="#767C4A" bgcolor="#FBF9C8" class="bordeinf"><div align="center" class="Estilo27"><span class="Estilo28">Nombre del Curso</span></div></td>
    <td width="55" bordercolor="#767C4A" bgcolor="#FBF9C8" class="bordeinf"><div align="center" class="Estilo27"><span class="Estilo28">Hora Inicio</span></div></td>
    <td width="55" bordercolor="#767C4A" bgcolor="#FBF9C8" class="bordeinf"><div align="center" class="Estilo27"><span class="Estilo28">Hora Fin</span></div></td>
    <td width="40" bordercolor="#767C4A" bgcolor="#FBF9C8" class="bordeinf"><div align="center" class="Estilo27"><span class="Estilo28">Dia</span></div></td>
     <td width="60" height="27" bordercolor="#767C4A" bgcolor="#FBF9C8" class="bordeinf"><div align="center" class="Estilo27">
       <span class="Estilo28">Fecha Inicio</span></div></td>
    <td width="60" bordercolor="#767C4A" bgcolor="#FBF9C8" class="bordeinf">
    <div align="center" class="Estilo27" style="width: 92; height: 12"><span class="Estilo28">
      Fecha fin</span></div></td>
  </tr>
  <%
  	while (rs_cruceshorario.eof=false)
  %>
  <tr bgcolor="#FFFFFF" height=20 onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" >
    <td height="22" bordercolor="#660000"  width="160"><span class="Estilo22"><%=rs_cruceshorario("xnombre_cur")%></span></td>
    <td bordercolor="#660000"  width="55"><div align="center"><span class="Estilo22"><%=rs_cruceshorario("xnombre_hor")%></span></div></td>	
    <td bordercolor="#660000"  width="55"><div align="center"><span class="Estilo22"><%=rs_cruceshorario("xhorafin_lho")%></span></div></td>
	<td bordercolor="#660000"  width="60"><div align="center"><span class="Estilo22"><%=rs_cruceshorario("xdia_lho")%></span></div></td>	
	<td height="22" bordercolor="#660000"  width="60"><div align="right"><span class="Estilo22"><%=rs_cruceshorario("xfechainicio_cup")%></span></div></td>
    <td bordercolor="#660000"  width="60"><div align="right"><span class="Estilo22"><%=rs_cruceshorario("xfechafin_cup")%></span></div></td>	
    <td height="22" bordercolor="#666600"  width="160"><span class="Estilo22"><%=rs_cruceshorario("ynombre_cur")%></span></td>
    <td bordercolor="#666600"  width="55"><div align="center"><span class="Estilo22"><%=rs_cruceshorario("ynombre_hor")%></span></div></td>	
    <td bordercolor="#666600"  width="55"><div align="center"><span class="Estilo22"><%=rs_cruceshorario("yhorafin_lho")%></span></div></td>
	<td bordercolor="#666600"  width="60"><div align="center"><span class="Estilo22"><%=rs_cruceshorario("ydia_lho")%></span></div></td>	
	<td height="22" bordercolor="#666600"  width="60"><div align="right"><span class="Estilo22"><%=rs_cruceshorario("yfechainicio_cup")%></span></div></td>
    <td bordercolor="#666600"  width="60"><div align="right"><span class="Estilo22"><%=rs_cruceshorario("yfechafin_cup")%></span></div></td>	
  </tr>
  <% 
  rs_cruceshorario.movenext
  	wend
set rs_cruceshorario=nothing
  %>
</table>

<h5 align="center" class="rojo">Regularizar estos cruces a través de la opción [AGREGADOS Y RETIROS], en casos especiales acercarse a su Dirección de Escuela.</h5>
</body>
</html>
<%end if%>