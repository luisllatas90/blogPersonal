<!--#include file="../../../../funciones.asp" -->
<%
if session("codigo_tfu") = "" then
    Response.Redirect("../../../../../sinacceso.html")
end if

If int(session("codigo_tfu"))=16 or int(session("codigo_tfu"))=1 then
	nivel=1
else
	nivel=0
end if

resultado=false
codigo_per= Request.querystring("codigo_per")
codigo_cac=Request.querystring("codigo_cac")
nombre_per=Request.querystring("nombre_per")

if codigo_per="" then codigo_per="-2"
if codigo_cac="" then codigo_cac=session("codigo_cac")

if (codigo_per<>"-2" and codigo_cac<>"") then
	resultado=true
end if

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCiclo=obj.Consultar("ConsultarCicloAcademico","FO","TO","")
		Set rsDocente=obj.Consultar("ConsultarDocente","FO","CL",codigo_cac,"")

		if resultado=true then
			Set rsCarga=Obj.Consultar("ConsultarCargaAcademica","FO",12,codigo_cac,codigo_per)
			
			if Not(rsCarga.BOF and rsCarga.EOF)=true then
				HayReg=true
			end if

		end if
	obj.CerrarConexion
Set obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registro de Notas</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/tooltip.js"></script>
<script type="text/javascript" language="JavaScript" src="../private/validarnotas.js"></script>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="100%" colspan="2" height="30" class="usattitulo">Registro de Notas</td>
  </tr>
  <tr>
    <td width="15%">Ciclo Acad�mico</td>
    <td width="75%">
    <%call llenarlista("cbocodigo_cac","AbrirRegistro('V')",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>
    </td>
  </tr>
  <tr>
    <td width="15%">Profesor</td>
    <td width="75%">
    <%call llenarlista("cbocodigo_per","AbrirRegistro('V')",rsDocente,"codigo_per","docente",codigo_per,"Seleccione el docente","","")%>
	</td>
  </tr>
  <tr>
  	<td width="15%">&nbsp;</td>
	<td width="75%" >
	<%if HayReg=true  then%>
  	<input onClick="AbrirRegistro('A','<%=codigo_per%>','<%=nombre_per%>','<%=nivel%>')" name="cmdAbrir" type="button" value="   Abrir Reg." class="modificar2" disabled="true" tooltip="Permite visualizar el registro de notas de la asignatura seleccionada">
  	<input onClick="AbrirRegistro('D','<%=codigo_per%>','<%=nombre_per%>',<%=nivel%>)" name="cmdDescargar" type="button" value="    Reg. Notas" class="word2" disabled="true" tooltip="Permite descargar un archivo de word, con el registro auxiliar de la asignatura seleccionada" style="width:90" />
  	<input onClick="AbrirRegistro('B','<%=codigo_per%>','<%=nombre_per%>','<%=nivel%>')" name="cmdBitacora" type="button" value="  Bitacora" class="buscar2" disabled="true" tooltip="Permite visualizar la bit�cora de los sucesos realizados en el registro de notas de la asignatura seleccionada">
  	<input onClick="AbrirAutorizacionNota('A')" name="cmdAutorizar" type="button" value="Autorizar llenado de notas" class="marcado2" style="width:160px">
  	<input onClick="AbrirAutorizacionNota('Q')" name="cmdQuitar" type="button" value="Quitar autorizaci�n de llenado de notas" class="eliminar2" style="width:220px">
    <input onClick="AbrirRegistro('X','<%=codigo_per%>','<%=nombre_per%>',<%=nivel%>)" name="cmdAsistencia" type="button" value="     Reg. Auxiliar" class="excel2" disabled="true" tooltip="Permite descargar un archivo de Excel, con el registro auxiliar de la asignatura seleccionada" style="width:90"  />

  	<%end if%>
	</td>
  </tr>
  </table>
<br>
<%
  	if HayReg="" then%>
  		<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No 
		se han registro asignaturas a su cargo, en el ciclo acad�mico seleccionado.</h5>
  	<%else
%>
<br>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="gray" width="100%">
	  <tr class="etabla">
	    <th width="5%">#</th>
	    <th width="10%">C�digo</th>
	    <th width="30%">Descripci�n</th>
	    <th width="10%">Grupo horario</th>
	    <th width="5%">Ciclo</th>
	    <th width="20%">Escuela Profesional</th>
	    <th width="5%">Matric.</th>
	    <th width="5%">Ret.</th>
	    <th width="10%">Llenado de Notas</th>
	  </tr>
	  <%
	  Do while not rsCarga.EOF
	  	i=i+1
	  	clase=""
	  	if rsCarga("codigo_fun")=1 then
	  		clase="class=""azul"" "
	  	end if
	  %>
	  <tr class="Sel" Typ="Sel" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" codigo_cup="<%=rsCarga("codigo_cup")%>" estadonota_cup="<%=rsCarga("estadonota_cup")%>" codigo_aut="<%=rsCarga("codigo_aut")%>" onClick="HabilitarRegistro(<%=nivel%>)">
	    <td width="5%" align="center" <%=clase%>><%=i%>&nbsp;</td>
	    <td width="10%" <%=clase%>><%=rsCarga("identificador_Cur")%>&nbsp;</td>
	    <td width="30%" <%=clase%>><%=rsCarga("nombre_cur")%>&nbsp;</td>
	    <td width="10%" <%=clase%>><%=rsCarga("grupoHor_Cup")%>&nbsp;</td>
	    <td width="5%" <%=clase%>><%=ConvRomano(rsCarga("ciclo_cur"))%>&nbsp;</td>
	    <td width="20%" <%=clase%>><%=rsCarga("nombre_cpf")%>&nbsp;</td>
	    <td width="5%" <%=clase%>><%=rsCarga("matriculados")%>&nbsp;</td>
	    <td width="5%" <%=clase%>><%=rsCarga("retirados")%>&nbsp;</td>
	    <td width="10%" class="etiqueta">
		<%if rsCarga("codigo_aut")=0 and rsCarga("estadonota_cup")<>"P" then%>
			Realizado
		<%elseif rsCarga("codigo_aut")>0 and rsCarga("estadonota_cup")<>"P" then%>
			Pendiente con autorizaci�n
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