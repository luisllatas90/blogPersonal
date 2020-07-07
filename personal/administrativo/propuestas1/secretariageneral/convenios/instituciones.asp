<!--#include file="../../../../../funciones.asp"-->
<html>
<head>
<title></title>
<link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
body {
	background-color: #F0F0F0;
}

.Estilo1 {
	font-size: 10pt;
	font-weight: bold;
}
-->
</style>
<style type="text/css">
<!--
a:link {
	text-decoration: none;
}
a:visited {
	text-decoration: none;
}
a:hover {
	text-decoration: none;
}
a:active {
	text-decoration: none;
}
.Estilo3 {
	color: #395ACC;
	font-size: 10pt;
	font-weight: bold;
}
-->
</style>
<script language="JavaScript" src="../../../../../private/funciones.js"></script>
</head>

<body topmargin="5" leftmargin="0" rightmargin="0"> 
<%codigo_cni=Request.QueryString("codigo_cni")%>
<table width="100%" border="0" align="center" cellpadding="1" cellspacing="0" class="contornotabla">
  <tr>
    <td colspan="2" align="center" bgcolor="#E1F1FB" class="bordederinf"><span class="Estilo5">
      <%
	 if codigo_cni="" then
		 codigo_cni=0
	 end if
	 Set objPART=Server.CreateObject("PryUSAT.clsAccesoDatos")
	 objPART.AbrirConexiontrans
	 set PARTICIPANTES=objPART.Consultar("consultarParticipantesConvenio","FO","ES",codigo_cni)
	 objPART.CerrarConexiontrans
	 set objPART=nothing		
		%>
      Instituci&oacute;n</span></td>
    <td colspan="2" align="center" bgcolor="#E1F1FB" class="bordederinf"><span class="Estilo5">Firmante</span></td>
  </tr>
  <tr>
    <td colspan="2" bgcolor="#FFFFFF" align="center" class="bordederinf">&nbsp;</td>
    <td colspan="2" bgcolor="#FFFFFF" align="center" class="bordederinf">&nbsp;</td>
  </tr>
  <%if Not PARTICIPANTES.eof then%>
  <%
				  else
				  do while Not PARTICIPANTES.eof
				  i=i+1
				  %>
  <tr>
    <td width="2%" valign="top"><%response.write( i & ".-")%></td>
    <td width="57%"><%response.write(PARTICIPANTES(0))%></td>
    <td width="38%"><%response.write(PARTICIPANTES(1))%></td>
    <td width="3%"><%if PARTICIPANTES(2)=1 then%>
        <img src="../../../../../images/menus/conforme_small.gif" width="16" height="16" />
        <%end if%>
    </td>
  </tr>
  <%PARTICIPANTES.MoveNext
		  loop%>
</table>
				<%end if%>
</body>
</html>
