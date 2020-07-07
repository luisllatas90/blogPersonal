<html>
<head>
<title>Documento sin t&iacute;tulo</title>
<link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
body {
	background-color: #FFFFFF;
}
-->
</style>
<link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css">

<style type="text/css">
<!--
.Estilo4 {
	font-size: 16px;
	color: #003399;
	font-weight: bold;
}
.Estilo5 {color: #000000}
.Estilo7 {color: #000000; font-weight: bold; }
.Estilo13 {
	color: #990000;
	font-size: 18;
}
.Estilo15 {color: #000000; font-weight: bold; font-size: 17px; }
.Estilo20 {color: #990000; font-weight: bold; font-size: 17px; }
.Estilo28 {font-size: 18}
.Estilo29 {color: #000000; font-size: 18; }
.Estilo30 {color: #990000; font-weight: bold; font-size: 18; }
.Estilo32 {color: #000000; font-size: 16px; }
.Estilo38 {font-size: 16px}
-->
</style>
<script language="JavaScript" src="../../../../../private/tooltip.js"></script>
</head>



<body topmargin="0" leftmargin="0" rightmargin="0">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="21%"><span class="Estilo4">
    <%
codigo_Prp=Request.QueryString("codigo_Prp")
version=Request.QueryString("version")

	Set objVERSION=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objVERSION.AbrirConexion
	Set RsVersiones=objVERSION.Consultar("CONSULTARVERSIONESPROPUESTA","FO","DA",codigo_prp,version)
	objVERSION.CerrarConexion
	set objVERSION=nothing
%>
Versi&oacute;n <%=version%></span> </td>
    <td width="79%"><table width="98%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="50" class="Estilo15">Moneda:</td>
        <td width="149" align="center" class="Estilo32"><%=RsVersiones("descripcion_tip")%></td>
        <td width="147" class="Estilo15">Tipo Cambio :</td>
        <td width="114" align="center" class="Estilo32"><%=RsVersiones("TIPOCAMBIO_DAP")%></td>
        <td width="70" class="Estilo15">Fecha:</td>
        <td width="226" align="center" class="Estilo32"><%=RsVersiones("FECHAACTUALIZACION_DAP")%></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td colspan="2" align="right">
      <span class="Estilo15">
      <%
codigo_Prp=Request.QueryString("codigo_Prp")
version=Request.QueryString("version")

	Set oObjArchivo=Server.CreateObject("PryUSAT.clsAccesoDatos")
	oObjArchivo.AbrirConexion
''	Response.Write(RsVersiones("codigo_dap"))
	Set RsArchivo=oObjArchivo.Consultar("ConsultarArchivosPropuesta","FO","CP",RsVersiones("codigo_dap"))
	oObjArchivo.CerrarConexion
	set oObjArchivo=nothing
%>
      <%=RsArchivo.RecordCount%> Archivos</span></td>
  </tr>
  <tr>
    <td colspan="2"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
      
      <tr>
        <td width="9%"><span class="Estilo15">Ingresos</span></td>
        <td width="2%"><span class="Estilo15">:</span></td>
        <td width="19%" align="right"><span class="Estilo29"><%=RsVersiones("INGRESO_DAP")%></span><span class="Estilo28">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
        <td width="11%"><span class="Estilo15">Ingresos</span></td>
        <td width="5%"><span class="Estilo15">: </span><span class="Estilo32">S/. </span></td>
        <td align="right"><span class="Estilo29"><%=RsVersiones("INGRESOMN_DAP")%></span><span class="Estilo28">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
        <td width="34%" rowspan="3">
		<table height="50" width="100%" border="0" cellpadding="0" cellspacing="0" class="contornotabla">
          <tr height="100%">
            <td height="100%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
              <%do while not RsArchivo.EOF%>
			  <TR height="20" >
                <td width="23%" align="center"><a href="../../../../../filespropuestas/<%=codigo_prp%>/<%=RsArchivo("nombre_apr")%>" target="_blank" class="Estilo5"> <img src="../../../../../images/ext/<%=right(RsArchivo(2),3)%>.gif" width="16" height="16"  border="0"> </a> </td>
                <td width="77%" align="left">
                  <span class="Estilo15">
                  <a href="../../../../../filespropuestas/<%=codigo_prp%>/<%=RsArchivo("nombre_apr")%>" target="_blank">
                  <%response.write(RsArchivo(3))%>
                  </a></span></td>
              </TR>
			  <%
			  RsArchivo.MoveNext
			  loop%>
            </table></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td><span class="Estilo15">Egresos</span></td>
        <td><span class="Estilo15">:</span></td>
        <td align="right"><span class="Estilo29"><U>&nbsp;&nbsp;<%=RsVersiones("EGRESO_DAP")%></U></span><span class="Estilo28">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
        <td><span class="Estilo15">Egresos</span></td>
        <td><span class="Estilo15">: </span><span class="Estilo32">S/. </span></td>
        <td width="20%" align="right"><span class="Estilo29"><U>&nbsp;&nbsp;<%=RsVersiones("EGRESOMN_DAP")%></U></span><span class="Estilo28">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
        </tr>
      <tr>
        <td><span class="Estilo15">Utilidad</span></td>
        <td><span class="Estilo15">:</span></td>
        <td align="right"><span class="Estilo29"><%=RsVersiones("UTILIDAD_DAP")%></span><span class="Estilo28">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
        <td><span class="Estilo20">Utilidad</span></td>
        <td><span class="Estilo20">: S/. </span></td>
        <td align="right"><span class="Estilo30"><%=RsVersiones("UTILIDADMN_DAP")%></span><span class="Estilo13"><strong>&nbsp;&nbsp;&nbsp;</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
        </tr>

      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td width="34%">&nbsp;</td>
      </tr>
      <tr>
        <td colspan="7" valign="top"><table width="100%" border="0" cellspacing="2" cellpadding="2">
          <tr>
            <td><table width="100%" border="0" cellpadding="1" cellspacing="1" class="contornotabla">
              <tr>
                <td><span class="Estilo20">Importancia</span></td>
              </tr>
              
              <tr>
                <td><span class="Estilo15"><%=RsVersiones("BENEFICIOS_DAP")%></span></td>
              </tr>
            </table></td>
          </tr>
        </table></td>
      </tr>
      
      
      <tr>
        <td colspan="7" valign="top"><table width="100%" border="0" cellspacing="2" cellpadding="2">
          <tr>
            <td><table width="100%" border="0" cellpadding="1" cellspacing="1" class="contornotabla">
              <tr>
                <td><span class="Estilo20">Resumen</span></td>
              </tr>
              
              <tr>
                <td><span class="Estilo15"><%=RsVersiones("IMPORTANCIA_DAP")%></span></td>
              </tr>
            </table></td>
          </tr>
        </table></td>
        </tr>
	

    </table></td>
  </tr>
</table>
</body>

</html>
