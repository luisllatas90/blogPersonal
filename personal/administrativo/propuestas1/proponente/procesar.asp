<!--#include file="../../../../funciones.asp"-->
<script>
		function popUp(URL) {
		day = new Date();
		id = day.getTime();
		var izq = 300
		var arriba= 200
		eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=400,height=350,left = "+ izq +",top = "+ arriba +"');");
		}	
</script>
<%

accion=Request.QueryString("accion")
tipo=Request.QueryString("tipo")

	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexionTrans	

if accion="refrescar" then ''dEVUELEVE LOS MISMOS VALORES POR QUERYSTRING
	codigo_prp=Request.Form("txtcodigo_prp")
	nombre=Request.Form("txtnombre_prp")
	instancia=Request.QueryString("instancia")
	prioridad=Request.Form("txtprioridad_prp")
	tipoPrp=Request.Form("cbotipopropuesta")
	facultad=Request.Form("cbofacultad")
	codReferencia=null
	esinforme="SI"
	codigo_cco=Request.Form("cbocentrodecostos")
	''---------------en datos propuestas como primera versión
	ingreso_dap=Request.Form("txtIngresos")
	egreso_dap=Request.Form("txtEgresos")
	utilidad_dap=Request.Form("txtUtilidad")
	beneficios_dap=Request.Form("txtbeneficiarios")
	importancia_dap=Request.Form("txtimportancia")
	version_dap=1''Request.Form("")
	moneda_dap=Request.Form("chkdolar")

	if moneda_dap	= "on" then
		moneda_dap=2
	else
		moneda_dap=1
	end if
	tipoCambio_dap=Request.Form("txtCambio")
	IngresoMN_Dap=Request.Form("txtIngresosMN")
	EgresoMN_Dap=Request.Form("txtEgresosMN")
	UtilidadMN_Dap=Request.Form("txtUtilidadMN")
			
	Response.Redirect("registrapropuesta.asp?centrocosto=" + codigo_cco + "&nombre_prp=" + nombre+ "&codigo_tpr=" + tipoPrp+ "&codigo_fac=" + facultad + "&prioridad_prp="+ prioridad + "&instancia_prp="+instancia+ "&ingreso_dap="+ingreso_dap+"&egreso_dap="+egreso_dap+"&utilidad_dap="+utilidad_dap+"&beneficios_dap="+beneficios_dap+"&importancia_dap="+importancia_dap+"&version_dap=1&IngresoMN_Dap="+ingresoMN_dap+"&EgresoMN_Dap="+EgresoMN_Dap+"&utilidadMN_dap="+utilidadMN_dap+"&moneda_dap="+cstr(moneda_dap)	)
end if

if accion="nuevo" then ''NUEVA PROPUESTA

	''------------cabecera de la propuesta
	codigo_prp=Request.Form("txtcodigo_prp")
	nombre=Request.Form("txtnombre_prp")
	instancia=Request.QueryString("instancia")
	prioridad=Request.Form("txtprioridad_prp")
	tipoPrp=Request.Form("cbotipopropuesta")
	facultad=Request.Form("cbofacultad")
	codReferencia=null
	esinforme="SI"
	codigo_cco=Request.Form("cbocentrodecostos")
	''---------------en datos propuestas como primera versión
	ingreso_dap=Request.Form("txtIngresos")
	egreso_dap=Request.Form("txtEgresos")
	utilidad_dap=Request.Form("txtUtilidad")
	beneficios_dap=Request.Form("txtbeneficiarios")
	importancia_dap=Request.Form("txtimportancia")
	version_dap=1''Request.Form("")
	moneda_dap=Request.Form("chkdolar")
	tipoCambio_dap=Request.Form("txtCambio")
	IngresoMN_Dap=Request.Form("txtIngresosMN")
	EgresoMN_Dap=Request.Form("txtEgresosMN")
	UtilidadMN_Dap=Request.Form("txtUtilidadMN")
	
	if estado="" then
		estado="P"
	end if
	
	if prioridad="" then
		prioridad="N"
	end if
	''1 : Soles | 2 : Dólares
	if moneda_dap = "" then
		moneda_dap=1
	else
		moneda_dap=2	
	end if
	
	tipoCambio_dap= Ltrim(Rtrim((trim(mid(tipoCambio_dap,7,len(trim(tipoCambio_dap))-5)))))

	tipoCambio_dap = cdbl(Rtrim(Ltrim(tipoCambio_dap)))
	
	CAD=objPropuesta.Ejecutar ("RegistraPropuesta",true,"PR",nombre,estado,instancia,prioridad,tipoPrp,facultad,codReferencia,esinforme,codigo_cco,ingreso_dap,egreso_dap,utilidad_dap,beneficios_dap,importancia_dap,version_dap,moneda_dap,tipoCambio_dap,IngresoMN_Dap,EgresoMN_Dap,UtilidadMN_Dap,NULL)

	
		ubi=InStr (trim(CAD),"|")
		codigo_prp=mid(trim(cad),1,ubi-1)
		codigo_dap=mid(trim(cad),ubi+1,len(trim(cad)))

	'Verificar si la propuesta va por la vía de consejo de facultad
	codigo_fac= objPropuesta.Ejecutar ("PRP_PerteneceAFacultad",true,codigo_cco,0)
