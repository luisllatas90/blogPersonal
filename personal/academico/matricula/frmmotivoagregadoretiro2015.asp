<!--#include file="../../../funciones.asp"-->
<%
ruta=request.querystring("ruta")
accion=request.querystring("accion")
' accion puede ser agregado, retiro , modificacion
codigo_dma	=	request.querystring("codigo_dma")
motivo_actual	=	request.querystring("motivoactual")
obsactual       =	request.querystring("obsactual")
tipo_mar=request.querystring("tipo_mar")
codigo_pes=request.querystring("codigo_pes")
mensajeboton="Guardar"
mensajetitulo="Retiro"
if tipo_mar="A" then
	mensajeboton="       Siguiente  "
	mensajetitulo="Agregado"
end if

Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	Set rsMotivo=Obj.Consultar("ConsultarMotivosAgregadoRetiro","FO",0,tipo_mar,0,0)
	'codigo_mar=rsMotivo("codigo_motivo")
	'obs=rsMotivo("obsmotivo")
	obj.CerrarConexion
Set obj=nothing

%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Motivos de Agregado/Retiro</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="Javascript">
	function ValidarMotivo()
	{
		
		if (frmmotivo.cbocodigo_mar.value==-2){
			alert("Debe elegir un motivo para guardar")
			frmmotivo.cbocodigo_mar.focus()
			return(false)
		}
		else{

			
				frmmotivo.submit();
			
			
		}
	}
</script>
</head>

<body bgcolor="#EEEEEE">
<form name="frmmotivo" method="POST" action="<%=ruta%>/procesarmatricula2015.asp?accion=<%=accion%>&codigo_dma=<%=codigo_dma%>&tipo_mar=<%=tipo_mar%>"  >
<p class="usattitulo">Especificar el motivo del <%=mensajetitulo%></p>

<table class="contornotabla" border="0" cellpadding="3" cellspacing="3" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr>
    <td width="100%" bgcolor="#E1F1FB" colspan="2"><u><b>Situación Actual</b></u></td>
  </tr>
  <tr>
    <td width="22%">Motivo actual:</td>
    <td width="78%"><%=motivo_actual%></td>
  </tr>
  <tr>
    <td width="22%" valign="top">Observación:</td>
    <td width="78%">
      <textarea disabled="true" class="cajas2" rows="5" name="txtobsactual" cols="20"><%=obsactual%></textarea>
   </td>
  </tr>
  <tr>
    <td width="100%" bgcolor="#E1F1FB" colspan="2"><u><b>Nueva Información</b></u></td>
  </tr>
  <tr>
    <td width="22%">Motivo:</td>
    <td width="78%">
      <%call llenarlista("cbocodigo_mar","",rsMotivo,"codigo_mar","descripcion_mar",codigo_mar,"Seleccione el motivo","","")%> &nbsp;</td>
  </tr>
  <tr>
    <td width="22%" valign="top">Observación:</td>
    <td width="78%">
      <textarea class="cajas2" rows="5" name="txtobs" cols="20"><%=obs%></textarea>
   </td>
  </tr>
  <tr>
    <td width="22%">&nbsp;</td>
    <td width="78%" align="center">
    &nbsp;</td>
  </tr>
</table>
<p align="center">
    <input class="guardar_prp" type="button" value="<%=mensajeboton%>" name="cmdguardar" onclick="ValidarMotivo()"><input class="noconforme1" type="button" value="        Cerrar" name="cmdCancelar" onclick="javascript:window.close()" >
</p>
</form>
<script type="text/javascript" language="JavaScript" src="../private/analytics-personal.js"></script>
</body>
</html>