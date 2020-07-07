<!--#include file="../../../../funciones.asp"-->
<%

codigo_rec=Request.QueryString("codigo_rec")
		Set Reunion=Server.CreateObject("PryUSAT.clsAccesoDatos")
		Reunion.AbrirConexion
			set RsReunion=Reunion.Consultar("ConsultarReunionConsejo","FO","PR",codigo_rec,0,0)
		Reunion.CerrarConexion
	NOMBRE=RsReunion("agenda_rec")
	LUGAR=RsReunion("lugar_rec")
	FECHA=RsReunion("FECHA_rec")
	TIPO=RsReunion("TIPO_rec")	

Response.ContentType = "application/msword"
Response.AddHeader "Content-Disposition","attachment;filename=" & NOMBRE & ".doc"

%>
<html>
<head>

<title>Acta de Reuni&oacute;n de Consejo Universitario</title>

<style type="text/css">
<!--
p {
	font-family: "Belwe Lt BT";
	font-size: 12pt;
	font-style: normal;
	font-weight: bold;
	font-variant: normal;
	color: #000000;
	text-align: justify;
}
.TITULO {
	font-family: "Belwe Lt BT";
	font-size: 12pt;
	font-style: normal;
	font-weight: bold;
	font-variant: normal;
	color: #000000;
	text-align: CENTER;
}
.Estilo1 {
	font-family: "Belwe Lt BT";
	font-size: 12pt;
	color: #000000;
	font-weight: bold;
}
.Estilo2 {font-family: "Belwe Lt BT"}


-->
</style>

</head>
<script language="JavaScript" src="private/validarpropuestas.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../private/calendario.js"></script>


<body topmargin="0" rightmargin="0" leftmargin="0">
<p align="center" class="TITULO">&nbsp;</p>
<p align="center" class="TITULO">&nbsp;</p>
<p align="center" class="TITULO">&nbsp;</p>
<p align="center" class="TITULO">&nbsp;</p>
<p align="center" class="TITULO">CONSEJO UNIVERSITARIO </p>
<p align="center" class="TITULO">ACTA N&ordm; <%=NOMBRE%> </p>
<p align="center" class="TITULO">		
		<%IF TIPO = "O"THEN%>
			-- Sesi&oacute;n Ordinaria -- 
		<%ELSE%>
			-- Sesi&oacute;n Extraordinaria --
		<%END IF%>	</p>
<p>&nbsp;</p>
<p>En Chiclayo, en la  sede de la   Universidad Cat&oacute;lica Santo Toribio de  Mogrovejo, a las nueve horas  del d&iacute;a quince de noviembre de dos mil seis, se reunieron los miembros del  Consejo Universitario, con la presencia del se&ntilde;or Rector, Mgtr. Pedro   Mendoza Guerrero,  los se&ntilde;ores: Mgtr. Esteban Puig Tarrats, Vicegran Canciller; Dr. V&iacute;ctor   Alvitres Castillo,  Vicerrector Acad&eacute;mico; Ing. Mart&iacute;n  Mares Ruiz, Director Acad&eacute;mico; Lic. Jorge   Chirinos Salazar, Decano de la Facultad de Ciencias; Dra. Olinda Vigo Vargas, Decana de la Facultad de Humanidades; Mgtr. Carlos Campana Marroqu&iacute;n, Administrador General; Mgtr. Jorge P&eacute;rez Uriarte, Secretario General,  para desarrollar la sesi&oacute;n ordinaria&nbsp;programada para la fecha.</p>
<p>El se&ntilde;or rector,  luego de verificar el qu&oacute;rum correspondiente, declar&oacute; abierta la sesi&oacute;n,  precisando la agenda:</p>
	<%
	Set Agenda=Server.CreateObject("PryUSAT.clsAccesoDatos")
	Agenda.AbrirConexion
	set RsAgenda=Agenda.Consultar("ConsultarAgendaReunionConsejo","FO","TO",codigo_rec,"")
	Agenda.CerrarConexion
	%>

<ol>
  	<%do while not RsAgenda.EOF%>
    <li class="Estilo1" style="list-style-type: armenian" > <%=RsAgenda("nombre_prp")%></li>

	<%
	RsAgenda.MoveNext
	loop%>

</ol>

