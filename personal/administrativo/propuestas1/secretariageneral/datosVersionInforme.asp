<html>
<head>
<title>Documento sin t&iacute;tulo</title>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
body {
	background-color: #FFFFFF;
}
-->
</style>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">

<style type="text/css">
<!--
.Estilo4 {
	font-size: 16px;
	color: #003399;
	font-weight: bold;
}
.Estilo5 {color: #000000}
.Estilo7 {color: #000000; font-weight: bold; }
.Estilo12 {color: #990000; font-weight: bold; }
.Estilo13 {color: #990000}
-->
</style>
<script language="JavaScript" src="../../../../private/tooltip.js"></script>
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
	Set RsVersiones=objVERSION.Consultar("ConsultarDatosVersionInforme","FO","es",codigo_prp,version)
	objVERSION.CerrarConexion
	set objVERSION=nothing
%>
      Versi&oacute;n <%=version%></span> </td>
    <td width="79%"><table width="98%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="114" align="center" class="Estilo5">&nbsp;</td>
        <td width="70" class="Estilo7">Fecha:</td>
        <td width="226" align="left" class="Estilo5"><%=RsVersiones("FECHAACTUALIZACION_DiP")%></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td colspan="2" align="right"><span class="Estilo7"><span class="Estilo4">
      <%
codigo_Prp=Request.QueryString("codigo_Prp")
version=Request.QueryString("version")

	Set oObjArchivo=Server.CreateObject("PryUSAT.clsAccesoDatos")
	oObjArchivo.AbrirConexion
''	Response.Write(RsVersiones("codigo_dap"))
	Set RsArchivo=oObjArchivo.Consultar("ConsultarArchivosPropuesta","FO","Ci",RsVersiones("codigo_dip"))
	oObjArchivo.CerrarConexion
	set oObjArchivo=nothing
%>
    </span><%=RsArchivo.RecordCount%> Archivos</span></td>
  </tr>
  <tr>
    <td colspan="2"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
      
        <tr>
          <td><span class="Estilo7">Fecha Inicio </span></td>
          <td><span class="Estilo7">:</span></td>
          <td align="right"><span class="Estilo5"><%=RsVersiones("fechainicioEjecucion_dip")%></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
          <td width="34%" rowspan="3"><table height="50" width="100%" border="0" cellpadding="0" cellspacing="0" class="contornotabla">
            <tr height="100%">
              <td height="100%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                <%do while not RsArchivo.EOF%>
                <TR height="20" >
                  <td width="23%" align="center"><a href="../../../../filespropuestas/<%=codigo_prp%>/<%=RsArchivo("nombre_apr")%>" target="_blank" class="Estilo5"> <img src="../../../../images/ext/<%=right(RsArchivo(2),3)%>.gif" width="16" height="16"  border="0"> </a> </td>
                      <td width="77%" align="left"><span class="Estilo5">
                        <%response.write(RsArchivo(3))%>
                      </span></td>
                    </TR>
                <%
			  RsArchivo.MoveNext
			  loop%>
                </table></td>
              </tr>
          </table></td>
        </tr>
        <tr>
        <td width="15%"><span class="Estilo7">Fecha Fin </span></td>
        <td width="5%"><span class="Estilo7">:</span></td>
        <td width="17%" align="right"><span class="Estilo5"><%=RsVersiones("fechaFinEjecucion_dip")%></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td width="24%">&nbsp;</td>
        <td width="5%">&nbsp;</td>
        </tr>
      <tr>
        <td><span class="Estilo7">Utilidad</span></td>
        <td><span class="Estilo7">:</span></td>
        <td align="right"><span class="Estilo5"><%=RsVersiones("utilidad_usat")%></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td width="34%">&nbsp;</td>
      </tr>
      <tr>
        <td colspan="6" valign="top"><table width="100%" border="0" cellspacing="2" cellpadding="2">
          <tr>
            <td><table width="100%" border="0" cellpadding="1" cellspacing="1" class="contornotabla">
              <tr>
                <td><span class="Estilo7">Objetivo</span></td>
              </tr>
              <tr>
                <td><span class="Estilo5"><%=RsVersiones("Objetivo_dip")%></span></td>
              </tr>
            </table></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td colspan="6" valign="top"><table width="100%" border="0" cellspacing="2" cellpadding="2">
          <tr>
            <td><table width="100%" border="0" cellpadding="1" cellspacing="1" class="contornotabla">
              <tr>
                <td><span class="Estilo7">Metas</span></td>
              </tr>
              <tr>
                <td><span class="Estilo5"><%=RsVersiones("metas_dip")%></span></td>
              </tr>
            </table></td>
          </tr>
          <tr>
            <td><table width="100%" border="0" cellpadding="1" cellspacing="1" class="contornotabla">
              <tr>
                <td><span class="Estilo7">Espectativa</span></td>
              </tr>
              <tr>
                <td><span class="Estilo5"><%=RsVersiones("espectativas_dip")%></span></td>
              </tr>
            </table></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td colspan="6" valign="top">&nbsp;</td>
      </tr>
    </table></td>
  </tr>
</table>
</body>

</html>