response.Write(codigo_fac)	
	if codigo_fac = 0 then
		'actualiza el destino a consejo universitario
		objPropuesta.Ejecutar "PRP_ActualizarDestino",false, codigo_prp, "U",codigo_fac
		objPropuesta.Ejecutar "RegistrarInvolucradosPropuesta",false, codigo_prp, codigo_cco,session("codigo_usu"),tipoPrp,0,facultad
	else
		'actualiza el destino a consejo de facultad	
		objPropuesta.Ejecutar "PRP_ActualizarDestino",false, codigo_prp, "F",codigo_fac
		objPropuesta.Ejecutar "PRP_RegistrarInvolucradosConsejoFacultad",false,codigo_prp,codigo_fac,tipoPrp,session("codigo_usu")
	end if
	objPropuesta.CerrarConexionTrans				 

	Response.Redirect("registrapropuesta.asp?codigo_prp=" + codigo_prp + "&codigo_dap=" + codigo_dap)

end if


if accion="actualizar" then '' ACTUALIZAR UNA PROPUESTA
//	response.write (accion)
	''------------cabecera de la propuesta
	
	codigo_prp=Request.Form("txtcodigo_prp")
	codigo_dap=Request.Form("txtcodigo_dap")	
	nombre=Request.Form("txtnombre_prp")
	//Response.Write(nombre)

	instancia=Request.QueryString("instancia")
	prioridad=Request.Form("txtprioridad_prp")
	tipoPrp=Request.Form("cbotipopropuesta")
	facultad=Request.Form("cbofacultad")
	codReferencia=null
	esinforme="SI"
	codigo_cco=Request.Form("cbocentrodecostos")
	''---------------en datos propuestas como primera versión
	ingreso_dap=Request.Form("txtIngresos")
	egreso_dap=Request.Form("txtEgresos")
	utilidad_dap=Request.Form("txtUtilidad")
	beneficios_dap=Request.Form("txtbeneficiarios")
	importancia_dap=Request.Form("txtimportancia")
	version_dap=1''Request.Form("")
	moneda_dap=Request.Form("chkdolar")
	tipoCambio_dap=Request.Form("txtCambio")
	IngresoMN_Dap=Request.Form("txtIngresosMN")
	EgresoMN_Dap=Request.Form("txtEgresosMN")
	UtilidadMN_Dap=Request.Form("txtUtilidadMN")
	
	if estado="" then
		estado="P"
	end if
	
	if prioridad="" then
		prioridad="N"
	end if
	''1 : Soles | 2 : Dólares
	if moneda_dap = "" then
		moneda_dap=1
	else
		moneda_dap=2	
	end if
	
//	tipoCambio_dap=mid(tipoCambio_dap,5,len(tipoCambio_dap))
	tipoCambio_dap= Ltrim(Rtrim((trim(mid(tipoCambio_dap,7,len(trim(tipoCambio_dap))-5)))))

	Response.write (codigo_dap)
	Response.write (codigo_prp)	
	objPropuesta.Ejecutar "ActualizarDatosPropuesta",false,codigo_dap,ingreso_dap,egreso_dap,utilidad_dap,beneficios_dap,importancia_dap,version_dap,moneda_dap,tipoCambio_dap,IngresoMN_Dap,EgresoMN_Dap,UtilidadMN_Dap
	objPropuesta.CerrarConexionTrans	
	Response.Redirect("registrapropuesta.asp?codigo_prp=" + codigo_prp + "&codigo_dap=" + codigo_dap)

