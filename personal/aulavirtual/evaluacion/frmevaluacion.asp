<!--#include file="clsevaluacion.asp"-->
<%
Accion=Request.querystring("Accion")
IdEvaluacion=Request.querystring("IdEvaluacion")
numfila=request.querystring("numfila")

if Accion="modificarevaluacion" then
	set evaluacion=new clsevaluacion
	with evaluacion
		.restringir=session("idcursovirtual")
		Arrevaluacion=.Consultar("2",idevaluacion,"","")
	end with
	Set evaluacion=nothing	
		idCategoria=Arrevaluacion(1,0)
		tituloevaluacion=Arrevaluacion(2,0)
		fechainicio=Arrevaluacion(3,0)
		fechafin=Arrevaluacion(4,0)
		Procesarfechas Fechainicio,FechaFin		
		descripcion=Arrevaluacion(5,0)
		instrucciones=Arrevaluacion(6,0)
		idcreador=Arrevaluacion(8,0)
		enlinea=Arrevaluacion(9,0)
		mostrarresultados=Arrevaluacion(10,0)
		incluirimagenes=Arrevaluacion(11,0)
		modificarrespuesta=Arrevaluacion(12,0)
		preguntaporpregunta=Arrevaluacion(13,0)
		retrocederpaginas=Arrevaluacion(14,0)
		respuestacorrecta=Arrevaluacion(15,0)
		limiteaccesos=Arrevaluacion(16,0)
		minutos=Arrevaluacion(17,0)		
		idtipopublic=Arrevaluacion(20,0)
		idestadorecurso=Arrevaluacion(21,0)
		nombrecategoria=Arrevaluacion(22,0)
else
	enlinea=1
	mostrarresultados=1
	incluirimagenes=1
	modificarrespuesta=1
	preguntaporpregunta=1
	procesarfechas now,session("fincursovirtual")
end if	
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>PASO 1: Registro de datos de la encuesta</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/calendario.js"></script>
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarevaluacion.js"></script>
</head>
<body topmargin="0" leftmargin="0">
<form name="frmevaluacion" method="POST" onSubmit="return validarevaluacion(this)" action="procesar.asp?Accion=<%=accion%>&idevaluacion=<%=idevaluacion%>">
<%BotonesAccion%>
<center>
<table width="98%" style="border-collapse: collapse" bordercolor="#111111" cellpadding="3" cellspacing="0">
  <tr>
    <td width="10%" class="etiqueta">Tipo*</td>
    <td  width="90%"><%call escribirlista("idcategoria","","",idCategoria,"clscategoria","1","evaluacion","","")%>
    </select></td>
  </tr>
  <tr>
    <td width="10%" class="etiqueta">Título *</td>
    <td  width="90%">
    <input  maxLength="100" size="82" name="tituloevaluacion" class="Cajas" value="<%=tituloevaluacion%>"></td>
  </tr>
  <tr>
    <td width="10%" valign="top" class="etiqueta">Descripción*</td>
    <td  width="90%">
    <textarea  name="descripcion" rows="3" cols="81" class="Cajas"><%=descripcion%></textarea></td>
  </tr>
  <tr>
    <td width="10%" valign="top" class="etiqueta">Instrucciones</td>
    <td  width="90%">
    <textarea  name="instrucciones" rows="3" cols="81" class="Cajas"><%=instrucciones%></textarea></td>
  </tr>
  <tr>
    <td width="100%" valign="top" class="etiqueta" colspan="2"><%BarraProgramacionFechas%>&nbsp;</td>
  </tr>
  <tr>
    <td width="683" valign="top" colspan="2" class="etiqueta">Seleccione las opciones que desee:</td>
  </tr>
  <tr>
    <td width="683" colspan="2">
    <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#EBEBEB" width="100%" id="AutoNumber1">
      <tr>
    <td width="21" valign="top">
    <input type="checkbox" name="enlinea" value="1" <%=SeleccionarItem("chk",enlinea,1)%>></td>
    <td  width="288" valign="top">
    <p>Mostrar la calificación en línea al final de la evaluación</td>
    <td  width="242" valign="top">Permitir al evaluado modificar su respuesta</td>
    <td width="22" valign="top" align="right">
    <input type="checkbox" name="modificarrespuesta" value="1" <%=SeleccionarItem("chk",modificarrespuesta,1)%>></td>
      </tr>
      <tr>
    <td width="21" valign="top">
    <input type="checkbox" name="preguntaporpregunta" value="1" <%=SeleccionarItem("chk",preguntaporpregunta,1)%>></td>
    <td  width="288" valign="top">Mostrar la evaluación 
    pregunta por pregunta<span style="font-weight: 400"> </span></td>
    <td  width="242" valign="top">Permitir al evaluado retroceder las páginas</td>
    <td width="22" valign="top" align="right">
    <input type="checkbox" name="retrocederpaginas" value="1" <%=SeleccionarItem("chk",retrocederpaginas,1)%>></td>
      </tr>
      <tr>
    <td width="21" valign="top">
    <input type="checkbox" name="mostrarresultados" value="1" <%=SeleccionarItem("chk",mostrarresultados,1)%>></td>
    <td  width="288" valign="top">Mostrar las respuestas realizadas en la 
    evaluación</td>
    <td  width="242" valign="top">Incluir imágenes en la misma pregunta</td>
    <td width="22" valign="top" align="right">
    <input type="checkbox" name="incluirimagenes" value="1" <%=SeleccionarItem("chk",incluirimagenes,1)%>></td>
      </tr>
      <tr>
    <td width="21" valign="top">
    <input type="checkbox" name="respuestacorrecta" value="1" <%=SeleccionarItem("chk",respuestacorrecta,1)%>></td>
    <td  width="288" valign="top">Mostrar al evaluado la respuesta correcta</td>
    <td  width="242" valign="top">
    <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="fondonulo">
      <tr>
        <td width="49%" align="right">Límite de duración</td>
        <td width="51%"><input type="text" onkeypress="validarnumero()" Onchange="validartiempoevaluacion()" name="minutos" size="4" class="cajas2" value="<%=iif(accion="modificarevaluacion",minutos,0)%>"> 
        minutos</td>
      </tr>
      <tr>
        <td width="49%" align="right">Límite de accesos:</td>
        <td width="51%">
    <input type="text" onkeypress="validarnumero()" name="vecesacceso" size="4" class="cajas2" value="<%=iif(accion="modificarevaluacion",limiteaccesos,1)%>" readonly> 
        veces</td>
      </tr>
    </table>
    </td>
    <td width="22" valign="top" align="right">&nbsp;</td>
      </tr>
      </table>
    </td>
  </tr>
</table>
</center>
</form>
</body>
</html>