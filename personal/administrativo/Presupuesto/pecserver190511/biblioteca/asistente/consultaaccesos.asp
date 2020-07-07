<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Consulta de Accesos a Bibliotecas Virtuales</title>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<%

	 Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	 objProp.AbrirConexion
	 set RsAccesos=objProp.Consultar("BIB_ConsultarVisitasBibliotecaVirtuales","FO")
	 objProp.CerrarConexion
	 set objProP=nothing

%>

<body>
<p class="usatTablaSubTitulo">Consulta de Accesos a Bibliotecas Virtuales</p>
<table width="100%" border="0" align="center" cellpadding="1" cellspacing="1" class="contornotabla">
  <tr>
    <td width="15%" class="etabla">Tipo Lector </td>
    <td width="20%" class="etabla">Carrera/&Aacute;rea</td>
    <td width="30%" class="etabla">Base</td>
    <td width="10%" class="etabla">A&ntilde;o</td>
    <td width="10%" class="etabla">Mes</td>
    <td width="15%" class="etabla">Nro. Accesos </td>
  </tr>
  <% for i =0 to RsAccesos.RecordCount -1 %>
  <tr>
    <td><%= RsAccesos(0)%></td>
    <td><%= RsAccesos(1)%></td>
    <td><%= RsAccesos(2)%></td>
    <td align="center"><%= RsAccesos(3)%></td>
    <td align="center"><%= RsAccesos(4)%></td>
    <td align="center"><%= RsAccesos(5)%></td>
  </tr>
  <%
  RsAccesos.MoveNext
  next
  %>
</table>
</body>
</html>