end if

if accion="nuevaVersion" then ''NUEVA VERSION DE LA PROPUESTA

	''------------cabecera de la propuesta
	codigo_prp=Request.Form("txtcodigo_prp")
	estado=Request.QueryString("estado")
	CODIGO_DAP=Request.QueryString("codigo_dap")
	nombre=Request.Form("txtnombre_prp")
	codigo_per=session("codigo_Usu")

	instancia=Request.QueryString("instancia")
	prioridad=Request.Form("txtprioridad_prp")
	tipoPrp=Request.Form("cbotipopropuesta")
	facultad=Request.Form("cbofacultad")
	codReferencia=null
	esinforme="SI"
	codigo_cco=Request.Form("cbocentrodecostos")
	''---------------en datos propuestas como primera versión
	ingreso_dap=Request.Form("txtIngresos")
	egreso_dap=Request.Form("txtEgresos")
	utilidad_dap=Request.Form("txtUtilidad")
	beneficios_dap=Request.Form("txtbeneficiarios")
	importancia_dap=Request.Form("txtimportancia")
	version_dap=1''Request.Form("")
	moneda_dap=Request.Form("chkdolar")
	tipoCambio_dap=Request.Form("txtCambio")
	IngresoMN_Dap=Request.Form("txtIngresosMN")
	EgresoMN_Dap=Request.Form("txtEgresosMN")
	UtilidadMN_Dap=Request.Form("txtUtilidadMN")
	
	if estado="" then
		estado="P"
	end if
	
	if prioridad="" then
		prioridad="N"
	end if
	''1 : Soles | 2 : Dólares
	if moneda_dap = "" then
		moneda_dap=1
	else
		moneda_dap=2	
	end if
	
	//tipoCambio_dap=mid(tipoCambio_dap,5,len(tipoCambio_dap))
	tipoCambio_dap= Ltrim(Rtrim((trim(mid(tipoCambio_dap,7,len(trim(tipoCambio_dap))-5)))))

	CAD=objPropuesta.Ejecutar ("RegistraVersionPropuesta",true,codigo_prp,ingreso_dap,egreso_dap,utilidad_dap,beneficios_dap,importancia_dap,moneda_dap,tipoCambio_dap,IngresoMN_Dap,EgresoMN_Dap,UtilidadMN_Dap,estado,0)

//Response.Write CAD & "|" & len(cad)

//	ubi=InStr (trim(CAD),"|")
//	response.write (codigo_prp)
//	codigo_prp=mid(trim(cad),1,ubi-1)
//	codigo_dap=mid(trim(cad),ubi+1,len(trim(cad)))
	codigo_dap=cstr(CAD)
		 
''	Set Rs=objPropuesta.Consultar("ConsultarEmailInvolucradoPropuesta","FO","NV",codigo_prp,codigo_per,"")
	objPropuesta.CerrarConexionTrans
''	do while not rs.eof
''	Set Obj= Server.CreateObject("AulaVirtual.clsEmail")
''		Mensajes=Obj.EnviarCorreo("sistemapropuestas@usat.edu.pe",Rs("asunto"),Rs("mensaje"),Rs("email"))
''	Set Obj=nothing
''	rs.MoveNext
''	loop
''	set rs=nothing

	Response.Redirect("registraversion.asp?codigo_prp=" + codigo_prp + "&codigo_dap=" + codigo_dap)

end if

if accion="actualizarversion" then '' ACTUALIZAR UNA VERSION DE UNA PROPUESTA
//	response.write (accion)
	''------------cabecera de la propuesta
	
	codigo_prp=Request.Form("txtcodigo_prp")
	codigo_dap=Request.Form("txtcodigo_dap")	
	nombre=Request.Form("txtnombre_prp")
	//Response.Write(nombre)
