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
    <td align="right" valign="bottom"><input type="button" value="Exportar" name="cmdExportar" id="cmdexportar" class="boton" onClick="EnviarDatos('exportarIdiomas.asp?id=<% response.write(request.querystring("id")) %>')"></td>
  </tr>
</table>

<!--
<table width="100%" border="1">
  <tr>
    <th>Month</th>
    <th>Savings</th>
    <th>Savings for holiday!</th>
  </tr>
  <tr>
    <td rowspan="2">$50</td>
    <td>January</td>
    <td>$100</td>
  </tr>
  <tr>
    <td>February</td>
    <td>$80</td>
  </tr>
  <tr>
    <td>February</td>
    <td>$80</td>
    <td>$80</td>
  </tr>
</table>
-->

<table width="100%" align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111">
        <!-- Cabecera de la Tabla Jerarquica -->
		<tr class="etabla"> 
		  <td rowspan="3">Item</td>
		  <td rowspan="3">Docente</td>
		  <td rowspan="3">Idioma</td>
		  <td rowspan="3">Centro de estudios</td>
		  <td rowspan="3">Año de graduación</td>
		  <td align="center" colspan="9">Nivel de conocimiento del idioma</td>
	    </tr>
		<tr class="etabla">
			<td align="center" colspan="3">Lee</td>
			<td align="center" colspan="3">Habla</td>
			<td align="center" colspan="3">Escribe</td>
		</tr>
		<tr class="etabla">
			<td align="center">Alto</td>
			<td align="center">Medio</td>
			<td align="center">Bajo</td>
			<td align="center">Alto</td>
			<td align="center">Medio</td>
			<td align="center">Bajo</td>
			<td align="center">Alto</td>
			<td align="center">Medio</td>
			<td align="center">Bajo</td>
		</tr>
		<!-- Fin de la Cabecera de la Tabla Jerarquica -->
	<!-- Inicio de Docentes con investigaciones-->
		<%
		Dim objIdiomas
		Dim rsIdiomas
		Dim intcodigo_per
		Dim intcodigo_ctf
		'**********************************
		'intcodigo_per= session("codigo_per")
		'*********************************************************************
		intcodigo_dac= request.querystring("id") 'session("codigo_dac")
		intcodigo_ctf = request.querystring("ctf") ' 001-JR
		'Set objIdiomas=Server.CreateObject("PryUSAT.clsDatIdiomas") ' 001-JR
		Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos") ' 001-JR
        Obj.AbrirConexion
			
		Set rsIdiomas=Server.CreateObject("ADODB.Recordset")	
		
        set rsIdiomas=Obj.Consultar("ConsultarIdiomas_v2","FO","TH",intcodigo_dac,intcodigo_ctf) ' 001-JR
        'set rsIdiomas=Obj.Consultar("ConsultarIdiomas","FO","TH",intcodigo_dac) ' 001-JR        
			
		NumReg=rsIdiomas.recordcount
		'bytContar=0 
		Dim intItem
		
		Dim codigo_com
		codigo_com = 0
		intItem=0
		Dim cod_pe
		cod_pe = 0
		Dim nro_cod_pe
		nro_cod_pe = 0
		
		do while not rsIdiomas.EOF 
		intItem=intItem+1
			'intcodigo_per = rsDocente("codigo_per")%>
		<tr class="Nivel0"> 
    
            <td align="center" width="1%" ><%=intItem%></td>
            <td width="45%"><%=rsIdiomas("docente")%></td>
			
			<td width="25%"><%=rsIdiomas("descripcion_Idi")%></td>
			<%if rsIdiomas("codigo_Ins")=1 then%>
				<td width="35%" align="center"><%=rsIdiomas("centroestudios")%></td>
			<%else%>
				<td width="35%" align="center"><%=rsIdiomas("nombre_Ins")%></td>
			<%end if%>
			<td width="10%" align="center"><%=rsIdiomas("aniograduacion")%></td>
			<%select case rsIdiomas("lee")
				case "0"%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center">X</td>
				<%case "1"%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center">X</td>
					<td width="10%" align="center"></td>
				<%case "2"%>
					<td width="10%" align="center">X</td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
				<%case else%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
			<%end select
			select case rsIdiomas("habla")
				case "0"%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center">X</td>
				<%case "1"%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center">X</td>
					<td width="10%" align="center"></td>
				<%case "2"%>
					<td width="10%" align="center">X</td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
					<%case else%>
				<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
			<%end select
			select case rsIdiomas("escribe")
				case "0"%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center">X</td>
				<%case "1"%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center">X</td>
					<td width="10%" align="center"></td>
				<%case "2"%>
					<td width="10%" align="center">X</td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
				<%case else%>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
					<td width="10%" align="center"></td>
			<%end select%>

		</tr>
	<% rsIdiomas.movenext
	loop%>
	</table>	





  <% rsIdiomas.close
	 Set rsIdiomas=Nothing
	 Set objIdiomas=Nothing%>
</body>
</html>

