<!--#include file="../../../../funciones.asp"-->
<script>
		function popUp(URL) {
		day = new Date();
		id = day.getTime();
		var izq = 300//(screen.width-ancho)/2
		//alert (izq)
		var arriba= 200//(screen.height-alto)/2
		eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=400,height=350,left = "+ izq +",top = "+ arriba +"');");
		}	
</script>
<%
on error resume next

accion=Request.QueryString("accion")
tipo=Request.QueryString("tipo")
Response.write(tipo)
//response.write(tipopropuesta)
	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexionTrans	

if accion="nuevo" then ''NUEVA PROPUESTA
//	response.write (accion)
	''------------cabecera de la propuesta
	codigo_prp=Request.Form("txtcodigo_prp")
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
	
	tipoCambio_dap=mid(tipoCambio_dap,5,len(tipoCambio_dap))

	CAD=objPropuesta.Ejecutar ("RegistraPropuesta",true,"PR",nombre,estado,instancia,prioridad,tipoPrp,facultad,codReferencia,esinforme,codigo_cco,ingreso_dap,egreso_dap,utilidad_dap,beneficios_dap,importancia_dap,version_dap,moneda_dap,tipoCambio_dap,IngresoMN_Dap,EgresoMN_Dap,UtilidadMN_Dap,0)
	ubi=InStr (trim(CAD),"|")

	codigo_prp=mid(trim(cad),1,ubi-1)
	codigo_dap=mid(trim(cad),ubi+1,len(trim(cad)))

	objPropuesta.Ejecutar "RegistrarInvolucradosPropuesta",false, codigo_prp, codigo_cco,session("codigo_usu"),tipoPrp,0,facultad

	objPropuesta.CerrarConexionTrans				 

	Response.Redirect("registrapropuesta.asp?codigo_prp=" + codigo_prp + "&codigo_dap=" + codigo_dap)

end if


if accion="actualizar" then '' ACTUALIZAR UNA PROPUESTA

	''------------cabecera de la propuesta
	
	codigo_prp=Request.Form("txtcodigo_prp")
	codigo_dap=Request.Form("txtcodigo_dap")	
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
	
	tipoCambio_dap=mid(tipoCambio_dap,5,len(tipoCambio_dap))

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

	CAD=objPropuesta.Ejecutar ("RegistraVersionPropuesta",true,codigo_prp,ingreso_dap,egreso_dap,utilidad_dap,beneficios_dap,importancia_dap,moneda_dap,tipoCambio_dap,IngresoMN_Dap,EgresoMN_Dap,UtilidadMN_Dap,estado,0)
	ubi=InStr (trim(CAD),"|")

	codigo_prp=mid(trim(cad),1,ubi-1)
	codigo_dap=mid(trim(cad),ubi+1,len(trim(cad)))

	objPropuesta.CerrarConexionTrans				 
	Response.Redirect("registraversion.asp?codigo_prp=" + codigo_prp + "&codigo_dap=" + codigo_dap)

end if

if accion="actualizarversion" then '' ACTUALIZAR UNA VERSION DE UNA PROPUESTA

	''------------cabecera de la propuesta
	
	codigo_prp=Request.Form("txtcodigo_prp")
	codigo_dap=Request.Form("txtcodigo_dap")	
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
	
	tipoCambio_dap=mid(tipoCambio_dap,5,len(tipoCambio_dap))

	Response.write (codigo_dap)
	Response.write (codigo_prp)	
	objPropuesta.Ejecutar "ActualizarDatosPropuesta",false,codigo_dap,ingreso_dap,egreso_dap,utilidad_dap,beneficios_dap,importancia_dap,version_dap,moneda_dap,tipoCambio_dap,IngresoMN_Dap,EgresoMN_Dap,UtilidadMN_Dap
	objPropuesta.CerrarConexionTrans	
	Response.Redirect("registraversion.asp?codigo_prp=" + codigo_prp + "&codigo_dap=" + codigo_dap)

end if

if accion="enviar" then '' envía una propuesta a la instancia siguiente
	veredicto=Request.QueryString("veredicto")
	codigo_prp=Request.QueryString("codigo_prp")
	menu=Request.QueryString("menu")
	instancia=Request.QueryString("instancia")
	estado=Request.QueryString("estado")
	informe=Request.QueryString("informe")
	inf=Request.QueryString("inf")
	estado_prop=Request.Form("estado_prop")	
