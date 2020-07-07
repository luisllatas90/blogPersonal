<!--#include file="clstarea.asp"-->
<%
modo=request.querystring("modo")
idtareausuario=request.querystring("idtareausuario")

if idtareausuario<>"" then
	Set tarea=new clstarea
	with tarea
		.restringir=session("idcursovirtual")
		Arrtarea=.Consultar("9",idtareausuario,"","")

		If IsEmpty(Arrtarea)=false then
			fechareg=Arrtarea(1,0)
			idtarea=Arrtarea(2,0)
			idcreador=Arrtarea(3,0)
			archivoversion=Arrtarea(4,0)		
			refidtareausuario=Arrtarea(5,0)
			bloqueada=Arrtarea(7,0)
			obs=Arrtarea(8,0)
			nombrecreador=Arrtarea(9,0)
			idestadorecurso=Arrtarea(10,0)
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
<script language="JavaScript" src="private/validartarea.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
</head>

<body topmargin="0" leftmargin="0">

<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="5%" class="etiqueta" valign="top" style="cursor:hand" rowspan="4">
   	<%if session("idestadocursovirtual")=1 then%>
   		<img border="0" src="../../../images/editar.gif" onclick="AbrirVersionTarea('M','<%=idtarea%>','<%=idtareausuario%>')" alt="Modificar las propiedades de la versión del documento">
   	<%end if
   	if modo="modificarversion" then%>
   	   	<img border="0" src="../../../images/guardar.gif" onclick="AbrirVersionTarea('G','<%=idtarea%>','<%=idtareausuario%>')" ALT="Guardar cambios realizados"/><br>
    	&nbsp;<img border="0" src="../../../images/salir.gif" onclick="AbrirVersionTarea('R','<%=idtarea%>','<%=idtareausuario%>')" ALT="Regresar atrás"/>
   <%end if%>    	
    </td>
    <td class="bordeinf" valign="top" colspan="3" width="90%">
    <%if session("idestadocursovirtual")=1 then
   		if modo="" and bloqueada=0 and idestadorecurso=1 then%>
   		<span style="cursor:hand" class="azul" onclick="AbrirVersionTarea('A','<%=idtarea%>','<%=idtareausuario%>')">
   		<img border="0" src="../../../images/anadir.gif"  alt="Añadir nueva versión del documento">
   		<b>Añadir nueva versión de tarea</b>
   		</span>
   		<%end if
   	end if%>
    </td>
  </tr>
  <tr>
    <td width="22%" class="etiqueta" valign="top">Fecha de Registro</td>
    <td width="40%" valign="top">: <%=fechareg%>&nbsp;</td>
    <td width="32%" valign="top" align="right"><%if bloqueada=0 then response.write .BuscarRuta(idtareausuario,"documento",archivoversion,"<img border='0' src='../../../images/ext/" & right(archivoversion,3) & ".gif'>&nbsp;Descargar tarea",session("idestadocursovirtual")) end if%>&nbsp;</td>
  </tr>
  <tr>
    <td width="22%" class="etiqueta" valign="top">Registrado por</td>
    <td width="72%" valign="top" colspan="2">: <%=nombrecreador%>&nbsp;</td>
  </tr>
  <tr>
    <td width="22%" class="etiqueta" valign="top">Observaciones</td>
    <td width="72%" valign="top" colspan="2">
    	<%if modo="modificarversion" then%>
    		<input class="cajas" type="textbox" id=obs value="<%=obs%>" size="20">
    	<%else
    		response.write ":&nbsp;" & PreparaMemo(obs)
    	end if%>
    </td>
  </tr>
  <%if (bloqueada=1 AND modo="") then%>
  <tr>
    <td width="5%" valign="top" align="right">&nbsp;</td>
    <td width="22%" valign="top" align="right"><img border="0" src="../../../images/bloquear.gif"></td>
    <td width="72%" valign="top" class="rojo" colspan="2">Tarea bloqueada para 
    registrar documentos</td>
  </tr>
  <%end if
  if modo="modificarversion" then%>
  <tr>
    <td width="5%" valign="top" align="right">
      &nbsp;</td>
    <td width="22%" valign="top" align="right">
      <input type="checkbox" id="chkbloqueada" value="1" <%=Marcar("1",bloqueada)%>></td>
    <td width="72%" valign="top" colspan="2">No Permitir <b>agregar</b> nuevas versiones al tarea</td>
  </tr>
    <%end if%>

  </table>

</body>

</html>
	<%end with
	Set tarea=nothing
end if
%>