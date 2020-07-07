<%
'---------------------- Procesos (Insert, Update, Delete)-------------------------
Dim strTipo
strTipo=Request.QueryString("Tipo")

Select case strTipo

'-------------------------------- Agregar: Insert ------------------------------------
Case "I006" 'frmRegistrarDetallePresupuestoAnual:AgregarDetallePresupuesto

		
		lngCodigo_Pto= request.form("txtCodigo_Pto")
		lngCodigo_Art= request.form("txtCodigo_Art")

		dblPrecioUnitario= formatNumber(request.form("txtPrecio"))
		dblCantGlobal= formatNumber(request.form("txtCantGlobal"))
		dblTotalGlobal=FormatNumber(dblPrecioUnitario*dblCantGlobal)

		periodo = split(request.Form("txtPeriodo"),",")
		ano= split(request.Form("txtAno"),",")
		strAno=ano(0)
		cant = split(request.Form("txtCantidad"),",")
		subt = split(request.Form("txtSubTotal"),",")
		tipoArt= request.Form("txtTipoArt")
		strTipoCon=request.Form("txtTipoCon")


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
	  	        objPresupuesto.AgregarDetalleEjecucion lngCodigo_Pto,lngCodigo_Art,trim(periodo(x)),cdbl(Ct),FormatNumber(cdbl(Ct)* dblPrecioUnitario),trim(ano(x))
				
			 else
				objPresupuesto.AgregarDetalleEjecucion lngCodigo_Pto,lngCodigo_Art,trim(periodo(x)),FormatNumber(cdbl(Ct)* dblPrecioUnitario),FormatNumber(cdbl(Ct)* dblPrecioUnitario),trim(ano(x))
				
			 end if
		next 
		%>

		<script language="Javascript">
			window.opener.location.reload()
			window.close()	
		</script>

<%

'--------------------------------- Modificar:Update------------------
Case "U002" 'frmConsolidadoPresupuestoAnual: ActualizarEstadoPresupuesto

    'Dim strAno,lngCodigo_Cco,strEstado,objPresupuesto,strTipoCon
	strAno=request.QueryString("ano")
	lngCodigo_Cco=request.QueryString("cco")
	strEstado=request.QueryString("est")
	strTipoCon=request.QueryString("tc")
    
			Set objPresupuesto= Server.CreateObject("PryUSAT.clsDatPresupuesto")

			objPresupuesto.ActualizarEstadoPresupuesto lngCodigo_Cco,strAno, strEstado

			set objPresupuesto=Nothing
			
			ruta="frmConsolidadoPresupuestoAnual.asp?"&"ano="&strAno& "&" & "tcon="& strTipoCon & "&" & "est=" & strEstado

			response.Redirect(ruta)


'-------------------------------- Eliminar: Delete------------------------------------------------------



Case "D003" 'FrmConsolidadoPresupuestoAnual: EliminarDetallePresupuesto
    'Dim objPresupuesto,strAno,strTipoConsulta,Valores

	strAno=Request.form("txtAno")
	strTipoConsulta=Request.form("txtTipoConsulta")
	strCC=Request.form("txtCC")

	Set objPresupuesto= Server.CreateObject("PryUSAT.clsDatPresupuesto")

	for each Data in request.Form("chkEliminar")
		
		valores = split(Data,"-")
		
		objPresupuesto.EliminarDetallePresupuesto valores(0),valores(1)
		
	next

	Set objPresupuesto=Nothing	
	
	ruta="frmConsolidadoPresupuestoAnual.asp?" & "ano=" & strAno & "&" & "tcon=" & strTipoConsulta & "&" & "tcc=" & strCC
	response.Redirect(ruta)


End select
%>