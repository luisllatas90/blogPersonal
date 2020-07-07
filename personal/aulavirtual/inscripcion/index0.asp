<%
'Verificar acceso
Login=Request.ServerVariables("LOGON_USER")

Set ObjUsuario= Server.CreateObject("PryUSAT.clsAccesoDatos")
	ObjUsuario.AbrirConexion
		Set rsPersonal=ObjUsuario.Consultar("consultaracceso","FO","P",Login,"")
	ObjUsuario.CerrarConexion
Set ObjUsuario=nothing

if Not(rsPersonal.BOF and rsPersonal.EOF) then
	session("codigo_Usu")=Login
	session("Nombre_Usu")=rsPersonal("personal")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Matrícula de Curso Taller Aula Virtual</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
</head>

<body>

<h4 align="center"><font color="#000080">Bienvenido Sr(a): <%=session("nombre_usu")%><br>
</font><font color="#FF0000">¿ Qué día desea asistir al Curso Taller sobre la Administración del Aula Virtual ?</font></h4>
<p align="center">&nbsp;</p>
<div align="center">
  <center>
<form name="frmFicha" Method="POST" Action="matricular.asp">
<table border="1" cellpadding="5" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="50%" id="AutoNumber1">
  <tr class="etabla">
    <td width="40%" height="24">Día</td>
    <td width="37%" height="24">Hora</td>
  </tr>
 <!--
  <tr>

    <td width="40%" height="1">
    <b>
    <input type="radio" value="JUEVES" checked name="optDia"> Jueves 04 de Mayo</b></td>
    <td width="37%" height="1">
    <blockquote>
      <p>Hora de Inicio: 4:30 pm. </p>
    <p>Hora de Fin: 6:30 pm.</blockquote>
    </td>
 </tr>
  <tr>
    <td width="40%" height="34"><b>
    <input type="radio" value="VIERNES" name="optDia"> Viernes 05 de Mayo</b></td>
    <td width="37%" height="34">
    <blockquote>
      <p>Hora de Inicio: 8:00 am. </p>
      <p>Hora de Fin: 10:00 am.</p>
    </blockquote>
    </td>
  </tr>
 -->
  <tr>
    <td width="40%" height="80"><b>
    <input type="radio" value="SÁBADO" name="optDia"> Sábado 06 de Mayo</b></td>
    <td width="37%" height="80">
    <blockquote>
      <p>Hora de Inicio: 9:00 am. </p>
    <p>Hora de Fin: 11:00 am.</blockquote>
    </td>
  </tr>
</table>
  </center>
</div>
<p>&nbsp;</p>
<p align="center"><input type="submit" value="Matricular" name="cmdGuardar">
<input type="button" value="Cancelar" name="cmdCancelar" onclick="top.window.close()"></p>
</form>
</body>
</html>
<%else%>
<script>alert("Lo sentimos Ud. no tiene acceso al campus virtual, por favor escriba un email a gchunga@usat.edu.pe")</script>
<%end if
Set rsPersonal=nothing
%>