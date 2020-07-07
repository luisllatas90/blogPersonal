<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>USAT Formamos personas y mejores profesionales</title></head>

<body>

<%
if( Request.QueryString("guardar")="SI") then
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			Obj.AbrirConexion
				encuestas=obj.Ejecutar("ENC_RegistrarModoSer",true,session("codigo_alu"))
			Obj.CerrarConexion
		Set obj=nothing
		
		response.redirect "principal.asp?pagina=" & session("enlace_apl")
else
%>
<table border="0" align="center" cellpadding="4" cellspacing="4">
  <tr>
    <td width="576"><img src="../images/mododeser.jpg" width="576" height="760" border="0" /></td>
    <td align="left"><form id="form1" name="form1" method="post" action="mododeser.asp?guardar=SI">
      <input type="submit" name="Submit" value="CONTINUAR" style="background-color:#FF6600; height:40; width:80; color:#0033CC; font-size:14px" />
    </form>    </td>
  </tr>
</table>
<%end if%>
</body>
</html>
