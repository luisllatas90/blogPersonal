<%
On Error Resume Next
ip=Request.ServerVariables("REMOTE_ADDR")
usuario="-No definido-"
if session("Usuario_bit")<>"" then 
    usuario=session("Usuario_bit")
end if

'Generar cadena a escribir
cadena=usuario & vbtab & now & vbtab & ip & vbTab & session("pagerror") & vbTab & session("nroerror") & vbTab & session("descripcionerror")

filename=server.mappath("errores.log")
set fs = createobject("scripting.filesystemobject")
    set writefile = fs.opentextfile(filename, 8, true)
    writefile.writeline(cadena)
    writefile.close
    set writefile=nothing
set fs=nothing

'De esta forma se llama al archivo error.asp
'obviamente al inicio de la página colocar on error resume next
If Err.Number<>0 then
    session("pagerror")="estudiante/izq.asp"
    session("nroerror")=err.number
    session("descripcionerror")=err.description    
	response.write("<script>top.location.href='../error.asp'</script>")
End If
%>
<html>
<head>
<style type="text/css">
.mensaje { font-family: Verdana,Arial;	font-weight: normal; font-size: 11px; color: #666666; }
    table
    {
        background-color: #FFFFFF;
        border: 1px solid #808080;
        border-collapse: collapse;
        padding: 3px;
    }
</style>
</head>
<body bgcolor="#F6F6F6">
<p>&nbsp;</p>
<center>
<table width="80%" border="0" cellspacing="0" cellpadding="3">
  <tr>
    <td width="15%" rowspan="3" align="center" valign="top">
    <img src="images/alerta.gif" /></td>
    <td style="border-style: none none solid none; border-width: 3px; border-color: #808080; font-family:verdana,arial; FONT-SIZE: 18px; color:#FF0000" width="85%" 
          height="50px">
        Ha ocurrido un error interno en el sistema</td>
  </tr>
  <tr>
    <td class="mensaje" width="85%">&nbsp;<br>
        Por favor, cierre su navegador de Internet e ingrese de nuevo al campus virtual.<br />
        <br />
        Si el error persiste contáctese con
        <a href="mailto:desarrollosistemas@usat.edu.pe">desarrollosistemas@usat.edu.pe</a><br>
    &nbsp;<br><span onClick="cerrarpagina()">
        <a href="https://intranet.usat.edu.pe/campusvirtual/index.asp" target="_top">Pagina Principal</a></span><br>
    &nbsp;<br>
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