<ol>
	<li class="Estilo1" style="list-style-type:upper-alpha; margin:-24"> Lectura  del acta anterior</li>
	<p class="Estilo1">El  secretario dio lectura al acta de la sesi&oacute;n ordinaria N&ordm; <%=NOMBRE%> y fue aprobada.</p>
  <li class="Estilo1" style="list-style-type:upper-alpha; margin:-24"> Secci&oacute;n Informes </li>
		<p>&nbsp;</p>
	<li class="Estilo1" style="list-style-type:upper-alpha; margin:-24"> Secci&oacute;n orden del d&iacute;a </li>
</ol>

<%
Set Agenda=Server.CreateObject("PryUSAT.clsAccesoDatos")
Agenda.AbrirConexion
set RsAgenda=Agenda.Consultar("ConsultarAgendaReunionConsejo","FO","TO",codigo_rec,"")
Agenda.CerrarConexion
%>

<ol>
  <%do while not RsAgenda.EOF%>
  <li class="Estilo1" style="list-style-type: armenian" > <%=RsAgenda("nombre_prp")%></li>
  <p>
			<%
			Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	objProp.AbrirConexiontrans
			set discusiones=objProp.Consultar("ConsultarDiscusionPropuesta","FO","PR",RsAgenda("codigo_prp"))
	    	objProp.CerrarConexiontrans
			set objProP=nothing
			%>
			<% do while not discusiones.eof%>
				<%=discusiones("descripcion_dpr")%><br>
  			<%discusiones.MoveNext
			loop%>
  </p>
  <p style="margin-left:0">Acuerdos:</p>
  <p>
    <%	    Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	objProp.AbrirConexiontrans
			set ACUERDOS=objProp.Consultar("ConsultarAcuerdosPropuesta","FO","PR",RsAgenda("codigo_prp"))
	    	objProp.CerrarConexiontrans
			set objProP=nothing
	%>
<ul>
<% do while not ACUERDOS.eof%>			
<li type="square" class="Estilo1"> 
<%=ACUERDOS("descripcion_apr")%><br>
</li>	
<p>
  <%ACUERDOS.MoveNext
loop%>
</p>
</ul>
	
  </p>

  <p>
    <%
RsAgenda.MoveNext
loop%>
</p>
  <p><strong>Habiendo  concluido la agenda, el rector levant&oacute; la sesi&oacute;n siendo las &nbsp;&nbsp;&nbsp; horas del  mismo d&iacute;a.</strong><strong> </strong><strong> </strong> </p>
  <table border="1" align="center" cellpadding="0" cellspacing="0">
    <tr>
      <td width="288" align="center" valign="bottom" class="Estilo1"><br>
          <br>
        <br>
        Mgtr. Pedro Mendoza Guerrero     <BR>        
          Rector        </td>
      <td width="288" align="center" valign="bottom" class="Estilo1">Mgtr.    Esteban Puig Tarrats<br>
      Vice    Gran Canciller</td>
    </tr>
    <tr>
      <td width="288" align="center" valign="bottom" class="Estilo1"><br>
          <br>
        <br>
        Dr.    V&iacute;ctor Alvitres Castillo<br>
      Vicerrector    Acad&eacute;mico</p>      </td>
      <td width="288" align="center" valign="bottom" class="Estilo1">Ing.    Mart&iacute;n Mares Ruiz<br>
      Director    Acad&eacute;mico</td>
    </tr>
    <tr>
      <td width="288" align="center" valign="bottom" class="Estilo1"><br>
        <br>
        <br>
        Dra. Olinda Vigo Vargas<br>
      Decana de Humanidades     </td>
      <td width="288" align="center" valign="bottom" class="Estilo1">Lic. Jorge Chirinos Salazar<br>
      Decano de Ciencias</td>
    </tr>
    <tr>
      <td width="288" height="40" align="center" valign="bottom" class="Estilo1"><br>
          <br>
        <br>
        Mgtr. Carlos     CampanaMarroqu&iacute;n<br>
              Administrador General
      </td>
      <td width="288" align="center" valign="bottom" class="Estilo1">Doyfe:Mgtr.    Jorge P&eacute;rez Uriarte <br>
      Secretario    General </td>
    </tr>
  </table>
  <p>&nbsp;</p>
  <p>&nbsp;</p>
</ol>
</body>
<script>
window.close()
</script>
</html>