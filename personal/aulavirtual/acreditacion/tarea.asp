<!--#include file="clsacreditacion.asp"-->
<%
if session("codigo_usu")="" then response.redirect "../tiempofinalizado.asp"

Dim Modalidad
Dim idindicador
Dim idtareaevaluacion
Dim fechareg

Modalidad=Request("modalidad")
idindicador=request.querystring("idindicador")
nombreindicador=request.querystring("nombreindicador")
idvariable=request.querystring("idvariable")
idseccion=request.querystring("idseccion")

idtareaevaluacion=Request("idtareaevaluacion")
titulotarea=Request("titulotarea")
tipoasignacion=Request("tipoasignacion")
fechainicio=Request("diainicio") & "/" & Request("mesinicio") & "/" & year(date)
fechafin=Request("diafin") & "/" & Request("mesfin") & "/" & year(date)
idevaluacionindicador=Request("idevaluacionindicador")

if Modalidad="" then
	if idvariable<>"" and idseccion<>"" then
		session("idvariable")=idvariable
		session("idseccion")=idseccion
	end if
end if

response.Buffer=true

function cargarlistausuarios(id,nombretarea,fechatarea)
	response.write "<tr id='tbltarea" & id & "' style='display:none'>"
	response.write "<td colspan=""7"" align=""right"">"
	%>
	<img border="0" src="../../../images/beforelastnode.gif" align="top">
	<iframe name="fratarea<%=id%>" src="listaresponsablestarea.asp?idtareaevaluacion=<%=id%>&nombretarea=<%=nombretarea%>&fechatarea=<%=fechatarea%>" style="border:1px solid #C0C0C0; width:98%" border="0" frameborder="0" height="100">
El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe>
	<%response.write "</tr></td>"
end function

dim tarea
	Set tarea=new clsacreditacion
		with tarea
			If Len(Request.form("cmdGuardar"))>0 then
				Select case Modalidad
					case "AgregarNuevo"
						Call tarea.Agregartareaevaluacion(idindicador,titulotarea,session("idacreditacion"),fechainicio,fechafin,tipoasignacion,session("codigo_usu"))
					case "Modificar"
						Call tarea.Modificartareaevaluacion(idtareaevaluacion,titulotarea,fechainicio,fechafin,tipoasignacion,session("codigo_usu"))
				end Select
				Modalidad=""
			Else
				If Modalidad="Eliminar" then
					Call tarea.Eliminartareaevaluacion(idtareaevaluacion,idevaluacionindicador)
				end If
			End if
	
			ArrDatos=tarea.ConsultarEvaluacionModeloAcreditacion("5",idindicador,session("idacreditacion"),0)
		end with
	Set tarea=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Tareas asignadas para la evaluacíon del Indicador</title>
<script language="JavaScript" src="private/validaracreditacion.js"></script>
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="Javascript">
	function actualizarmodeloevaluado()
	{
		window.opener.location.href="modeloacreditacion.asp?idindicadorE=<%=idindicador%>&idvariableE=<%=session("idvariable")%>&idseccionE=<%=session("idseccion")%>"
		window.close()
	}	
</script>
</head>
<body topmargin="0" leftmargin="0">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr class="barraherramientas">
    <td>&nbsp;
<input type="button" onclick="MM_goToURL('self','tarea.asp?idindicador=<%=idindicador%>&Modalidad=AgregarNuevo&nombreindicador=<%=nombreindicador%>');return document.MM_returnValue" value="   Agregar" name="cmdagregar" class="agregar3"><input type="button" onClick="actualizarmodeloevaluado()" value="Cerrar" name="cmdcerrar" class="cerrar3"></td>
  </tr>
  <tr>
    <td class="e1" align="center" valign="bottom">Indicador: <%=nombreindicador%>&nbsp;</td>
  </tr>
</table>
<br>
<form name="frmtarea" method="post" onSubmit="return validartareaevaluacion(this)" ACTION="tarea.asp?Modalidad=<%=Modalidad%>&idindicador=<%=idindicador%>&nombreindicador=<%=nombreindicador%>">
<table width="100%" border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" id="tblMenu">
  <tr class="etabla2"> 
      <td width="5%" colspan="2">&nbsp;</td>
      <td width="25%">Descripción de la tarea</td>
      <td width="10%">Tipo</td>
      <td>Fecha Inicio</td>
      <td>Fecha Fin</td>
      <td>&nbsp;</td>
	</tr>
