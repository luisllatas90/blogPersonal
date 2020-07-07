<!--#include file="../../../../funciones.asp"-->
<%
datos=request.QueryString
codigo_cco=Request.QueryString("codigo_cco")
criterio=trim(request.form("txtcriterio"))
campo=request.form("cbocampos")
tipoBD=request.form("cbotipoBD")
codigo_ped=session("codigo_ped")
if codigo_ped="" then codigo_ped=0

function claseTabla(ByVal codigo)
	if codigo=0 then
		claseTabla="class=""contornotabla"""
	else
		claseTabla="class=""pedido"""	
	end if
end function

if criterio<>"" then
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsLibros=Obj.Consultar("ConsultarBibliografiaASolicitar","FO",tipoBD,campo,ReemplazarTildes(criterio),codigo_ped)
		Obj.CerrarConexion
	Set obj=nothing

	if Not(rsLibros.BOF and rsLibros.EOF) then
		HayReg=true
		total=rsLibros.recordcount
	end if
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Buscar Bibliografía a Solicitar</title>
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/tooltip.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/validarpagina.js"></script>
<script type="text/javascript" language="JavaScript" src="../private/validarpedido.js"></script>
<style type="text/css">
.solicitar
{
	border: 1px solid #666666;
	background: #FEFFE1 url('../../../../images/carrito.gif') no-repeat 0% center;
	width: 80;
	font-family: Tahoma;
	font-size: 8pt;
	height: 20;
	cursor: hand;
	font-weight: bolder;
}

.pedido {
	border: thin groove #FF0000;
	background-position: center center;
	background-image: url('../../../../images/pedido.gif');
	background-repeat: no-repeat;
}

</style>
<script language="Javascript">
function AgregarCanasta()
{
	var tbl=document.getElementById("tblLibro" + Libro)
	tbl.className="pedido"
	var fila=tbl.getElementsByTagName('tr')
	var celda=fila[6].getElementsByTagName('td')
	celda[1].innerHTML="&nbsp;"
}
</script>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
</head>
<body onLoad="document.all.txtcriterio.focus()" style="background-color: #F0F0F0">
<table width="100%">
	<tr>
		<td style="width: 70%"><span class="usatTitulo">Búsqueda de bibliografía a solicitar</span></td>
		<td style="width: 30%" align="right">
		<input style="width:122px; height: 25px;" name="cmdNuevo" type="button" value="  Nueva bibliografía" class="agregar2" onClick="AbrirDetallePedido('N','<%=codigo_cco%>')" tooltip="<b>Nueva Bibliografía</b><br>Permite registrar nueva bibliografía, NO EXISTENTE en las bases de datos de Biblioteca">
		<input name="cmdRegresar" type="button" value="  Regresar" class="regresar2" style="width:80; height: 25;" onClick="location.href='frmpedido.asp?modo=M&codigo_cco=<%=codigo_cco%>'">
		</td>
	</tr>
</table>
<form name="frmbuscarLibros" method="post" action="frmbuscarbibliografia.asp?<%=datos%>">
<table width="100%" class="contornotabla" align="center">
	<tr>
		<td style="width: 3%"><b>Buscar:</b></td>
		<td style="width: 15%">
		<input name="txtcriterio" type="text" class="Cajas2" value="<%=criterio%>" maxlength="30"></td>
		<td style="width: 10%"><select name="cbocampos" class="Cajas2" >
		<option value="T" <%=SeleccionarItem("cbo",campo,"T")%>>Titulo</option>
<!--'	<option value="A" <%=SeleccionarItem("cbo",campo,"A")%>>Autor</option>
		<option value="E" <%=SeleccionarItem("cbo",campo,"E")%>>Editorial</option>
		<option value="F" <%=SeleccionarItem("cbo",campo,"F")%>>Edición</option>
		<option value="M" <%=SeleccionarItem("cbo",campo,"M")%>>Materia</option>
		<option value="R" <%=SeleccionarItem("cbo",campo,"R")%>>Tipo de Material</option>
-->		
		</select></td>
		<td style="width: 3%" align="right">en</td>
		<td style="width: 15%">
		<select name="cbotipoBD" class="Cajas2">
		<option value="C" <%=SeleccionarItem("cbo",tipoBD,"C")%>>Editoriales 
		(Catálogos)</option>
		<option value="L" <%=SeleccionarItem("cbo",tipoBD,"L")%>>Biblioteca USAT</option>
		</select></td>
		<td style="width: 3%" align="left" style="cursor:hand">
		<img src="../../../../images/menus/buscar_small.gif" onClick="BuscarBibliografia(frmbuscarLibros)">
		</td>
	</tr>
</table>
</form>
<span id="mensaje" class="rojo"></span>
<%if criterio<>"" and HayReg=true then%>
<input type="hidden" name="txtCodigo_cco" value="<%=request.querystring("codigo_cco")%>">

