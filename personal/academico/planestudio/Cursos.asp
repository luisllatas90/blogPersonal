<% @ LANGUAGE="VBSCRIPT" TRANSACTION =Required%>
<%
'********************************OBSERVACIONES******************************
'actualizado el 11/06/05
'***************************************************************************
 %>
<HTML>
<HEAD>
<title>curso</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" >
function validarfrmcurso()
{

	if (frmcurso.txtidentificador_cur.value == "")
	{
	alert("Por favor ingrese el campo identificador_Cur");
	frmcurso.txtidentificador_cur.focus();
	return (false);
	}

	if (frmcurso.txtnombre_cur.value == "")
	{
	alert("Por favor ingrese el campo nombre_Cur");
	frmcurso.txtnombre_cur.focus();
	return (false);
	}
}
</script>
</HEAD>
<% 
dim objDpto
dim rsDpto 
set objDpto =Server.CreateObject("PryUSAT.clsDatDepartamentoAcademico")
set rsDpto =Server.CreateObject("ADODB.Recordset")
set rsDpto=objDpto.ConsultarDepartamentoAcademico("RS","TO","")
%>
<BODY>
<form name="frmcurso" method="post"action="procesar.asp" onLoad="document.all.txtidentificador_cur.focus()">
<fieldset style="padding: 2">
    
  <legend  class="usatTitulo"><font size="4">Registro de curso</font></legend>
  <table width="555" border="0">
    <tr> 
      <td width="149" height="32" class="usatLineaCelda">Identificador Curso</td>
      <td width="396" colspan="9"><input name="txtidentificador_cur" type="text" class="usatCajas" size="7" maxlength="7"> 
      </td>
    </tr>
    <tr> 
      <td width="149" height="24"> Nombre Curso</td>
      <td colspan="9"><input name="txtnombre_cur" type="text" class="usatCajas" size="70" maxlength="80"></td>
    </tr>
    <tr> 
      <td width="149" height="27">Complementario</td>
      <td colspan="9"><input type="checkbox" name="chkcomplementario_Cur" value="1"> 
      </td>
    </tr>
    <tr> 
      <td width="149" height="28">Departamento</td>
      <td colspan="9"><select name="cboDpto">
          <option value="0">---Seleccione Departamento---</option>
          <% do while not rsDpto.eof %>
          <option value="<%=rsDpto(0)%>"><%=rsDpto("nombre_Dac")%></option>
          <% rsDpto.movenext
			loop
			%>
        </select> </tr>
    <tr> 
      <td width="149" height="30">&nbsp;</td>
      <td colspan="9"> <input type="submit" value="Guardar" name="smtGuardar" onClick="validarfrmcurso()" class="usatGuardar"> 
        <input type="button" value="Cancelar" name="cmdCancelar" onClick=location="Cursos.asp" class="usatSalir"> 
      </td>
    </tr>
  </table>
  </fieldset>
</form>
</BODY>
</HTML>