''	Response.Write(veredicto & "<br>")
''	Response.Write(codigo_prp)	
	
	objPropuesta.Ejecutar "PRP_DarVeredictoPropuestaSecretaria",false,codigo_prp,session("codigo_Usu"),Veredicto
	objPropuesta.CerrarConexionTrans
	if Veredicto="C" then	
		codigo_per=session("codigo_Usu")
''		objPropuesta1.AbrirConexion
''			Set Rs=objPropuesta1.Consultar("ConsultarEmailInvolucradoPropuesta","FO","SE",codigo_prp,codigo_per,Veredicto)
''		objPropuesta1.CerrarConexion
	
''		do while not rs.eof
''		Set Obj= Server.CreateObject("AulaVirtual.clsEmail")
''			Mensajes=Obj.EnviarCorreo("sistemapropuestas@usat.edu.pe",Rs("asunto"),Rs("mensaje"),Rs("email"))
''		Set Obj=nothing
''		rs.MoveNext
''		loop
''		set rs=nothing
	end if
	Response.Redirect("contenido.asp?menu=" + menu + "&instancia=" + instancia + "&estado=" + estado + "&informe=" + informe + "&inf=" + inf + "&estado_prop="+estado_prop)


end if


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
	ObsCom=Request.QueryString("ObsCom")

	codigo_cop=objPropuesta.ejecutar ("RegistraComentario",true,codigo_ipr,codigorespuesta_cop,estado_cop,asunto_cop,comentario_cop,ObsCom,0	)

	codigo_per=session("codigo_Usu")
''	Set Rs=objPropuesta.Consultar("ConsultarEmailInvolucradoPropuesta","FO","RC",codigo_prp,codigo_per,"")
	objPropuesta.CerrarConexionTrans
''	do while not rs.eof
''	Set Obj= Server.CreateObject("AulaVirtual.clsEmail")
''		Mensajes=Obj.EnviarCorreo("sistemapropuestas@usat.edu.pe",Rs("asunto"),Rs("mensaje"),Rs("email"))
''	Set Obj=nothing
''	rs.MoveNext
''	loop
''	set rs=nothing	

 	if Request.QueryString("envio_cop") = 1 then%>
	<script>
		window.opener.location.reload()	  
		window.close()
	</script>
<%
	else
		response.redirect "registracomentario.asp?codigo_ipr=" & codigo_ipr & "&nivelrestriccion_cop=P&coment1=" & codigorespuesta_cop & "&estado_cop=A&asunto_cop=" & asunto_cop & "&comentario_cop=" & comentario_cop & "&remLen=" & remLen & "&accion=guardarcomentario&codigo_prp="	& codigo_prp & "&newcodigo_cop=" & codigo_cop & "&attach=" & attach & "&ObsCom="& ObsCom
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
	ObsCom=Request.QueryString("ObsCom")

	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexionTrans
	objPropuesta.Ejecutar "ActualizaComentario",FALSE,codigo_cop,estado_cop,asunto_cop,comentario_cop,ObsCom
	objPropuesta.CerrarConexionTrans	
//	response.redirect "registracomentario.asp?codigo_ipr=" & codigo_ipr & "&nivelrestriccion_cop=P&coment1=" & codigorespuesta_cop & "&estado_cop=A&asunto_cop=" & asunto_cop & "&comentario_cop=" & comentario_cop & "&remLen=" & remLen & "&accion=actualizacomentario&codigo_prp="	& codigo_prp & "&newcodigo_cop=" & codigo_cop
 	if Request.QueryString("envio_cop") = 1 then
	//response.write("cierra ventana")%>
	<script>
	  //	alert("hola")
		window.opener.location.reload()
		window.close()
	  </script>
<%
	else
	response.redirect "registracomentario.asp?codigo_ipr=" & codigo_ipr & "&nivelrestriccion_cop=P&coment1=" & codigorespuesta_cop & "&estado_cop=A&asunto_cop=" & asunto_cop & "&comentario_cop=" & comentario_cop & "&remLen=" & remLen & "&accion=actualizacomentario&codigo_prp="	& codigo_prp & "&newcodigo_cop=" & codigo_cop  & "&ObsCom="& ObsCom
	end if	
end if
	//RegistraPropuesta=codigo_prp	 
	//If Err.Number<>0 then
	//	objPropuesta.CancelarConexionTrans
	//	response.redirect "../../../../error.asp?Numero=" & Err.Number & "&Recurso=" & Err.Source & "&Descripcion=" & Err.description & "&rutaPagina=" & Request.ServerVariables("SCRIPT_NAME")		
	//End If	
	Set objPropuesta=nothing
