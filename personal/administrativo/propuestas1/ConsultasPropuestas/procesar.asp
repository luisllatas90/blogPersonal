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

accion=request.querystring("accion")

//------------------------------------------------------------	
//    PROCESAR PARA EL NUEVO DISEÑO DEL MÓDULO DE PROPUESTAS
//------------------------------------------------------------

//CAPTURA LOS DATOS EN VARIABLES
accion=Request.QueryString("accion")
codigo_prp=Request.QueryString("codigo_prp")
nombre_prp=Request.QueryString("nombre_prp")
descripcion_prp=Request.QueryString("descripcion_prp")
instancia_prp=Request.QueryString("instancia_prp")
prioridad_prp=trim(Request.QueryString("prioridad_prp"))
codigo_tpr=Request.QueryString("codigo_tpr")
remLen=Request.QueryString("remLen")
codigo_pcc=Request.QueryString("codigo_pcc")
codigo_fac=Request.QueryString("codigo_fac")
modifica=Request.QueryString("modifica")


if accion="guardar" then

nombre=request.form("txtnombre_prp")
descripcion=request.form("txtdescripcion_prp")
tipopropuesta=request.form("cbotipopropuesta")
codigo_cco= request.form("cbocentrodecostos")
attach=Request.QueryString("attach")	 
prioridad_prp=Request.QueryString("prioridad_prp")
//guarda una propuesta como borrador	
Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexionTrans
	
		//REGISTRA LA PROPUESTA
		codigo_prp = objPropuesta.ejecutar("RegistraPropuesta",true,nombre_prp,descripcion_prp,instancia_prp,prioridad_prp,codigo_tpr,0)
	 
		//REGISTRA AL PROPONENTE COMO INVOLUCRADO EN LA PROPUESTA CON TIPO PROPONENTE
		codigo_Ipr = objPropuesta.ejecutar("RegistraInvolucrado",true,codigo_prp,codigo_pcc,"S","A","P","P",0)

		//BUSCA AL DIRECTOR(s) DEL AREA
		
		set rsRevisores=objPropuesta.Consultar("ConsultarResponsablesPropuesta","FO","DI",codigo_pcc,0)
		
		// INSERTA AL DIRECTOR(S) DEL AREA COMO INVOLUCRADO EN LA PROPUESTA, CON INSTANCIA D Y TIPO R
		
		do while not rsRevisores.eof
			director=objPropuesta.ejecutar ("RegistraInvolucrado",true,codigo_prp,rsRevisores(0),"S","A","D","P",0)			 
			rsRevisores.movenext()
		loop

	//BUSCA LOS ENCARGADOS DE REVISAR LA PROPUESTA
	set rsRevisores=objPropuesta.Consultar("ConsultarResponsablesPropuesta","FO","RE",codigo_tpr,codigo_fac)
	// INSERTA LOS ENCARGADOS DE REVISAR LA PROPUETSA COMO REVISORES DE LA PROPUESTA EN PRIMERA INSTANCIA
		do while not rsRevisores.eof
			objPropuesta.ejecutar "RegistraInvolucrado",false,codigo_prp,rsRevisores(0),"S","A","R","R",0
			rsRevisores.movenext()
		loop
		set rsRevisores = nothing
	//BUSCA LOS MIEMBRO DE CONSEJO
	set rsConsejo=objPropuesta.Consultar("ConsultarResponsablesPropuesta","FO","CO",0,0)
	//INSERTA A LOS MIEMBRO DE CONSEJO UNIVERSITARIO COMO REVISORES EN LA INSTANCIA DE CONSEJO
		do while not rsConsejo.eof
			objPropuesta.ejecutar "RegistraInvolucrado",false,codigo_prp,rsConsejo(0),"S","A","C","R",0
			rsConsejo.movenext()
		loop
		set rsConsejo = nothing

	//BUSCA SECRETARIO GENERAL
	set rsSecretaria=objPropuesta.Consultar("ConsultarResponsablesPropuesta","FO","SG",0,0)
	//INSERTA A LOS MIEMBRO DE CONSEJO UNIVERSITARIO COMO REVISORES EN LA INSTANCIA DE CONSEJO
		do while not rsSecretaria.eof
			objPropuesta.ejecutar "RegistraInvolucrado",false,codigo_prp,rsSecretaria(0),"S","A","S","R",0
			rsSecretaria.movenext()
		loop
		set rsSecretaria = nothing
		
	//INSERTA LA PROPUESTA COMO EL PRIMER COMENTARIO
		comentario=objPropuesta.ejecutar ("RegistraComentario",true,codigo_Ipr,"P",0,"P",nombre_prp,descripcion_prp,0)
		objPropuesta.CerrarConexionTrans		

