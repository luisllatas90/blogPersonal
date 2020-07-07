<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Avisos Campus</title>

<style type="text/css">
<!--
.Estilo1 {	color: #FF6600;
	font-weight: bold;
}
.Estilo14 {color: #FF3300; font-weight: bold; }
-->
</style>

<script type="text/javascript" language="javascript" src="lytebox.js"></script>




</head>

<body topmargin="0" leftmargin="0" rightmargin="0">

<table width="100%" border="0" cellspacing="1" cellpadding="1">
<%
	set cn=server.createobject("pryusat.clsaccesodatos")
	
	cn.abrirconexion	
	set rs= cn.consultar ("CV_ConsultarAvisoCampus","FO","V","P",-1)
	cn.cerrarconexion
	
	for i= i to rs.recordcount-1
%>

  <tr>
    <td ><table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="<%=RS("ColorFondo_Avc")%>" >
      <tr>
        <td valign="top">&nbsp;</td>
        <td align="left" valign="middle" class="Estilo1">&nbsp;</td>
      </tr>
      <tr>
        <td width="6%" valign="top" ><img src="../images/arrow.gif" width="11" height="11" /></td>
        <td width="94%" align="left" valign="middle"  class="Estilo1">
		<table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td width="90%" valign="middle"><%=rs("Titulo_Avc")%> </td>
              <td width="10%" align="center"><img src="../images/librohoja.gif" width="12" height="15" /></td>
            </tr>
        </table>
		</td>
      </tr>
      <tr>
        <td >&nbsp;</td>
        <td ><table width="100%" border="0" cellspacing="3" cellpadding="3">
            <tr>
              <td><table width="100%" border="0" cellspacing="3" cellpadding="3">
                  <tr>
                    <td width="47%" valign="top"><p><%=rs("descripcion_avc")%><br />
                      <br/>
					  <%if rs("vermas_avc") <> "" then%>
                          <a href="<%=rs("vermas_avc")%>" target="_top">Participa AQUI!!!</a> <br />
                     <%end if%>
                      </p></td>
                    <td width="53%" align="center"><p><a href="<%=rs("vinculoImagen")%>" rel="lytebox" target="<%=rs("target")%>"><img src="<%=rs("img_avc")%>" alt="<%=rs("titulo_avc")%>" border="1" /></a></p></td>
                  </tr>
              </table></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td align="right" ><%=rs("fecha_avc")%></td>
      </tr>
      <tr height="1px">
        <td bgcolor="#999999"></td>
        <td align="right" bgcolor="#999999"></td>
      </tr>
    </table></td>
  </tr>
  <%
  rs.movenext
  next
  %>
</table>
<p>&nbsp;</p>
</body>
</html>