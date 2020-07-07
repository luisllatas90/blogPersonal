<!--#include file="../../../../funciones.asp"-->
<%
if(session("codigo_usu") = "") then
    Response.Redirect("../../../../sinacceso.html")
end if

'***************************************************************************************
'CV-USAT
'Autor			: Gerardo Chunga
'Fecha de Creación	: 01/03/2006
'Observaciones		: 
'***************************************************************************************
Dim codigo_pes,modalidad,codigo_alu

codigo_pes=request.querystring("codigo_pes")
modalidad=request.querystring("modalidad")
codigo_alu=request.querystring("codigo_alu")
modulo=request.querystring("mod")

if codigo_pes="" then codigo_pes=session("Codigo_Pes")
if codigo_pes="" then codigo_pes="-2"
bloquear=false
%>
<HTML>
<HEAD>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../private/validarmodalidadmatricula.js"></script>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
</HEAD>
<BODY onload="document.all.txtcodigouniver_alu.focus()">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr>
    <td width="60%" class="usattitulo">Registro de Traslados de Estudiantes</td>
	<%if codigo_alu="" then%>
    <td width="40%"><%call buscaralumno("matricula/mantenimiento/frmtraslados.asp","../../",modulo)%></td>
    <%end if%>
  </tr>
</table>
<br>
<%if codigo_alu<>"" then%>
<!--#include file="../../fradatos.asp"-->
<form AUTOCOMPLETE="OFF">
<table cellpadding="3" cellspacing="0" style="border-collapse: collapse; " bordercolor="#111111" width="89%" class="contornotabla">   	
        <tr>
	      <td width="100%" colspan="2" class="etabla" style="text-align: left" height="13">
          Detalle de Traslado</td>
        </tr>
        <tr>
      	<td width="36%">Tipo de Traslado</td>
      	<td width="69%"><select name="cboModalidad">
			<option value="TI" <%=SeleccionarItem("cbo",modalidad,"TI")%>>Traslado Interno</option>
			<!--<option value="TE" <%=SeleccionarItem("cbo",modalidad,"TE")%>>Traslado Externo</option>-->
			</select>
      	</td>
    	</tr>
      <tr><td width="36%">Carrera Profesional de traslado</td>
      <td width="69%">
        <%
        call planalumnoescuela("",session("codigo_alu"),session("codigo_pes"),codigo_pes,"S",session("codigo_test"))
        %>
		</td>
        </tr>
      	<tr> 
      <td width="36%">&nbsp;</td>
      <td width="69%">
      <input type="button" value="Guardar" id="smtGuardar" name="smtGuardar" class="usatguardar" onclick="ValidarTraslados('<%=modalidad%>','<%=codigo_alu%>')">
      <input OnClick="location.href='about:blank'" type="button" value="Cancelar" name="cmdCancelar" class="usatsalir">
      </td>
    </tr>
  </table>
</form>
<%end if

Set rsPlan=nothing
%>
</BODY>
</HTML>