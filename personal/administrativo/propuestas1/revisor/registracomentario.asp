<!--#include file="../../../../funciones.asp"-->


<html>
<head>
<title>Registro de Comentarios</title>

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
</style>
	
	<%
	codigo_cop=Request.QueryString("codigo_cop")
	if Request.QueryString("respuesta") = 1 then
		coment1=codigo_cop
	else
		coment1=Request.QueryString("coment1")
	end if
	
	if Request.QueryString("estado_prp")="A" then
		estado_prp="A"
	else
		estado_Prp="P"
	end if
	
	%>
</head>
<script language="JavaScript" src="private/validarpropuestas.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script>

	function ReemplazaCadenaEnter(cadena){
	var n = cadena.length
	for (var i=0; i<n; i++)
		cadena = cadena.replace('\n',"<br>");	
	return cadena
	}	

	function CambiarCheck(){
		if (document.all.ChkComentario.checked==true){
			frmpropuesta.txtasunto_cop.value="Comentario"
		}
		else{
			frmpropuesta.txtasunto_cop.value="Observación"
		}			
		document.all.txtcomentario_cop.focus()
	}
	
	function Validar(modo)
	{	
		var codigo_prp=frmpropuesta.txtcodigo_prp.value	
		var newcodigo_cop=frmpropuesta.txtnewcodigo_cop.value
		var asunto_cop=frmpropuesta.txtasunto_cop.value
		var comentario_cop = ReemplazaCadenaEnter(frmpropuesta.txtcomentario_cop.value)
		var codigo_ipr=<%=Request.QueryString("codigo_ipr")%>
		var estado_prp="<%=estado_prp%>"
		
		
		var coment1=<%=coment1%>//Request.QueryString("coment1")
		
		var remLen=frmpropuesta.remLen.value	
		var codigo_cop=	frmpropuesta.txtnewcodigo_cop.value
		var cadena = "Ingrese los datos: "		

		if (asunto_cop==""){
		cadena = cadena + "Asunto del Comentario | "
		}
		if (comentario_cop==""){
		cadena = cadena + "Descripción del Comentario | "
		}
		if (cadena=="Ingrese los datos: "){		
		 	switch(modo)
			{						
			case "A":
				if (newcodigo_cop==""){
				//alert("Guardar nuevo comentario con estado I, y adjuntar")
//				location.href="procesar.asp?codigo_ipr=" + codigo_ipr + "&nivelrestriccion_cop=P&codigorespuesta_cop=" + coment1 + "&estado_cop=I&asunto_cop=" + asunto_cop + "&comentario_cop=" + comentario_cop + "&remLen=" + remLen + "&accion=guardarcomentario&codigo_prp=" + codigo_prp	+ "&attach=1"									
				location.href="procesar.asp?codigo_ipr=" + codigo_ipr + "&nivelrestriccion_cop=" + estado_prp + "&codigorespuesta_cop=" + coment1 + "&estado_cop=I&asunto_cop=" + asunto_cop + "&comentario_cop=" + comentario_cop + "&remLen=" + remLen + "&accion=guardarcomentario&codigo_prp=" + codigo_prp	+ "&attach=1"									
				}
				else{
					popUp('../../../../libreriaNet/propuestas/adjuntar.aspx?codigo_prp=' + codigo_prp + '&codigo_cop=' + codigo_cop + '&estado_prp=' + estado_prp)
				}
			
			//		location.href="procesar.asp?codigo_pcc=" + codigo_pcc + "&codigo_tpr=" + codigo_tpr + "&codigo_fac=" + codigo_fac + "&nombre_prp=" + nombre_prp + "&descripcion_prp=" + descripcion_prp + "&codigo_prp=" + codigo_prp + "&remLen=" + remLen + "&instancia_prp=P&prioridad_prp=" + prioridad_prp + "&accion=actualizar"			
			//		popUp('adjuntar.asp?codigo_prp=' + codigo_prp)
					//alert("error")				
			break
			case "E":
			var mensaje=confirm("¿Desea enviar la observación?")
			if (mensaje==true){
				if (newcodigo_cop==""){
//				location.href="procesar.asp?codigo_ipr=" + codigo_ipr + "&nivelrestriccion_cop=P&codigorespuesta_cop=" + coment1 + "&estado_cop=A&asunto_cop=" + asunto_cop + "&comentario_cop=" + comentario_cop + "&remLen=" + remLen + "&accion=guardarcomentario&codigo_prp=" + codigo_prp + "&envio_cop=1"							
				location.href="procesar.asp?codigo_ipr=" + codigo_ipr + "&nivelrestriccion_cop=" + estado_prp + "&codigorespuesta_cop=" + coment1 + "&estado_cop=A&asunto_cop=" + asunto_cop + "&comentario_cop=" + comentario_cop + "&remLen=" + remLen + "&accion=guardarcomentario&codigo_prp=" + codigo_prp + "&envio_cop=1"							
//				alert("cerrar ventana")
				}
				else{
//				location.href="procesar.asp?codigo_ipr=" + codigo_ipr + "&nivelrestriccion_cop=P&codigorespuesta_cop=" + coment1 + "&estado_cop=A&asunto_cop=" + asunto_cop + "&comentario_cop=" + comentario_cop + "&remLen=" + remLen + "&accion=actualizacomentario&codigo_prp=" + codigo_prp + "&codigo_cop=" + codigo_cop + "&envio_cop=1"	
				location.href="procesar.asp?codigo_ipr=" + codigo_ipr + "&nivelrestriccion_cop=" + estado_prp + "&codigorespuesta_cop=" + coment1 + "&estado_cop=A&asunto_cop=" + asunto_cop + "&comentario_cop=" + comentario_cop + "&remLen=" + remLen + "&accion=actualizacomentario&codigo_prp=" + codigo_prp + "&codigo_cop=" + codigo_cop + "&envio_cop=1"	
				//alert("cerrar ventana")
				}
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
	var izq = 300//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=400,height=390,left = "+ izq +",top = "+ arriba +"');");
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

<center>
</center>
<%
	codigo_cop=Request.QueryString("codigo_cop")
	codigo_prp=Request.QueryString("codigo_prp")
	comentario_cop=Request.QueryString("comentario_cop")
	asunto_cop=Request.QueryString("asunto_cop")
	if Request.QueryString("respuesta") = 1 then
		coment1=codigo_cop
	else
		coment1=Request.QueryString("coment1")
	end if
	
	codigo_ipr=Request.QueryString("codigo_ipr")
	remLen=Request.QueryString("remLen")
	newcodigo_cop=Request.QueryString("newcodigo_cop")
	if Request.QueryString("attach") <>"" then
		attach=1
	else
		attach=0
	end if
	if attach=1 then%>
		<script>
		popUp("../../../../libreriaNet/propuestas/adjuntar.aspx?codigo_prp=" + <%=codigo_prp%> + "&codigo_cop=" + <%=newcodigo_cop%>)
		</script>
	<%end if%>
		
<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td class="bordeinf"><table width="97%" border="0" align="center" cellpadding="0" cellspacing="5">
      <tr>
        <td>&nbsp;
          <input onClick="Validar('E')"   name="cmdenviar" type="button" class="enviarpropuesta" id="cmdenviar" value="          Enviar">
          &nbsp;
          <input onClick="Validar('A')" name="cmdadjuntar" type="button" class="attach_" id="cmdadjuntar" value="        Adjuntar">
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
                <td width="93%" colspan="2" valign="top"></td>
              </tr>

              <tr>
                <td colspan="2" valign="middle">Asunto</td>
                <td colspan="2" valign="top"><input value="<%if asunto_cop="" then Response.Write("Comentario") else Response.Write(asunto_cop)end if%>" name="txtasunto_cop" type="text" id="txtasunto_cop" tabindex="1" size="70" maxlength="35" /></td>
                </tr>
              <tr>
                <td colspan="2" align="left" valign="top"><input onClick="Validar('A')"   width="50" name="Submit3222" type="button" class="attach" value="      Ver adjuntos"></td>
                <td colspan="2" align="right" valign="bottom">&nbsp;</td>
                </tr>
              
              <tr>
                <td colspan="4" align="center" valign="top">
				<textarea name="txtcomentario_cop" cols="69" rows="12" wrap="virtual" id="txtcomentario_cop" tabindex="1" wrap="physical" onKeyDown="textCounter(this.form.txtcomentario_cop,this.form.remLen,1000);" onKeyUp="textCounter(this.form.txtcomentario_cop,this.form.remLen,1000);"><%=Replace(comentario_cop,"<br>",chr(13))%></textarea></td>
              </tr>
			  <tr>
                <td width="5%" align="left" valign="top"><img src="../../../../images/cargando2.gif" width="20" height="20">
                  <input name="txtcodigopadre_cop" type="hidden" id="txtcodigopadre_cop" value="<%=codigopadre_cop%>" size="1" maxlength="1"></td>
                <td width="2%" align="right" valign="top"><input name="txtcodigo_prp" type="hidden" id="txtcodigo_prp" value="<%=codigo_prp%>" size="1" maxlength="1"></td>
			    <td align="right" valign="top"><span class="Estilo3">
			      <input name="txtcoment1" type="hidden" id="txtcoment1" value="<%=coment1%>" size="1" maxlength="1">
                  <input name="txtnewcodigo_cop" type="hidden" id="txtnewcodigo_cop" value="<%=newcodigo_cop%>" size="1" maxlength="1">
			    </span></td>
			    <td align="right" valign="top"><span class="Estilo3"><input align="right" name="remLen" type="text" class="piepagina" 
				<%if remLen<>"" then %>
				value=<%=remLen%>
				<%else%>
				value="1000"
				<%end if%>
				 size="3" maxlength="4" readonly>
				 <script>
				 	document.all.txtcomentario_cop.focus()
				 </script>
                </span><span class="piepagina">caracteres</span><span class="Estilo3"></font> </span></td>
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
