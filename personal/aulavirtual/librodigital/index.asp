<!--#include file="clslibrodigital.asp"-->
<%

Set contenido=new clslibrodigital
	with contenido
		.restringir=session("idcursovirtual")
		ArrDatos=.consultar("1",session("idcursovirtual"),session("codigo_usu"),"")
	end with
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Contenidos digitales</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarlibrodigital.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
<style>
<!--
a:link       { text-decoration: underline }
-->
</style>
</head>

<body>

<table class="bordeinf" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
  <tr>
    <td width="80%" class="e1">Contenidos digitales</td>
   <%if session("tipofuncion")<>3 then%>
    <td width="20%" align="right">
    <input name="cmdAgregar" type="button" onClick="AbrirLibrodigital('A')" class="nuevo" onclick="" value="    Nuevo">
    </td>
    <%end if%>    
  </tr>
</table>
<br>
<%If IsEmpty(ArrDatos)=true then%>
	<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se ha encontrado contenido digital registrado</p>
<%else%>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" id="AutoNumber1">
  <tr class="etabla">
    <td width="33%" colspan="2">Título</td>
    <td width="30%">Descripción</td>
    <td width="20%">Disponibilidad</td>
    <td width="13%">Acción</td>
  </tr>
  <%for i=lbound(Arrdatos,2) to ubound(Arrdatos,2)%>
  <tr id="fila<%=ArrDatos(0,i)%>">
    <td width="3%" valign="top" style="border-right-style: none; border-right-width: medium">
    <img border="0" src="../../../images/previo.gif" alt="Vista previa del libro digital"></td>
    <td width="30%" valign="top" style="border-left-style: none; border-left-width: medium"><a href="Javascript:AbrirLibrodigital('<%=iif(Arrdatos(10,i)=0,"B","V")%>','<%=arrdatos(0,i)%>')"><%=Arrdatos(1,i)%></a>&nbsp;</td>
    <td width="30%" valign="top"><%=Arrdatos(4,i)%>&nbsp;</td>
    <td width="20%" valign="top"><%=Arrdatos(2,i)%> -<br><%=Arrdatos(3,i)%>&nbsp;</td>
    <td width="13%" valign="top" style="cursor:hand" align="right">
    <%if Arrdatos(9,i)=session("codigo_usu") then%>
    <img border="0" onClick="AbrirLibrodigital('M','<%=ArrDatos(0,i)%>')" src="../../../images/editar.gif" alt="Modificar Libro digital">
    <img border="0" onClick="AbrirLibrodigital('E','<%=ArrDatos(0,i)%>')" src="../../../images/eliminar.gif" alt="Eliminar libro digital">
    <img border="0" onClick="AbrirLibrodigital('P','<%=ArrDatos(0,i)%>')" src="../../../images/<%=iif(arrdatos(6,i)=3,"p1","todos")%>.gif" ALT="Administrar permisos"/>
    <img border="0" onClick="AbrirLibrodigital('C','<%=ArrDatos(0,i)%>')" src="../../../images/lista.gif" align="absbottom" alt="Administrar contenido temático del libro digital">
    <%end if%>
    </td>
  </tr>
  <%next%>
</table>
<%end if%>
</body>
</html>