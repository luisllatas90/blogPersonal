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
.Estilo3 {
	font-size: 8px;
	font-family: Arial, Helvetica, sans-serif;
	color: #000000;
}
-->
</style></head>
<script language="JavaScript" src="private/validarpropuestas.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
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
		//var prioridad=<%=Request.QueryString("prioridad")%>
		//frmpropuesta.cmdprioridad.disabled=true
		var mensaje=confirm("¿Desea dar prioridad alta a esta propuesta?")
		if (mensaje==true){
		//frmpropuesta.txtprioridad_prp.value='A'
		location.href="registrapropuesta.asp?codigo_pcc=" + codigo_pcc + "&codigo_tpr=" + codigo_tpr + "&codigo_fac=" + codigo_fac + "&nombre_prp=" + nombre_prp + "&descripcion_prp=" + descripcion_prp + "&codigo_prp=" + codigo_prp + "&remLen=" + remLen + "&prioridad_prp=A"  + "&codigo_cop=" + codigo_cop + "&instancia_prp=" + instancia_prp + "&modifica=" + modifica
		}*/
		frmpropuesta.txtprioridad_prp.value='A'
		frmpropuesta.prior.style.display=""
	}
	
	function Validar(modo)
	{	
		var codigo_pcc=frmpropuesta.cbocentrodecostos.value
		var codigo_tpr=frmpropuesta.cbotipopropuesta.value
		var codigo_fac=frmpropuesta.cbofacultad.value
		var nombre_prp=frmpropuesta.txtnombre_prp.value
		var descripcion_prp=frmpropuesta.txtdescripcion_prp.value
		var codigo_prp=frmpropuesta.txtcodigo_prp.value	
		var prioridad_prp=frmpropuesta.txtprioridad_prp.value
		var remLen=frmpropuesta.remLen.value		
		var codigo_cop = frmpropuesta.txtcodigo_cop.value
		var instancia_prp=frmpropuesta.txtinstancia.value
		var modifica=frmpropuesta.txtmodifica.value
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
		if (codigo_fac<0){
		cadena = cadena + "Facultad | "
		}
		if (nombre_prp==""){
		cadena = cadena + "Nombre de Propuesta | "
		}
		if (descripcion_prp==""){
		cadena = cadena + "Descripción de Propuesta | "
		}
		
		if (cadena=="Ingrese los datos: "){
		
		 	switch(modo)
			{						
			case "A":
			//					alert(modifica)
			if (codigo_prp==""){
			//frmpropuesta.txtcodigo_prp.value="123456"
					location.href="procesar.asp?codigo_pcc=" + codigo_pcc + "&codigo_tpr=" + codigo_tpr + "&codigo_fac=" + codigo_fac + "&nombre_prp=" + nombre_prp + "&descripcion_prp=" + descripcion_prp + "&codigo_prp=" + codigo_prp + "&remLen=" + remLen + "&instancia_prp=P&prioridad_prp=" + prioridad_prp + "&accion=guardar&attach=1" + "&codigo_cop=" + codigo_cop + "&modifica=" + modifica

					// popUp('adjuntar.asp?codigo_prp=' + codigo_prp)
			}
			else{
					location.href="procesar.asp?codigo_pcc=" + codigo_pcc + "&codigo_tpr=" + codigo_tpr + "&codigo_fac=" + codigo_fac + "&nombre_prp=" + nombre_prp + "&descripcion_prp=" + descripcion_prp + "&codigo_prp=" + codigo_prp + "&remLen=" + remLen + "&instancia_prp=" + instancia_prp + "&prioridad_prp=" + prioridad_prp + "&accion=actualizar" + "&codigo_cop=" + codigo_cop + "&modifica=" + modifica
					// popUp('adjuntar.asp?codigo_prp=' + codigo_prp)			
					popUp('adjuntar.asp?codigo_prp=' + codigo_prp + '&codigo_cop=' + codigo_cop + '&modifica=' + modifica)
					//alert("error")
				}
			break
			case "B":
			if (codigo_prp==""){			
			var guardaborrador=false
			guardaborrador=confirm ("¿Desea guardar la propuesta como borrador?")		
				if (guardaborrador==true){
					location.href="procesar.asp?codigo_pcc=" + codigo_pcc + "&codigo_tpr=" + codigo_tpr + "&codigo_fac=" + codigo_fac + "&nombre_prp=" + nombre_prp + "&descripcion_prp=" + descripcion_prp + "&codigo_prp=" + codigo_prp + "&remLen=" + remLen + "&instancia_prp=P&prioridad_prp=" + prioridad_prp + "&accion=guardar"	+ "&codigo_cop=" + codigo_cop 	+ "&modifica=" + modifica			
				}	
			}
			else{
					location.href="procesar.asp?codigo_pcc=" + codigo_pcc + "&codigo_tpr=" + codigo_tpr + "&codigo_fac=" + codigo_fac + "&nombre_prp=" + nombre_prp + "&descripcion_prp=" + descripcion_prp + "&codigo_prp=" + codigo_prp + "&remLen=" + remLen + "&instancia_prp=P&prioridad_prp=" + prioridad_prp + "&accion=actualizar" + "&codigo_cop=" + codigo_cop	+ "&modifica=" + modifica		
			}
			break
			case "E":
			if (codigo_prp==""){
			enviarpropuesta=confirm ("¿Desea enviar la propuesta al director?")		
				if (enviarpropuesta==true){
					location.href="procesar.asp?codigo_pcc=" + codigo_pcc + "&codigo_tpr=" + codigo_tpr + "&codigo_fac=" + codigo_fac + "&nombre_prp=" + nombre_prp + "&descripcion_prp=" + descripcion_prp + "&codigo_prp=" + codigo_prp + "&remLen=" + remLen + "&instancia_prp=D&prioridad_prp=" + prioridad_prp + "&accion=guardar" + "&codigo_cop=" + codigo_cop + "&envio_prp=1"
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
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=400,height=350,left = "+ izq +",top = "+ arriba +"');");
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
	<form action="procesar.asp?accion=<%=accion%>" method="post" enctype="multipart/form-data" name="frmpropuesta" id="frmpropuesta">


<%
	accion="propuesta_paso1"
	prioridad_prp=Request.QueryString("prioridad_prp")
	codigo_pcc=Request.QueryString("codigo_pcc")
	codigo_tpr=Request.QueryString("codigo_tpr")
	codigo_fac=Request.QueryString("codigo_fac")
	nombre_prp=Request.QueryString("nombre_prp")
	descripcion_prp=Request.QueryString("descripcion_prp")
	codigo_prp=Request.QueryString("codigo_prp")
	codigo_cop=Request.QueryString("codigo_cop")		
	remLen=Request.QueryString("remLen")
	modifica= Request.QueryString("modifica")
	if modifica = "" then modifica=0

	if modifica=1 or modifica=2 then
			//Response.write(modifica)
		 	Set prop=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	prop.AbrirConexion
			set RSprop=prop.Consultar("ConsultarPropuestas","FO","PR",codigo_prp,"","","")
	    	prop.CerrarConexion
			set prop=nothing
			nombre_prp=RSprop("nombre_prp")
			codigo_tpr=RSprop("codigo_tpr")
			descripcion_prp=trim(RSprop("descripcion_prp"))
			prioridad_prp=RSprop("prioridad_prp")	
			remLen = 1000 - cint(trim(len(descripcion_prp)))		
			
		 	Set prop=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	prop.AbrirConexion
			set RScco=prop.Consultar("ConsultarInvolucradoPropuesta","FO","IN",codigo_prp,session("codigo_Usu"))
	    	prop.CerrarConexion
			set prop=nothing
			codigo_pcc=RScco("codigo_pcc")
			SET RScco=nothing
			
		 	Set prop=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	prop.AbrirConexion
			set RSFcom=prop.Consultar("ConsultarComentarios","FO","PC",codigo_prp,0)
	    	prop.CerrarConexion
			set prop=nothing
			codigo_cop=RSFcom("codigo_cop")			
			SET RSFcom=nothing		
			
		 	Set prop=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	prop.AbrirConexion
			set RSFac=prop.Consultar("ConsultaFacultadPropuesta","FO","TO",codigo_prp)
	    	prop.CerrarConexion
			set prop=nothing
			if RSFac.BOF = false then
			codigo_fac=RSFac("codigo_fac")
			else
			codigo_fac=15
			end if			
			SET RSFac=nothing				
				
			//Response.Write(len(trim(descripcion_prp)))
	end if
	
	if Request.QueryString("attach") <>"" then
		attach=1
	else
		attach=0
	end if
	if attach=1 then%>
		<script>
		popUp("adjuntar.asp?codigo_prp=" + <%=codigo_prp%> + "&codigo_cop=" +<%=codigo_cop%> + "&modifica=" +<%=modifica%>)
		</script>
		
	<%end if%>
	<input name="txtinstancia" type="hidden" id="txtinstancia" value="<%=Request.QueryString("instancia_prp")%>" size="3" maxlength="3">
	<input name="txtmodifica" type="hidden" id="txtmodifica" value="<%=Request.QueryString("modifica")%>" size="3" maxlength="3">
	<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td class="bordeinf"><table width="97%" border="0" align="center" cellpadding="0" cellspacing="5">
      <tr>
        <td><input onClick="Validar('B')"  name="cmdborrador" type="button" class="guardar_prp" id="cmdborrador" value="         Borrador" 
		<%if modifica=1 then 
			if Request.QueryString("instancia_prp") <> "P" then%>	
				disabled="disabled" 
			<%end if%>
		<%end if%>>
          &nbsp;
          <input name="cmdprioridad" type="button" class="prioridad" id="cmdprioridad" onClick="darprioridad()" value="      Prioridad">
          &nbsp;<input onClick="Validar('A')" name="cmdadjuntar" type="button" class="attach_" id="cmdadjuntar" value="        Adjuntar">
  &nbsp;		   <input onClick="Validar('E')"   name="cmdenviar" type="button" class="enviarpropuesta" id="cmdenviar" value="          Enviar"></td></tr>
    </table></td>
  </tr>
  
  <tr>
    <td valign="top"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td align="center" valign="top">
            <table width="95%" border="0" align="center" cellpadding="2" cellspacing="2">
              <tr  height="3">
                <td colspan="2" valign="top"></td>
                <td width="73%" valign="top"></td>
              </tr>
              <tr>
                <td colspan="2" valign="middle">Área </td>
                <td valign="top"><% 
		 	Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC.AbrirConexiontrans
			set rsCentroCostos=objCC.Consultar("ConsultarCentroCosto","FO","PR",session("codigo_Usu"))
	    	ObjCC.CerrarConexiontrans
			set objCC=nothing
			''response.write(session("codigo_Usu"))
		 	CALL llenarlista("cbocentrodecostos","",rsCentroCostos,"codigo_pcc","descripcion_Cco",codigo_pcc,"Seleccionar Área","","")
			set rsCentroCostos = nothing
		 %></td>
                </tr>

              <tr>
                <td colspan="2" valign="middle">Nombre de propuesta
				  
				  <img src="../../../../images/menus/prioridad_.gif" name="prior" width="18" height="17" id="prior"  <%if prioridad_prp<>"A" then%> style="display:none" <%end if%>>				  </td>
                <td valign="top"><input value="<%=nombre_prp%>" name="txtnombre_prp" type="text" id="txtnombre_prp" tabindex="1" size="65" maxlength="150" /></td>
                </tr>
              <tr>
                <td colspan="2" valign="middle">Tipo Propuesta </td>
                <td valign="top"><% 
		 	Set objCC1=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC1.AbrirConexiontrans
			set rsTipoPropuesta=objCC1.Consultar("ConsultarTipoPropuestas","FO","TO")
	    	ObjCC1.CerrarConexiontrans
			set objCC1=nothing
		
		 	call llenarlista("cbotipopropuesta","",rsTipoPropuesta,"codigo_tpr","descripcion_Tpr",codigo_tpr,"Seleccionar Tipo de Propuesta","","")
			set rsTipoPropuesta = nothing
		 %></td>
                </tr>
              <tr>
                <td colspan="2" valign="middle">Facultad</td>
                <td valign="top"><% 
		 	Set objCC2=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC2.AbrirConexiontrans
			set rsFacultad=objCC2.Consultar("ConsultarFacultad","FO","TO","")
	    	ObjCC2.CerrarConexiontrans
			set objCC2=nothing
		
		 	call llenarlista("cbofacultad","",rsFacultad,"codigo_fac","nombre_fac",codigo_fac,"Seleccionar Facultad","","")
			set rsFacultad = nothing
		 %></td>
                </tr>
              <tr>
                <td colspan="2" align="left" valign="top"><input  width="50" name="Submit3222" type="button" class="attach" value="      Ver adjuntos" onClick="Validar('A')" name="cmdadjuntar"></td>
                <td align="center" valign="top">&nbsp;</td>
                </tr>
              
              <tr>
                <td colspan="3" align="center" valign="top">
				<textarea name="txtdescripcion_prp" cols="69" rows="12" wrap="virtual" id="txtdescripcion_prp" tabindex="1" wrap="physical" onKeyDown="textCounter(this.form.txtdescripcion_prp,this.form.remLen,1000);" onKeyUp="textCounter(this.form.txtdescripcion_prp,this.form.remLen,1000);"><%=descripcion_prp%></textarea></td>
              </tr>
			  <tr>
                <td width="17%" align="left" valign="top"><img src="../../../../images/cargando2.gif" width="20" height="20">
                <input name="txtcodigo_cop" type="hidden" id="txtcodigo_cop" value="<%=codigo_cop%>" size="1" maxlength="1"></td>
                <td width="10%" align="right" valign="top">
				<input name="txtcodigo_prp" type="hidden" id="txtcodigo_prp" value="<%=codigo_prp%>" size="1" maxlength="1"></td>
			    <td align="right" valign="top"><span class="Estilo3">
                <input name="txtprioridad_prp" type="hidden" id="txtprioridad_prp" value="<%=prioridad_prp%>" size="1" maxlength="1">
                <input align="right" name="remLen" type="text" class="piepagina" 
				<%if remLen<>"" then %>
				value=<%=remLen%>
				<%else%>
				value="1000"
				<%end if%>
				 size="3" maxlength="4" readonly>
                </span><span class="piepagina">caracteres</span><span class="Estilo3"></font> </span></td>
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
	<%end if
%>
</form>
</body>
</html>
