<!--#include file="clsdocumento.asp"-->
<%
Dim TipoFormulario,Titulo,archivo

Accion=Request.querystring("Accion")
idcarpeta=request.querystring("idcarpeta")
idDocumento=request.querystring("idDocumento")
tipodoc=request.querystring("tipodoc")
numfila=request.querystring("numfila")

set documento=new clsdocumento
	with documento
		.restringir=session("idcursovirtual")
		.RetornarTipofrm Accion,tipodoc,TipoFormulario,Titulo
		
		If (Accion="modificardocumento" or Accion="modificarcarpeta") then
			ArrDatos=.consultar("3",idDocumento,"","","")
			archivo=Arrdatos(2,0)
			titulodocumento=Arrdatos(3,0)
			Fechainicio=Arrdatos(5,0)
			Fechafin=Arrdatos(6,0)
			Procesarfechas Fechainicio,FechaFin
			descripcion=Arrdatos(7,0)
			escritura=Arrdatos(15,0)
		Else
			procesarfechas now,session("fincursovirtual")
		End if
		
		if (tipodoc="A" AND Accion="agregardocumento") then
			archivo=GenerarNombreArchivo(session("codigo_usu"))
			pagina="guardararchivo.asp"
		else
			pagina="procesar.asp"
		end if
	end with
set documento=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registrar documentos</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/calendario.js"></script>
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validardocumento.js"></script>
</head>
<body topmargin="0" onload="document.all.titulodocumento.focus()">
<form name="frmdocumento" <%=TipoFormulario%> method="post" onSubmit="return validardocumento(this,'<%=tipodoc%>','<%=session("tipofuncion")%>')" action="<%=pagina & "?Accion=" & Accion%>&idDocumento=<%=idDocumento%>&idcarpeta=<%=idcarpeta%>&tipodoc=<%=tipodoc%>&archivo=<%=archivo%>&numfila=<%=numfila%>">
<input type="hidden" name="doCreate" value="true">
<input type="hidden" name="txtidcarpeta" value="<%=idcarpeta%>">
<fieldset style="padding: 2">
  <legend class="e1">Datos <%=titulo%></legend>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
    <tr>
      <td width="23%" valign="top" class="etiqueta">Nombre</td>
      <td width="77%" valign="top">
    <input  maxLength="100" size="74" name="titulodocumento" class="cajas" value="<%=titulodocumento%>"></td>
    </tr>
    <%If (tipodoc="A" and Accion="agregardocumento") then%>
    <tr>
      <td width="23%" valign="top" class="etiqueta">Buscar ubicación del archivo&nbsp;</td>
      <td width="77%" valign="top"><input class="cajas" type="File" name="file" size="50" style="height:20"></td>
    </tr>
    <%End if
    if session("tipofuncion")<>3 then%>
    <tr>
    <td width="100%" valign="top" colspan="2" align="left"><%BarraProgramacionFechas%>&nbsp;</td>
    </tr>
    <%end if%>
    <tr>
      <td width="23%" valign="top" class="etiqueta">Comentario</td>
      <td width="77%" valign="top">
      <textarea  name="descripcion" rows="3" cols="72" class="cajas"><%=descripcion%></textarea>
    </td>
    </tr>
    <%
    if tipodoc<>"L" then
    	call escribirtipopublicacion(Accion)
    end if
    %>
    <input type="hidden" name="versiondoc" value="0">
    <%if tipodoc="C" then%>
    <tr>
      <td width="23%" align="right" valign="top"><input type="checkbox" name="escritura" value="1" <%=Marcar("1",escritura)%>>&nbsp;</td>
      <td width="77%" valign="top">
          	<font class="azul"><b>Permiso de Escritura</b></font><br>
    Los participantes de la actividad académica podrán registrar documentos en la 
      carpeta </td>
    </tr>
    <%end if%>
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