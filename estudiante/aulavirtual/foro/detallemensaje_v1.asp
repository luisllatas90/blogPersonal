<!--#include file="clsforo.asp"-->
<%
idforo=request.querystring("idforo")
idforomensaje=request.querystring("idforomensaje")
tituloforo=request.querystring("tituloforo")
idestadorecurso=request.querystring("idestadorecurso")
vistaprevia=request.querystring("vistaprevia")

Set foro=new clsforo
	with foro
		.restringir=session("idcursovirtual")
		Arrforo=.Consultar("6",idforomensaje,"","")
		ArrRptas=.Consultar("7",idforomensaje,"","")
		
		If IsEmpty(Arrforo)=false then
			fechareg=ArrForo(0,0)
			titulomensaje=ArrForo(1,0)
			descripcionmensaje=ArrForo(2,0)
			nombreusuario=ArrForo(3,0)
			idcreador=ArrForo(4,0)
			idtipousuario=ArrForo(5,0)
			calificacionmaxima=ArrForo(7,0)
			tipocalificacion=ArrForo(8,0)
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Mensajes del Tema de discusión</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarforo.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
</head>
<body>
<%if vistaprevia="" then%>
<table cellspacing="0" width="100%" cellpadding="3" bgcolor="#C7E0CE" class="contornotabla" height="5%" style="border-collapse: collapse" bordercolor="#111111">
  <tr class="etiqueta">
    <td width="10%" valign="top" height="5%">Tema:</td>
    <td width="85%" valign="top" height="5%"><%=tituloforo%>&nbsp;</td>
    <td width="5%" valign="top" height="5%">
    <input type="button" value=" Regresar" name="cmdregresar" class="salir" onclick="AbrirForo('R','<%=idforo%>','<%=tituloforo%>','<%=idestadorecurso%>')"></td>
  </tr>
</table>
<%end if%>
  <br>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla" height="40%">
  <tr>
    <td valign="top" width="5%" bgcolor="#C7E0CE" rowspan="3">
    <%=mostrarfoto(idtipousuario,idcreador,"P")%>
    </td>
    <td valign="top" width="67%" bgcolor="#B9DBF7" height="10%">
    <b><%=titulomensaje%></b><br>
    <%=nombreusuario%> - Fecha de Registro: <%=fechareg%>
   </td>
   	<%if session("idestadocursovirtual")=1 and vistaprevia="" then%>
    <td valign="top" width="11%" bgcolor="#B9DBF7" height="10%">
    <input type="button" value="  Responder" name="cmdresponder" class="boton4" onclick="AbrirMensaje('R','<%=idforo%>','<%=titulomensaje%>','<%=idforomensaje%>')">
    </td>
    <%end if%>
  </tr>
  <tr>
    <td width="85%" valign="top" height="85%" colspan="2">
     <div id="listadiv" style="height:100%">
    <%=preparamemo(descripcionmensaje)%>
    </div>
    </td>
  </tr>
  <%if idcreador=session("codigo_usu") and vistaprevia="" then%>
  <tr>
    <td valign="top" width="85%" align="right" height="5%" colspan="2" class="bordesup">
    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
      <tr>
        <td width="61%"><%'=.MostrarCalificador(calificacionmaxima,tipocalificacion,session("tipofuncion"))%></td>
        <td width="39%" align="right">
    <img src="../../../images/editar.gif">&nbsp;<a href="Javascript:AbrirMensaje('M','<%=idforomensaje%>')">Modificar</a> 
        |
    <img src="../../../images/eliminar.gif">&nbsp;<a href="Javascript:AbrirMensaje('E','<%=idforomensaje%>','<%=idforo%>','<%=tituloforo%>','<%=idestadorecurso%>')">Eliminar</a> 
        | 
    <%call enviaremail("cursovirtual",session("idcursovirtual"),"2")%> Enviar 
        e-mail </td>
      </tr>
    </table>
    </td>
  </tr>  
  <%end if%>
  </table>
  <!--Cargar mensajes de respuesta-->
  <br>
  <%If IsEmpty(ArrRptas)=false then
	  for j=lbound(ArrRptas,2) to ubound(ArrRptas,2)%>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla" height="40%">
  <tr>
    <td valign="top" width="5%" bgcolor="#C7E0CE" rowspan="3">
    <%=mostrarfoto(ArrRptas(5,j),ArrRptas(4,j),"P")%>
    </td>
    <td valign="top" width="67%" bgcolor="#B9DBF7" height="10%">
    <b><%=ArrRptas(1,j)%></b><br>
    <%=ArrRptas(3,j)%> - Fecha de Registro: <%=ArrRptas(0,j)%>
   </td>
   	<%if session("idestadocursovirtual")=1 and vistaprevia="" then%>
    <td valign="top" width="11%" bgcolor="#B9DBF7" height="10%">
    <input type="button" value="  Responder" name="cmdresponder" class="boton4" onclick="AbrirMensaje('R','<%=idforo%>','<%=titulomensaje%>','<%=idforomensaje%>')">
    </td>
    <%end if%>
  </tr>
  <tr>
    <td width="85%" valign="top" height="85%" colspan="2">
     <div id="listadiv" style="height:100%">
    <%=preparamemo(ArrRptas(2,j))%>
    </div>
    </td>
  </tr>
  <%if ArrRptas(4,j)=session("codigo_usu") and vistaprevia="" then%>
  <tr>
    <td valign="top" width="85%" align="right" height="5%" colspan="2" class="bordesup">
    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
      <tr>
        <td width="61%"><%'=.MostrarCalificador(ArrRptas(8,j),ArrRptas(9,j),session("tipofuncion"))%></td>
        <td width="39%" align="right">
    <img src="../../../images/editar.gif">&nbsp;<a href="Javascript:AbrirMensaje('M','<%=idforomensaje%>')">Modificar</a> 
        |
    <img src="../../../images/eliminar.gif">&nbsp;<a href="Javascript:AbrirMensaje('E','<%=idforomensaje%>','<%=idforo%>','<%=tituloforo%>','<%=idestadorecurso%>')">Eliminar</a> 
        | 
    <%call enviaremail("cursovirtual",session("idcursovirtual"),"2")%> Enviar 
        e-mail </td>
      </tr>
    </table>
    </td>
  </tr>  
  <%end if%>
  </table>
  <br>
  	<%next
  end if%>
</body>
</html>
	<%end if
end with
Set foro=nothing
%>