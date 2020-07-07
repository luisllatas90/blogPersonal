<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<html>
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
	<link rel="stylesheet" type="text/css" href="../private/estilo.css">
	<script language="JavaScript" src="../private/funciones.js"></script>
	<script language="JavaScript">
		function Buscar(){

		document.frmBuscarArticulo.action = "frmArticuloConcepto.asp?tip=C" 
		document.frmBuscarArticulo.submit();
		}

		function validaSubmite(){ 
						
		    if (document.frmArticuloConcepto.txtArt.value == "") 
			{
       		alert("Debe Ingresar La Descripcion del Concepto de Gasto") 
				frmArticuloConcepto.txtArt.focus();
				return (false);	

   			}

			//if (document.frmArticuloConcepto.cboCta.value == "0") 
			//{
       		//alert("Debe Seleccionar La Cuenta Contable") 
				//frmArticuloConcepto.cboCta.focus();
				//return (false);	

   			//}

   	      	return (true);	

		}
		</script>

	
</head>
<body>
	<!-- Encabezado de Pagina-->

	<table align="center" width="80%">
	  <tr>
    	  <th align="center" class="table">CONCEPTOS DE GASTOS</th>
	  </tr>
	  <tr>
		<td><hr></td>
	  </tr>
	</table>
	<!-- Fin de Encabezado de Pagina -->

	<!-- Menu de Opciones -->
	<table width="80%" align="center">
		<tr>
			<td width="22%" align="left"><A href="frmArticuloConcepto.asp?tip=N" style="text-decoration:none" ><img border="0" src="../images/nuevo.gif" align="baseline" width="16" height="16"> Nuevo Concepto de Gasto</A></td>
			<td width="30%" align="left"><A href="frmArticuloConcepto.asp?tip=C" style="text-decoration:none" ><img border="0" src="../images/previo.gif" align="baseline" width="16" height="16"> 
            Consultar Conceptos de Gastos</A></td>
			<td width="27%">&nbsp;</td>
		</tr>
		<tr>
			<td colspan="3" width="80%">
            --------------------------------------------------------------------------------------</td>
		</tr>
	</table>
	<!-- Fin del Menu de Opciones-->

<%
Dim objArticulo
Dim rsArticulo
Dim strTipo
Dim strTipoBus
Dim strParam

Dim lngCodigo_Art
Dim strDescripcion_Art
Dim dblPrecioUnitario_Art 
Dim lngCodigo_Pco 

strTipoBus= request.form("cboTipoBus")
strParam= request.form("txtParametro")

strTipo=trim(Request.QueryString("tip"))
lngCodigo_Art=trim(Request.QueryString("codArt"))


 if Trim(strTipo)="C" then%>


		<!-- Parametro de Busqueda de Articulo -->
		<form name="frmBuscarArticulo" method="post" action="">
			<table align="center" width="80%" border="0">
				<tr>
					<td width="20%" class="etabla2">Buscar Articulo Según:</td>
					<td width="70%" align="left" colspan="4">
						
						<select name="cboTipoBus">
						  <% 
							SNC=""	  
							SDC=""			  
							
							if trim(strTipoBus)="NC" THEN 
								 SNC="Selected"
								 
							else
								SDC="Selected"

							end if		 
						  %>	
						    <option value="DC" <%=SDC%>>Descripcion de Concepto</option>
						    <option value="NC" <%=SNC%>>Numero de Cuenta</option>
						</select>
					</td>
					
					<tr>
						<td width="20%" class="etabla2">Parámetro de Consulta:</td>
						<td width="70%"  colsPan="4" align="left">
                        <input type="text" name="txtParametro" value="<%=strParam%>" size="20">&nbsp;<input type="button" class="buscar" name="cmdBuscar" value="" onClick="Buscar()"></td>
					    
					</tr>
					
			</table>

		</form>
		
		<!--Fin de Parametro de Busqueda -->



	<%
	
 	IF strTipoBus <>"" and strParam<>"" THEN 
	
		Set objArticulo= Server.CreateObject("PryUSAT.clsDatArticuloConcepto")
		Set rsArticulo= Server.CreateObject("ADODB.RecordSet")
		set rsArticulo = objArticulo.ConsultarArticuloConcepto("RS",strTipoBus,strParam,"") 	
		
		if rsArticulo.recordcount >0 then%>

    		<form method="post" action="Procesos.asp?Tipo=D004" id="frmEliminarArticulo" name="frmEliminarArticulo">
		 		<table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="80%">
					<tr class="etabla">
					  <td width="40%" align="left">Descripción</td>
					  
					  <td width="8%" align="right">Precio(S/.)</td>
					  <td width="10%">Cuenta</td>
					  <td width="30%" align="left">Descripción de Cuenta</td>
					  <td width="5%"><input type="checkbox" name="chkmarcartodo" onClick="if (this.checked) {SeleccionarTodos(chkEliminar)} else {QuitarTodos(chkEliminar)}" value="ON" ><input type="Submit" name="cmdEliminar" value="Elim." class=""></td> 
					</tr>

				<%do while not rsArticulo.eof
					lngCodigo_Art=trim(rsArticulo("codigo_Art")) 
					
					
					%>
		            <a href="frmArticuloConcepto.asp?codArt=<%=lngCodigo_Art%>&tip=E">
				    <tr align="center" onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)" style="cursor:hand">
		        		<td align="left"><%=trim(rsArticulo("descripcion_Art"))%>&nbsp;</td>
					
					
						<td align="right"><%=trim(rsArticulo("precioUnitario_Art"))%>&nbsp;</td>
						<td><%=trim(rsArticulo("numeroCuenta_Pco"))%>&nbsp;</td>
						<td align="left"><%=trim(rsArticulo("descripcionCuenta_Pco"))%>&nbsp;</td>
						<td><input type="checkbox" name="chkEliminar" value="<%=lngCodigo_Art%>"></td>
		    
				    </tr>
					</a>
					<%rsArticulo.movenext
				loop%>
		 	  </table>			
		   </form>	
  		<%else%>
  			<table width="80%" align="center">
  				<tr align="center">
  					<td>No se encontró ningún registro para los valores especificados</td>
  				</tr>
  			</table>
  		<%end if

		rsArticulo.Close 
		Set rsArticulo=Nothing
    	set objArticulo=Nothing
   end if
