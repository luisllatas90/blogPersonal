<!--#include file="../../../../funciones.asp"-->
<html>
<head>
<title>Presentaci&oacute;n de Reuni&oacute;n de Consejo Universitario</title>

<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--

.Estilo3 {
	color: #990000;
	font-size: 14pt;
	font-weight: bold;
	font-family: Arial, Helvetica, sans-serif;
}
.Estilo5 {
	font-family: Verdana;
	font-weight: bold;
	font-size: 10pt;
	color: #000000;
}
.Estilo17 {font-size: 18px; font-weight: bold; color: #000000; }
.Estilo18 {font-size: 18px}
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
	var izq = 300//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=1,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=500,height=450,left = "+ izq +",top = "+ arriba +"');");
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
		location.href="procesar.asp?accion=eliminaItemAgenda&Nombre=" + nombre + "&Fecha=" + fecha + "&Lugar=" + lugar + "&tipo=" + tipo + "&codigo_rec=" + codigo_rec  + "&codigo_prp=" + codigo

}
function postegarPropuesta(propuesta){

		var codigo_rec=document.all.codigo_rec.value
		var nombre=document.all.TxtNombre.value		
		var fecha=document.all.TxtFecha.value				
		var lugar=document.all.TxtLugar.value	
		var tipo=document.all.CboTipo.value	
		
		location.href="procesar.asp?accion=postegarPropuesta&Nombre=" + nombre + "&Fecha=" + fecha + "&Lugar=" + lugar + "&tipo=" + tipo + "&codigo_rec=" + codigo_rec  + "&codigo_prp=" + propuesta

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
	codigo_prp=Request.QueryString("codigo_prp")
	codigo_per=session("codigo_Usu")
%>
	<form action="procesar.asp?accion=<%=accion%>" method="post" enctype="multipart/form-data" name="frmpropuesta" id="frmpropuesta">

<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  
  <tr>
    <td valign="top">
	<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td height="5%" colspan="5" align="right" valign="top">
		<span style="cursor:hand" class="Estilo5" onClick="window.close()">X</span>&nbsp;&nbsp;      </tr>
      <tr>
        <td height="1%" colspan="5" align="center"><span class="Estilo3">Calificaciones a la propuesta </span></td>
      </tr>
      <tr>
        <td width="3%" height="5%" align="center" valign="top">&nbsp;</td>
        <td width="73%" align="right" valign="middle" ><a href="presentacion_agenda.asp?CODIGO_REC=<%=codigo_rec%>"> <img style="visibility:hidden" src="../../../../images/img3.gif"width="32" height="32" border="0"></a> &nbsp;&nbsp;</td>
        <td width="7%" align="left" valign="middle"><span class="Estilo51"><strong><a style="visibility:hidden"  href="presentacion_agenda.asp?CODIGO_REC=<%=codigo_rec%>">Volver a Agenda </a></strong></span></td>
        <td width="5%" align="right" valign="middle"><a href="presentacion_propuesta.asp?CODIGO_REC=<%=codigo_rec%>&codigo_prp=<%=codigo_prp%>"> <img src="../../../../images/menus/envio_prop.gif"width="35" height="35" border="0"></a> &nbsp;&nbsp; </td>
        <td width="12%" align="left" valign="middle"><span class="Estilo51"><strong><a href="presentacion_propuesta.asp?CODIGO_REC=<%=codigo_rec%>&codigo_prp=<%=codigo_prp%>">Volver a Propuesta </a></strong></span></td>
      </tr>
      
      <tr>
        <td height="5%" colspan="5" align="center" valign="top"><hr color="#990000"></td>
      </tr>
      <tr>
        <td height="70%" colspan="5" valign="top"><table width="98%" height="100%" border="0" align="left" cellpadding="0" cellspacing="0">         
          <tr>
            <td align="left" valign="top">
			<%
			' VERIFICA SI ES PROPUESTA PROVENIENTE DE CONSEJO UNIVERSITARIO O DE FACULAD
			Set objDestino=Server.CreateObject("PryUSAT.clsAccesoDatos")
			objDestino.AbrirConexion
			 destino=objDestino.Ejecutar("PRP_ConsejoDeDestino",true,codigo_prp,"")
			objDestino.CerrarConexion
			
			if destino="U"  then
			%> 			
			<table width="95%" border="0" align="center" cellpadding="0" cellspacing="3">
              <%
					Set objRev=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objRev.AbrirConexiontrans
					set revisor=objRev.Consultar("ConsultarResponsablesPropuesta","FO","UF",codigo_prp,0)
					objRev.CerrarConexiontrans
					set objRev=nothing					
						
					%>
              <tr>
                <td colspan="4" align="center"><span class="Estilo17">Consejo Universitario </span><span class="Estilo18"></span><span class="Estilo18"></span></td>
                </tr>
              <tr>
                <td colspan="2" align="left">&nbsp;</td>
                <td width="7%" align="center">&nbsp;</td>
                <td width="17%" align="center">&nbsp;</td>
              </tr>
              <tr>
                <td colspan="2" align="left">&nbsp;</td>
                <td align="center">&nbsp;</td>
                <td align="center">&nbsp;</td>
              </tr>
              <tr>
                <td colspan="2" align="left"><span class="Estilo18"></span></td>
                <td align="center"><span class="Estilo18"></span></td>
                <td align="center"><span class="Estilo18"></span></td>
              </tr>
              <%
					revisor.movefirst()
					do while not revisor.eof
						if ucase(revisor(1))="C" then
%>
              <tr>
                <td width="3%" align="left"><span class="Estilo17">-</span></td>
                <td width="73%" align="left"><span class="Estilo17">
                  <%response.write(revisor(0))%>
                </span></td>
                <td align="center"><span class="Estilo17">
                  <%
							select case revisor(2)
							case "P"%>
                  <img border="0" src="../../../../images/menus/menu3.gif">
                  <%case "C"%>
                  <img border="0" src="../../../../images/menus/conforme_small.gif">
                  <%case "N"%>
                  <img border="0" src="../../../../images/menus/noconforme_small.gif">
                  <%case "O"%>
                  <img border="0" src="../../../../images/menus/editar_1_s.gif">
                  <%end select				

						%>
                </span></td>
                <td align="center"><span class="Estilo17">
                  <%
				select case UCase(revisor(2))
		  		case "P"
					response.write("Pendiente")
		  		case "C"
					response.write("Conforme")
		  		case "N"
					response.write("<FONT COLOR=blue>No Conforme</font>")
				case "O"
					response.write("<FONT COLOR=blue>Observado</font>")	
		  		case "-"
					response.write("Informado")									
				end select					
				%>
                </span></td>
              </tr>
              <% end if
					revisor.movenext()
				loop%>
              <% set revisor = nothing %>
            </table>
			<p>
			  <% else %>
		            </p>
			<table width="95%" border="0" align="center" cellpadding="0" cellspacing="3">
              		
	  <tr>	
			    <td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
	  </tr>
	        <%
					Set objRev=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objRev.AbrirConexiontrans
					set revisor=objRev.Consultar("ConsultarResponsablesPropuesta","FO","UF",codigo_prp,0)
					objRev.CerrarConexiontrans
					set objRev=nothing						
			%>
	<% if revisor.recordcount>1 then%>			
	  <tr align="center" class="Estilo17">
	    <td colspan="4">Consejo Universitario </td>
	    </tr>
	  <tr>
	    <td>&nbsp;</td>
	    <td>&nbsp;</td>
	    <td>&nbsp;</td>
	    <td>&nbsp;</td>
	    </tr>
		<%
		revisor.movefirst()
		do while not revisor.eof
		if ucase(revisor(1))="C" then
		%>		
	  <tr class="Estilo17">
        <td align="left">-</td>
	    <td align="left"><%response.write(revisor(0))%>        </td>
	    <td align="center"><%
							select case revisor(2)
							case "P"%>
            <img border="0" src="../../../../images/menus/menu3.gif">
            <%case "C"%>
            <img border="0" src="../../../../images/menus/conforme_small.gif">
            <%case "N"%>
            <img border="0" src="../../../../images/menus/noconforme_small.gif">
            <%case "O"%>
            <img border="0" src="../../../../images/menus/editar_1_s.gif">
            <%end select				

						%></td>
	    <td align="center"><%
							select case revisor(2)
							case "P"
								response.write("Pendiente")
							case "C"
								response.write("Conforme")
							case "N"
								response.write("No Conforme")
							case "O"
								response.write("Observado")	
							case "-"
								response.write("Informado")		
							end select				
						%></td>
	    </tr>	
		      <% end if
					revisor.movenext()
				loop
				set revisor = nothing %>
		<%else%>
<tr>
                <td colspan="4" align="center" class="Estilo17"><strong>Consejo de Facultad </strong></td>
                </tr>
              <tr>
                <td colspan="2" align="left">&nbsp;</td>
                <td width="4%" align="center">&nbsp;</td>
                <td width="21%" align="center">&nbsp;</td>
              </tr>
              <%Set objRev=Server.CreateObject("PryUSAT.clsAccesoDatos")
			objRev.AbrirConexiontrans
			set revisor=objRev.Consultar("ConsultarResponsablesPropuesta","FO","FA",codigo_prp,0)
			objRev.CerrarConexiontrans
			set objRev=nothing					
					do while not revisor.eof

						%>
              <tr class="Estilo17">
                <td width="3%" align="left" class="Estilo17">-</td>
                <td width="72%" align="left"><%response.write(revisor(0))%></td>
                <td align="center"><%
							select case revisor(2)
							case "P"%>
                    <img border="0" src="../../../../images/menus/menu3.gif">
                    <%case "C"%>
                    <img border="0" src="../../../../images/menus/conforme_small.gif">
                    <%case "N"%>
                    <img border="0" src="../../../../images/menus/noconforme_small.gif">
                    <%case "O"%>
                    <img border="0" src="../../../../images/menus/editar_1_s.gif">
                    <%end select				

						%></td>
                <td align="center">
				<%
							select case revisor(2)
							case "P"
								response.write("Pendiente")
							case "C"
								response.write("Conforme")
							case "N"
								response.write("No Conforme")
							case "O"
								response.write("Observado")
							case "-"
								response.write("Informado")																																		
							end select				
							%></td>
              </tr>             
			 <% 
					revisor.movenext()  
					loop
					set revisor = nothing 
			%>
			  <tr>
			   <td>&nbsp;</td>
			   <td>&nbsp;</td>
			   <td>&nbsp;</td>
			   <td>&nbsp;</td>
			  </tr>
			  <tr>
			    <td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
				<td>&nbsp;</td>
			    </tr>

      <%		Set objRev=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objRev.AbrirConexiontrans
					set revisor=objRev.Consultar("ConsultarResponsablesPropuesta","FO","RC",codigo_prp,0)
					objRev.CerrarConexiontrans
					set objRev=nothing					
					do while not revisor.eof
						if UCase(revisor(1))="R" then
						%>
						
      <tr class="Estilo17">
        <td width="3%" align="left">-</td>
        <td width="72%" align="left"><%response.write(revisor(0))%>        </td>
        <td align="center"><%
							select case revisor(2)
							case "P"%>
              <img border="0" src="../../../../images/menus/menu3.gif">
              <%case "C"%>
              <img border="0" src="../../../../images/menus/conforme_small.gif">
              <%case "N"%>
              <img border="0" src="../../../../images/menus/noconforme_small.gif">
              <%case "O"%>
              <img border="0" src="../../../../images/menus/editar_1_s.gif">
              <%end select%>
			  </td>
        <td align="center"><%		
							select case revisor(2)
							case "P"
								response.write("Pendiente")
							case "C"
								response.write("Conforme")
							case "N"
								response.write("No Conforme")
							case "O"
								response.write("Observado")	
							case "-"
								response.write("Informado")																
							end select		
						
						 
						%></td>
        <% end if
					revisor.movenext()
					loop						
					
					set revisor = nothing %>
      </tr>		
		<% end if%>  	  
            </table>
			    <% end if%>
			  
			  </td>
          </tr>
        </table></td>
    </table>
	</td>
  </tr>
</table>

</form>
</body>
</html>
