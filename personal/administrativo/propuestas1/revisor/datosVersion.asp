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
	Set RsVersiones=objVERSION.Consultar("CONSULTARVERSIONESPROPUESTA","FO","DA",codigo_prp,version)
	objVERSION.CerrarConexion
	set objVERSION=nothing
%>
Versi&oacute;n <%=version%></span> </td>
    <td width="79%"><table width="98%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="50" class="Estilo7">Moneda:</td>
        <td width="149" align="center" class="Estilo5"><%=RsVersiones("descripcion_tip")%></td>
        <td width="147" class="Estilo7">Tipo Cambio :</td>
        <td width="114" align="center" class="Estilo5"><%=RsVersiones("TIPOCAMBIO_DAP")%></td>
        <td width="70" class="Estilo7">Fecha:</td>
        <td width="226" align="center" class="Estilo5"><%=RsVersiones("FECHAACTUALIZACION_DAP")%></td>
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
	Set RsArchivo=oObjArchivo.Consultar("ConsultarArchivosPropuesta","FO","CP",RsVersiones("codigo_dap"))
	oObjArchivo.CerrarConexion
	set oObjArchivo=nothing
%>
    </span><%=RsArchivo.RecordCount%> Archivos</span></td>
  </tr>
  <tr>
    <td colspan="2"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
      
      <tr>
        <td width="9%"><span class="Estilo7">Ingresos</span></td>
        <td width="2%"><span class="Estilo7">:</span></td>
        <td width="19%" align="right"><span class="Estilo5"><%=RsVersiones("INGRESO_DAP")%></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td width="11%"><span class="Estilo7">Ingresos</span></td>
        <td width="5%"><span class="Estilo7">: </span><span class="Estilo5">S/. </span></td>
        <td align="right"><span class="Estilo5"><%=RsVersiones("INGRESOMN_DAP")%></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td width="34%" rowspan="3">
		<table height="50" width="100%" border="0" cellpadding="0" cellspacing="0" class="contornotabla">
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
        <td><span class="Estilo7">Egresos</span></td>
        <td><span class="Estilo7">:</span></td>
        <td align="right"><span class="Estilo5"><U>&nbsp;&nbsp;<%=RsVersiones("EGRESO_DAP")%></U></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td><span class="Estilo7">Egresos</span></td>
        <td><span class="Estilo7">: </span><span class="Estilo5">S/. </span></td>
        <td width="20%" align="right"><span class="Estilo5"><U>&nbsp;&nbsp;<%=RsVersiones("EGRESOMN_DAP")%></U></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
      <tr>
        <td><span class="Estilo7">Utilidad</span></td>
        <td><span class="Estilo7">:</span></td>
        <td align="right"><span class="Estilo5"><%=RsVersiones("UTILIDAD_DAP")%></span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td><span class="Estilo12">Utilidad</span></td>
        <td><span class="Estilo12">: S/. </span></td>
        <td align="right"><span class="Estilo12"><%=RsVersiones("UTILIDADMN_DAP")%></span><span class="Estilo13"><strong>&nbsp;&nbsp;&nbsp;</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></td>
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
                <td><span class="Estilo7">Resumen</span></td>
              </tr>
              <tr>
                <td><span class="Estilo5"><%=RsVersiones("IMPORTANCIA_DAP")%></span></td>
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
                  <td><span class="Estilo7">Importancia</span></td>
                </tr>
                <tr>
                  <td><span class="Estilo5"><%=RsVersiones("BENEFICIOS_DAP")%></span></td>
                </tr>
              </table>
              </td>
          </tr>
        </table></td>
        </tr>
      <tr>
        <td colspan="7" valign="top"><table width="100%" border="0" cellspacing="2" cellpadding="2">
          <tr>
            <td>
			<%
				Set oObjActividad=Server.CreateObject("PryUSAT.clsAccesoDatos")
				oObjActividad.AbrirConexion
				Set RsActividad=oObjActividad.Consultar("ConsultarActividadesPropuesta","FO","es",RsVersiones("codigo_dap"),0)
				oObjActividad.CerrarConexion
				set oObjActividad=nothing				
				%>
				
				<%if RsActividad.eof =false then %>
			<table width="100%" border="0" cellpadding="1" cellspacing="1" class="contornotabla">
              <tr>
                <td colspan="2" align="left"><span class="Estilo5"><strong>Actividades</strong></span>
                                    </td>
                <td align="center"><span class="Estilo7">Inicio</span></td>
                <td align="center"><span class="Estilo7">Fin</span></td>
                <td align="center"><span class="Estilo7">Monto</span></td>
                <td align="center"><span class="Estilo7">Observaci&oacute;n</span></td>
              </tr>
              
              <%do while not RsActividad.EOF
			  e=e+1 '' contador para mostrar en primera columna
			  %>
              <tr>
                <td width="4%" align="right"><span class="Estilo5"><%=e%>.-&nbsp;</span></td>
                <td width="43%"><span class="Estilo5"><%=RsActividad("descripcion_atp")%></span></td>
                <td width="13%"><span class="Estilo5"><%=RsActividad("fechainicio_atp")%></span></td>
                <td width="11%"><span class="Estilo5"><%=RsActividad("fechaFin_atp")%></span></td>
                <td width="11%" align="right"><span class="Estilo5"><%=formatnumber(RsActividad("costo_atp"),2)%>&nbsp;&nbsp;</span></td>
                <td width="18%"><span class="Estilo5"><%=RsActividad("observacion_atp")%></span></td>
              </tr>
  </span>
  
  <%
			  RsActividad.MoveNext
			  loop%>
            </table>
			<%end if %>
			</td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
</table>
</body>

</html>
