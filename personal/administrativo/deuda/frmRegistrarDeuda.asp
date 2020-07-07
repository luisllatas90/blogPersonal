<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<html>
<head>
<title>Documento sin t&iacute;tulo</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link href="../../../private/usatcss.css" rel="stylesheet" type="text/css">
</head>

<body>
<p> 
  <% 
Dim codResp
Dim tipoResp
Dim nomResp
codResp=Request.QueryString("id") 
tipoResp=Request.QueryString("tr") 
'Localizar Datos del Responsable
if tipoResp="E" then
        tipoResponsable=" ESTUDIANTE "
		Set objRes= Server.CreateObject("PryUSAT.clsDatAlumno")
		Set rsRes= Server.CreateObject("ADODB.RecordSet")
		Set rsRes= objRes.ConsultarAlumno("RS","CO",codResp)
		if rsRes.recordcount >0 then
			nomResp=rsRes("Alumno")
		end if
end if

if tipoResp="P" then
        tipoResponsable=" TRABAJADOR "
		'FALTA
end if

if tipoResp="V" then
	        tipoResponsable=" OTROS "
			'FALTA
end if





Dim objCon
Dim rsCon
Set objCon=Server.CreateObject("PryUSAT.clsDatServicio")
Set rsCon=Server.CreateObject("ADODB.Recordset")
Set rsCon= objCon.ConsultarServicioConcepto ("RS","TO","")
%>
</p>
<form action="registrarDeuda.asp" method="post" name="frmRegistrarDeuda">
  <table width="75%" border="0" align="center">
    <tr> 
      <td colspan="4"><div align="center"><img src="images/regdeuda.jpg"></div></td>
    </tr>
    <tr> 
      <td colspan="4"></td>
    </tr>

    <tr> 
      <td width="24%" class="usatCabeceraCelda"><div align="right">
          Tipo de Responsable:
        </div></td>
      <td colspan="3"><%=tipoResponsable%></td>
    </tr>
    <tr> 
      <td class="usatCabeceraCelda"><div align="right">Responsable:</div></td>
      <td colspan="3"><b><%=nomResp%></b></td>
    </tr>
    <tr> 
      <td background="images/borde.jpg"><input name="txtTipoResp" type="hidden" id="txtTipoResp" value="<%=tipoResp%>"></td>
      <td colspan="3" background="images/borde.jpg"><font size="2" face="Arial, Helvetica, sans-serif">&nbsp; 
        <input name="txtCodResp" type="hidden" id="txtCodResp" value="<% =codResp%>">
        </font></td>
    </tr>
    <tr> 
      <td class="usatCabeceraCelda"> 
        <div align="right">Fecha:</div></td>
      <td colspan="3">
      <input name="txtFecha" type="text" id="txtFecha" size="20">
        </td>
    </tr>
    <tr> 
      <td class="usatCabeceraCelda"> 
        <div align="right">Concepto de Deuda:</div></td>
      <td colspan="3"><select name="cboConcep_Deu" id="cboConcep_Deu">
          <% do while not rsCon.eof %>
          <option value="<%=rsCon("codigo_Sco")%>"><%=rsCon("descripcion_Sco")%></option>
          <% rsCon.movenext
		   loop
		   rsCon.close
		   set rsCon=Nothing
		   set objCon= Nothing
	    %>
        </select>
        </td>
    </tr>
    <tr> 
      <td class="usatCabeceraCelda"> <div align="right">Monto de la Deuda:</div></td>
      <td width="19%"> 
      <input name="txtMon_Deu" type="text" id="txtMon_Deu3" size="20">
        </td>
      <td width="12%"> <div align="right">Moneda:</div></td>
      <td width="45%" class="usatDato"> 
        <select name="cboMoneda" id="select2">
          <option value="S">Soles</option>
          <option value="D">Dolares</option>
          <option value="E">Euros</option>
        </select> Como Decimal Reconoce a la coma (,)</td>
    </tr>
    <tr> 
      <td class="usatCabeceraCelda"> <div align="right">Observaci&oacute;n:</div></td>
      <td colspan="3"><input name="txtObserv" type="text" id="txtObserv" size="80">
        </td>
    </tr>
    <tr> 
      <td>&nbsp;</td>
      <td colspan="2"><div align="right"> <input name="cmdGrabar" type="submit" id="cmdGrabar" value="Registrar Deuda" class="usatCmdboton">
        </div></td>
      <td><input name="cmdCancelar" type="reset" id="cmdCancelar" value="Cancelar Registro" class="usatCmdboton"></td>
    </tr>
  </table>
  </form>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>

<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
</body>
</html>