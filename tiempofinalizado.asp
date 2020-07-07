<%
session.abandon
Session.Contents.RemoveAll
session("codigo_usu")=""
%>
<html>
    <head>
    <style>
        .textog {  font-family: Verdana,Arial;	font-style: normal;	font-weight: normal; font-size: 10px; color: #666666; }
        .textopagc { font-family: Verdana,Arial;	font-weight: normal; font-size: 11px; color: #666666; }
        .pie { font-family: Verdana,Arial;	font-style: normal;	font-weight: normal; font-size: 10px; color: #cccccc; }
        .pie A { font-family: Verdana,Arial;	font-style: normal;	font-weight: normal; font-size: 10px; color: #cccccc; TEXT-DECORATION: none; }
    table
    {
        background-color: #FFFFFF;
        border: 1px solid #808080;
        border-collapse: collapse;
        padding: 3px;
    }
.mensaje { font-family: Verdana,Arial;	font-weight: normal; font-size: 11px; color: #666666; }
    </style>
    <title>Tiempo de sesión finalizado</title>
	<script type="text/javascript" language="javascript">
		function cerrarpagina()
		{
			top.window.close()
		}
	</script>
    </head>
    <body topmargin="0" leftmargin="0" bgcolor="#e8e8e8">
	
<p>&nbsp;</p>
<center>
<table width="80%" border="0" cellspacing="0" cellpadding="3">
  <tr>
    <td width="15%" rowspan="3" align="center" valign="top">
    <img src="images/alerta.gif" /></td>
    <td style="border-style: none none solid none; border-width: 3px; border-color: #808080; font-family:verdana,arial; FONT-SIZE: 18px; color:#FF0000" width="85%" 
          height="50px">
        Ha finalizado su sesión en el sistema</td>
  </tr>
  <tr>
    <td class="mensaje" width="85%">&nbsp;<br>
        <b>Ha superado el límite de inactividad en el sistema.</b>
        <br />
&nbsp;<br />
        Por favor, cierre su navegador de Internet e ingrese de nuevo al campus virtual.<br>
    &nbsp;<br>
    &nbsp;    <br>
    <span style="font-size:10px">&copy; <%=year(now)%>. Universidad Católica Santo Toribio de Mogrovejo <%=year(date)%>.
    Derechos reservados.<br />
        <a href="mailto:desarrollosistemas@usat.edu.pe">desarrollosistemas@usat.edu.pe</a><br />
        <br />
        </span></td>
  </tr>
  </table>
</center>  
	
    </body>
</html>