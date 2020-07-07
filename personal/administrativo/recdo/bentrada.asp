<!--#include file="../../../funciones.asp"-->
<% 
	tipobus=request.querystring("tipobus")
	termino=request.querystring("termino")
	campo=request.querystring("campo")
	resultado=request.querystring("resultado")	
	
	termino=iif(termino="","0",trim(termino))
	campo=iif(campo="","0",trim(campo))
	if tipobus="" then tipobus=1
	total=0

if (trim(termino)<>"") then
	'response.write "Busqueda :" & tipobus & " Año : " & session("idanio") & " Campo : " & campo & " termino :"  & termino
	Set obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
		obj.AbrirConexion
		Set rsDocumentos= obj.Consultar("ConsultarArhivoDocumentario","FO",tipoBus,session("idanio"),campo,ReemplazarTildes(termino))
		obj.CerrarConexion
	set obj= Nothing
    
  	Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio
	
	ArrEncabezados=Array("Exp.","Fecha","Asunto","Procedencia","Destinatario")
	ArrCampos=Array("numeroexpediente","fecha","asunto","nombreprocedencia","nombredestinatario")
	ArrCeldas=Array("5%","15%","40%","20%","20%")
	ArrCamposEnvio=Array("idarchivo","idprocedencia","iddestinatario")
	pagina="detalledocumento.asp?modo=R"

	if Not(rsDocumentos.BOF and rsDocumentos.EOF) then
		alto="height=""95%"""
		resultado="S"
		total=rsDocumentos.recordcount
	else
		resultado="N"
	end if
end if
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>Bandeja de Entrada</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="private/validararchivo.js"></script>
<base target="Contenido">
<script type="text/javascript" language="Javascript">
function MostrarTitulo(num)
	{
		<%if tipobus=1 then%>
			lbltotal.innerHTML="Documentos recibidos (" + num + ") - <%=MonthName(month(date),False) & "  " & session("nombreanio")%>"
		<%else%>
			lbltotal.innerHTML="Documentos encontrados (" + num + ") año <%=session("nombreanio")%>"
		<%end if%>
	}
</script>
</head>
<body onload="MostrarTitulo('<%=total%>')">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="65%" id="lbltotal" class="usatTitulo">&nbsp;</td>
    <td width="35%" align="right" class="NoImprimir">
	<%
	if session("codigo_usu")=1024 or session("codigo_usu")=471 or session("codigo_usu")=466 then%>
	<a target="_self" href="../../../librerianet/recdo/frmregistrarexpedientes.aspx?idanio=<%=session("idanio")%>&idusuario=<%=session("codigo_usu")%>">Doc. Rectorado</a>
	<%end if%>

    <input type="button" class="agregar2" value="   Nuevo" OnClick="AbrirArchivo('0','agregararchivo')" NAME="cmdNuevo" > <input type="button" class="buscar2" value="Buscar" OnClick="AbrirBusqueda()" NAME="cmdBuscar" > </td>
  </tr>
</table>
<%if resultado="S" then%>
<input name="hdString" type="hidden">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%> id="tblmaestro">
<tr id="trLista" valign="top">
  		<td width="100%" height="50%" colspan="6">
  		<%
  		call CrearRpteTabla(ArrEncabezados,rsDocumentos,"",ArrCampos,ArrCeldas,"","I",pagina,"S",ArrCamposEnvio,"ResaltarPestana2('0','','')")
  		%>
  		</td>
	</tr>
<tr id="trDetalle" >
  		<td width="100%" height="45%" colspan="6">
			<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
				<tr height="8%">
					<td class="pestanaresaltada" id="tab" align="center" width="25%" onclick="ResaltarPestana2('0','','');AbrirFicha('detalledocumento.asp',this)">
                    Detalle del Documento</td>
					<td width="1%" height="10%" class="bordeinf">&nbsp;</td>
					<td class="pestanabloqueada" id="tab" align="center" width="25%" onclick="ResaltarPestana2('1','','');AbrirFicha('listamovimientos.asp',this)">
                    Movimientos del Documento</td>
                    <td width="1%" height="10%" class="bordeinf">&nbsp;</td>
                    <td width="45%" height="10%" class="bordeinf" align="right">
                    <img border="0" src="../../../images/maximiza.gif" style="cursor:hand" ALT="Maximizar ventana" onClick="Maximizar(this,'../../../','100%','65%')">
                    </td>
				</tr>
	  			<tr height="92%">
		    	<td width="100%" valign="top" colspan="10" class="pestanarevez">
					<span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Elija 
					el estudiante que desea verificar sus datos</span>
					<iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0">
					</iframe>
				</td>
			  </tr>
			</table>
  		</td>
  	</tr>
</table>
<%end if%>
</body>
</html>