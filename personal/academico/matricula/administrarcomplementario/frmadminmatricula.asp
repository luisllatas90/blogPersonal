<!--#include file="../../../../funciones.asp"-->
<%
on error resume next
Enviarfin session("codigo_usu"),"../../../../"

codigo_alu=request.querystring("codigo_alu")
modo=request.querystring("modo")
apto=request.querystring("apto")

paginaorigen="matricula/administrarcomplementario/frmadminmatricula.asp"

if codigo_alu<>"" and modo="resultado" then
	'determinar si el alumno tiene alguna categorizacion
	'if (cdbl(session("codigo_cpf"))=4 or cdbl(session("codigo_cpf"))=11 or cdbl(session("codigo_cpf"))=3) then 
	'	response.redirect "../mensajes.asp?proceso=B"
	'end if

	response.redirect("../verificaraccesomatriculaComp.asp?rutaActual=../academico/matricula/administrarComplementario")
	apto="S"

    
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Administrar matrícula del estudiante</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="private/validarfichamatricula.js"></script>
</head>
<body onload="document.all.txtcodigouniver_alu.focus()">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="60%" class="usattitulo">Asesorar matrícula del estudiante (<%=session("descripcion_cac")%>)</td>
	<%if codigo_alu="" then%>
    <td width="40%"><%call buscaralumno(paginaorigen,"../../",2) 'request.querystring("mod")%></td>
    <%end if%>
  </tr>
  <tr>
    <td width="100%" colspan="2">&nbsp;</td>
  </tr>
</table>
<br/>
<%if codigo_alu <> ""  then%>
<!--#include file="../../fradatos.asp"-->
  <%' Bloquear a estudiantes con separación
  tieneSeparacion= 0
 
    dim rsSeparacion 
	Set objSeparacion=Server.CreateObject("PryUSAT.clsAccesoDatos")	
	objSeparacion.AbrirConexion
	Set rsSeparacion=objSeparacion.Consultar("ACAD_ConsultarSeparacionVigente","FO",codigo_alu)
	objSeparacion.CerrarConexion
	Set objSeparacion=nothing

	if Not(rsSeparacion.BOF and rsSeparacion.EOF) then
	    tieneSeparacion= 1
	    if rsSeparacion("codigo_tse") =1 then
	        motivoSeparacion = "<b>" & rsSeparacion("descripcion_tse") & "</b>" & " desde " & rsSeparacion("fechaIni_sep") & " hasta " & rsSeparacion("fechafin_sep") & " por motivo: " & "<b>" & rsSeparacion("motivo_sep") & "</b>"
	        if session("codigo_tfu")=11 then ' SI ES COMPLEMENTARIO QUE NO BLOQUEE POR SEPARACION TEMPORAL
              tieneSeparacion= 0
            end if 
	    else
	        motivoSeparacion = "<b>" & rsSeparacion("descripcion_tse") & "</b>" & " por motivo: <b>" & rsSeparacion("motivo_sep") & "</b>"
	    end if 
	end if 

  if tieneSeparacion = 1 then %>
    <br/>
	<table align="center" bgcolor="#EEEEEE" style="width: 80%;height:10%" cellpadding="3" class="contornotabla_azul">
		<tr>
			<td valign="middle" align="center">
			<img alt="Mensaje" src="../../../../Images/menus/noconforme_1.gif" alt="" 
                    style="height: 46px; width: 47px"></td>
			<td>
				El estudiante tiene <%=motivoSeparacion%> <br/><br/>
				Por lo cual no podrá matricularse para el semestre. Cualquier duda consulte con el 
                Director de Escuela</td>
		</tr>
		</table>
  <% else  %>
    <%if session("Ultimamatricula")<>session("descripcion_cac") and apto="S" then%>
	<table align="center" bgcolor="#EEEEEE" style="width: 80%;height:30%" cellpadding="3" class="contornotabla_azul">
		<tr>
			<td rowspan="2" valign="top">
			<img alt="Mensaje" src="../../../../images/alerta.gif" alt=""></td>
			<td class="usatTitulousat">
				No se han encontrado asignaturas matriculadas para el ciclo académico <%=session("descripcion_cac")%>
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
	<!--<td class="pestanabloqueada" id="tab" align="center" width="14%" onclick="ResaltarPestana2('2','','estadocuenta.asp')">-->
	<td class="pestanabloqueada" id="tab" align="center" width="14%" onclick="ResaltarPestana2('2','','../../../../librerianet/academico/admincuentaper.aspx?id=<%=session("codigo_alu")%>&VerDatos=0')">
        Estado de cuenta</td>   
    <td width="1%" class="bordeinf">&nbsp;</td>
	<td class="pestanabloqueada" id="tab" align="center" width="14%" onclick="ResaltarPestana2('3','','../../../../librerianet/academico/historial_personal.aspx?id=<%=session("codigo_alu")%>&VerDatos=0')">
        Historial</td>
    <td width="32%" class="bordeinf" align="right">&nbsp;</td>
</tr>
<tr height="93%">
<td width="100%" valign="top" colspan="8" class="pestanarevez">
	<iframe id="fracontenedor" height="100%" width="100%" border="0" frameborder="0" src="detallematricula.asp">
	</iframe>
</td>
</tr>
</table>
<%  end if
  end if 
end if 
%>
<%
    If Err.Number <> 0 Then  
        Response.Write (Err.Description& "<br>")                     
    End If
%>
</body>
</html>