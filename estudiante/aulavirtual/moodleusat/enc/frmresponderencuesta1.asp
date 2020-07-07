<!--#include file="../../../../NoCache.asp"-->
<%
'if session("codigo_usu")="" then response.redirect "../../../../tiempofinalizado.asp"

dim Mensaje,mostrarScript,IdPregunta

idevaluacion=request.querystring("idevaluacion")
idPregunta=0
accion="GuardarTodo"

function MostrarTiempo(minutos)
	If minutos>0 then
		MostrarTiempo="Onload=""Empezar(" & int(minutos) & ")"" "
	end if
end function

Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
	obj.AbrirConexion
	Set rsEncuesta=obj.Consultar("ConsultarEvaluacion","FO",8,idevaluacion,0,0)
	Set rsPreguntas=obj.Consultar("DI_ConsultarEncuestaParaResponder","FO",idevaluacion)
	obj.CerrarConexion
Set Obj=nothing

If Not(rsEncuesta.BOF and rsEncuesta.EOF) then
	tituloencuesta=rsEncuesta("tituloevaluacion")
	minutos=rsEncuesta("minutos")
	instrucciones="&nbsp;" & rsEncuesta("instrucciones")
	if instrucciones="" then instrucciones="&nbsp;Por favor responda a las siguientes preguntas:"
end if
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Encuesta</title>
<script language="JavaScript">
//------------------Variables para controlar el tiempo de evaluación------------------------
var InicioTiempo = 0;
var TiempoActual=0;
var tEmpezar  = 1; //DEBE INICIAR EN 0 Y SE CAMBIA A 1 CUANDO SE PRESIONE ALGUN BONTON
var TiempoInicial=null
//-------------------------------------------------------------------------------------------

function validarTodaPregunta()
{
var totalRptas=0
var totalPgtas=document.all.cIdPregunta.length

	for(var i=0;i<totalPgtas;i++){
	    var Control=document.getElementById("descripcionrpta" + document.all.cIdPregunta[i].value)
	    
	        /*Validar las cajas de Texto si están vacías*/
	        if(Control.type=="text" || Control.type=="textarea"){					
				if (Control.value==""){
					totalRptas=eval(totalRptas)-1
				}
			    else{
			    	totalRptas=eval(totalRptas)+1
			    }
			}
        	/*Validar los check/option*/
			if(Control.type=="radio" || Control.type=="checkbox"){
			  	totalRptas=totalRptas+VerificarMarca(Control.name)
			}
	}
	
	totalRptas=Math.abs(totalRptas)

  	if(totalRptas<totalPgtas){
		anteriorPgta=""
		alert("Por favor debe responder a todas las preguntas formuladas")
		return(false)
 	}
	else{
	  	frmPreguntas.submit()
	}
}

function VerificarMarca(ctrl)
{
	var estado=0
	var arrOpt=document.all.item(ctrl)
	
	if (arrOpt.length==undefined){
		if (arrOpt.checked==true)
			{estado=1}
	}
	else{
	    for(var i=0;i<arrOpt.length;i++){
			//if (document.all.item(ctrl,i).checked==true){
			if (arrOpt[i].checked==true){
				estado=1
				break
			}
	    }
	}
	
	return(estado)
}

function MostrarMensaje(mensaje){
	if (mensaje!=""){
		alert(mensaje)
	}
}

function ActualizarTiempo(TiempoDuracion){
	var Minutos, Segundos
   	if(tEmpezar){
      	if(TiempoInicial==null)
		{TiempoInicial=new Date()}
		var FechaActual = new Date();
		var DiferenciaFecha = FechaActual.getTime() - TiempoInicial.getTime();
		TiempoActual=(DiferenciaFecha / 1000);
		Minutos=Math.floor(TiempoActual / 60)
		Segundos = Math.floor(TiempoActual % 60)
		txtTiempo.innerHTML=Minutos + " min. " + Segundos + " seg."
		if(Minutos>='<%=minutos%>'){
			alert("Su Tiempo para responder la Encuesta HA FINALIZADO\n Esta ventana se cerrará y se guardarán sólo las preguntas realizadas hasta el momento")
	   		frmPreguntas.submit()
	   	}
		else
			{setTimeout("ActualizarTiempo()",1000)}
	  }
}

function Empezar(TiempoDuracion) {
   tEmpezar   = new Date();
   txtTiempo.innerHTML = "0";
   InicioTiempo  = setTimeout("ActualizarTiempo("+ TiempoDuracion +")", 1000);
}

function PararTiempo()
{
  clearInterval(InicioTiempo)
}

