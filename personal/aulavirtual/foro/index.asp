<!--#include file="clsforo.asp"-->
<%
Dim IdDocMarcado,scriptResalte

idOpt=request.querystring("idOpt")
IdDocMarcado=request.querystring("IdDocMarcado")
numfila=request.querystring("numfila")

	Dim foro
	Dim Arrforo
	
	Set foro=new clsforo
		with foro
			.restringir=session("idcursovirtual")
			Arrforo=.Consultar("1",session("idcursovirtual"),"","")
						
			if numfila<>"" and IsEmpty(Arrforo)=false then
				scriptResalte="onLoad=""ResaltarArchivo('" & numfila & "','" & IdDocMarcado & "')"""
			end if
		end with
	Set foro=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de foroes</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarforo.js"></script>
<base target="listadocs">
</head>
<body <%=scriptResalte%>>
<input type="hidden" id="txtelegido">
<input type="hidden" id="txttituloforo">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="90%" height="22" class="e1">Temas de discusión</td>
    <td width="10%" align="right" height="22">
    <%if session("creartemas")=1 or session("codigo_tfu")<>3 then%>
    <input type="button" value="Agregar nuevo" name="cmdNuevo" class="nuevo" onclick="AbrirForo('A')">
    <%end if%>
    </td>
  </tr>
</table>
<br>
<%If IsEmpty(Arrforo)=true then%>
	<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se han encontrado 
    temas de discusión registrados</p>
<%else%>
<table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#666666" width="100%" height="87%">
  <tr class="fondonulo">
    <td width="50%" height="3%"><b>&nbsp;Título de la Encuesta</b></td>
    <td width="25%" height="3%"><b>&nbsp;Disponibilidad</b></td>
    <td width="20%" height="3%"><b>Autor</b>&nbsp;</td>
    <td width="10%" height="3%"><b>Debates</b>&nbsp;</td>
  </tr>
  <tr>
  <td width="100%" align="center" colspan="4" valign="top" height="45%">
  <DIV id="listadiv" style="height:100%">
  <table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tbllistaforo">	  
  <%for I=lbound(Arrforo,2) to Ubound(Arrforo,2)%>
  <tr id="fila<%=Arrforo(0,I)%>" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onClick="Resaltarfila(tbllistaforo,this,'detalleforo.asp?idforo=<%=Arrforo(0,i)%>&numfila=<%=i%>')">
    <td width="3%" height="10px"><img border='0' src="../../../images/vineta.gif"/>&nbsp;</td>
    <td width="47%" height="10px"><%=Arrforo(3,I)%>&nbsp;</td>
    <td width="28%" height="10px"><%=Arrforo(1,I) & " - " & Arrforo(2,I)%>&nbsp;</td>
    <td width="17%" height="10px"><%=Arrforo(5,I)%></td>
    <td width="10%" height="10px"><%=Arrforo(4,I)%></td>
  </tr>
  <%next%>
  </table>
  </DIV>  
  </td></tr>
  <tr>
  	<td width="100%" colspan="4" height="3%">
  	<table cellSpacing="0" cellPadding="3" width="100%" border="0" style="border-collapse: collapse" bordercolor="#111111" height="100%">
  	<tr>
    <td class="paginaDoc" width="65%"><img border="0" src="../../../images/menos.gif" onclick="MostrarTabla(detalleforo,'../../../images/',this)"> 
    Datos del tema de discusion</td>
    <td width="25%" class="azul" background="../../../images/fondopestana2.gif" align="right" valign="top"><b><%=ubound(Arrforo,2)+1%> 
    Tema(s) de discusión</b></td>
  	</tr>
  	</table> 	
  	</td>
  </tr>
  <tr id="detalleforo">
  	<td width="100%" colspan="4" height="40%"><span class="sugerencia" id="mensajedetalle">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Actualmente no hay ningúna 
    tema seleccionado de la Lista.</span>
  	<iframe name="fradetalle" height="100%" width="100%" border="0" frameborder="0">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe>
  	</td>
  </tr>
</table>
<%end if%>
</body>
</html>