<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>

<html>
<head>
	<title>Consultar Grados académicos del profesor</title>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
	<script language="JavaScript" src="../../../private/funciones.js"></script>
	<script>
	function EnviarDatos(pagina){
		//var ncpf=cboescuela.options[cboescuela.selectedIndex].text
		//var ncac=cbocicloacademico.options[cbocicloacademico.selectedIndex].text
		
		location.href=pagina //+ "?idDepartamento=" + intcodigo_Dac.value 
		//+ "&idcicloacademico=" + cbocicloacademico.value + "&descripcion_cpf=" + ncpf + "&descripcion_cac=" + ncac
	}
</script>
<link href="../../../private/estilo.css" rel="stylesheet" type="text/css">
</head>
<body>
<table width="100%" border="0">
  <tr>
    <td align="right" valign="bottom"><input type="button" value="Exportar" name="cmdExportar" id="cmdexportar" class="boton" onClick="EnviarDatos('exportarTituloProf.asp?id=<% response.write(request.querystring("id")) %>&ctf=<% response.write(request.querystring("ctf")) %>')"></td>
  </tr>
</table>
<table width="100%" align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111">
		<tr class="etabla"> 
		  <td rowspan="2">Item</td>
		  <td rowspan="2">Profesor</td>
		  <td rowspan="2">Título</td>
		  <td rowspan="2">Institución</td>
		  <td rowspan="2">Situación</td>
		  <td align="center" class="etabla" colspan="3">Años</td>
		  <tr class="etabla">  <td align="center">Ingreso</td> <td align="center">Egreso</td> <td align="center">Titulación</td></tr>
	    </tr>
	   	
		<%
		Dim objTitulos
		Dim rsTitulos
		'Dim intcodigo_per
		
		intcodigo_ctf = request.QueryString ("ctf") 'treyes 09/01/2018
		intcodigo_dac = request.querystring("id") 'session("codigo_Dac")
		
		'Set objTitulos=Server.CreateObject("PryUSAT.clsDatTituloProfesional")
		'Set rsTitulos=Server.CreateObject("ADODB.Recordset")
		'Set rsTitulos= objTitulos.ConsultarTituloProfesional ("RS","TH",intcodigo_dac)
		
		Set objTitulos=Server.CreateObject("PryUSAT.clsAccesoDatos") 'treyes 09/01/2018
		objTitulos.AbrirConexion 'treyes 09/01/2018 
        Set rsTitulos=Server.CreateObject("ADODB.Recordset") 'treyes 09/01/2018
        Set rsTitulos=objTitulos.Consultar("ConsultarTituloProfesional_V2","FO",intcodigo_dac,intcodigo_ctf) 'treyes 09/01/2018

		NumReg=rsTitulos.recordcount
		'bytContar=0 
		Dim intItem
		intItem=0
		do while not rsTitulos.EOF 
		intItem=intItem+1%>
		
		<tr class="Nivel0" style="cursor:hand"> 
			<td align="center" width="1%"><%=intItem%></td>
			<td width="30%"><%=rsTitulos("docente")%>&nbsp;</td>
			<td width="20%" align="center"><%=rsTitulos("descripcion_Tpf")%></td>
			<td width="10%" align="center"><%=rsTitulos("nombre_ins")%></td>
			<td width="20%" align="center"><%=rsTitulos("descripcion_Sit")%></td>
			<td width="10%" align="center"><%=rsTitulos("anioIngreso_Tpr")%></td>
			<td width="10%" align="center"><%=rsTitulos("anioEgreso_Tpr")%></td>
			<td width="10%" align="center"><%=rsTitulos("anioGrad_Tpr")%></td>
	</tr>
	<% rsTitulos.movenext
	loop%>
	</table>
	<% rsTitulos.close
	 Set rsTitulos=Nothing
	 Set objTitulos=Nothing%>

</body>
</html>


