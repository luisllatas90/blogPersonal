<!--#include file="clscursovirtual.asp"-->
<%
codigo_apl=request.querystring("codigo_apl")
codigo_tfu=request.querystring("codigo_tfu")
idestadorecurso=request.querystring("idestadorecurso")
if idestadorecurso="" then idestadorecurso=1
varAplicacion=Request.ServerVariables("QUERY_STRING")
varAplicacion=replace(varAplicacion,"idestadorecurso=1","")
varAplicacion=replace(varAplicacion,"idestadorecurso=3","")
varAplicacion="&" & varAplicacion
varAplicacion=replace(varAplicacion,"&&","&")
	dim curso
	Set curso=new clscursovirtual
		with curso
			ArrDatos=.Consultar("2",session("codigo_usu"),codigo_apl,idestadorecurso)
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Actividades Académicas e Institucionales</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarcurso.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
</head>
<body topmargin="0" leftmargin="0">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
  <tr>
    <td valign="top" class="barraherramientas" height="5%" width="50%" align="right">
    <b>Mostrar</b>:<select  name="cboidestadorecurso" onChange="actualizarlista('listacursos.asp?idestadorecurso=' + this.value + '<%=varAplicacion%>')">
    <option value="1" <%=Seleccionar(idestadorecurso,"1")%>>En proceso</option>
    <option value="3" <%=Seleccionar(idestadorecurso,"3")%>>Finalizado</option>
    </select>&nbsp;  
    </td>
  </tr>
<%If IsEmpty(Arrdatos)=false then%>  
  <tr><td width="100%" height="95%" valign="top">
   <DIV id="listadiv" style="height:100%;">
  <input type="hidden" id="txtelegido">
   <table width="100%" style="border-collapse: collapse" bordercolor="#111111" cellpadding="3" cellspacing="0">
<%for i=Lbound(Arrdatos,2) to Ubound(Arrdatos,2)%>
  <tr>
    <td width="2%" valign="top">
    <img border="0" src="../../../images/vineta.gif"></td>
    <td width="65%" valign="top" id="curso<%=Arrdatos(0,i)%>"><a TARGET="_top" title="<%=Arrdatos(4,i)%>" href="abrircurso.asp?Idcursovirtual=<%=Arrdatos(0,I)%>&nombrecursovirtual=<%=Arrdatos(3,I)%>&idestadorecurso=<%=Arrdatos(5,I)%>&fechainicio=<%=Arrdatos(1,I)%>&fechafin=<%=Arrdatos(2,I)%>&creador=<%=Arrdatos(7,I)%>&tipofuncion=<%=Arrdatos(9,I)%>&descripciontipofuncion=<%=Arrdatos(10,I)%>&creartemas=<%=Arrdatos(11,I)%>&temapublico=<%=Arrdatos(12,I)%>&integrartematarea=<%=Arrdatos(13,I)%>&integrarrptatarea=<%=Arrdatos(14,I)%>&numusuarios=<%=Arrdatos(15,I)%><%=varAplicacion%>"><%=Arrdatos(3,I)%></a>&nbsp;</td>
    <td width="30%" valign="top" class="azul">Duración: <%=FormatDateTime(Arrdatos(1,I),2)%> - <%=FormatDateTime(Arrdatos(2,I),2)%>&nbsp;</td>
    <td width="3%" valign="top"><%if Arrdatos(7,I)=session("codigo_usu") then%><img style="cursor:hand" src="../../../images/propiedades.gif"/ onclick="AbrirCurso('M','<%=Arrdatos(0,i)%>','<%=codigo_apl%>','<%=codigo_tfu%>')"><%end if%></td>
  </tr>
<%next%>
	</td></tr></table></DIV>
<%else
	response.Write ("<tr><td valign=top><b>No se encontraron registros</b>&nbsp;</td></tr>")
end if%>
</table>
</body>
</html>
	<%end with
Set curso=nothing%>