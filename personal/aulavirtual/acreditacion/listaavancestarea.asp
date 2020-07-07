<!--#include file="clsacreditacion.asp"-->
<%
if session("codigo_usu")="" then response.redirect "../tiempofinalizado.asp"

Dim Modalidad,idtareaevaluacion,autorizado,pjeavance,maxpje,nombrearchivoavance

Modalidad=Request("modalidad")
idtareaevaluacion=request.querystring("idtareaevaluacion")
autorizado=request.querystring("autorizado")
idevaluacionindicador=request.querystring("idevaluacionindicador")
idvariable=request.querystring("idvariable")
idseccion=request.querystring("idseccion")

idavancetarea=Request("idavancetarea")
nombrearchivoavance=Request("nombrearchivoavance")
tituloarchivoavance=Request("tituloarchivoavance")
pjeavance=Request("pjeavance")
maxpje=0

response.Buffer=true

dim responsable
	Set responsable=new clsacreditacion
		with responsable
			If Modalidad="Eliminar" then
				Call .Eliminaravancetarea(idavancetarea,idtareaevaluacion,idevaluacionindicador,session("idacreditacion"),idvariable,idseccion,nombrearchivoavance)
			End If

			ArrDatos=.ConsultarEvaluacionModeloAcreditacion("9",idtareaevaluacion,session("idacreditacion"),0)
			If IsEmpty(ArrDatos)=false then
				maxpje=Arrdatos(6,0)
			end if
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
<script language="JavaScript" src="private/validaracreditacion.js"></script>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">

</head>
<body topmargin="0" leftmargin="0">
<form name="frmresponsable" method="post" ACTION="listaavancestarea.asp?Modalidad=<%=Modalidad%>&idavancetarea=<%=idavancetarea%>&idtareaevaluacion=<%=idtareaevaluacion%>&autorizado=<%=autorizado%>&idevaluacionindicador=<%=idevaluacionindicador%>&idvariable=<%=idvariable%>&idseccion=<%=idseccion%>">
<table width="100%" border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111">
  <tr class="barraherramientas"> 
      <td width="15%" style="text-align: left">Fecha de Registro</td>
      <td width="40%" style="text-align: left">Descripción del archivo</td>
      <td width="10%">Avance</td>
      <td width="15%">Observaciones</td>
      <td width="15%">
      <%if autorizado="C" then%>
      <input type="button" onclick="AbrirAvance('<%=idtareaevaluacion%>','<%=autorizado%>','<%=maxpje%>','<%=idevaluacionindicador%>','<%=idvariable%>','<%=idseccion%>')" value="  Realizar tarea" name="cmdRealizar" class="agregar3" style="width: 75%">
      <%end if%></td>
	</tr>
<%if IsEmpty(ArrDatos)=false then					  
	for i=Lbound(Arrdatos,2) to Ubound(Arrdatos,2)%>
    <tr>
    <td width="15%"><%=Arrdatos(1,I)%></td>
    <td width="40%">
    <!--
        '---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '---------------------------------------------------------------------------------------------------------------
    -->
	<a TARGET="_blank" href="https://intranet.usat.edu.pe/dpdu/acreditacion/archivos/<%=Arrdatos(2,I)%>"><%=Arrdatos(3,I)%></a>
    </td>
    <td width="10%"><%=Arrdatos(4,I)%></td>
    <td width="15%">&nbsp;</td>
	<td align="right" style="cursor:hand" width="15%">
	<%if session("codigo_usu")=Arrdatos(5,I) then%>
		<input type="button" onclick="ConfirmarEliminar('¿Está seguro que desea eliminar el documento registrado?','listaavancestarea.asp?idavancetarea=<%=Arrdatos(0,I)%>&nombrearchivoavance=<%=Arrdatos(2,I)%>&idtareaevaluacion=<%=idtareaevaluacion%>&autorizado=<%=autorizado%>&idevaluacionindicador=<%=idevaluacionindicador%>&idvariable=<%=idvariable%>&idseccion=<%=idseccion%>')" value="Eliminar" name="cmdEliminar" class="eliminar2" style="width: 50%">
	<%end if%>
	</td>
	</tr>
	<%Next
 end if%>
 </table>
</form>
</body>
</html>