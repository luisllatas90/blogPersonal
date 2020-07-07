<!--#include file="../../../../funcionesaulavirtual.asp"-->
<%
'if session("codigo_usu")="" then response.redirect "../../../../tiempofinalizado.asp"
idevaluacion=request.querystring("idevaluacion")

dim Mensaje,mostrarScript,IdPregunta
	
	function MostrarTiempo()
		If session("minutos")>0 then
			MostrarTiempo="Onload=""Empezar(" & int(session("minutos")) & ")"" "
		end if
	end function
	'oncontextmenu="return false"
	'OnUnload=AbrirEvaluacion("T")

'---------------------------------------------------------
'Consultar Preguntas
'---------------------------------------------------------
idPregunta=0

Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
	obj.AbrirConexion
	Set rsPreguntas=obj.Consultar("DI_ConsultarEncuestaParaResponder","FO",idevaluacion)
	obj.CerrarConexion
Set Obj=nothing
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Encuesta</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript">
function validarTodaPregunta(formulario)
{
var totalRptas=0
var totalPgtas=document.all.cIdPregunta.length

	for(var i=0;i<totalPgtas;i++){
	    var Control=document.getElementById("descripcionrpta" + formulario.cIdPregunta[i].value)
	    
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
	  	return(true)
	}
}
</script>
<base target="preguntasEvaluacion">
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
<style>
<!--
.Pregunta    { color: #8A2106; font-size: 13pt; font-weight: bold }
-->
</style>
</head>
<body  <%=mostrarTiempo%> topmargin="0" leftmargin="0" class="menusuperior">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="15%">
  <tr>
    <td width="20%" rowspan="3"><img src="../../../../images/logo.jpg"></td>
    <td width="100%" class="e1" colspan="2"><%=session("tipoeval")%>: <%=session("tituloeval")%>&nbsp;</td>
  </tr>
  <tr>
    <td width="60%"><%=session("descripcioneval")%></td>
    <%if session("minutos")>0 then%>
    <td width="20%" class="tiempo"><b>Duración: <%=iif(session("minutos")=0,"",session("minutos"))%> min.</b></td>
    <%end if%>
  </tr>
  <tr>
    <td width="60%"><%=session("instrucciones")%>&nbsp;</td>
    <%if session("minutos")>0 then%>
    <td width="20%" ><b>Transcurrido:</b>&nbsp;<span class="tiempo" id="txtTiempo" style="margin-top: 0">0</span>
    </td>
    <%end if%>
  </tr>
</table>
<%If Not(rsPreguntas.BOF and rsPreguntas.EOF) then%>
<form name="frmPreguntas" method="post" onSubmit="return validarTodaPregunta(this)" action="procesarencuesta.asp?accion=<%=accion%>">
<table class="contornotabla" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="80%">
      <tr class="encabezadopregunta">
        <td width="40%" height="10%" class="e1">Por favor responda a las siguientes preguntas:</td>
        <td width="10%" align="right" height="10%">
        <input type="submit" value="   Guardar Todo" name="cmdGuardar" class="guardar" style="width: 110"></td>
      </tr>
<tr>
<td width="90%" height="90%" colspan="2">
<div id="listadiv" style="height:100%" class="NoImprimir">
<table border="0" cellpadding="5" cellspacing="0" style="border-collapse: collapse" width="100%">
<%Do While Not rsPreguntas.EOF
	i=i+1

	if cdbl(idPregunta)<>cdbl(rsPreguntas("IdPregunta")) then
		idPregunta=rsPreguntas("idPregunta")
		j=j+1
	%>
  	<tr class="pregunta">
    	<td width="3%" valign="top"><b><%=j%>.</b></td>
	    <td width="97%" valign="top"><b><%=rsPreguntas("titulopregunta")%></b>&nbsp;</td>
  	</tr>
  	<%end if%>
  	<tr>
    <td width="100%" colspan="2">
    <%Select case rsPreguntas("idtipopregunta")
    	case 1%><input type="text" name="descripcionrpta<%=rsPreguntas("idPregunta")%>" size="80" value="<%=rsPreguntas("descripcionrpta")%>" class="Cajas" idPre="Pregunta<%=rsPreguntas("idPregunta")%>" >
    	<%case 5%><textarea rows="4" name="descripcionrpta<%=rsPreguntas("idPregunta")%>" cols="80" class="Cajas" idPre="Pregunta<%=rsPreguntas("idPregunta")%>" ><%=descripcionrpta%></textarea>
    	<%case else%>
    	<table border="0" cellpadding="2" cellspacing="0" style="border-width:0; border-collapse: collapse" bordercolor="#EBEBEB" width="80%">
    	<tr>
    	    <td width="5%" align="center">
        		<%if rsPreguntas("idtipopregunta")=2 or rsPreguntas("idtipopregunta")=3 then%>
        		<input type="radio" name="descripcionrpta<%=rsPreguntas("idPregunta")%>" value="<%=rsPreguntas("idalternativa")%>" idPre="Pregunta<%=rsPreguntas("idPregunta")%>">
        		<%end if
        		if rsPreguntas("idtipopregunta")=6 then%>
        			<input type="checkbox" name="descripcionrpta<%=rsPreguntas("idPregunta")%>" value="<%=rsPreguntas("idalternativa")%>" idPre="Pregunta<%=rsPreguntas("idPregunta")%>">
        		<%end if%>
        	</td>
	        <td width="80%"><%=rsPreguntas("tituloalternativa")%></td>
    	    <td><span id="mensaje<%=rsPreguntas("idalternativa")%>"><%=rsPreguntas("mensaje")%></span></td>
	      	</tr>
		</table>
    <%End select%>
    <input type="hidden" name="cIdTipoPregunta" value="<%=rsPreguntas("idtipopregunta")%>">
    <input type="hidden" name="cIdPregunta" value="<%=rsPreguntas("idPregunta")%>">
    </td>
  </tr>
  <%if rsPreguntas("URL")<>"" then%>
  <tr>
    <td width="100%" colspan="2">
    <b>Referencias:</b><br><br><img border="0" src="../../images/<%=rsPreguntas(7,i)%>">
	</td>
  </tr>
  <%end if%>
  <tr><td width="100%" colspan="2">&nbsp;</td></tr>
	<%
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