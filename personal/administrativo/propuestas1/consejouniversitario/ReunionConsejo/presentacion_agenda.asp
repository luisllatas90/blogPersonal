<!--#include file="../../../../../funciones.asp"-->
<html>
<head>
<title>Registro de Reuni&oacute;n de Consejo Universitario</title>

<link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--

.Estilo3 {
	color: #990000;
	font-size: 18pt;
	font-weight: bold;
	font-family: Arial, Helvetica, sans-serif;
}
.Estilo5 {
	font-family: Verdana;
	font-weight: bold;
	font-size: 10pt;
	color: #000000;
}
.Estilo21 {
	color: #990000;
	font-weight: bold;
	font-size: 14px;
	font-family: Arial, Helvetica, sans-serif;
}
.Estilo23 {color: #003366; font-size: 18px; font-weight: bold; }
-->
</style>

</head>
<!--<script language="JavaScript" src="private/validarpropuestas.js"></script>-->
<script language="JavaScript" src="../../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../../private/calendario.js"></script>
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
%>
	<form action="procesar.asp?accion=<%=accion%>" method="post" enctype="multipart/form-data" name="frmpropuesta" id="frmpropuesta">

<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  
  <tr>
    <td valign="top">
	<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td height="5%" align="right" valign="top">
		<span style="cursor:hand" class="Estilo5" onClick="window.close()">X</span>&nbsp;&nbsp;
      </tr>
      <tr>
        <td height="5%" align="center">&nbsp;</td>
      </tr>
      <tr>
        <td height="10%" align="center" valign="top"><span class="Estilo3">AGENDA: </span></td>
      </tr>
      
      <tr>
        <td height="5%" align="center" valign="top"><%
	Set Agenda=Server.CreateObject("PryUSAT.clsAccesoDatos")
	Agenda.AbrirConexion
	set RsAgenda=Agenda.Consultar("ConsultarAgendaReunionConsejo","FO","TO",codigo_rec,"A")
	Agenda.CerrarConexion
	%></td>
      </tr>
      <tr>
        <td height="70%" valign="top"><table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">

          <%do while not RsAgenda.eof
	  	i=i+1
	  %>
          <tr>
            <td width="9%"><span class="Estilo23"><%=i%>.-&nbsp;</span></td>
            <td width="75%"><span class="Estilo23"><a href="presentacion_propuesta.asp?codigo_rec=<%=codigo_rec%>&codigo_prp=<%=RsAgenda(0)%>"><%=RsAgenda("NOMBRE_PRP")%></a></span></td>
            <td width="16%" align="right"><span class="Estilo21"></span><span class="Estilo21">
              
			  <%
			
			  select case RsAgenda("estado")
					case "P"
			 		 Response.Write("(" & "Postergada" & ")")
			  end select
			  %> 
              
			  <%
			  select case RsAgenda("estado_prp")
			  	case "A"
					Response.Write("(" &"Aprobado" & ")")
			  	case "R"
					Response.Write("(" &"Denegado" & ")")
			  	case "D"
					Response.Write("(" &"Derivada" & ")")
			  	case "O"
					Response.Write("(" &"Observada" & ")")	
			  	case "P"
					Response.Write("")										
			end select
			  %> 
                 </span></td>
          </tr>
          <tr>
            <td colspan="3">&nbsp;</td>
            </tr>
          <%RsAgenda.MoveNext
	  	loop%>
        </table>
		</td>
      </tr>
      <tr>
        <td height="5%">&nbsp;</td>
      </tr>
    </table>
	</td>
  </tr>
</table>

</form>
</body>
</html>