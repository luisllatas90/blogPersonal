<%
'---------------------- Procesos (Insert, Update, Delete)-------------------------
Dim strTipo
strTipo=Request.QueryString("Tipo")

Select case strTipo

'-------------------------------- Agregar: Insert ------------------------------------

 Case "I001" 'frmPresupuesto.asp
	
	    'Dim lngCodigo_Pto, lngCodigo_Cco,strAno_Pto ,bytNumero_Pto, dtmFechaElaboracion_Pto, strObservacion_Pto,dblTotalGeneral_Pto,strMoneda_Pto,strEstado_Pto,dtmFechaInicio_Pto,	dtmFechaFin_Pto,strFormaDetalle_Pto, strTipo_Pto,objPresupuesto,rsPresupuesto,objParametro,rsParametro
		
		lngCodigo_Cco= Request.form("cboCentroCosto")
		
		strAno_Pto =Request.form("txtAno")
		bytNumero_Pto=Request.form("txtNroPre")
		dtmFechaElaboracion_Pto=Date()
		strObservacion_Pto=Request.form("txtObservacion")
		dblTotalGeneral_Pto="0"
		strMoneda_Pto=Request.form("cboMoneda")
		strEstado_Pto="P"
		dtmFechaInicio_Pto=Request.form("txtFechaIni")
		dtmFechaFin_Pto=Request.form("txtFechaFin")
		strFormaDetalle_Pto =Request.form("cboFormaDetalle")
		strTipo_Pto=Request.form("txtTipo_Pto")
	
		'-------- Hector: 03/12/07
		
		habilitado= cbool(0)
		Set cn= Server.CreateObject("PryUSAT.clsAccesoDatos")
		cn.AbrirConexion
		
		Set rs= Server.CreateObject("ADODB.RecordSet")
		set rs = cn.Consultar("VerificarPermisoPresupuesto","FO",lngCodigo_Cco,strAno_Pto)
		if rs.Eof <> true  And rs.Bof <> True then
			habilitado = cbool(rs("habilitado_Pto"))
		end if
		
		rs.Close
		set rs = nothing
		cn.CerrarConexion
		set cn = nothing
		
		'----------------------------------
				
		
	
			if cint(lngCodigo_Cco)=0 then
						ruta="frmPresupuesto.asp?tpto=" & strTipo_Pto & "&" & "msg=1"
						response.Redirect(ruta)
						
			else
					
					
					Set objParametro= Server.CreateObject("PryUSAT.clsDatPresupuesto")
					Set rsParametro= Server.CreateObject("ADODB.RecordSet")
					set rsParametro = objParametro.ConsultarParametroPresupuesto("RS",strAno_Pto) 	
				
					if rsParametro.recordcount >0 then
						
			
							Set objPresupuesto= Server.CreateObject("PryUSAT.clsDatPresupuesto")
							Set rsPresupuesto= Server.CreateObject("ADODB.RecordSet")
							set rsPresupuesto = objPresupuesto.BuscarPresupuesto("RS",lngCodigo_Cco,strAno_Pto,bytNumero_Pto,strTipo_Pto) 	
				
							if rsPresupuesto.recordcount >0 then
								'Ese Numero de Presupuesto del Año ya Existe Para Ese Centro de Costos
								lngCodigo_Pto = rsPresupuesto("codigo_Pto")
								strEstado_Pto= rsPresupuesto("estado_Pto")
							else
								'Verificar Si el Presupuesto Tiene Vigencia y se pueden regsitrar todavia presupuestos
								
								if cbool(rsParametro("estado_Pto"))=True or habilitado =true  then
								
									'Grabar el Presupuesto (Cabecera del Presupuesto)
									 lngCodigo_Pto = objPresupuesto.AgregarPresupuesto (lngCodigo_Cco, strAno_Pto, bytNumero_Pto, dtmFechaElaboracion_Pto, strObservacion_Pto, dblTotalGeneral_Pto, strMoneda_Pto, strEstado_Pto,dtmFechaInicio_Pto,dtmFechaFin_Pto,strFormaDetalle_Pto,strTipo_Pto)
								else
								
									ruta="frmPresupuesto.asp?tpto=" & strTipo_Pto & "&" & "msg=1" 
									response.Redirect(ruta)
									

								end if
				
							end if
							
							rsPresupuesto.Close 
							Set rsPresupuesto = Nothing
							Set objPresupuesto= Nothing
						
							'ruta="frmConsolidadoPresupuesto.asp?" & "id=" & lngCodigo_Pto  & "&Vig=" & trim(rsParametro("estado_Pto"))
							ruta="frmConsolidadoPresupuesto.asp?" & "id=" & lngCodigo_Pto  & "&Vig=" & habilitado
							response.Redirect(ruta)
	

					
					else
						ruta="frmPresupuesto.asp?tpto=" & strTipo_Pto & "&" & "msg=1"
						response.Redirect(ruta)
						
					
					end if

					rsParametro.close			
					Set rsParametro= Nothing
					Set objParametro=Nothing
					

			end if