end if

if Trim(strTipo)="N" OR Trim(strTipo)="E" then

		if strTipo="E" THEN

			Set objArticulo= Server.CreateObject("PryUSAT.clsDatArticuloConcepto")
			Set rsArticulo= Server.CreateObject("ADODB.RecordSet")
			set rsArticulo = objArticulo.ConsultarArticuloConcepto("RS","CO",lngCodigo_Art,strTipo) 	
			
			if rsArticulo.recordcount >0 then

				blnFuncion_Art=trim(rsArticulo("Funcion_Art"))
				strDescripcion_Art=trim(rsArticulo("Descripcion_Art"))
				dblPrecioUnitario_Art =formatNumber(trim(rsArticulo("PrecioUnitario_Art")))
				lngCodigo_Pco =trim(rsArticulo("Codigo_Pco"))

			end if
			rsArticulo.Close 
			Set rsArticulo=Nothing
			set	objArticulo=Nothing
			
			accion="Procesos.asp?Tipo=U003"	
		else
		
			accion="Procesos.asp?Tipo=I004" 

		END IF
%>
   <br>

    <table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="80%">
   	     <form  onSubmit="return validaSubmite()" method="post" action="<%=accion%>" id="frmArticuloConcepto" name="frmArticuloConcepto">
			  <input type="hidden" name="txtTip" value="<%=strTipo%>">
			  <input type="hidden" name="txtCodArt" value="<%=lngCodigo_Art%>">
			  

				<tr>
					<td class="etabla">Descripción del&nbsp; Gasto:</td>
					<td>
                    <input Type"text" name="txtArt"  value="<%=strDescripcion_Art%>" size="60" maxlength="50"></td>
					
				</tr>

				
				<tr>
					<td class="etabla">Precio del  Gasto (S/.):</td>
					<td><input Type"text" name="txtPre" size="10" value="<%=dblPrecioUnitario_Art %>"></td>
					
				</tr>
				
				<tr>
					<td class="etabla">Cuenta Contable:</td>
					  <!-- Poner las Cuentas-->
					  
					    <td width="70%"> 
							<%
								Dim objCuenta
								Dim rsCuenta
								
								Set objCuenta= Server.CreateObject("PryUSAT.clsDatPlanContable")
								Set rsCuenta= Server.CreateObject("ADODB.RecordSet")
								Set rsCuenta= objCuenta.ConsultarPlanContable("RS","TO","","")
								
								%>
							<select name="cboCta" id="cboCta" >
							  <option value="0">---Seleccione Cuenta Contable--- </option>
							  <% do while not rsCuenta.eof 
										seleccionar="" 
									if (cint(lngCodigo_Pco )=rsCuenta(0)) then seleccionar="SELECTED " %>
							  <option value= "<%=rsCuenta(0)%>" <%=seleccionar%>> <%=rsCuenta("numeroCuenta_Pco") & " - " & rsCuenta("descripcionCuenta_Pco")%></option>
							  <% rsCuenta.movenext
									loop
									rsCuenta.Close
									Set rsCuenta=Nothing 
									set objCuenta= Nothing
								   %>
							</select>
						
					  </td>

					  <!-- Fin de Cuentas-->
					
				</tr>
			
				<p>
				<table align="center" width="80%">
				<tr>
					  <td colspan="2" align="right">
			    		<input type="submit" value="Grabar" name="cmdGrabar" class="guardar">
					    <input type="button" value="Cancelar" name="cmdCancelar" class="salir"  onClick=location="frmArticuloConcepto.asp?tip=C">
					  </td>
			   	  
				</tr>
				</table>

	</form>
 </table>

<%end if%>

</body>
</html>