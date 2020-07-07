<!--#include file="clstarea.asp"-->
<%
idtarea=request.querystring("idtarea")
titulotarea=request.querystring("titulotarea")
idtipotarea=request.querystring("idtipotarea")
descripciontipotarea=request.querystring("descripciontipotarea")
permitirreenvio=request.querystring("permitirreenvio")
modo=request.querystring("modo")
idestadotarea=request.querystring("idestadotarea")

Set tarea=new clstarea
	with tarea
		.restringir=session("idcursovirtual")
		num=iif(session("tipofuncion")=3,"6","5")
		ArrParticipantes=.Consultar(num,idtarea,idtipotarea,session("codigo_usu"))
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>realizar tarea</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validartarea.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
</head>
<body>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="82%" class="e4" valign="top">Tarea: <%=titulotarea%>&nbsp;</td>
    <%if modo="" then%>
    <td width="18%" valign="top" align="right">
    <%if idestadotarea=3 then%>
    <span class=rojo>La tarea ha sido bloqueda para enviar nuevos archivos</span>
    <%else%>
	<input OnClick="Realizartarea('A',txtidestadotareagral,'<%=idtarea%>')" type="button" value="  Enviar tarea" name="cmdCancelar" id="cmdagregar" class="nuevo">
	<%end if%>
	</td>
    <%end if%>
  </tr>
</table>
<input type="hidden" id="txtidestadotareagral" value="0">
<%If isEmpty(ArrParticipantes) then%>
	<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se han registrado envios de la tarea asignada</p>
<%else%>
<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>Tipo 
de tarea: <%=descripciontipotarea%></b><br>Haga clic en el participantes, para 
visualizar los archivos que ha registrado</p>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" id="AutoNumber2">
  <tr class="etabla">
    <td width="5%">&nbsp;</td>
    <td width="25%">Fecha de Registro</td>
    <td width="75%">Apellidos y Nombres</td>
    <td width="25%">Observaciones</td>
  </tr>
  <%for i=lbound(ArrParticipantes,2) to ubound(ArrParticipantes,2)%>
  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
    <td width="5%"><%=i+1%>&nbsp;</td>
    <td width="25%"><%=ArrParticipantes(4,i)%>&nbsp;</td>
    <td width="75%"><%=.AbrirTareaRealizada(permitirreenvio,idtarea,titulotarea,ArrParticipantes(0,i),ArrParticipantes(3,i),ArrParticipantes(1,i))%>&nbsp;</td>
    <td width="25%" align="center""><%=ArrParticipantes(2,i)%>&nbsp;</td>
  </tr>
  	<%next%>
  </table>
<%end if%>
</body>
</html>
	<%end with
Set tarea=nothing
%>