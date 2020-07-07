<!--#include file="../NoCache.asp"-->
<%
'*************************************************************************************
'Cargar datos de matr�cula y anuncios de Escuelas
'*************************************************************************************

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
	Set rsArchivo=Obj.Consultar("ConsultarMatricula","FO",30,session("codigo_cac"),session("codigo_cpf"),0)
	Obj.CerrarConexion
Set obj=nothing

if Not(rsArchivo.BOF and rsArchivo.EOF) then
	Cronograma=rsArchivo("Cronograma")
	Aviso=rsArchivo("Aviso")
	ancho="width='100%'"	
else
	ancho="width='70%'"
End if

Set rsArchivo=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Comunicados por Escuela Profesional</title>
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
<style>
<!--
.titulomensaje { font-size: 13pt; font-weight: bold; color:#FF0000 }
-->
</style>
</head>
<body>
<p class="usattitulo">Matr�cula <%=session("descripcion_cac")%></p>
  <table border="1" cellpadding="5" cellspacing="3" style="border-collapse: collapse" bordercolor="#C0C0C0" <%=ancho%> height="80%">
  <tr>
    <td width="15%" valign="top" height="15%" bgcolor="#D9ECFF">
    <p class="usatEtiqOblig">
    <a href="../ayuda/matricula.pdf">
    <img border="0" src="../images/img1.gif">Manual del Sistema de Matr�cula</a></p>
    <p>Encuentra aqu� la gu�a del sistema de matr�cula, que indica paso a paso como realizar tu pre-matr�cula, 
    agregados y retiros.</td>
    <%if aviso<>"" then%>
    <td width="15%" valign="top" bgcolor="#F1E187" rowspan="4" height="100%">
	<p class="usatEtiqOblig"><img border="0" src="../images/img6.gif"> Aviso de 
	la Escuela Profesional</p>
	<%=Aviso%>
    </td>
    <%end if%>
  </tr>
  <tr>
    <td width="15%" valign="top" height="15%" bgcolor="#E6E6FA">
  <p class="titulomensaje"><a href="avisos/reglamento/ReglamentoPensiones2008-1.html"><img border="0" src="../images/img5.gif">
  <font color="#FF0000">Pre-Matriculate aqu�</font></a></p>
  <p>La pre-matricula, es un proceso via Internet mediante el cual el estudiante elige las 
  asignaturas que va a llevar en el ciclo acad�mico. <br>
  <br>
  Para hacer efectiva la matr�cula, el estudiante debe <b>realizar el pago 
  correspondiente</b> en cualquier oficina del Banco de Cr�dito a nivel nacional.</p>
    </td>
  </tr>
  <tr>
    <td width="15%" valign="top" height="50%" bgcolor="#CCFFCC">
  <p class="usatEtiqOblig"><img border="0" src="../images/img3.gif">Calendario 
	de Matr�cula</p>
	<%=Cronograma%>
  </td>
  </tr>
  <tr>
    <td width="15%" valign="top" height="15%" bgcolor="#EBE1BF">
  <p class="usatEtiqOblig"><a href="frmsugerencia.asp"><b><img border="0" src="../images/mensaje.gif"> </b>
  Cont�ctenos</a></p>
  <p>Env�a tus comentarios o sugerencias sobre el Proceso de Matr�cula o Uso del 
  Sistema</td>
  </tr>
</table>
</body>
</html>