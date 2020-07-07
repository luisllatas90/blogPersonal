 <style>
<!--
.titulo      { color: #000080; font-weight: bold; text-align: center }
-->
    </style>
<%
Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=" & "Presupuesto Total" & ".xls"


Dim bytContar
Dim strAno
Dim strTotalGeneral
Dim strTipoConsulta
Dim seleccionar1
Dim seleccionar2


strAno=request.querystring("ano")
strTipoConsulta=Request.QueryString("tc")


'Creamos Objetos
Set objPresupuesto = Server.CreateObject("PryUSAT.clsDatPresupuesto")
Set rsTotalEmpresa=Server.CreateObject("ADODB.Recordset")
Set rsPresupuesto=Server.CreateObject("ADODB.Recordset")
Set rsCentroCosto=Server.CreateObject("ADODB.Recordset")




Set rsTotalEmpresa= objPresupuesto.ConsultarConsolidadoPresupuesto("RS","TO",strAno,strTipoConsulta,"","")

IF rsTotalEmpresa.recordcount >0 then
		
	strEgresoAnual= formatNumber(rsTotalEmpresa("totalEgreso"))
	strIngresoAnual=formatNumber(rsTotalEmpresa("totalIngreso"))
	strDiferencia=formatNumber(rsTotalEmpresa("diferencia"))
	strMoneda=rsTotalEmpresa("moneda")
else
	strEgresoAnual="S/. 0.00"
	strIngresoAnual="S/. 0.00"
	strMoneda="S/."
end if

'---------------Funcion Para Totales---------------
function TotalConsolidado(tipo,ano, param1,param2,param3)
 
	Set rsPresupuesto= objPresupuesto.ConsultarConsolidadoPresupuesto("RS",tipo,ano,param1,param2,param3)
		if rsPresupuesto.recordcount >0 then
			TotalConsolidado= formatNumber(rsPresupuesto("total"))

		else
			TotalConsolidado="0.00"
		end if
end function
'-------------Fin de Fucnion ---------------------------
%>


<!--Empezamos a Construir la Estructura Jerarquica  -->

<%IF trim(strAno)<>"" then %>
    <!--I1: Creamos Tabla que Contendra Toda la Estructura --> 
	<table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
		  
		  <!-- Cabecera de la Tabla de Estructura-->
		    <tr class="titulo"> 
			    <td height ="65" colspan=6><font size="5">Total Presupuestado Para El Año - <%= strAno%></font></td>
		   </tr>  

		  <tr align="center" class="nivel2">
		  	<td colspan="2" align="right"><b>Total (<%=strMoneda%>)</b></td>
			<td><%=strIngresoAnual%>&nbsp;</td>
			<td><b><%=strEgresoAnual%>&nbsp;</b></td>
			<td><%=strDiferencia%>&nbsp;</td>
			<td align="center">&nbsp;</td>
		  </tr>
		  
		  <tr> 
		  
		  
		    <td bgcolor="#C0C0C0">Item</td>
		    <td bgcolor="#C0C0C0">Descripcion</td>
		    <td bgcolor="#C0C0C0">Ingresos (<%=strMoneda%>)</td>
		    <td bgcolor="#C0C0C0">Egresos (<%=strMoneda%>)</td>
		    <td bgcolor="#C0C0C0">Diferencia (<%=strMoneda%>)</td>
			<td width="1%" bgcolor="#C0C0C0">Est</td>
			
		  </tr>
		  <!-- Fin de Cabecera de la Tabla de Estructura-->
		  

		   <% 		   
			
			
			
			Set rsCentroCosto= objPresupuesto.ConsultarRubrosPresupuesto("RS","CC",strAno,strTipoConsulta,"","")
		   %>

            <!-- Iniciamos el Bucle de Centro de Costos   --> 
			
			  <%
			  contar=0
			  Do while Not rsCentroCosto.eof   
			  	contar = contar + 1
				lngCodigo_Cco = rsCentroCosto("codigo_Cco")
				strEstado_Cco=TRIM(rsCentroCosto("estado_Pto"))
				strCentro=trim(rsCentroCosto("descripcion_Cco"))
							   
				'Calcular Total Por Centro de Costos
				totalEgreso=formatNumber(rsCentroCosto("TotalEgreso"))
				totalIngreso=formatNumber(rsCentroCosto("TotalIngreso"))
				diferencia=formatNumber(totalIngreso - totalEgreso)
				 %>
			 	 
			 	 <tr class="Nivel0"> 
 			        <td align="center" width="1%"><%=contar%>&nbsp;</td>
					 <td width="50%" ><%=strCentro%>&nbsp;</td>
					 <td width="15%" align="right"><%=totalIngreso%>&nbsp;</td>
					 <td width="15%" align="right"><%=totalEgreso%>&nbsp;</td>
					 <td width="15%" align="right"><%=diferencia%>&nbsp;</td>
					  <%if strEstado_Cco="P" then%>
						 <td align="center">NA</td>
						
					 <%else%>
						 <td align="center">AP</td>
					 
				     <%end if%>

				 </tr>			

			    <%rsCentroCosto.movenext
		      Loop 
		 
		     %>	
		<!-- Fin  del Bucle de Centro de Costos   --> 
   </table>
   <!--F1: Cerramos Tabla que Contiene Toda la Estructura  -->
<%End if

'Creamos Objetos
Set objPresupuesto = Nothing
Set rsTotalEmpresa=Nothing
Set rsPresupuesto=Nothing
Set rsCentroCosto=Nothing



%>