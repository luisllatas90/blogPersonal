<%
Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=" & "Ejecutado" & ".xls"
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
strNivel = Request.QueryString("cboNivel")  

		  	
		  	if trim(strAno_Pto) <>"" and  lngCodigo_Cco <> "" then
		
				Set objPresupuesto= Server.CreateObject("PryUSAT.clsAccesoDatos")
				objPresupuesto.AbrirConexion
					Set rsPresupuesto= Server.CreateObject("ADODB.RecordSet")
					set rsPresupuesto= objPresupuesto.consultar("CompararPresupuestadoEjecutado","FO", strNivel, strAno_Pto,lngCodigo_Cco)
				objPresupuesto.CerrarConexion
	
				if rsPresupuesto.recordcount >0 then
						contar = 0
						totalPre = 0
						totalEje = 0

						%>
						 <table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="960" height="117">
						 	<tr>
						 		<td colspan=7 width="914" height="14"><%response.write "Area: " & rsPresupuesto("descripciónAre") & " - " & strAno_Pto%> </td>	
						 	</tr>
							<tr>
								<td width="39" class="etabla" rowspan="2" height="49">ITEM</td>
								<td width="652" class="etabla" rowspan="2" height="49">DETALLE DE ARTICULO</td>
								<td width="202" class="etabla" colspan="2" height="14">
                                PRESUPUESTADO</td>
								<td width="178" class="etabla" colspan="2" height="14">
                                EJECUTADO</td>
								<td width="112" class="etabla" rowspan="2" height="49">
                                DIFERENCIA</td>
							 </tr>
							
							<tr>
								<td width="90" class="etabla" height="28">CANT. TOTAL</td>
								<td width="112" class="etabla" height="28">TOTAL PRESUPUEST. 
                                (S/.)</td>
								<td width="78" class="etabla" height="28">CANT. TOTAL</td>
								<td width="100" class="etabla" height="28">TOTAL EJECUTADO 
                                (S/.)</td>
							 </tr>
							
							<%DO WHILE NOT rsPresupuesto.EOF
								contar = contar +1
								%>
								
								<TR onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)" style="cursor:hand">	
									<td width="39" align="center" height="13"><%=contar%></td>
									<td width="652" height="13"><%=TRIM(rsPresupuesto("descripcion_Art"))%></td>
									<td width="90" align="center" height="13"><%=rsPresupuesto("cantPre")%></td>
									<td width="112" align="center" height="13"><%=formatNumber(trim(rsPresupuesto("totalPre")),4)%></td>
									<td width="78" align="center" height="13"><%=rsPresupuesto("cantEje")%></td>
									<td width="112" align="center" height="13"><%=formatNumber(trim(rsPresupuesto("totalEje")),4)%></td>
									<td width="100" align="right" height="13"><%=formatNumber(TRIM(rsPresupuesto("diferencia")),4)%></td>
								</TR>
							
							  <% totalPre = totalPre + cdbl(rsPresupuesto("totalPre"))
							     totalEje = totalEje + cdbl(rsPresupuesto("totalEje"))

							  	rsPresupuesto.MOVENEXT
							LOOP%>
								<tr>	 
									<td colspan=2 width="698" height="14"> </td>
									<td class="etabla" width="90" height="14">TOTAL (S/. ) :</td>
									<td class="etabla" width="112" height="14"><%=formatNumber(totalPre,4)%>
                                    </td>
									<td class="etabla" width="78" height="14">TOTAL (S/.)</td>
									<td class="etabla" width="100" height="14"><%=formatNumber(totalEje,4)%> </td>
									<td class="etabla" width="112" height="14">
                                    <%=formatNumber((totalPre - totalEje),4)%></td>
								<table>

				<%else%>
					
						<p>
						<p>

						<table align="center"> 
						<tr><td><font color="#800000">No se ha encontrado presupuesto ejecutado para su centro de costos. 
                          </font></td></tr>
				<%end if
		
			end if

		  	%>
	    
</body>
</html>