Case "I002" 'frmRegistrarDetallePresupuesto: AgregarDetallePresupuesto

 
	lngCodigo_Pto= request.form("txtCodigo_Pto")
	lngCodigo_Art= request.form("txtCodigo_Art")

	dblPrecioUnitario= formatNumber(request.form("txtPrecio"))
	dblCantGlobal= formatNumber(request.form("txtCantGlobal"))
	dblTotalGlobal=formatNumber(dblPrecioUnitario*dblCantGlobal)

	periodo = split(request.Form("txtPeriodo"),",")
	ano= split(request.Form("txtAno"),",")
	cant = split(request.Form("txtCantidad"),",")
	subt = split(request.Form("txtSubTotal"),",")
	tipoArt= request.Form("txtTipoArt")

	Set objPresupuesto= Server.CreateObject("PryUSAT.clsDatPresupuesto")

	if cbool(tipoArt)=true then
		objPresupuesto.AgregarDetallePresupuesto lngCodigo_Pto,lngCodigo_Art,dblPrecioUnitario,dblCantGlobal,dblTotalGlobal
	else
		objPresupuesto.AgregarDetallePresupuesto lngCodigo_Pto,lngCodigo_Art,"1", dblTotalGlobal,dblTotalGlobal
	end if

	for x=0 to ubound(periodo) 
		if cbool(tipoArt)=true then
			Ct=trim(cant(x))
			
			if Ct="" then 
			  Ct="0"
			end if  
			objPresupuesto.AgregarDetalleEjecucion lngCodigo_Pto,lngCodigo_Art,trim(periodo(x)),cdbl(Ct),formatNumber(dblPrecioUnitario * cdbl(Ct)),trim(ano(x))		 
		
		 else
		 	objPresupuesto.AgregarDetalleEjecucion lngCodigo_Pto,lngCodigo_Art,trim(periodo(x)),formatNumber(dblPrecioUnitario * cdbl(Ct)),formatNumber(dblPrecioUnitario * cdbl(Ct)),trim(ano(x))
			
		 end if
	next 
		response.Redirect "frmConsolidadoPresupuesto.asp?id=" & lngCodigo_Pto


Case "I003" 'frmBuscarPresupuesto: ImportarPresupuesto

		'Dim lngCodigo_PtoN,strAno_PtoN,lngCodigo_Cco, strTipo_Pto,strAno_Pto,objPresupuesto

		lngCodigo_PtoN=request.form("txtCodigo_PtoN") 
		strAno_PtoN=request.form("txtAno_PtoN")
		lngCodigo_Cco=request.form("txtCodigo_Cco")
		strAno_Pto=request.form("txtAno_Pto")
		strTipo_Pto=request.form("txtTipo_Pto")


			Set objPresupuesto= Server.CreateObject("PryUSAT.clsDatPresupuesto")
			Set rsPresupuesto= Server.CreateObject("ADODB.RecordSet")
			set rsPresupuesto = objPresupuesto.BuscarPresupuesto("RS",lngCodigo_Cco,strAno_Pto,"1",strTipo_Pto) 	
			
				
			if rsPresupuesto.recordcount >0 then
				'Si existe Presupuesto a Importar
				
				Set objPresupuesto= Server.CreateObject("PryUSAT.clsDatPresupuesto")
				objPresupuesto.ImportarPresupuesto lngCodigo_PtoN,strAno_PtoN,lngCodigo_Cco,strAno_Pto,strTipo_Pto
				
				%>
				<script language="Javascript">
					window.opener.location.reload();
					window.close()	
				</script>
				
			<%else
				'No existe Presupuesto a importar
				
				ruta="frmBuscarPresupuesto.asp?cpton=" & lngCodigo_PtoN & "&anon=" & strAno_PtoN & "&cco=" &  lngCodigo_Cco &  "&tip=" & strTipo_Pto & "&men=0"
				Response.redirect(ruta)
			end if





