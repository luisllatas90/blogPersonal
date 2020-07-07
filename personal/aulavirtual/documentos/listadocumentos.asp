<!--#include file="clsdocumento.asp"-->
<%
Dim IdDocMarcado,scriptResalte

IdCarpeta=request.querystring("IdCarpeta")
IdDocMarcado=request.querystring("IdDocMarcado")
numfila=request.querystring("numfila")

If IdCarpeta<>"" then
	Dim documento
	Dim Arrdocumento
	
	Set documento=new clsdocumento
		with documento
			.restringir=session("idcursovirtual")
			ArrDocumento=.Consultar("2",session("codigo_usu"),IdCarpeta,session("Idcursovirtual"),"")
			
			if numfila<>"" and IsEmpty(Arrdocumento)=false then
				scriptResalte="onLoad=""ResaltarArchivo('" & numfila & "','" & IdDocMarcado & "')"""
			end if
		end with
	Set documento=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de documentos</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validardocumento.js"></script>
<base target="listadocs">
</head>
<body topmargin="0" leftmargin="2" <%=scriptResalte%>>
<input type="hidden" id="txtelegido">
<%If IsEmpty(Arrdocumento)=true then%>
	<h5 class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se han encontrado documentos en la carpeta seleccionada</h5>
<%else%>
<table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#666666" width="100%" height="100%">
  <tr class="fondonulo">
    <td width="70%" colspan="2" height="3%"><b>&nbsp;</b><b>Nombre del archivo</b></td>
    <td width="30%" height="3%">
    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
	  <tr>
    	<td width="95%"><b>&nbsp;Fecha de registro</b></td>
	    <td width="5%">
        <img style="cursor:hand" border="0" onclick="OcultarCarpetas(this)" src="../../../images/maximiza.gif" width="18" height="18"></td>
	  </tr>
	</table></td>
  </tr>
  <tr>
  <td width="100%" align="center" colspan="4" valign="top" height="50%">
  <DIV id="listadiv" style="height:100%;">
  <table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tbllistadocumentos">	  
  <%for I=lbound(ArrDocumento,2) to Ubound(ArrDocumento,2)%>
  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onClick="ResaltarArchivo('<%=I%>','<%=ArrDocumento(0,i)%>')" valign="top">
    <td width="3%" height="10px"><img border='0' src="../../../images/ext/<%=right(ArrDocumento(3,I),3)%>.gif"/>&nbsp;</td>
    <td width="67%" height="10px"><%=ArrDocumento(4,I)%>&nbsp;</td>
    <td width="30%" height="10px"><%=lcase(ArrDocumento(1,I))%>&nbsp;</td>
  </tr>
  <%next%>
  </table>
  </DIV>  
  </td></tr>
  <tr>
  	<td width="100%" colspan="3" height="3%">
  	<table cellSpacing="0" cellPadding="3" width="100%" border="0" style="border-collapse: collapse" bordercolor="#111111" height="100%">
  	<tr>
    <td class="paginaDoc" width="65%"><img style="cursor:hand" border="0" src="../../../images/menos.gif" onclick="MostrarTabla(detalledoc,'../../../images/',this)"> Detalle del Archivo</td>
    <td width="25%" class="azul" background="../../../images/fondopestana2.gif" align="right" valign="top"><b><%=ubound(ArrDocumento,2)+1%> archivos</b></td>
  	</tr>
  	</table> 	
  	</td>
  </tr>
  <tr id="detalledoc">
  	<td width="100%" colspan="3" height="34%"><span class="sugerencia" id="mensajedetalledoc">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Actualmente no hay ningún documento seleccionado de la Lista.</span>
  	<iframe name="fradetalle" height="100%" width="100%" border="0" frameborder="0">
    El explorador no admite los marcos flotantes o no está configurado actualmente para mostrarlos.</iframe>
  	</td>
  </tr>
</table>
<%end if%>
</table>
</h1>
</table>
</h1>
</body>
</html>
<%end if%>