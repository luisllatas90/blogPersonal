<!--#include file="clsdocumento.asp"-->
<%
Dim scriptCarpetaPredeterminada,veces
Dim NumNodoAbierto

Set documento=new clsdocumento
	with documento
		.restringir=session("idcursovirtual")
		NumNodoAbierto =request.querystring("NumNodoAbierto")
		veces=request.querystring("veces")

		scriptCarpetaPredeterminada=.PredeterminarCarpeta(veces,session("codigo_usu"),session("idcursovirtual"),session("tipofuncion"))
		if veces="" then
			'Se produce cuando se despliega un nodo de las carpetas
			scriptCarpetaPredeterminada="Onload=""ResaltarCarpetaMarcada('" & session("tipofuncion") & "','" & usuarioactual & "')"""
		end if
%>
<html>
<HEAD>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="../../../private/Menu.js"></script>
<script language="JavaScript" src="private/validardocumento.js"></script>
<STYLE>
<!--
	img 	{border:0px none;align:absbottom}
	tr      {top: 0;cursor:hand }
-->
</STYLE>
<base target="_self">
</HEAD>
<body bgcolor="#EBE1BF" topmargin="0" oncontextmenu="return event.ctrlKey" onClick="OcultarMenuPopUp()" leftmargin="0" <%=scriptCarpetaPredeterminada%>>
<table cellpadding=0 cellspacing=0 border=0 width="100%" heigth="100%">
<%if session("tipofuncion")<>3 then%>
<tr><td class="azul" onclick="AgregarCarpeta()"><img border="0" src="../../../images/abierto.gif" name="arrImgCarpetas" id="imgCarpeta0"><img border="0" src="../../../images/nuevo.gif"><span id="spCarpeta0">&nbsp;<b>Carpeta Principal</b></span></td></tr>
<%end if
		response.write .crearArbolArchivos(0,"",session("codigo_usu"),session("idcursovirtual"),session("tipofuncion"),session("codigo_usu"))
	end with
Set documento=nothing
%>
</table>
<div id="MenuDir" class="contornotabla" oncontextmenu="return event.ctrlKey" onclick="ActivarMenuPopUp()" style="position:absolute;display:none;width:140">
<table border="0" cellpadding="3" cellspacing="0" width="100%" bgcolor="#FFFFFF">
  <tr>
    <td width="100%" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" id="mnuAgregar">
    <img border="0" src="../../../images/cerrado.gif"> Nueva Carpeta&nbsp;</td>
  </tr>
  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
    <td width="100%" id="mnuModificar">
    <img border="0" src="../../../images/editar.gif">Modificar Carpeta&nbsp;</td>
  </tr>
  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
    <td width="100%" id="mnuEliminar">
    <img border="0" src="../../../images/eliminar.gif"> Eliminar Carpeta&nbsp;</td>
  </tr>
  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
    <td width="100%" class="bordeinf" id="mnuMover">
    <img border="0" src="../../../images/mover.gif"> Mover Carpeta...&nbsp;</td>
  </tr>
  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
    <td width="100%" id="mnuPermisos">
    <img border="0" src="../../../images/menu0.gif">Administrar Permisos&nbsp;</td>
  </tr>
</table>
</div>

</body>
</html>