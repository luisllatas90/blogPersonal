<!--#include file="../../../../funciones.asp"-->
<%
nivel=0
codigo_per=session("codigo_Usu")
nombre_per=session("nombre_usu")
codigo_cac=Request.querystring("codigo_cac")
if codigo_per="" then codigo_per=0
if codigo_cac="" then codigo_cac=session("codigo_cac")	

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCiclo=obj.Consultar("ConsultarCicloAcademico","FO","TO","")
		Set rsCarga=Obj.Consultar("ConsultarCargaAcademica","FO",12,codigo_cac,codigo_per)

		if Not(rsCarga.BOF and rsCarga.EOF)=true then
			HayReg=true
		end if
	obj.CerrarConexion
Set obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/tooltip.js"></script>
<script type="text/javascript" language="JavaScript" src="../private/validarnotas.js"></script>
</head>
<body>
<p class="usattitulo">Mis cursos</p>
<table border="0" cellpadding="3" cellspacing="0" width="100%">
  <tr>
    <td width="15%">Ciclo Académico</td>
    <td width="75%">
    <%call llenarlista("cbocodigo_cac","actualizarlista('miscursos.asp?codigo_cac=' + this.value)",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>
	</td>
  </tr>
  <tr>
    <td width="15%">Docente</td>
    <td width="75%"><%=session("Nombre_Usu")%>&nbsp;</td>
  </tr>
  <tr>
  	<td width="15%">&nbsp;</td>
	<td width="75%" >
	<%if HayReg=true then%>
	
  	<input onClick="AbrirRegistro('A','<%=codigo_per%>','<%=nombre_per%>',<%=nivel%>)" name="cmdAbrir" type="button" value="    Nota Final" class="modificar2" disabled="true" tooltip="Permite visualizar el registro de notas de la asignatura seleccionada" />
	
	<input onClick="AbrirRegistro('I','<%=codigo_per%>','<%=nombre_per%>',<%=nivel%>)" name="cmdAbrir2" type="button" value="     Investigaciones " class="marcado2" disabled="true" tooltip="Permite visualizar los trabajos de investigacion de los estudiantes." style="width:100" /> 
	
  	<input onClick="AbrirRegistro('D','<%=codigo_per%>','<%=nombre_per%>',<%=nivel%>)" name="cmdDescargar" type="button" value="    Reg. Notas" class="word2" disabled="true" tooltip="Permite descargar un archivo de word, con el registro auxiliar de la asignatura seleccionada" style="width:90" />
	
  	<input onClick="AbrirRegistro('B','<%=codigo_per%>','<%=nombre_per%>',<%=nivel%>)" name="cmdBitacora" type="button" value="  Bitacora" class="buscar2" disabled="true" tooltip="Permite visualizar la bitácora de los sucesos realizados en el registro de notas de la asignatura seleccionada" />
	
	<input onClick="AbrirRegistro('S','<%=codigo_per%>','<%=nombre_per%>',<%=nivel%>)" name="cmdSylabus" type="button" value="    Asistencia y Notas" class="horario2" tooltip="Permite Activar modulo de Asistencia y Notas parciales" style="width:120; display:none"  />
	
	<input onClick="AbrirRegistro('X','<%=codigo_per%>','<%=nombre_per%>',<%=nivel%>)" name="cmdAsistencia" type="button" value="     Reg. Auxiliar" class="excel2" disabled="true" tooltip="Permite descargar un archivo de Excel, con el registro auxiliar de la asignatura seleccionada" style="width:90"  />
		
  	<%end if%>
	</td>
  </tr>
</table>
<%
  	if HayReg="" then%>
  		<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No 
		se han registro asignaturas a su cargo, en el ciclo académico seleccionado.</h5>
  	<%else
%>
<br>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="gray" width="100%">
	  <tr class="etabla">
	    <th width="5%">#</th>
	    <th width="10%">Código</th>
	    <th width="30%">Descripción</th>
	    <th width="10%">Grupo horario</th>
	    <th width="5%">Ciclo</th>
	    <th width="20%">Escuela Profesional</th>
	    <!--<th width="5%">Matric.</th>
	    <th width="5%">Ret.</th>-->
	    <th width="10%">Llenado de Notas</th>
	  </tr>
	  <%
	  Do while not rsCarga.EOF
	  	i=i+1
	  %>
	  <tr class="Sel" Typ="Sel" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" codigo_cup="<%=rsCarga("codigo_cup")%>" estadonota_cup="<%=rsCarga("estadonota_cup")%>" codigo_aut="<%=rsCarga("codigo_aut")%>" codigo_cpf="<%=rsCarga("codigo_cpf")%>" onClick="HabilitarRegistro(<%=nivel%>)">
	   <td width="5%" align="center"><%=i%></td>
	    <td width="10%"><%=rsCarga("identificador_Cur")%></td>
	    <td width="30%"><%=rsCarga("nombre_cur")%></td>
	    <td width="10%"><%=rsCarga("grupoHor_Cup")%></td>
	    <td width="5%"><%=ConvRomano(rsCarga("ciclo_cur"))%></td>
	    <td width="20%"><%=rsCarga("nombre_cpf")%></td>
	    <!--<td width="5%"><%=rsCarga("matriculados")%></td>
	    <td width="5%"><%=rsCarga("retirados")%></td>-->
	    <td width="10%" class="etiqueta">
		<%if rsCarga("codigo_aut")=0 and rsCarga("estadonota_cup")<>"P" then%>
			Realizado
		<%elseif rsCarga("codigo_aut")>0 and rsCarga("estadonota_cup")<>"P" then%>
			Pendiente con autorización
			<%else%>
			Pendiente
		<%end if%>
	    </td>
	  </tr>
	  	<%
	  	rsCarga.movenext
	  Loop
	  %>
	</table>
	<%
	end if
Set rsCarga=nothing
%>
<p align="right" id="mensaje" class="rojo">&nbsp;</p>
</body>
</html>
