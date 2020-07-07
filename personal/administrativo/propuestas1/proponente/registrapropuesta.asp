<!--#include file="../../../../funciones.asp"-->
<html>
<head>
<title>Registro de Propuestas</title>

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
div#commentForm {margin: 0px 0px 0px 0px;
display:block;
position:absolute;
left:0px;
top:0Px;
}
.Estilo2 {color: #990000;
	font-weight: bold;
}
-->
</style></head>
<script language="JavaScript" src="private/validarpropuestas.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/tooltip.js"></script>
<script>
	function darprioridad()
	{
		frmpropuesta.txtprioridad_prp.value='A'
		frmpropuesta.prior.style.display=""
	}
	
	function TipoPropuesta(id){

	var id_= frmpropuesta.cbotipopropuesta.value 
	if ((id_==2) || (id_==3) || (id_==9)){
		frmpropuesta.cbofacultad.style.visibility="hidden" 
		document.all.facu.style.visibility="hidden" 		
	}
	else{
		frmpropuesta.cbofacultad.style.visibility="visible" 
		document.all.facu.style.visibility="visible" 		
	}
		if(id_==3){
			if (document.all.txtIngresos !=undefined) {
			document.all.txtIngresos.value=0
			document.all.txtEgresos.value=0
			document.all.txtIngresosMN.value=0
			document.all.txtEgresosMN.value=0
			}
		}
	}

	function ReemplazaCadenaEnter(cadena){
	var n = 10//cadena.length
	for (var i=0; i<n; i++)
		cadena = cadena.replace('\n',"<br>");	
	return cadena
	}	
	
	function Validar(modo)
	{	
		var codigo_pcc=frmpropuesta.cbocentrodecostos.value
		var codigo_tpr=frmpropuesta.cbotipopropuesta.value
		var codigo_fac=frmpropuesta.cbofacultad.value
		var nombre_prp=frmpropuesta.txtnombre_prp.value
		var codigo_prp=frmpropuesta.txtcodigo_prp.value	
		var prioridad_prp=frmpropuesta.txtprioridad_prp.value
		var instancia_prp=frmpropuesta.txtinstancia.value				
		var modifica=frmpropuesta.txtmodifica.value
		var ingresos=frmpropuesta.txtIngresos.value
		var egresos=frmpropuesta.txtEgresos.value
		var beneficiarios=ReemplazaCadenaEnter(frmpropuesta.txtbeneficiarios.value)
		var importancia=ReemplazaCadenaEnter(frmpropuesta.txtimportancia.value)
		var codigo_dap=frmpropuesta.txtcodigo_dap.value
		var cadena = "Ingreso de datos: "		
	
		if(frmpropuesta.txtinstancia.value==""){
		frmpropuesta.txtinstancia.value="P"
		}
		if (codigo_pcc<0){
		cadena = cadena + "Área | "
		}
		if (codigo_tpr<0){
		cadena = cadena + "Tipo de Propuesta | "
		}		
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
		cadena = cadena + "Importancia | "
		}		

		if (importancia==""){
		cadena = cadena + "Resumen | "
		}		
		if (document.all.txtimportancia.value.length>1000)
		{
		cadena=cadena + "El resumen debe tener a los más 1000 caracteres. | "
		}
		
		if (document.all.txtbeneficiarios.value.length>500)
		{
		cadena=cadena + "La importancia debe tener a los más 500 caracteres."
		}
		if (cadena=="Ingreso de datos: "){
		 	switch(modo)
			{						
			case "A":
			if (codigo_prp==""){
			}
			else{
					popUp('../../../../libreriaNet/propuestas/adjuntar.aspx?codigo_prp=' + codigo_prp + '&codigo_dap=' + codigo_dap + '&modifica=' + modifica)
			}
			break
			case "B":
			if (codigo_prp==""){			
			var guardaborrador=false
			guardaborrador=confirm ("¿Desea guardar la propuesta como BORRADOR?, esta acción NO REPRESENTA EL ENVÍO de su propuesta a la instancia siguiente.")		
				if (guardaborrador==true){
					frmpropuesta.action="procesar.asp?accion=nuevo&instancia=B"   //  (instancia B) proponente que guarda como borrador |  (instancia P) proponente que envìa como director
					frmpropuesta.submit()
				}	
			}
			else{
			guardaborrador=confirm ("¿Desea guardar la propuesta como BORRADOR?, esta acción NO REPRESENTA EL ENVÍO de su propuesta a la instancia siguiente.")		
				if (guardaborrador==true){
					
					frmpropuesta.action="procesar.asp?accion=actualizar&instancia=B&codigo_dap="+codigo_dap+"&codigo_prp="+codigo_prp   //  (instancia B) proponente que guarda como borrador |  (instancia P) proponente que envìa como director
					frmpropuesta.submit()
				}	
			}
			
			break
			case "D":
			if (codigo_prp!=""){
			enviarpropuesta=confirm ("¿Desea ENVIAR la propuesta al Director del Área del: " + frmpropuesta.cbocentrodecostos.options [frmpropuesta.cbocentrodecostos.selectedIndex].innerText + "?")		
				if (enviarpropuesta==true){
					frmpropuesta.action="procesar.asp?accion=enviar&codigo_dap="+codigo_dap+"&codigo_prp="+codigo_prp //  (instancia B) proponente que guarda como borrador |  (instancia P) proponente que envìa como director
					frmpropuesta.submit()
				}				
			}
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
	var izq = 300	//(screen.width-ancho)/2

   	var arriba= 200	//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=400,height=390,left = "+ izq +",top = "+ arriba +"');");
	}
	function calcularUtilidad(){
	var ingreso = document.all.txtIngresos.value
	var egreso = document.all.txtEgresos.value
	var ingresoMN = document.all.txtIngresosMN.value
	var egresoMN = document.all.txtEgresosMN.value	
	var cambio= document.all.txtCambio.value.substring(4,document.all.txtCambio.value.length)
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

		document.all.txtIngresosMN.value=document.all.txtIngresos.value* cambio
		document.all.txtEgresosMN.value= document.all.txtEgresos.value * cambio
	}
	else{
		document.all.txtIngresosMN.value=document.all.txtIngresos.value
		document.all.txtEgresosMN.value= document.all.txtEgresos.value
		}
		document.all.txtUtilidadMN.value=Math.round((document.all.txtIngresosMN.value-document.all.txtEgresosMN.value)	*100)/100
	}
	
	function verificaMoneda(check){

	var cambio= document.all.txtCambio.value.substring(4,document.all.txtCambio.value.length)
	if(document.all.chkdolar.checked==true){
		document.all.lblmoneda1.innerHTML="&nbsp;&nbsp;$"
		document.all.lblmoneda2.innerHTML="&nbsp;&nbsp;$"		
		document.all.lblmoneda3.innerHTML="&nbsp;&nbsp;$"		
		document.all.txtCambio.style.visibility="visible"
		document.all.txtIngresosMN.value=document.all.txtIngresosMN.value * cambio
		document.all.txtEgresosMN.value= document.all.txtEgresosMN.value * cambio
		document.all.txtUtilidadMN.value=document.all.txtUtilidad.value	* cambio
	}
	else{
		document.all.lblmoneda1.innerHTML='S/.'	
		document.all.lblmoneda2.innerHTML='S/.'	
		document.all.lblmoneda3.innerHTML='S/.'	
		document.all.txtCambio.style.visibility="hidden"		
		document.all.txtIngresosMN.value=document.all.txtIngresos.value
		document.all.txtEgresosMN.value= document.all.txtEgresos.value
		document.all.txtUtilidadMN.value=	document.all.txtUtilidad.value		
		}	
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

	function EsFacultad(CentroCosto){
//	alert (CentroCosto);
	frmpropuesta.action="procesar.asp?accion=refrescar" ;// se envía la página para su posterior verificación de pertenecer a una facultad
	frmpropuesta.submit();


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

<%			Set objCambio=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	objCambio.AbrirConexion
				set Rscambio=objCambio.Consultar("ConsultarCambioDelDia","FO",cint(2))
	    	objCambio.CerrarConexion
			set objCambio=nothing					

IF Rscambio.RecordCount=0 then

%>
<script>
	alert('El tipo de cambio no se encuentra actualizado, por favor comuníquese con Contabilidad-Tesorería')
	window.close()
</script>
<%
else					
		 	Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC.AbrirConexion
				''consulta los centros de costos a los que est&aacute; asigando el trabajador
				set rsCentrosdeCostos=objCC.Consultar("ConsultarCentroCosto","FO","TO",0)
				if centrocosto<>""  then					
					centrocosto=centrocosto
				else
					set rsCentroCostos=objCC.Consultar("ConsultarCentroCosto","FO","PR",session("codigo_Usu"))								
				end if
			ObjCC.CerrarConexion
			set objCC=nothing	
IF rsCentroCostos.RecordCount =0 then


%>
<script>
	alert('Usted no se encuentra asignado a ningún Área, por favor comuníquese con Dirección de Personal')
	window.close()
</script>
<%
else

 %>
<body topmargin="0" rightmargin="0" leftmargin="0">
<form method="post" name="frmpropuesta" id="frmpropuesta">




<%
	codigo_prp=Request.QueryString("codigo_prp")
	if Request.QueryString("modificacion")=1 then
			Set vers=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	vers.AbrirConexion			
				Set Rs=vers.Consultar("CONSULTARVERSIONESPROPUESTA","FO","PV",codigo_prp,0)
			vers.CerrarConexion	
			codigo_dap=Rs("codigo_dap")
			set rs=nothing
			set vers=nothing
	else
		codigo_dap=Request.QueryString("codigo_dap")
	end if
	if (codigo_prp<>"") and  (codigo_dap<>"") then '' si ya hay una propuesta cargada para modificar

		''consultar los datos de la propuesta
		 ''	Modificar="SI"
	Set P=Server.CreateObject("PryUSAT.clsAccesoDatos")
		P.AbrirConexion	
		set RsPropuesta_=P.Consultar ("ConsultarPropuestas","FO","cp",0,0,codigo_prp,0,0)
		centrocosto=RsPropuesta_("codigo_cco")
		nombre_prp=RsPropuesta_("nombre_prp")
		codigo_tpr=RsPropuesta_("codigo_tpr")
		codigo_fac=RsPropuesta_("codigo_fac")
		prioridad_prp=RsPropuesta_("prioridad_prp")
		instancia_prp=RsPropuesta_("instancia_prp")
				'Response.Write(prioridad)
				
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
	else
		centrocosto=Request.QueryString("centrocosto")
		
		nombre_prp=Request.QueryString("nombre_prp")
		codigo_tpr=Request.QueryString("codigo_tpr")
		codigo_fac=Request.QueryString("codigo_fac")
		prioridad_prp=Request.QueryString("prioridad_prp")
		instancia_prp=Request.QueryString("instancia_prp")

		ingreso_dap=Request.QueryString("ingreso_dap")
		egreso_dap=Request.QueryString("egreso_dap")
		utilidad_dap=Request.QueryString("utilidad_dap")
		beneficios_dap=Request.QueryString("beneficios_dap")
		importancia_dap=Request.QueryString("importancia_dap")
		version_dap=Request.QueryString("version_dap")
		fechaActualizacion_dap=Request.QueryString("fechaActualizacion_dap")
		tipocambio_dap=Request.QueryString("tipocambio_dap")
		moneda_dap=Request.QueryString("moneda_dap")
		ingresoMN_dap=Request.QueryString("ingresoMN_dap")
		egresoMN_dap=Request.QueryString("egresoMN_dap")
		utilidadMN_dap=Request.QueryString("utilidadMN_dap")	
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
	<div id="commentForm"  style="width:100%; height:80px" onMouseOver="minimizarGrid()" onMouseOut="minimizarGrid()"  >
      <table  cellpadding="2"  bgcolor="#FFFFCC" width="100%" style="border: 1px solid #808080; filter:alpha(opacity=90)">
        <tr>
          <td align="center" valign="baseline"><table width="100%" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td width="3%" align="right" class="piepagina"><img src="../../../../images/help.gif" alt="1" width="22" height="17" border="0" /></td>
                <td width="88%" align="left" class="piepagina"><span class="Estilo2">&nbsp;&nbsp;Ayuda on line: <b><span style="color:#000000">Pasos para Registar una Propuesta </span></b></span></td>
                <td width="9%" align="right" valign="top" class="piepagina"><img src="../../../../images/menus/Cerrar_s.gif" width="16" height="16" style="cursor:hand" title="Cerrar" onClick="document.all.commentForm.style.display='none'" ></td>
              </tr>
          </table></td>
        </tr>
        <tr ID="TablaInfoAyuda" >
          <td valign="baseline"  ><p> <span class="piepagina"> <span style="color:#990000"><b>Paso 01 :</b></span></span> <span class="piepagina" style="color:#000000;text-align:justify"> <strong>Ingresar los datos</strong> de la propuesta, a excepci&oacute;n de Registro de Actividades.<br>
                    <span class="Estilo2">Paso 02:</span> Hacer <strong>clic</strong> en el Bot&oacute;n &quot;<strong>Guardar</strong>&quot;, as&iacute; la propuesta ser&aacute; guardada pero no enviada.<br>
                    <span class="Estilo2">Paso 03:</span> Hacer <strong>clic</strong> en el Bot&oacute;n &quot;<strong>Adjuntar</strong>&quot;, para subir los archivos relacionados.</span> <br>
                    <span class="piepagina"> <span class="Estilo2">Paso 04:</span><span class="Estilo9"> Hacer <strong>clic</strong> en el Bot&oacute;n &quot;<strong>Actividades</strong>&quot; para registrar las actividades (de existir).</span></span> <br>
                    <span class="piepagina"><span class="Estilo2">Paso 05:</span> <span class="Estilo9">Para enviar la propuesta la Instancia Siguiente hacer clic en el Bot&oacute;n &quot;<strong>Enviar</strong>&quot;. </span></span></p></td>
        </tr>
      </table>
  </div>
	<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td class="bordeinf"><table width="97%" border="0" align="center" cellpadding="0" cellspacing="5">
      <tr>
        <td><input onClick="Validar('B')"  name="cmdborrador" type="button" class="guardar_prp" id="cmdborrador" value="         Guardar"
		<%if modifica=1 then 
			if Request.QueryString("instancia_prp") <> "P" then%>
				disabled="disabled"
			<%end if%>
		<%end if%>>
          &nbsp;
          <input name="cmdprioridad" type="button" class="prioridad" id="cmdprioridad" onClick="darprioridad()"  value="      Prioridad" <%if codigo_prp<>"" then %> disabled="disabled" <%end if%>>
          &nbsp;<input onClick="Validar('A')" name="cmdadjuntar" type="button" class="attach_" id="cmdadjuntar" value="        Adjuntar" <%if codigo_prp="" then%>  disabled="disabled" <%end if%>>
  &nbsp;		   <input onClick="Validar('D')"   name="cmdenviar" type="button" class="enviarpropuesta" id="cmdenviar" value="          Enviar" <%if codigo_dap="" then%>disabled="disabled" <%end if%>>
			</td>
		  </tr>
    </table></td>
  </tr>  
  <tr>
    <td valign="top"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td align="center" valign="top">
            <table width="95%" border="0" align="center" cellpadding="2" cellspacing="2">
              <tr  height="6">
                <td colspan="2" valign="top"></td>
                <td colspan="3" valign="top"></td>
              </tr>
              <tr  height="10">
                <td colspan="2" valign="top"></td>
                <td colspan="3" valign="top"></td>
              </tr>
              <tr>
                <td colspan="2" valign="middle"><span class="Estilo9"> Nombre de propuesta
				  
			      <img src="../../../../images/menus/prioridad_.gif" name="prior" width="18" height="17" id="prior"  <%if prioridad_prp<>"A" then%> style="display:none" <%end if%>> </span></td>
                <td colspan="3" valign="top"><input name="txtnombre_prp" type="text" class="Cajas2" id="txtnombre_prp" tabindex="1" value="<%=nombre_prp%>" size="65" maxlength="150" ></td>
                </tr>
              <tr>
                <td colspan="2" valign="middle"><span class="Estilo9">Área</span><span class="rojo">*</span></td>
                <td colspan="3" valign="top">
			<% 	



		 	Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC.AbrirConexion
				''consulta los centros de costos a los que est&aacute; asigando el trabajador
				set rsCentrosdeCostos=objCC.Consultar("ConsultarCentroCosto","FO","TO",0)
				if centrocosto<>""  then					
					centrocosto=centrocosto
				else
					set rsCentroCostos=objCC.Consultar("ConsultarCentroCosto","FO","PR",session("codigo_Usu"))	
					if rsCentroCostos.recordCount>0 then
						centrocosto=rsCentroCostos("codigo_cco")
					end if								
				end if
			facu= objCC.Ejecutar ("PRP_PerteneceAFacultad",true,centrocosto,0)
			ObjCC.CerrarConexion
			set objCC=nothing	
		 	CALL llenarlista("cbocentrodecostos","EsFacultad('" & centrocosto & "')",rsCentrosdeCostos,"codigo_cco","descripcion_Cco",centrocosto,"Seleccionar Área","","")
			set rsCentroCostos = nothing
			%>
		 </td>
              </tr>
              <tr>
                <td colspan="2" valign="middle">&nbsp;</td>
                <td colspan="3" valign="top"><span id="msgAreaDirector" class="rojo"><strong>*Atenci&oacute;n:</strong> El &aacute;rea selecionada refiere a la primera instancia de revisi&oacute;n de la propuesta y ser&aacute; revisada por su Director</span>. </td>
              </tr>
              <tr>
                <td colspan="2" valign="middle"><span class="Estilo9">Tipo Propuesta </span></td>
                <td width="50%" valign="top"><% 
		 	Set objCC1=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC1.AbrirConexion
				set rsTipoPropuesta=objCC1.Consultar("ConsultarTipoPropuestas","FO","TO")
	    	ObjCC1.CerrarConexion
			set objCC1=nothing
	
		 	call llenarlista("cbotipopropuesta","onChange=TipoPropuesta(this.value)",rsTipoPropuesta,"codigo_tpr","descripcion_Tpr",codigo_tpr,"Seleccionar Tipo de Propuesta","","")


			set rsTipoPropuesta = nothing
			 %></td>
                <td width="1%" align="right" valign="top"><span id="facu" name="facu">Facultad</span></td>
                <td width="29%" valign="top"><% 
		 	Set objCC2=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC2.AbrirConexion
			set rsFacultad=objCC2.Consultar("ConsultarFacultad","FO","TO","")
	    	ObjCC2.CerrarConexion
			set objCC2=nothing
		
		 	call llenarlista("cbofacultad","",rsFacultad,"codigo_fac","nombre_fac",codigo_fac,"Todas las Facultades","","")
			'response.Write(facu)
			set rsFacultad = nothing%>
		<%
		'Response.Write(facu)
		if facu <> 0 then%>
		<script>
		frmpropuesta.cbofacultad.style.display="none" 
		document.all.facu.style.display="none" 
		</script>			
		<%end if%>	
					
			<script> TipoPropuesta(3)	</script>
		 	<%if Modificar="SI" then%> 

		<script>
			frmpropuesta.cbocentrodecostos.disabled =true
			frmpropuesta.cbotipopropuesta.disabled =true
			frmpropuesta.cbofacultad.disabled=true
			frmpropuesta.txtnombre_prp.disabled=true		
		</script>
		<%	end if %>
	
		</td>
              </tr>
              <tr>
                <td colspan="5" valign="middle"><hr style="color:#990000; border:1"></td>
                </tr>
              <tr>
                <td colspan="5" align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="20%">
				 
                      D&oacute;lares
                        <input name="chkdolar" type="checkbox" id="chkdolar" onClick="verificaMoneda('this')" <%if moneda_dap=2 then%> checked="checked" <%end if%>>
																											
                        <input name="txtCambio" type="text" id="txtCambio" value="T.C. &nbsp;<%=Replace(formatNumber(iif(moneda_dap<>2,Rscambio(3),tipocambio_dap),2,false),",",".")%>" size="6" style="border:none; <%if moneda_dap<>2 then%> visibility:hidden; <%end if%> background-color:#F0F0F0; color:#0000FF" readonly="readonly" tooltip="<b>Tipo de Cambio</b><br>Al <%=iif(moneda_dap<>2,date(),formatdatetime(fechaActualizacion_dap,1))%>, asigando por Contabilidad"></td>
                    <td width="16%" align="right"><span class="Estilo8">Ingresos</span>                      <span class="etiqueta cursoM" id="lblmoneda1" style="text-align:right" name="lblmoneda1">S/.</span></td>
                    <td width="13%"><input name="txtIngresos" type="text" class="Cajas" id="txtIngresos" size="9" maxlength="9"  onKeyPress="validarnumero()" onKeyUp="calcularUtilidad();"  style="text-align:right" value="<%=replace(ingreso_dap,",",".")%>"></td>
                    <td width="11%" align="right"><span class="Estilo9">Egresos</span>					 <span class="etiqueta cursoM" id="lblmoneda2" style="text-align:right" name="lblmoneda2">S/.</span>					</td>
                    <td width="11%"><input name="txtEgresos" type="text" class="Cajas" id="txtEgresos" size="9" maxlength="9" onKeyPress="validarnumero()" onKeyUp="calcularUtilidad()" style="text-align:right"  value="<%=replace(Egreso_dap,",",".")%>"></td>
                    <td width="13%" align="right"><span class="Estilo7">Utilidad</span>
					 <span class="etiqueta cursoM" id="lblmoneda3" style="text-align:right" name="lblmoneda3">S/.</span>					</td>
                    <td width="16%"><input name="txtUtilidad" type="text"  id="txtUtilidad" size="12" maxlength="12" onKeyPress="validarnumero()"  style="text-align:center; border:none; background-color:#F0F0F0" readonly="readonly"   value="<%=iif(utilidad_dap="",0,replace(utilidad_dap,",","."))%>"></td>
                    </tr>
                  <tr>
                    <td> <span class="Estilo7">En Moneda Nacional: </span></td>
                    <td align="right"><span class="Estilo9"><strong>Ingresos S/.</strong> </span></td>
                    <td><input name="txtIngresosMN" type="text" readonly="readonly" class="Cajas" id="txtIngresosMN" style="text-align:right; border:none; background-color:#F0F0F0" size="9" maxlength="9"  onKeyPress="validarnumero()" onKeyUp="calcularUtilidad();"  style="text-align:right" value="<%=replace(IngresoMN_dap,",",".")%>"></td>
                    <td align="right"><span class="Estilo9"><strong>Egresos S/.</strong> </span></td>
                    <td><input name="txtEgresosMN" type="text" readonly="readonly" class="Cajas" id="txtEgresosMN"  style="text-align:right; border:none; background-color:#F0F0F0" size="9" maxlength="9" onKeyPress="validarnumero()" onKeyUp="calcularUtilidad()" style="text-align:right"  value="<%=replace(EgresoMN_dap,",",".")%>"></td>
                    <td align="right"><span class="Estilo9"><strong>Utilidad S/. </strong></span></td>
                    <td> <input name="txtUtilidadMN" type="text"  id="txtUtilidadMN"  style="text-align:center; border:none; background-color:#F0F0F0" onKeyPress="validarnumero()"   size="12" maxlength="12" readonly="readonly"   value="<%=iif(utilidadMN_dap="",0,replace(utilidadMN_dap,",","."))%>"></td>
                  </tr>
                </table></td>
                </tr>
              <tr>
                <td height="16" align="left" valign="top">Resumen</td>
                <td colspan="4" rowspan="2" align="center" valign="top">
				<textarea name="txtimportancia" rows="7" class="Cajas2" id="txtimportancia" onKeyUp="ContarTextArea(txtimportancia,'1000',txtcuentaImportancia)"><%=importancia_dap%></textarea></td>
                </tr>
              <tr>
                <td align="right" valign="top">
				 <span class="Estilo5">(Hasta 
				<span id="txtcuentaImportancia" class="Estilo5">
				1000 caracteres.				</span>)</span></td>
              </tr>
              
              <tr>
                <td width="4%" align="left" valign="top">Importancia</td>
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
                <td align="right" valign="middle"><input type="button" name="cmdActividades"  value="   Agregar" class="attach_prp" style="width: 85" <%if codigo_prp="" then%> disabled="disabled" <%end if%> onClick="popUp('RegistraActividad_prop.asp?codigo_dap=<%=codigo_dap%>')" tooltip="Registrar una nueva actividad"></td>
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

</form>

</body>
<%
end if ' de tipocambio

end if ' de persona
%>
</html>
