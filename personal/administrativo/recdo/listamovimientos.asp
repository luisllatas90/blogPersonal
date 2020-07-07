<!--#include file="clsrecepcion.asp"-->
<%
if session("codigo_usu")="" then response.Redirect "../../../tiempofinalizado.asp"

idarchivo=request.querystring("idarchivo")
numeroexpediente=request.querystring("numeroexpediente")

if idarchivo="" then idarchivo=0

Dim recepcion

	set recepcion=new clsrecepcion
		ArrMovimiento=recepcion.ConsultarArchivosRegistrados("3",idarchivo,0,0)
	set recepcion=nothing

%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>Movimientos</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="private/validararchivo.js"></script>
</head>
<body bgcolor="#EEEEEE">
<p>
    <input type="button" class="agregar2" value="   Agregar" OnClick="AbrirMovimiento('A','<%=idarchivo%>','<%=numeroexpediente%>')" NAME="cmdNuevo" >
    <%if IsEmpty(ArrMovimiento)=false then
    	if session("codigo_tfu")=1 then%>
    <input type="button" class="modificar2" value="    Modificar" OnClick="AbrirMovimiento('M','<%=idarchivo%>','<%=numeroexpediente%>')" NAME="cmdmodificar" >
    <input type="button" class="eliminar2" value="  Eliminar" onClick="EliminarMovimiento('<%=idarchivo%>','<%=numeroexpediente%>')" NAME="cmdEliminar" >
    	<%end if
    end if%></p>
<%If IsEmpty(ArrMovimiento)=true then
		response.write "<h5>El documento no ha sido enviado a su destinatario</h5>"
	else%>
<table bgcolor="white" width="100%" style="BORDER-COLLAPSE: collapse" bordercolor="#808080" cellpadding="3" cellspacing="0" border="1">
  <tr class="etabla">
  <td width="18" height="22">&nbsp;</td>
  <td width="154" height="22">Fecha</td>
  <td width="105" height="22">Desde</td>
  <td width="109" height="22">Hacia</td>
  <td width="100" height="22">Nº de Cargo</td>
  <td width="83" height="22">Motivo</td>
  <td width="10" height="22">Estado</td>
  <td height="22">Registrado por</td>
 </tr>
 <%for j =lbound(ArrMovimiento,2) to UBound(ArrMovimiento,2)%>
 <tr align="left" valign="top">
    <td width="18" height="14"><input type="radio" value="<%=ArrMovimiento(0,j)%>" name="idmovimiento" onClick="txtmovimiento.value=this.value"></td>
    <td width="154" height="14"><%=ArrMovimiento(1,j)%> - <%=ArrMovimiento(2,j)%></td>
	<td width="105" height="14"><%=ArrMovimiento(3,j)%></td>
    <td width="109" height="14"><%=ArrMovimiento(4,j)%></td>
    <td width="100" height="14"><%=ArrMovimiento(8,j)%></td>
    <td width="83" height="14"><%=ArrMovimiento(6,j)%></td>
    <td width="10" height="14"><%=ArrMovimiento(7,j)%></td>
    <td height="14"><%=ArrMovimiento(9,j)%></td>
   </tr>
  <%Next%>
</table>
	<input type="hidden" id="txtmovimiento">
<%end If%>
</body>
</html>