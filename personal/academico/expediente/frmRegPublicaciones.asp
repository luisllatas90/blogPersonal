<%
'***************************************************************************************
'CV-USAT
'Archivo			: publicaciones.asp
'Autor			: USAT
'Fecha de Creación		: 07/11/200516:45:51
'Observaciones		: formulario para ingreso/modificación de datos de la tabla PUBLICACIONES
'***************************************************************************************
Dim objSituacion
Dim rsSituacion
Set objSituacion=Server.CreateObject("PryUSAT.clsDatSituacion")
Set rsSituacion=Server.CreateObject("ADODB.Recordset")
Set rsSituacion= objSituacion.ConsultarSituacion("RS","TO","")

Dim objArea
Dim rsArea
Set objArea=Server.CreateObject("PryUSAT.clsDatPublicacion")
Set rsArea=Server.CreateObject("ADODB.Recordset")
Set rsArea= objArea.ConsultarArea("RS","TO","")

Dim objTipoPub
Dim rsTipoPub
Set objTipoPub=Server.CreateObject("PryUSAT.clsDatPublicacion")
Set rsTipoPub=Server.CreateObject("ADODB.Recordset")
Set rsTipoPub= objTipoPub.ConsultarTipoPublicacion("RS","TO","")

Dim objMedioPub
Dim rsMedioPub
Set objMedioPub=Server.CreateObject("PryUSAT.clsDatPublicacion")
Set rsMedioPub=Server.CreateObject("ADODB.Recordset")
Set rsMedioPub= objMedioPub.ConsultarMedioPublicacion("RS","TO","")

Dim objInvestigacion
Dim rsInvestigacion
Set objInvestigacion=Server.CreateObject("PryUSAT.clsDatInvestigacion")
Set rsInvestigacion=Server.CreateObject("ADODB.Recordset")
Set rsInvestigacion= objInvestigacion.ConsultarInvestigacion("RS","DO",session("codigo_Usu"))
%>
<HTML>
<HEAD>
<meta name="GENERATOR" content="Microsoft Visual Studio 6.0">
<title>publicaciones</title>
<link rel="stylesheet" type="text/css" href="private/usatcss.css">
<script language="JavaScript" src="private/validaciones.js"></script>
<link href="../../../private/estilo.css" rel="stylesheet" type="text/css">
</HEAD>
<BODY>
<form name="frmpublicaciones" method="post"  action="RegPublicaciones.asp">
<fieldset style="padding: 2">
<legend class="usatTituloPagina">Registro de publicaciones</legend>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tblpublicaciones">
    <tr> 
      <td width="20%">T&iacute;tulo</td>
      <td width="80%"><input class="usatcajas" type="text" value="<%=strtitulo_Pub%>" name="txttitulo_Pub" size="50" maxlength="100"></td>
    </tr>
    <tr> 
      <td width="20%">Fecha de publicaci&oacute;n</td>
      <td width="80%"><input class="usatcajas" type="text" value="<%=dtmfecha_Pub%>" name="txtfecha_Pub" size="16" maxlength="16"></td>
    </tr>
    <tr> 
      <td width="20%">Situacion</td>
      <td width="80%"><select name="cbocodigo_Sit">
          <option value="0">---Seleccione Situación---</option>
          <% do while not rsSituacion.eof 
			seleccionar="" 
			if(cint(request.form("cbocodigo_Sit"))=rsSituacion(0)) then seleccionar="SELECTED"%>
          <option value="<%=rsSituacion(0)%>" <%=seleccionar%>> 
          <%=rsSituacion("Descripcion_Sit")%> </option>
          <% rsSituacion.movenext
		loop%>
        </select></td>
    </tr>
    <tr> 
      <td width="20%">Observaciones</td>
      <td width="80%"><textarea class="usatcajas" rows="2" name="atxobservaciones_Pub" cols="80" style="width: 350"><%=strobservaciones_Pub%></textarea></td>
    </tr>
    <tr> 
      <td width="20%">Area de conocimiento</td>
      <td width="80%"><select name="cbocodigo_Aco">
          <option value="0">---Seleccione Area---</option>
          <% do while not rsArea.eof 
			seleccionar="" 
			if(cint(request.form("cbocodigo_Aco"))=rsArea(0)) then seleccionar="SELECTED"%>
          <option value="<%=rsArea(0)%>" <%=seleccionar%>> 
          <%=rsArea("descripcion_Aco")%> </option>
          <% rsArea.movenext
		loop%>
        </select></td>
    </tr>
    <tr> 
      <td width="20%">Tipo de publicaci&oacute;n</td>
      <td width="80%"><select name="cbocodigo_TPu">
          <option value="0">---Seleccione Tipo---</option>
          <% do while not rsTipoPub.eof 
			seleccionar="" 
			if(cint(request.form("cbocodigo_TPu"))=rsTipoPub(0)) then seleccionar="SELECTED"%>
          <option value="<%=rsTipoPub(0)%>" <%=seleccionar%>> 
          <%=rsTipoPub("Descripcion_TPu")%> </option>
          <% rsTipoPub.movenext
		loop%>
        </select></td>
    </tr>
    <tr> 
      <td width="20%">Medio de publicaci&oacute;n</td>
      <td width="80%"><select name="cbocodigo_MPu">
          <option value="0">---Seleccione Medio---</option>
          <% do while not rsMedioPub.eof 
			seleccionar="" 
			if(cint(request.form("cbocodigo_MPu"))=rsMedioPub(0)) then seleccionar="SELECTED"%>
          <option value="<%=rsMedioPub(0)%>" <%=seleccionar%>> 
          <%=rsMedioPub("descripcion_MPu")%> </option>
          <% rsMedioPub.movenext
		loop%>
        </select></td>
    </tr>
    <tr> 
      <td>&nbsp;</td>
      <td><p> 
          <input type="radio" name="optInvest" value="0">
          No est&aacute; basada en una investigaci&oacute;n existente <br>
          <input type="radio" name="optInvest" value="1">
          Basada en una investigaci&oacute;n realizada </p>
        </td>
    </tr>
    <tr> 
      <td>&nbsp;</td>
      <td><select name="cbocodigo_Inv">
          <option value="0">---Seleccione Investigación---</option>
          <% do while not rsInvestigacion.eof 
			seleccionar="" 
			if(cint(request.form("cbocodigo_MPu"))=rsInvestigacion(0)) then seleccionar="SELECTED"%>
          <option value="<%=rsInvestigacion(0)%>" <%=seleccionar%>> <%=rsInvestigacion("titulo_Inv")%> 
          </option>
          <% rsInvestigacion.movenext
		loop%>
        </select></td>
    </tr>
    <tr> 
      <td width="20%">&nbsp;</td>
      <td width="80%"><input type="hidden" name="codigo_per" value="<%=session("codigo_Usu")%>"> <input type="submit" value="Guardar" name="smtGuardar" class="usatguardar"> 
        <input OnClick="window.close()" type="button" value="Cancelar" name="cmdCancelar" class="usatsalir"> 
      </td>
    </tr>
  </table>
</fieldset>
</form>
</BODY>
</HTML>

