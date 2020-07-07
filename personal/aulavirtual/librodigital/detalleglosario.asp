<!--#include file="clslibrodigital.asp"-->
<%
Dim modo,idlibrodigital,letra
Dim modalidad,campo,termino

modo=request.querystring("modo")
idlibrodigital=request.querystring("idlibrodigital")
letra=request.querystring("letra")
modalidad=request.querystring("modalidad")
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Pagina nueva 1</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarlibrodigital.js"></script>
</head>
<body topmargin="0" leftmargin="0">
<%
if idlibrodigital="" then%>
	<h5 class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp; Haga click en la letra según el término que desea buscar</h5>
<%else
	Set contenido=new clslibrodigital
		with contenido
			.restringir=session("idcursovirtual")
			if modalidad="busqueda" then
				termino=request.querystring("termino")
				campo=request.querystring("campo")
				if (campo="%%%%%" OR campo="") then
					campo="descripcion"
					termino="%%%%%"
				end if
				ArrDatos=.consultar("9",idlibrodigital,campo,termino)
				letra="Resultados de búsqueda de la palabra "" " & termino & " """
			else
				ArrDatos=.consultar("6",idlibrodigital,letra,"")
			end if
		end with
	Set contenido=nothing
	
	if IsEmpty(ArrDatos)=true then%>
		<h5>&nbsp;&nbsp;No se han registrado términos en el glosario</h5>
	<%else%>
		<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%">
		  <tr><td width="100%" colspan="3" class="etabla2"><%=letra%>&nbsp;</td></tr>
		  <%for i=lbound(Arrdatos,2) to Ubound(ArrDatos,2)%>
		  <tr>
		  <%if modo="administrar" then%>
		  <td class="bordeinf" width="3%" valign="top">
		  <img style="cursor:hand" onClick="AbrirGlosario('M','<%=Arrdatos(0,i)%>')" border="0" src="../../../images/editar.gif" ALT="Haga clic aquí para modificar el término del glosario"/>
		  <img style="cursor:hand" onClick="AbrirGlosario('E','<%=Arrdatos(0,i)%>')" border="0" src="../../../images/eliminar.gif" ALT="Haga clic aquí para Eliminar el término del glosario"/>
		  &nbsp;</td>
		  <%end if%>
		  <td class="bordeinf" width="27%" valign="top"><b><%=Arrdatos(1,i)%></b>&nbsp;</td>
		  <td class="bordeinf" width="70%" valign="top"><%=PreparaMemo(Arrdatos(2,i))%>&nbsp;</td>
		  </tr>
		  <%next%>
		</table>	
	<%end if
end if%>
</body>
</html>