<!--#include file="../../../../funcionesaulavirtual.asp"-->
<%
Accion=Request.querystring("Accion")
IdEvaluacion=Request.querystring("IdEvaluacion")
codigo_ccv=Request.querystring("refcodigo_ccv")
enlinea=1
mostrarresultados=0
modificarrespuesta=0
procesarfechas now,session("fincursovirtual")
HayPreg=false
if idevaluacion="" or (cdbl(idevaluacion)=cdbl(codigo_ccv)) then idevaluacion=0

	Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
		Set rsTipoPregunta=obj.Consultar("ConsultarEvaluacion","FO",10,idevaluacion,0,0)
		Set rsPreguntas=obj.Consultar("ConsultarEvaluacion","FO",3,idevaluacion,0,0)				
		
		if Accion="modificarevaluacion" then
			Set rsEvaluacion=obj.Consultar("ConsultarEvaluacion","FO",2,idevaluacion,0,0)	
		end if
		obj.CerrarConexion
	Set Obj=nothing

if Accion="modificarevaluacion" then
	tituloevaluacion=rsEvaluacion("tituloevaluacion")
	fechainicio=rsEvaluacion("fechainicio")
	fechafin=rsEvaluacion("fechafin")
	Procesarfechas Fechainicio,FechaFin		
	instrucciones=rsEvaluacion("instrucciones")
	idcreador=rsEvaluacion("idusuario")
	enlinea=rsEvaluacion("enlinea")
	mostrarresultados=rsEvaluacion("mostrarresultados")
	incluirimagenes=rsEvaluacion("incluirimagenes")
	modificarrespuesta=rsEvaluacion("modificarrespuesta")
	preguntaporpregunta=rsEvaluacion("preguntaporpregunta")
	retrocederpaginas=rsEvaluacion("retrocederpaginas")
	respuestacorrecta=rsEvaluacion("respuestacorrecta")
	limiteaccesos=rsEvaluacion("vecesacceso")
	minutos=rsEvaluacion("minutos")
	idtipopublic=rsEvaluacion("idtipopublicacion")
	idestadorecurso=rsEvaluacion("idestadorecurso")
end if

if Not(rsPreguntas.BOF and rsPreguntas.EOF) then
	HayPreg=true
end if
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registro de datos de la encuesta</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../../private/calendario.js"></script>
<script language="JavaScript" src="../../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript">
//Validar datos de ingreso de la evaluación
function validarevaluacion()
{
  var estado=true

  if (frmencuesta.tituloevaluacion.value == "")
  {
   alert("Ingrese el título de la evaluación.");
    frmencuesta.tituloevaluacion.focus();
    return (false);
  }

  if (frmencuesta.instrucciones.value == "")
  {
    alert("Ingrese las instrucciones que deben de tomar en cuenta los que serán evaluados.");
    frmencuesta.instrucciones.focus();
    return (false);
  }

  frmencuesta.cmdGuardar.disabled=false
  frmencuesta.cmdCancelar.disabled=false
  frmencuesta.action="procesar.asp?Accion=<%=accion%>&idevaluacion=<%=idevaluacion%>&codigo_ccv=<%=codigo_ccv%>"
  frmencuesta.submit()
}

function EliminarPregunta(idpreg)
{
	var pagina="procesar.asp?accion=eliminarpregunta&idpregunta=" + idpreg + "&idevaluacion=<%=idevaluacion%>&codigo_ccv=<%=codigo_ccv%>"
	var mensaje="Acción irreversible.\n ¿Está seguro completamente seguro que desea Eliminar la pregunta?"
	Eliminar(mensaje,pagina)
}

function AgregarPreguntas(obj)
{
	if (obj.value!=""){
		frmencuesta.action="procesar.asp?modo=P&Accion=<%=accion%>&idevaluacion=<%=idevaluacion%>&codigo_ccv=<%=codigo_ccv%>&idtipopregunta=" + obj.value
		obj.value=""		
		frmencuesta.submit()
	}
}
</script>
</head>
<body onLoad="frmencuesta.tituloevaluacion.focus()">
<form name="frmencuesta" method="POST">
<p class="e4">Registro de encuestas en línea</p>
<table class="contornotabla" width="100%" style="border-collapse: collapse" bordercolor="#111111" cellpadding="3" cellspacing="0" bgcolor="#FFFFFF">
  <tr>
    <td width="10%" class="etiqueta">Título</td>
    <td  width="90%">
    <input  maxLength="100" size="82" name="tituloevaluacion" class="Cajas" value="<%=tituloevaluacion%>"></td>
  </tr>
  <tr>
    <td width="10%" valign="top" class="etiqueta">&nbsp;</td>
    <td  width="90%">   		
