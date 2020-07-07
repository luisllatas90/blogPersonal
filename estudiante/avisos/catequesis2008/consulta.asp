<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Consulta de Incritos a Catequesis 2008</title>

<link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
.Estilo1 {
	font-size: 10pt;
	font-weight: bold;
}
.Estilo4 {font-size: 10px; font-weight: bold; }
-->
</style>
<link href="../../../private/estilo.css" rel="stylesheet" type="text/css">
</head>

<body>
<p class="MI Estilo1">Almunos inscritos en Catequesis 2008</p>
<table width="100%" border="0" align="center" cellpadding="2" cellspacing="0" class="contornotabla">
  <tr class="MI">
    <td class="bordeizqinf"><span class="Estilo4">Paterno</span></td>
    <td class="bordeizqinf"><span class="Estilo4">Materno</span></td>
    <td class="bordeizqinf"><span class="Estilo4">Nombres</span></td>
    <td class="bordeizqinf"><span class="Estilo4">C&oacute;digo</span></td>
    <td class="bordeizqinf"><span class="Estilo4">Carrera</span></td>
    <td class="bordeizqinf"><span class="Estilo4">Direcci&oacute;n</span></td>
    <td class="bordeizqinf"><span class="Estilo4">&Uacute;ltimo Sacramento </span></td>
    <td class="bordeizqinf"><span class="Estilo4">Tel&eacute;fono Fijo </span></td>
    <td class="bordeizqinf"><span class="Estilo4">Celular</span></td>
    <td class="bordeizqinf"><span class="Estilo4">E-mail</span></td>
    <td class="bordeizqinf"><span class="Estilo4">Fecha Registro </span></td>
  </tr>
  <%
  	Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion			
			set rs = obj.Consultar ("ConsultarParticipantesCatequesis","FO")
		
		
		do while not rs.EOF
  %>
  <tr>
    <td class="bordeizqinf">&nbsp;<%=Rs(0)%>&nbsp;</td>
    <td class="bordeizqinf">&nbsp;<%=Rs(1)%>&nbsp;</td>
    <td class="bordeizqinf">&nbsp;<%=Rs(2)%>&nbsp;</td>
    <td class="bordeizqinf">&nbsp;<%=Rs(3)%>&nbsp;</td>
    <td class="bordeizqinf">&nbsp;<%=Rs(4)%>&nbsp;</td>
    <td class="bordeizqinf">&nbsp;<%=Rs(5)%>&nbsp;</td>
    <td class="bordeizqinf">&nbsp;<%=Rs(6)%>&nbsp;</td>
    <td class="bordeizqinf">&nbsp;<%=Rs(7)%>&nbsp;</td>
    <td class="bordeizqinf">&nbsp;<%=Rs(8)%>&nbsp;</td>
    <td class="bordeizqinf">&nbsp;<%=Rs(9)%>&nbsp;</td>
    <td class="bordeizqinf">&nbsp;<%=Rs(10)%>&nbsp;</td>
  </tr>
	  <%
	  rs.MoveNext
	  loop
	  
	  obj.CerrarConexion
	  %>
</table>
<p class="usatTituloIcono"><%Response.Write("Total de Inscritos: " & rs.RecordCount)%></p>
</body>



</html>
