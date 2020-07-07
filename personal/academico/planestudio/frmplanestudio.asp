<% @ LANGUAGE="VBSCRIPT" TRANSACTION =Required%>
<%
'*****************************************************************************
'CV-USAT
'Archivo: planestudio.asp
'Autor	: Leyder
'Fecha de Creación	: 26/04/200511:26:24
'Observaciones	: formulario para ingreso de datos de la tabla planestudio
'actualizado el 11/06/05
'*****************************************************************************
%>
<HTML>
<HEAD>
<title>planestudio</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/ValidarPlanEstudio.js"></script>
</HEAD>
<BODY>
<form name="frmplanestudio" method="post"  action="procesarplanest.asp" onSubmit="validarfrmplanestudio(frmplanestudio)">
 <fieldset style="padding: 2; width:90%">
  <legend class="usatTitulo">Registro de Plan de Estudio</legend>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="102%" id="tblplanestudio" class="usatFormatoCelda">
    <!--DWLayoutTable-->
    <tr> 
      <td width="140" height="30">Carrera Profesional</td>
      <td colspan="6"> 
        <%
	  dim Olistar
	  dim Rslistar
		set Olistar=Server.CreateObject("PryUSAT.clsDatCarreraProfesional")
  		set Rslistar=Server.CreateObject("ADODB.Recordset")
   		set Rslistar=Olistar.ConsultarCarreraProfesional("RS","TO","")
   		set Olistar=Nothing %>
        <select size="1" name="cbocodigo_cpf" class="usatCajas">
          <option value="">---Seleccione la Carrera Profesional---</option>
          <%
		'Generamos el menu desplegable
		Do While not Rslistar.eof%>
          <option value="<%=Rslistar(0)%>"><%=Rslistar("nombre_Cpf")%></option>
          <%RSlistar.movenext
		Loop
		%>
        </select> </td>
    </tr>
    <tr class=""> 
      <td height="28"> Descripción</td>
      <td colspan="6" valign="top"><input type="text" name="txtdescripcion_Pes" size="80" maxlength="80" class="usatCajas"></td>
    </tr>
    <tr> 
      <td height="28"> Abreviatura</td>
      <td colspan="2" valign="top"><input type="text" name="txtabreviatura_Pes" size="20" maxlength="20" class="usatCajas"> 
      </td>
      <td colspan="2" valign="top">Ciclo Academico </td>
      <td width="145" valign="top"><input type="text" name="txtcicloacadinicio_pes" size="10" maxlength="10" class="usatCajas"></td>
      <td width="138" valign="top">&nbsp;</td>
    </tr>
    <tr> 
      <td width="140" height="24" valign="top">Tipo Periodo</td>
      <td colspan="2" valign="top"><select size="1" name="cbotipoperiodo_pes" class="usatCajas">
          <option value="">---Seleccione Ciclo Academico---</option>
          <option value="A">Anual</option>
          <option value="S">Semestral</option>
        </select></td>
      <td colspan="2" valign="top">Cantidad Periodo</td>
      <td valign="top"><input class="usatcajas" type="text" name="txtcantidadperiodo_pes" size="3" maxlength="3" onKeyUp="ComprobarNumero(this)"></td>
      <td valign="top">&nbsp;</td>
    </tr>
    <tr> 
      <td width="140" height="28">NumeroDoc</td>
      <td colspan="2" valign="top"><input class="usatcajas" type="text" name="txtnumerodoc_pes" size="35"  maxlength="35"></td>
      <td colspan="2" valign="top">FechaDoc</td>
      <td valign="top"><input class="usatcajas" type="text" name="txtfechadoc_pes" size="16" maxlength="16" onChange="valFecha(This,frmplanestudio)"></td>
      <td>&nbsp;</td>
    </tr>
    <tr> 
      <td width="140" height="28">Total Creditos Obl</td>
      <td width="116" valign="top"> <input class="usatcajas" type="text" name="txttotalcreobl_pes" size="4" maxlength="4" onKeyUp="ComprobarNumero(this)"> 
        <div align="justify"></div>      
      </td>
      <td width="250" valign="top">Total Credito ElecObl</td>
      <td width="59" valign="top"><input class="usatcajas" type="text" name="txttotalcredelecobl_pes" size="4" maxlength="4" value="0" onKeyUp="ComprobarNumero(this)"></td>
      <td width="107" valign="top"><div align="justify"></div>
        <div align="justify">Total Horas</div>
        </td>
      <td valign="top"><input class="usatcajas" type="text" name="txttotalhoras_pes" size="5" maxlength="5" onKeyUp="ComprobarNumero(this)"></td>
      <td valign="top">&nbsp;</td>
    </tr>
    <tr> 
      <td width="140" height="28">Sumillas</td>
      <td colspan="6"><input class="usatcajas" type="text" name="txtsumillas_pes" size="50" maxlength="50"></td>
    </tr>
    <tr> 
      <td width="140" height="30">Malla Curricular</td>
      <td colspan="6"> <input class="usatcajas" type="file" name="txtmallacurricular_pes"  size="50" maxlength="50"></td>
    </tr>
    <tr> 
      <td width="140" height="31">&nbsp;</td>
      <td colspan="6"> <input type="submit" value="Guardar" name="cmdGuardar" class="usatGuardar" onClick="validarfrmplanestudio(frmplanestudio)"> 
        <input type="button" value="Cancelar" name="cmdCancelar" onClick=location="listaplanestudio.asp" class="usatsalir"> 
      </td>
    </tr>
  </table>
</fieldset>
</form>
</BODY>
</HTML>