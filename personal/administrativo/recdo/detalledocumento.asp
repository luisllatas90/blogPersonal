<!--#include file="clsrecepcion.asp"-->
<%
if session("codigo_usu")="" then response.Redirect "../../../tiempofinalizado.asp"
idarchivo=request.querystring("idarchivo")
numeroexpediente=request.querystring("numeroexpediente")

Dim recepcion

if idarchivo="" then
	response.write "<h3>Haga clic en el documento a consultar</h3>"
else
	set recepcion=new clsrecepcion
		ArrDetalle=recepcion.ConsultarArchivosRegistrados("2",idarchivo,0,0)
	set recepcion=nothing
	
	If IsEmpty(ArrDetalle)=true then
		response.write "<h5>No se ha registrado el documento seleccionado</h5>"
	else
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<HEAD>
<title>Detalle de documento</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script type="text/javascript" language="JavaScript" src="private/validararchivo.js"></script>
<base target="Contenido">
</HEAD>
<body bgcolor="#EEEEEE">
<script type="text/javascript" language="javascript">
	parent.document.all.hdString.value="<%=request.querystring%>"
</script>
 	<table bgcolor="white" width="100%" cellspacing="0" cellpadding="3" border="1" style="BORDER-COLLAPSE: collapse" bordercolor="teal" height="100%">
 	<tr>
 	<%if session("codigo_tfu")=1 then%>
  	<td style="cursor:hand" class="fondonulo" width="22" height="76" rowspan="8" 
            valign="top" bgcolor="#E9E9E9"><img ALT="Modificar documento seleccionado"  onClick="AbrirArchivo('<%=idarchivo%>','modificararchivo')" border="0" src="../../../images/editar.gif"><p onClick="EliminarArchivo('eliminararchivo','&idarchivo=<%=idarchivo%>')"><img ALT="Eliminar documento seleccionado"  border="0" src="../../../images/eliminar.gif"></p>
	&nbsp;</td>
  	<%end if%>
  	<td width="108" height="13">Fecha Registro</td>
		<td width="295" height="13"><%=ArrDetalle(1,0)%> - <%=ArrDetalle(2,0)%></td>
		<td height="13" width="48" style="width: 263px">Prioridad:
		<%If ArrDetalle(11,0)=0 then%><font color="#008000">Normal</font>
		<%ElseIf ArrDetalle(11,0)=1 then%><font color="#808000">Media</font><%End If
		If ArrDetalle(11,0)=2 then%> <font color="#ff0000">Alta</font><%End If%>
		</tr>
  <tr>
    <td width="108" height="13">Tipo doc.:</td>
    <td width="572" height="13" colspan="2"><%=ArrDetalle(3,0)%>&nbsp;-&nbsp;<%=ArrDetalle(4,0)%></td>
  </tr>
  <tr>
    <td width="108" height="10">Nº Exp.:</td>
    <td width="572" height="10" colspan="2"><%=ArrDetalle(5,0)%></td>
	</tr>   
	<tr>
	  <td width="108" height="10">Asunto:</td>
	  <td width="572" height="10" colspan="2"><%=ArrDetalle(6,0)%></td>
	</tr>
	<tr>
	  <td width="108" height="10">Procedencia:</td>
    <td width="572" height="10" colspan="2"><%=ArrDetalle(7,0)%></td>
  </tr>
	<tr>
	  <td width="108" height="10">Dirigido a:</td>
    <td width="572" height="10" colspan="2"><%=ArrDetalle(8,0)%></td>
  </tr>
  <tr>
	  <td width="108" height="10">Observaciones:</td>
    <td width="572" height="10" colspan="2"><%=ArrDetalle(10,0)%></td>
  </tr>
  <tr>
	  <td width="108" height="10">Registrado por:</td>
    <td width="572" height="10" colspan="2"><%=ArrDetalle(12,0)%></td>
  </tr>
  </table>
	<%end if
end if%>
</body>
</HTML>