<!--#include file="../../../../funciones.asp"-->


<html>
<head>
<title>Registro de Reuni&oacute;n de Consejo Universitario</title>

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
.Estilo5 {color: #000000}
.Estilo6 {color: #990000}
-->
</style>

</head>
<script language="JavaScript" src="private/validarpropuestas.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/calendario.js"></script>
<script>

	
	function popUp(URL) {
	day = new Date();
	id = day.getTime();
	var izq = 200//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 100//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=1,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=700,height450,left = "+ izq +",top = "+ arriba +"');");
	}
function textCounter(field, countfield, maxlimit) {
if (field.value.length > maxlimit) // if too long...trim it!
field.value = field.value.substring(0, maxlimit);
else 
countfield.value = maxlimit - field.value.length;
}
// End -->
function ProgramarAgenda(){
	var codigo_rec=document.all.codigo_rec.value
	if (codigo_rec!=''){
		popUp("AsignarPropuestasReunion.asp?codigo_rec=" + codigo_rec)
	}
}

function Validar(accion){
		var codigo_rec=document.all.codigo_rec.value
		var nombre=document.all.TxtNombre.value		
		var fecha=document.all.TxtFecha.value				
		var lugar=document.all.TxtLugar.value	
		var tipo=document.all.CboTipo.value			
		var cadena="Ingrese los Campos: "		
		
		if (nombre=="") {
			cadena=cadena + "Nombre | "
		}
		
		if (fecha=="") {
			cadena=cadena + "Fecha | "
		}	
		
		if (lugar=="") {
			cadena=cadena + "Lugar | "
		}	
					
		if (cadena=="Ingrese los Campos: "){
		//	if (codigo_rec==''){
				if (confirm ("Desea guardar los datos de la Reunión de Consejo Universitario")==true){
					location.href="procesar.asp?accion=registrareunion&Nombre=" + nombre + "&Fecha=" + fecha + "&Lugar=" + lugar + "&modifica=" + accion + "&tipo=" + tipo + "&codigo_rec=" + codigo_rec 
				}
		//	}	
		//	else{
		//		alert("Modificar")
		//	}
				
		}
		else{
			//alert (cadena)
		}
		
}
function eliminarItem(codigo){
		var codigo_rec=document.all.codigo_rec.value
		var nombre=document.all.TxtNombre.value		
		var fecha=document.all.TxtFecha.value				
		var lugar=document.all.TxtLugar.value	
		var tipo=document.all.CboTipo.value	
		if (confirm("¿Desea Eliminar la propuesta?")==1){
			location.href="procesar.asp?accion=eliminaItemAgenda&Nombre=" + nombre + "&Fecha=" + fecha + "&Lugar=" + lugar + "&tipo=" + tipo + "&codigo_rec=" + codigo_rec  + "&codigo_prp=" + codigo
		}
}
function postegarPropuesta(propuesta){

		var codigo_rec=document.all.codigo_rec.value
		var nombre=document.all.TxtNombre.value		
		var fecha=document.all.TxtFecha.value				
		var lugar=document.all.TxtLugar.value	
		var tipo=document.all.CboTipo.value	
		if (confirm("¿Desea Postergar la propuesta?")==1){
			location.href="procesar.asp?accion=postegarPropuesta&Nombre=" + nombre + "&Fecha=" + fecha + "&Lugar=" + lugar + "&tipo=" + tipo + "&codigo_rec=" + codigo_rec  + "&codigo_prp=" + propuesta
		}
}
function AdjuntarGrabacion(){
	var codigo_rec=document.all.codigo_rec.value	
	day = new Date();
	id = day.getTime();
	var izq = 300//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	var URL="adjuntargrabacion.asp?codigo_rec=" + codigo_rec	
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=1,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=500,height=250,left = "+ izq +",top = "+ arriba +"');");
}

function verActa(){
	var codigo_rec=document.all.codigo_rec.value	
	day = new Date();
	id = day.getTime();
	var izq = 80//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 20//(screen.height-alto)/2
	var URL="GenerarActa.asp?codigo_rec=" + codigo_rec	
	location.href=URL

	//eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=1,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=900,height=600');");
}
function verPresentacion(){
	var codigo_rec=document.all.codigo_rec.value	
	day = new Date();
	id = day.getTime();
	var izq = 300//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	var URL="presentacion_intro.asp?codigo_rec=" + codigo_rec	
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,fullscreen=yes');");
}
function AdjuntarActa(){
	var codigo_rec=document.all.codigo_rec.value	
	day = new Date();
	id = day.getTime();
	var izq = 300//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	var URL="AdjuntarActa.asp?codigo_rec=" + codigo_rec	
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=1,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=500,height=250,left = "+ izq +",top = "+ arriba +"');");
}
</script>

<body topmargin="0" rightmargin="0" leftmargin="0">
<%
//codigo_rec=Request.QueryString("codigo_rec")
codigo_rec=Request.QueryString("codigo_rec")
if Request.QueryString("modifica")= "1" then
		Set Reunion=Server.CreateObject("PryUSAT.clsAccesoDatos")
		Reunion.AbrirConexion
			set RsReunion=Reunion.Consultar("ConsultarReunionConsejo","FO","PR",codigo_rec,0,0)
		Reunion.CerrarConexion
	NOMBRE=RsReunion("agenda_rec")
	LUGAR=RsReunion("lugar_rec")
	FECHA=RsReunion("FECHA_rec")
	TIPO=RsReunion("TIPO_rec")		
else
	NOMBRE=Request.QueryString("NOMBRE")
	LUGAR=Request.QueryString("LUGAR")
	FECHA=Request.QueryString("FECHA")
	TIPO=Request.QueryString("TIPO")
end if

%>
	<form action="procesar.asp?accion=<%=accion%>" method="post" enctype="multipart/form-data" name="frmpropuesta" id="frmpropuesta">

<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td height="47" class="bordeinf"><table width="98%" border="0" align="center" cellpadding="0" cellspacing="5">
      <tr>
        <td><input onClick="Validar('N')"   name="cmdguardar" type="button" class="guardar_prp" id="cmdguardar" value="          Guardar">
          <input <%if codigo_rec="" then%> disabled="disabled"<%end if%> onClick="AdjuntarGrabacion()" name="cmdadjuntar" type="button" class="grabacion_prp" id="cmdadjuntar" value="        Grabaci&oacute;n">
          <input <%if codigo_rec="" then%> disabled="disabled"<%end if%> onClick="AdjuntarActa()" name="cmdadjuntar" type="button" class="nuevocomentario" id="cmdadjuntar" value="         Acta">
          <input <%if codigo_rec="" then%> disabled="disabled"<%end if%> onClick="verPresentacion()" name="cmdadjuntar2" type="button" class="presentacion" id="cmdadjuntar2" value="     Presentaci&oacute;n">
          <input <%if codigo_rec="" then%> disabled="disabled"<%end if%> onClick="verActa()" name="cmdadjuntar22" type="button" class="enviarpropuesta" id="cmdadjuntar22" value="      Genera Acta" style="visibility:hidden"></td>
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
                <td colspan="2" valign="middle"><span class="Estilo5">
                  <input  name="codigo_rec" type="hidden" id="codigo_rec" value="<%=CODIGO_REC%>" size="2" maxlength="2">
                  <input name="modificar_rec" type="hidden" id="modificar_rec" size="2" maxlength="2">
                </span></td>
                <td colspan="3" valign="top"><span class="Estilo5"></span></td>
              </tr>
              <tr>
                <td colspan="2" align="left" valign="top"><span class="Estilo5"><strong>Nombre</strong></span></td>
                <td align="center" valign="top"><span class="Estilo5"><strong>:</strong></span></td>
                <td colspan="2" align="left" valign="top"><input name="TxtNombre" type="text" class="Cajas2 " id="TxtNombre" value="<%=NOMBRE%>" maxlength="100"></td>
              </tr>
              <tr>
                <td colspan="2" align="left" valign="top"><span class="Estilo5"><strong>Fecha</strong></span></td>
                <td align="center" valign="top"><span class="Estilo5"><strong>:</strong></span></td>
                <td colspan="2" align="left" valign="top"><span class="Estilo5">
                  <input name="TxtFecha" type="text" disabled="disabled" class="Cajas" id="TxtFecha" value="<%=FECHA%>" size="10" maxlength="10">
                  <input name="Submit" type="button" class="cunia" value="  " onClick="MostrarCalendario('TxtFecha')" >
                </span></td>
              </tr>
              <tr>
                <td colspan="2" align="left" valign="top"><span class="Estilo5"><strong>Lugar</strong></span></td>
                <td align="center" valign="top"><span class="Estilo5"><strong>:</strong></span></td>
                <td colspan="2" align="left" valign="top"><input name="TxtLugar" type="text" class="Cajas2 " id="TxtLugar" value="<%=LUGAR%>" maxlength="100"></td>
              </tr>
              <tr>
                <td colspan="2" align="left" valign="top"><span class="Estilo5"><strong>Tipo</strong></span></td>
                <td align="center" valign="top"><span class="Estilo5"><strong>:</strong></span></td>
                <td colspan="2" align="left" valign="top"><span class="Estilo5">
                  <select name="CboTipo" id="CboTipo">
                    <option <%IF TIPO ="O" THEN%>  selected="selected" <%END IF%> value="O">Ordinaria</option>
                    <option <%IF TIPO ="E" THEN%>  selected="selected" <%END IF%> value="E">Extraordinaria</option>
                  </select>
                </span> </td>
              </tr>
              
              <tr>
                <td colspan="5" align="center" valign="top"><span class="Estilo5"></span></td>
              </tr>
              <tr>
                <td colspan="4" align="left" valign="middle"><span class="Estilo5"><strong>Agenda de la Reuni&oacute;n de Consejo</strong> </span></td>
                <td width="61%" align="left" valign="top">
				<input  name="cmdProgramar" type="button" class="buscar1 " id="cmdProgramar" onClick="ProgramarAgenda()" value=" Programar Agenda" width="300" <%if codigo_rec="" then%>disabled="disabled" <%end if%>>
				</td>
              </tr>
              <tr>
                <td colspan="5" align="center" valign="top">

				<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="contornotabla Estilo5">
                  <tr>
                    <td colspan="2" align="center" bgcolor="#E1F1FB" class="bordeinf"><span class="Estilo5"><strong>Descripci&oacute;n de la Propuesta </strong></span></td>
                    <td width="11%" align="center" bgcolor="#E1F1FB" class="bordeinf Estilo5">&nbsp;</td>
                    <td width="11%" align="center" bgcolor="#E1F1FB" class="bordeinf"><span class="Estilo5"><strong>Fecha</strong></span></td>

                    <td width="5%" align="center" bgcolor="#E1F1FB" class="Estilo5 bordeinf"><strong class="bordeizq">Acciones</strong></td>

                  </tr>
				  				<%
				if 	codigo_rec<>"" then
				Set Agenda=Server.CreateObject("PryUSAT.clsAccesoDatos")
				Agenda.AbrirConexion
				set RsAgenda=Agenda.Consultar("ConsultarAgendaReunionConsejo","FO","TO",codigo_rec,"")
				Agenda.CerrarConexion
								%>
                  <%do while not RsAgenda.eof
	  				i=i+1
	  %>
                  <tr>
                    <td width="3%" bgcolor="#FFFFFF"><span class="Estilo5"><%=i%>.-&nbsp;</span></td>		
					<%
					select case RsAgenda("estado_prp")
						case "A" 
							estadoActual="Aprobada"
						case "R" 
							estadoActual="Denegada"
						case "P" 
							estadoActual="Pendiente"
					end select
					%>
                    <td width="50%" valign="top" bgcolor="#FFFFFF"><span class="Estilo5"><%=RsAgenda(1)%>.<%=RsAgenda(3)%> </span> </td>
                    <td align="center" valign="top" bgcolor="#FFFFFF"><span class="Estilo6">(<%=estadoActual%>)</span></td>
                    <td align="center" valign="top" bgcolor="#FFFFFF"><span class="Estilo5"><%=RsAgenda(2)%></span></td>

                    <td align="center" bgcolor="#FFFFFF">					  <img  border="0"  style="cursor:hand" src="../../../../images/menus/noconforme_small.gif" alt="Eliminar" width="16" height="16"  onClick="eliminarItem('<%=RsAgenda(0)%>')" <%if RsAgenda("estado_prp")<>"P" then%> style="visibility:hidden" <%end if%>>  <img  style="cursor:hand" src="../../../../images/menus/menu3.gif" alt="Postergar" width="16" height="16"  onClick="postegarPropuesta('<%=RsAgenda(0)%>')" <%if RsAgenda("estado_prp")<>"P" then%> style="visibility:hidden" <%end if%>></td>

                  </tr>
                  <%RsAgenda.MoveNext
	  				loop
				end if
		%>				  
                </table></td>
              </tr>
              <tr>
                <td colspan="5" align="center" valign="top"><span class="Estilo5"></span></td>
              </tr>
			  <tr>
                <td width="5%" align="left" valign="top">&nbsp;</td>
                <td width="16%" align="right" valign="top"><span class="Estilo5"></span></td>
			    <td width="2%" align="right" valign="top"><span class="Estilo5"></span></td>
			    <td colspan="2" align="right" valign="top"><span class="Estilo3"></font> </span></td>
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