// actualzia si se realizará un cambio de instancia
//--------------------------------------

	 // registra al interesado(proponente como interesado de la propuesta)
 	if Request.QueryString("envio_prp") = 1 then
	//response.write("cierra ventana")
	if instancia_prp= "D" then
		ActualizarFechaRevisionPropuesta codigo_prp,instancia_prp	
	end if
	%>
	
	<script>
		window.opener.location.reload()
		top.window.close()
	  </script>
	<%
	else
		response.redirect "registrapropuesta.asp?codigo_pcc=" & codigo_pcc & "&codigo_tpr=" & codigo_tpr & "&codigo_fac=" & codigo_fac & "&nombre_prp=" & nombre_prp & "&descripcion_prp=" & descripcion_prp & "&codigo_prp=" & codigo_prp & "&remLen=" & remLen & "&instancia_prp=P&prioridad_prp=" & prioridad_prp & "&accion=guardar&attach=" & attach & "&codigo_cop=" & comentario	 & "&modifica=" & modifica
	end if
		 
end if	

if 	 accion="actualizar" then

nombre=request.form("txtnombre_prp")
descripcion=request.form("txtdescripcion_prp")
tipopropuesta=request.form("cbotipopropuesta")
codigo_cco= request.form("cbocentrodecostos")
comentario=Request.QueryString("codigo_cop")
attach=Request.QueryString("attach")
instancia_prp=Request.QueryString("instancia_prp")

Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexionTrans
	objPropuesta.ejecutar "ActualizarPropuesta",false,"TO",codigo_prp,nombre_prp,descripcion_prp,instancia_prp,prioridad_prp,codigo_tpr,0	
	objPropuesta.CerrarConexionTrans	

 	if Request.QueryString("envio_prp") = 1 then
		if instancia_prp= "D" then
			ActualizarFechaRevisionPropuesta codigo_prp,instancia_prp	
		end if%>
	<script>

		window.opener.location.reload()
		window.close()
	  </script>
<%	else
		response.redirect "registrapropuesta.asp?codigo_pcc=" & codigo_pcc & "&codigo_tpr=" & codigo_tpr & "&codigo_fac=" & codigo_fac & "&nombre_prp=" & nombre_prp & "&descripcion_prp=" & descripcion_prp & "&codigo_prp=" & codigo_prp & "&remLen=" & remLen & "&instancia_prp=" & instancia_prp & "&prioridad_prp=" & prioridad_prp & "&accion=actualizar&codigo_cop=" & comentario  & "&modifica=" & modifica
	end if


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

	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexionTrans
	codigo_cop=objPropuesta.ejecutar ("RegistraComentario",true,codigo_ipr,nivelrestriccion_cop,codigorespuesta_cop,estado_cop,asunto_cop,comentario_cop,0	)
	objPropuesta.CerrarConexionTrans	
	
 	if Request.QueryString("envio_cop") = 1 then%>
	<script>
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

	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexionTrans
	objPropuesta.Ejecutar "ActualizaComentario",FALSE,codigo_cop,estado_cop,asunto_cop,comentario_cop
	objPropuesta.CerrarConexionTrans	
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

	//RegistraPropuesta=codigo_prp	 
	//If Err.Number<>0 then
	//	objPropuesta.CancelarConexionTrans
	//	response.redirect "../../../../error.asp?Numero=" & Err.Number & "&Recurso=" & Err.Source & "&Descripcion=" & Err.description & "&rutaPagina=" & Request.ServerVariables("SCRIPT_NAME")		
	//End If	


