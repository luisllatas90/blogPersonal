<!--#include file="../../../funcionesaulavirtual.asp"-->
<%
Dim usuario

titulo=request.querystring("titulo")
idtabla=request.querystring("idtabla")
nombretabla=request.querystring("nombretabla")
descripcion=request.querystring("descripcion")

Set Obj= Server.CreateObject("AulaVirtual.clsCategoria")
	ArrDatos=Obj.Consultar("3",nombretabla,idtabla,"")
Set Obj= Nothing
	If IsEmpty(ArrDatos)=false then
		idtipopublicacion=ArrDatos(0,0)
	end if

if idtipopublicacion="" then idtipopublicacion=1	
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Permisos de acceso al recurso seleccionado</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarusuario.js"></script>
<script>
function MostrarTR(ctrl)
{
	switch(ctrl.value)
	{
		case "1":
			mensaje.innerHTML="Acceso a todos los participantes del curso o actividad académica (<%=session("numusuarios")%> participantes)"
			tblusuarios.style.display="none"
			tblbarra.style.display=""
			break
		case "2":
			mensaje.innerHTML="&nbsp;"
			tblusuarios.style.display=""
			tblbarra.style.display="none"		
			break
		case "3":
			mensaje.innerHTML="Acceso al Usuario: <%=session("Nombre_Usu")%>"
			tblusuarios.style.display="none"
			tblbarra.style.display=""	
			break
	}
}
</script>
</head>
<body onLoad="MostrarTR(cbxidtipopublicacion)">
<p class="e1"><%=titulo%><br><%=ucase(descripcion)%></p>
<center>
<table class="encabezadopregunta" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="95%" height="10%">
  <tr>
    <td valign="top" class="etiqueta">Especifique ¿Qué usuarios de la Actividad Académica desea que tengan acceso al recurso seleccionado?</td>
    <td valign="top"><%call escribirlista("cbxidtipopublicacion","multiple","OnChange=""MostrarTR(this)""",idtipopublicacion,"clscategoria","2","","","")%>&nbsp;</td>
  </tr>
  <tr id="tblbarra" style="display:none">
    <td valign="top"><input type="button" onClick="ValidarPermiso('<%=idtabla%>','<%=nombretabla%>','<%=idtipopublicacion%>')" value="Guardar" name="cmdGuardar" class="guardar"> 
    <input onClick="window.close()" type="button" value="Cancelar" name="cmdCancelar" class="cerrar"></td>
    <td valign="top">&nbsp;</td>
  </tr>
</table>
<br>
<table border="0" style="border-collapse:collapse" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="95%" id="tblusuarios" style="display:none" height="70%">
  <tr>
    <td width="100%">
    <iframe name="frausuarios" id="frausuarios" src="listapermisos.asp?modo=1&idtabla=<%=idtabla%>&nombretabla=<%=nombretabla%>" height="100%" width="100%" border="0" frameborder="0" scrolling="no">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe></td>
  </tr>
</table>
<h4 id="mensaje"></h4>
</center>
</body>
</html>