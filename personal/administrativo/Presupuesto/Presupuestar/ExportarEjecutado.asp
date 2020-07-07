<%
Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=" & "PedAlmacen" & ".xls"
%>

<html>
<head>
<title>Presupuesto Ejecutado</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
    <style>
<!--
.etabla   	{ color:#FFFFFF; font-weight: bold; background-color: #004E98; text-align:center }
.titulo      { color: #000080; font-weight: bold; text-align: center }
-->
    </style>

</head>

<body>
<%
strBandera="0"
lngCodigo_Cco= Request.QueryString("cboCentroCosto") 
strAno_Pto= Request.QueryString("txtAno")  

%>

		 	
		  
		  	<%
		  	
		  	if trim(strAno_Pto) <>"" and  lngCodigo_Cco <> "" then
		
				Set objPresupuesto= Server.CreateObject("PryUSAT.clsAccesoDatos")
				objPresupuesto.AbrirConexion
					Set rsPresupuesto= Server.CreateObject("ADODB.RecordSet")
					set rsPresupuesto= objPresupuesto.consultar("AlmacenSQLConsolidadoSalidaPorArea","FO",lngCodigo_Cco,strAno_Pto)
				objPresupuesto.CerrarConexion
	
				if rsPresupuesto.recordcount >0 then
						contar = 0
						total = 0
						
						%>
						 <table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="960">
						 	<tr>
						 		<th align="center" colspan=4 class="titulo" width="952">
                                <font size="4">SALIDAS DE ALMACÉN AÑO: <%=strAno_Pto%></font></th>
						 	</tr>	
						 	<tr>
						 		<td colspan=4 width="952"><%response.write "Area: " & rsPresupuesto("descripciónAre")%> &nbsp;</td>	
						 	</tr>
							<tr>
								<td width="42" bgcolor="#C0C0C0" align="center" >
                                <b>ITEM</b></td>
								<td width="615" bgcolor="#C0C0C0" align="center" >
                                <b>DETALLE DE ARTICULO</b></td>
								<td width="107" bgcolor="#C0C0C0" align="center" >
                                <b>CANTIDAD TOTAL</b></td>
								<td width="167" bgcolor="#C0C0C0" align="center" >
                                <b>COSTO TOTAL</b></td>
							 </tr>
							
							<%DO WHILE NOT rsPresupuesto.EOF
								contar = contar +1
								%>
								
								<TR>	
									<td width="42" align="center"><%=contar%>&nbsp;</td>
									<td width="615"><%=TRIM(rsPresupuesto("detalleArticulo"))%>&nbsp;</td>
									<td width="107" align="center"><%=rsPresupuesto("cantidadTotal")%>&nbsp;</td>
									<td width="167" align="right"><%=formatNumber(TRIM(rsPresupuesto("subTotal")),4)%>&nbsp;</td>
								
								</TR>
							
							  <% total = total + rsPresupuesto("subTotal")
							  	rsPresupuesto.MOVENEXT
							LOOP%>
								<tr>	 
									<td colspan=2 width="664"> </td>
									<td width="107" bgcolor="#C0C0C0" align="right" >
                                    <b>TOTAL (S/. ) :</b></td>
									<td width="167" bgcolor="#C0C0C0" ><b><%=formatNumber(total,4)%></b><b>
                                    </b>
                                    <p align="right">&nbsp;</td>
								<table>

				<%end if
		
			end if

		  	%>
	    
</body>
</html>