<!--#include file="../../../../funciones.asp"-->
<%
	tipo=request.querystring("tipo")
	codigo_alu=request.querystring("codigo_alu")
	codigo_pes=request.querystring("codigo_pes")

	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsHistorial=Obj.Consultar("GenerarCertificadoEstudios","FO",tipo,codigo_alu,codigo_pes)
	Obj.CerrarConexion
	Set Obj=nothing
	
	If (rsHistorial.BOF and rsHistorial.EOF) then%>
			<script language="Javascript">
				alert("No se han registrado Historial Académico para el estudiante seleccionado")
				history.back(-1)
			</script>
	<%else
		
		alumno=rsHistorial("alumno")
		nombre_esp=rsHistorial("especialidad")
		nombre_cpf=rsHistorial("escuela")
		nombre_fac=rsHistorial("facultad")
		codigouniver_alu=rsHistorial("codigouniver_alu")
		
		Response.ContentType = "application/msword"
		Response.AddHeader "Content-Disposition","attachment;filename=" & codigouniver_alu & ".doc"
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<style>
<!--
.etiqueta    { font-weight: bold }
.cabezera    { font-weight: bold; text-align: center; background-color: #C0C0C0; border-left-width:1; border-right-width:1; border-top-style:solid; border-top-width:1; border-bottom-style:solid; border-bottom-width:1}
body         { font-family: Belwe Lt BT; font-size: 9pt }
td           { font-size: 9pt }
-->
</style>
</head>
<body style="margin-top:3cm">
<table cellpadding="2" cellspacing="0" style="border-collapse: collapse" width="100%">
  <THEAD>
  <tr><td colspan="6">
  <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="130">
  <tr>
    <th style="font-size: 16pt" width="100%" colspan="2" height="50" align="center" valign="top">
    CERTIFICADO DE ESTUDIOS
    </th>
  </tr>
  <tr>
    <td width="24%" height="19" class="etiqueta">Apellidos y Nombres</td>
    <td width="76%" height="19">:&nbsp;<%=alumno%>&nbsp;</td>
  </tr>
  <tr>
    <td width="24%" height="19" class="etiqueta">Facultad</td>
    <td width="76%" height="19">:&nbsp;<%=nombre_fac%></td>
  </tr>
  <tr>
    <td width="24%" height="19" class="etiqueta">Escuela Profesional</td>
    <td width="76%" height="19">:&nbsp;<%=nombre_cpf%>&nbsp;</td>
  </tr>
  <%if nombre_esp<>"" then%>
  <tr>
    <td width="24%" height="19" class="etiqueta">Especialidad</td>
    <td width="76%" height="19">:&nbsp;<%=nombre_esp%></td>
  </tr>
  <%end if%>
  <tr>
    <td width="24%" height="19" class="etiqueta">Código Nº</td>
    <td width="76%" height="19">:&nbsp;<%=codigouniver_alu%>&nbsp;</td>
  </tr>
  </table>
  </td></tr>
  <tr><td>&nbsp;</td></tr>
  <tr>
    <td class="cabezera" height="14" width="13%">Código</td>
    <td class="cabezera" height="14" width="47%">Asignatura</td>
    <td class="cabezera" height="14" width="10%">Promedio</td>
    <td class="cabezera" height="14" width="10%">Créditos</td>
    <td class="cabezera" height="14" width="10%">Semestre Académico</td>
    <td class="cabezera" height="14" width="10%">Ciclo</td>
  </tr>
  </THEAD>
  <%
  Semestre=rsHistorial("descripcion_Cac") 'Capturar el 1er Semestre
  totalcrd=0:notacrd=0:totalcursos=0:NumConvalidaciones=0
  
  Do while not rsHistorial.eof
	  	  	
		totalcursos=totalcursos+1
		totalcrd=totalcrd + cdbl(rsHistorial("creditocur_dma")) 'Sumatoria de Créditos matriculados
		notacrd=notacrd + cdbl(rsHistorial("notacredito"))  'Sumatorio de Nota * Crédito(Calculado)
		
		If Semestre<>rsHistorial("descripcion_cac") then
	  		Semestre=rsHistorial("descripcion_cac")
	  		response.write "<tr><td colspan=""6"">&nbsp;</td></tr>"
  		End if
  		
  		if rsHistorial("tipoMatricula_dma")="C" then
  			NumConvalidaciones=NumConvalidaciones+1
  		end if
  %>
  <tr>
    <td height="14" width="13%"><%=rsHistorial("identificador_cur")%></td>
    <td height="14" width="47%"><%=rsHistorial("nombre_cur")%></td>
    <td align="center" height="14" width="10%"><%=rsHistorial("notafinal_dma")%></td>
    <td align="center" height="14" width="10%"><%=rsHistorial("creditoCur_Dma")%></td>
    <td align="center" height="14" width="10%"><%=rsHistorial("descripcion_cac")%></td>
    <td align="center" height="14" width="10%"><%=ConvRomano(rsHistorial("ciclo_cur"))%></td>
  </tr>
  	<%rsHistorial.movenext
  loop%>
  </table>
  <%if NumConvalidaciones>0 then%>
  	<p><b>(*)Asignaturas convalidadas</b></p>
  <%end if%>	
  <br>
<div align="right">
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="60%" id="AutoNumber2">
    <tr>
      <td width="84%" align="right">TOTAL DE ASIGNATURAS CURSADAS</td>
      <td width="16%" class="etiqueta">:&nbsp;<%=totalcursos%></td>
    </tr>
    <%if int(totalcrd)>0 then%>
    <tr>
      <td width="84%" align="right">TOTAL DE CRÉDITOS APROBADOS</td>
      <td width="16%" class="etiqueta">:&nbsp;<%=totalcrd%></td>
    </tr>
    <tr>
      <td width="84%" align="right">PROMEDIO PONDERADO ACUMULADO</td>
      <td width="16%" class="etiqueta">:&nbsp;<%=FormatNumber((notacrd/totalcrd),2)%>&nbsp;</td>
    </tr>
    <%end if%>
  </table>
</div>
<p align="right"><b>Chiclayo, <%=QuitarDia(formatdatetime(date,1))%></b></p>
<p>&nbsp;</p>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="30%" align="right" class="etiqueta">
  <tr>
    <td width="100%" align="center">Mgtr. Jorge Pérez Uriarte</td>
  </tr>
  <tr>
    <td width="100%" align="center">Secretario General</td>
  </tr>
</table>
</body>
</html>
	<%end if
Set rsHistorial=nothing
%>