<!--#include file="clsevaluacion.asp"-->
<%

Accion=request.querystring("accion")

idtipopregunta=Request.querystring("idtipopregunta")
idevaluacion=Request.querystring("idevaluacion")
IdPregunta=Request.querystring("IdPregunta")
idtipopregunta=IIF(idtipopregunta="",5,idtipopregunta)
totalpreg=request.querystring("totalpreg")
if totalpreg="" then totalpreg=0
totalpreg=int(totalpreg)+1

	if Accion="modificarpregunta" then
		set evaluacion=new clsevaluacion
		with evaluacion
			.restringir=session("idcursovirtual")
			Arrdatos=.Consultar("5",idpregunta,"","")
		end with
		set evaluacion=nothing
		
		idtipopregunta=Arrdatos(2,0)
		ordenpregunta=Arrdatos(4,0)
		titulopregunta=Arrdatos(5,0)
		URL=Arrdatos(7,0)		
		pjebueno=Arrdatos(8,0)
		pjemalo=Arrdatos(9,0)
		pjeblanco=Arrdatos(10,0)
		obligatoria=Arrdatos(11,0)
		duracion=Arrdatos(12,0)
		valorpredeterminado=ArrDatos(13,0)
		cambiopregunta="onChange=""seleccionarTipopregunta('" & idtipopregunta & "')"""
	else
		OrdenPregunta=totalpreg
		tipoPregBase=0
		cambiopregunta="onChange=""cambiartipopregunta('" & accion & "','" & idevaluacion & "','" & idpregunta & "')"""
	end if
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>PASO 2: Registrar preguntas</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarevaluacion.js"></script>
</head>
<body topmargin="0" leftmargin="0">
<form name="frmPregunta" method="POST" onSubmit="return validarpregunta(this)" action="procesar.asp?Accion=<%=accion%>&idevaluacion=<%=idevaluacion%>&idPregunta=<%=idpregunta%>&idtipopregunta=<%=idtipopregunta%>">
<table cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" id="tblbotones" class="barraherramientas">
  	<tr><td>
	<input type="submit" value=" Guardar" class="guardar3" name="cmdGuardar">
	<input onClick="location.href='listapreguntas.asp?idevaluacion=<%=idevaluacion%>'" type="button" value="   Cancelar" name="cmdCancelar" class="cerrar3">
	<span id="mensaje" style="color:#FF0000"></span>
	</td></tr>
</table>
<center>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="574">
  <tr>
    <td width="56"  height="3%" class="etiqueta">Tipo:</td>
    <td width="150"  height="3%"><%call escribirlista("idtipopregunta","",cambiopregunta,idtipopregunta,"clsevaluacion","10","evaluacion","","")%>
	</td>
    <td width="123"  height="3%" align="right" class="etiqueta">Orden</td>
    <td width="50"  height="3%">
        <input type="text" onkeypress="validarnumero()" name="ordenpregunta" size="2" class="Cajas" value="<%=ordenpregunta%>" style="width: 20"></td>
    <td width="116"  height="3%" class="etiqueta">Obligatoria</td>
    <td width="75"  height="3%">
	      <input type="checkbox" name="obligatoria" value="1" checked></td>
  </tr>
  <tr>
    <td width="568"  height="3%" colspan="6" class="etiqueta">Enunciado de la Pregunta </td>
  </tr>
  <tr>
    <td width="568" colspan="6" height="5%" valign="top">
    <textarea rows="5" name="titulopregunta" cols="76" class="Cajas" style="height: 50"><%=titulopregunta%></textarea></td>
  </tr>
  <%if idtipopregunta=5 then%>
  <tr>
    <td width="568" colspan="6" class="etiqueta" height="3%" >Valor predeterminado</td>
  </tr>
  <tr>
    <td width="568" colspan="6" height="5%" valign="top">
    <textarea rows="5" name="valorpredeterminado" cols="76" class="Cajas" style="height: 50"><%=valorpredeterminado%></textarea></td>
  </tr>
  <tr>
    <td width="568" colspan="6" height="8%">
    <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="562">
  <tr>
    <td width="249"  height="20" class="etiqueta">Puntajes de Respuestas:&nbsp;
    Correctas</td>
    <td width="50"  height="20">
        <input type="text" onkeypress="validarnumero()" name="pjebueno" size="2" class="Cajas" value="0" style="width: 20"></td>
    <td width="82"  height="20" align="right" class="etiqueta">Incorrectas</td>
    <td width="20"  height="20">
        <input type="text" onkeypress="validarnumero()" name="pjemalo" size="2" class="Cajas" value="0" style="width: 20"></td>
    <td width="105"  height="20" class="etiqueta">Nulas (Blanco)</td>
    <td width="20"  height="20">
        <input type="text" onkeypress="validarnumero()" name="pjeblanco" size="2" class="Cajas" value="0" style="width: 20"></td>
  </tr>
  <tr>
    <td width="249"  height="20" class="etiqueta">Duración de la pregunta</td>
    <td width="50"  height="20">
        <input type="text" onkeypress="validarnumero()" name="duracion" size="2" class="Cajas" value="0" style="width: 20"> min</td>
    <td width="82"  height="20" align="right" class="etiqueta">&nbsp;</td>
    <td width="20"  height="20">
        &nbsp;</td>
    <td width="105"  height="20" class="etiqueta">Incluir imágenes</td>
    <td width="20"  height="20"><input type="checkbox" name="incluirimagen" value="1"></td>
  </tr>
  </table>
  </td></tr>
  <%end if%>
  </table>
  <%If accion="modificarpregunta" then
	  Select case idtipopregunta
			case 2,3,6,7%>
	        <iframe name="fraAlternativas" src="frmalternativa.asp?idPregunta=<%=idPregunta%>&idtipopregunta=<%=idtipopregunta%>" border="0" frameborder="0" style="width: 95%" height="100%">
El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe>
  	 <%end select
  end if%>
</center>
</form>
</body>
</html>