<!--#include file="../../../../funciones.asp"-->
<%
Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=rptetotalmatriculados.xls"

codigo_cac=request.querystring("codigo_cac")
descripcion_cac=request.querystring("descripcion_cac")

Set obj = Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	Set rsCiclo=Obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
	set rsResumen=Obj.Consultar("ConsultarAlumnosMatriculados","FO",7,codigo_cac,0,0)
	obj.CerrarConexion
Set obj=nothing
%>
<html>
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
	<meta http-equiv="Content-Language" content="es">
	<Title>Resumen de Matriculados y PreMatriculados por Ciclo Académico</title>
    <style>
<!--
.usattitulo  { font-size: 14pt; font-weight: bold }
.etabla      { font-weight: bold; text-align: center; background-color: #DFDBA4 }
-->
    </style>
</head>
<body>
<p class="usattitulo">Resumen de Estudiantes matriculados y prematriculados</p>

<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="20%"><b>Ciclo Académico</b></td>
    <td width="25%" align="center">
    <%=descripcion_cac%>
    </td>
  </tr>
</table>
<br>
<table style="width: 100%;border-collapse: collapse" border="1" cellpadding="3" cellspacing="0" bordercolor="#666666">
	<tr class="etabla">
		<td rowspan="2">Escuela Profesional</td>
		<td colspan="3">ASIGNATURAS DE ESCUELA</td>
		<td colspan="3">ASIGNATURAS COMPLEMENTARIAS</td>
		<td rowspan="2">Sub Total</td>
	</tr>
	<tr class="etabla">
		<td style="width: 10%" >Prematriculados</td>
		<td style="width: 10%" >Matriculados</td>
		<td style="width: 10%" >Total</td>
		<td style="width: 10%" >PreMatriculados</td>
		<td style="width: 10%" >Matriculados</td>
		<td style="width: 10%" >Total</td>
	</tr>
<%
T1=0:T2=0:T3=0:T4=0
Do while not rsResumen.EOF

	rsubTotal=0
	totalEscuela=rsResumen("preescuela") + rsResumen("matescuela")
	totalCompl=rsResumen("precompl") + rsResumen("matcompl")
	subTotal=totalPre+totalMat
	
	T1=T1+rsResumen("preescuela")
	T2=T2+rsResumen("matescuela")
	T3=T3+rsResumen("precompl")
	T4=T4+rsResumen("matcompl")

	rtotalPre=rtotalPre+totalEscuela
	rtotalMat=rtotalMat+totalCompl
	rtotal=rtotalPre+rtotalMat
%>	
	<tr>
		<td style="width: 40%"><%=rsResumen("nombre_cpf")%>&nbsp;</td>
		<td style="width: 10%" align="center" onmouseover="Resaltar(1,this,'S')" onmouseout="Resaltar(0,this,'S')" onclick="AbrirEscuela('PE','<%=rsResumen("codigo_cpf")%>','<%=rsResumen("preescuela")%>')"><%=rsResumen("preescuela")%>&nbsp;</td>
		<td style="width: 10%" align="center" onmouseover="Resaltar(1,this,'S')" onmouseout="Resaltar(0,this,'S')" onclick="AbrirEscuela('ME','<%=rsResumen("codigo_cpf")%>','<%=rsResumen("matescuela")%>')"><%=rsResumen("matescuela")%>&nbsp;</td>
		<td style="width: 10%" align="center" class="azul" ><%=totalEscuela%>&nbsp;</td>
		<td style="width: 10%" align="center" onmouseover="Resaltar(1,this,'S')" onmouseout="Resaltar(0,this,'S')" onclick="AbrirEscuela('PC','<%=rsResumen("codigo_cpf")%>','<%=rsResumen("precompl")%>')"><%=rsResumen("precompl")%>&nbsp;</td>
		<td style="width: 10%" align="center" onmouseover="Resaltar(1,this,'S')" onmouseout="Resaltar(0,this,'S')" onclick="AbrirEscuela('MC','<%=rsResumen("codigo_cpf")%>','<%=rsResumen("matcompl")%>')"><%=rsResumen("matcompl")%>&nbsp;</td>
		<td style="width: 10%" align="center" class="azul"><%=totalCompl%>&nbsp;</td>
		<td style="width: 10%" align="center" class="etabla"><%=totalEscuela+totalCompl%>&nbsp;</td>
	</tr>
	<%rsResumen.movenext
Loop

Set rsResumen=nothing
%>	
	<tr class="etabla">
		<td style="width: 10%" align="center">TOTAL</td>
		<td style="width: 10%" align="center"><%=T1%>&nbsp;</td>
		<td style="width: 10%" align="center"><%=T2%>&nbsp;</td>
		<td style="width: 10%" align="center"><%=rtotalPre%>&nbsp;</td>
		<td style="width: 10%" align="center"><%=T3%>&nbsp;</td>
		<td style="width: 10%" align="center"><%=T4%>&nbsp;</td>
		<td style="width: 10%" align="center"><%=rtotalMat%>&nbsp;</td>
		<td style="width: 10%" align="center"><%=rtotal%>&nbsp;</td>
	</tr>
</table>
<p><strong><em>Fecha de actualización:&nbsp;<%=now%></em></strong></p>
</body>
</html>