<!--#include file="../../../../funciones.asp"-->
<html>
<head>
<title>Presentaci&oacute;n de Reuni&oacute;n de Consejo Universitario</title>

<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--
.Estilo5 {
	font-family: Verdana;
	font-weight: bold;
	font-size: 10pt;
	color: #000000;
}
.Estilo25 {
	font-size: 18px;
	color: #000000;
	font-weight: bold;
}
.Estilo26 {
	color: #990000;
	font-size: 15px;
	font-family: Arial, Helvetica, sans-serif;
	font-weight: bold;
}
.Estilo27 {
	font-size: 18px;
	font-weight: bold;
	font-family: Arial, Helvetica, sans-serif;
	color: #0066CC;
}
.Estilo37 {color: #000000;
font-size:16}
.Estilo46 {font-size: 18px}
.Estilo51 {
	color: #990000;
	font-weight: bold;
	font-size: 18px;
}
.Estilo52 {
	color: #990000;
	font-weight: bold;
	font-size: 18px;
}
.Estilo54 {
	color: #0066CC;
	font-size: 18px;
}
.Estilo55 {
	color: #990000;
	font-size: 18px;
	font-family: Arial, Helvetica, sans-serif;
	font-weight: bold;
}
.Estilo57 {
	color: #000000;
	font-weight: bold;
}
.Estilo58 {color: #000000; font-size: 16; font-weight: bold; }
-->
</style>
</head>
<script language="JavaScript" src="private/validarpropuestas.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/calendario.js"></script>
<script language="JavaScript" src="../../../../private/tooltip.js"></script>
<script>
function enviarVersion(codigo_prp,version,inf){
	if (inf=='IN'){
		fraversion.location.href="datosVersionInforme_Presentacion.asp" + "?codigo_prp=" + codigo_prp + "&version=" +  version
	}
	else{
		fraversion.location.href="datosVersion_presentacion.asp" + "?codigo_prp=" + codigo_prp + "&version=" +  version	
	}
	
}
	function AbrirPopUps(pagina,alto,ancho,ajustable,bestado,barras)
{
   izq = (screen.width-ancho)/2
   arriba= (screen.height-alto)/2

   var ventana=window.open(pagina,"popup","height="+alto+",width="+ancho+",statusbar="+bestado+",scrollbars="+barras+",top" + arriba + ",left" + izq + ",resizable="+ajustable+",toolbar=no,menubar=no");
   ventana.location.href=pagina
   ventana=null
}
</script>
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
function RegistraResponsable(codigo_prp,codigo_per,Informe){
	day = new Date();
	id = day.getTime();
	var izq = 180//(screen.width-ancho)/2

   	var arriba= 180//(screen.height-alto)/2
//	if (Informe="IN"{
//		var URL="RegistraResponsable.asp?codigo_prp="+codigo_prp+"&codigo_per="+codigo_per+"&Informe="+Informe		
//	}
//	else{
		var URL="RegistraResponsable.asp?codigo_prp="+codigo_prp+"&codigo_per="+codigo_per+"&Informe="+Informe
		eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=1,location=0,statusbar=0,status=0,menubar=0,resizable=0,width=650,height=450,left = "+ izq +",top = "+ arriba +"');");

//	}
}

function RegistraDiscusion(codigo_prp,codigo_rec){
	//var codigo_rec=document.all.codigo_rec.value	
	day = new Date();
	id = day.getTime();
	var izq = 180//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 180//(screen.height-alto)/2
	var URL="RegistraDiscusion.asp?codigo_prp="+codigo_prp+"&codigo_rec="+codigo_rec
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=1,location=0,statusbar=0,status=0,menubar=0,resizable=0,width=650,height=450,left = "+ izq +",top = "+ arriba +"');");
}

function RegistraAcuerdo(codigo_prp,codigo_rec){
	//var codigo_rec=document.all.codigo_rec.value	
	day = new Date();
	id = day.getTime();
	var izq = 180//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 180//(screen.height-alto)/2
	var URL="RegistraAcuerdo.asp?codigo_prp="+codigo_prp+"&codigo_rec="+codigo_rec
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=1,location=0,statusbar=0,status=0,menubar=0,resizable=0,width=650,height=450,left = "+ izq +",top = "+ arriba +"');");
}

function Aprobar (codigo_prp,CODIGO_REC,codigo_per,informe){
//alert (informe)
	if (confirm ("¿Desea Aprobar la propuesta?")==true){
		if (informe=="IN") {
			document.all.cargar.style.visibility="visible"
			location.href="procesar.asp?accion=aprueba_prp&codigo_prp=" + codigo_prp + "&CODIGO_REC=" + CODIGO_REC		
		}
		else{
			RegistraResponsable (codigo_prp,codigo_per,informe)
		}

	}
}
function Denegar (codigo_prp,CODIGO_REC){
	if (confirm ("¿Desea Denegar la propuesta?")==true){
		document.all.cargar.style.visibility="visible"
		location.href="procesar.asp?accion=Denegar_prp&codigo_prp=" + codigo_prp + "&CODIGO_REC=" + CODIGO_REC		
	}
}
function IrAgenda(codigo_rec,codigo_prp,codigo_per){
	var TxtEstado=document.all.TxtEstado.value
	//alert(codigo_per)
	if (TxtEstado=="P"){
		if (confirm("No ha calificado la propuesta, es necesario que sea Aprobada o Denegada, ¿Desea salir de la propuesta?")==0){
			
		}
		else{
		
		location.href="presentacion_agenda.asp?CODIGO_REC="+codigo_rec
		}
	}
	else{
		location.href="presentacion_agenda.asp?CODIGO_REC="+codigo_rec
	}
}
</script>

<body topmargin="0" rightmargin="0" leftmargin="0">

<form action="procesar.asp?accion=<%=accion%>" method="post" enctype="multipart/form-data" name="frmpropuesta" id="frmpropuesta">
  <p>
    <%
//codigo_rec=Request.QueryString("codigo_rec")
codigo_rec=Request.QueryString("codigo_rec")
codigo_prp=Request.QueryString("codigo_prp")
%>
  </p>
  <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
    <tr>
      <td height="2%" colspan="3" align="right" valign="top"><span style="cursor:hand" class="Estilo5" onClick="window.close()">
        <%if codigo_prp <>"" then

	 Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	objProp.AbrirConexiontrans
			set propuesta=objProp.Consultar("ConsultarPropuestas","FO","CP","","",codigo_prp,"","")
	    	objProp.CerrarConexiontrans
			set objProP=nothing
	%>
        <%informe_prp=propuesta("esinforme_prp")%>
      </span> <span style="cursor:hand" class="Estilo5" onClick="window.opener.location.reload()	 ;window.close()">X</span>&nbsp;&nbsp; </td>
    </tr>
    <tr>
      <td height="3%" colspan="3" align="center"><span class="Estilo52 Estilo46">
        <%if isnull(propuesta("codigoreferencia_prp")) then
			Response.Write("PROPUESTA: &nbsp;")
		else
			Response.Write("INFORME: &nbsp;")
		end if
		%>
        <span class="Estilo54">
        <%response.write(propuesta("nombre_prp"))%>
      </span></span></td>
    </tr>
    <tr>
      <td height="4%" colspan="3" align="center" valign="top"><TABLE align="center" cellpadding="0" cellspacing="0">
          <%
					Set objInt=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objInt.AbrirConexiontrans
					set interesado=objInt.Consultar("ConsultarResponsablesPropuesta","FO","PR",codigo_prp,0)
					objInt.CerrarConexiontrans
					set objInt=nothing
					interesado.MoveLast
					codigo_per=interesado("codigo_per")
					interesado.MoveFirst
					do while not interesado.eof
						%>
          <TR>
            <TD align="center" class="Estilo26 Estilo46"><%response.write(interesado(0))%>
            </TD>
          </TR>
          <%
						interesado.movenext()
					loop
					set interesado = nothing					
					%>
        </TABLE>
          <span class="Estilo25 Estilo46">(
            <%response.write(propuesta("DESCRIPCION_CCO"))%>
            )</span></td>
    </tr>
    <tr>
      <td height="3%" colspan="3" align="center" valign="top">&nbsp;
        <%''Response.Write(propuesta("estado_prp"))%></td>
    </tr>
    <tr>
      <td height="3%" align="left" valign="top"><span style="cursor:hand" class="Estilo27">&nbsp;&nbsp;Propuesta registrada el:
        <%response.write(propuesta(2))%>
        por el proponente. </span></td>
      <td width="4%" align="right" valign="middle"><img src="../../../../images/img3.gif" width="32" height="32" border="0" style="cursor:hand" onClick="IrAgenda(<%=codigo_rec%>,<%=codigo_prp%>,<%=codigo_per%>)" > </td>
      <td width="14%" align="left" valign="middle"><span style="cursor:hand" class="Estilo52" onClick="IrAgenda(<%=codigo_rec%>,<%=codigo_prp%>,<%=codigo_per%>)">&nbsp; Agenda </span></td>
    </tr>
    <tr>
      <td height="1%" colspan="3" align="left" valign="top"><hr style="color:#990000"></td>
    </tr>


    <tr>
      <td height="4%" colspan="3"><%
				end if
				SET propuesta = NOTHING
				%>
      &nbsp;</td>
    </tr>
  </table>
  <p>&nbsp;    </p>
</form>

<DIV ID="cargar" style="visibility:hidden; position:absolute; top:40%; left:35%" >
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