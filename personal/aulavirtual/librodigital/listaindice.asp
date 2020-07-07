<!--#include file="clslibrodigital.asp"-->
<%
Dim veces,usuarioactual
Dim NumNodoAbierto

modo=request.querystring("modo")
idlibrodigital=request.querystring("idlibrodigital")
titulolibro=request.querystring("titulolibro")
Set contenido=new clslibrodigital
	with contenido
		.restringir=session("idcursovirtual")
		usuarioactual=session("codigo_usu")
		usuarioactual=replace(usuarioactual,"\","/")
		NumNodoAbierto =request.querystring("NumNodoAbierto")
%>
<html>
<HEAD>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarlibrodigital.js"></script>
<STYLE>
<!--
	img 	{border:0px none;align:absbottom}
	tr      {top: 0;cursor:hand }
-->
</STYLE>
<base target="_self">
</HEAD>
<!--oncontextmenu="return event.ctrlKey"-->
<body oncontextmenu="return event.ctrlKey" <%if modo="administrar" then%>onClick="OcultarMenuPopUp()"<%end if%>>
<input type="hidden" id="txtidcontenido" value="<%=numNodoAbierto%>">
<input type="hidden" id="txttitulocontenido" value="<%=numNodoAbierto%>">
<input type="hidden" id="txtidlibrodigital" value="<%=idlibrodigital%>">
<input type="hidden" id="txttitulolibro" value="<%=titulolibro%>">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse; " width="100%">
  <tr>
    <td width="65%" class="e4" valign="top">&nbsp;<%=titulolibro%></td>
    <td width="35%" align="right" valign="top">
    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
      <tr>
      	<td width="34%"><%if session("codigo_usu")="USAT\gchunga" then%><img border="0" src="../../../images/propiedades.gif">Anotaciones<%end if%></td>
        <td width="33%" onClick="AbrirGlosario('V','<%=idlibrodigital%>','','<%=modo%>')">
        <img border="0" src="../../../images/librohoja.gif"> Glosario</td>
        <td width="33%" onClick="AbrirContenido('R','<%=idlibrodigital%>')">
        <img border="0" src="../../../images/bien.gif"> Recopilar</a></td>
      </tr>
    </table>
   	</td>
  </tr>
</table>
<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla" height="90%">
  <tr>
    <td width="90%" valign="top">
    <DIV id="listadiv" style="height:100%">
	<table cellpadding=3 cellspacing=0 border=0 width="100%">
		<tr><td id="tbl0" onClick="AbrirContenido('A','<%=idlibrodigital%>')"><img name="arrImgcontenidos" id="imgcontenido0" border="0" src="../../../images/libroabierto.gif" ALT="Agregar Índice General">&nbsp;<span id="spcontenido0" class="e1">Tabla de Contenidos</span></td></tr>
		<%response.write .CargarIndice(0,"",idlibrodigital,modo,titulolibro)
	end with
	Set contenido=nothing
	%>
	</table>
	</div>
	</td>
  </tr>
</table>
<div id="MenuDir" class="contornotabla" oncontextmenu="return event.ctrlKey" onclick="ActivarMenuPopUp()" style="position:absolute;display:none;width:140">
<table border="0" cellpadding="3" cellspacing="0" width="100%" bgcolor="#F2F2F2" style="border-collapse: collapse" bordercolor="#111111">
  <tr>
    <td width="100%" id="mnuAgregar">
    <img border="0" src="../../../images/librocerrado.gif"> Agregar índice&nbsp;</td>
  </tr>
  <tr>
    <td width="100%" id="mnuModificar">
    <img border="0" src="../../../images/editar.gif">Modificar índice&nbsp;</td>
  </tr>
  <tr>
    <td width="100%" id="mnuEliminar">
    <img border="0" src="../../../images/eliminar.gif"> Eliminar índice&nbsp;</td>
  </tr>
  <tr>
    <td width="100%" id="mnuContenido">
    <img border="0" src="../../../images/librohoja.gif"> Agregar contenido&nbsp;</td>
  </tr>
</table>
</div>
</body>
</html>