<%if IsEmpty(ArrDatos)=false then
	num=0								  
	for i=Lbound(Arrdatos,2) to Ubound(Arrdatos,2)
		num=num+1%>			  
    <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onClick="MostrarTabla(tbltarea<%=Arrdatos(1,I)%>,'../images/',imgtarea<%=Arrdatos(1,I)%>)">
    <td width="2%" align="right">
    <img id="imgtarea<%=Arrdatos(1,I)%>" border="0" src="../../../images/mas.gif"></td>
    <td width="3%"><%=num%>.</td>
    <td width="15%">
    <%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then%>
	    <input  maxLength="100" size="30" name="titulotarea" class="cajas" value="<%=ArrDatos(2,I)%>"></td>
	<%else
		response.write ArrDatos(2,I)
	end if%>
    <td width="10%">
    <%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then%>
    	<select  name="tipoasignacion">
		<option value="I" <%=Seleccionar(ArrDatos(3,I),"I")%>>Individual</option>
    	<option value="G" <%=Seleccionar(ArrDatos(3,I),"G")%>>Grupal</option>
	    </select>
	<%else
		response.write iif(ArrDatos(3,I)="I","Individual","Grupal")
	end if%> 
	</td>
    <td>
    <%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then
    ProcesarFechas Arrdatos(4,i),now%>
	<select  name="diainicio">
	<%for d=1 to 31%>
		<option value="<%=iif(len(d)=1,"0" & d,d)%>" <%=seleccionar(Diai,d)%>><%=iif(len(d)=1,"0" & d,d)%></option>
	<%next%>
    </select> <select  name="mesinicio">
	<%for m=1 to 12%>
		<option value="<%=iif(len(m)=1,"0" & m,m)%>" <%=seleccionar(Mesi,m)%>><%=left(MonthName(m, False),3)%></option>
  	<%next%>
    </select> <%=year(date)%>
	<input name="idtareaevaluacion" type="hidden" id="idtareaevaluacion" value="<%=Arrdatos(1,I)%>">
	<%else%>
        <%=Arrdatos(4,I)%>
    <%end if%></td>
    <td>
    <%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then
    ProcesarFechas Arrdatos(5,i),now%>
	<select  name="diafin">
	<%for d=1 to 31%>
		<option value="<%=iif(len(d)=1,"0" & d,d)%>" <%=seleccionar(Diai,d)%>><%=iif(len(d)=1,"0" & d,d)%></option>
	<%next%>
    </select> <select  name="mesfin">
	<%for m=1 to 12%>
		<option value="<%=iif(len(m)=1,"0" & m,m)%>" <%=seleccionar(Mesi,m)%>><%=left(MonthName(m, False),3)%></option>
  	<%next%>
    </select> <%=year(date)%>
	<%else%>
        <%=Arrdatos(5,I)%>
    <%end if%></td>
	<td align="right" style="cursor:hand">
	<%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then%>
  		<input type="submit" value="    " name="cmdGuardar" class="imgGuardar">
  		<img border="0" src="../../../images/salir.gif" onclick="MM_goToURL('self','tarea.asp?idindicador=<%=idindicador%>&nombreindicador=<%=nombreindicador%>&idtareaevaluacion=<%=Arrdatos(1,I)%>');return document.MM_returnValue" >
	<%else%>
  		<img ALT="Modificar tarea" border="0" src="../../../images/editar.gif" onclick="MM_goToURL('self','tarea.asp?idindicador=<%=idindicador%>&nombreindicador=<%=nombreindicador%>&idtareaevaluacion=<%=Arrdatos(1,I)%>&modalidad=Modificar&recordid=<%=num%>');return document.MM_returnValue" width="18" height="13" >
  		<img ALT="Eliminar tarea" border="0" src="../../../images/eliminar.gif" onclick="ConfirmarEliminar('¿Está seguro que desea eliminar la tarea <%=Arrdatos(2,I)%>?','tarea.asp?idtareaevaluacion=<%=Arrdatos(1,I)%>&idindicador=<%=idindicador%>&nombreindicador=<%=nombreindicador%>&idevaluacionindicador=<%=Arrdatos(0,I)%>');" >
	<%end if%>
	</td>
	</tr>
		<%cargarlistausuarios Arrdatos(1,I),ArrDatos(2,I),Arrdatos(4,i) & " hasta " & Arrdatos(5,i)
	Next
 end if
 if trim(modalidad)="AgregarNuevo" then
 	ProcesarFechas now,now%>
  	<tr>
       <td width="2%">&nbsp;</td>
       <td width="3%"><%=num+1%>.</td>
       <td width="15%"><input  maxLength="100" size="30" name="titulotarea" class="cajas"></td>
       <td width="10%">
    <select  name="tipoasignacion">
	<option value="I">Individual</option>
    <option value="G" selected>Grupal</option>
    </select></td>
       <td>
       <select  name="diainicio">
	<%for d=1 to 31%>
		<option value="<%=iif(len(d)=1,"0" & d,d)%>" <%=seleccionar(Diai,d)%>><%=iif(len(d)=1,"0" & d,d)%></option>
	<%next%>
    </select> <select  name="mesinicio">
	<%for m=1 to 12%>
		<option value="<%=iif(len(m)=1,"0" & m,m)%>" <%=seleccionar(Mesi,m)%>><%=left(MonthName(m, False),3)%></option>
  	<%next%>
    </select> <%=year(date)%></td>
       <td>
       <select  name="diafin">
	<%for d=1 to 31%>
		<option value="<%=iif(len(d)=1,"0" & d,d)%>" <%=seleccionar(Diai,d)%>><%=iif(len(d)=1,"0" & d,d)%></option>
	<%next%>
    </select> <select  name="mesfin">
	<%for m=1 to 12%>
		<option value="<%=iif(len(m)=1,"0" & m,m)%>" <%=seleccionar(Mesi,m)%>><%=left(MonthName(m, False),3)%></option>
  	<%next%>
    </select> <%=year(date)%> </td>
		<td align="right" style="cursor:hand">
		<input type="submit" value="     " name="cmdGuardar" class="imgGuardar">
        <img ALT="Deshacer cambios en la tarea" border="0" src="../../../images/salir.gif" onclick="MM_goToURL('self','tarea.asp?idindicador=<%=idindicador%>&nombreindicador=<%=nombreindicador%>');return document.MM_returnValue" ></td>
      </tr>
  <%end if%>
  </td>
  </tr>
  </table>
</form>
</body>
</html>