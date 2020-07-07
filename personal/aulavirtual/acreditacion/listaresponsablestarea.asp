<!--#include file="clsacreditacion.asp"-->
<%
if session("codigo_usu")="" then response.redirect "../tiempofinalizado.asp"

Dim Modalidad,idtareaevaluacion,idindicador,nombreindicador,nombretarea
Dim numcoord

Modalidad=Request("modalidad")
idtareaevaluacion=request.querystring("idtareaevaluacion")
idindicador=request.querystring("idindicador")
nombreindicador=request.querystring("nombreindicador")
nombretarea=request.querystring("nombretarea")
fechatarea=request.querystring("fechatarea")

idresponsabletarea=Request("idresponsabletarea")
idusuario=Request("idusuario")
tiporesponsable=Request("tiporesponsable")
numcoord=iif(session("numcoord")="",0,session("numcoord"))
nombredestino=Request("nombredestino")

response.Buffer=true

dim responsable
	Set responsable=new clsacreditacion
		with responsable
			If Len(Request.form("cmdGuardar"))>0 then
				Select case Modalidad
					case "AgregarNuevo"
						Call .Agregarresponsabletarea(idusuario,idtareaevaluacion,tiporesponsable,session("codigo_usu"),nombretarea,fechatarea,session("nombrecarrera"),session("nombre_usu"),nombredestino)
					case "Modificar"
						Call .Modificarresponsabletarea(idresponsabletarea,idusuario,tiporesponsable)
				end Select
				Modalidad=""
			Else
				If Modalidad="Eliminar" then
					Call .Eliminarresponsabletarea(idresponsabletarea)
				end If
			End if
	
			ArrDatos=.ConsultarEvaluacionModeloAcreditacion("6",idtareaevaluacion,session("idacreditacion"),0)
			ArrUsuario=.ConsultarUsuario("1",0,0,0)
			session("numcoord")=""
		end with
	Set responsable=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Responsables de la tarea asignada</title>
<script language="JavaScript" src="../../../private/validaciones.js"></script>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="Javascript">
	function actualizarnombredestino(lista)
	{
		var caja=document.all.nombredestino
		caja.value=lista.options[lista.selectedIndex].text
	}
</script>
</head>
<body topmargin="0" leftmargin="0">
<form name="frmresponsable" method="post" ACTION="listaresponsablestarea.asp?Modalidad=<%=Modalidad%>&idresponsabletarea=<%=idresponsabletarea%>&idtareaevaluacion=<%=idtareaevaluacion%>&idindicador=<%=idindicador%>&nombreindicador=<%=nombreindicador%>&nombretarea=<%=nombretarea%>&fechatarea=<%=fechatarea%>">
<table width="100%" border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111">
  <tr class="etabla"> 
      <td width="45%" colspan="2" style="text-align: left">&nbsp;Responsable de 
      la tarea</td>
      <td style="text-align: left">Tipo de asignación</td>
      <td width="30%" style="cursor:hand; text-align:right" onclick="MM_goToURL('self','listaresponsablestarea.asp?idtareaevaluacion=<%=idtareaevaluacion%>&Modalidad=AgregarNuevo&idindicador=<%=idindicador%>&nombreindicador=<%=nombreindicador%>&nombretarea=<%=nombretarea%>&fechatarea=<%=fechatarea%>');return document.MM_returnValue">
      <img ALT="Añadir responsables a la tarea seleccionada" border="0" src="../../../images/anadir.gif">&nbsp;</td>
	</tr>
<%if IsEmpty(ArrDatos)=false then
	num=0								  
	for i=Lbound(Arrdatos,2) to Ubound(Arrdatos,2)
		session("numcoord")=ArrDatos(4,0)
		num=num+1%>			  
    <tr>
    <td width="5%" align="right"><%=num%>.</td>
    <td width="40%"><%=ArrDatos(2,I)%></td>
    <td>
    <%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then%>
    	<select  name="tiporesponsable">
		<option value="P" <%=Seleccionar(ArrDatos(3,I),"P")%>>Participante</option>
    	<option value="C" <%=Seleccionar(ArrDatos(3,I),"C")%>>Coordinador</option>
	    </select>
	<%else
		response.write iif(ArrDatos(3,I)="P","Participante","Coordinador")
	end if%> 
	</td>
	<td align="right" style="cursor:hand" width="30%">
	<%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then%>
  		<input type="submit" value="     " name="cmdGuardar" class="imgGuardar">
  		<img border="0" src="../../../images/salir.gif" onclick="MM_goToURL('self','listaresponsablestarea.asp?idtareaevaluacion=<%=idtareaevaluacion%>&idindicador=<%=idindicador%>&nombreindicador=<%=nombreindicador%>&idresponsabletarea=<%=Arrdatos(1,I)%>&nombretarea=<%=nombretarea%>&fechatarea=<%=fechatarea%>');return document.MM_returnValue" >
	<%else%>
  		<img ALT="Eliminar responsable de la tarea" border="0" src="../../../images/eliminar.gif" onclick="ConfirmarEliminar('¿Está seguro que desea eliminar el usuario seleccionado?','listaresponsablestarea.asp?idresponsabletarea=<%=Arrdatos(0,I)%>&idtareaevaluacion=<%=idtareaevaluacion%>&idindicador=<%=indicador%>&nombreindicador=<%=nombreindicador%>&nombretarea=<%=nombretarea%>&fechatarea=<%=fechatarea%>')">
	<%end if%>
	</td>
	</tr>
	<%Next
 end if
 if trim(modalidad)="AgregarNuevo" then%>
  	<tr>
       	<td width="5%" align="right"><%=num+1%>.</td>
       	<td width="40%">
	<select name="idusuario" style="width: 300" onChange="actualizarnombredestino(this)">
	<%if IsEmpty(ArrUsuario)=false then
	for j=lbound(ArrUsuario,2) to Ubound(ArrUsuario,2)%>
		<option value="<%=ArrUsuario(0,j)%>"><%=ArrUsuario(1,j)%></option>
	<%next
	end if%>
		<input type="hidden" name="nombredestino">
		</td>
       	<td>
    	<select  name="tiporesponsable">
		<option value="P">Participante</option>
		<%if numcoord=0 then%>
    	<option value="C">Coordinador</option>
    	<%end if%>
	    </select></td>
		<td align="right" style="cursor:hand">
		<input type="submit" value="     " name="cmdGuardar" class="imgGuardar">
		<img ALT="Deshacer cambios" border="0" src="../../../images/salir.gif" onclick="MM_goToURL('self','listaresponsablestarea.asp?idtareaevaluacion=<%=idtareaevaluacion%>&idindicador=<%=idindicador%>&nombreindicador=<%=nombreindicador%>&nombretarea=<%=nombretarea%>&fechatarea=<%=fechatarea%>');return document.MM_returnValue"></td>
      </tr>
  <%end if%>
  </td>
  </tr>
  </table>
</form>
</body>
</html>