//	estado=Request.Form("")
	instancia=Request.QueryString("instancia")
	prioridad=Request.Form("txtprioridad_prp")
	tipoPrp=Request.Form("cbotipopropuesta")
	facultad=Request.Form("cbofacultad")
	codReferencia=null
	esinforme="SI"
	codigo_cco=Request.Form("cbocentrodecostos")
	''---------------en datos propuestas como primera versión
	ingreso_dap=Request.Form("txtIngresos")
	egreso_dap=Request.Form("txtEgresos")
	utilidad_dap=Request.Form("txtUtilidad")
	beneficios_dap=Request.Form("txtbeneficiarios")
	importancia_dap=Request.Form("txtimportancia")
	version_dap=1''Request.Form("")
	moneda_dap=Request.Form("chkdolar")
	tipoCambio_dap=Request.Form("txtCambio")
	IngresoMN_Dap=Request.Form("txtIngresosMN")
	EgresoMN_Dap=Request.Form("txtEgresosMN")
	UtilidadMN_Dap=Request.Form("txtUtilidadMN")
	
	if estado="" then
		estado="P"
	end if
	
	if prioridad="" then
		prioridad="N"
	end if
	''1 : Soles | 2 : Dólares
	if moneda_dap = "" then
		moneda_dap=1
	else
		moneda_dap=2	
	end if
	
//	tipoCambio_dap=mid(tipoCambio_dap,5,len(tipoCambio_dap))
	tipoCambio_dap= Ltrim(Rtrim((trim(mid(tipoCambio_dap,7,len(trim(tipoCambio_dap))-5)))))	
	//Response.write(tipoCambio_dap)
	//Response.write (codigo_dap)
	//Response.write (codigo_prp)	
	objPropuesta.Ejecutar "ActualizarDatosPropuesta",false,codigo_dap,ingreso_dap,egreso_dap,utilidad_dap,beneficios_dap,importancia_dap,version_dap,moneda_dap,tipoCambio_dap,IngresoMN_Dap,EgresoMN_Dap,UtilidadMN_Dap
	objPropuesta.CerrarConexionTrans	
	Response.Redirect("registraversion.asp?codigo_prp=" + codigo_prp + "&codigo_dap=" + codigo_dap)

end if

if accion="enviar" then '' ACTUALIZAR UNA VERSION DE UNA PROPUESTA

	''------------cabecera de la propuesta
	
	codigo_prp=Request.Form("txtcodigo_prp")
	codigo_dap=Request.Form("txtcodigo_dap")	
	nombre=Request.Form("txtnombre_prp")
	codigo_per=session("codigo_Usu")
	instancia=Request.QueryString("instancia")
	prioridad=Request.Form("txtprioridad_prp")
	tipoPrp=Request.Form("cbotipopropuesta")
	facultad=Request.Form("cbofacultad")
	codReferencia=null
	esinforme="SI"
	codigo_cco=Request.Form("cbocentrodecostos")
	''---------------en datos propuestas como primera versión
	ingreso_dap=Request.Form("txtIngresos")
	egreso_dap=Request.Form("txtEgresos")
	utilidad_dap=Request.Form("txtUtilidad")
	beneficios_dap=Request.Form("txtbeneficiarios")
	importancia_dap=Request.Form("txtimportancia")
	version_dap=1''Request.Form("")
	moneda_dap=Request.Form("chkdolar")
	tipoCambio_dap=Request.Form("txtCambio")
	IngresoMN_Dap=Request.Form("txtIngresosMN")
	EgresoMN_Dap=Request.Form("txtEgresosMN")
	UtilidadMN_Dap=Request.Form("txtUtilidadMN")
	
	if estado="" then
		estado="P"
	end if
	
	if prioridad="" then
		prioridad="N"
	end if
	''1 : Soles | 2 : Dólares
	if moneda_dap = "" then
		moneda_dap=1
	else
		moneda_dap=2	
	end if
	
	tipoCambio_dap=mid(tipoCambio_dap,5,len(tipoCambio_dap))
	//Response.write(tipoCambio_dap)
//	Response.write (codigo_dap)
//	Response.write (codigo_prp)	
		
	objPropuesta.Ejecutar "DarVeredictoPropuesta",false,codigo_prp,session("codigo_Usu"),"C"
	objPropuesta.CerrarConexionTrans
	Response.Write("<script>window.opener.location.reload()</script>")
	Response.Write("<script>alert('Se ha enviado la propuesta a la instancia siguiente')</script>")		
	Response.Write("<script>window.close()</script>")


