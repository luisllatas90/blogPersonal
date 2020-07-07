<!--#include file="../../../../funciones.asp"-->
<!--#include file="../../../../clsautocompletar.asp"-->
<%
'***************************************************************************************
'CV-USAT
'Autor				: Gerardo Chunga
'Fecha de Creación	: 07/08/2005
'Observaciones		: 
'***************************************************************************************
Dim codigo_pes,modalidad,codigo_alu

codigo_pes=request.querystring("codigo_pes")
modalidad=request.querystring("modalidad")
codigo_alu=request.querystring("codigo_alu")

if codigo_pes="" then codigo_pes=session("Codigo_Pes")
if codigo_pes="" then codigo_pes="-2"
bloquear=false

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	if codigo_pes<>"-2" then
		Set rsCiclo= obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
		Set rsCurso= obj.Consultar("ConsultarCursoPlan","FO",4,codigo_pes,0,0)
	end if
	if (modalidad="EXSUF" or modalidad="NORMAL") then
		Set rsDocente= obj.Consultar("ConsultarDocente","FO","MI","","")
	end if
	obj.CerrarConexion
Set objCiclo=nothing
%>
<HTML>
<HEAD>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../private/validarmodalidadmatricula.js"></script>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
</HEAD>
<BODY onload="document.all.txtcodigouniver_alu.focus()">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="60%" class="usattitulo">Registro de Otras modalidades de 
	Matrícula</td>
	<%if codigo_alu="" then%>
    <td width="40%"><%call buscaralumno("matricula/mantenimiento/frmotramatricula.asp","../../",-1)%></td>
    <%end if%>
  </tr>
