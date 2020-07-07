<!--#include file="../../../../../funciones.asp"-->
<html>
<head>
<title>Presentaci&oacute;n de Reuni&oacute;n de Consejo Universitario</title>

<link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css">
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
.Estilo6 {
	color: #000000;
	font-size: 16;
}
.Estilo8 {
	font-size: 16px;
	color: #000000;
	font-family: Arial, Helvetica, sans-serif;
}
-->
</style>

</head>
<script language="JavaScript" src="private/validarpropuestas.js"></script>
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
        <td height="1%" colspan="5" align="center"><span class="Estilo3">Observaciones y Comentarios a la propuesta </span></td>
      </tr>
      <tr>
        <td width="3%" height="5%" align="center" valign="top">&nbsp;</td>
        <td width="73%" align="right" valign="middle" ><a href="presentacion_agenda.asp?CODIGO_REC=<%=codigo_rec%>"> <img style="visibility:hidden" src="../../../../../images/img3.gif"width="32" height="32" border="0"></a> &nbsp;&nbsp;</td>
        <td width="7%" align="left" valign="middle"><span class="Estilo51"><strong><a style="visibility:hidden"  href="presentacion_agenda.asp?CODIGO_REC=<%=codigo_rec%>">Volver a Agenda </a></strong></span></td>
        <td width="5%" align="right" valign="middle"><a href="presentacion_propuesta.asp?CODIGO_REC=<%=codigo_rec%>&codigo_prp=<%=codigo_prp%>"> <img src="../../../../../images/menus/envio_prop.gif"width="35" height="35" border="0"></a> &nbsp;&nbsp; </td>
        <td width="12%" align="left" valign="middle"><span class="Estilo51"><strong><a href="presentacion_propuesta.asp?CODIGO_REC=<%=codigo_rec%>&codigo_prp=<%=codigo_prp%>">Volver a Propuesta </a></strong></span></td>
      </tr>
      
      <tr>
        <td height="5%" colspan="5" align="center" valign="top"><hr color="#990000"></td>
      </tr>
      <tr>
        <td height="70%" colspan="5" valign="top"><table width="98%" height="100%" border="0" align="left" cellpadding="0" cellspacing="0">         
          <tr>
            <td align="left" valign="top">
<table width="100%" height="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td align="left" valign="top" class="Estilo6">

	<%
	if Request.QueryString("codigo_prp")<>"" then
	codigo_prp=Request.QueryString("codigo_prp")
	codigo_per=session("codigo_Usu")
	Set objInv=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objInv.AbrirConexion
	set involucrado=objInv.Consultar("ConsultarInvolucradoPropuesta","FO","JE",codigo_per,codigo_prp)
	codigo_ipr=involucrado(0)
	''Response.write(codigo_inv)
	objInv.CerrarConexion
	set objInv=nothing
	set involucrado = nothing	

	Set objCom=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objCom.AbrirConexiontrans
		set comentario1=objCom.Consultar("ConsultarComentarios","FO","PI",codigo_prp,0)
	objCom.CerrarConexiontrans 
	set objCom=nothing
	''Response.Write(comentario1.RecordCount)
	if comentario1.EOF then
	else
	
	coment1=comentario1("codigo_cop")
	set comentario1 = nothing
	%>

      <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="3">

      <tr>
        <td width="57%" align="center" valign="top">
		
		<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
                      <tr>
                        <td valign="top">
					      <span class="Estilo8">
					      <%		
					CargarComentarios coment1					
					%>
					      </span></td>
                      </tr>
                      <tr>
                        <td valign="top">&nbsp;</td>
                      </tr>
              </table>		  </td>
        </tr>
    </table></td>
  </tr>
</table>
<span class="Estilo6">
  <%end if
  end if
  %>			
</span></td>
          </tr>
        </table></td>
    </table>
	</td>
  </tr>
</table>

