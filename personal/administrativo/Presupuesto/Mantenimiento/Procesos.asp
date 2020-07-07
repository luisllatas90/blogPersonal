<%
'---------------------- Procesos (Insert, Update, Delete)-------------------------
Dim strTipo
strTipo=Request.QueryString("Tipo")

Select case strTipo

'-------------------------------- Agregar: Insert ------------------------------------


Case "I004" 'frmArticuloConcepto: AgregarArticuloConcepto
	
	'Dim blnFuncion_Art, strDescripcion_Art, intCodigo_Uni, dblPrecioUnitario_Art, strMoneda_Art, lngCodigo_Pco, dblStockActual_Art, dblStockMinimo_Art,objArticulo
	
	
	strDescripcion_Art=request.form("txtArt")
	lngCodigo_Pco=request.form("cboCta")
	dblPrecioUnitario_Art=request.form("txtPre")
	

	if trim(dblPrecioUnitario_Art)="" then dblPrecioUnitario_Art="0"

	Set objArticulo= Server.CreateObject("PryUSAT.clsDatArticuloConcepto")
	objArticulo.AgregarArticuloConcepto strDescripcion_Art, lngCodigo_Pco,dblPrecioUnitario_Art
	Set objArticulo=Nothing


	ruta="frmArticuloConcepto.asp?tip=C"
	response.Redirect(ruta)
			


Case "I005" 'frmConceptoIngresoEgreso :AgregarConceptoIngresoEgreso

	'Dim strDescripcion_Cie,strTipo_Cie,lngCodigo_Cge,objConcepto
	strDescripcion_Cie= trim(Request.Form("txtDesCon"))
	strTipo_Cie= Request.Form("cboTipCon")
	lngCodigo_Cge= Request.Form("cboConGen")

	Set objConcepto= Server.CreateObject("PryUSAT.clsDatConceptoIngresoEgreso")

	objConcepto.AgregarConceptoIngresoEgreso strDescripcion_Cie,strTipo_Cie,lngCodigo_Cge

	Set objConcepto=Nothing
	Response.Redirect("frmCuentaConcepto.asp")


Case "I007" 'frmParametro.asp : AgregarParametroPresupuesto

		'Dim strAno_Ppr,strFormaDetalle_Pto, strMoneda_Pto, dblProvisionMorosidad_Pto, dblRecuperacionMorosidad_Pto, dtmFechaInicio_Pto, dtmFechaFin_Pto, dblSaldoCajaAnoAnterior_Pto, strEstado_Pto
		'Dim objPresupuesto,objParametro,rsParametro, strTipo

		strAno_Ppr=Request.Form("txtAno")
		strFormaDetalle_Pto=Request.Form("cboForDet")
		strMoneda_Pto=Request.Form("cboMon")
		dblProvisionMorosidad_Pto=Request.Form("txtProMor")
		dblRecuperacionMorosidad_Pto=Request.Form("txtRecMor")
		dtmFechaInicio_Pto=Request.Form("txtFecIni")
		dtmFechaFin_Pto=Request.Form("txtFecFin")
		dblSaldoCajaAnoAnterior_Pto=Request.Form("txtSal")
		strEstado_Pto=cstr(Request.Form("chkVigencia"))
		strTipo=request.Form("txtTip")
		
		

		
		if strEstado_Pto="" then strEstado_Pto="0"
		if strEstado_Pto="ON" THEN strEstado_Pto="1"
		

		if trim(strTipo)="N" then
			'VERIFICAR SI EXISTE
			Set objParametro= Server.CreateObject("PryUSAT.clsDatPresupuesto")
			Set rsParametro= Server.CreateObject("ADODB.RecordSet")
			set rsParametro = objParametro.ConsultarParametroPresupuesto("RS",strAno_Ppr) 	
				
			if rsParametro.recordcount >0 then
				'EXISTE

			else
				'NO EXISTE-GRABAR NUEVO
				Set objPresupuesto= Server.CreateObject("PryUSAT.clsDatPresupuesto")
				objPresupuesto.AgregarParametroPresupuesto strAno_Ppr,strFormaDetalle_Pto, strMoneda_Pto, dblProvisionMorosidad_Pto, dblRecuperacionMorosidad_Pto, dtmFechaInicio_Pto, dtmFechaFin_Pto, dblSaldoCajaAnoAnterior_Pto, strEstado_Pto
				set objPresupuesto=Nothing
				
			end if

			rsParametro.Close
			set rsParametro=Nothing
			set objParametro=Nothing
			

		ELSE
			'EDITAR
				Set objPresupuesto= Server.CreateObject("PryUSAT.clsDatPresupuesto")
				objPresupuesto.ModificarParametroPresupuesto strAno_Ppr,strFormaDetalle_Pto, strMoneda_Pto, dblProvisionMorosidad_Pto, dblRecuperacionMorosidad_Pto, dtmFechaInicio_Pto, dtmFechaFin_Pto, dblSaldoCajaAnoAnterior_Pto, strEstado_Pto
				set objPresupuesto=Nothing

			
		END IF

		Response.Redirect("frmParametro.asp?tip=C")