%>
''---------------------------------------------------------------------------------------
<%
if 	 accion="registrareunion" then

	codigo_rec=Request.QueryString("codigo_Rec")
	Nombre=Request.QueryString("Nombre")
	Fecha=Request.QueryString("Fecha")
	Lugar=Request.QueryString("Lugar")
	tipo=Request.QueryString("tipo")
	//codigo_rec=Request.QueryString("codigo_rec")		
	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexionTrans
		if codigo_rec="" then
	
		''guardar
			codigo_rec= objPropuesta.Ejecutar ("RegistrarReunionConsejo",true,Nombre,Fecha,Lugar,null,null,tipo,0)
		'' Actualiza con el código de la facultad si es que els ecretarioq ue registra la reunion es secretario de una facultad
			objPropuesta.Ejecutar "PRP_ActualizaReunion",false,codigo_rec , session("codigo_usu")
			
			objPropuesta.CerrarConexionTrans
			response.redirect "registrareunion.asp?accion=registrareunion&Nombre=" & Nombre & "&Fecha=" & Fecha & "&Lugar=" & Lugar & "&modifica=" & accion & "&tipo=" & tipo & "&codigo_rec=" & codigo_rec 
//			response.redirect "registracomentario.asp?codigo_ipr=" & codigo_ipr & "&nivelrestriccion_cop=P&coment1=" & codigorespuesta_cop & "&estado_cop=A&asunto_cop=" & asunto_cop & "&comentario_cop=" & comentario_cop & "&remLen=" & remLen & "&accion=guardarcomentario&codigo_prp="	& codigo_prp & "&newcodigo_cop=" & codigo_cop & "&attach=" & attach
		else
		''modificar
			objPropuesta.Ejecutar "ActualizarReunionConsejo",false,"UP",codigo_rec,Nombre,Fecha,Lugar,tipo
			objPropuesta.CerrarConexionTrans
			response.redirect "registrareunion.asp?accion=registrareunion&Nombre=" & Nombre & "&Fecha=" & Fecha & "&Lugar=" & Lugar & "&modifica=" & accion & "&tipo=" & tipo & "&codigo_rec=" & codigo_rec 
	
		end if
end if


if 	 accion="ProgramarAgenda" then

	codigo_rec=Request.QueryString("codigo_Rec")
	Cad_Prp=Request.QueryString("Cad_Prp")

	
	//codigo_rec=Request.QueryString("codigo_rec")		
	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexionTrans
			objPropuesta.Ejecutar "RegistrarPropuestasReunionConsejo",false,codigo_rec,Cad_Prp
			objPropuesta.CerrarConexionTrans
			%>
			//response.redirect "registrareunion.asp?accion=registrareunion&Nombre=" & Nombre & "&Fecha=" & Fecha & "&Lugar=" & Lugar & "&modifica=" & accion & "&tipo=" & tipo & "&codigo_rec=" & codigo_rec 
//			response.redirect "registracomentario.asp?codigo_ipr=" & codigo_ipr & "&nivelrestriccion_cop=P&coment1=" & codigorespuesta_cop & "&estado_cop=A&asunto_cop=" & asunto_cop & "&comentario_cop=" & comentario_cop & "&remLen=" & remLen & "&accion=guardarcomentario&codigo_prp="	& codigo_prp & "&newcodigo_cop=" & codigo_cop & "&attach=" & attach
			<script>window.opener.location.reload();window.close()</script>
<%
end if



if 	 accion="eliminaItemAgenda" then

codigo_rec=Request.QueryString("codigo_rec")
NOMBRE=Request.QueryString("NOMBRE")
LUGAR=Request.QueryString("LUGAR")
FECHA=Request.QueryString("FECHA")
TIPO=Request.QueryString("TIPO")
codigo_prp=Request.QueryString("codigo_prp")


	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexionTrans
	objPropuesta.Ejecutar "EliminarReunionConsejoPropuesta",false,"DE",codigo_prp
	objPropuesta.CerrarConexionTrans

	response.redirect "registrareunion.asp?codigo_Rec=" & codigo_Rec & "&NOMBRE=" & NOMBRE & "&LUGAR=" & LUGAR & "&FECHA=" & FECHA & "&TIPO=" & TIPO & "&codigo_prp=" & codigo_prp
