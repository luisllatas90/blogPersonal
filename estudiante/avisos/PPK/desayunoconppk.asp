<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Desayuno de trabajo con PPK</title>
<style type="text/css">
<!--
body {
	background-color: #676767;
}
body,td,th {
	font-family: Verdana, Arial, Helvetica, sans-serif;
}
.Estilo1 {
	color: #FFFFFF;
	font-weight: bold;
	font-size: 24px;
}
.Estilo2 {
	color: #FFFF00;
	font-weight: bold;
	font-size: 24px;
}
.Estilo3 {font-size: 24px}
.Estilo4 {
	color: #FFFF00;
	font-weight: bold;
}
.Estilo5 {
	color: #FFFFFF;
	font-weight: bold;
}
-->
</style>
<script language="javascript">
function enviar(){
//alert(document.all.radiobutton[0].checked);
if ((document.all.radiobutton[0].checked == false) && (document.all.radiobutton[1].checked == false)) {
	alert('Seleccione una opción');
}else{
if (document.all.radiobutton[0].checked == false){
	document.location.href=('desayunoconppk.asp?opcion=NO')
}else{
	document.location.href=('desayunoconppk.asp?opcion=SI')
}
}

}

function irlista(){
	document.location.href=('../../../personal/listaaplicaciones.asp')
}
</script>
</head>
<body>
<div align="center">
  <p class="Estilo4">
    <%
	
set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		
if Request.QueryString("opcion")<>"" then
if Request.QueryString("opcion")="SI" then
	obj.AbrirConexion			
		obj.ejecutar "PPK_RegistrarParticipacion",false,"RE",session("codigo_usu"),1
	obj.CerrarConexion

else
	obj.AbrirConexion			
		obj.ejecutar "PPK_RegistrarParticipacion",false,"RE",session("codigo_usu"),0
	obj.CerrarConexion
end if
%>
    Su elecci&oacute;n ha sido registrada como: <% Response.write(Request.QueryString("opcion"))%> participar, gracias</p>
  <p class="Estilo4"><br /> 
    <input type="button" name="Submit32" value="Regresar" onclick="irlista()" />
    <%

else
	

	obj.AbrirConexion			
		set rs=obj.consultar ("PPK_RegistrarParticipacion","FO","CO",session("codigo_usu"),0)
	obj.CerrarConexion


if rs.RecordCount>0 then %>
    Usted ya defini&oacute; anteriormente su participaci&oacute;n </p>
  <p>
    <input type="button" name="Submit3" value="Regresar" onclick="javascript:history.back()" />
  </p>
  <p>
    <%
else
%>
    
    
  </p>
</div>
<form id="form1" name="form1" method="post" action="desayunaconppk.asp">
<table width="100%" border="0">
  <tr>
    <td width="41%">&nbsp;</td>
    <td width="59%">&nbsp;</td>
  </tr>
  <tr>
    <td height="36" colspan="2" align="center" bgcolor="#333333"><span class="Estilo5">Semana de Ingenier&iacute;a 2009:</span></td>
    </tr>
  <tr>
    <td align="right"><img src="big.jpg" width="300" height="400" /></td>
    <td><table width="100%" border="0">
      <tr>
        <td width="8%">&nbsp;</td>
        <td width="92%"><span class="Estilo1">&iquest;Deseas participar?</span></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td><span class="Estilo2">
          <input name="radiobutton" type="radio" value="SI" />
          SI</span></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td><span class="Estilo3"></span></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td><span class="Estilo2">
          <input name="radiobutton" type="radio" value="NO" />
        NO</span></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td><input type="button" name="Submit" value="Aceptar" onclick="enviar()" />
          <input type="button" name="Submit2" value="Cancelar" onclick="javascript:history.back()" /></td>
      </tr>
    </table>    </td>
  </tr>
  <tr>
    <td>&nbsp;</td>
    <td>&nbsp;</td>
  </tr>
</table>
</form>

<% end if

end if%>
</body>
</html>
