<!--#include file="../usuario/clsusuario.asp"-->
<%
Dim usuario
accion=request.querystring("accion")
idcursovirtual=request.querystring("idcursovirtual")
codigo_apl=request.querystring("codigo_apl")
codigo_tfu=request.querystring("codigo_tfu")

set usuario=new clsusuario
	usuario.Restringir=session("codigo_usu")
	ArrDatos=usuario.consultar("6","cursovirtual",idcursovirtual,"")
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
<script language="JavaScript" src="private/validarcurso.js"></script>
</head>
<body topmargin="0" leftmargin="0">
<form name="frmLista" method="POST" onSubmit="return validareliminarpermiso(this)" action="procesar.asp?accion=eliminaraccesocursovirtual&idcursovirtual=<%=idcursovirtual%>&codigo_apl=<%=codigo_apl%>&codigo_tfu=<%=codigo_tfu%>">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="barraherramientas">
    <tr>
      <td width="100%">
      <%if codigo_apl<>4 then
      	'if codigo_tfu<3 then%>
  		<input type="button" onclick="Javacript:location.href='../usuario/frmbuscarusuario.asp?idtabla=<%=idcursovirtual%>&nombretabla=cursovirtual&codigo_apl=<%=codigo_apl%>&codigo_tfu=<%=codigo_tfu%>'" value="   Agregar público" class="agregar3" style="width: 120" name="cmdGuardar">
		<input type="submit" value="Eliminar" name="cmdEliminar" class="eliminar3">
		<input type="button" onClick="top.window.close()" value="Cerrar" name="cmdCancelar" class="cerrar3">
  	  	<%'end if
  	  end if%>
	  <input type="button" onClick="EnviarDatosAcceso('<%=idcursovirtual%>')" value="   Enviar datos de acceso" name="cmdEnviar" class="permisos3" style="width: 160">
  	  <span id="mensaje" style="color:#FF0000"></span>
  	</td>
    </tr>
  </table>
<br>
<%If IsEmpty(Arrdatos)=false then%>
<table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%" height="85%">
  <tr class="etabla">
    <td width="3%" height="10%">
    	<%if Ubound(Arrdatos,2)>0 then
    		if codigo_tfu<>3 then%>
		    <input type="checkbox" name="chkSeleccionar" onclick="MarcarTodoCheck(frmLista)" value="1">
		   	<%end if
		end if%></td>
    <td width="17%" height="10%">Tipo de Usuario</td>
    <td width="57%" height="10%">Apellidos y Nombres</td>
    <td width="23%" height="10%">Tipo de acceso</td>
  </tr>
  <tr>
  <td width="100%" align="center" colspan="4" valign="top" height="90%">
  <DIV id="listadiv" style="height:100%">
  <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#E9E9E9" width="100%" id="tbllista">
  <%for i=lbound(Arrdatos,2) to ubound(Arrdatos,2)%>
  <tr id="fila<%=Arrdatos(0,I)%>">
    <td width="3%" align="center">
    <%if arrdatos(3,I)<>session("codigo_usu") then
    	'if codigo_apl<>4 then
    		if codigo_tfu<>3 then
    			if Arrdatos(5,i)>0 then%>
    				<img src="../../../images/bloquear.gif" ALT="Bloqueado para eliminarlo porque el usuario ha registrado recursos en el Curso Virtual(<%=arrdatos(5,i)%> recursos)"/>
    			<%else%>
	    		<input type="checkbox" name="chk" value="<%=Arrdatos(0,I)%>" onclick=pintafilamarcada(this) id="chk<%=Arrdatos(0,i)%>" idtipo_usu="<%=Arrdatos(8,i)%>" codigo_usu="<%=Arrdatos(3,i)%>" nombre_usu="<%=Arrdatos(1,i)%>" clave_usu="<%=iif(IsNull(Arrdatos(6,i))=true,"CLAVE DE CORREO",Arrdatos(6,i))%>" email_usu="<%=Arrdatos(7,i)%>">
    			<%end if
    		end if
    	'end if
    end if%>
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
  <span class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b><%=Ubound(Arrdatos,2)+1%> </b> usuarios con acceso al Recurso seleccionado</span>
</form>
<%end if%>
</body>
</html>