<!--#include file="../../../../funciones.asp"-->
<%
codigo_tfu=session("codigo_tfu")
codigo_cup=request.querystring("codigo_cup")
codigo_cac=request.querystring("codigo_cac")
codigo_pes=request.querystring("codigo_pes")
codigo_cur=request.querystring("codigo_cur")

if codigo_cac<>"" then
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Detalle de curso programado</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../private/validarprogramacion.js"></script>
</head>
<body>
<%
Set objCurso=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objCurso.AbrirConexion
		Set rsCursoProgramado= objCurso.Consultar("ConsultarCursoProgramado","FO",2,codigo_cup,codigo_cac,codigo_pes,codigo_cur)
	objCurso.CerrarConexion
Set objCurso=nothing

If Not(rsCursoProgramado.BOF and rsCursoProgramado.EOF) then
%>
<input name="txtcodigo_cupPadre" type="hidden" value="<%=codigo_cup%>" />
<table style="width: 100%" class="contornotabla">
	<tr class="etiqueta" bgcolor="#FFFFCC">
		<td>Fecha Inicio: <%=rsCursoProgramado("fechainicio_cup")%></td>
		<td>Fecha Fin: <%=rsCursoProgramado("fechafin_cup")%></td>
		<td>Fecha Retiro: <%=rsCursoProgramado("fecharetiro_cup")%></td>
	</tr>
</table>
<p class="usatCeldaMenuSubTitulo"><u>Asignaturas que forman parte del grupo horario</u>:</p>
<table border="1" bordercolor="gray" cellpadding="2" cellspacing="0" style="border-collapse: collapse" width="100%">
  <tr class="etabla">
    <td width="15%">Fecha de Creación</td>
    <td width="65%">Asignatura</td>    
    <td width="10%">Inscritos</td>
    <td width="10%">Eliminar</td>
  </tr>
  <%Do while Not rsCursoProgramado.EOF
  		curso=rsCursoProgramado("nombre_cur")
  		if cdbl(codigo_cup)=cdbl(rsCursoProgramado("codigo_cup")) then
  			clase="class='rojo'"
  			curso="<b>" & curso & "(Principal)</b>"
  		else
  			clase=""
		  	i=i+1  			
  		end if
  %>
  <tr valign="top" <%=clase%> onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)">
    <td width="15%"><%=rsCursoProgramado("fechadoc_cup")%></td>
    <td width="65%">
    <%=curso%><br>
  	<%=rsCursoProgramado("abreviatura_pes")%><br><br>
    <em>Registrado por: <%=rsCursoProgramado("login_per")%></em>
    </td>
    <td width="10%" align="center"><%=rsCursoProgramado("total_mat")%></td>
    <td width="10%" align="center" class="rojo">
	<%if (codigo_tfu=1 or codigo_tfu=7 or codigo_tfu=16) or _
		int(codigo_cac)>=int(session("codigo_cac")) then
		
		if (codigo_tfu=1 or codigo_tfu=7 or codigo_tfu=16) or _
			cdbl(rsCursoProgramado("usuario_cup"))=cdbl(session("codigo_usu")) then
    %>
    <img class="imagen" alt="Eliminar Grupo Horario" src="../../../../images/eliminar.gif" onclick="AbrirGrupo('E','<%=codigo_cac%>','<%=codigo_pes%>','<%=codigo_cur%>','<%=rsCursoProgramado("codigo_cup")%>')">
		<%else
			response.write "[Bloqueado porque ha sido registrado por otro usuario]"
		end if
	else%>
    <b>[Bloqueado]</b>
    <%end if%>
    </td>
  </tr>
  	<%rsCursoProgramado.movenext
  Loop
  	Set rsCursoProgramado=nothing
  %>
</table>
<p align="right" class="usatEtiqOblig">Nº de Sub Grupos: <%=i%></p>
<%else%>
	<p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; El curso programado seleccionado, no existe en la Base de datos</p>
<%end if%>
</body>
</html>
<%end if%>