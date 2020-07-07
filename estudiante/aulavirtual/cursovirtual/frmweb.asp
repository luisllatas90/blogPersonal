<!--#include file="clscursovirtual.asp"-->
<%
accion=request.querystring("accion")
idcursovirtual=request.querystring("idcursovirtual")
modo=request.querystring("modo")
mostrar=Request.querystring("mostrar")
if modo="X" then ventana="AbrirMensaje()"
if mostrar="" then mostrar="si"

dim curso
	Set curso=new clscursovirtual
		with curso
			ArrDatos=.Consultar("3",idcursovirtual,"","")
		end with
		web=ArrDatos(6,0)
	Set curso=nothing
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Página Web del Curso: <%=titulocursovirtual%></title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
</head>
<body onLoad="CargarPagina();ResaltaVista('0')" leftmargin="0" topmargin="3" bgcolor="#EEEEEE">
<form name="frmEditar" onSubmit="return <%=ventana%>;ConvertirHTML()" METHOD="POST" ACTION="procesar.asp?accion=modificarwebcurso&idcursovirtual=<%=idcursovirtual%>&modo=<%=modo%>">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%">
  <tr id="tblherramientas">
    <td width="100%" valign="top">
    <%if mostrar="si" then%><input type="submit" value="     Guardar" name="cmdGuardar" class="guardar4"><%end if%> 
	&nbsp;<select onchange="EjecutarComando('fontname',this[this.selectedIndex].value);">
		<option value="Arial">Arial</option>
		<option value="Arial Black">Arial Black</option>
		<option value="Arial Narrow">Arial Narrow</option>
        <option value="Comic Sans MS">Comic Sans MS</option>
        <option value="Courier New">Courier New</option>
		<option value="System">System</option>
		<option value="Tahoma">Tahoma</option>
		<option value="Times New Roman">Times New Roman</option>
		<option value="Verdana">Verdana</option>
		<option value="Wingdings">Wingdings</option>
	</select> <select onchange="EjecutarComando('fontsize',this[this.selectedIndex].value);">
		<%for i=1 to 14%>
			<option value="<%=i%>"><%=i%>pt</option>
		<%next%>
	</select> &nbsp;|<img src="../../../images/negrita.gif" alt="Negrita" onClick="Negrita()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="absmiddle"><img src="../../../images/cursiva.gif" alt="Cursiva" onClick="Cursiva()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="absmiddle"><img src="../../../images/subrayado.gif" alt="Subrayado" onClick="Subrayado()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="absmiddle"><img src="../../../images/superindice.gif"  onClick="Superindice()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Superíndice">
     <img src="../../../images/subindice.gif" onClick="Subindice()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Subíndice"> |<img src="../../../images/izquierda.gif" onClick="AlineaIzquierda()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Alinear a la izquierda"><img src="../../../images/centrado.gif" onClick="AlineaCentro()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Alinear al centro"><img src="../../../images/derecha.gif" onClick="AlineaDerecha()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Alinear a la derecha">
    |<img src="../../../images/numeros.gif" onClick="Numeros()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Numeración"><img src="../../../images/lista.gif"  onClick="Vinetas()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Viñetas">
     <img src="../../../images/alinearizq.gif" onClick="SangriaDerecha()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Reducir sangría">
     <img src="../../../images/alinearder.gif" onClick="SangriaIzquierda()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Aumentar sangría"> 
    |<img src="../../../images/color.gif" onClick="VentanaPaletaColores()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Color de fuente">|<img src="../../../images/tabla.gif" onClick="VentanaTabla()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Insertar tabla"><img border="0" src="../../../images/imagen.gif" onClick="AgregarImagen('frmimagen.asp?idcursovirtual=<%=idcursovirtual%>')" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Insertar imágen desde archivo"><img border="0" src="../../../images/vinculo.gif" onClick="Vinculo()" onmouseover="ResaltaEntrada(this);" onmouseout="ResaltaSalida(this);" onmousedown="ResaltaAbajo(this);" onmouseup="ResaltaArriba(this);" align="middle" alt="Hipervínculo"></td>
  </tr>
</table>
  <iframe width="100%" height="85%" id="Contenido" border="0" name="Contenido" src="plantilla.html" target="_self"></iframe>
	<script language="JavaScript" src="../../../private/formato.js"></script>
   	<div style="position:absolute;width:200;left:461;top:88;height:25;visibility:hidden">
		<textarea rows="1" name="web" cols="20"><%=web%></textarea>
	</div>
<input onclick="VistaHTML(false);ResaltaVista('0','<%=mostrar%>');" type="button" value="   Normal" name="cmdnormal" class="normal">
<input onclick="VistaHTML(true);ResaltaVista('1','<%=mostrar%>');" type="button" value="      HTML    " name="cmdhtml" class="html">
</form>
</body>
</html>