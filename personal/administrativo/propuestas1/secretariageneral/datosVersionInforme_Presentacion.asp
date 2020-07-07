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
.Estilo7 {color: #000000; font-weight: bold; }
.Estilo15 {color: #000000; font-weight: bold; font-size: 15pt; }
.Estilo17 {color: #000000; font-size: 15pt; }
.Estilo19 {color: #000000; font-weight: bold; font-size: 14pt; }
.Estilo20 {font-size: 14pt}
.Estilo21 {color: #000000; font-size: 14pt; }
.Estilo23 {color: #000000; font-size: 14px; font-weight: bold; }
.Estilo30 {color: #000000; font-size: 14px; }
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
        <td width="114" align="center" class="Estilo30">&nbsp;</td>
        <td width="70" class="Estilo23">Fecha:</td>
        <td width="226" align="left" class="Estilo30"><%=RsVersiones("FECHAACTUALIZACION_DiP")%></td>
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
          <td><span class="Estilo19">Fecha Inicio </span></td>
          <td><span class="Estilo19">:</span></td>
          <td align="right"><span class="Estilo21"><%=RsVersiones("fechainicioEjecucion_dip")%></span><span class="Estilo20">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
          <td><span class="Estilo20"></span></td>
          <td>&nbsp;</td>
          <td width="34%" rowspan="3"><table height="50" width="100%" border="0" cellpadding="0" cellspacing="0" class="contornotabla">
            <tr height="100%">
              <td height="100%" valign="top"><table width="100%" border="0" cellspacing="0" cellpadding="0">
                <%do while not RsArchivo.EOF%>
                <TR height="20" >
                  <td width="23%" align="center"><a href="../../../../filespropuestas/<%=codigo_prp%>/<%=RsArchivo("nombre_apr")%>" target="_blank" class="Estilo23"> <img src="../../../../images/ext/<%=right(RsArchivo(2),3)%>.gif" width="16" height="16"  border="0"> </a> </td>
                      <td width="77%" align="left"><span class="Estilo23">
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
        <td width="15%"><span class="Estilo19">Fecha Fin </span></td>
        <td width="5%"><span class="Estilo19">:</span></td>
        <td width="17%" align="right"><span class="Estilo21"><%=RsVersiones("fechaFinEjecucion_dip")%></span><span class="Estilo20">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
        <td width="24%"><span class="Estilo20"></span></td>
        <td width="5%">&nbsp;</td>
        </tr>
      <tr>
        <td><span class="Estilo19">Utilidad</span></td>
        <td><span class="Estilo19">:</span></td>
        <td align="right"><span class="Estilo21"><%=RsVersiones("utilidad_usat")%></span><span class="Estilo20">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
        <td><span class="Estilo20"></span></td>
        <td>&nbsp;</td>
        </tr>
      <tr>
        <td><span class="Estilo20"></span></td>
        <td><span class="Estilo20"></span></td>
        <td><span class="Estilo20"></span></td>
        <td><span class="Estilo20"></span></td>
        <td>&nbsp;</td>
        <td width="34%">&nbsp;</td>
      </tr>
      <tr>
        <td colspan="6" valign="top"><table width="100%" border="0" cellspacing="2" cellpadding="2">
          <tr>
            <td width="73%" valign="top"><table width="100%" border="0" cellpadding="1" cellspacing="1" class="contornotabla">
              <tr>
                <td><span class="Estilo15">Objetivo</span></td>
              </tr>
              <tr>
                <td><span class="Estilo17"><%=RsVersiones("Objetivo_dip")%></span></td>
              </tr>
            </table></td>
            </tr>
          <tr>
            <td valign="top"><table width="100%" border="0" cellpadding="1" cellspacing="1" class="contornotabla">
              <tr>
                <td><span class="Estilo15">Metas</span></td>
              </tr>
              <tr>
                <td><span class="Estilo17"><%=RsVersiones("metas_dip")%></span></td>
              </tr>
            </table></td>
          </tr>
          <tr>
            <td valign="top"><table width="100%" border="0" cellpadding="1" cellspacing="1" class="contornotabla">
              <tr>
                <td><span class="Estilo15">Espectativa</span></td>
              </tr>
              <tr>
                <td><span class="Estilo17"><%=RsVersiones("espectativas_dip")%></span></td>
              </tr>
            </table></td>
          </tr>
          <tr>
            <td>&nbsp;</td>
          </tr>
        </table></td>
      </tr>
      
    </table></td>
  </tr>
</table>
</body>

</html>
