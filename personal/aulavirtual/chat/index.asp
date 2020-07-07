<%
Dim codigo_usu

codigo_usu=session("codigo_usu")
codigo_usu=replace(codigo_usu,"\","-")
codigo_usu=replace(codigo_usu,"/","-")

	Set Obj= Server.CreateObject("AulaVirtual.clsUsuario")
		ArrDatos=Obj.ConsultarConvocatoria(session("idcursovirtual"),session("codigo_usu"))
		if IsEmpty(ArrDatos)=false then
			usuarioschat=ArrDatos(0,0)
		end if
	Set Obj=nothing
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>iniciar sesion</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script>
	function AbrirChat()
	{
		<%if session("codigo_usu")<>"" then%>
		var izq = (screen.width-650)/2
   		var arriba= (screen.height-400)/2
   		var pagina="abrirchat.asp"

	   	var ventana=window.open(pagina,"frmchat","height=400,width=600,top=" + arriba +",left=" + izq + ",resizable=no");
	   	ventana.location.href=pagina
	   	ventana=null
	   	mensajechat.innerHTML="Haga clic en el menú CHAT, para actualizar esta página e iniciar una nueva sesión"
	   	totalconect.innerHTML='<%=usuarioschat+1%>'
	   	cmdiniciar.value="    Sesión Iniciada"
	   	cmdiniciar.disabled=true
	   	<%else%>
	   		top.location.href="../../../tiempofinalizado.asp"
	   	<%end if%>
	}
</script>
</head>

<body>
<center>
<h4>&nbsp;</h4>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="50%" height="311" class="contornotabla">
  <tr>
    <td class="etabla" height="22"><p class="e3" align="center">Chat en línea</p></td>
  </tr>
  <tr>
    <td align="center" height="14" class="etiqueta">Nombre de usuario</td>
  </tr>
  <tr>
    <td align="center" height="96" valign="top" class="e4"><%=session("nombre_usu")%>&nbsp;</td>
  </tr>
  <tr>
    <td align="center" height="39" valign="top"><img border="0" src="../../../images/iniciando.gif"></td>
  </tr>
  <tr>
    <td align="center" height="21" id="mensajechat">Para acceder haga clic en el botón</td>
  </tr>
  <tr>
    <td align="center" valign="top" height="25">
<input type="button" onclick="AbrirChat()" value="   Iniciar sesión" id="cmdiniciar" class="buscar" style="width: 120"></td>
  </tr>
  <tr>
    <td align="center" height="52" valign="top" class="rojo">[ Total de usuarios conectados: <span id="totalconect"><%=usuarioschat%></span> ]</td>
  </tr>
</table>
</center>
</body>

</html>