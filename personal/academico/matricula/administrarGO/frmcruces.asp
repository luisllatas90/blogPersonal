<!--#include file="../../../../NoCache.asp"-->
<%
    codigo_alu=request.QueryString("codigo_alu")
	codigo_cac=request.QueryString("codigo_cac")
	'codigo_pes=request.QueryString("codigo_pes")
	cursosprogramados=request.QueryString("cursosprogramados")	

  'codigo_alu=Request.Form("codigo_alu")
  'codigo_cac=Request.Form("codigo_cac")
  'cursosprogramados=Request.Form("cursosprogramados")
  sw=0

		
	Set objmatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
	objMatricula.AbrirConexion
	'************************ primero realizar la validacion de cruces de horario *****************
	'set rs_cruceshorario = objMatricula.Consultar("sp_validarcrucesmatriculaweb","FO",codigo_alu,codigo_pes,codigo_cac,cursosprogramados)	
	set rs_cruceshorario = objMatricula.Consultar("ACAD_validaCrucesMatriculaGO","FO",codigo_alu,codigo_cac,cursosprogramados)
	'set rs_cruceshorario = objMatricula.Consultar("sp_validarcrucesmatriculaweb_v2","FO",codigo_alu,codigo_pes,codigo_cac,cursosprogramados)
		objMatricula.cerrarconexion
 %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" >
<title>Detalle del cruce de horarios</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
</head>
<body bgcolor="#EEEEEE">
<p align="center" class="usatTituloLogin">	Se ha producido el siguiente cruce de horario:</p>
<table bgcolor="white" width="100%" border="1" align="center" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="gray">
  <tr class="etabla">
    <td width="160">Nombre del Curso</td>
    <td width="55">Hora Inicio</td>
    <td width="55">Hora Fin</td>
    <td width="40">Dia</td>
    <td width="60">Fecha Inicio</td>
    <td width="60">Fecha fin</td>    
    <td width="55">Hora Inicio</td>
    <td width="55">Hora Fin</td>
  </tr>
  <%
    while (rs_cruceshorario.eof=false)
    sw=1
  %>
      <tr height=20 onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)">
        <td width="160"><%=rs_cruceshorario("nombre_Cur")%></td>
        <td width="55"><%=rs_cruceshorario("nombre_Hor")%></td>	
        <td width="55"><%=rs_cruceshorario("horaFin_Lho")%></td>
	    <td width="60"><%=rs_cruceshorario("dia_Lho")%></td>	
	    <td width="60"><%=rs_cruceshorario("vfechainicio_cup")%></td>
        <td width="60"><%=rs_cruceshorario("vfechafin_cup")%></td>	
        <td width="160"><%=rs_cruceshorario("CruceInicio")%></td>        
        <td width="55"><%=rs_cruceshorario("CruceFin")%></td>	
      </tr>
      <% 
      rs_cruceshorario.movenext
    wend
set rs_cruceshorario=nothing
  %>
</table>
<input type="hidden" id="sw" value="<%=sw%>">
<p align="center" class="azul">	Verifique que en los Cursos a Matricular, no existan cruces de horario. </p>
</body>
</html>