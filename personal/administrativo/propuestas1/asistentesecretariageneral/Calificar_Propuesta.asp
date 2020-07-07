<!--#include file="../../../../funciones.asp"-->


<html>
<head>
<title>Calificar la propuesta</title>

<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--
.Estilo5 {
	font-size: 10pt;
	font-weight: bold;
	color: #000000;
}
.Estilo6 {
	color: #000000;
	font-weight: bold;
}
.Estilo8 {color: #000000}
.Estilo10 {
	font-size: 9px;
	font-weight: bold;
}
-->
</style></head>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/calendario.js"></script>
<script language="JavaScript">
function RegistrarInstitucion(){
var resolucion=frminstitucion.txtresolucion.value
var tipo=frminstitucion.cbotipo.value
var fecha=frminstitucion.txtFechaInicio.value

var cadena = "Ingrese los datos: "	

if (resolucion==""){
cadena = cadena + " Resolución |"
}
if (tipo<0){
	cadena = cadena + " Tipo |"
}
if (fecha==""){
	cadena = cadena + " Fecha |"
}

if (cadena != "Ingrese los datos: "){
	alert(cadena)
}
else{
	location.href="procesar.asp?resolucion=" + resolucion + "&tipo=" + tipo + "&fecha=" + fecha  + "&accion=resolucion"
	window.opener.location.reload()
	window.close()
}
}

function RegistraAcuerdo(codigo_prp,codigo_rec){
var acuerdo=frminstitucion.txtacuerdo.value
	if (acuerdo==""){
	alert ("Debe escribir el contenido del acuerdo antes de guardarlos")
	}
	else{
//	alert (codigo_prp)
//	alert (codigo_rec)	
	location.href="procesar.asp?codigo_prp=" + codigo_prp + "&codigo_rec=" + codigo_rec + "&acuerdo=" + acuerdo  + "&accion=registraAcuerdo"	
	}
}
function Actualizar(codigo_apr,codigo_prp){
//	alert (codigo_apr)
	var texto=eval ("frminstitucion.TextArea_" + codigo_apr + ".value")
//	alert (texto)
	location.href="registraAcuerdo.asp?codigo_apr="+codigo_apr+"&texto="+texto+"&guardar=si&codigo_prp="+codigo_prp
	eval ("frminstitucion.guardar_" + codigo_apr + ".style.visibility='hidden'")
}

function Eliminar(codigo_apr,codigo_prp){
//	alert (codigo_apr)
//	alert (codigo_prp)	
	location.href="registraAcuerdo.asp?codigo_apr="+codigo_apr+"&eliminar=si&codigo_prp="+codigo_prp
}
function modificar(objeto,boton){
//alert (boton)
eval ("frminstitucion." + boton + ".style.visibility='visible'")
eval ("frminstitucion." + objeto + ".style.border='inset'")
eval ("frminstitucion." + objeto + ".readOnly=false")
}

	function popUp(URL) {
	day = new Date();
	id = day.getTime();
	var izq = 300//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=400,height=350,left = "+ izq +",top = "+ arriba +"');");
	}
	function EnviarResponsable(codigo_prp,codigo_per,codigo_cco,informe){
		//alert(informe)
		codigo_per=frmResponsable.cboPersonal.value
		codigo_cco=frmResponsable.cbocentrodecostos.value	
		document.all.cargar.style.visibility="visible"
		frmResponsable.action="calificar_propuesta.asp?accion=guardar&codigo_prp="+codigo_prp+"&codigo_per="+codigo_per+"&codigo_cco="+codigo_cco+"&informe="+informe //  (instancia B) proponente que guarda como borrador |  (instancia P) proponente que envìa como director
		frmResponsable.submit()
		window.opener.location.reload()
	}
</script>
	  
<body topmargin="0" rightmargin="0" leftmargin="0">
<%
''CARAGR DATOS DEL COMBO PERSONAL
    Set objPER=Server.CreateObject("PryUSAT.clsAccesoDatos")
   	objPER.AbrirConexion	
	    Set rsPER = objPER.Consultar("spPla_ConsultarPersonal", "FO", "CB", 0)
   	objPER.CerrarConexion
	set objPER=nothing
%>
<%
codigo_per=Request.QueryString("codigo_per")
''codigo_cco=Request.QueryString("codigo_cco")
codigo_prp=Request.QueryString("codigo_prp")
''codigo_rec=Request.QueryString("codigo_rec")
guardar=Request.QueryString("guardar")
eliminar=Request.QueryString("eliminar")
''informe_prp=Request.QueryString("informe")
%>


<%
if Request.QueryString("accion")="guardar" then
	codigo_apr=Request.QueryString("codigo_apr")
    Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
   	objProp.AbrirConexionTrans
		if informe_prp="IN" then
   	''-----------------------------------------------------------
	''INFORMA A LOS INVOLUCRADOS LA CALIFICACION DEL INFORME
	''-----------------------------------------------------------		
			objPropuesta.Ejecutar "ModificarEstadoPropuesta",false,"ap",codigo_prp,"A"
	''		Set Rs=objProp.Consultar("ConsultarEmailInvolucradoPropuesta","FO","CA",codigo_prp,0,"")
	objProp.CerrarConexionTrans
	''		do while not rs.eof
	''		Set Obj= Server.CreateObject("AulaVirtual.clsEmail")
	''			Mensajes=Obj.EnviarCorreo("sistemapropuestas@usat.edu.pe",Rs("asunto"),Rs("mensaje"),Rs("email"))
	''		Set Obj=nothing
	''		rs.MoveNext
	''		loop
	''		set rs=nothing			
		else
   	''-----------------------------------------------------------------------------------------------------
	''INFORMA AL RESPONSABLE Y A LOS INVOLUCRADOS LA CALIFICACION DE LA PROPUESTA Y EL RESPONSABLE ASIGNADO
   	''-----------------------------------------------------------------------------------------------------
			objPropuesta.Ejecutar "ModificarEstadoPropuesta",false,"AP",codigo_prp,"A"

			objProp.Ejecutar "RegistraInformeAsignado",false,"AR",codigo_prp,codigo_per,codigo_cco
			
	''		Set Rs=objProp.Consultar("ConsultarEmailInvolucradoPropuesta","FO","IR",codigo_prp,codigo_per,"")
	objProp.CerrarConexionTrans
	''		do while not rs.eof
	''		Set Obj= Server.CreateObject("AulaVirtual.clsEmail")
	''			Mensajes=Obj.EnviarCorreo("sistemapropuestas@usat.edu.pe",Rs("asunto"),Rs("mensaje"),Rs("email"))
	''		Set Obj=nothing
	''		rs.MoveNext
	''		loop
	''		set rs=nothing			
		end if
	set objProP=nothing
	Response.Write("<script>window.close()</script>")

end if
%>
<%
''consultar si hay datos de un responsable y Área para esa propuesta
   if codigo_cco="" AND Request.QueryString("SELECCION")<>"SI" then
   		//Response.Write("<script> alert('Esta propusta ya ha sido asignada un responsable')</script>")
		Set objResp=Server.CreateObject("PryUSAT.clsAccesoDatos")
		objResp.AbrirConexion
			set RsResp=objResp.Consultar ("RegistraInformeAsignado","fo","CR",codigo_prp,0,0)
		objResp.CerrarConexion
		set objResp=nothing
		if RsResp.eof then
		else
		
			codigo_per=RsResp(0)
			codigo_cco=RsResp(1)		
		end if
	end if
%>
<form action="RegistraResponsable.asp?accion=guardar&codigo_prp=<%=codigo_prp%>&codigo_per=<%=codigo_per%>&codigo_cco=<%=codigo_cco%>" method="post" name="frmResponsable" id="frmResponsable">
  <table width="100%" border="0" cellspacing="0" cellpadding="0">

    <tr>
      <td bgcolor="#F0F0F0" class="bordeinf"><table  width="95%" height="100%" border="0" align="center" cellpadding="0" cellspacing="5">
        <tr>
          <td valign="top"><input name="Submit" type="button" onClick="EnviarResponsable('<%=codigo_prp%>','<%=codigo_per%>','<%=centrocosto%>','<%=informe_prp%>')" class="conforme1" value="         Aprobar"> <input name="Submit2" type="button" onClick="EnviarResponsable('<%=codigo_prp%>','<%=codigo_per%>','<%=centrocosto%>','<%=informe_prp%>')" class="editar1" value="          Observar">
            <input name="Submit3" type="button" onClick="EnviarResponsable('<%=codigo_prp%>','<%=codigo_per%>','<%=centrocosto%>','<%=informe_prp%>')" class="noconforme1" value="         Denegar">
            <input name="Submit42" type="button" onClick="EnviarResponsable('<%=codigo_prp%>','<%=codigo_per%>','<%=centrocosto%>','<%=informe_prp%>')" class="enviarpropuesta" value="          Derivar"></td>
	    	<%	    
		  	Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	objProp.AbrirConexion
			set propuesta=objProp.Consultar("ConsultarPropuestas","FO","CP","","",codigo_prp,"","")
			Set proponente = objProp.Consultar("ConsultarInvolucradoPropuesta","FO","PR",codigo_prp,0)
	    	objProp.CerrarConexion
			set objProP=nothing
			
			if cstr(codigo_per)=""  then		
				codigo_per=proponente("codigo_per")
			//	response.write (codigo_per)
			end if
			informe_prp=propuesta("esinforme_prp")
			''Response.Write(informe_prp)
			%>
          </tr>
      </table></td>
    </tr>
    <tr>
      <td valign="top"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td width="100%"><table width="80%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td colspan="3" align="center" valign="top">&nbsp;</td>
            </tr>
            <tr>
              <td colspan="3" align="center" valign="top"><span class="Estilo5"> Responsable de Ejecuci&oacute;n de la Propuesta:</span></td>
              </tr>
            <tr>
              <td colspan="3" align="center" valign="top"><font size="2"color="#990000"><strong><%=propuesta("nombre_prp")%></strong></font></td>
            </tr>
            
            <tr>
              <td colspan="2">&nbsp;</td>
              <td>&nbsp;</td>
            </tr>
            <tr>
              <td colspan="2">&nbsp;</td>
              <td>&nbsp;</td>
            </tr>
            <tr>
              <td width="26%"><span class="Estilo8 Estilo6"><strong>Personal</strong></span></td>
              <td width="3%"><span class="Estilo6">:</span></td>
              <td width="71%"><span class="Estilo6 Estilo8">
                <%call llenarlista("cboPersonal","actualizarlista('calificar_propuesta.asp?SELECCION=SI&codigo_per=' + this.value + '&codigo_prp="& codigo_prp &"')",rsPER,"codigo_per","personal",codigo_per,"Seleccione un trabajador","","")
			  	rsPER.close
		    	set rsPer= nothing
				 Set objProp1=Server.CreateObject("PryUSAT.clsAccesoDatos")
				 objProp1.AbrirConexion
					 set personal=objProp1.Consultar("spPla_ConsultarPersonal","FO","ES",codigo_per)
				 objProp1.CerrarConexion
				 set objProp1=nothing				
			  if codigo_per<>"" then
			  %>
                <%end if%>
              </span></td>
            </tr>
            <tr>
              <td>&nbsp;</td>
              <td>&nbsp;</td>
              <td>&nbsp;</td>
            </tr>
            <tr>
              <td><span class="Estilo6">Área </span></td>
              <td><span class="Estilo6">:</span></td>
              <td><span class="Estilo8">
                <% 
				centrocosto=propuesta("codigo_cco")
		 	Set objCC=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC.AbrirConexion
				''consulta los centros de costos a los que est&aacute; asigando el trabajador
				if centrocosto<>""  then
					centrocosto=centrocosto
				else
				set rsCentroCostos=ObjCC.Consultar("ConsultarCentroCosto","FO","PR",CODIGO_PER)
				IF rsCentroCostos.EOF THEN
					centrocosto=""
				ELSE	
					centrocosto=rsCentroCostos("codigo_cco")
				END IF
				end if
				if codigo_cco<>"" then
					centrocosto=codigo_cco					
				end if
				set rsCentrosdeCostos=objCC.Consultar("ConsultarCentroCosto","FO","TO",0)
			ObjCC.CerrarConexion
			set objCC=nothing

		 	CALL llenarlista("cbocentrodecostos","",rsCentrosdeCostos,"codigo_cco","descripcion_Cco",centrocosto,"Seleccionar Área","","")
			set rsCentroCostos = nothing
		 %>
              </span></td>
            </tr>
            <tr>
              <td colspan="2">&nbsp;</td>
              <td>&nbsp;</td>
            </tr>
            <tr>
              <td colspan="3"><table width="100%" border="0" align="center" cellpadding="3" cellspacing="0" bgcolor="#FFFFFF">
                <tr>
                  <td><span class="Estilo8 Estilo6 Estilo8"><strong>Dedicaci&oacute;n</strong></span></td>
                  <td width="44%"><span class="Estilo8">
                    <%
			  
					  if IsNull(personal("descripcion_ded")) then
					  	dedicacion= ""
					  else
					 	dedicacion = " - " & personal("descripcion_ded")
					  end if					  
					  Response.Write(dedicacion)
					
					  
					  %>
                  </span></td>
                  <td width="33%" rowspan="3" align="center" valign="middle"><%IF ISNULL(personal("foto")) THEN%>
                      <img src="../../../../images/FotoVacia.gif" width="100" height="130" border="0" usemap="#Map">
                      <%ELSE%>
                      <img src="../../../imgpersonal/<%=personal("codigo_per") &  ".jpg"%>" Width="100px" Height="130px" border="0" usemap="#Map" class="contornotabla_azul">
                      <%END IF%>                  </td>
                </tr>
                <tr>
                  <td><span class="Estilo6 Estilo8">Tipo </span></td>
                  <td><span class="Estilo8">
                    <%
					  if IsNull(personal("descripcion_tpe")) then
					  	tipo= ""
					  else
					  	tipo =" - " & personal("descripcion_tpe")
					  end if
					  
					  Response.Write(tipo)									  
					  %>					  
                    </span></td>
                  </tr>
                <tr>
                  <td valign="top"><p class="Estilo6">Observaci&oacute;n:</p>                    <span class="Estilo10">(<span class="Estilo8">Hasta</span> <span class="Estilo8" id="txtCuentaObservacion">500 caracteres.</span>)</span></td>
                  <td align="left" valign="top"><textarea name="txtObservacion" cols="40" rows="5" id="txtObservacion"  onKeyUp="ContarTextArea(txtObservacion,'500',txtCuentaObservacion)"></textarea></td>
                  </tr>
                <tr>
                  <td colspan="2">&nbsp;</td>
                  <td width="33%" align="center" valign="middle">&nbsp;</td>
                </tr>
                
                
                
              </table>
             </td>
              </tr>
		
            <tr>
              <td colspan="3" ></td>
            </tr>


          </table></td>
        </tr>
        <tr>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
        </tr>

      </table></td>
    </tr>
  </table>
</form>

<DIV ID="cargar" style="visibility:hidden; position:absolute; top:40%; left:25%" >
<table class="contornotabla" bgcolor="#FFFFCC" cellpadding="5" cellspacing="5" align="center">
<TR>
	<TD>

	</TD>
</tR>
<TR>
	<TD align="center">
	<strong>
		<span style=" color:#990000">	Por favor, espere mientras se procesa la información... </span>
	</strong>
	</TD>
</tR>
<TR>
	<TD align="center">
		<img src="../../../../images/cargando.gif" >
	</TD>
</tR>
<TR>
	<TD>

	</TD>
</tR>
</table>
</DIV>
</body>
</html>
