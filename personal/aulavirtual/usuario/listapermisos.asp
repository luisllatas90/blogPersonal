<!--#include file="clsusuario.asp"-->
<%
Dim usuario
modo=request.querystring("modo")
idtabla=request.querystring("idtabla")
nombretabla=request.querystring("nombretabla")

set usuario=new clsusuario
	usuario.Restringir=session("idcursovirtual")
	ArrDatos=usuario.consultar("4",nombretabla,idtabla,"")
Set usuario=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Usuarios que tienen acceso</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
</head>
<body topmargin="0" leftmargin="0">
<form name="frmLista" method="POST" action="procesar.asp?accion=eliminarpermiso&modo=<%=modo%>&titulo=<%=titulo%>&idtabla=<%=idtabla%>&nombretabla=<%=nombretabla%>&descripcion=<%=descripcion%>">
  <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" class="barraherramientas">
    <tr>
      <td>
  <input type="button" onclick="Javacript:location.href='frmagregarusuariosrecurso.asp?modo=<%=modo%>&idtabla=<%=idtabla%>&nombretabla=<%=nombretabla%>'" value="     Agregar participantes" class="agregar3" style="width: 150">
  <input type="submit" value="Eliminar" name="cmdEliminar" class="eliminar3">
  </td>
    </tr>
  </table>
  <br>
<%If IsEmpty(Arrdatos)=false then%>
<table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%" height="78%">
  <tr class="etabla">
    <td width="3%" height="10%"><%if Ubound(Arrdatos,2)>0 then%>
    <input type="checkbox" name="chkSeleccionar" onclick="MarcarTodoCheck(frmLista)" value="1"><%end if%></td>
    <td width="17%" height="10%">Tipo de Usuario</td>
    <td width="57%" height="10%">Apellidos y Nombres</td>
    <td width="23%" height="10%">Tipo de acceso</td>
  </tr>
  <td width="100%" align="center" colspan="4" valign="top" height="90%"> 
  <DIV id="listadiv" style="height:100%">
  <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#E9E9E9" width="100%" id="tbllista">
  <%for i=lbound(Arrdatos,2) to ubound(Arrdatos,2)%>
  <tr id="fila<%=Arrdatos(0,I)%>">
    <td width="3%" align="center">
    <%if arrdatos(3,I)<>session("codigo_usu") then%>
	    <input type="checkbox" name="chk" value="<%=Arrdatos(0,I)%>" onclick=pintafilamarcada(this)>
    <%end if%>
    </td>
    <td width="17%">&nbsp;<%=Arrdatos(4,I)%></td>
    <td width="60%">&nbsp;<%=Arrdatos(1,I)%></td>
    <td width="20%">&nbsp;<%=Arrdatos(2,I)%></td>
  </tr>
  <%next%>
    </table>
    </DIV>
    </td>
  </tr>
  </table>
  <span class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b><%=Ubound(Arrdatos,2)+1%> de <%=session("numusuarios")%></b> usuarios que comparten el Recurso seleccionado</span>
</form>
<%else
	response.redirect "frmagregarusuariosrecurso.asp?modo=" & modo & "&idtabla=" & idtabla & "&nombretabla=" & nombretabla
end if%>
</body>
</html>