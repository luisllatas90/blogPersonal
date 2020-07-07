<!--#include file="../../../../funciones.asp"-->


<html>
<head>
<title>Registro de Versi&oacute;n de una Propuesta</title>

<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--
body {
	background-color: #f0f0f0;
}
.Estilo5 {
	font-size: 9px;
	color: #990000;
}
.Estilo7 {
	color: #000000;
	font-weight: bold;
}
.Estilo8 {color: #333333}
.Estilo9 {color: #000000}
.Estilo10 {
	color: #990000;
	font-weight: bold;
	font-size: 10pt;
}
.Estilo2 {color: #990000;
	font-weight: bold;
}
div#commentForm {margin: 0px 0px 0px 0px;
display:block;
position:absolute;
left:0px;
top:0Px;
}
-->
</style></head>
<script language="JavaScript" src="private/validarpropuestas.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/tooltip.js"></script>
<script>
	function darprioridad()
	{
	/*	var codigo_pcc=frmpropuesta.cbocentrodecostos.value
		var codigo_tpr=frmpropuesta.cbotipopropuesta.value
		var codigo_fac=frmpropuesta.cbofacultad.value
		var nombre_prp=frmpropuesta.txtnombre_prp.value
		var descripcion_prp=frmpropuesta.txtdescripcion_prp.value
		var codigo_prp=frmpropuesta.txtcodigo_prp.value
		var remLen=frmpropuesta.remLen.value
		var codigo_cop = frmpropuesta.txtcodigo_cop.value
		var instancia_prp=frmpropuesta.txtinstancia.value
		var modifica=frmpropuesta.txtmodifica.value		
		remLen

		//frmpropuesta.cmdprioridad.disabled=true
		var mensaje=confirm("¿Desea dar prioridad alta a esta propuesta?")
		if (mensaje==true){
		//frmpropuesta.txtprioridad_prp.value='A'
		location.href="registrapropuesta.asp?codigo_pcc=" + codigo_pcc + "&codigo_tpr=" + codigo_tpr + "&codigo_fac=" + codigo_fac + "&nombre_prp=" + nombre_prp + "&descripcion_prp=" + descripcion_prp + "&codigo_prp=" + codigo_prp + "&remLen=" + remLen + "&prioridad_prp=A"  + "&codigo_cop=" + codigo_cop + "&instancia_prp=" + instancia_prp + "&modifica=" + modifica
		}*/
		frmpropuesta.txtprioridad_prp.value='A'
		frmpropuesta.prior.style.display=""
	}

	function SalirVersion(){
	var codigo_dap=frmpropuesta.txtcodigo_dap.value
		if (codigo_dap==''){
			if (confirm('No ha registrado una versión, ¿Desea salir?')==true) {
				window.close()
		
			}
		}
		else{
		window.close()
		}
	}	
	function Validar(modo)
	{	
		var codigo_pcc=frmpropuesta.cbocentrodecostos.value
		var codigo_tpr=frmpropuesta.cbotipopropuesta.value
		var codigo_fac=frmpropuesta.cbofacultad.value
		var nombre_prp=frmpropuesta.txtnombre_prp.value
		var codigo_prp=frmpropuesta.txtcodigo_prp.value	
		var prioridad_prp=frmpropuesta.txtprioridad_prp.value
//		var codigo_cop = frmpropuesta.txtcodigo_cop.value
		var instancia_prp=frmpropuesta.txtinstancia.value				
		var modifica=frmpropuesta.txtmodifica.value
		var ingresos=frmpropuesta.txtIngresos.value
		var egresos=frmpropuesta.txtEgresos.value
		var beneficiarios=frmpropuesta.txtbeneficiarios.value
		var importancia=frmpropuesta.txtimportancia.value
		var codigo_dap=frmpropuesta.txtcodigo_dap.value
//			alert(modifica)	
		var cadena = "Ingrese los datos: "		
	
		if(frmpropuesta.txtinstancia.value==""){
		frmpropuesta.txtinstancia.value="P"
		}
		
		if (codigo_pcc<0){
		cadena = cadena + "Área | "
		}
		if (codigo_tpr<0){
		cadena = cadena + "Tipo de Propuesta | "
		}		
//		if (codigo_fac<0){
//		cadena = cadena + "Facultad | "
//		}
		if (nombre_prp==""){
		cadena = cadena + "Nombre de Propuesta | "
		}
		if (ingresos==""){
		cadena = cadena + "Ingresos | "
		}		

		if (egresos==""){
		cadena = cadena + "Egresos | "
		}		
		if (beneficiarios==""){
		cadena = cadena + "Beneficiarios | "
		}		

		if (importancia==""){
		cadena = cadena + "Importancia | "
		}		

		if (cadena=="Ingrese los datos: "){
		
		 	switch(modo)
			{						
			case "A":
			//					alert(modifica)
			if (codigo_prp==""){
			//frmpropuesta.txtcodigo_prp.value="123456"
				//	location.href="procesar.asp?codigo_pcc=" + codigo_pcc + "&codigo_tpr=" + codigo_tpr + "&codigo_fac=" + codigo_fac + "&nombre_prp=" + nombre_prp + "&descripcion_prp=" + descripcion_prp + "&codigo_prp=" + codigo_prp + "&remLen=" + remLen + "&instancia_prp=P&prioridad_prp=" + prioridad_prp + "&accion=guardar&attach=1" + "&codigo_cop=" + codigo_cop + "&modifica=" + modifica
					// popUp('adjuntar.asp?codigo_prp=' + codigo_prp)
			}
			else{
					//location.href="procesar.asp?codigo_pcc=" + codigo_pcc + "&codigo_tpr=" + codigo_tpr + "&codigo_fac=" + codigo_fac + "&nombre_prp=" + nombre_prp + "&descripcion_prp=" + descripcion_prp + "&codigo_prp=" + codigo_prp + "&remLen=" + remLen + "&instancia_prp=" + instancia_prp + "&prioridad_prp=" + prioridad_prp + "&accion=actualizar" + "&codigo_cop=" + codigo_cop + "&modifica=" + modifica
					// popUp('adjuntar.asp?codigo_prp=' + codigo_prp)			
					popUp('../../../../libreriaNet/propuestas/adjuntar.aspx?codigo_prp=' + codigo_prp + '&codigo_dap=' + codigo_dap + '&modifica=' + modifica)
					window.opener.location.reload()
					//alert("error")
				}
			break
			case "B":
			
			if (codigo_dap==""){			
			var guardaborrador=false
			//guardaborrador=confirm ("¿Desea guardar la propuesta como BORRADOR?, esta acción NO REPRESENTA EL ENVÍO de su propuesta a la instancia siguiente.")		
			//	if (guardaborrador==true){
					frmpropuesta.action="procesar.asp?accion=nuevaVersion&estado=B"   //  (instancia B) proponente que guarda como borrador |  (instancia P) proponente que envìa como director
					frmpropuesta.submit()
					window.opener.location.reload()
					alert ("Se ha registrado una nueva versión de la propuesta, ADJUNTE LOS ARCHIVOS, REGISTRE LAS ACTIVIDADES y cierre la ventana")
					
					//location.href="procesar.asp?codigo_pcc=" + codigo_pcc + "&codigo_tpr=" + codigo_tpr + "&codigo_fac=" + codigo_fac + "&nombre_prp=" + nombre_prp + "&descripcion_prp=" + descripcion_prp + "&codigo_prp=" + codigo_prp + "&remLen=" + remLen + "&instancia_prp=P&prioridad_prp=" + prioridad_prp + "&accion=guardar"	+ "&codigo_cop=" + codigo_cop 	+ "&modifica=" + modifica			
			//	}	
			}
			else{
			//guardaborrador=confirm ("¿Desea guardar la propuesta como BORRADOR?, esta acción NO REPRESENTA EL ENVÍO de su propuesta a la instancia siguiente.")		
			//	if (guardaborrador==true){

					frmpropuesta.action="procesar.asp?accion=actualizarversion&estado=B&codigo_dap="+codigo_dap+"&codigo_prp="+codigo_prp   //  (instancia B) proponente que guarda como borrador |  (instancia P) proponente que envìa como director
					frmpropuesta.submit()
					window.opener.location.reload()
					alert ("Se ha registrado la información de la propuesta, ADJUNTE LOS ARCHIVOS, REGISTRE LAS ACTIVIDADES y cierre la ventana")
					//location.href="procesar.asp?codigo_pcc=" + codigo_pcc + "&codigo_tpr=" + codigo_tpr + "&codigo_fac=" + codigo_fac + "&nombre_prp=" + nombre_prp + "&descripcion_prp=" + descripcion_prp + "&codigo_prp=" + codigo_prp + "&remLen=" + remLen + "&instancia_prp=P&prioridad_prp=" + prioridad_prp + "&accion=guardar"	+ "&codigo_cop=" + codigo_cop 	+ "&modifica=" + modifica			
			//	}	
			}
			
			break
			case "D":
			if (codigo_prp==""){
			enviarpropuesta=confirm ("¿Desea ENVIAR la propuesta al Director del Área del: " + frmpropuesta.cbocentrodecostos.options [frmpropuesta.cbocentrodecostos.selectedIndex].innerText + "?")		
				if (enviarpropuesta==true){
					frmpropuesta.action="procesar.asp?accion=nuevo&instancia=P" //  (instancia B) proponente que guarda como borrador |  (instancia P) proponente que envìa como director
					frmpropuesta.submit()
				}				
			}
			
			else{
					if (instancia_prp=="P"){
					instancia_prp="D"
					}
					location.href="procesar.asp?codigo_pcc=" + codigo_pcc + "&codigo_tpr=" + codigo_tpr + "&codigo_fac=" + codigo_fac + "&nombre_prp=" + nombre_prp + "&descripcion_prp=" + descripcion_prp + "&codigo_prp=" + codigo_prp + "&remLen=" + remLen + "&instancia_prp=" + instancia_prp + "&prioridad_prp=" + prioridad_prp + "&accion=actualizar" + "&codigo_cop=" + codigo_cop  + "&envio_prp=1"
			//alert ("Todo OK, cambiar de estado a D")
			}
			//window.close() 
			break
			}		
		}
		else{
		alert (cadena)
		}		
	}	
	
	function popUp(URL) {
	day = new Date();
	id = day.getTime();
	var izq = 300//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=400,height=390,left = "+ izq +",top = "+ arriba +"');");
	}
	function calcularUtilidad(){
	var ingreso = document.all.txtIngresos.value
	var egreso = document.all.txtEgresos.value
	var ingresoMN = document.all.txtIngresosMN.value
	var egresoMN = document.all.txtEgresosMN.value	
	var cambio= document.all.txtCambio.value.substring(4,document.all.txtCambio.value.length)
//	.substring(4,document.all.txtCambio.value.length)
	utilidad=ingreso-egreso

	document.all.txtUtilidad.value=Math.round(utilidad*100)/100
	//document.all.txtUtilidadMN.value=Math.round(document.all.txtUtilidadMN*100)/100	
	if (utilidad<0) {
		document.all.txtUtilidad.className='rojo'
		document.all.txtUtilidadMN.className='rojo'		
	}
	else{
		document.all.txtUtilidad.className='azul'
		document.all.txtUtilidadMN.className='azul'		
	}

	if(document.all.chkdolar.checked==true){

//		alert(document.all.txtCambio.value)
		document.all.txtIngresosMN.value=document.all.txtIngresos.value* cambio
		document.all.txtEgresosMN.value= document.all.txtEgresos.value * cambio
		//alert(document.all.chkdolar.checked)
	}
	else{

		//alert(document.all.txtCambio.value)
		document.all.txtIngresosMN.value=document.all.txtIngresos.value
		document.all.txtEgresosMN.value= document.all.txtEgresos.value
		}
		document.all.txtUtilidadMN.value=Math.round((document.all.txtIngresosMN.value-document.all.txtEgresosMN.value)	*100)/100
	}
	
	function verificaMoneda(check){
	//
				var cambio= document.all.txtCambio.value.substring(4,document.all.txtCambio.value.length)
	if(document.all.chkdolar.checked==true){
		document.all.lblmoneda1.innerHTML="&nbsp;&nbsp;$"
		document.all.lblmoneda2.innerHTML="&nbsp;&nbsp;$"		
		document.all.lblmoneda3.innerHTML="&nbsp;&nbsp;$"		
		document.all.txtCambio.style.visibility="visible"
//		alert(document.all.txtCambio.value)
		document.all.txtIngresosMN.value=document.all.txtIngresosMN.value * cambio
		document.all.txtEgresosMN.value= document.all.txtEgresosMN.value * cambio
		document.all.txtUtilidadMN.value=	document.all.txtUtilidad.value	* cambio
		//alert(document.all.chkdolar.checked)
	}
	else{
		document.all.lblmoneda1.innerHTML='S/.'	
		document.all.lblmoneda2.innerHTML='S/.'	
		document.all.lblmoneda3.innerHTML='S/.'	
		document.all.txtCambio.style.visibility="hidden"		
//		alert(document.all.txtCambio.value)
		document.all.txtIngresosMN.value=document.all.txtIngresos.value
		document.all.txtEgresosMN.value= document.all.txtEgresos.value
		document.all.txtUtilidadMN.value=	document.all.txtUtilidad.value		
		}	

			//.substring(4,document.all.txtCambio.length)
			//alert(cambio)			
//	if (chkdolar.checked==true){
//		alert('aaaa')
//	}
	
	}
	
function minimizarGrid(){
    var posx = 0;
    var posy = 0;
    if (!e) var e = window.event;
    if (e.pageX || e.pageY)
    {
        posx = e.pageX;
        posy = e.pageY;
    }
    else if (e.clientX || e.clientY)
    {
        posx = e.clientX;
        posy = e.clientY;
    }
if ((posy<=80)){
	//alert(posy)
	document.all.TablaInfoAyuda.style.display="none"
	//alert (document.all.TablaInfoAyuda.style.display)
	document.all.commentForm.style.width="40%"
	document.all.commentForm.style.left="60%"
	}
	else{
	document.all.TablaInfoAyuda.style.display="block"
	document.all.commentForm.style.width="100%"
	document.all.commentForm.style.left="0"
	}
}
	
</script>
<script LANGUAGE="JavaScript">
<!-- Begin
function textCounter(field, countfield, maxlimit) {
if (field.value.length > maxlimit) // if too long...trim it!
field.value = field.value.substring(0, maxlimit);
else 
countfield.value = maxlimit - field.value.length;
}
// End -->
</script>


	  
<body topmargin="0" rightmargin="0" leftmargin="0">
<div id="commentForm"  style="width:100%; height:80px" onMouseOver="minimizarGrid()" onMouseOut="minimizarGrid()"  >
  <table  cellpadding="2"  bgcolor="#FFFFCC" width="100%" style="border: 1px solid #808080; filter:alpha(opacity=90)">
    <tr>
      <td align="center" valign="baseline"><table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td width="3%" align="right" class="piepagina"><img src="../../../../images/help.gif" alt="Ayuda" width="22" height="17" border="0" /></td>
          <td width="88%" align="left" class="piepagina"><span class="Estilo2">&nbsp;&nbsp;Ayuda on line: <b><span style="color:#000000">Pasos para Registar una Propuesta </span></b></span></td>
          <td width="9%" align="right" valign="top" class="piepagina"><img src="../../../../images/menus/Cerrar_s.gif" alt="Cerrar" width="16" height="16" style="cursor:hand" title="Cerrar" onClick="document.all.commentForm.style.display='none'" ></td>
        </tr>
      </table></td>
    </tr>
    <tr ID="TablaInfoAyuda" >
      <td valign="baseline"  ><p> <span class="piepagina"><span style="color:#990000"><b>Paso 01 :</b></span></span> <span class="piepagina" style="color:#000000;text-align:justify"> <strong>Ingresar los datos</strong> de la propuesta, ingresos, egresos, moneda, beneficiarios, importancia. <br />
                <span class="Estilo2">Paso 02:</span> Hacer <strong>Clic</strong> en el Bot&oacute;n &quot;<strong>Guardar</strong>&quot;, para registrar la informaci&oacute;n y ativar el bot&oacute;n que permite adjuntar los archivos de la propuesta. <br />
                <span class="Estilo2">Paso 03:</span> Hacer clic en el Bot&oacute;n &quot;<strong>Adjuntar</strong>&quot;, para subir los archivos relacionados y en el Bot&oacute;n &quot;<strong>Actividades</strong>&quot; para registrar las actividades. </span><br />
                <span class="piepagina"><span class="Estilo2">Paso 04:</span> <span class="Estilo9">Si ha realizado cambios en la informaci&oacute;n hacer <strong>Clic</strong> nuevamente en el Bot&oacute;n &quot;<strong>Guardar</strong>&quot;. Para <strong>Finalizar</strong> el proceso <strong>Cierre la ventana</strong>. </span></span></p></td>
    </tr>
  </table>
</div>
<form method="post" name="frmpropuesta" id="frmpropuesta">


<%

	codigo_prp=Request.QueryString("codigo_prp")
	codigo_dap=Request.QueryString("codigo_dap")
	if (codigo_prp<>"") then '' si ya hay una propuesta cargada para modificar
	%>

		<%''consultar los datos de la propuesta
		 	Modificar="SI"
			Set P=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	P.AbrirConexion	
			set RsPropuesta_=P.Consultar ("ConsultarPropuestas","FO","cp",0,0,codigo_prp,0,0)
			centrocosto=RsPropuesta_("codigo_cco")
			nombre_prp=RsPropuesta_("nombre_prp")
			codigo_tpr=RsPropuesta_("codigo_tpr")
			codigo_fac=RsPropuesta_("codigo_fac")
			prioridad_prp=RsPropuesta_("prioridad_prp")
			instancia_prp=RsPropuesta_("instancia_prp")
			Response.Write(prioridad)
			
			set RsVersiones_=P.Consultar ("CONSULTARVERSIONESPROPUESTA","FO","ES",codigo_prp,0)			
			ingreso_dap=RsVersiones_("ingreso_dap")
			egreso_dap=RsVersiones_("egreso_dap")
			utilidad_dap=RsVersiones_("utilidad_dap")
			beneficios_dap=RsVersiones_("beneficios_dap")
			importancia_dap=RsVersiones_("importancia_dap")
			version_dap=RsVersiones_("version_dap")
			fechaActualizacion_dap=RsVersiones_("fechaActualizacion_dap")
			tipocambio_dap=RsVersiones_("tipocambio_dap")
			moneda_dap=RsVersiones_("moneda_dap")
			ingresoMN_dap=RsVersiones_("ingresoMN_dap")
			egresoMN_dap=RsVersiones_("egresoMN_dap")
			utilidadMN_dap=RsVersiones_("utilidadMN_dap")
			
		''consultar datos de la version
			P.CerrarConexion
	end if

		


	modifica= Request.QueryString("modifica")
	if modifica = "" then modifica=0

	
	if Request.QueryString("attach") <>"" then
		attach=1
	else
		attach=0
	end if
	if attach=1 then%>
		<script>
		popUp("../../../../libreriaNet/propuestas/adjuntar.aspx?codigo_prp=" + <%=codigo_prp%> + "&codigo_cop=" +<%=codigo_cop%> + "&modifica=" +<%=modifica%>)
		</script>
		
	<%end if%>
	<span class="Estilo7">
	<input name="txtcodigo_prp" type="hidden" id="txtcodigo_prp" value="<%=codigo_prp%>" maxlength="3">
	</span>
	<span class="Estilo7">
	<input name="txtcodigo_dap" type="hidden" id="txtcodigo_dap" value="<%=codigo_dap%>" maxlength="1">
	</span>
	<input name="txtinstancia" type="hidden" id="txtinstancia" value="<%=instancia_prp%>" maxlength="3">
	<input name="txtmodifica" type="hidden" id="txtmodifica" value="<%=Request.QueryString("modifica")%>" maxlength="3">
	<input name="txtprioridad_prp" type="hidden" id="txtprioridad_prp" value="<%=prioridad_prp%>">
	<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td class="bordeinf"><table width="97%" border="0" align="center" cellpadding="0" cellspacing="5">
      <tr>
        <td> <input onClick="Validar('B')"  name="cmdborrador" type="button" class="enviarpropuesta" id="cmdborrador" value="         Guardar" 
		<%if modifica=1 then 
			if Request.QueryString("instancia_prp") <> "P" then%>	
				disabled="disabled" 
			<%end if%>
		<%end if%>>
          &nbsp;&nbsp;
          <input onClick="Validar('A')" name="cmdadjuntar" type="button" class="attach_" id="cmdadjuntar" value="        Adjuntar" <%if codigo_dap="" then%> disabled="disabled" <%end if%>>
		            <input onClick="SalirVersion()" name="cmdSalir" type="button" class="salir_prp" id="cmdSalir" value="            Salir" >
  &nbsp;</td>
		  </tr>
    </table></td>
  </tr>  
  <tr>
    <td valign="top"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td align="center" valign="top">
            <table width="95%" border="0" align="center" cellpadding="2" cellspacing="2">
              <tr  height="3">
                <td colspan="2" valign="top"></td>
                <td colspan="3" valign="top"></td>
              </tr>
              <tr>
                <td colspan="2" valign="middle">Nombre de propuesta <img src="../../../../images/menus/prioridad_.gif" name="prior" width="18" height="17" id="prior"  <%if prioridad_prp<>"A" then%> style="display:none" <%end if%>> </td>
                <td colspan="3" valign="top"><input name="txtnombre_prp" type="text" class="Cajas2" id="txtnombre_prp" tabindex="1" value="<%=nombre_prp%>" size="65" maxlength="150" ></td>
              </tr>
              <tr>
                <td colspan="2" valign="middle">Área </td>
                <td colspan="3" valign="top"><% 
				
		 	Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC.AbrirConexion
				''consulta los centros de costos a los que está asigando el trabajador
				if centrocosto<>""  then
					centrocosto=centrocosto
				else
				set rsCentroCostos=objCC.Consultar("ConsultarCentroCosto","FO","PR",session("codigo_Usu"))
					centrocosto=rsCentroCostos("codigo_cco")

				end if
				set rsCentrosdeCostos=objCC.Consultar("ConsultarCentroCosto","FO","TO",0)
			ObjCC.CerrarConexion
			set objCC=nothing

		 	CALL llenarlista("cbocentrodecostos","",rsCentrosdeCostos,"codigo_cco","descripcion_Cco",centrocosto,"Seleccionar Área","","")
			set rsCentroCostos = nothing
		 %></td>
                </tr>
              <tr>
                <td colspan="2" valign="middle">Tipo Propuesta </td>
                <td width="44%" valign="top"><% 
		 	Set objCC1=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC1.AbrirConexion
			set rsTipoPropuesta=objCC1.Consultar("ConsultarTipoPropuestas","FO","TO")
	    	ObjCC1.CerrarConexion
			set objCC1=nothing

		
		 	call llenarlista("cbotipopropuesta","",rsTipoPropuesta,"codigo_tpr","descripcion_Tpr",codigo_tpr,"Seleccionar Tipo de Propuesta","","")


			set rsTipoPropuesta = nothing
			 %></td>
                <td width="1%" align="right" valign="top">Facultad</td>
                <td width="35%" valign="top"><% 
		 	Set objCC2=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC2.AbrirConexion
			set rsFacultad=objCC2.Consultar("ConsultarFacultad","FO","TO","")
	    	ObjCC2.CerrarConexion
			set objCC2=nothing
		
		 	call llenarlista("cbofacultad","",rsFacultad,"codigo_fac","nombre_fac",codigo_fac,"Todas las Facultades","","")
			set rsFacultad = nothing
		 
		 			if Modificar="SI" then%> 

		<script>
		frmpropuesta.cbocentrodecostos.disabled =true
		frmpropuesta.cbotipopropuesta.disabled =true
		frmpropuesta.cbofacultad.disabled=true
		frmpropuesta.txtnombre_prp.disabled=true
			
		</script>
		<%	end if
		 %></td>
              </tr>
              <tr>
                <td colspan="5" valign="middle"><hr style="color:#990000; border:1"></td>
                </tr>
              <tr>
                <td colspan="5" align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="20%">
<%			Set objCambio=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	objCambio.AbrirConexion
				set Rscambio=objCambio.Consultar("ConsultarCambioDelDia","FO",cint(2))
	    	objCambio.CerrarConexion
			set objCambio=nothing					
					
			%>					 
                      D&oacute;lares
                        <input name="chkdolar" type="checkbox" id="chkdolar" onClick="verificaMoneda('this')" <%if moneda_dap=2 then%> checked="checked" <%end if%>>
																											
                        <input name="txtCambio" type="text" id="txtCambio" value="T.C. &nbsp;<%=Replace(formatNumber(iif(moneda_dap<>2,Rscambio(3),tipocambio_dap),2,false),",",".")%>" size="6" style="border:none; <%if moneda_dap<>2 then%> visibility:hidden; <%end if%> background-color:#F0F0F0; color:#0000FF" readonly="readonly" tooltip="<b>Tipo de Cambio</b><br>Al <%=iif(moneda_dap<>2,date(),formatdatetime(fechaActualizacion_dap,1))%>, asigando por Contabilidad"></td>
                    <td width="16%" align="right"><span class="Estilo8">Ingresos</span>                      <span class="etiqueta cursoM" id="lblmoneda1" style="text-align:right" name="lblmoneda1">S/.</span></td>
                    <td width="13%"><input name="txtIngresos" type="text" class="Cajas" id="txtIngresos" size="9" maxlength="9"  onKeyPress="validarnumero()" onKeyUp="calcularUtilidad();"  style="text-align:right" value="<%=REPLACE(ingreso_dap,",",".")%>"></td>
                    <td width="11%" align="right"><span class="Estilo9">Egresos</span>					 <span class="etiqueta cursoM" id="lblmoneda2" style="text-align:right" name="lblmoneda2">S/.</span>					</td>
                    <td width="11%"><input name="txtEgresos" type="text" class="Cajas" id="txtEgresos" size="9" maxlength="9" onKeyPress="validarnumero()" onKeyUp="calcularUtilidad()" style="text-align:right"  value="<%=REPLACE(Egreso_dap,",",".")%>"></td>
                    <td width="13%" align="right"><span class="Estilo7">Utilidad</span>
					 <span class="etiqueta cursoM" id="lblmoneda3" style="text-align:right" name="lblmoneda3">S/.</span>					</td>
                    <td width="16%"><input name="txtUtilidad" type="text"  id="txtUtilidad" size="12" maxlength="12" onKeyPress="validarnumero()"  style="text-align:center; border:none; background-color:#F0F0F0" readonly="readonly"   value="<%=iif(utilidad_dap="",0,REPLACE(utilidad_dap,",","."))%>"></td>
                    </tr>
                  <tr>
                    <td> <span class="Estilo7">En Moneda Nacional: </span></td>
                    <td align="right"><span class="Estilo9"><strong>Ingresos S/.</strong> </span></td>
                    <td><input name="txtIngresosMN" type="text" readonly="readonly" class="Cajas" id="txtIngresosMN" style="text-align:right; border:none; background-color:#F0F0F0" size="9" maxlength="9"  onKeyPress="validarnumero()" onKeyUp="calcularUtilidad();"  style="text-align:right" value="<%=REPLACE(IngresoMN_dap,",",".")%>"></td>
                    <td align="right"><span class="Estilo9"><strong>Egresos S/.</strong> </span></td>
                    <td><input name="txtEgresosMN" type="text" readonly="readonly" class="Cajas" id="txtEgresosMN"  style="text-align:right; border:none; background-color:#F0F0F0" size="9" maxlength="9" onKeyPress="validarnumero()" onKeyUp="calcularUtilidad()" style="text-align:right"  value="<%=REPLACE(EgresoMN_dap,",",".")%>"></td>
                    <td align="right"><span class="Estilo9"><strong>Utilidad S/. </strong></span></td>
                    <td> <input name="txtUtilidadMN" type="text"  id="txtUtilidadMN"  style="text-align:center; border:none; background-color:#F0F0F0" onKeyPress="validarnumero()"   size="12" maxlength="12" readonly="readonly"   value="<%=iif(utilidadMN_dap="",0,REPLACE(utilidadMN_dap,",","."))%>"></td>
                  </tr>
                </table></td>
                </tr>
               <tr>
                <td height="16" align="left" valign="top"><span class="Estilo7">Resumen</span></td>
                <td colspan="4" rowspan="2" align="center" valign="top">
				<textarea name="txtimportancia" rows="7" class="Cajas2" id="txtimportancia" onKeyUp="ContarTextArea(txtimportancia,'1000',txtcuentaImportancia)"><%=importancia_dap%></textarea></td>
                </tr>
              <tr>
                <td align="right" valign="top">
				 <span class="Estilo5">(Hasta <span id="txtcuentaImportancia" class="Estilo5">1000 caracteres</span>)</span></td>
              </tr>             
              <tr>
                <td width="4%" align="left" valign="top"><span class="Estilo9"><strong>Importancia</strong></span></td>
                <td colspan="4" rowspan="2" align="left" valign="top">
				<textarea name="txtbeneficiarios" rows="5" class="Cajas2" id="txtbeneficiarios"  onKeyUp="ContarTextArea(txtbeneficiarios,'500',txtcuentabeneficiarios)"><%=beneficios_dap%></textarea></td>
                </tr>
              <tr>
                <td align="right" valign="top">
				 <span class="Estilo5">(Hasta <span id="txtcuentabeneficiarios" class="Estilo5">500 caracteres.</span>)</span></td>
              </tr>

              <tr>
                <td colspan="5" align="left" valign="top"><hr style="color:#990000; border:1"></td>
                </tr>
              <tr>
                <td align="left" valign="middle"><span class="Estilo7">Actividades
                  
                  
                </span></td>
                <td colspan="3" align="center" valign="middle">
				<%if codigo_prp<>"" then%><span class="Estilo10" tooltip="Registre las actividades que desarrollará en su propuesta"> Registro de Actividades </span><%end if%></td>
                <td align="right" valign="middle"><input type="button" name="cmdActividades"  value="   Agregar" class="attach_prp" style="width: 85" <%if codigo_dap="" then%> disabled="disabled" <%end if%> onClick="popUp('RegistraActividad_prop.asp?codigo_dap=<%=codigo_dap%>')" tooltip="Registrar una nueva actividad"></td>
              </tr>
              <tr>
                <td colspan="5" align="left" valign="top" >
					<iframe scrolling="yes"  id="listaAct" name="listaAct" height="100%" width="100%" scrolling="auto" frameborder="0"  src="actividadesPropuesta.asp?Codigo_prp=<%=codigo_prp%>&codigo_dap=<%=codigo_dap%>"></iframe>				</td>
                </tr>
            </table>
       </td>
      </tr>
    </table></td>
  </tr>
</table>
<%
	if modifica = 2 then%>
	<script>
		frmpropuesta.cbocentrodecostos.disabled=true
		frmpropuesta.cbotipopropuesta.disabled=true
		frmpropuesta.cbofacultad.disabled=true
		frmpropuesta.txtnombre_prp.disabled=true
		frmpropuesta.txtdescripcion_prp.disabled=true	
		frmpropuesta.cmdborrador.disabled=true	
		frmpropuesta.cmdprioridad.disabled=true	
		frmpropuesta.cmdadjuntar.disabled=true	
		frmpropuesta.cmdenviar.disabled=true			
		
	</script>
	<%end if%>
</form>
</body>
</html>
