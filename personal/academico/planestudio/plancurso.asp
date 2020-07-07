<%@LANGUAGE="VBSCRIPT" %>
<%
'********************************************************************
'Creado el 11/06/05
'********************************************************************
%>
<html>
<head>
<title>Registrar Plancurso</title>
<script language="JavaScript" src="../../../private/ValidarPlancurso.js"></script>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
</head>
<body>
<%
dim codcur
codcur=request.QueryString("Codigocurso")
nomCur=request.QueryString("nomCurso")
Dim identificador
Dim departamento
identificador=""
departamento=""
'*******************Consultar curso*******************************
if codcur <> "" then 
Dim objcursoPlan
Dim rscursoPlan
Set objcursoPlan=Server.CreateObject("PryUSAT.clsDatCurso")
Set rscursoPlan=Server.CreateObject("ADODB.Recordset")
Set rscursoPlan= objcursoPlan.ConsultarCurso("RS","CO",codcur,"")
identificador=rscursoPlan("identificador_Cur")
departamento=rscursoPlan("nombre_Dac")
set objcursoPlan=Nothing
end if
'*****************************************************************
%>
<form name="frmplancurso" method="post" action="ProcesarPlancurso.asp">
<fieldset style="padding: 2; width:90%">
  <legend class="usatTitulo">Registrar Cursos a un Plan</legend>
<br>
</p>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse"  width="100%" id="tblplanestudio" class="usatFormatoCelda">
    <tr> 
      <td width="193" class="usatceldatitulo">Plan Estudio</td>
      <td colspan="5" class="usatceldatitulo"><%=session("Abreviatura")%> <input type="hidden" name="txtcodplan" value="<%=session("codplan")%>"></td>
    </tr>
    <tr> 
      <td>Codigo del Curso</td>
      <td colspan="5"><%=identificador%></td>
	     
    </tr>
    <tr> 
      <td>Curso</td>
      <td colspan="5"><%=nomCur%> <input type="hidden" name="txtcodcur" value="<%=codcur%>"> 
      </td>
    </tr>
    <tr> 
      <td>Departamento</td>
      <td colspan="5"><%=departamento%></td>
    </tr>
    <tr> 
      <td>Tipo curso</td>
      <td colspan="5"><select size="1" name="cbotipo_cur">
          <option value="0">--Seleccione tipo curso--</option>
          <option value="FG">Formacion General</option>
          <option value="FB">Formacion Basica</option>
          <option value="FE">Formacion Especializada</option>
          <option value="CO">Complementarios</option>
        </select></td>
    </tr>
    <tr> 
      <td height="25">Ciclo </td>
      <td colspan="2" valign="top"><select size "1" name="cbociclo_cur">
          <option value="0">-selecione-</option>
          <option value="1">I</option>
          <option value="2">II</option>
          <option value="3">III</option>
          <option value="4">IV</option>
          <option value="5">V</option>
          <option value="6">VI</option>
          <option value="7">VII</option>
          <option value="8">VIII</option>
          <option value="9">IX</option>
          <option value="10">X</option>
          <option value="11">XI</option>
          <option value="12">XII</option>
        </select></td>
      <td colspan="3">&nbsp;</td>
    </tr>
    <tr> 
      <td height="25">Electivo</td>
      <td colspan="2" valign="top"><input type="radio" name="optelectivo_cur" value="1">
        Si 
        <input name="optelectivo_cur" type="radio" value="0" checked>
        No </td>
      <td colspan="3"><input name="chkpractica_cur" type="checkbox" value="1">
        Practica</td>
    </tr>
    <tr> 
      <td height="28">Horas Teoria</td>
      <td valign="top"> <input name="txthorasteo_cur" type="text" class="usatcajas" size="1" maxlength="1"  onKeyDown="Total()" onKeyUp="ComprobarNumero1()"></td>
      <td>Horas Practicas</td>
      <td width="25"><input name="txthoraspra_cur" type="text" value="0" class="usatcajas" size="1" maxlength="2" onKeyDown="Total()"onKeyUp="ComprobarNumero2()"></td>
      <td width="158">Total Horas</td>
      <td width="298"> <input name="txttotalhoras_cur" type="text" class="usatcajas" size="1" maxlength="2" disabled> 
        <input type="hidden" name="txttotalhorascur"> </td>
    </tr>
    <tr> 
      <td height="28">Creditos</td>
      <td width="14" valign="top"><input name="txtcreditos_cur" type="text" class="usatcajas" size="3" onKeyUp="ComprobarNumero()" maxlength="3"></td>
      <td width="174">Horas Laboratorio</td>
      <td> <input name="txthoraslab_cur2" type="text" value="0" class="usatcajas" size="1" maxlength="1" onKeyDown="Total()"onKeyUp="ComprobarNumero3()"></td>
      <td>Horas Asesoria</td>
      <td> <input name="txthorasase_cur" type="text" class="usatcajas" size="1" maxlength="1"  onKeyDown="Total()" onKeyUp="ComprobarNumero4()" value="0"></td>
    </tr>
    <tr> 
      <td>ssdi_Cur</td>
      <td colspan="5"><input name="txtssdi_cur" type="text" class="usatcajas" size="10" maxlength="10"></td>
    </tr>
    <tr> 
      <td>cfu_Cur </td>
      <td colspan="5"><input name="txtcfu_cur" type="text" class="usatcajas" size="1" maxlength="1" value="0"></td>
    </tr>
    <tr> 
      <td><input type="hidden" name="txtEstado" value="1"></td>
      <td colspan="5"> <input type="submit" value="Guardar" name="smtGuardar" onClick="validarfrmplancurso(frmplancurso)" class="usatGuardar" title="Guardar datos"> 
        <input type="button" value="Cancelar" name="cmdCancelar" onClick=location="plancurso.asp" class="usatsalir" title="cancelar opcion"> 
      </td>
    </tr>
  </table>
</fieldset>
</form>
</body>
</html>