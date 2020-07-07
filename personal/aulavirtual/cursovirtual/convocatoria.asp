<%
	Set Obj= Server.CreateObject("AulaVirtual.clsUsuario")
		ArrDatos=Obj.ConsultarConvocatoria(session("idcursovirtual"),session("codigo_usu"))
		if IsEmpty(ArrDatos)=false then
			usuarioschat=ArrDatos(0,0)
			documentos=ArrDatos(1,0)
			encuestas=ArrDatos(2,0)			
			foros=ArrDatos(3,0)
			mensajes=ArrDatos(4,0)
			agenda=ArrDatos(5,0)
			tareas=ArrDatos(6,0)
		end if
	Set Obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Opciones según el cursovirtual</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<style fprolloverstyle>A:hover {color: #FF0000}
</style>
</head>
<body>
<table cellSpacing="0" cellPadding="3" width="100%" border="0" style="border-collapse: collapse" bordercolor="#111111" height="100%">
  <tr>
    <td width="65%" valign="top">
    <iframe name="fraweb" src="vistaprevia.asp" height="100%" width="100%" border="0" frameborder="0" target="_self" class="contorno">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe></td>
    <td width="25%" valign="top">
        <table cellSpacing="0" cellPadding="3" width="100%" border="0" class="contornotabla" style="border-collapse: collapse" bordercolor="#111111">
          <tr>
            <td colSpan="3" class="etabla2" style="text-align: left">Avisos</td>
          </tr>
          <tr>
            <td width="1">
            <img src="../../../images/menu1.gif"></td>
            <td  noWrap>Eventos del calendario para hoy</td>
            <td class="rojo" ><%=agenda%></span></td>
          </tr>
          <tr>
            <td width="1">
            <img src="../../../images/menu0.gif"></td>
            <td  noWrap><a href="../chat/index.asp">Usuarios en el chat</a></td>
            <td class="rojo"><%=usuarioschat%>&nbsp;</td>
          </tr>
          <tr>
            <td width="1">
            <img src="../../../images/menu2.gif"></td>
            <td  noWrap><a href="../foro/index.asp">Nuevos temas de discusión</a></td>
            <td class="rojo"><%=foros%>&nbsp;</td>
          </tr>
          <tr>
            <td width="1">
            <img src="../../../images/menu5.gif"></td>
            <td  noWrap><a href="../foro/index.asp">Mensajes nuevos sin responder</a></td>
            <td class="rojo" ><%=mensajes%></span></td>
          </tr>
          <tr>
            <td width="1">
            <img src="../../../images/menu6.gif"></td>
            <td  noWrap><a href="../evaluacion/index.asp">Encuestas pendientes</a></td>
            <td class="rojo" ><%=encuestas%></span></td>
          </tr>
          <tr>
            <td width="1">
            <img src="../../../images/menu7.gif"></td>
            <td  noWrap><a href="../documentos/index.asp?veces=1">Documentos nuevos sin revisar</a></td>
            <td class="rojo" ><%=documentos%></span></td>
          </tr>
          <tr>
            <td width="1">
            <img src="../../../images/nota.gif"></td>
            <td  noWrap><a href="../tareas/index.asp">Tareas por realizar</a></td>
            <td class="rojo" ><%=tareas%>&nbsp;</td>
          </tr>
        </table>
        </td>
  </tr>
  </table>
</body>
</html>