<table width="100%" class="contornotabla" bgcolor="white" height="83%" align="center">
	<tr height="95%">
		<td width="100%" valign="top">
		<div id="listadiv" style="height:100%; left: 3; top: 0;" class="NoImprimir">
		<table width="100%">
			<%
			i=0
			Do while not rsLibros.EOF
				if (i mod 2)=0 then
			%>
			<tr>
				<!--Primera columna-->
				<td style="width: 48%" valign="top">
					<table border="0" width="100%" <%=clasetabla(rsLibros("codigo_dpe"))%> id="tblLibro<%=rsLibros(0)%>">
					<tr class="piepagina" valign="top" >
						<td class="usatCeldaGris" colspan="2" align="right">
						<span class="etiqueta">Fecha de actualización: <%=rsLibros("FechaRegActual")%></span>						</td>
					</tr>
					<tr class="piepagina" valign="top" >
						<td style="width: 15%" class="usatCeldaGris">Título</td>
						<td style="width: 85%"> <%= rsLibros("titulo")%>&nbsp;</td>
					</tr>
					<tr class="piepagina" valign="top" >
						<td style="width: 15%" class="usatCeldaGris">Autor</td>
						<td style="width: 85%"><%=rsLibros("nombreautor")%>&nbsp;</td>
					</tr>
					<tr class="piepagina" valign="top" >
						<td style="width: 15%" class="usatCeldaGris">Editorial</td>
						<td style="width: 85%"><%=rsLibros("nombreeditorial")%>. <%=rsLibros("edicion")%>&nbsp;</td>
					</tr>
					<tr class="piepagina" valign="top" >
						<td style="width: 15%" class="usatCeldaGris">Existencias</td>
						<td style="width: 85%"><%=rsLibros("ejemplares")%>&nbsp;</td>
					</tr>
					<tr class="piepagina" valign="top" >
						<td style="width: 15%" class="usatCeldaGris">Precio Unit.</td>
						<td style="width: 85%"><%=rsLibros("precio")%>&nbsp;</td>
					</tr>
					<tr class="piepagina" valign="top" >
						<td style="width: 15%" class="usatCeldaGris">&nbsp;</td>
						<td style="width: 85%" align="right">
						<%
						if rsLibros("codigo_dpe")=0 then
						titulo=replace(rsLibros("titulo"),"""","") 
						%>
						  <input name="cmdSolicitar" type="button" value="    Solicitar" class="solicitar" 
						  <%if tipoBD ="C" then%> onClick="AbrirDetalleDeCatalogo('<%=codigo_cco%>','<%=titulo%>','<%=rsLibros("preciounit")%>','<%=rsLibros("moneda")%>')" <%else%>onClick="AbrirDetallePedidoLst('A','<%=rslibros(0)%>','<%=codigo_cco%>','<%=rsLibros("preciounit")%>','<%=rsLibros("moneda")%>')" <%end if%> >
						  						
						<%end if%>						</td>
					</tr>
					</table>
				</td>
				<!--Separador de tablas-->
				<td style="width: 3%">&nbsp;</td>
				<%else%>
				<!--Segund columna-->
				<td style="width: 48%" valign="top">
				<table border="0" width="100%" <%=clasetabla(rsLibros("codigo_dpe"))%> id="tblLibro<%=rsLibros(0)%>">
				<tr class="piepagina" valign="top" >
					<td class="usatCeldaGris" colspan="2" align="right">
					<span class="etiqueta">Fecha de actualización: <%=rsLibros("FechaRegActual")%></span>					</td>
				</tr>
				<tr class="piepagina" valign="top" >
					<td style="width: 15%" class="usatCeldaGris">Título</td>
					<td style="width: 85%"><%=rsLibros("titulo")%>&nbsp;</td>
				</tr>
				<tr class="piepagina" valign="top" >
					<td style="width: 15%" class="usatCeldaGris">Autor</td>
					<td style="width: 85%"><%=rsLibros("nombreautor")%>&nbsp;</td>
				</tr>
				<tr class="piepagina" valign="top" >
					<td style="width: 15%" class="usatCeldaGris">Editorial</td>
					<td style="width: 85%"><%=rsLibros("nombreeditorial")%>. <%=rsLibros("edicion")%>&nbsp;</td>
				</tr>
				<tr class="piepagina" valign="top" >
					<td style="width: 15%" class="usatCeldaGris">Existencias</td>
					<td style="width: 85%"><%=rsLibros("ejemplares")%>&nbsp;</td>
				</tr>
				<tr class="piepagina" valign="top" >
					<td style="width: 15%" class="usatCeldaGris">Precio Unit.</td>
					<td style="width: 85%"><%=rsLibros("precio")%>&nbsp;</td>
				</tr>
				<tr class="piepagina" valign="top" >
					<td style="width: 15%" class="usatCeldaGris">&nbsp;</td>
				  <td style="width: 85%" align="right"><% ''					Response.Write(Rslibros(0))   %>
					
					<%
						titulo=replace(rsLibros("titulo"),"""","") 
						%>
											  <input name="cmdSolicitar" type="button" value="    Solicitar" class="solicitar" 
						  <%if tipoBD ="C" then%> onClick="AbrirDetalleDeCatalogo('<%=codigo_cco%>','<%=titulo%>','<%=rsLibros("preciounit")%>','<%=rsLibros("moneda")%>')" <%else%>onClick="AbrirDetallePedidoLst('A','<%=rslibros(0)%>','<%=codigo_cco%>','<%=rsLibros("preciounit")%>','<%=rsLibros("moneda")%>')" <%end if%> ></td>
				</tr>
				</table>
				</td>
			</tr>
				<%
				end if
				i=i+1
				rsLibros.movenext
			Loop%>			
		</table>
		</div>
		</td>
	</tr>
	<tr height="5%" class="azul">
		<td class="bordesup" align="right" bgcolor="#FFF8DC">Resultados de búsqueda: <%=total%> coincidencias</td></tr>
</table>	
<%elseif criterio<>"" then%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp; No se han encontrado coincidencias según sus criterios de búsqueda</h5>
<%end if%>
</body>
</html>
