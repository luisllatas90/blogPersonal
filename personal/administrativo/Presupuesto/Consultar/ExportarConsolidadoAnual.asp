<%
Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=" & "Presupuesto Consolidado" & ".xls"


strAno=request.querystring("ano")
strTipo=Request.QueryString("tipo")
strEstado =Request.QueryString("estado")
total =Request.QueryString("total")
simbolo=Request.QueryString("simbolo")


Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
    
obj.AbrirConexion

%>
<html>
<head>
	<title>Consultar Detalle de Presupuesto</title>
	
</head>

<body>

<!-- Tabla que Contendra la Estructura Jerarquica-->
<table width="100%" align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111">
	<!-- Titulo de la Pagina-->
	  <tr>
		<th colspan=3 align="center">CONSOLIDADO DE PRESUPUESTOS APROBADOS  -  AÑO: <%=strAno%></th>
	  </tr>
	  <tr>
	 	 <td colspan=3><hr></td>
	  </tr>
	<!--Fin del Titulo -->

        <!-- Cabecera de la Tabla Jerarquica -->
		<tr> 
		  <td  bgcolor="#C0C0C0" align="center"><b>Ítem</b></td>
		  <td bgcolor="#C0C0C0" align="center" ><b>Descripción</b></td>
		  <td align =center bgcolor="#C0C0C0" >
	          <p align="right"><b>Total(<%=simbolo%>) : <%=formatNumber(total)%></b></td>
		</tr>
		<!-- Fin de la Cabecera de la Tabla Jerarquica -->
		
		<!-- Inicio de Conceptos-->
		<%
		Dim totalConcepto
		Dim objConcepto
		Dim rsConcepto
		Dim lngCodigo_Cie
	
	
		Set rsConcepto=Server.CreateObject("ADODB.Recordset")
		Set rsConcepto= obj.Consultar("ConceptosUtilizadosPresupuesto", "ST",strAno,strTipo,strEstado)

		bytContar=0 
		
		item=0
		do while not rsConcepto.EOF 
			lngCodigo_Cie = rsConcepto("codigo_Cie")
			totalConcepto= formatNumber(rsConcepto("total"))
			item=item+1
			%>
			
			<tr > 
				<td align="center" width="3%" bgcolor="#FFFFCC"><%=item%>&nbsp;</td>
				<td width="80%" bgcolor="#FFFFCC"><%=rsConcepto("descripcion_Cie") %>&nbsp;</td>
				<td width="17%" align="right" bgcolor="#FFFFCC"><%=TotalConcepto%>&nbsp;</td>
		 	</tr>
			
			
			
			<!-- Inicio de Cuentas-->
			<%
			Dim objPlan
			Dim rsPlan
			Dim TotalCuenta

			Set rsPlan=Server.CreateObject("ADODB.Recordset")
			Set rsPlan= obj.Consultar("CuentasUtilizadasPresupuesto","ST",strAno,strTipo,strEstado,lngCodigo_Cie)
			
			if rsplan.recordcount>0 then%>
				<tr>
				 
					<%
					do while not rsPlan.eof
						lngCodigo_Pco=rsPlan("codigo_Pco")
						bytContar=bytContar+1
						TotalCuenta=formatNumber(rsPlan("total"))
							
						%>		
						<td>&nbsp;</td>
						<td align="left"><%=trim(rsPlan("descripcionCuenta_Pco"))%>&nbsp;</td>
						<td align="right"><%=TotalCuenta%>&nbsp;</td>
							
				 </tr>		
						<%rsPlan.movenext
					Loop		
			end if
			 rsPlan.close
			 Set rsPlan=Nothing
			%>
			 
			 <!-- Fin de Cuentas-->
		
	       <%rsConcepto.movenext
		loop
		rsConcepto.close
		set rsConcepto= Nothing
		%>
		
		<!--Fin de Conceptos -->
		<%
obj.CerrarConexion
set obj= Nothing
		%>
</table>
<!-- Fin de la Tabla que Contendra la Estructura Jerarquica-->
</body>
</html>