//-------------------------------------------------------------------------------
//----PARA VALIDAR EL VEREDICTO DEL DIRECTOR DEL ÁREA
//-------------------------------------------------------------------------------
	
if accion="veredicto" then
usuario=session("codigo_Usu")
codigo_prp=Request.QueryString("codigo_prp")
veredicto=Request.QueryString("veredicto")
veredicto2=Request.QueryString("veredicto2")
menu=Request.QueryString("menu")
instanciaRevisores=Request.QueryString("instanciaRevisores")

Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
objPropuesta.AbrirConexionTrans
//consulta todas la veces que el personalpartcipa en una propuesta en las distintas instancias
set RsInvolucrados=objPropuesta.consultar("ConsultarInvolucradoPropuesta","FO","IN",codigo_prp,usuario)

//con esto se actualiza el veredicto en todas las instancias que el revisor interviene
//recorre a todos los involucrados detectados
Do While not RsInvolucrados.EOF
	//actualiza el veredicto del revisor en la instancia correspondiente
	objPropuesta.ejecutar "ActualizaVerdictoRevisor",false,"CP",usuario,codigo_prp,RsInvolucrados("instancia_Ipr"),veredicto	
	RsInvolucrados.MoveNext
loop	
// si quien da el veredicto es un director al dar su conformidad debe pasar a instancia revisor
if (instanciaRevisores ="D") and (veredicto="C") AND (veredicto2<>"C")then

//	ActualizarFechaRevisionPropuesta codigo_prp, "R"
//---------------------------------------------------------------------------------------------------------
//ACTUALIZA LOS TIEMPOS DE INICIO Y FIN DE REVISION DE LOS REVISORES DE LA PROPUESTA
	Set Rsdays = objPropuesta.Consultar("ConsultarParametrosPropuesta","FO","IN","R")
	dia=CINT(Rsdays("valor_ppr"))	 
	set Rsdays= nothing	
	//calcual la fecha finla de revisón de propuesta en días hábiles
	fin=FechaFinHabil (FormatDateTime(date(),2),dia)
	response.write(fin)	
	//CONSULTAR TODOS LOS REVISORES DE UNA PROPUESTA SEGUN SU INSTANCIA
	//Set Rsdays1 = objPr.Consultar("ConsultarParametrosPropuesta","FO","IN",instancia)
	//RESPONSE.WRITE RSDAYS1(0)			 
	//consulta quienes son los que revisarán la propuesta según la instancia
	set revisor=objPropuesta.Consultar("ConsultarResponsablesPropuesta","FO","RC",cint(codigo_prp),0)
	do while not revisor.eof
		if revisor("instancia_Ipr") = "R" then
			//ACTUALIZAR FECHAS DE INICIO Y FIN  DE REVISON PARA DIRECTORES		
			response.write(revisor("codigo_ipr")) & "<BR>"			
			objPropuesta.ejecutar "ActualizarFechasRevisionPropuesta",false,revisor("codigo_ipr"), date(), fin
		end if
	revisor.MoveNext
	loop
//---------------------------------------------------------------------------------------------------------
	objPropuesta.ejecutar "ActualizarPropuesta", false, "VE", codigo_prp,0,0, "R",0,0	
else
//response.write("no se actualizó")	
end if

//
if instanciaRevisores="R" and (veredicto="C") AND (veredicto2<>"C") then
//CONSULTAR LOS VERDICTOS DE LOS TODOS LOS REVISORES
	SET rsVeredictos = objPropuesta.consultar ("VerificarVerdicto","FO","SE",0,codigo_prp," ")
	cambio=1
	do while not rsVeredictos.eof						
		if rsVeredictos(0)="C" then
			cambiar=1
		else
			cambiar=2
		end if
		cambio=cambio*cambiar
		rsVeredictos.movenext()	
	loop					  		  
	if cambio = 1 then
		objPropuesta.ejecutar "ActualizarPropuesta", false, "VE", codigo_prp,0,0, "S",0,0
