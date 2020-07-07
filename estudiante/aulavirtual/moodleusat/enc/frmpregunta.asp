<!--#include file="../../../../funcionesaulavirtual.asp"-->
<%
Accion=request.querystring("accion")
idtipopregunta=Request.querystring("idtipopregunta")
idevaluacion=Request.querystring("idevaluacion")
IdPregunta=Request.querystring("IdPregunta")
codigo_ccv=request.querystring("codigo_ccv")
totalAlt=0
	if Accion="modificarpregunta" then
		Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
			Set rsDatos=obj.Consultar("ConsultarEvaluacion","FO",5,idPregunta,0,0)
			Set rsAlternativas=obj.Consultar("ConsultarEvaluacion","FO",7,idPregunta,0,0)			
		obj.CerrarConexion
		Set Obj=nothing
				
		idtipopregunta=rsDatos("idtipopregunta")
		ordenpregunta=rsDatos("ordenpregunta")
		titulopregunta=rsDatos("titulopregunta")
		URL=rsDatos("url")
		pjebueno=rsDatos("pjebueno")
		pjemalo=rsDatos("pjemalo")
		pjeblanco=rsDatos("pjeblanco")
		obligatoria=rsDatos("obligatoria")
		duracion=rsDatos("duracion")
		valorpredeterminado=rsDatos("valorpredeterminado")
		cambiopregunta="onChange=""seleccionarTipopregunta('" & idtipopregunta & "')"""
		
		if Not(rsAlternativas.BOF and rsAlternativas.EOF) then
			HayAlt=true
			totalAlt=rsAlternativas.recordcount
		end if
	end if
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>PASO 2: Registrar preguntas</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estiloaulavirtual.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funcionesaulavirtual.js"></script>
<script type="text/javascript" language="JavaScript">
function AccionPregunta(modo,id)
{
	if (modo=="AA" || modo=="GP"){
		if (frmPregunta.ordenpregunta.value == ""){
			alert("Ingrese el orden que tiene la pregunta.");
	    	frmPregunta.ordenpregunta.focus();
			return (false)
		}
	
		if (frmPregunta.titulopregunta.value == ""){
			alert("Escriba el enunciado de la pregunta.");
		    frmPregunta.titulopregunta.focus();
			return (false)
		}
		
		if (modo=="AA"){
			showModalDialog("frmalternativa.asp?codigo_ccv=<%=codigo_ccv%>&orden=<%=totalAlt+1%>",window,"dialogWidth:400px;dialogHeight:250px;status:no;help:no;center:yes")
		}
		if (modo=="GP"){
			frmPregunta.action="procesar.asp?Accion=<%=accion%>&idevaluacion=<%=idevaluacion%>&idPregunta=<%=idpregunta%>&idtipopregunta=<%=idtipopregunta%>&codigo_ccv=<%=codigo_ccv%>"
   			frmPregunta.submit()
		}	
	}
	
	if (modo=="MA"){
		showModalDialog("frmalternativa.asp?idalternativa=" + id,window,"dialogWidth:400px;dialogHeight:350px;status:no;help:no;center:yes")
	}
	
	if (modo=="EA"){
		if (confirm("Acción irreversible.\n ¿Está seguro completamente seguro que desea Eliminar la alternativa?")==true){
			frmPregunta.action="procesar.asp?Accion=eliminaralternativa&idalternativa=" + id + "&idevaluacion=<%=idevaluacion%>&idPregunta=<%=idpregunta%>&idtipopregunta=<%=idtipopregunta%>&codigo_ccv=<%=codigo_ccv%>"
   			frmPregunta.submit()
		}	
	}	
}