<table width="100%" style="border-collapse: collapse" bordercolor="#111111" cellpadding="2" cellspacing="0">
  <tr style="zindex:-1">
    <td width="60" class="etiqueta">Publicado</td>
    <td  width="50">Desde</td>
    <td colspan="5">
    <select  name="horainicio" onChange="VerificarTurno('I')">
    <%for i=1 to 12%>
		<option value="<%=i%>" <%=seleccionar(horai,i)%>><%=i%></option>
	<%next%>
    </select> <select  name="mininicio">
    <%for i=0 to 45 step 15%>
		<option value="<%=iif(len(i)=1,"0" & i,i)%>" <%=seleccionar(mini,i)%>><%=iif(len(i)=1,"0" & i,i)%></option>
	<%next%>
    </select><select  name="turnoinicio">
    <option value="am" <%=seleccionar(turnoi,"am")%>>a.m</option>
    <option value="pm" <%=seleccionar(turnoi,"pm")%>>p.m</option>
    </select><input type="text" name="fechainicio" size="12" class="Cajas2" readonly value="<%=fechainicio%>"><input type="button" class="cunia" onClick="MostrarCalendario('fechainicio')"></td>
  </tr>
  <tr>
    <td width="60">&nbsp;</td>
    <td  width="50">Hasta</td>
    <td colspan="5">
    <select  name="horafin" onChange="VerificarTurno('F')">
    <%for i=1 to 12%>
		<option value="<%=i%>" <%=seleccionar(horaf,i)%>><%=i%></option>
	<%next%>
    </select> <select  name="minfin">
    <%for i=0 to 45 step 15%>
		<option value="<%=iif(len(i)=1,"0" & i,i)%>" <%=seleccionar(minf,i)%>><%=iif(len(i)=1,"0" & i,i)%></option>
	<%next%>
    </select><select  name="turnofin">
    <option value="am" <%=seleccionar(turnof,"am")%>>a.m</option>
    <option value="pm" <%=seleccionar(turnof,"pm")%>>p.m</option>
    </select><input type="text" name="fechafin" size="12" class="Cajas2" readonly value="<%=fechafin%>"><input type="button" class="cunia" onClick="MostrarCalendario('fechafin')">
    </td>
  </tr>
</table>
    </td>
  </tr>
  <tr>
    <td width="10%" valign="top" class="etiqueta">Instrucciones</td>
    <td  width="90%">
    <textarea  name="instrucciones" rows="3" cols="81" class="Cajas"><%=instrucciones%></textarea></td>
  </tr>
  <tr>
    <td width="100%" valign="top" colspan="2" class="etiqueta">&nbsp;</td>
  </tr>
  <tr>
    <td width="100%" valign="top" colspan="2" class="boton">&nbsp;Opciones de 
    configuración:</td>
  </tr>
  <tr>
    <td width="100%" colspan="2">
    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#EBEBEB" width="90%" id="AutoNumber1">
      <tr>
    <td width="47" valign="top">
    <input type="checkbox" name="respuestacorrecta" value="1" <%=SeleccionarItem("chk",respuestacorrecta,1)%>></td>
    <td  width="283" valign="top">Mostrar al evaluado la respuesta correcta</td>
    <td width="20" valign="top">
    <input type="text" onkeypress="validarnumero()" Onchange="validartiempoevaluacion()" name="minutos" size="4" class="cajas2" value="<%=iif(accion="modificarevaluacion",minutos,0)%>"></td>
    <td  width="342" valign="top">minutos de duración de la encuesta</td>
      </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td width="100%" colspan="2">
    &nbsp;</td>
  </tr>
  <tr>
    <td width="100%" colspan="2" class="boton">&nbsp;Preguntas de la encuesta:&nbsp;&nbsp;&nbsp;
    <select size="1" name="cbopregunta" onChange="AgregarPreguntas(this)">
    <%If Not(rsTipoPregunta.BOF and rsTipoPregunta.EOF) then%>
    <option value="">Añadir preguntas...</option>
    <%Do While Not rsTipoPregunta.EOF%>
    <option value="<%=rsTipoPregunta("idtipopregunta")%>">&nbsp;-&nbsp;<%=rsTipoPregunta("nombretipo")%></option>
    <%	rsTipoPregunta.movenext
    Loop
    end if
    Set rsTipoPregunta=nothing
    %>
    </select>
    </td>
  </tr>
</table>
<%If HayPreg=true then%>
<br>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%" bgcolor="#FFFFFF">
  <tr class="etabla2">
    <td width="5%" height="5%">Orden</td>
    <td width="10%" height="5%">Tipo</td>
    <td width="60%" height="5%">Enunciado</td>
    <td width="10%" height="5%">Obligatoria</td>
    <td width="10%" height="5%">&nbsp;</td>
  </tr>
  <%Do While Not rsPreguntas.EOF%>
  <tr>
    <td width="5%" height="5%"><%=rsPreguntas("ordenpregunta")%></td>
    <td width="10%" height="5%"><%=rsPreguntas("nombretipo")%>&nbsp;</td>
    <td width="60%" height="5%"><%=rsPreguntas("titulopregunta")%></td>
    <td width="10%" height="5%"><%=rsPreguntas("obligatoria")%>&nbsp;</td>
    <td width="10%" height="5%">
    <a TARGET="_self" href="frmpregunta.asp?accion=modificarpregunta&idpregunta=<%=rsPreguntas("idPregunta")%>&idevaluacion=<%=idevaluacion%>&codigo_ccv=<%=codigo_ccv%>">
    	<img border="0" src="../../../../images/editar.gif"></a>
    	<img border="0" src="../../../../images/eliminar.gif" onclick="EliminarPregunta('<%=rsPreguntas("idPregunta")%>');" class="imagen">
    </td>
  </tr>
  <%	rsPreguntas.movenext
  Loop
  %>
</table>
<%end if

Set rsPreguntas=nothing
%>
<p align="center">
	<%if accion="modificarevaluacion" and HayPreg=true then%>
	<input type="button" value=" Guardar" class="guardar" name="cmdGuardar" onClick="validarevaluacion()">
	<%end if%>
	<input onClick="location.href='../cargando.asp?rutapagina=tematicacurso.asp'" type="button" value="   Cancelar" name="cmdCancelar" class="cerrar">
</p>
</form>
</body>
</html>