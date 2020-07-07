
<%
Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
	codigofoto=obEnc.CodificaWeb("069" & session("codigoUniver_alu"))
set obEnc=Nothing

'----------------------------------------------------------------------
'Fecha: 29.10.2012
'Usuario: yperez
'Motivo: Cambio de URL del servidor de la WebUSAT [www.usat.edu.pe->intranet.edu.pe]
'----------------------------------------------------------------------

%>

<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tbldatosestudiante" class="contornotabla">
  <tr>
    <td width="20%" rowspan="4" valign="top">
        <img border="0" src="https://intranet.usat.edu.pe/imgestudiantes/<%=codigofoto%>" width="104" height="118" alt="Sin Foto"></td>
    <td width="20%">Código Universitario&nbsp;</td>
    <td class="usatsubtitulousuario" width="80%">: <%=session("codigoUniver_alu")%>&nbsp;</td>
  </tr>
  <tr>
    <td width="20%">Apellidos y Nombres</td>
    <td class="usatsubtitulousuario" width="80%">: <%=session("alumno")%></td>
  </tr>
  <tr>
    <td width="20%">Escuela Profesional&nbsp;</td>
    <td class="usatsubtitulousuario" width="55%">: <%=session("nombre_cpf")%>&nbsp;</td>
  </tr>
  <tr>
    <td width="20%">Ciclo de Ingreso</td>
    <td class="usatsubtitulousuario" width="55%">: <%=session("cicloIng_Alu")%></td>
  </tr>
  </table>