'--------------------------------- Modificar:Update------------------

Case "U003" 'frmArticuloConcepto:ModificarArticuloConcepto	

		'Dim lngCodigo_Art, blnFuncion_Art, strDescripcion_Art, intCodigo_Uni, dblPrecioUnitario_Art, strMoneda_Art, lngCodigo_Pco, dblStockActual_Art, dblStockMinimo_Art,objArticulo
		
		lngCodigo_Art= request.form("txtCodArt")
		strDescripcion_Art=request.form("txtArt")
		dblPrecioUnitario_Art=request.form("txtPre")
		lngCodigo_Pco=request.form("cboCta")
			

		if trim(dblPrecioUnitario_Art)="" then dblPrecioUnitario_Art="0"

		Set objArticulo= Server.CreateObject("PryUSAT.clsDatArticuloConcepto")
		objArticulo.ModificarArticuloConcepto lngCodigo_Art,strDescripcion_Art, lngCodigo_Pco, dblPrecioUnitario_Art
		Set objArticulo=Nothing

		ruta="frmArticuloConcepto.asp?tip=C"
		response.Redirect(ruta)


Case "U004" 'frmCuentaConcepto.asp :AsignarConceptoCuenta

	'Dim lngCodigo_Cie,objPresupuesto

	lngCodigo_Cie= request.Form("txtCodigo_Cie")
	if lngCodigo_Cie<>"" then
		Set objPresupuesto= Server.CreateObject("PryUSAT.clsDatPresupuesto")
		for each cuenta in request.Form("chkCuenta")
			objPresupuesto.AsignarConceptoCuenta cuenta,lngCodigo_Cie
		next
		Set objPresupuesto= Nothing

		ruta="frmCuentaConcepto.asp?tipo=M" & "&CodCie=" & lngCodigo_Cie
		response.Redirect(ruta)
	else
		response.Redirect("frmCuentaConcepto.asp")
	end if


Case "U005" 'frmCuentaConcepto.asp :QuitarConceptoCuenta

	'Dim lngCodigo_Cie,objPresupuesto

	lngCodigo_Cie= request.Form("txtCodigo_Cie2")

	if lngCodigo_Cie<>"" then
		Set objPresupuesto= Server.CreateObject("PryUSAT.clsDatPresupuesto")
		
		for each cuenta in request.Form("chkCuenta2")
			objPresupuesto.QuitarConceptoCuenta cuenta
			
		next
		Set objPresupuesto= Nothing

		ruta="frmCuentaConcepto.asp?tipo=M" & "&CodCie=" & lngCodigo_Cie
		response.Redirect(ruta)
	else
		response.Redirect("frmCuentaConcepto.asp")
	end if
	

'-------------------------------- Eliminar: Delete------------------------------------------------------

Case "D004" 'frmArticuloConcepto: EliminarArticuloConcepto
    'Dim objArticulo

	Set objArticulo= Server.CreateObject("PryUSAT.clsDatArticuloConcepto")

	for each codArt in request.Form("chkEliminar")
		objArticulo.EliminarArticuloConcepto codArt
		
	next

	Set objArticulo=Nothing	
	
	ruta="frmArticuloConcepto.asp?tip=C"
	response.Redirect(ruta)


Case "D005" 'frmParamettro: 

	strAno=request.QueryString("ano")
    
	Set objParametro= Server.CreateObject("PryUSAT.clsDatPresupuesto")
	strRpta = objParametro.EliminarParametroPresupuesto(strAno)

	set objPresupuesto=Nothing

	ruta="frmParametro.asp?tip=C&rpta=" & strRpta
	response.Redirect(ruta)
		'response.write strRpta

End select
%>