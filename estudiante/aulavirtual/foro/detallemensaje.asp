<!--#include file="clsforo.asp"-->
<%
idforo=request.querystring("idforo")
idforomensaje=request.querystring("idforomensaje")
tituloforo=request.querystring("tituloforo")
idestadorecurso=request.querystring("idestadorecurso")
vistaprevia=request.querystring("vistaprevia")

Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")

function CargarFoto(byval idtipo,byval usuario,Byval modo)
	Dim alto,ancho
	alto=" height=""143"" "
	ancho=" width=""108"" "
	
	if modo="P" then
		alto=" height=""65"" "
		ancho=" width=""50"" "
	end if
	if trim(idtipo)="2" then
		usuario=obEnc.CodificaWeb("069" & usuario)
		CargarFoto="<img border=""0"" src=""http://www.usat.edu.pe/imgestudiantes/" & usuario & """ " & alto & ancho & ">"
	else
		CargarFoto="<img border=""0"" src=""../../imgpersonal/" & usuario & """ " & alto & ancho & ">"
	end if
end function

	if session("idcursovirtual")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

		Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
			Obj.AbrirConexion
			Set Arrforo=Obj.Consultar("ConsultarForo","FO",6,idforomensaje,0,0)
			Set ArrRptas=Obj.Consultar("ConsultarForo","FO",7,idforomensaje,0,0)
			Obj.CerrarConexion
	  	Set Obj= Nothing
		
		If Not(arrForo.BOF and arrForo.EOF) then
			fechareg=ArrForo("fechareg")
			titulomensaje=ArrForo(1)
			descripcionmensaje=ArrForo(2)
			nombreusuario=ArrForo(3)
			idcreador=ArrForo(4)
			idtipousuario=ArrForo(5)
			calificacionmaxima=ArrForo(7)
			tipocalificacion=ArrForo(8)
			foto=ArrForo(9)
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
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
    <%=CargarFoto(idtipousuario,foto,"P")%>
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
  <%If Not(ArrRptas.BOF and ArrRptas.EOF) then
	  Do while Not ArrRptas.EOF%>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla" height="40%">
  <tr>
    <td valign="top" width="5%" bgcolor="#C7E0CE" rowspan="3">
    <%=CargarFoto(ArrRptas(5),ArrRptas(10),"P")%>
    </td>
    <td valign="top" width="67%" bgcolor="#B9DBF7" height="10%">
    <b><%=ArrRptas(1)%></b><br>
    <%=ArrRptas(3)%> - Fecha de Registro: <%=ArrRptas(0)%>
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
    <%=preparamemo(ArrRptas(2))%>
    </div>
    </td>
  </tr>
  <%if ArrRptas(4)=session("codigo_usu") and vistaprevia="" then%>
  <tr style="display:none">
    <td valign="top" width="85%" align="right" height="5%" colspan="2" class="bordesup">
    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
      <tr>
        <td width="61%">...</td>
        <td width="39%" align="right">
    <img src="../../../images/editar.gif">&nbsp;<a href="Javascript:AbrirMensaje('M','<%=ArrRptas("idforomensaje")%>')">Modificar</a> 
        |
    <img src="../../../images/eliminar.gif">&nbsp;<a href="Javascript:AbrirMensaje('E','<%=ArrRptas("idforomensaje")%>','<%=idforo%>','<%=tituloforo%>','<%=idestadorecurso%>')">Eliminar</a> 
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
  	<%
  		ArrRptas.movenext
  	Loop
  end if%>
</body>
</html>
	<%end if
Set foro=nothing
Set ArrRptas=nothing

Set obEnc=nothing
%>