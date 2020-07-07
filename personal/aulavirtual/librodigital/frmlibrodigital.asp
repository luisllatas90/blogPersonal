<!--#include file="clslibrodigital.asp"-->
<%
accion=Request.querystring("accion")
idlibrodigital=request.querystring("idlibrodigital")

set librodigital=new clslibrodigital
	with librodigital
		.restringir=session("idcursovirtual")
	
		If (Accion="modificarlibrodigital") then
			ArrDatos=.consultar("2",idlibrodigital,"","")
			fechareg=Arrdatos(9,0)
			titulolibrodigital=Arrdatos(2,0)
			descripcion=Arrdatos(3,0)
			Fechainicio=Arrdatos(4,0)
			Fechafin=Arrdatos(5,0)
			Procesarfechas Fechainicio,FechaFin
		Else
			procesarfechas now,session("fincursovirtual")
		End if
	end with
set librodigital=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registrar contenido digital</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/calendario.js"></script>
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarlibrodigital.js"></script>
</head>
<body topmargin="0" onload="document.all.titulolibrodigital.focus()">
<form name="frmlibrodigital" method="post" onSubmit="return validarlibrodigital(this)" action="procesar.asp?Accion=<%=Accion%>&idlibrodigital=<%=idlibrodigital%>">
<fieldset style="padding: 2">
  <legend class="e1">Datos del contenido digital</legend>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
    <tr>
      <td width="23%" valign="top" class="etiqueta">Título</td>
      <td width="77%" valign="top">
    <input  maxLength="100" size="74" name="titulolibrodigital" class="cajas" value="<%=titulolibrodigital%>"></td>
    </tr>
    <tr>
    <td width="100%" valign="top" colspan="2" align="left"><%BarraProgramacionFechas%>&nbsp;</td>
    </tr>
    <tr>
      <td width="23%" valign="top" class="etiqueta">Descripción</td>
      <td width="77%" valign="top">
      <textarea  name="descripcion" rows="3" cols="72" class="cajas"><%=descripcion%></textarea>
    </td>	
    </tr>
    <%call escribirtipopublicacion(accion)%>
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