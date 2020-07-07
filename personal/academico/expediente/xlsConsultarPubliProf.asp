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
    <td align="right" valign="bottom"><input type="button" value="Exportar" name="cmdExportar" id="cmdexportar" class="boton" onClick="EnviarDatos('exportarPublicaciones.asp')"></td>
  </tr>
</table>
<table width="100%" align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111">
        <!-- Cabecera de la Tabla Jerarquica -->
		<tr class="etabla"> 
		  <td>Item</td>
		  <td>Docente</td>
		  <td>Título</td>
		  <td>Area de conocimiento</td>
		  <td>Tipo de publicación</td>
		  <td>Fecha de publicación</td>
		  <td>Medio de publicación</td>
		  <td>Observaciones</td>

	    </tr>
		<!-- Fin de la Cabecera de la Tabla Jerarquica -->
	<!-- Inicio de Docentes con investigaciones-->
		<%
		Dim objPublicacion
		Dim rsPublicacion
		Dim intcodigo_per
		
        Dim intcodigo_ctf ' 001-JR
				
		'**********************************
		'intcodigo_dac=5
		'*********************************************************************
		intcodigo_dac=session("codigo_dac")
        intcodigo_ctf = request.querystring("ctf") ' 001-JR
		'Set objPublicacion=Server.CreateObject("PryUSAT.clsDatPublicacion") ' 001-JR
		'Set rsPublicacion=Server.CreateObject("ADODB.Recordset") ' 001-JR
		'Set rsPublicacion= objPublicacion.ConsultarPublicacion ("RS","DE",intcodigo_dac) ' 001-JR
		
		Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos") 
		Obj.AbrirConexion
		Set rsPublicacion=Server.CreateObject("ADODB.Recordset")
		set rsPublicacion=Obj.Consultar("ConsultarPublicacion_v2","FO","DE",intcodigo_dac,intcodigo_ctf)
		'set rsPublicacion=Obj.Consultar("ConsultarPublicacion_v2","FO","DE",intcodigo_dac)
		
		NumReg=rsPublicacion.recordcount
		
		'bytContar=0 
		Dim intItem
		intItem=0
		do while not rsPublicacion.EOF 
		intItem=intItem+1
			'intcodigo_per = rsDocente("codigo_per")%>
		<tr class="Nivel0" style="cursor:hand"> 
			<td align="center" width="1%"><%=intItem%></td>
			<td width="55%"><%=rsPublicacion("docente")%></td>
			<td width="55%"><%=rsPublicacion("titulo_Pub")%></td>
			<td width="25%" align="center"><%=rsPublicacion("descripcion_Aco")%></td>
			<td width="10%" align="center"><%=rsPublicacion("descripcion_Tpu")%></td>
			<td width="10%" align="center"><%=rsPublicacion("fecha_Pub")%></td>
			<td width="10%" align="center"><%=rsPublicacion("descripcion_Mpu")%></td>
			<td width="10%" align="center"><%=rsPublicacion("observaciones_Pub")%></td>
	</tr>
	<% rsPublicacion.movenext
	loop%>
	</table>
	
  <% rsPublicacion.close
	 Set rsPublicacion=Nothing
	 Set objPublicacion=Nothing%>
</body>
</html>

