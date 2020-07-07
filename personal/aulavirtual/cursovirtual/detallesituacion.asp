<!--#include file="../../../funcionesaulavirtual.asp"-->
<%
idusuario=request.querystring("idusuario")
idcursovirtual=session("idcursovirtual")
modo=request.querystring("modo")
if modo="" then num=1
	
buscar=true
'Obtener datos del usuario

Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
obj.AbrirConexion
	Set rsusuario=obj.Consultar("ConsultarUsuario","FO",8,idusuario,0,0)

If Not(rsusuario.BOF and rsusuario.EOF) then
	HayReg=true
	Set rsRecursos=obj.Consultar("ConsultarDesempeno","FO",0,idusuario,idcursovirtual,0)
	Set rsVisitas=obj.Consultar("ConsultarDesempeno","FO",1,idusuario,idcursovirtual,0)
End if
obj.CerrarConexion
Set obj=nothing

If HayReg=true then
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Detalle</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css" media="print"/>
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
</head>

<body>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="68%" class="e4">Desempeño del Participante</td>
    <td width="44%" align="right" class="NoImprimir"><%call enviarimpresion(modo,2)%></td>
  </tr>
</table>
<br>
<table cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla">
  <tr>
    <td width="28%" class="etiqueta" valign="top">&nbsp;Código</td>
    <td width="62%" valign="top">:&nbsp;<%=rsUsuario(0)%></td>
    <td width="15%" rowspan="5" valign="top">
     <%'=mostrarfoto(rsUsuario(4),rsUsuario(5),"N")%></td>
  </tr>
  <tr>
    <td width="28%" class="etiqueta" valign="top">&nbsp;Apellidos y Nombres</td>
    <td width="62%" valign="top">:&nbsp;<%=rsUsuario(1)%></td>
  </tr>
  <tr>
    <td width="28%" class="etiqueta" valign="top">&nbsp;email</td>
    <td width="62%" valign="top">:&nbsp;<%=rsUsuario(2)%></td>
  </tr>
  <tr>
    <td width="28%" class="etiqueta" valign="top">&nbsp;</td>
    <td width="62%" valign="top">&nbsp;</td>
  </tr>
  <tr>
    <td width="28%" class="etiqueta" valign="top">&nbsp;</td>
    <td width="62%" align="right" valign="top" class="NoImprimir"></td>
  </tr>
</table>
<p class="e4">Acceso al curso</p>
<%If Not(rsVisitas.BOF and rsVisitas.EOF) then%>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="50%">
    <tr class="etabla">
     <td width="5%">#</td>
      <td width="40%">Fecha de Ingreso</td>
      <td width="5%">Visitas</td>
    </tr>
    <%Do while Not rsVisitas.EOF
    	i=i+1%>
    <tr>
      <td width="5%"><%=i%>&nbsp;</td>
      <td width="40%"><%=rsVisitas("FechaEntrada")%>&nbsp;</td>
      <td width="5%"><%=rsVisitas("visitas")%>&nbsp;</td>
    </tr>
	    <%rsVisitas.movenext
	Loop%>
  </table>
	<%	
	Set rsVisitas=nothing
else%>
<h5>No se han registrado visitas al curso</h5>
<%end if%>

<p class="e4">Acceso a recursos</p>
<%If Not(rsRecursos.BOF and rsRecursos.EOF) then%>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="90%">
    <tr class="etabla">
     <td width="5%">#</td>
      <td width="30%">Recurso</td>
      <td width="60%">Descripción</td>
      <td width="5%">Veces</td>
    </tr>
    <%
    i=0
    Do while Not rsRecursos.EOF
    	i=i+1%>
    <tr>
      <td width="5%"><%=i%>&nbsp;</td>
      <td width="30%"><%=rsRecursos("recurso")%>&nbsp;</td>
      <td width="60%"><%=rsRecursos("nombrerecurso")%>&nbsp;</td>
      <td width="5%"><%=rsRecursos("veces")%>&nbsp;</td>
    </tr>
    	<%	rsRecursos.movenext
		Loop
		%>
  </table>
<%else%>
<h5>No se han encontrado acceso a los recursos publicados</h5>
<%end if

Set rsRecursos=nothing		
%>
</body>
</html>
<%end if%>