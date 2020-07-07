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
    <td align="right" valign="bottom"><input type="button" value="Exportar" name="cmdExportar" id="cmdexportar" class="boton" onClick="EnviarDatos('exportarInvestigaciones.asp')"></td>
  </tr>
</table>

<table width="100%" align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111">
        <!-- Cabecera de la Tabla Jerarquica -->
		<tr class="etabla"> 
		  <td>Item</td>
		  <td>Docente</td>
		  <td>Título</td>
		  <td>Tipo</td>
		  <td>Inicio</td>
		  <td>Término</td>
		  <td>Estado</td>
		  <td>Publicación.</td>
		  <td>Fecha Publicación</td>
		  <td>Medio de Publicación</td>
		  <td>Tipo de Publicación</td>
	    </tr>
		<%
		
		Dim objInvPub
		Dim rsInvPub
		Set objInvPub=Server.CreateObject("PryUSAT.clsDatInvestigacion")
		Set rsInvPub=Server.CreateObject("ADODB.Recordset")
		Set rsInvPub= objInvPub.ConsultarInvestigacionPublicacion ("RS","TO","")
		
		Dim objInvestigacion
		Dim rsInvestigacion
		'Dim intcodigo_per
		intcodigo_dac=session("codigo_Dac")
		Set objInvestigacion=Server.CreateObject("PryUSAT.clsDatInvestigacion")
		Set rsInvestigacion=Server.CreateObject("ADODB.Recordset")
		Set rsInvestigacion= objInvestigacion.ConsultarInvestigacion ("RS","DE",intcodigo_dac)
		NumReg=rsInvestigacion.recordcount
		Dim intItem
		intItem=0
		do while not rsInvestigacion.EOF 
		intItem=intItem+1%>
		<tr class="Nivel0"> 
			<td align="center" width="1%"><%=intItem%></td>
			<td width="80%"><%=rsInvestigacion("docente")%>&nbsp;</td>
			<td width="20%" align="center"><%=rsInvestigacion("Titulo_Inv")%></td>
			<td width="10%" align="center"><%=rsInvestigacion("descripcion_tin")%></td>
			<td width="20%" align="center"><%=rsInvestigacion("fechaini_inv")%></td>
			<td width="10%" align="center"><%=rsInvestigacion("fechafin_inv")%></td>
			<td width="20%" align="center"><%=rsInvestigacion("descripcion_ein")%></td>
			<%rsInvPub.movefirst
			do while not rsInvPub.eof
				Dim mostrar
				if rsInvestigacion("codigo_Inv")=rsInvPub("codigo_Inv") then
					mostrar="SI"
				else
					mostrar="NO"
				end if
				'response.Write("<td>inv.codigo= "&rsInvestigacion("codigo_Inv")&" invpub.codigo= "&rsInvPub("codigo_Inv")&"</td>")
			rsInvPub.movenext
			loop%>
			<td width="10%" align="center"><%=mostrar%></td>
			<td width="10%" align="center"><%=rsInvestigacion("fecha_pub")%></td>
			<td width="10%" align="center"><%=rsInvestigacion("descripcion_mpu")%></td>
			<td width="10%" align="center"><%=rsInvestigacion("descripcion_tpu")%></td>
	</tr>
	<% rsInvestigacion.movenext
	loop%>
	</table>
  <% rsInvestigacion.close
	 Set rsInvestigacion=Nothing
	 Set objInvestigacion=Nothing%>

</body>
</html>