</table>
<br>
<%if codigo_alu<>"" then%>
<!--#include file="../../fradatos.asp"-->
<form AUTOCOMPLETE="OFF">
<table cellpadding="3" cellspacing="0" style="border-collapse: collapse; " bordercolor="#111111" width="90%" class="contornotabla">   	
        <tr>
	      <td width="100%" colspan="6" class="etabla" style="text-align: left" height="13">
			Detalle de Matrícula</td>
        </tr>
        <tr>
      	<td width="30%">Modalidad del Matrícula</td>
      	<td width="70%" colspan="5"><select name="cboModalidad" onChange="ActualizarListaotramatricula('<%=codigo_alu%>')">
			<option>--Seleccione la modalidad de Matrícula--</option>
			<%if session("codigo_usu")<>1275 and session("codigo_usu")<>1631 and session("codigo_usu")<>1698 and session("codigo_usu")<>1742 and session("codigo_usu")<>1950 and session("codigo_usu")<>1946 and session("codigo_usu")<>1182 and session("codigo_usu")<>1581 then %>
			<option value="EXSUF" <%=SeleccionarItem("cbo",modalidad,"EXSUF")%>>
			Exámen de Suficiencia</option>

			<option value="CNVTR" <%=SeleccionarItem("cbo",modalidad,"CNVTR")%>>
			Convalidación por Traslado</option>			
			<option value="EXUBIC" <%=SeleccionarItem("cbo",modalidad,"EXUBIC")%>>
			Exámen de Ubicación</option>
			<option value="PIMEU" <%=SeleccionarItem("cbo",modalidad,"PIMEU")%>>
			Convalidación PIMEU</option>
			<%end if%>
			<%if session("codigo_tfu")=1 or session("codigo_tfu")=16 or session("codigo_usu")=1275 or session("codigo_usu")=1631 or session("codigo_usu")=1698 or session("codigo_usu")=1002 or session("codigo_usu")=1742 or session("codigo_usu")=1950 or session("codigo_usu")=1946 or session("codigo_usu")=1182 or session("codigo_usu")=1581  then %>
			<option value="NORMAL" <%=SeleccionarItem("cbo",modalidad,"NORMAL")%>>
			Matrícula Normal</option>
			<%end if%>
			<%if session("codigo_usu")=1275 or session("codigo_usu")=1631 or session("codigo_usu")=1698 then%>
			<option value="EXSUF" <%=SeleccionarItem("cbo",modalidad,"EXSUF")%>>
			Exámen de Suficiencia</option>
			<option value="CNVPR" <%=SeleccionarItem("cbo",modalidad,"CNVPR")%>>
			Convalidación por Escuela Pre</option>
			<option value="CNVTR" <%=SeleccionarItem("cbo",modalidad,"CNVTR")%>>
			Convalidación por Traslado</option>
			<option value="EXUBIC" <%=SeleccionarItem("cbo",modalidad,"EXUBIC")%>>
			Exámen de Ubicación</option>
			<%end if%>
			<option value="EXTRA" <%=SeleccionarItem("cbo",modalidad,"EXTRA")%>>
			Examen Extraordinario</option>
			<option value="CNVPLA" <%=SeleccionarItem("cbo",modalidad,"CNVPLA")%>>
			Convalidación por Cambio de Plan</option>		
			<option value="REGMAT" <%=SeleccionarItem("cbo",modalidad,"REGMAT")%>>
			Regularización de Matrícula</option>			
			<option value="SUFESP" <%=SeleccionarItem("cbo",modalidad,"SUFESP")%>>
			Suficiencia Especial</option>
			</select>
      	</td>
    	</tr>
   <%if modalidad<>"" then%>    	
      <tr><td width="30%">Plan de estudios</td>
      <td width="70%" colspan="5">
        <%       
        call planalumnoescuela("ActualizarListaotramatricula('" & codigo_alu & "')",session("codigo_alu"),session("codigo_pes"),codigo_pes,"S",session("codigo_test"))
        %>
		</td>
        </tr>
        <%if codigo_pes<>"-2" then%>
        <tr>
            <td width="30%">Semestre académico en que aprobó</td>
      		<td width="70%" colspan="5">
          	<%        	
         	call llenarlista("cbocodigo_cac","",rsCiclo,"codigo_cac","descripcion_cac","","Seleccione el Semestre académico","","")
			%>
        	</td>
        </tr>
        <%if (modalidad="EXSUF" or modalidad="NORMAL") then%>
        <tr>
      	<td width="30%">Docente</td>
	    <td width="70%" colspan="5">
          <%
			call llenarlista("cbocodigo_per","",rsDocente,"codigo_per","docente","","Seleccione el Docente del Curso","","")
		  %>
        </td>
        </tr>
        <%end if%>
      <tr>
      <td width="30%">Curso</td>
      <td width="70%" colspan="5">
          <%		
				CajaTexto="txtcurso"
			   	AnchoCampos = array(8,15,50,8,8,8)
				ArrEncabezados=array("ID","Código","Nombre del Curso","Ciclo","Créd.","Hrs.")
				CampoTexto  = "nombre_cur"
			   	CampoPK     = "codigo_cur"
			   	Autocompletar= "nombre_cur;codigo_cur"
			   	AnchoLista  = 540
			   	Call CrearListaDesplegable("../../../../",rsCurso,"document.all.smtGuardar")
			
			if (rsCurso.BOF and rsCurso.EOF) then
				Bloquear=true
			end if
		  %>
        </td>
        </tr>
      <%if bloquear=false then
	      if modalidad="NORMAL" then%>
      <tr>
      	<td width="30%">Grupo Horario</td>
	      <td width="13%"><input class="cajas" type="text" id="txtGrupohor_cup" name="txtGrupohor_cup" size="8" maxlength="15"></td>
	      <td width="13%">Asistencias</td>
	      <td width="8%"><input class="cajas" type="text" onkeypress="validarnumero()" id="txtAsistencias" name="txtAsistencias" size="3" maxlength="3"></td>
      	  <td width="14%">Inasistencias</td>
      	  <td width="14%"><input class="cajas" type="text" onkeypress="validarnumero()" id="txtInasistencias" name="txtInasistencias" size="3" maxlength="3"></td>
        </tr>
	   <%end if%>        
       <tr>
          <td width="30%">Nota</td>
      	  <td width="66%" colspan="5"> 
          <input class="cajas" type="text" onkeypress="validarnumero()" onkeyup="validarnota(this);if(event.keyCode==13){validarotramatricula('<%=modalidad%>','<%=codigo_alu%>')}" id="txtNota" name="txtNota" size="2" maxlength="2"></td>
        </tr>
      	<tr> 
      <td width="30%">&nbsp;</td>
      <td width="70%" colspan="5">
      <input type="button" value="Guardar" id="smtGuardar" name="smtGuardar" class="usatguardar" onclick="validarotramatricula('<%=modalidad%>','<%=codigo_alu%>')">
      <input OnClick="location.href='about:blank'" type="button" value="Cancelar" name="cmdCancelar" class="usatsalir">
      </td>
    </tr>
    	   <%end if
    	end if
   end if%>
  </table>
</form>
<%end if

Set rsPlan=nothing
Set rsCiclo=nothing
Set rsDocente=nothing
Set rsCurso=nothing
%>
</BODY>
</HTML>