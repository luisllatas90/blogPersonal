<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<html>
<head>
<script>
	function EnviarDatos(pagina){
		//var ncpf=cboescuela.options[cboescuela.selectedIndex].text
		//var ncac=cbocicloacademico.options[cbocicloacademico.selectedIndex].text
		
		location.href=pagina //+ "?idDepartamento=" + intcodigo_Dac.value 
		//+ "&idcicloacademico=" + cbocicloacademico.value + "&descripcion_cpf=" + ncpf + "&descripcion_cac=" + ncac
	}
</script>
<title>Consultar Grados académicos del profesor</title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
	<script language="JavaScript" src="../../../private/funciones.js"></script>
<link href="../../../private/estilo.css" rel="stylesheet" type="text/css">
</head>
<body>
<table width="100%" border="0">
  <tr>
    <td align="right" valign="bottom"><input type="button" value="Exportar" name="cmdExportar" id="cmdexportar" class="boton" onClick="EnviarDatos('exportarGrados.asp?id=<% response.write(request.querystring("id")) %>&ctf=<% response.write(request.querystring("ctf")) %>')"></td>
  </tr>
</table>

<table width="100%" align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111">
        <!-- Cabecera de la Tabla Jerarquica -->
		<tr class="etabla"> 
		  <td rowspan="2">Item</td>
		  <td rowspan="2">Profesor</td>
		  <td rowspan="2">Grado</td>
		  <td rowspan="2">TipoGrado</td>
		  <td rowspan="2">Mención</td>
		  <td rowspan="2">Situación</td>
		  <td rowspan="2">Institucion</td>
		  <td align="center" colspan="3">Años</td>
		  <tr class="etabla"><td>Ing.</td> <td>Egr.</td> <td>Grad.</td> </tr>
	    </tr>
		<!-- Fin de la Cabecera de la Tabla Jerarquica -->
	<!-- Inicio de Docentes con investigaciones-->
		<%
		Dim objGrados
		Dim rsGrados
		'Dim intcodigo_per
		intcodigo_dac= request.querystring("id") 'session("codigo_Dac")
		intcodigo_ctf = request.querystring("ctf") 'treyes 09/01/2018
		
		'Set objGrados=Server.CreateObject("PryUSAT.clsDatGrados")
		'Set rsGrados=Server.CreateObject("ADODB.Recordset")
		'Set rsGrados= objGrados.ConsultarGradoAcademico ("RS","TH",intcodigo_dac)
		
		Set objGrados=Server.CreateObject("PryUSAT.clsAccesoDatos") 'treyes 09/01/2018
		objGrados.AbrirConexion 'treyes 09/01/2018 
        Set rsGrados=Server.CreateObject("ADODB.Recordset") 'treyes 09/01/2018
        Set rsGrados=objGrados.Consultar("ConsultarGradoAcademico_V2","FO",intcodigo_dac,intcodigo_ctf) 'treyes 09/01/2018
		
		NumReg=rsGrados.recordcount
		'bytContar=0 
		Dim intItem
		intItem=0
		do while not rsGrados.EOF 
		intItem=intItem+1
			'intcodigo_per = rsDocente("codigo_per")%>
		<tr class="Nivel0" style="cursor:hand"> 
			<td align="center" width="1%"><%=intItem%></td>
			<td width="70%"><%=rsGrados("docente")%>&nbsp;</td>
			<td width="20%" align="center"><%=rsGrados("nombre_Gra")%></td>
			<td width="10%" align="center"><%=rsGrados("descripcion_TGr")%></td>
			<td width="20%" align="center"><%=rsGrados("mencion_GPr")%></td>
			<td width="10%" align="center"><%=rsGrados("descripcion_Sit")%></td>
			<td width="20%" align="center"><%=rsGrados("nombre_Ins")%></td>
			<td width="10%" align="center"><%=rsGrados("anioIngreso_GPr")%></td>
			<td width="10%" align="center"><%=rsGrados("anioEgreso_GPr")%></td>
			<td width="10%" align="center"><%=rsGrados("anioGrad_GPr")%></td>
	</tr>
	<% rsGrados.movenext
	loop%>
	</table>
  <% rsGrados.close
	 Set rsGrados=Nothing
	 Set objGrados=Nothing%>

</body>
</html>

