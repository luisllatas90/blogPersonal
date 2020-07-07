<!--#include file="clstarea.asp"-->
<%
idtarea=request.querystring("idtarea")
numfila=request.querystring("numfila")

Set tarea=new clstarea
	with tarea
		.restringir=session("idcursovirtual")
		ArrTarea=.Consultar("2",idtarea,session("codigo_usu"),"")
	
		If IsEmpty(ArrTarea)=false then
			fechareg=ArrTarea(1,0)
			titulotarea=ArrTarea(3,0)
			fechainicio=ArrTarea(4,0)
			fechafin=ArrTarea(5,0)
			descripcion=ArrTarea(6,0)
			idcreador=ArrTarea(7,0)
			idtipopublic=ArrTarea(9,0)
			idestadorecurso=ArrTarea(10,0)
			idtipotarea=ArrTarea(13,0)
			descripciontipotarea=Arrtarea(15,0)
			permitirreenvio=Arrtarea(11,0)
			if permitirreenvio=1 then
				descripciontipotarea= descripciontipotarea & " (se registrarán todas las versiones del documento a realizar)"
			end if
			
			if idtipotarea=3 or idtipotarea=5 or idtipotarea=6 then
				pagina="frmadministrar.asp?idtipotarea=" & idtipotarea
			else
				pagina="frmtarea.asp?idtipotarea=" & idtipotarea
			end if
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Detalle de tarea</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validartarea.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
</head>
<body topmargin="0" leftmargin="0">
  <input type="hidden" id="txtelegido" value="<%=idtarea%>">
  <table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
    <tr>
      <td width="3%" rowspan="3" valign="top">
	<%if idcreador=session("codigo_usu") and session("idestadocursovirtual")=1 then%>
    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
      <tr>
        <td width="100%"><img style="cursor:hand" onClick="AbrirTarea('P')" src="../../../images/<%=iif(idtipopublic=3,"p1","todos")%>.gif" ALT="Haga click aquí para modificar los permisos al recurso"/>&nbsp;</td>
      </tr>
      <tr>
        <td width="100%"><img style="cursor:hand" onClick="AbrirTarea('M','<%=numfila%>','<%=pagina%>')" border="0" src="../../../images/editar.gif" ALT="Haga clic aquí para modificar la tarea"/>&nbsp;</td>
      </tr>
      <tr>
        <td width="100%"><img style="cursor:hand" onClick="AbrirTarea('E','<%=numfila%>')" border="0" src="../../../images/eliminar.gif" ALT="Haga clic aquí para Eliminar la tarea"/>&nbsp;</td>
      </tr>
      <tr>
        <td width="100%"><%call enviaremail("tarea",idtarea,idtipopublic)%>&nbsp;</td>
      </tr>
    </table>
    <%else%>
       <img src="../../../images/<%=iif(idtipopublic=3,"p1","menu0")%>.gif">
    <%end if%>
      </td>
      <td width="15%">Duración</td>
      <td width="72%">:&nbsp;<%=Fechainicio & " hasta " & Fechafin%></td>
      <td width="10%" rowspan="4" valign="top">
      <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
      <tr>
        <td width="100%" class="encabezadopregunta" align="center">
        	<%call .AbrirTarea(session("tipofuncion"),session("codigo_usu"),idestadorecurso,idtarea,titulotarea,idtipotarea,descripciontipotarea,permitirreenvio)%> &nbsp;</td>
      </tr>
      </table>
      </td>
    </tr>
    <tr>
      <td width="15%">Titulo de la tarea</td>
      <td width="72%" id="txttitulotarea">:&nbsp;<%=titulotarea%>&nbsp;</td>
    </tr>
    <tr>
      <td width="15%">Descripción</td>
      <td width="72%">:&nbsp;<%=PreparaMemo(descripcion)%></td>
    </tr>
    <tr>
      <td width="3%" valign="top">&nbsp;</td>
      <td width="15%">Tipo de tarea</td>
      <td width="72%">: <%=descripciontipotarea%></td>
    </tr>
  </table>
</body>
</html>
	<%end if
end with
Set tarea=nothing
%>