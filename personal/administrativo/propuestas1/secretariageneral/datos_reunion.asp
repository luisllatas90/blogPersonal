<html>
<head>
<script>
function Informar(propuesta,codigo_rec){

		if (confirm("¿Desea Informar de los acuerdos de la propuesta?")==1){
			cargar.style.visibility='visible'
			location.href="datos_reunion.asp?accion=InformarAcuerdos&codigo_rec=" + codigo_rec  + "&codigoEnvAcu=" + propuesta

		}
}
function postegarPropuesta(propuesta,codigo_rec){


		if (confirm("¿Desea Postergar la propuesta?")==1){
			location.href="datos_reunion.asp?accion=postegarPropuesta&codigo_rec=" + codigo_rec  + "&codigo_prp=" + propuesta
		}
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
function RegistraResponsable(codigo_prp,codigo_per){
	//var codigo_rec=document.all.codigo_rec.value	
	day = new Date();
	id = day.getTime();
	var izq = 180//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 180//(screen.height-alto)/2
	var URL="RegistraResponsable.asp?codigo_prp="+codigo_prp+"&codigo_per="+codigo_per
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


</script>
<title>Datos reuni&oacute;n</title>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
body {
	background-color: #FFFFFF;
}
-->
</style>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">

<style type="text/css">
<!--
.Estilo1 {
	color: #000000;
	font-weight: bold;
}
.Estilo2 {color: #000000}
.Estilo3 {color: #990000}
-->
</style>
</head>

<DIV ID="cargar" style="visibility:hidden; position:absolute; top:20%; left:20%" >
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
<p>

<%
if Request.QueryString("informar")="SI" then
	codigo_rec=Request.QueryString("codigo_rec")
	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexionTrans
	Set Rs=objPropuesta.Consultar("ConsultarEmailInvolucradoPropuesta","FO","RE",codigo_rec,0,"")
	objPropuesta.CerrarConexionTrans
	do while not rs.eof
	Set Obj= Server.CreateObject("AulaVirtual.clsEmail")
		Mensajes=Obj.EnviarCorreo("sistemapropuestas@usat.edu.pe",Rs("asunto"),Rs("mensaje"),Rs("email"))
	Set Obj=nothing
	rs.MoveNext
	loop
	set rs=nothing				

	Response.Write("<script>alert('Se ha informado de la reunión a los miembros de Consejo Universitario via E-mail')</script>")
end if
%>
<%if Request.QueryString("accion")="InformarAcuerdos" then

	codigo_rec=Request.QueryString("codigo_rec")
	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexionTrans
	Set Rs=objPropuesta.Consultar("ConsultarEmailInvolucradoPropuesta","FO","AC",Request.QueryString("codigoEnvAcu"),0,"")
	objPropuesta.CerrarConexionTrans
	do while not rs.eof
	Set Obj= Server.CreateObject("AulaVirtual.clsEmail")
		Mensajes=Obj.EnviarCorreo("sistemapropuestas@usat.edu.pe",Rs("asunto"),Rs("mensaje"),Rs("email"))
	Set Obj=nothing
	rs.MoveNext
	loop
	set rs=nothing				

	Response.Write("<script>alert('Se ha informado de los acuerdos de la propuesta via E-mail')</script>")
end if
%>

<%
if Request.QueryString("accion")="postegarPropuesta" then

	Set objPropuesta=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objPropuesta.AbrirConexionTrans
	objPropuesta.Ejecutar "PostergarReunionConsejoPropuesta",false,"UP",Request.QueryString("codigo_prp"),Request.QueryString("codigo_rec")
	objPropuesta.CerrarConexionTrans
	
end if
%>
  <%
  codigo_rec=Request.QueryString("codigo_rec")
  if Request.QueryString("codigo_rec")<>"" then

	 Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	objProp.AbrirConexiontrans
			set propuesta=objProp.Consultar("ConsultarReunionConsejo","FO","PR",codigo_rec,0,0)	
	    	objProp.CerrarConexiontrans
			set objProP=nothing
	%>
<body topmargin="0" leftmargin="0" rightmargin="0">  
  	
</p>
<table width="100%" height="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="76%" rowspan="5" align="left" valign="top"><table width="95%" border="0" align="center" cellpadding="0" cellspacing="1">
      <tr>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
        <td valign="top">&nbsp;</td>
      </tr>
      <tr>
        <td valign="top"><span class="Estilo2"></span></td>
        <td valign="top"><span class="Estilo2"></span></td>
        <td valign="top"><span class="Estilo2"></span></td>
      </tr>
      <tr>
        <td width="16%" valign="top"><span class="Estilo2"><strong>Nombre</strong></span></td>
        <td width="3%" valign="top"><span class="Estilo2"><strong>: </strong></span></td>
        <td width="81%" valign="top"><div align="justify" class="Estilo2">
          <%response.write(propuesta("agenda_rec"))%>
        </div></td>
      </tr>
      <tr>
        <td valign="top"><span class="Estilo2"></span></td>
        <td valign="top"><span class="Estilo2"></span></td>
        <td valign="top"><span class="Estilo2"></span></td>
      </tr>
      <tr>
        <td valign="top"><span class="Estilo2"><strong>Fecha</strong></span></td>
        <td valign="top"><span class="Estilo2"><strong>:</strong></span></td>
        <td valign="top"><span class="Estilo2">
          <%response.write(propuesta("fecha_rec"))%>
        </span></td>
      </tr>
      <tr>
        <td valign="top"><span class="Estilo2"></span></td>
        <td valign="top"><span class="Estilo2"></span></td>
        <td valign="top"><span class="Estilo2"></span></td>
      </tr>
      <tr>
        <td valign="top"><span class="Estilo2"><strong>Lugar</strong></span></td>
        <td valign="top"><span class="Estilo2"><strong>:</strong></span></td>
        <td valign="top"><span class="Estilo2">
          <%response.write(propuesta("lugar_rec"))%>
        </span></td>
      </tr>
      <tr>
        <td valign="top"><span class="Estilo2"></span></td>
        <td valign="top"><span class="Estilo2"></span></td>
        <td valign="top"><span class="Estilo2"></span></td>
      </tr>
      <tr>
        <td colspan="3" valign="top"><span class="Estilo2"><strong> Agenda de la reuni&oacute;n de Consejo Universitario :</strong></span></td>
        </tr>
    </table>    </td>
    <td width="11%" height="29" align="right" valign="middle" class="bordeizq"><img src="../../../../images/menus/attachfiles_small.gif"></td>
    <td width="13%" align="left" valign="middle"><span class="Estilo2"><strong>Acta</strong></span></td>
  </tr>
  <tr>
    <td height="22" colspan="2" align="center" valign="top" class="bordeizqinf">

				    <table width="95%" border="0" align="center" cellpadding="0" cellspacing="2">

                      <tr>
                        <td width="8%" align="center" class="Estilo2">
						<%
						if isnull(propuesta("acta_rec")) or propuesta("acta_rec")="" then
						Response.Write("-")
						else
						%>
						<a href="../../../../filespropuestas/actas/<%=propuesta("acta_rec")%>" target="_blank"> 
						<img src="../../../../images/ext/<%=right(propuesta("acta_rec"),3)%>.gif" width="16" height="16" border="0">						</a>						</td>
                        <td width="92%" align="center">
						<%response.write(propuesta("acta_rec") )
						end if
						%>						</td>
                      </tr>
    </table>    </td>
  </tr>
  <tr>
    <td height="42" align="right" valign="middle" class="bordeizq etiqueta"><img src="../../../../images/menus/kmid.gif" width="30" height="31"></td>
    <td height="42" align="left" valign="middle"><span class="etiqueta Estilo2">&nbsp;Grabaci&oacute;n</span></td>
  </tr>
  <tr>
	<td height="22" colspan="2" align="center" valign="top" class="bordeizqinf Estilo2">&nbsp;
	    <% 
	//Response.write(propuesta("grabacion_rec"))

	if isnull(propuesta("grabacion_rec")) or propuesta("grabacion_rec")="" then
		Response.write("-")
		//Response.write("No se ha registrado grabación")
	else
	%>
		<embed  autostart="false"  src="../../../../filespropuestas/grabaciones/GRAB_REC_<%=codigo_rec%><%=right(propuesta("grabacion_rec"),4)%>"> 
	<%end if%>
	</td>
  </tr>
  <tr>
    <td height="12" colspan="2" align="center" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td height="127" colspan="3" align="left" valign="top">
	<%
	Set Agenda=Server.CreateObject("PryUSAT.clsAccesoDatos")
	Agenda.AbrirConexion
	set RsAgenda=Agenda.Consultar("ConsultarAgendaReunionConsejo","FO","TO",codigo_rec,"")
	Agenda.CerrarConexion
	

	%>
	<table width="95%" border="0" align="center" cellpadding="0" cellspacing="0" class="contornotabla">
      <tr>
        <td colspan="2" align="center" bgcolor="#E1F1FB" class="bordeinf"><span class="Estilo1">Denominaci&oacute;n de la  Propuesta </span></td>
        <td width="92" align="center" bgcolor="#E1F1FB" class="bordeinf">&nbsp;</td>
        <td width="92" align="center" bgcolor="#E1F1FB" class="bordeinf"><span class="Estilo1">Fecha</span></td>
        <td width="0" align="center" bgcolor="#CCCCCC" class="Estilo2 bordeinf Estilo2">&nbsp;</td>
        <td colspan="5" align="center" bgcolor="#E1F1FB" class="Estilo2 bordeinf"><strong>Acciones</strong></td>
        </tr>
      <%do while not RsAgenda.eof
	  	i=i+1
	  %>
	  <tr>
        <td width="17"><span class="Estilo2"><%=i%>.-&nbsp;</span></td>
		<%
		select case RsAgenda("estado_prp")
			case "A" 
			 	estadoActual="Aprobada"
			case "R"
			 	estadoActual="Denegada"
			case "P"
			 	estadoActual="Pendiente"
			case "D"
			 	estadoActual="Derivada"
			case "O"
			 	estadoActual="Observada"
		end select
		
		''Response.Write(RsAgenda("esinforme_prp"))
		%>
        <td width="450" valign="top"><span class="Estilo2"><%=RsAgenda(1)%>. <%=RsAgenda("estado")%></span></td>
        <td align="center"><span class="Estilo3">(<%=estadoActual%>)</span></td>
        <td align="center"><span class="Estilo2"><%=RsAgenda(2)%></span></td>
        <td width="0" align="center" bgcolor="#CCCCCC">&nbsp;</td>
        <td width="23" align="center"><span class="bordesup Estilo2">
        <img onClick="Informar('<%=RsAgenda(0)%>','<%=codigo_rec%>')" src="../../../../images/menus/prioridad_.gif" alt="Informar Acuerdos" style="cursor:hand" >
        <%
					Set objInt=Server.CreateObject("PryUSAT.clsAccesoDatos")
					objInt.AbrirConexiontrans
					set interesado=objInt.Consultar("ConsultarResponsablesPropuesta","FO","PR",RsAgenda(0),0)
					objInt.CerrarConexiontrans
					set objInt=nothing
					if interesado.RecordCount=0 then
						interesado.MoveLast
						codigo_personal=interesado("codigo_per")
						centro_costos=interesado("codigo_cco")
					%>
        <%
					end if
					set interesado=Nothing	
					if RsAgenda("esinforme_prp")="IN"then 
						else 

					%>
        </span></td>
        <td width="23" align="center"><span class="bordesup Estilo2"><img src="../../../../images/menus/Persona_s.gif" <% if RsAgenda("estado_prp")<>"A" then %>style="visibility:hidden" <%end if%> alt="Editar Responsable" width="16" height="16" border="0" style="cursor:hand" onClick="RegistraResponsable('<%=RsAgenda(0)%>','<%=RsAgenda("codigo_per")%>')">			</span>		</td>
       			<% end if %>
	    <td width="21" align="center">
		<span class="bordesup" >
		<img src="../../../../images/menus/todos.gif" alt="Editar Discusiones" border="0" style="cursor:hand"  onClick="RegistraDiscusion('<%=RsAgenda(0)%>','<%=codigo_rec%>')">		
		
		</span> 
		
		</td>
	    <td width="30" align="center">
		<span class="bordesup">
		<img  src="../../../../images/menus/editar.gif"  alt="Editar Acuerdos" border="0" style="cursor:hand" onClick="RegistraAcuerdo('<%=RsAgenda(0)%>','<%=codigo_rec%>')"> </span> </td>
	    <td width="30" align="center">
		<img  style="cursor:hand" src="../../../../images/menus/menu3.gif" alt="Postergar" width="16" height="16"  onClick="postegarPropuesta('<%=RsAgenda(0)%>','<%=codigo_rec%>')" <%if RsAgenda("estado_prp")<>"P" then%> style="visibility:hidden" <%end if%>> </td>
	  </tr>
	  <tr>
	    <td colspan="2"></td>
	    <td></td>
	    <td></td>
	    <td width="0" bgcolor="#CCCCCC"><span class="Estilo2"></span></td>
	    <td></td>
	    <td></td>
	    <td></td>
	    <td></td>
	    <td></td>
	  </tr>
	  <%RsAgenda.MoveNext
	  	loop%>
    </table></td>
  </tr>
</table>

</body>
<%end if%>

</html>