function Deshabilitar()
{
	if (event.ctrlKey) {
		event.ctrlkey=0;
		return false;
	}
	if (event.altKey) {
	event.altKey=0;
	return false;
}

	if(window.event && window.event.keyCode == 116){
		window.event.keyCode = 0;
		return false;
	}
}
document.onkeydown = Deshabilitar;
</script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
<style>
<!--
.Pregunta    { color: #000000; font-size: 10pt; font-weight: bold }
.TituloEncuesta { color: #000080; font-size: 14pt; font-weight: bold }
.Tiempo      { font-size: 10pt; color: #FF0000; font-weight: bold; font-family:Courier New }
.Instrucciones { font-size: 10pt; color: #800000; font-weight: bold; text-transform:uppercase; font-family:Arial Narrow  }
-->
</style>
<link rel="stylesheet" type="text/css" href="../../../../private/estiloaulavirtual.css">
</head>
<body <%=mostrarTiempo(minutos)%> style="background-color: #DEE0C5" oncontextmenu="return false">
<%If Not(rsPreguntas.BOF and rsPreguntas.EOF) then%>
<form name="frmPreguntas" method="post" action="procesarencuesta.asp?accion=<%=accion%>&idEvaluacion=<%=IdEvaluacion%>">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="95%">
  <tr>
    <td width="100%" class="tituloencuesta" colspan="2" height="5%"><%=tituloencuesta%>&nbsp;</td>
  </tr>
  <%if minutos>0 then%>
  <tr>
    <td width="70%" height="5%"><b>Duración: <%=minutos%> min.</b></td>
    <td width="20%" height="5%" align="right" class="tiempo"><b>Transcurrido:</b>&nbsp;<span class="tiempo" id="txtTiempo" style="margin-top: 0">0</span></td>
  </tr>
  <%end if%>
  <tr>
    <td width="70%" height="5%" class="instrucciones"><%=instrucciones%></td>
    <td width="20%" height="5%" align="right"><input type="button" value="   Enviar" name="cmdGuardar" class="guardar" onClick="validarTodaPregunta()">
    </td>
  </tr>
  <tr>
  <td width="90%" height="90%" colspan="2" class="contornotabla" bgcolor="white">
	<DIV id="listadiv" style="height:100%;">
	<table border="0" cellpadding="5" cellspacing="0" style="border-collapse: collapse" width="100%">
	<%Do While Not rsPreguntas.EOF
	i=i+1

	if cdbl(idPregunta)<>cdbl(rsPreguntas("IdPregunta")) then
		idPregunta=rsPreguntas("idPregunta")
		j=j+1
	%>
	<tr><td width="100%" colspan="2">&nbsp;</td></tr>
  	<tr class="pregunta">
    	<td width="3%" valign="top"><b><%=j%>.</b></td>
	    <td width="97%" valign="top"><b><%=rsPreguntas("titulopregunta")%></b>&nbsp;</td>
  	</tr>
  	<%end if%>
  	<tr>
  	<td width="3%" valign="top"></td>
    <td width="97%">
    <%Select case rsPreguntas("idtipopregunta")
    	case 1%><input type="text" name="descripcionrpta<%=rsPreguntas("idPregunta")%>" size="80" value="<%=rsPreguntas("valorpredeterminado")%>" class="Cajas" idPre="Pregunta<%=rsPreguntas("idPregunta")%>" >
    	<%case 5%><textarea rows="4" name="descripcionrpta<%=rsPreguntas("idPregunta")%>" cols="80" class="Cajas" idPre="Pregunta<%=rsPreguntas("idPregunta")%>"><%=rsPreguntas("valorpredeterminado")%></textarea>
    	<%case else%>
        		<%if rsPreguntas("idtipopregunta")=2 or rsPreguntas("idtipopregunta")=3 then%>
        		<input type="radio" name="descripcionrpta<%=rsPreguntas("idPregunta")%>" value="<%=rsPreguntas("idalternativa")%>" idPre="Pregunta<%=rsPreguntas("idPregunta")%>" onClick="MostrarMensaje('<%=rsPreguntas("mensaje")%>')">
        		<%end if
        		if rsPreguntas("idtipopregunta")=6 then%>
        		<input type="checkbox" name="descripcionrpta<%=rsPreguntas("idPregunta")%>" value="<%=rsPreguntas("idalternativa")%>" idPre="Pregunta<%=rsPreguntas("idPregunta")%>" onClick="MostrarMensaje('<%=rsPreguntas("mensaje")%>')">
        		<%end if%>
				&nbsp;<%=rsPreguntas("tituloalternativa")%>
    <%End select%>
    <input type="hidden" name="cIdTipoPregunta" value="<%=rsPreguntas("idtipopregunta")%>">
    <input type="hidden" name="cIdPregunta" value="<%=rsPreguntas("idPregunta")%>">
    </td>
  	</tr>
  	<%if rsPreguntas("URL")<>"" then%>
  	<tr>
    	<td width="100%" colspan="2"><b>Referencias:</b><br><br><img border="0" src="<%=rsPreguntas("URL")%>">
	</td>
  	</tr>
  	<%end if

	rsPreguntas.movenext
	Loop
	%>
	</table>
	</div>
   </td>
   </tr>
</table>
</form>
<%else%>
	<h5>No se han registrado Preguntas para esta Evaluación</h5>
<%End if
Set rsPreguntas=nothing
%>
</body>
</html>