'--------------------------------- Modificar:Update------------------
Case "U001" 'frmEditarDetallePresupuesto

		lngCodigo_Pto= request.form("txtCodigo_Pto")
		lngCodigo_Art= request.form("txtCodigo_Art")

		dblPrecioUnitario= formatNumber(request.form("txtPrecio"))
		dblCantGlobal= formatNumber(request.form("txtCantGlobal"))
		dblTotalGlobal=FormatNumber(dblPrecioUnitario*dblCantGlobal)

		periodo = split(request.Form("txtPeriodo"),",")
		ano= split(request.Form("txtAno"),",")
		cant = split(request.Form("txtCantidad"),",")
		subt = split(request.Form("txtSubTotal"),",")
		tipoArt= request.Form("txtTipoArt")

		Set objPresupuesto= Server.CreateObject("PryUSAT.clsDatPresupuesto")

		if cbool(tipoArt)=true then
		
			objPresupuesto.AgregarDetallePresupuesto lngCodigo_Pto,lngCodigo_Art,dblPrecioUnitario,dblCantGlobal,dblTotalGlobal
			
		else
			objPresupuesto.AgregarDetallePresupuesto lngCodigo_Pto,lngCodigo_Art,"1", dblTotalGlobal,dblTotalGlobal
			
		end if

		for x=0 to ubound(periodo) 
			if cbool(tipoArt)=true then
				Ct=trim(cant(x))
				
				if Ct="" then 
				  Ct="0"
				end if  
				objPresupuesto.AgregarDetalleEjecucion lngCodigo_Pto,lngCodigo_Art,trim(periodo(x)),cdbl(Ct),FormatNumber(cdbl(Ct)*dblPrecioUnitario),trim(ano(x))
				
			 else
				 objPresupuesto.AgregarDetalleEjecucion lngCodigo_Pto,lngCodigo_Art,trim(periodo(x)),FormatNumber(cdbl(Ct)*dblPrecioUnitario),FormatNumber(cdbl(Ct)*dblPrecioUnitario),trim(ano(x))
			 end if
		next 

			%>

					<script language="Javascript">
						window.opener.location.reload()
						window.close()
					</script>
			<%


'-------------------------------- Eliminar: Delete------------------------------------------------------

case "D001" 'frmConsolidadoPresupuesto: EliminarPresupuesto

    'Dim lngCodigo_Pto,strTipo_Pto
    
    lngCodigo_Pto= request.Form("txtcodigo_Pto")
    strTipo_Pto= request.Form("txtTipo_Pto")
	
	Set objPresupuesto= Server.CreateObject("PryUSAT.clsDatPresupuesto")

	objPresupuesto.EliminarPresupuesto lngCodigo_Pto
	
	Set objPresupuesto= Nothing

	ruta="frmPresupuesto.asp?tpto=" & strTipo_Pto 
	response.Redirect(ruta)

Case "D002" 'frmConsolidadoPresupuesto: EliminarDetallePresupuesto

    'Dim objPresupuesto,lngCodigo_Pto

	lngCodigo_Pto=Request.form("txtCod_Pto")

	Set objPresupuesto= Server.CreateObject("PryUSAT.clsDatPresupuesto")

	
	for each codArt in request.Form("chkEliminar")
		
		objPresupuesto.EliminarDetallePresupuesto lngCodigo_Pto,codArt
	next

	ruta="frmConsolidadoPresupuesto.asp?" & "id=" & lngCodigo_Pto  
	response.Redirect(ruta)

End select
%>