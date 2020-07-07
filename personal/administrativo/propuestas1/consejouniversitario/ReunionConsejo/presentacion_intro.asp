<!--#include file="../../../../../funciones.asp"-->


<html>
<head>
<title>Registro de Reuni&oacute;n de Consejo Universitario</title>

<link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--

.Estilo3 {
	color: #990000;
	font-size: 18pt;
	font-weight: bold;
	font-family: Arial, Helvetica, sans-serif;
}
.Estilo4 {
	color: #000000;
	font-size: 14pt;
}
.Estilo5 {
	font-family: Verdana;
	font-weight: bold;
	font-size: 10pt;
	color: #000000;
}
.Estilo7 {
	font-size: 16px;
	font-family: Arial;
	font-weight: bold;
	color: #000000;
}
.Estilo8 {
	color: #000000;
	font-weight: bold;
	font-family: Arial, Helvetica, sans-serif;
	font-size: 16px;
}
-->
</style>

</head>
<!--<script language="JavaScript" src="private/validarpropuestas.js"></script>-->
<script language="JavaScript" src="../../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../../private/calendario.js"></script>
<script>

	
	function popUp(URL) {
	day = new Date();
	id = day.getTime();
	var izq = 300//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=1,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=500,height=450,left = "+ izq +",top = "+ arriba +"');");
	}

</script>

<body topmargin="0" rightmargin="0" leftmargin="0" ON>
<%
//codigo_rec=Request.QueryString("codigo_rec")
codigo_rec=Request.QueryString("codigo_rec")
if Request.QueryString("modifica")= "1" then
		Set Reunion=Server.CreateObject("PryUSAT.clsAccesoDatos")
		Reunion.AbrirConexion
			set RsReunion=Reunion.Consultar("ConsultarReunionConsejo","FO","PR",codigo_rec,0,0)
		Reunion.CerrarConexion
	NOMBRE=RsReunion("agenda_rec")
	LUGAR=RsReunion("lugar_rec")
	FECHA=RsReunion("FECHA_rec")
	TIPO=RsReunion("TIPO_rec")		
else
	NOMBRE=Request.QueryString("NOMBRE")
	LUGAR=Request.QueryString("LUGAR")
	FECHA=Request.QueryString("FECHA")
	TIPO=Request.QueryString("TIPO")
end if

%>
	<form action="procesar.asp?accion=<%=accion%>" method="post" enctype="multipart/form-data" name="frmpropuesta" id="frmpropuesta">

<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  
  <tr>
    <td valign="top"><table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td align="right" valign="top">
			<span style="cursor:hand" class="Estilo5" onClick="window.close()">X</span>&nbsp;&nbsp;
		</td>
      </tr>
      <tr>
        <td align="center">&nbsp;</td>
      </tr>
      <tr>
        <td align="center"><span class="Estilo3">SESI&Oacute;N DE CONSEJO UNIVERSITARIO</span></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td align="center">&nbsp;<img src="../../../../../images/menus/LOGO.jpg"></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td align="center"><span class="Estilo4">
		<%IF TIPO = "O"THEN%>
			-- Sesi&oacute;n Ordinaria -- 
		<%ELSE%>
			-- Sesi&oacute;n Extraordinaria --
		<%END IF%>			
		 </span></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td align="center"><span class="Estilo7">
          <%Response.Write(date)%>
        </span></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td align="right"><span class="Estilo8"><a href="presentacion_agenda.asp?codigo_rec=<%=codigo_rec%>">Ir a Agenda </a></span></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
    </table></td>
  </tr>
</table>

</form>
</body>
</html>
