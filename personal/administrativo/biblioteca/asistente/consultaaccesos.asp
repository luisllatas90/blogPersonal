<!--<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>-->

<!--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
-->
<%

	 Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	 objProp.AbrirConexion
	 set RsAccesos=objProp.Consultar("BIB_ConsultarVisitasBibliotecaVirtuales","FO")
	 objProp.CerrarConexion
	 set objProP=nothing
	 
%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Consulta de Accesos a Bibliotecas Virtuales</title>
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>

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
<% i=0
  Do while not RsAccesos.EOF 
  %>
  <tr>
    <td><%= RsAccesos("Tipo")%></td>
    <td><%= RsAccesos("Carrera")%></td>
    <td><%= RsAccesos("base")%></td>
    <td align="center"><%= RsAccesos("Anio")%></td>
  <td align="center"><%= RsAccesos("Mes")%></td>  
  <td align="center"><%= RsAccesos("Visitas")%></td>
  </tr>
  
  <%
  RsAccesos.movenext
    loop
    set RsAccesos=nothing
    %>
</table>
</body>
</html>
