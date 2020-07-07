<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Documento sin t&iacute;tulo</title>
<style type="text/css">
<!--
body,td,th {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 12px;
}
.Estilo1 {
	color: #990000;
	font-weight: bold;
}
.Estilo8{
color:#FF0000; font-size:24px}
-->
</style></head>
<%
if (Request.QueryString("guardar")="SI") then

if Request.form("pleno")="" or Request.form("fraterno")="" then
		Response.redirect("elecciondelegados.asp?votar=SI")
else
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			Obj.AbrirConexion
	'	response.write (Request.form("pleno"))
	'	response.write (Request.form("fraterno"))
				obj.Ejecutar "AVI_DelegadosSistemas2009",false,"VO",session("codigo_alu"),Request.form("pleno"),Request.form("fraterno")
			Obj.CerrarConexion
		Set obj=nothing
		
		response.redirect "../principal.asp?pagina=" & session("enlace_apl")
end if		
else
%> 
<form id="form1" name="form1" method="post" action="elecciondelegados.asp?guardar=SI">
<body>
<table width="600" border="0" align="center" cellpadding="3" cellspacing="3">
  <tr>
    <td><img src="urna.jpg" width="600" height="142" /></td>
  </tr>
  <tr>
    <td align="left" 
          style="font-size: medium; font-weight: bold; font-family: Arial, Helvetica, sans-serif; color: #FF0000;">
        Selecione UN candidato de cada lista&nbsp;</td>
  </tr>
  <tr>
    <td><table width="100%" border="0" cellspacing="3" cellpadding="3">
      <tr>
        <td width="50%"><span class="Estilo1">Delegado Pleno </span></td>
        <td width="50%"><span class="Estilo1">Delegado Fraterno </span></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td><p>
<input name="pleno" type="radio" value="1" />          
HUATUCO GRANDA, BEELLYE JEAN JACKELYN</p>
          </td>
        <td><input name="fraterno" type="radio" value="1" />
          PALACIN SILVA, MAR&Iacute;A VICTORIA</td>
      </tr>
      <tr>
        <td><input name="pleno" type="radio" value="2" />
          BERRIOS GUEVARA, Y&Eacute;INER MICHAEL</td>
        <td><input name="fraterno" type="radio" value="2" />
          VOTO EN BLANCO </td>
      </tr>
      <tr>
        <td><input name="pleno" type="radio" value="3" />
          VOTO EN BLANCO </td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td align="center"><input type="Submit" name="Submit" value="   Votar   " /></td>
  </tr>
  <tr>
    <td bgcolor="#990100" height="2"></td>
  </tr>
</table>
</body>
<%end if%>
<%
if (Request.QueryString("votar")="SI") then
	response.write("<font class='Estilo8' ><center>SELECCIONE UN CANDIDATO Y EFECTÚE SU VOTACIÓN</center></font>")
end if
%>
</form>
</html>
