<!--#include file="../../../../funciones.asp"-->


<html>
<head>
<title>Registro de Versi&oacute;n de un Informe</title>

<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--
body {
	background-color: #f0f0f0;
}
.Estilo7 {
	color: #000000;
	font-weight: bold;
}
.Estilo8 {color: #000000}
.Estilo9 {
	color: #990000;
	font-weight: bold;
	font-size: 16px;
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
.Estilo10 {color: #990000}
-->
</style></head>
<script language="JavaScript" src="private/validarpropuestas.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/tooltip.js"></script>
<script language="JavaScript" src="../../../../private/calendario.js"></script>
<script>
	function darprioridad()
	{

		frmpropuesta.txtprioridad_prp.value='A'
		frmpropuesta.prior.style.display=""
	}
	
	function Validar(modo)
	{	

		var codigo_dip=frmpropuesta.txtcodigo_dip.value	
		var codigo_prp=frmpropuesta.txtcodigo_prp.value	
		var prioridad_prp=frmpropuesta.txtprioridad_prp.value
		var instancia_prp=frmpropuesta.txtinstancia.value				
		var modifica=frmpropuesta.txtmodifica.value
		var FechaInicio=frmpropuesta.txtFechaInicio.value
		var FechaFin=frmpropuesta.txtFechaFin.value
		var utilidad=frmpropuesta.txtutilidad.value
		var objetivos=frmpropuesta.txtObjetivos.value
		var metas=frmpropuesta.txtmetas.value
		var espectativas=frmpropuesta.txtespectativas.value		
		var cadena = "Ingrese los datos: "		
		if(frmpropuesta.txtinstancia.value==""){
		frmpropuesta.txtinstancia.value="P"
		}
		
		if (utilidad==""){
		cadena = cadena + "Utilidad | "
		}		
		if (FechaInicio==""){
		cadena = cadena + "Fecha de Inicio | "
		}		
		if (FechaFin==""){
		cadena = cadena + "Fecha de Fin | "
		}		
		if (objetivos==""){
		cadena = cadena + "Objetivos | "
		}		
		if (metas==""){
		cadena = cadena + "Metas | "
		}				
		if (espectativas==""){
		cadena = cadena + "Espectativas | "
		}						
				
		if (cadena=="Ingrese los datos: "){
		
		 	switch(modo)
			{						
			case "A":

			if (codigo_prp==""){
			}
			else{
					popUp('../../../../libreriaNet/propuestas/adjuntar.aspx?codigo_prp=' + codigo_prp + '&codigo_dip=' + codigo_dip + '&modifica=' + modifica)
					window.opener.location.reload()
				}
			break
			case "G":

			if (codigo_dip==""){			
			var guardaborrador=false
					frmpropuesta.action="procesar.asp?accion=nuevoInforme"  
					frmpropuesta.submit()
					alert ("Se ha registrado una nueva versión del Informe de la propuesta, ADJUNTE LOS ARCHIVOS y cierre la ventana")
					window.opener.location.reload()					

			}
			else{				
					frmpropuesta.action="procesar.asp?accion=actualizarInforme&codigo_dip="+codigo_dip+"&codigo_prp="+codigo_prp
					frmpropuesta.submit()
					window.opener.location.reload()
			}
			
			break
			case "E":
			if (codigo_dip==""){
					frmpropuesta.action="procesar.asp?accion=nuevoInforme&envia=SI"  
					frmpropuesta.submit()
					window.opener.location.reload()
			}
			
			else{				
					frmpropuesta.action="procesar.asp?accion=actualizarInforme&codigo_dip="+codigo_dip+"&codigo_prp="+codigo_prp
					frmpropuesta.submit()
					window.opener.location.reload()
			}
			}		
		}
		else{
		alert (cadena)
		}		
	}		
	function popUp(URL) {
	day = new Date();
	id = day.getTime();
	var izq = 300

   	var arriba= 200
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
		document.all.txtUtilidadMN.value=	document.all.txtUtilidad.value	* cambio
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

<form method="post" name="frmpropuesta" id="frmpropuesta">
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
        <td valign="baseline"  ><p> <span class="piepagina"><span style="color:#990000"><b>Paso 01 :</b></span></span> <span class="piepagina" style="color:#000000;text-align:justify"> <strong>Ingresar los datos</strong> de la propuesta, ingresos, egresos, moneda, objetivos, metas y espectativas. <br />
                  <span class="Estilo2">Paso 02:</span> Hacer <strong>Clic</strong> en el Bot&oacute;n &quot;<strong>Guardar</strong>&quot;, para registrar la informaci&oacute;n y ativar el bot&oacute;n que permite adjuntar los archivos de la propuesta. <br />
                  <span class="Estilo2">Paso 03:</span> Hacer clic en el Bot&oacute;n &quot;<strong>Adjuntar</strong>&quot;, para subir los archivos relacionados y en el Bot&oacute;n &quot;<strong>Actividades</strong>&quot; para registrar las actividades. </span><br />
                  <span class="piepagina"><span class="Estilo2">Paso 04:</span> <span class="Estilo8">Si ha realizado cambios en la informaci&oacute;n hacer <strong>Clic</strong> nuevamente en el Bot&oacute;n &quot;<strong>Guardar</strong>&quot;. Para <strong>Finalizar</strong> el proceso <strong>Cierre la ventana</strong>. </span></span></p></td>
      </tr>
    </table>
  </div>


<%

	codigo_prp=Request.QueryString("codigo_prp")
	codigo_dip=Request.QueryString("codigo_dip")
	if (codigo_prp<>"") then '' si ya hay una propuesta cargada para modificar
	%>

		<%''consultar los datos de la propuesta
		 	''Modificar="SI"
			Set P=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	P.AbrirConexion	
			set RsPropuesta_=P.Consultar ("ConsultarPropuestas","FO","cp",0,0,codigo_prp,0,0)
			centrocosto=RsPropuesta_("codigo_cco")
			nombre_prp=RsPropuesta_("nombre_prp")
			codigo_tpr=RsPropuesta_("codigo_tpr")
			codigo_fac=RsPropuesta_("codigo_fac")
			prioridad_prp=RsPropuesta_("prioridad_prp")
			instancia_prp=RsPropuesta_("instancia_prp")
			''Response.Write(prioridad)
			
			set RsVersiones_=P.Consultar ("ConsultarDatosVersionInforme","FO","LI",codigo_prp,0)			
			if RsVersiones_.RecordCount >0 then
				objetivo_dip=RsVersiones_("objetivo_dip")
				metas_dip=RsVersiones_("metas_dip")
				espectativas_dip=RsVersiones_("espectativas_dip")
				utilidad_usat=RsVersiones_("utilidad_usat")
				tipo_dip=RsVersiones_("tipo_dip")
				version_dip=RsVersiones_("version_dip")
				fechaActualizacion_dip=RsVersiones_("fechaActualizacion_dip")
				fechaInicioEjecucion_dip=RsVersiones_("fechaInicioEjecucion_dip")
				fechaFinEjecucion_dip =RsVersiones_("fechaFinEjecucion_dip")
			end if	
			RsVersiones_.Close
			Set RsVersiones_=Nothing
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
	<input name="txtcodigo_dip" type="HIDDEN" id="txtcodigo_dip" value="<%=codigo_dip%>" maxlength="1">
	</span>
	<input name="txtinstancia" type="hidden" id="txtinstancia" value="<%=instancia_prp%>" maxlength="3">
	<input name="txtmodifica" type="hidden" id="txtmodifica" value="<%=Request.QueryString("modifica")%>" maxlength="3">
	<input name="txtprioridad_prp" type="hidden" id="txtprioridad_prp" value="<%=prioridad_prp%>">
	<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td class="bordeinf"><table width="97%" border="0" align="center" cellpadding="0" cellspacing="5">
      <tr>
        <td>
		<input onClick="Validar('<%=iif(version_dip=1,"E","G")%>')"  name="cmdborrador" type="button" class="enviarpropuesta" id="cmdborrador" value="         Guardar" 
		<%if modifica=1 then  
			if Request.QueryString("instancia_prp") <> "P" then%>	
				disabled="disabled" 
			<%end if%>
		<%end if%>>
          &nbsp;&nbsp;
          <input onClick="Validar('A')" name="cmdadjuntar" type="button" class="attach_" id="cmdadjuntar" value="        Adjuntar" <%if codigo_dip="" then%> disabled="disabled" <%end if%>>
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
			  <tr><td> </td>
			  </tr>
              <tr>
                <td colspan="5" align="center" valign="middle">
				<span class="Estilo9">         
				Informe 
                </span></td>
                </tr>
              <tr>
                <td colspan="2" valign="middle"><span class="Estilo7">Nombre de propuesta <img src="../../../../images/menus/prioridad_.gif" name="prior" width="18" height="17" id="prior"  <%if prioridad_prp<>"A" then%> style="display:none" <%end if%>> </span></td>
                <td colspan="3" valign="top"><input readonly="readonly" name="txtnombre_prp" type="text" class="Cajas2 " id="txtnombre_prp" tabindex="1" value="<%=nombre_prp%>" size="65" maxlength="150" ></td>
              </tr>
              <tr>
                <td colspan="2" valign="middle"><span class="Estilo7">Área </span></td>
                <td colspan="3" valign="top"><span class="Estilo8">
                  <% 
				
		 	Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC.AbrirConexion
				''consulta los centros de costos a los que est&aacute; asigando el trabajador
				if centrocosto<>""  then
					centrocosto=centrocosto
				else
				set rsCentroCostos=objCC.Consultar("ConsultarCentroCosto","FO","PR",session("codigo_Usu"))
					centrocosto=rsCentroCostos("codigo_cco")

				end if
				set rsCentrosdeCostos=objCC.Consultar("ConsultarCentroCosto","FO","TO",0)
			ObjCC.CerrarConexion
			set objCC=nothing

		 	CALL llenarlista("cbocentrodecostos","",rsCentrosdeCostos,"codigo_cco","descripcion_Cco",centrocosto,"Seleccionar &Aacute;rea","","disabled=true")
			set rsCentroCostos = nothing
		 %>
                </span></td>
                </tr>
              <tr>
                <td colspan="2" valign="middle"><span class="Estilo7">Tipo Propuesta </span></td>
                <td width="40%" valign="top"><span class="Estilo8">
                  <% 
		 	Set objCC1=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC1.AbrirConexion
			set rsTipoPropuesta=objCC1.Consultar("ConsultarTipoPropuestas","FO","TO")
	    	ObjCC1.CerrarConexion
			set objCC1=nothing

		
		 	call llenarlista("cbotipopropuesta","",rsTipoPropuesta,"codigo_tpr","descripcion_Tpr",codigo_tpr,"Seleccionar Tipo de Propuesta","","disabled=true")


			set rsTipoPropuesta = nothing
			 %>
                </span></td>
                <td width="4%" align="right" valign="top"><span class="Estilo7">Facultad</span></td>
                <td width="36%" valign="top"><span class="Estilo8">
                  <% 
		 	Set objCC2=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC2.AbrirConexion
			set rsFacultad=objCC2.Consultar("ConsultarFacultad","FO","TO","")
	    	ObjCC2.CerrarConexion
			set objCC2=nothing
		
		 	call llenarlista("cbofacultad","",rsFacultad,"codigo_fac","nombre_fac",codigo_fac,"Todas las Facultades","","")
			set rsFacultad = nothing
		 
		 			''if Modificar="SI" then
					%> 

		          <script>
		frmpropuesta.cbocentrodecostos.disabled =true
		frmpropuesta.cbotipopropuesta.disabled =true
		frmpropuesta.cbofacultad.disabled=true
		frmpropuesta.txtnombre_prp.disabled=true
			
		          </script>
		          <%	''end if	 %>
                </span></td>
              </tr>
              <tr>
                <td colspan="5" valign="middle"><hr style="color:#990000; border:1"></td>
                </tr>
              
              <tr>
                <td colspan="5" align="left" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td width="13%" align="left"><span class="Estilo7">Fecha Inicio</span><span class="Estilo8"></span></td>
                    <td width="20%"><span class="Estilo8">
                      <input readonly="readonly" name="txtFechaInicio" type="text" id="txtFechaInicio" size="8" maxlength="10" value="<%=fechaInicioEjecucion_dip%>">
                      <input name="Submit" type="button" class="cunia" value="  " onClick="MostrarCalendario('txtFechaInicio')" >
                    </span></td>
                    <td width="11%" align="right"><span class="Estilo7">Fecha Fin </span><span class="Estilo8"></span></td>
                    <td width="21%"><span class="Estilo8">
                      <input  readonly="readonly" name="txtFechaFin" type="text" id="txtFechaFin" size="8" maxlength="10" value="<%=fechaFinEjecucion_dip%>">
                      <input name="Submit2" type="button"  class="cunia" value="  " onClick="MostrarCalendario('txtFechaFin')" >
                    </span></td>
                    <td width="12%"><span class="Estilo7">Utilidad  S/.        </span></td>
                    <td width="23%"><input name="txtutilidad" type="text" class="Cajas2 " id="txtutilidad" tabindex="1" value="<%=utilidad_usat%>" size="65" maxlength="150" onKeyPress="validarnumero()" ></td>
                    </tr>
                </table></td>
                </tr>
              
              <tr>
                <td align="left" valign="top"><span class="Estilo8"><strong>Objetivo</strong></span></td>
                <td colspan="4" rowspan="2" align="left" valign="top"><textarea name="txtObjetivos" rows="5" class="Cajas2 Estilo8" id="txtObjetivos"  onKeyUp="ContarTextArea(txtObjetivos,'200',txtcuentatxtObjetivos)" style="overflow:auto"><%=objetivo_dip%></textarea></td>
              </tr>
              <tr>
                <td align="right" valign="top"> <span class="Estilo10">(Hasta <span id="txtcuentatxtObjetivos" class="Estilo5"> 200 caracteres.</span>)	</span>	</td>
              </tr>
              <tr>
                <td colspan="5" align="left" valign="top"><span class="Estilo8"></span></td>
                </tr>
              
              <tr>
                <td width="4%" align="left" valign="top"><span class="Estilo8"><strong>Metas</strong></span></td>
                <td colspan="4" rowspan="2" align="left" valign="top">
				<textarea name="txtmetas" rows="5" class="Cajas2 Estilo8" id="txtmetas"  onKeyUp="ContarTextArea(txtmetas,'200',txtcuentatxtmetas)" style="overflow:auto"><%=metas_dip%></textarea></td>
                </tr>
              <tr>
                <td align="right" valign="top"><span class="Estilo10">(Hasta <span id="txtcuentatxtmetas" class="Estilo5"> 200 caracteres.</span>)</span>  </td>
              </tr>
              <tr>
                <td height="16" align="left" valign="top"><span class="Estilo8"><strong>Espectativas</strong></span></td>
                <td colspan="4" rowspan="2" align="center" valign="top">
				<textarea name="txtespectativas" rows="7" class="Cajas2 Estilo8" id="txtespectativas" onKeyUp="ContarTextArea(txtespectativas,'200',txtcuentaespectativas)" style="overflow:auto"><%=espectativas_dip%></textarea></td>
                </tr>
              <tr>
                <td align="right" valign="top"><span class="Estilo10">(Hasta <span id="txtcuentaespectativas" class="Estilo5">200 caracteres.</span>)</span> </td>
              </tr>
              <tr>
                <td colspan="5" align="left" valign="top"><hr style="color:#990000; border:1"></td>
                </tr>
              <tr>
                <td colspan="5" align="right" valign="top"><span class="Estilo10">(Todos los campos deben ser llenados, pues son obligatorios.) </span></td>
              </tr>
            </table>
       </td>
      </tr>
    </table></td>
  </tr>
</table>
    <div align="center">
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
    </div>
</form>
</body>
</html>
