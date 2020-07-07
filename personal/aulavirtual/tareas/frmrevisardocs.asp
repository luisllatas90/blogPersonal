<!--#include file="clstarea.asp"-->
<%
idtarea=request.querystring("idtarea")
titulotarea=request.querystring("titulotarea")
idtipotarea=request.querystring("idtipotarea")
descripciontipotarea=request.querystring("descripciontipotarea")

Set tarea=new clstarea
	with tarea
		.restringir=session("idcursovirtual")
		ArrRecursos=.Consultar("3",idtarea,session("codigo_usu"),"")
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
</head>
<body>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="82%" class="e4" valign="top">Tarea: <%=titulotarea%>&nbsp;</td>
    <td width="18%" valign="top" align="right"> <input OnClick="location.href='index.asp'" type="button" value="Cancelar" name="cmdCancelar" id="cmdCancelar" class="salir"></td>
  </tr>
</table>
<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b>Tipo 
de tarea: <%=descripciontipotarea%></b><br>Haga clic en el recurso para realizar la tarea</p>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" id="AutoNumber2">
  <tr class="etabla">
    <td width="5%">&nbsp;</td>
    <td width="85%">Descripción del recurso</td>
    <td width="10%">Estado de tarea</td>
  </tr>
  <%If isEmpty(ArrRecursos)=false then
  	for i=lbound(Arrrecursos,2) to ubound(Arrrecursos,2)%>
  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onClick="Realizartarea('D',txtidestadotarea<%=i%>,'<%=idtarea%>','<%=ArrRecursos(0,i)%>','<%=ArrRecursos(5,i)%>',txtestadotarea<%=i%>)">
    <td width="5%"><%=i+1%><input type="hidden" id="txtidestadotarea<%=i%>" value="<%=Arrrecursos(8,i)%>">&nbsp;</td>
    <td width="85%"><%=.ObtenerTipoIcono(ArrRecursos(5,i))%>&nbsp;<%=ArrRecursos(4,i)%>&nbsp;</td>
    <td width="10%" id="txtestadotarea<%=i%>" align="center" class="<%=iif(Arrrecursos(8,i)=0,"rojo","azul")%>"><%=iif(Arrrecursos(8,i)=0,"Pendiente","Realizada")%>&nbsp;</td>
  </tr>
  	<%next
  end if%>
  </table>

</body>

</html>
	<%end with
Set tarea=nothing
%>