<!--#include file="../../../../../funciones.asp"-->


<html>
<head>
<title>Registro de Convenios</title>

<link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css">
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
.Estilo4 {color: #F0F0F0}
.Estilo19 {font-size: 10px; font-family: Arial, Helvetica, sans-serif; color: #000000; }
-->
</style>
<script type="text/JavaScript">
<!--
function MM_jumpMenu(targ,selObj,restore){ //v3.0
  eval(targ+".location='"+selObj.options[selObj.selectedIndex].value+"'");
  if (restore) selObj.selectedIndex=0;
}
//-->
</script>
</head>
<script language="JavaScript" src="../../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../../private/calendario.js"></script>
<script>

	function ActivarDuraciones(){
	var indefinido=frmconvenio.chkDuracionIndefinida.checked
	if (indefinido==true){
		frmconvenio.txtDuracion.value=''
		frmconvenio.cboPeriodo.disabled=true
		frmconvenio.txtDuracion.disabled=true
	}
	else{
		frmconvenio.cboPeriodo.disabled=false	
		frmconvenio.txtDuracion.disabled=false
	}
	}	
	function adjuntar (codigo_cni){
	if(codigo_cni!=''){
	popUpAdjuntar("adjuntarconvenio.asp?codigo_cni=" + codigo_cni)
	}
	else{
		alert("Debe guardar el convenio para poder subir el archivo PDF")
	}
	//location.href="adjuntarconvenio.asp?codigo_cni=" + codigo_cni
//location.href="procesar.asp?denominacion=" + denominacion + "&Ambito=" + Ambito + "&modalidad=" + modalidad + "&descripcion=" + descripcion + "&Duracion=" + Duracion + "&Periodo=" + Periodo + "&FechaInicio=" + FechaInicio + "&Renovacion="+ Renovacion + "&NumCopias=" + NumCopias + "&accion=guardar" + "&Observacion=" + Observacion + "&Responsable=" + Responsable + "&Referencia=" + Referencia + "&remLen=" + remLen + "&resolucion=" + resolucion + "&NameResponsable=" + NameResponsable + "&NameReferencia=" + NameReferencia + "&NameResolucion="  + NameResolucion + "&modifica=" + modifica		
	}
	function Validar1(modo){
	var codigo_cni=frmconvenio.txtcodigo_cni.value
	var cadena = "Ingrese los datos: "	
	var denominacion=frmconvenio.txtdenominacion.value
	var Ambito=frmconvenio.cboAmbito.value
	var modalidad=frmconvenio.cboModalidad.value
	var descripcion=frmconvenio.txtdescripcion_prp.value
	var Duracion=frmconvenio.txtDuracion.value
	var Periodo=frmconvenio.cboPeriodo.value
	var FechaInicio =frmconvenio.txtFechaInicio.value	
	var Renovacion=frmconvenio.chkRenovacion.checked
	var NumCopias=frmconvenio.txtNumCopias.value
	var Observacion=frmconvenio.txtObservacion.value
	var Responsable=frmconvenio.txtResponsable.value
	var NameResponsable=frmconvenio.txtNameResponsable.value
	var Referencia=frmconvenio.txtReferencia.value
	var NameReferencia=frmconvenio.txtNameReferencia.value	
	var remLen=frmconvenio.remLen.value		
	var resolucion =frmconvenio.txtResolucion.value
	var NameResolucion=frmconvenio.txtNameResolucion.value	
	var modifica=frmconvenio.txtmodifica.value
	//alert (denominacion)

	if (Renovacion==true){
		Renovacion=1
	}
	else{
		Renovacion=0
	}
	if(denominacion==""){
	cadena = cadena + "Denominación | "
	}

	if (Ambito<0){
	cadena = cadena + "Ámbito | "
	}
	if (modalidad<0){
	cadena = cadena + "Modalidad | "
	}
	if (descripcion==""){
	cadena = cadena + "Objetivos | "
	}	
	if (FechaInicio==""){
	cadena = cadena + "Fecha de Inicio | "
	}		
	//alert (Renovacion)
		if (cadena != "Ingrese los datos: "	){
			alert (cadena)
		}
		else{
			if (codigo_cni==''){
			//alert ('Registrar Nuevo Convenio')
//denominacion Ambito modalidad descripcion Duracion Periodo FechaInicio Renovacion NumCopias Observacion Responsable Referencia remLen resolucion
			location.href="procesar.asp?denominacion=" + denominacion + "&Ambito=" + Ambito + "&modalidad=" + modalidad + "&descripcion=" + descripcion + "&Duracion=" + Duracion + "&Periodo=" + Periodo + "&FechaInicio=" + FechaInicio + "&Renovacion="+ Renovacion + "&NumCopias=" + NumCopias + "&accion=guardar" + "&Observacion=" + Observacion + "&Responsable=" + Responsable + "&Referencia=" + Referencia + "&remLen=" + remLen + "&resolucion=" + resolucion + "&NameResponsable=" + NameResponsable + "&NameReferencia=" + NameReferencia + "&NameResolucion="  + NameResolucion + "&modifica=" + modifica
			frmconvenio.cmdinstituciones.disabled=false
			}
			else{
		//	alert ('Registrar Modificación')
			//alert (codigo_cni)
			location.href="procesar.asp?codigo_cni=" + codigo_cni + "&denominacion=" + denominacion + "&Ambito=" + Ambito + "&modalidad=" + modalidad + "&descripcion=" + descripcion + "&Duracion=" + Duracion + "&Periodo=" + Periodo + "&FechaInicio=" + FechaInicio + "&Renovacion="+ Renovacion + "&NumCopias=" + NumCopias + "&accion=modificar" + "&Observacion=" + Observacion + "&Responsable=" + Responsable + "&Referencia=" + Referencia + "&remLen=" + remLen + "&resolucion=" + resolucion + "&NameResponsable=" + NameResponsable + "&NameReferencia=" + NameReferencia + "&NameResolucion="  + NameResolucion + "&modifica=" + modifica
			}
		}	
		
	}
	function eliminaParticipante(codigo_cin){

	var codigo_cni=frmconvenio.txtcodigo_cni.value
	var cadena = "Ingrese los datos: "	
	var denominacion=frmconvenio.txtdenominacion.value
	var Ambito=frmconvenio.cboAmbito.value
	var modalidad=frmconvenio.cboModalidad.value
	var descripcion=frmconvenio.txtdescripcion_prp.value
	var Duracion=frmconvenio.txtDuracion.value
	var Periodo=frmconvenio.cboPeriodo.value
	var FechaInicio =frmconvenio.txtFechaInicio.value	
	var Renovacion=frmconvenio.chkRenovacion.checked
	var NumCopias=frmconvenio.txtNumCopias.value
	var Observacion=frmconvenio.txtObservacion.value
	var Responsable=frmconvenio.txtResponsable.value
	var NameResponsable=frmconvenio.txtNameResponsable.value
	var Referencia=frmconvenio.txtReferencia.value
	var NameReferencia=frmconvenio.txtNameReferencia.value	
	var remLen=frmconvenio.remLen.value		
	var resolucion =frmconvenio.txtResolucion.value
	var NameResolucion=frmconvenio.txtNameResolucion.value	
	var modifica=frmconvenio.txtmodifica.value
	//alert (denominacion)

	if (Renovacion==true){
		Renovacion=1
	}
	else{
		Renovacion=0
	}
	if(denominacion==""){
	cadena = cadena + "Denominación | "
	}

	if (Ambito<0){
	cadena = cadena + "Ámbito | "
	}
	if (modalidad<0){
	cadena = cadena + "Modalidad | "
	}
	if (descripcion==""){
	cadena = cadena + "Objetivos | "
	}	
	if (FechaInicio==""){
	cadena = cadena + "Fecha de Inicio | "
	}		
	//alert (Renovacion)
		if (cadena != "Ingrese los datos: "	){
			alert (cadena)
		}
		else{
	if (confirm("¿Desea eliminar este participante del convenio?")==true){
//		alert ("Elimina")
		location.href="procesar.asp?codigo_cni=" + codigo_cni + "&denominacion=" + denominacion + "&Ambito=" + Ambito + "&modalidad=" + modalidad + "&descripcion=" + descripcion + "&Duracion=" + Duracion + "&Periodo=" + Periodo + "&FechaInicio=" + FechaInicio + "&Renovacion="+ Renovacion + "&NumCopias=" + NumCopias + "&accion=eliminaParticipante" + "&Observacion=" + Observacion + "&Responsable=" + Responsable + "&Referencia=" + Referencia + "&remLen=" + remLen + "&resolucion=" + resolucion + "&NameResponsable=" + NameResponsable + "&NameReferencia=" + NameReferencia + "&NameResolucion="  + NameResolucion + "&modifica=" + modifica + "&codigo_cin=" + codigo_cin
	 }
		}		 
	}
	
	function popUpInstituciones(URL) {
	day = new Date();
	id = day.getTime();
	var izq = 250//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=650,height=400,left = "+ izq +",top = "+ arriba +"');");
	}	
	function popUpAdjuntar(URL) {
	day = new Date();
	id = day.getTime();
	var izq = 300//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=400,height=250,left = "+ izq +",top = "+ arriba +"');");
	}
	function popUp(URL) {
	day = new Date();
	id = day.getTime();
	var izq = 300//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=650,height=400,left = "+ izq +",top = "+ arriba +"');");
	}

	function pupUpModal(URL){
	showModalDialog(URL,window,"dialogWidth:450px;dialogHeight:380px;status:no;help:no;center:yes;scroll:no");	
	window.location.reload()
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
	<form action="procesar.asp?accion=<%=accion%>" method="post" enctype="multipart/form-data" name="frmconvenio" id="frmconvenio">

<%
codigo_cni=Request.QueryString("codigo_cni")	

if Request.QueryString("modifica")="1" then
''response.write Request.QueryString("modifica")
	 Set objconveniodat=Server.CreateObject("PryUSAT.clsAccesoDatos")
	 objconveniodat.AbrirConexiontrans
	 set datosConvenio=objconveniodat.Consultar("ConsultarConvenios","FO","ES",codigo_cni,0,0)
	 objconveniodat.CerrarConexiontrans
	 set objconveniodat=nothing		
	 

	Ambito= datosConvenio(2)
	Modalidad= datosConvenio(4)
	Denominacion= datosConvenio(1)
	descripcion= datosConvenio(6)
	modifica= "1"  
	Duracion= datosConvenio(7)
	if datosConvenio(8)="(Días)" then
		Periodo= "1"
	end if
	if datosConvenio(8)="(Meses)" then
		Periodo= "2"
	end if
	if datosConvenio(8)="(Años)" then
		Periodo= "3"
	end if	
	FechaInicio= datosConvenio(9)
	
	Renovacion=datosConvenio(14)
	NumCopias= datosConvenio(20)
	Observacion=datosConvenio(15)
	''Responsable=datosConvenio(0)
	Referencia=datosConvenio(17)
	resolucion=datosConvenio(18)
	NameResolucion=datosConvenio(19)
	''NameReferencia=datosConvenio(0)
	''NameResponsable=datosConvenio(0)


		if IsNull(Referencia) then
		  	  ''Response.Write("-")
		else
			 Set objRef=Server.CreateObject("PryUSAT.clsAccesoDatos")
			 objRef.AbrirConexiontrans
			 set ConvRef=objRef.Consultar("ConsultarConvenios","FO","ES",Referencia,0,0)
			 objRef.CerrarConexiontrans
			 set objRef=nothing		  
			 NameReferencia=(ConvRef(1))
	 	end if
		''consultar responsable
		 Set objResp=Server.CreateObject("PryUSAT.clsAccesoDatos")
		 objResp.AbrirConexiontrans
		 set Responsables=objResp.Consultar("ConsultarResponsablesConvenio","FO","ES",codigo_cni)
		 objResp.CerrarConexiontrans
		 set objResp=nothing	
		 if Responsables.EOF OR Responsables.BOF THEN
		 else
		 	Responsable=Responsables(1)
			NameResponsable=Responsables(0)
		end if
		remLen=1000-len(descripcion)
else


	remLen=Request.QueryString("remLen")
	Ambito= Request.QueryString("Ambito")
	Modalidad= Request.QueryString("Modalidad")	
	Denominacion= Request.QueryString("denominacion")	

	descripcion= Request.QueryString("descripcion")
	modifica= Request.QueryString("modifica")
	Duracion= Request.QueryString("Duracion")	
	Periodo= Request.QueryString("Periodo")
	FechaInicio= Request.QueryString("FechaInicio")
	Renovacion= Request.QueryString("Renovacion")
	NumCopias= Request.QueryString("NumCopias")
	Observacion=Request.QueryString("Observacion")
	Responsable=Request.QueryString("Responsable")
	Referencia=Request.QueryString("Referencia")
	resolucion=Request.QueryString("resolucion")
	NameResolucion=Request.QueryString("NameResolucion")
	NameReferencia=Request.QueryString("NameReferencia")
	NameResponsable=Request.QueryString("NameResponsable")
end if
	if codigo_cni	="" then
		codigo_cni=0
	else
		codigo_cni=codigo_cni
	end if
	if Periodo = "" then
		Periodo=1
	end if
	if modifica = "" then modifica=0


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
	<input name="txtcodigo_cni" type="hidden" id="txtcodigo_cni" value="<%=Request.QueryString("codigo_cni")%>" size="3" maxlength="3">
	<input name="txtmodifica" type="hidden" id="txtmodifica" value="<%=Request.QueryString("modifica")%>" size="3" maxlength="3">
	<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td class="bordeinf">
	<table width="97%" border="0" align="center" cellpadding="0" cellspacing="5">
      <tr>
        <td><input onClick="Validar1('N')"  name="cmdborrador" type="button" class="guardar_prp" id="cmdborrador" value="         Guardar" >
          &nbsp;&nbsp;
          <input onClick="adjuntar('<%=Request.QueryString("codigo_cni")%>')" name="cmdadjuntar" type="button" class="attach_" id="cmdadjuntar" value="        Archivo Adjunto">
		 
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
                <td width="15%" valign="top"></td>
                <td colspan="3" valign="top"></td>
              </tr>
              <tr>
                <td valign="middle">Denominaci&oacute;n</td>
                <td colspan="3" align="left" valign="top"><input name="txtdenominacion" type="text" class="Cajas2" id="txtdenominacion" value='<% Response.Write(denominacion)%>' size="70" maxlength="399"></td>
              </tr>

              <tr>
                <td valign="middle">Instituciones<img src="../../../../images/menus/prioridad_.gif" name="prior" width="18" height="17" id="prior"  <%if prioridad_prp<>"A" then%> style="display:none" <%end if%>></td>
                <td colspan="3" align="left" valign="top"><input name="cmdinstituciones" type="button"  <%if Request.QueryString("codigo_cni")="" then%>disabled="disabled" <%end if%> class="buscar_prp_small" id="cmdinstituciones" value="     Buscar " onClick="popUp('frmBuscainstituciones.asp?codigo_cni=<%=Request.QueryString("codigo_cni")%>')"></td>
              </tr>
              <tr valign="middle">
                <th height="10%" colspan="4" bordercolor="0">
		<%
	 Set objPART=Server.CreateObject("PryUSAT.clsAccesoDatos")
	 
	 objPART.AbrirConexiontrans
	 set PARTICIPANTES=objPART.Consultar("consultarParticipantesConvenio","FO","ES",codigo_cni)
	 objPART.CerrarConexiontrans
	 set objPART=nothing		
		%>
		
		<table width="100%" border="0" align="center" cellpadding="1" cellspacing="0" class="contornotabla">
          <tr>
            <td colspan="2" align="center" bgcolor="#E1F1FB" class="bordederinf">Instituci&oacute;n</td>
            <td align="center" bgcolor="#E1F1FB" class="bordederinf">Firmante</td>
            <td align="center" bgcolor="#E1F1FB" class="bordederinf">Gestor</td>
          </tr>
		  <%do while Not PARTICIPANTES.eof
		  i=i+1
		  %>
          <tr>		  
            <td bgcolor="#FFFFFF" width="2%" valign="top"><%response.write( i & ".-")%></td>
            <td width="57%" valign="top" bgcolor="#FFFFFF"><%response.write(ConvertirTitulo(PARTICIPANTES(0)))%></td>
            <td width="37%" valign="top" bgcolor="#FFFFFF"><%response.write(PARTICIPANTES(1))%></td>
            <td width="4%" align="right" bgcolor="#FFFFFF">
			<%if PARTICIPANTES(2)=1 then%>
				<img src="../../../../../images/menus/conforme_small.gif" width="16" height="16">
			<%end if%>			
			<%if Request.QueryString("modifica")=1 then%>
				<img src="../../../../../images/menus/Eliminar_s.gif"width="16" height="16" border="0" style="cursor:hand" onClick="eliminaParticipante(<%=PARTICIPANTES("codigo_cin")%>)">
			<%end if%>			</td>			
          </tr>
		  <%PARTICIPANTES.MoveNext
		  loop%>
        </table>				</th>
              </tr>

              <tr>
                <td valign="middle">&Aacute;mbito</td>
                <td colspan="3" align="left" valign="top"><% 
		 	Set objCC1=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC1.AbrirConexiontrans
			set rsAmbito=objCC1.Consultar("ConsultarAmbitoConvenio","FO","TO",0)
	    	ObjCC1.CerrarConexiontrans
			set objCC1=nothing
		
		 	call llenarlista("cboAmbito","",rsAmbito,"codigo_amc","descripcion_amc",Ambito,"Seleccionar Ámbito","","")
			set rsAmbito = nothing
		 %></td>
                </tr>
              <tr>
                <td valign="middle">Modalidad</td>
                <td colspan="3" align="left" valign="top"><% 
		 	Set objCC2=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC2.AbrirConexiontrans
			set rsModalidad=objCC2.Consultar("ConsultarModalidadConvenio","FO","TO",0)
	    	ObjCC2.CerrarConexiontrans
			set objCC2=nothing
		
		 	call llenarlista("cboModalidad","",rsModalidad,"codigo_mdc","descripcion_mdc",Modalidad,"Seleccionar Modalidad","","")
			set rsModalidad = nothing
		 %></td>
                </tr>
              <tr>
                <td align="left" valign="middle">Objetivos</td>
                <td colspan="3" align="right" valign="top">
                <input align="right"				
				<%if remLen<>"" then %>
				value=<%=remLen%>
				<%else%>
				value="1000"
				<%end if%>				 name="remLen" type="text" class="Cajas" size="3" maxlength="4" readonly>
                <span class="piepagina">caracteres</span></td>
                </tr>
              <tr>
                <td colspan="4" align="left" valign="middle">
				<textarea name="txtdescripcion_prp" cols="69" rows="6" wrap="virtual" id="txtdescripcion_prp" tabindex="1" wrap="physical" onKeyDown="textCounter(this.form.txtdescripcion_prp,this.form.remLen,1000);" onKeyUp="textCounter(this.form.txtdescripcion_prp,this.form.remLen,1000);"><%=descripcion%></textarea>				</td>
                </tr>
              
              <tr valign="middle">
                <td colspan="4" align="center">				</td>
              </tr>
              <tr>
                <td valign="middle">Duraci&oacute;n</td>
                <td colspan="3" align="left" valign="middle">
				<input <%if Duracion="" or isnull(Duracion) then %>  disabled="disabled" <%end if%> name="txtDuracion" type="text" class="Cajas" id="txtDuracion" value="<%=Duracion%>" size="10" maxlength="2"  onKeyPress="validarnumero()" >                  
				  &nbsp;&nbsp;&nbsp;
				  <select name="cboPeriodo" class="Cajas" id="cboPeriodo" <%if Duracion="" or isnull(Duracion) then %>  disabled="disabled" <%end if%>>
                    <option value="1" selected="selected">D&iacute;as</option>
                    <option <%if Periodo=2 then %> selected="selected" <%end if%> value="2">Meses</option>
                    <option <%if Periodo=3 then %> selected="selected" <%end if%> value="3">A&ntilde;os</option>
                    <option <%if Periodo=4 then %> selected="selected" <%end if%> value="3">Semanas</option>					
                    </select>
				  <span class="Estilo3">
				  <input name="txtprioridad_prp" type="hidden" id="txtprioridad_prp" value="<%=prioridad_prp%>" size="1" maxlength="1">
                  </span><span class="Estilo3"></font> 
                  <input name="chkDuracionIndefinida" <%if Duracion="" or isnull(Duracion) then %> checked="checked" <%end if%> type="checkbox" class="Cajas" id="chkDuracionIndefinida" onClick="ActivarDuraciones()" value="ON">
                  </span>
                  Duraci&oacute;n Indefinida </td>
                </tr>
              <tr>
                <td valign="middle">Fecha de Inicio </td>
                <td width="24%" align="left" valign="bottom"><input name="txtFechaInicio" type="text" class="Cajas" id="txtFechaInicio" value="<%=FechaInicio%>" size="10" maxlength="10" readonly>
                <input name="Submit" type="button" class="cunia" value="  " onClick="MostrarCalendario('txtFechaInicio')" ></td>
                <td width="12%" align="left" valign="middle"><span class="Estilo4">Fecha de Fin </span></td>
                <td width="49%" align="left" valign="middle"><input name="txtFechafin" type="text" class="Cajas" id="txtFechafin" size="10" maxlength="10" style="visibility:hidden"></td>
              </tr>
              <tr>
                <td valign="middle">Renovaci&oacute;n</td>
                <td align="left" valign="middle"><input name="chkRenovacion" <%if Renovacion=1 then%> checked="checked" <%end if%> type="checkbox" id="chkRenovacion" value="checkbox"></td>
                <td align="left" valign="middle">&nbsp;</td>
                <td align="left" valign="middle">&nbsp;</td>
              </tr>
              <tr>
                <td valign="middle">N&uacute;mero de Copias</td>
                <td align="left" valign="middle"><input name="txtNumCopias" type="text" class="Cajas" id="txtNumCopias" value="<%=NumCopias%>" size="10" maxlength="2" onKeyPress="validarnumero()" ></td>
                <td align="left" valign="middle">Resoluci&oacute;n</td>
                <td align="left" valign="middle"><input name="txtNameResolucion" type="text" disabled="disabled" class="Cajas" id="txtNameResolucion" value="<%=NameResolucion%>" size="30" maxlength="25" >
                  <input name="cmdResolucion" type="button" class="buscar_prp_small" id="cmdResolucion" value="     Buscar" onClick="popUp('frmBuscaResoluciones.asp')">
                  <input name="txtResolucion" type="hidden" class="Cajas" id="txtResolucion" value="<%=Resolucion%>" size="3"></td>
              </tr>
              
              
              <tr>
                <td valign="middle">Observaci&oacute;n</td>
                <td colspan="3" align="left" valign="top"><input name="txtObservacion" type="text" class="Cajas2" id="txtObservacion" value="<%=Observacion%>" size="70" maxlength="150"></td>
              </tr>
              <tr>
                <td valign="middle">Responsable</td>
                <td colspan="3" align="left" valign="top"><input name="txtNameResponsable"  type="text" disabled="disabled" class="Cajas" id="txtNameResponsable" value="<%=NameResponsable%>" size="50" maxlength="50" >
                  <input name="Submit22" type="button" class="buscar_prp_small" value="     Buscar" onClick="popUp('frmBuscaResponsable.asp')">
                  <input name="txtResponsable" type="hidden" class="Cajas" id="txtResponsable" value="<%=Responsable%>" size="3" maxlength="3"></td>
              </tr>
              <tr>
                <td valign="middle">Referencia</td>
                <td colspan="3" align="left" valign="top">
			
				
				  <input name="txtNameReferencia"  type="text" disabled="disabled" class="Cajas" id="txtNameReferencia" value="<%=NameReferencia%>" size="50" maxlength="100">
                  <input name="Submit23" type="button" class="buscar_prp_small" value="     Buscar" onClick="popUp('frmBuscaReferencia.asp')">
                  <input name="txtReferencia" type="hidden" class="Cajas" id="txtReferencia" value="<%=Referencia%>" size="3"></td>
              </tr>
              <tr>
                <td valign="top">&nbsp;</td>
                <td colspan="3" align="left" valign="top">&nbsp;</td>
              </tr>
            </table>
       </td>
      </tr>
    </table></td>
  </tr>
</table>
</form>
</body>
</html>