</form>
</body>
</html>
<%
Public Sub CargarComentarios(byval codigorespuesta_cop)
dim objCom1  
dim RsComentarios
 //consulta mi recordset
 		Set objCom1=Server.CreateObject("PryUSAT.clsAccesoDatos")
		objCom1.AbrirConexiontrans
		set RsComentarios=objCom1.Consultar("ConsultarComentarios","FO","CO",codigorespuesta_cop,0)
		objCom1.CerrarConexiontrans
		set objCom1=nothing%>
		<table  width="97%" align="right" cellspacing="0" border="0" cellpadding="0">
		<input type="hidden" id="txtelegido">
		<%
		do while not  RsComentarios.eof
		o=o+1
		%>
		<tr>
		<td>
		<%
			if RsComentarios("HIJOS")>0 then
				//response.write(RsComentarios("asunto_cop"))
				//response.write("<br>")
				//inicio 
				  ''response.write(RsComentarios("codigo_cop"))
 					Set objLeido=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objLeido.AbrirConexion
					set RsLeido=objLeido.Consultar("ConsultarComentarios","FO","CP",RsComentarios(0),codigo_per)
					objLeido.CerrarConexion
					set objLeido=nothing
					if   RsLeido.BOF=true then
						leido=0
					else
						leido=1							
					end if
				%>
				  <table  cellpadding="1" cellspacing="1" width="100%"   border="0" cellpadding="0" cellspacing="0" class="contornotabla" id="fila<%=RsComentarios(0)%>" bgcolor="#FFFFCC"> 
    <tr>
      <td width="75%" align="left" valign="middle">
	 <font size="3" color="#000066" face="Arial, Helvetica, sans-serif"> 
	 <strong>De: <%response.write(RsComentarios("nombre"))%></strong></font>
	  </td>
      <td width="25%" align="center" valign="middle">	 
  		<font size="2" color="#000066" face="Arial, Helvetica, sans-serif"> <strong>Enviado el: </strong>
        <%response.write(RsComentarios("fecha_cop"))%>
		</font>
	  </td>
    </tr>
    <tr>
      <td colspan="2" align="left">
	  <font size="3" color="#990000" face="Arial, Helvetica, sans-serif">
	  <strong>Asunto: <%response.write(RsComentarios("asunto_cop"))%>
	 </strong></font>
	  </td>
      </tr>

      <td colspan="2" align="left" >
	  <div align="justify"> <font  size="3" color="#000000" face="Arial, Helvetica, sans-serif">
	  <strong>
        <%response.write(RsComentarios("comentario_cop"))%>
	</strong>	
      </font>
	  </div>
		</td>

	  </tr> 	  
  </table>
				<%//fin				
				call CargarComentarios(RsComentarios("codigo_cop"))				
			else			
			//inicio
					''response.write(RsComentarios(0))
 					Set objLeido=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objLeido.AbrirConexion
					set RsLeido=objLeido.Consultar("ConsultarComentarios","FO","CP",RsComentarios(0),codigo_per)
					objLeido.CerrarConexion
					if   RsLeido.BOF=true then
						leido=0
					else
						leido=1	
					end if					
			%>
			  <table cellpadding="1" bgcolor="#FFFFCC" cellspacing="1"  width="100%"  border="0" cellpadding="0" cellspacing="0" class="contornotabla" align="right">
    <tr>
      <td   width="75%" align="left" valign="middle">
	 <font size="3" color="#000066" face="Arial, Helvetica, sans-serif"> 
	 <strong>De: <%response.write(RsComentarios("nombre"))%></strong></font>
	  </td>
      <td width="25%" align="center" valign="middle">	  
  		<font size="2" color="#000066" face="Arial, Helvetica, sans-serif"> <strong>Enviado el: </strong>
        <%response.write(RsComentarios("fecha_cop"))%>
		</font>
	  </td>
    </tr>
    <tr>
      <td colspan="2" align="left" >
	  <font size="3" color="#990000" face="Arial, Helvetica, sans-serif">
	  <strong>Asunto: <%response.write(RsComentarios("asunto_cop"))%>
	 </strong></font>
	  </td>
</tr>

      <td colspan="2" align="left" >
	  <div align="justify"> <font  size="3" color="#000000" face="Arial, Helvetica, sans-serif">
	  <strong>
        <%response.write(RsComentarios("comentario_cop"))%>
	</strong>	
      </font>
	  </div>
		</td>
      </tr> 	    
    <tr>
    </tr>
  </table>		
			
<%//		fin
//		response.write(RsComentarios("asunto_cop"))
	//			response.write("<br>")
			end if				
			RsComentarios.movenext
		%>					
		</td><tr>		
		<%
		loop %>
	</table>
		<%
		set RsComentarios = nothing	
  end Sub
%>
