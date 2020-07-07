<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Catequesis</title>
<link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
.Estilo1 {
	font-size: 12pt;
	font-weight: bold;
}
-->
</style>
</head>

<%
	Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion			
			Set rs=obj.Consultar("ALU_ConsultarDatosCapellania","FO",session("codigo_usu"))
%>



<body topmargin="0" leftmargin="0" rightmargin="0" bottommargin="0">
<form id="frmCatequesis" method="post" action="registrocatequesis.asp">
<table width="100%" height="600" border="0" cellpadding="0" cellspacing="0" class="contornotabla">
  <tr>
    <td width="80%" align="left" valign="top">
	<iframe src="http://www.usat.edu.pe/programas/catequesis/" frameborder="0" width="100%" height="600">
	</iframe>
	</td>
    <td width="20%" valign="top" bgcolor="#FFFFCC" class="bordeizq">
	  <!-- ************************************************************** DESDE ACA ************************************* -->
	  <%
	  mostrar = obj.Ejecutar("AVI_MostrarAviso",true,session("codigo_usu"),"CATEQUESIS","A","")
	  if mostrar="SI" then
	  %>

<table width="100%" border="0" cellspacing="0" cellpadding="4">
      <tr>
        <td class="tituloformulario"><div align="center" class="Estilo1">INSCRIPCIONES</div></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
	  
      <tr>
        <td>&Uacute;ltimo Sacramento recibido </td>
      </tr>
      <tr>
        <td align="center"><select name="cboSacramento" size="1" id="cboSacramento">
          <option value="NINGUNO" selected="selected">NINGUNO</option>
          <option value="BAUTISMO">BAUTISMO</option>
          <option value="COMUNI&Oacute;N">COMUNI&Oacute;N</option>
          <option value="CONFIRMACI&Oacute;N">CONFIRMACI&Oacute;N</option>
          <option value="MOTRIMONIO">MATRIMONIO</option>
        </select>        </td>
      </tr>
      <tr>
        <td>Tel&eacute;fono</td>
      </tr>
      <tr>
        <td align="center"><input name="txtTelefono" type="text" id="txtTelefono" value="<%=rs.fields("telefonocasa_Dal")%>" maxlength="20" /></td>
      </tr>
      <tr>
        <td>Celular</td>
      </tr>
      <tr>
        <td align="center"><input name="txtcelular" type="text" id="txtcelular" value="<%=rs.fields("telefonomovil_Dal")%>" maxlength="20" /></td>
      </tr>
      <tr>
        <td>e-mail</td>
      </tr>
      <tr>
        <td align="center"><input name="txtemail" type="text" id="txtemail" value="<%=rs.fields("email_alu")%>" maxlength="40" /></td>
      </tr>
      <tr>
        <td>Direcci&oacute;n</td>
      </tr>
      <tr>
        <td align="center"><input name="txtdireccion" type="text" id="txtdireccion" value="<%=rs.fields("direccion_Dal")%>" maxlength="50" /></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td align="center"><input name="Submit" type="submit" class="imgGuardar" value="     Participar" /></td>
      </tr>
      <tr>
        <td align="center">&nbsp;</td>
      </tr>
      <tr>
        <td align="left"><p align="justify">Al momento de la inscripci&oacute;n se aportar&aacute; S/.5.00 y llegada la Ceremonia se abonar&aacute; S/.10.00 para material de trabajo y gastos de la Ceremonia as&iacute; como suscripci&oacute;n en los Libros Parroquias.</p>          </td>
      </tr>
      <tr>
        <td align="left"><div align="justify">Se recomienda comprar un Compencio de Catecismo. </div></td>
      </tr>
    </table>
      <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    <p class="bordeizq">&nbsp;</p>
    </td>
  </tr>
  <!-- ************************************** HASTA  *********************************** -->
  <%
ELSE
RESPONSE.Write("<br><br>")
Response.Write("<center><B>")
RESPONSE.Write("Su participación ya ha sido registrada. <br><br> Muchas gracias.<br><br>Capellanía USAT")
Response.Write("</center></B>")
END IF

		obj.CerrarConexion
	Set obj=nothing
%>
</table>
</from>
</body>

</html>
