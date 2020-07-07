<!--#include file="clsdocumento.asp"-->
<%
modo=request.querystring("modo")
idversion=request.querystring("idversion")

if idversion<>"" then
	Set documento=new clsdocumento
	with documento
		.restringir=session("idcursovirtual")
		ArrDocumento=.Consultar("6",idversion,"","","")

		If IsEmpty(ArrDocumento)=false then
			fechareg=ArrDocumento(1,0)
			archivoversion=ArrDocumento(2,0)
			tituloversion=ArrDocumento(3,0)
			iddocumento=ArrDocumento(4,0)
			idcreador=ArrDocumento(5,0)
			publica=Arrdocumento(6,0)
			bloqueada=ArrDocumento(7,0)
			estado=ArrDocumento(8,0)
			obs=ArrDocumento(9,0)
			refidversion=ArrDocumento(10,0)
			nusuario=ArrDocumento(11,0)
			mensajebloqueo=iif(bloqueada=0,"bloquear","bien")
		end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Detalle de versión</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validardocumento.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
</head>

<body topmargin="0" leftmargin="0" class="fondonulo">

<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr>
    <td width="5%" class="etiqueta" valign="top" style="cursor:hand" rowspan="4">
    <%if modo="modificarversion" and session("idestadocursovirtual")=1 then %>
    	<img border="0" src="../../../images/guardar.gif" onclick="GuardarVersion('<%=iddocumento%>','<%=idversion%>')" ALT="Guardar cambios realizados"/><br>
    	<img border="0" src="../../../images/salir.gif" onclick="ModoVersion('<%=iddocumento%>','<%=idversion%>')" ALT="Regresar atrás"/>
    <%elseif (bloqueada="0" and modo="" and session("idestadocursovirtual")=1) then%>
    		<img border="0" src="../../../images/anadir.gif" onclick="AgregarVersion('<%=iddocumento%>','<%=idversion%>')" alt="Añadir nueva versión del documento"><br>
   	<%end if
    if (idcreador=session("codigo_usu") and session("idestadocursovirtual")=1) then%>
    	<img border="0" src="../../../images/editar.gif" onclick="ModificarVersion('<%=iddocumento%>','<%=idversion%>')" alt="Modificar las propiedades de la versión del documento">
    	<br><img border="0" src="../../../images/eliminar.gif" onclick="EliminarVersion('<%=iddocumento%>','<%=idversion%>','<%=archivoversion%>')" alt="Eliminar versión del documento">
    <%end if%>
    </td>
    <td width="22%" class="etiqueta" valign="top">Fecha de Registro</td>
    <td width="40%" valign="top">: <%=fechareg%>&nbsp;</td>
    <td width="32%" valign="top" align="right"><%if (publica=0 and modo="") then%><%=.BuscarRuta("versiondocumento",idversion,"A",0,archivoversion,"<img border='0' src='../../../images/ext/" & right(archivoversion,3) & ".gif'>&nbsp;Descargar documento",estado)%><%end if%>&nbsp;</td>
  </tr>
  <tr>
    <td width="22%" class="etiqueta" valign="top">Descripción</td>
    <td width="72%" valign="top" class="azul" colspan="2">:
    <%if modo="modificarversion" then%>
    	<input type="text" name="tituloversion" value="<%=tituloversion%>" class="cajas" size="20" style="width: 80%">
    <%else
    	response.write tituloversion
    end if%>
    </td>
  </tr>
  <tr>
    <td width="22%" class="etiqueta" valign="top">Registrado por</td>
    <td width="72%" valign="top" colspan="2">: <%=nusuario%>&nbsp;</td>
  </tr>
  <tr>
    <td width="22%" class="etiqueta" valign="top"><%if obs<>"" then%>Observaciones<%end if%></td>
    <td width="72%" valign="top" colspan="2"><%if obs<>"" then response.write ":&nbsp;" & PreparaMemo(obs)%>&nbsp;</td>
  </tr>
  <%if (bloqueada=1 OR publica=1) AND modo="" then%>
  <tr>
    <td width="5%" valign="top" align="right">&nbsp;</td>
    <td width="22%" valign="top" align="right"><img border="0" src="../../../images/bloquear.gif"></td>
    <td width="72%" valign="top" class="rojo" colspan="2">Documento 
    Bloqueado&nbsp;temporalmente por el usuario:<%if bloqueada=1 then%><br>-&nbsp;No permite AGREGAR nuevas versiones.<%end if%><%if publica=1 then%><br>-&nbsp;No permite DESCARGALO<%end if%></td>
  </tr>
  <%end if
  if modo="modificarversion" then%>
  <tr>
    <td width="5%" valign="top" align="right">
      &nbsp;</td>
    <td width="22%" valign="top" align="right">
      <input type="checkbox" name="bloqueada" value="1" <%=Marcar("1",bloqueada)%>></td>
    <td width="72%" valign="top" colspan="2">No Permitir <b>agregar</b> nuevas versiones al documento</td>
  </tr>
  <tr>
    <td width="5%" valign="top" align="right">
      &nbsp;</td>
    <td width="22%" valign="top" align="right">
      <input type="checkbox" name="publica" value="1" <%=Marcar("1",publica)%>></td>
    <td width="72%" valign="top" colspan="2">No Permitir <b>descargar</b> el 
    documento y visualizar las observaciones realizadas</td>
  </tr>
    <%end if%>

  </table>

</body>

</html>
	<%end with
	Set documento=nothing
end if
%>