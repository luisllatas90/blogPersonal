<!--#include file="clslibrodigital.asp"-->
<%
Dim accion,idindice,idcontenido

accion=Request.querystring("accion")
idlibrodigital=request.querystring("idlibrodigital")
idindice=request.querystring("idindice")
idcontenido=request.querystring("idcontenido")

set contenido=new clslibrodigital
	with contenido
		.restringir=session("codigo_usu")
		
		If Accion="modificarindice" then
			ArrDatos=.consultar("5",idcontenido,"","")
			fechareg=ArrDatos(10,0)
			ordencontenido=Arrdatos(3,0)
			titulocontenido=Arrdatos(4,0)
			Fechainicio=Arrdatos(6,0)
			Fechafin=Arrdatos(7,0)
			if IsNull(fechainicio)=false then
				Procesarfechas Fechainicio,FechaFin
			else
				ProcesarFechas session("fechainicio"),session("fechafin")
			end if
			idindice=ArrDatos(8,0)
			idestadorecurso=ArrDatos(9,0)
		Else
			procesarfechas now,session("fincursovirtual")
			fechareg=now()
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
<body topmargin="0" onload="document.all.ordencontenido.focus()">
<form name="frmcontenido" method="post" onSubmit="return validarindice(this)" action="procesar.asp?accion=<%=Accion%>&idindice=<%=idindice%>&idcontenido=<%=idcontenido%>&idlibrodigital=<%=idlibrodigital%>">
<fieldset style="padding: 2">
  <legend class="e1">Datos del índice</legend>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
    <tr>
      <td width="23%" valign="top" class="etiqueta">Orden</td>
      <td width="77%" valign="top">
    <input  maxLength="50" size="74" name="ordencontenido" class="cajas" value="<%=ordencontenido%>"></td>
    </tr>
    <tr>
      <td width="23%" valign="top" class="etiqueta">Título</td>
      <td width="77%" valign="top">
    <input  maxLength="500" size="74" name="titulocontenido" class="cajas" value="<%=titulocontenido%>"></td>
    </tr>
    <tr>
      <td width="100%" valign="top" class="etiqueta" colspan="2"><%BarraProgramacionFechas%>&nbsp;</td>
    </tr>
    <tr>
      <td width="23%" valign="top" align="right">
      <input type="checkbox" name="idestadorecurso" value="3" <%=Marcar("3",idestadorecurso)%>></td>
      <td width="77%" valign="top" class="rojo">Bloquear visualización para los participantes 
      del curso</td>	
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