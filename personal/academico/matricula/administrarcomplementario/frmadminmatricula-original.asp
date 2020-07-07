<!--#include file="../../../../funciones.asp"-->
<%
Enviarfin session("codigo_usu"),"../../../../"

codigo_alu=request.querystring("codigo_alu")
modo=request.querystring("modo")
apto=request.querystring("apto")

'---------------------------------------------
'INDICAR MANUAL CODIGO_CAC Y DESCRIPCION_CAC
'---------------------------------------------
tipo_cac="E"
codigo_cac=35
descripcion_Cac="2010-0"
'---------------------------------------------

if codigo_alu<>"" and modo="resultado" then
	'determinar si el alumno tiene alguna categorizacion
	'if (cdbl(session("codigo_cpf"))=4 or cdbl(session("codigo_cpf"))=11 or cdbl(session("codigo_cpf"))=3) then 
	'	response.redirect "../mensajes.asp?proceso=B"
	'end if

	response.redirect("../verificaraccesomatricula2.asp?rutaActual=../academico/matricula/administrarcomplementario")
	apto="S"
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Administrar matrícula del estudiante</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="private/validarfichamatricula.js"></script>
</head>
<body onload="document.all.txtcodigouniver_alu.focus()">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="60%" class="usattitulo">Matrícula con cargo (<%=descripcion_cac%>)</td>
	<%if codigo_alu="" then%>
    <td width="40%"><%call buscaralumno("matricula/administrarcomplementario/frmadminmatricula.asp","../../")%></td>
    <%end if%>
  </tr>
  <tr>
    <td width="100%" colspan="2">&nbsp;</td>
  </tr>
</table>
<%if codigo_alu<>"" then%>
<!--#include file="../../fradatos.asp"-->
<br>
<%if session("Ultimamatricula")<>descripcion_cac and apto="S" then%>
<table align="center" bgcolor="#EEEEEE" style="width: 80%;height:30%" cellpadding="3" class="contornotabla_azul">
	<tr>
		<td rowspan="2" valign="top">
		<img alt="Mensaje" src="../../../../images/alerta.gif"></td>
		<td class="usatTitulousat">
			No se han encontrado asignaturas matriculadas para el ciclo académico <%=descripcion_cac%>
		</td>
	</tr>
	<tr class="usatTitulo">
		<td>¿Desea realizar una nueva matrícula?</td>
	</tr>		
	<tr>
		<td align="center">&nbsp;</td>
		<td align="center">
		<input type="button" value="       Aceptar" name="cmdNueva" class="conforme1" onClick="modificarmatricula('N','<%=session("codigo_pes")%>')">
		</td>
	</tr>
</table>
<%else%>
<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="73%">
<tr height="7%">
	<td class="pestanaresaltada" id="tab" align="center" width="22%" onclick="ResaltarPestana2('0','','detallematricula.asp')">
    Asignaturas Matriculadas</td>
	<td width="1%" class="bordeinf">&nbsp;</td>
	<td class="pestanabloqueada" id="tab" align="center" width="14%" onclick="ResaltarPestana2('1','','vsthorario.asp')">
    Horarios</td>
	<td width="1%" class="bordeinf">&nbsp;</td>
	<td class="pestanabloqueada" id="tab" align="center" width="14%" onclick="ResaltarPestana2('2','','estadocuenta.asp')">
    Estado de cuenta</td>   
    <td width="32%" class="bordeinf" align="right">&nbsp;</td>
</tr>
<tr height="93%">
<td width="100%" valign="top" colspan="6" class="pestanarevez">
	<iframe id="fracontenedor" height="100%" width="100%" border="0" frameborder="0" src="detallematricula.asp">
	</iframe>
</td>
</tr>
</table>
<%end if
end if%>
</body>
</html>