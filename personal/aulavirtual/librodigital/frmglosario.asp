<!--#include file="clslibrodigital.asp"-->
<%
Dim accion,idglosario,idlibrodigital

accion=Request.querystring("accion")
idlibrodigital=request.querystring("idlibrodigital")
idglosario=request.querystring("idglosario")

set contenido=new clslibrodigital
	with contenido
		.restringir=session("idcursovirtual")
		
		If Accion="modificarglosario" then
			ArrDatos=.consultar("7",idglosario,"","")
			fechareg=ArrDatos(5,0)
			tituloglosario=Arrdatos(2,0)
			descripcion=Arrdatos(3,0)
		End if
	end with
set contenido=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registrar índice temático (Fecha de registro:<%=fechareg%>)</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/calendario.js"></script>
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarlibrodigital.js"></script>
</head>
<body topmargin="0" onload="document.all.tituloglosario.focus()">
<form name="frmcontenido" method="post" onSubmit="return validarglosario(this)" action="procesar.asp?accion=<%=Accion%>&idglosario=<%=idglosario%>&idlibrodigital=<%=idlibrodigital%>">
<fieldset style="padding: 2">
  <legend class="e1">Agregar término al glosario </legend>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
    <tr>
      <td width="23%" valign="top" class="etiqueta">Término</td>
      <td width="77%" valign="top">
    <input  maxLength="500" size="74" name="tituloglosario" class="cajas" value="<%=tituloglosario%>"></td>
    </tr>
    <tr>
      <td width="23%" valign="top" class="etiqueta">
      Descripción del Término</td>
      <td width="77%" valign="top" class="rojo">
      <textarea  name="descripcion" rows="10" cols="72" class="cajas"><%=descripcion%></textarea></td>	
    </tr>
    <tr>
      <td width="23%">&nbsp;</td>
      <td width="77%">
    <input type="submit" value="Guardar" name="cmdGuardar" id="cmdGuardar" class="guardar"> <input OnClick="window.close()" type="button" value="Cancelar" name="cmdCancelar" id="cmdCancelar" class="salir">
    <span id="mensaje" style="color:#FF0000"></span>
    </td>
    </tr>
  </table>
</fieldset>
</form>
</body>
</html>