Response.Write(codigo_Rec)
end if

if 	 accion="postegarPropuesta" then

codigo_rec=Request.QueryString("codigo_rec")
NOMBRE=Request.QueryString("NOMBRE")
LUGAR=Request.QueryString("LUGAR")
FECHA=Request.QueryString("FECHA")
TIPO=Request.QueryString("TIPO")
codigo_prp=Request.QueryString("codigo_prp")

	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexionTrans
	objPropuesta.Ejecutar "PostergarReunionConsejoPropuesta",false,"UP",codigo_prp,codigo_rec
	objPropuesta.CerrarConexionTrans
	response.redirect "registrareunion.asp?codigo_Rec=" & codigo_Rec & "&NOMBRE=" & NOMBRE & "&LUGAR=" & LUGAR & "&FECHA=" & FECHA & "&TIPO=" & TIPO & "&codigo_prp=" & codigo_prp
	''Response.Write(codigo_Rec)
end if


if 	 accion="registraAcuerdo" then

codigo_rec=Request.QueryString("codigo_rec")
codigo_prp=Request.QueryString("codigo_prp")
acuerdo=Request.QueryString("acuerdo")

	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexion
	objPropuesta.Ejecutar "RegistrarAcuerdoPropuestas",false,codigo_prp,acuerdo
	objPropuesta.CerrarConexion
	response.redirect "registraAcuerdo.asp?codigo_Rec=" & codigo_Rec & "&acuerdo=" & acuerdo & "&codigo_prp=" & codigo_prp
''	Response.Write(codigo_Rec)
end if

if 	 accion="registraDiscusion" then

codigo_rec=Request.QueryString("codigo_rec")
codigo_prp=Request.QueryString("codigo_prp")
discusion=Request.QueryString("discusion")

	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexion
	objPropuesta.Ejecutar "RegistrarDiscusionPropuestas",false,codigo_prp,discusion
	objPropuesta.CerrarConexion
	response.redirect "registradiscusion.asp?codigo_Rec=" & codigo_Rec & "&discusion=" & discusion & "&codigo_prp=" & codigo_prp
''	Response.Write(codigo_Rec)
end if

if 	 accion="aprueba_prp" then

	codigo_rec=Request.QueryString("codigo_Rec")
	codigo_prp=Request.QueryString("codigo_prp")

	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexion
			objPropuesta.Ejecutar "ModificarEstadoPropuesta",false,"ap",codigo_prp,"A"

	''		Set Rs=objPropuesta.Consultar("ConsultarEmailInvolucradoPropuesta","FO","CA",codigo_prp,0,"")
	objPropuesta.CerrarConexion
	''		do while not rs.eof
	''		Set Obj= Server.CreateObject("AulaVirtual.clsEmail")
	''			Mensajes=Obj.EnviarCorreo("sistemapropuestas@usat.edu.pe",Rs("asunto"),Rs("mensaje"),Rs("email"))
	''		Set Obj=nothing
	''		rs.MoveNext
	''		loop
	''		set rs=nothing	

			response.redirect "presentacion_propuesta.asp?codigo_prp=" & codigo_prp &  "&codigo_rec=" & codigo_rec


end if

if 	 accion="Denegar_prp" then

	codigo_rec=Request.QueryString("codigo_Rec")
	codigo_prp=Request.QueryString("codigo_prp")

	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexionTrans
			objPropuesta.Ejecutar "ModificarEstadoPropuesta",false,"AP",codigo_prp,"R"
''			Set Rs=objPropuesta.Consultar("ConsultarEmailInvolucradoPropuesta","FO","CA",codigo_prp,0,"")
			objPropuesta.CerrarConexionTrans
''			do while not rs.eof
''			Set Obj= Server.CreateObject("AulaVirtual.clsEmail")
''				Mensajes=Obj.EnviarCorreo("sistemapropuestas@usat.edu.pe",Rs("asunto"),Rs("mensaje"),Rs("email"))
''			Set Obj=nothing
''			rs.MoveNext
''			loop
''			set rs=nothing	
			response.redirect "presentacion_propuesta.asp?codigo_prp=" & codigo_prp &  "&codigo_rec=" & codigo_rec


end if
%>
''---------------------------------------------------------------------------------------
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
		set RSfechaprogramada = objPr.Consultar("ConsultarInvolucradoPropuesta","FO","TO",revisor("codigo_ipr"),0)
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