end if
//	al fin de cada proceso se debe CerrarConexionTrans				 
	//objPropuesta.CerrarConexionTrans

if 	 accion="guardarcomentario" then

	codigo_ipr=Request.QueryString("codigo_ipr")
	nivelrestriccion_cop=Request.QueryString("nivelrestriccion_cop")
	codigorespuesta_cop=Request.QueryString("codigorespuesta_cop")
	estado_cop=Request.QueryString("estado_cop")	
	asunto_cop=Request.QueryString("asunto_cop")
	comentario_cop=Request.QueryString("comentario_cop")
	remLen=Request.QueryString("remLen")
	attach=Request.QueryString("attach")
	codigo_prp=Request.QueryString("codigo_prp")

	codigo_cop=objPropuesta.ejecutar ("RegistraComentario",true,codigo_ipr,codigorespuesta_cop,estado_cop,asunto_cop,comentario_cop,"O",0	)

	codigo_per=session("codigo_Usu")
	objPropuesta.CerrarConexionTrans

	
 	if Request.QueryString("envio_cop") = 1 then %>
	  <script>
		alert('Se ha registrado el comentario de la propuesta satisfactoriamente.')
		window.opener.location.reload()	  
		window.close()
	  </script>
<%
	else
	response.redirect "registracomentario.asp?codigo_ipr=" & codigo_ipr & "&nivelrestriccion_cop=P&coment1=" & codigorespuesta_cop & "&estado_cop=A&asunto_cop=" & asunto_cop & "&comentario_cop=" & comentario_cop & "&remLen=" & remLen & "&accion=guardarcomentario&codigo_prp="	& codigo_prp & "&newcodigo_cop=" & codigo_cop & "&attach=" & attach
	end if
end if

if 	 accion="actualizacomentario" then

	codigo_ipr=Request.QueryString("codigo_ipr")
	nivelrestriccion_cop=Request.QueryString("nivelrestriccion_cop")
	codigorespuesta_cop=Request.QueryString("codigorespuesta_cop")
	estado_cop=Request.QueryString("estado_cop")	
	asunto_cop=Request.QueryString("asunto_cop")
	comentario_cop=Request.QueryString("comentario_cop")
	remLen=Request.QueryString("remLen")
	attach=Request.QueryString("attach")
	codigo_prp=Request.QueryString("codigo_prp")
	codigo_cop=Request.QueryString("codigo_cop")	
	codigo_per=session("codigo_Usu")	
	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexion
		objPropuesta.Ejecutar "ActualizaComentario",FALSE,codigo_cop,"A",asunto_cop,comentario_cop,"O"
	objPropuesta.CerrarConexion	
 	if Request.QueryString("envio_cop") = 1 then%>
		<script>
			window.opener.location.reload()
			window.close()
		</script>
<%
	else
		response.redirect "registracomentario.asp?codigo_ipr=" & codigo_ipr & "&nivelrestriccion_cop=P&coment1=" & codigorespuesta_cop & "&estado_cop=A&asunto_cop=" & asunto_cop & "&comentario_cop=" & comentario_cop & "&remLen=" & remLen & "&accion=actualizacomentario&codigo_prp="	& codigo_prp & "&newcodigo_cop=" & codigo_cop
	end if	
end if

''--------------------------------------------------------
if accion="nuevoInforme" then ''NUEVA VERSION DEL INFORME DE LA PROPUESTA

	''------------cabecera de la propuesta
	codigo_prp=Request.Form("txtcodigo_prp")
	envia=Request.QueryString("envia")
	codigo_per=session("codigo_Usu")
	instancia=Request.QueryString("instancia")
	codigo_cco=Request.Form("cbocentrodecostos")
	''---------------en datos propuestas como primer INFORME DE  versión
	objetivo_dip=Request.Form("txtObjetivos")
	metas_dip=Request.Form("txtmetas")
	espectativas_dip=Request.Form("txtespectativas")
	utilidad_usat=Request.Form("txtutilidad")
	FechaInicioEjecucion_Dip=Request.Form("txtFechaFin")
	FechaFinEjecucion_Dip=Request.Form("txtFechaInicio")           
    codigo_dip=objPropuesta.Ejecutar ("RegistraVersionInforme",true,codigo_prp,metas_dip,objetivo_dip,espectativas_dip,utilidad_usat,FechaInicioEjecucion_Dip,FechaFinEjecucion_Dip,0)
