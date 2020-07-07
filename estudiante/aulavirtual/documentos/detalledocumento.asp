<!--#include file="clsdocumento.asp"-->
<%
idDocumento=request.querystring("idDocumento")
numfila=request.querystring("numfila")

Set documento=new clsdocumento
	with documento
		.restringir=session("idcursovirtual")
		ArrDocumento=.Consultar("3",IdDocumento,"",session("idcursovirtual"),"")
		
		If IsEmpty(ArrDocumento)=false then
			tipodoc=ArrDocumento(1,0)
			archivo=ArrDocumento(2,0)
			titulodocumento=ArrDocumento(3,0)
			idcreador=ArrDocumento(4,0)
			fechainicio=ArrDocumento(5,0)
			fechafin=ArrDocumento(6,0)
			descripcion=ArrDocumento(7,0)
			estado=ArrDocumento(8,0)
			refIdDocumento=ArrDocumento(9,0)
			fechareg=ArrDocumento(11,0)
			fechamod=ArrDocumento(12,0)
			idtipopublic=ArrDocumento(13,0)
			versiondoc=Arrdocumento(14,0)
			idestadorecurso=Arrdocumento(16,0)
			creador=Arrdocumento(32,0)
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Detalle de documento</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validardocumento.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
</head>
<body topmargin="0" leftmargin="0">
  <input type="hidden" id="txttitulodocumento" value="<%=titulodocumento%>">
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td valign="top"  rowspan="5" width="5%">
    <%if idcreador=session("codigo_usu") and session("idestadocursovirtual")=1 then%>
    <img style="cursor:hand" onClick="PermisosDoc('<%=idDocumento%>')" src="../../../images/<%=iif(idtipopublic=3,"p1","todos")%>.gif" ALT="Haga click aquí para modificar los permisos al recurso"/><br>
    <img style="cursor:hand" onClick="ModificarDoc('<%=refIdDocumento%>','<%=idDocumento%>','<%=tipodoc%>','<%=numfila%>')" border="0" src="../../../images/editar.gif" ALT="Haga clic aquí para modificar el documento"/><br><br>
    <%'call enviaremail("documento",idDocumento,idtipopublic)
	else%><br>
    	   <img src="../../../images/<%=iif(ArrDocumento(10,i)=3,"p1","menu0")%>.gif">
    <%end if%>
   </td>
    <td width="25%" valign="top" class="etiqueta">Duración</td>
    <td valign="top" width="70%">:&nbsp;<%=Fechainicio & " hasta " & Fechafin%>&nbsp;</td>
  </tr>
  <tr>
    <td width="25%" valign="top" class="etiqueta">Nombre de archivo</td>
    <td valign="top" width="70%" class="azul">:&nbsp;<%=titulodocumento%>&nbsp;</td>
  </tr>
  <%if descripcion<>"" then%>
  <tr>
    <td width="25%" valign="top" class="etiqueta">Descripción</td>
    <td valign="top" width="70%">:&nbsp;<%=PreparaMemo(descripcion)%>&nbsp;</td>
  </tr>
  <%end if
  if tipodoc="L" then%>
  <tr>
    <td width="25%" valign="top" class="etiqueta">Páginas web</td>
    <td valign="top" width="70%"><%=.CargarEnlaces(iddocumento)%>&nbsp;</td>
  </tr>
  <%end if%>
  <tr>
    <td width="25%" valign="top" class="etiqueta">Registrado por</td>
    <td valign="top" width="70%">:&nbsp;<%=creador%></td>
  </tr>
  <%
  if tipodoc<>"L" then
  	link="<img border='0' src='../../../images/ext/" & right(archivo,3) & ".gif'>&nbsp;Descargar documento"
    punto="TARGET=""_blank"" "
    
    if tipodoc="P" then
  	    link="<img border='0' src='../../../images/ext/" & right(archivo,3) & ".gif'>&nbsp;Abrir Página Web"
  	    'punto=""
    end if
  %>
  <tr style="cursor:hand">
    <td valign="top" width="5%">&nbsp;</td>
    <td width="95%" valign="top" colspan="2" align="right">
	<%
	response.write("<a " & punto & " href=descargar.asp?tipodoc=" & tipodoc & "&tblrecurso=documento&Doc=" & iddocumento & "&Ruta=" & archivo & ">" & link & "</a>")
	%></td>
  </tr>
  <%end if%>
</table>
</body>
</html>
	<%end if
end with
Set documento=nothing
%>