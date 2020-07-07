<!--#include file="../../../funciones.asp"-->
<%
Dim codigo_cpf,codigo_pes

nombre_cpf=request.querystring("nombre_cpf")
codigo_cpf=request.querystring("codigo_cpf")
codigo_pes=request.querystring("codigo_pes")
nombre_pes=request.querystring("nombre_pes")

Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=" & nombre_cpf & codigo_pes & ".xls"
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Seleccione el curso que desea agregar a la matrícula</title>
<style>
<!--
.usatceldatitulo { font-weight: bold; text-align: center; background-color: #CCFFCC }
.titulo      { font-family: Arial; font-size: 13pt; color: #800000; text-align: center; 
               font-weight: bold }
-->
</style>
</head>
<body>
<table cellpadding="2" cellspacing="0" style="border-collapse: collapse; border: 0px solid #C0C0C0; " bordercolor="#111111" width="100%">
  <tr>
    <td width="100%" height="3%" colspan="8" class="titulo">Planes de Estudio</td>
  </tr>
  <tr>
    <td width="26%" height="3%" colspan="3">Escuela Profesional</td>
    <td width="74%" height="3%"><%=nombre_cpf%></td>
  </tr>
  <tr>
    <td width="26%" height="3%" colspan="3">Plan de Estudio</td>
    <td width="74%" height="3%"><%=nombre_pes%></td>
  </tr>
  </table>
<br>
<%
	Set objPlan=Server.CreateObject("PryUSAT.clsDatPlanestudio")
		Set rsCursos= objPlan.ConsultarCursoPlan("RS","PL",codigo_pes,codigo_cpf,0)
	Set objPlan=nothing
%>
<table cellpadding="2" cellspacing="0" style="border-collapse: collapse; border: 0px solid #C0C0C0; " bordercolor="#111111" width="100%">
  <tr>
    <td width="100%" height="83%" valign="top">
    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
      <tr class="usatceldatitulo">
    	<td width="5%" height="5%">&nbsp;</td>
    	<td width="5%" height="5%">Tipo</td>
	    <td width="10%" height="5%">Código</td>
    	<td width="55%" height="5%">Descripción del curso</td>
	    <td width="5%" height="5%">Crd.</td>
    	<td width="5%" height="5%">Hrs.</td>
	    <td width="5%" height="5%">Ciclo</td>
	    <td width="5%" height="5%">Obligatorio</td>
      </tr>
    	<%k=0
		Do while not rsCursos.eof
			k=k+1
			totalcrd=totalcrd + rsCursos("cred.")
			totalhrs=totalhrs + rsCursos("horas")
		%>
      <tr>
        <td width="5%"><%=k%>&nbsp;</td>
	    <td width="8%"><%=rsCursos("tipo")%>&nbsp;</td>
    	<td width="10%"><%=rsCursos("indentificador")%>&nbsp;</td>
	    <td width="63%"><%=rsCursos("nombre")%>&nbsp;</td>
    	<td width="5%" align="center"><%=rsCursos("cred.")%>&nbsp;</td>
	    <td width="5%" align="center"><%=rsCursos("horas")%>&nbsp;</td>
    	<td width="5%" align="center"><%=ConvRomano(rsCursos("ciclo"))%>&nbsp;</td>
		<td width="5%" align="center"><%=iif(rsCursos("electivo")=0,"Sí","No")%>&nbsp;</td>
      </tr>
      	<%rsCursos.movenext
      	loop
      Set rscursos=nothing%>
      <tr bgcolor="#FFFFCC">
        <td width="86%" colspan="4" align="right"><b>Total: </b></td>
    	<td width="5%" align="center"><b><%=totalcrd%></b>&nbsp;</td>
	    <td width="5%" align="center"><b><%=totalhrs%></b>&nbsp;</td>
    	<td width="5%" align="center">&nbsp;</td>
    	<td width="5%" align="center">&nbsp;</td>
      </tr>
      	</table>
</table>
</body>
</html>