function EnviarPreguntaAlternativa()
{
	var alternativa="&ordenalt=" + ordenalt + "&tituloalt=" + tituloalt + "&mensajealt=" + mensajealt + "&correctoalt=" + correctoalt + "&codigo_ccv=" + codigo_ccv
	
	frmPregunta.action="procesar.asp?Accion=<%=accion%>&idevaluacion=<%=idevaluacion%>&idPregunta=<%=idpregunta%>&idtipopregunta=<%=idtipopregunta%>" + alternativa
	frmPregunta.submit()
}
</script>
</head>
<body onload="frmPregunta.ordenpregunta.focus()">
<p class="e4">Pregunta</p>

<form name="frmPregunta" method="POST">
<span class="azul"><b>Orden / Enunciado</b></span>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="3%" valign="top">
        <input type="text" onkeypress="validarnumero()" name="ordenpregunta" size="2" value="<%=ordenpregunta%>" style="width: 20"></td>
    <td width="97%">
    <textarea rows="5" name="titulopregunta" cols="76" class="Cajas"><%=titulopregunta%></textarea><br>
		Pregunta obligatoria: <input type="checkbox" name="obligatoria" value="1" checked></td>
  </tr>
  <%if idtipopregunta=5 then%>
  <tr>
    <td width="3%" valign="top">
        &nbsp;</td>
    <td width="97%">
    <b>predeterminado del enunciado</b></td>
  </tr>
  <tr>
    <td width="3%" valign="top">
        &nbsp;</td>
    <td width="97%">
    <textarea rows="5" name="valorpredeterminado" cols="76" class="Cajas"><%=valorpredeterminado%></textarea></td>
  </tr>
  <%end if
  
  if (idtipopregunta=2 or idtipopregunta=3 or idtipopregunta=6 or idtipopregunta=7) then%>
  <tr>
    <td width="3%" valign="top">&nbsp;</td>
    <td width="97%">
  	<input onClick="AccionPregunta('AA')" type="button" value="    Añadir alternativa" style="width:120px" name="cmdAgregar" class="agregar2"></td>
  </tr>
  <%if HayAlt=true then%>  
  <tr>
    <td width="3%" valign="top">&nbsp;</td>
    <td width="97%">
    <table width="95%" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080">
  	<tr class="etabla2"> 
      <td width="5%">Eliminar</td>
      <td width="5%">Correcta</td>
      <td width="5%">Orden</td>
      <td width="40%">Alternativa</td>
      <td width="30%">Mensaje de respuesta incorrecta</td>
  	</tr>
	<%Do while Not rsAlternativas.EOF%>
  	<tr> 
      <td width="5%" align="center">
	  <img alt="Eliminar" src="../../../../images/eliminar.gif" border="0" width="14" height="13" class="imagen" onclick="AccionPregunta('EA','<%=rsAlternativas("idalternativa")%>')"></td>
      <td width="5%" align="center">
      <%=iif(rsAlternativas("rptaCorrecta")=1,"Sí","No")%>&nbsp;</td>
      <td width="5%" align="center"><%=rsAlternativas("orden")%>&nbsp;</td>
      <td width="40%"><%=rsAlternativas("tituloalternativa")%>&nbsp;</td>
      <td width="30%"><%=rsAlternativas("mensaje")%>&nbsp;</td>
  	</tr>
  	<%
  		rsAlternativas.movenext
  		Loop
  	%>
  	</table>
    </td>
  </tr>
  <%end if
  
  end if%>
  </table>
  <p align="center">
    <%if accion="agregarpregunta" or accion="modificarpregunta" then
    	if (idtipopregunta=5 or HayAlt=true) then%>
	  	<input type="button" value=" Guardar" class="guardar" name="cmdGuardar" onClick="AccionPregunta('GP')">
    	<%end if
    end if%>
	<input onClick="location.href='frmencuesta.asp?accion=modificarevaluacion&idevaluacion=<%=idevaluacion%>&codigo_ccv=<%=codigo_ccv%>'" type="button" value="   Cancelar" name="cmdCancelar" class="cerrar">
	<span id="mensaje" style="color:#FF0000"></span>
  </p>
</form>
</body>
</html>