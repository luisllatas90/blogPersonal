<!--#include file="../../../../funciones.asp"-->
<%

codigo_cac=request.querystring("codigo_cac")
if codigo_cac="" then codigo_cac=session("codigo_cac")

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
	<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
	<link rel="stylesheet" type="text/css" href="../../../../private/estiloimpresion.css" media="print">		
	<script language="JavaScript" src="../../../../private/funciones.js"></script>
	<script language="javascript">
	function AbrirEscuela(tipo,codigo_cpf,valor)
	{
			if (valor>0){
				var fila=window.event.srcElement.parentElement;
				if (fila.tagName == "TR"){
					var nombre_cpf=fila.getElementsByTagName('td')[0].innerText
					var codigo_cac=cbocodigo_cac.value
					
					var pagina="../academico/matricula/consultapublica/lstestudiantesresumen.asp??nombre_cpf=" + nombre_cpf + "&codigo_cpf=" + codigo_cpf + "&tipo=" + tipo + "&codigo_cac=" + codigo_cac
				}
			}
			
			window.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
	}
	
	function ConsultarMatricula()
	{
		window.location.href="../../../aplicacionweb2/cargando.asp?rutapagina=../academico/matricula/consultapublica/totmatriculaciclo.asp?codigo_cac="+ cbocodigo_cac.value
	}
	
	function ExportarRpte()
	{
		window.location.href="xlstotmatriculaciclo.asp?codigo_cac=" + cbocodigo_cac.value + "&descripcion_cac=" + cbocodigo_cac.options[cbocodigo_cac.selectedIndex].text
	
	}
	</script>	
	
</head>

<body>
<p class="usattitulo">Resumen de Estudiantes matriculados y prematriculados</p>

<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="20%">Ciclo Académico</td>
    <td width="20%" align="center">
    <%call llenarlista("cbocodigo_cac","",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"Seleccione el ciclo académico","","")%>
    </td>
    <td width="60%" align="right" class="NoImprimir">
    <input class="buscar2" name="cmdbuscar" type="button" value="Buscar..." onclick="ConsultarMatricula()">
	<input class="imprimir2" name="cmdImprimir" type="button" value="Imprimir" onclick="imprimir('N','','')">
	<input class="excel2" name="cmdExportar" type="button" value="Exportar" onclick="ExportarRpte()">
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