''	if envia= "SI" then		
		objPropuesta.Ejecutar "DarVeredictoInforme",false,codigo_prp,session("codigo_Usu"),"C"
''		objPropuesta.Ejecutar "ActualizarPropuesta",false, "IN",codigo_prp,"","","","",0
''	end if

''	Set Rs=objPropuesta.Consultar("ConsultarEmailInvolucradoPropuesta","FO","NV",codigo_prp,codigo_per,"")
	objPropuesta.CerrarConexionTrans
''	do while not rs.eof
''		Set Obj= Server.CreateObject("AulaVirtual.clsEmail")
''			Mensajes=Obj.EnviarCorreo("sistemapropuestas@usat.edu.pe",Rs("asunto"),Rs("mensaje"),Rs("email"))
''		Set Obj=nothing
''		rs.MoveNext
''	loop
''	set rs=nothing	

	Response.Redirect "registrainforme.asp?codigo_prp=" & codigo_prp & "&codigo_dip=" & codigo_dip
end if

if accion="actualizarInforme" then ''actualiza una  VERSION DEL INFORME DE LA PROPUESTA
//	response.write (accion)
	''------------cabecera de la propuesta
	codigo_prp=Request.QueryString("codigo_prp")
	codigo_dip=Request.QueryString("codigo_dip")
	''---------------en datos propuestas como primer INFORME DE  versión
	objetivo_dip=Request.Form("txtObjetivos")
	metas_dip=Request.Form("txtmetas")
	espectativas_dip=Request.Form("txtespectativas")
	utilidad_usat=Request.Form("txtutilidad")
	FechaInicioEjecucion_Dip=Request.Form("txtFechaFin")
	FechaFinEjecucion_Dip=Request.Form("txtFechaInicio")     
	  
    objPropuesta.Ejecutar "Modificar_DatosInformePropuesta",true,codigo_dip, objetivo_dip, metas_dip,espectativas_dip,utilidad_usat,FechaInicioEjecucion_Dip,FechaFinEjecucion_Dip
	objPropuesta.CerrarConexionTrans				 
	Response.Redirect "registrainforme.asp?codigo_prp=" & codigo_prp & "&codigo_dip=" & codigo_dip
end if





%>

<%
Sub ActualizarFechaRevisionPropuesta(codigo_prp,instancia)
dim revisor
//response.write(codigo_prp)
Set objPr=Server.CreateObject("PryUSAT.clsAccesoDatos")
objPr.AbrirConexionTrans	
Set Rsdays = objPr.Consultar("ConsultarParametrosPropuesta","FO","IN",instancia)
dia=CINT(Rsdays("valor_ppr"))	 
set Rsdays= nothing	
fin=FechaFinHabil (FormatDateTime(date(),2),dia)
//response.write(fin)	
//consultar dias del revisor en instancia D	
//CONSULTAR TODOS LOS REVISORES DE UNA PROPUESTA SEGUN SU INTANCIA
	set revisor=objPr.Consultar("ConsultarResponsablesPropuesta","FO","RC",codigo_prp,0)


	do while not revisor.eof
		if revisor("instancia_Ipr") = instancia then
		//consultar si ya tiene una fecha de verdicto, si ya existe fecha de veredicto entonces no actualizar fechas		
		set RSfechaprogramada = objPr.Consultar("","FO","TO",revisor("codigo_ipr"),0)
			fecha_fin=RSfechaprogramada("fechafin_Ipr")
			if IsNull(fecha_fin) then
			//response.write 	fecha_fin
				//ACTUALIZAR FECHAS DE INICIO Y FIN  DE REVISON PARA DIRECTORES		
				//response.write(revisor("codigo_ipr")) & "<BR>"
				ObjPr.ejecutar "ActualizarFechasRevisionPropuesta",false,revisor("codigo_ipr"), date(), fin				
			end if				
		end if
	revisor.MoveNext
	loop

				 
	objPr.CerrarConexionTrans			
End Sub
%>