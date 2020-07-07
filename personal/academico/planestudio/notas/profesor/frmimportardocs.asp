<!--#include file="../../../../funciones.asp"-->
<%
codigo_per=session("codigo_usu")
idcursovirtualdestino=request.querystring("idcursovirtualdestino")
codigo_cac=request.querystring("codigo_cac")
curso=request.querystring("curso")

if codigo_cac="" then codigo_cac="-1"

	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion

		Set rsCiclo=Obj.Consultar("ConsultarCicloAcademico","FO","TO",0)	
		if codigo_per<>"" and codigo_cac<>"-1" then
			Set rsCarga=Obj.Consultar("ConsultarCargaAcademica","FO","14",codigo_cac,codigo_per)
			
			if Not(rsCarga.BOF and rsCarga.EOF) then
				HayReg=true
			end if
		end if
	Obj.CerrarConexion
	Set Obj=nothing
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<title>Importar documentos de cursos virtuales anteriores</title>
<script type="text/javascript" language="javascript">
function ConfirmarCurso()
{
	var alerta=""
	alerta="¿Está completamente seguro que desea copiar todos los documentos del curso seleccionado?"

	if (txtelegido.value>0){
		if (confirm(alerta)==true){
			mensaje.innerHTML="<b>&nbsp;Espere un momento por favor...</b>"
			location.href="procesaraulavirtual.asp?accion=copiardocumentosanteriores&idcursovirtualdestino=<%=idcursovirtualdestino%>&idcursovirtualorigen=" + txtelegido.value
		}
	}
}

function BuscarCursosVirtuales()
{
	location.href="frmimportardocs.asp?codigo_cac=" + cbocodigo_cac.value + "&curso=<%=curso%>&idcursovirtualdestino=<%=idcursovirtualdestino%>"
}

function SeleccionarCursoDestino(id)
{
	if (eval(event.srcElement.parentElement.cells[7].innerText)>0){
		SeleccionarFila()
		txtelegido.value=id	
		cmdGuardar.disabled=false
	}
	else{
		alert("No se puede seleccionar una asignatura que no haya publicado documentos")
	}
}

</script>
</head>

<body bgcolor="#F0F0F0">
<input name="txtelegido" type="hidden" value="0" />
<fieldset name="fraBuscar">
<legend class="etiqueta">Seleccione la asignatura de dónde se 
copiarán los documentos, según el ciclo académico</legend>
<table width="100%">
	<tr>
		<td width="25%" class="etiqueta">Profesor</td>
		<td>: <%=session("nombre_usu")%>&nbsp;</td>
	</tr>
	<tr>
		<td width="25%" class="etiqueta">Ciclo Origen</td>
		<td>
			<%call llenarlista("cbocodigo_cac","BuscarCursosVirtuales()",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"Seleccione el ciclo académico anterior","","")%>
		</td>
	</tr>
</table>
<br>
<%if HayReg=true then%>
<table border="1" bgcolor="white" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%">
  <tr class="etabla">
    <td width="5%" height="26">#</td>
    <td width="12%" height="26">Código</td>
    <td width="30%" height="26">Descripción</td>
    <td width="10%" height="26">GH</td>
    <td width="5%" height="26">Ciclo</td>
    <td width="30%" height="26">Escuela Profesional</td>
    <td width="5%" height="26">Matriculados</td>
    <td width="5%" height="26">Documentos<br>
	publicados</td>
  </tr>
  <%
  	Do while not rsCarga.EOF 	
 		
 		if cdbl(idcursovirtualdestino)<>cdbl(rsCarga("idcursovirtual")) then
 			i=i+1
  %>
  <tr class="Sel" Typ="Sel" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onclick="SeleccionarCursoDestino('<%=rsCarga("idcursovirtual")%>')">
    <td width="5%" align="center"  ><%=i%>&nbsp;</td>
    <td width="12%"><%=rsCarga("identificador_Cur")%>&nbsp;</td>
    <td width="30%"><%=rsCarga("nombre_Cur")%>&nbsp;</td>
    <td width="10%" align="center"><%=rsCarga("grupoHor_Cup")%>&nbsp;</td>
    <td width="5%" align="center"><%=ConvRomano(rsCarga("ciclo_cur"))%>&nbsp;</td>
    <td width="30%" class="piepagina"><%=rsCarga("nombre_cpf")%>(<%=rsCarga("descripcion_pes")%>)&nbsp;</td>
    <td width="10%" align="center"><%=rsCarga("matriculados")%>&nbsp;</td>
    <td width="10%" align="center"><%=rsCarga("docs")%></td>
  </tr>
  	<%
	  	end if
  	rsCarga.movenext
  loop
  Set rsCarga=nothing%>
</table>
</fieldset>
<br>
<fieldset name="fraDestino">
<legend class="etiqueta">Copiar todos los documentos a la asignatura:</legend>
<br>
<table width="100%">
	<tr>
		<td width="25%" class="etiqueta">Ciclo Destino</td>
		<td>: <%=session("descripcion_cac")%></td>
	</tr>
	<tr>
		<td width="25%" class="etiqueta">Asignatura Destino</td>
		<td class="rojo">: <%=curso%>&nbsp;</td>
	</tr>
	<tr>
		<td width="100%" colspan="2" align="right" id="mensaje" class="rojo">
		<input type="button" class="guardar_prp"  value="     Iniciar copiado..."  disabled=true onClick="ConfirmarCurso()" name="cmdGuardar">
		</td>
	</tr>
</table>
</fieldset>
<%elseif codigo_cac<>"-1" then%>
<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado asignaturas habilitadas en el aula virtual, para el ciclo seleccionado</h5>
<%end if%>
</body>
</html>