<!--#include file="clsevaluacion.asp"-->
<%
Dim IdDocMarcado,scriptResalte

idOpt=request.querystring("idOpt")
IdDocMarcado=request.querystring("IdDocMarcado")
numfila=request.querystring("numfila")
if idOpt="" then idOpt=1

	Dim evaluacion
	Dim Arrevaluacion
	
	Set evaluacion=new clsevaluacion
		with evaluacion
			.restringir=session("idcursovirtual")
			Arrevaluacion=.Consultar("1",session("codigo_usu"),session("Idcursovirtual"),IdOpt)
			
			if numfila<>"" and IsEmpty(Arrevaluacion)=false then
				scriptResalte="onLoad=""ResaltarArchivo('" & numfila & "','" & IdDocMarcado & "')"""
			end if
		end with
	Set evaluacion=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de evaluaciones</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarevaluacion.js"></script>
<base target="listadocs">
</head>
<body <%=scriptResalte%>>
<input type="hidden" id="txtelegido">
<input type="hidden" id="txttituloevaluacion">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="90%" height="22" class="e1">Lista de Cuestionarios
    <select  name="idmostrar" onChange="actualizarlista('index.asp?idOpt='+ this.value)">
    <option value="1" <%If IdOpt=1 then response.write "SELECTED"%>>Activos</option>
    <option value="3" <%If IdOpt=3 then response.write "SELECTED"%>>No Activos</option>
    </select></td>
    <td width="10%" align="right" height="22">
    <%if session("tipofuncion")<>3 then%>
    <input type="button" value="    Agregar" id="cmdAgregar" class="nuevo" onclick="MuestraMenuTemp(MenuEval)">
    <%end if%>
    </td>
  </tr>
</table>
<div id="MenuEval" style="position:absolute;visibility:hidden;z-index:4">
<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" width="150" class="colorbarra">
  <tr onMouseOver="Resaltar(1,this,'S','#EBEBEB')" onMouseOut="Resaltar(0,this,'S','#EBEBEB')" onClick="AbrirEvaluacion('A')">
    <td width="5%"><img border="0" src="../../../images/nuevo.gif"></td>
    <td width="95%">Cuestionario&nbsp;</td>
  </tr>
  <!--
  <tr onMouseOver="Resaltar(1,this,'S','#EBEBEB')" onMouseOut="Resaltar(0,this,'S','#EBEBEB')" onClick="AgregarDoc('L')">
    <td width="5%" valign="top"><img border="0" src="../../../images/menu3.GIF"></td>
    <td width="95%">Registro de fechas presenciales</td>
  </tr>
  <tr onMouseOver="Resaltar(1,this,'S','#EBEBEB')" onMouseOut="Resaltar(0,this,'S','#EBEBEB')" onClick="AgregarDoc('L')">
    <td width="5%" valign="top"><img border="0" src="../../../images/prop.GIF"></td>
    <td width="95%">Registro de evaluación</td>
  </tr>
  -->
  </table>
</div>
<div style="z-index:0">
<br>
<%If IsEmpty(Arrevaluacion)=true then%>
	<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se han encontrado 
    encuestas por realizar</p>
<%else%>
<table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#666666" width="100%" height="87%">
  <tr class="fondonulo">
    <td width="60%" colspan="2" height="3%"><b>&nbsp;Título de la Encuesta</b></td>
    <td width="40%" height="3%"><b>&nbsp;Disponibilidad</b></td>
  </tr>
  <tr>
  <td width="100%" align="center" colspan="4" valign="top" height="45%">
  <DIV id="listadiv" style="height:100%">
  <table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tbllistaevaluacion">	  
  <%for I=lbound(Arrevaluacion,2) to Ubound(Arrevaluacion,2)%>
  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onClick="ResaltarEvaluacion('<%=I%>','<%=Arrevaluacion(0,i)%>')">
    <td width="3%" height="10px"><img border='0' src="../../../images/vineta.gif"/>&nbsp;</td>
    <td width="57%" height="10px"><%=Arrevaluacion(1,I)%>&nbsp;</td>
    <td width="40%" height="10px"><%=Arrevaluacion(2,I) & " - " & Arrevaluacion(3,I)%>&nbsp;</td>
  </tr>
  <%next%>
  </table>
  </DIV>  
  </td></tr>
  <tr>
  	<td width="100%" colspan="3" height="3%">
  	<table cellSpacing="0" cellPadding="3" width="100%" border="0" style="border-collapse: collapse" bordercolor="#111111" height="100%">
  	<tr>
    <td class="paginaDoc" width="65%"><img border="0" src="../../../images/menos.gif" onclick="MostrarTabla(detalleeval,'../../../images/',this)"> 
    Detalles del recurso</td>
    <td width="25%" class="azul" background="../../../images/fondopestana2.gif" align="right" valign="top"><b><%=ubound(Arrevaluacion,2)+1%> 
    evaluacion-es</b></td>
  	</tr>
  	</table> 	
  	</td>
  </tr>
  <tr id="detalleeval">
  	<td width="100%" colspan="3" height="40%"><span class="sugerencia" id="mensajedetalleeval">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Actualmente no hay ningúna evaluacion seleccionado de la Lista.</span>
  	<iframe name="fradetalle" height="100%" width="100%" border="0" frameborder="0">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe>
  	</td>
  </tr>
</table>
<%end if%>
</div>
</body>
</html>