//		ActualizarFechaRevisionPropuesta codigo_prp, "S"
//	ACTUALIZA LA FECHA DE REVISON DEL SECRETARIO GENERAL
//----------------------------------------------------------------
		//consulta cuantos días le corresponde para  revisar la propuesta según la instancia
		Set Rsdays = objPropuesta.Consultar("ConsultarParametrosPropuesta","FO","IN","S")
		dia=CINT(Rsdays("valor_ppr"))	 
		set Rsdays= nothing	
		//calcual la fecha finla de revisón de propuesta en días hábiles
		fin=FechaFinHabil (FormatDateTime(date(),2),dia)
		response.write(fin)	
		//CONSULTAR TODOS LOS REVISORES DE UNA PROPUESTA SEGUN SU INSTANCIA
		//consulta quienes son los que revisarán la propuesta según la instancia
		set revisor=objPropuesta.Consultar("ConsultarResponsablesPropuesta","FO","RC",cint(codigo_prp),0)
		do while not revisor.eof
			if revisor("instancia_Ipr") = "S" then
				//ACTUALIZAR FECHAS DE INICIO Y FIN  DE REVISON PARA DIRECTORES			
				objPropuesta.ejecutar "ActualizarFechasRevisionPropuesta",false,revisor("codigo_ipr"), date(), fin
			end if
		revisor.MoveNext
		loop
//------------------------------------------------------------------
	else
	//	response.write(cambio)
	end if
end if

	  
//		''verifica el veredicto consultándodolo según el usuario que se ha logueado, el codigo_prp y la instancia en alq ue se encuentra	  	
//			SET VeredictoActual=objVeredicto.consultar ("VerificarVerdicto","FO","VE",usuario,prop,REQUEST.querystring("instancia"))
//			veredicto1=VeredictoActual(0)
//			''response.write(veredicto1)
//			objVeredicto.CerrarConexion
//			set objVeredicto=nothing 		  
objPropuesta.CerrarConexionTrans
Response.Redirect("contenido.asp?veredicto=" & veredicto2 & "&menu=" & menu)

end if

//------------------------------------------------------------------------------------------
//SE USA CUANDO SE DESEA REGISTRAR LAS FECHAS QUE TOEN EL REVISOR PARA REVISAR LA PROPUESTA
//------------------------------------------------------------------------------------------
Sub ActualizarFechaRevisionPropuesta(codigo_prp,instancia)
dim revisor
//response.write(codigo_prp)
//RESPONSE.WRITE INSTANCIA

Set objPr=Server.CreateObject("PryUSAT.clsAccesoDatos")
objPr.AbrirConexion	
	//consulta cuantos días le corresponde para  revisar la propuesta según la instancia
	Set Rsdays = objPr.Consultar("ConsultarParametrosPropuesta","FO","IN",instancia)
	dia=CINT(Rsdays("valor_ppr"))	 
	set Rsdays= nothing	
	//calcual la fecha finla de revisón de propuesta en días hábiles
	fin=FechaFinHabil (FormatDateTime(date(),2),dia)
	response.write(fin)	
	//CONSULTAR TODOS LOS REVISORES DE UNA PROPUESTA SEGUN SU INSTANCIA
	//Set Rsdays1 = objPr.Consultar("ConsultarParametrosPropuesta","FO","IN",instancia)
	//RESPONSE.WRITE RSDAYS1(0)			 
	//consulta quienes son los que revisarán la propuesta según la instancia
	set revisor=objPr.Consultar("ConsultarResponsablesPropuesta","FO","RC",cint(codigo_prp),0)
	do while not revisor.eof
		if revisor("instancia_Ipr") = instancia then
			//ACTUALIZAR FECHAS DE INICIO Y FIN  DE REVISON PARA DIRECTORES		
			//response.write(revisor("codigo_ipr")) & "<BR>"			
			ObjPr.ejecutar "ActualizarFechasRevisionPropuesta",false,revisor("codigo_ipr"), date(), fin
		end if
	revisor.MoveNext
	loop
objPr.CerrarConexion	
		
End Sub


//If Err.Number<>0 then
	//	objPropuesta.CancelarConexionTrans
	//	response.redirect "../../../error.asp?Numero=" & Err.Number & "&Recurso=" & Err.Source & "&Descripcion=" & Err.description & "&rutaPagina=" & Request.ServerVariables("SCRIPT_NAME")
		
//End If	
Set objPropuesta=nothing
%>