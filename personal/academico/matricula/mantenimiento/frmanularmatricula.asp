<!--#include file="../../../../funciones.asp"-->
<%
if(session("codigo_usu") = "") then
    Response.Redirect("../../../../sinacceso.html")
end if

on error resume next

accion=request.querystring("accion")
modo=request.querystring("modo")
HayReg=false
if modo="" then
	session("codigo_alu")=""
end if

if (modo="resultado" and session("codigo_alu")<>"") then
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
		Set rsMatricula=Obj.Consultar("ConsultarMatricula","FO","2",session("codigo_alu"),0,0)
		Obj.CerrarConexion
	Set obj=nothing

	if Not(rsMatricula.BOF AND rsMatricula.EOF) then
		HayReg=true
		alto="99%"
	end if
end if
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<META HTTP-EQUIV="Last-modified:" CONTENT="11-April-07">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Anular matrículas</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript">
	function VerificarAccion(obj)
	{
		if (obj.value=="D"){
			trCiclo.style.display="none"
		}
		
		if (obj.value=="T"){
			trCiclo.style.display=""
		}

	}
	
	function EnviarDatos()
	{
		var codigo_cac=0
		
		if (document.all.cbocodigo_mat!=undefined)
		{
			codigo_cac=cbocodigo_mat.value
		}
	
		location.href="procesar.asp?accion=anularmatricula&codigo_alu=<%=session("codigo_alu")%>&tipo=" + cbotipo.value + "&obs=" + txtobs.value + "&codigo_cac=" + codigo_cac
	}
</script>
</head>
<body bgcolor="#f0f0f0" onload="document.all.txtcodigouniver_alu.focus();">
<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
    <tr>
      <td class="usattitulo" style="height: 4%; width: 35%;">Anular Matrícula</td>
      <td style="height: 4%; width: 65%;">
     	<%
     	if (modo<>"resultado" or session("codigo_alu")="") then 	
	    	call buscaralumno("matricula/mantenimiento/frmanularmatricula.asp","../../",-1)
	    end if
	    %>
      </td>
    </tr>
    <%if (modo="resultado" or session("codigo_alu")<>"") then%>
	<tr>
      <td colspan="2" class="etiqueta" valign="top" height="15%">
      <!--#include file="../../fradatos.asp"-->
      </td>
    </tr>
   	<%end if%>
</table>
<br>
<%if (modo="resultado" OR session("codigo_alu")<>"") then%>
<table class="contornotabla" border="0" cellpadding="4" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
	<tr>
		<td style="width: 25%">Acciones</td>
		<td style="width: 65%">
		<select name="cbotipo" onchange="VerificarAccion(this)">
			<option value="T">Retiro de ciclo académico</option>
			<!--<option value="V">Reserva de matrícula</option>-->
			<option value="D">Retiro definitivo de la Universidad</option>
			<option value="C">Retiro de ciclo académico sin complementario</option>
			<option value="E">Retiro por devolución de matrícula</option>
			<option value="I">Retiro por inasistencia</option>
		</select>
		</td>
	</tr>
	<tbody id="trCiclo">
	<tr>
		<td style="width: 25%">Ciclos Matriculados</td>
		<td class="azul" style="width: 65%">
		<%
		if HayReg=true then
			rsMatricula.movefirst
			Call llenarlista("cbocodigo_mat","",rsMatricula,"codigo_cac","descripcion_cac",0,"","","")
		else
			response.write "El estudiante no ha efectuado matrículas"
		end if
		%>
		</td>
	</tr>
	<tr>
		<td style="width: 25%">&nbsp;</td>
		<td class="rojo" style="width: 65%">
		(*) Seleccione el ciclo matriculado al cual se desea retirar</td>
	</tr>
	</tbody>
	<tr>
		<td style="width: 25%" valign="top">Observaciones</td>
		<td style="width: 65%">
		<textarea name="txtobs" cols="20" rows="5" class="Cajas2" onkeypress="ContarTextArea(this,255)"></textarea></td>
	</tr>
	<tr>
		<td align="right" colspan="2" width="100%" id="lblcontador" class="rojo">
		
		</td>
	</tr>
	<tr>
		<td style="width: 25%">&nbsp;</td>
		<td style="width: 65%">&nbsp;</td>
	</tr>
	</table>
<p align="center">
<input name="cmdGuardar" type="button" value="      Ejecutar acción" class="guardar_prp" onclick="EnviarDatos()">
</p>
<%end if

if(Err.number <> 0) then
    response.Write("Error: " & Err.Description  & "<br/> Source: " & Err.Source )
end if
%>
</body>
</html>