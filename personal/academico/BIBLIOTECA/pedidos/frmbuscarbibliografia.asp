<!--#include file="../../../../funciones.asp"-->

<SCRIPT>
function BuscarBibliografia(frm)
{
	if (frm.txtcriterio.value.length<3){
		alert("Por favor especifique el término de búsqueda")
		frm.txtcriterio.focus()
		return(false)
	}
	//mensaje.innerHTML="<b>Espere un momento...</b>"
	AbrirMensaje('../../../../images/')
	
	frm.submit()
}

function AbrirDetallePedidoLst(modo,param1,codigo_cco,precio, moneda)
{
//	var codigo_cco=0
	var pagina=""	
//	alert(param1)
	switch(modo)
		{
		case "A":
				//var codigo_cco=document.all.cboCodigo_cco.value		
				var tipoBD=document.all.cbotipoBD
				var textoBD=tipoBD.options[tipoBD.selectedIndex].text
				var Libro=null
				pagina="frmdetallepedido.asp?accion=agregardetallepedido&tipoBD=" + tipoBD.value + "&textoBD=" + textoBD + "&idLibro=" + param1 + "&codigo_cco=" + codigo_cco+"&precio="+ precio + "&moneda=" + moneda
				AbrirPopUp(pagina,'400','590')
//				showModalDialog(pagina,window,"dialogWidth:590px;dialogHeight:400px;status:no;help:no;center:yes")
				break	

		}
}
</script>
<%
datos=request.QueryString
codigo_cco=Request.QueryString("codigo_cco")
criterio=trim(request.form("txtcriterio"))
campo=request.form("cbocampos")
tipoBD=request.form("cbotipoBD")
codigo_ped=session("codigo_ped")
codigo_ped=""
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
	Set Obj=nothing

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
	  <td width="5%" align="center" ><strong>En</strong></td>
	  <td width="20%" >
	    <select name="cbotipoBD" class="Cajas2">
          <option value="L" <%=SeleccionarItem("cbo",tipoBD,"L")%>>Biblioteca USAT</option>
        </select>	  </td>
		<td width="10%" align="center" ><strong>buscar</strong></td>
		<td width="20%" >
		  <input name="txtcriterio" type="text" class="Cajas2" value="<%=criterio%>" maxlength="30"></td>
		<td width="5%" align="center" ><p><strong>por</strong></p>	    </td>
		<td width="10%" style="width: 10%"><select name="cbocampos" class="Cajas2" >
		<option value="T" <%=SeleccionarItem("cbo",campo,"T")%>>Titulo</option>
		<option value="A" <%=SeleccionarItem("cbo",campo,"A")%>>Autor</option>
<!--'	<option value="A" <%=SeleccionarItem("cbo",campo,"A")%>>Autor</option>
		<option value="E" <%=SeleccionarItem("cbo",campo,"E")%>>Editorial</option>
		<option value="F" <%=SeleccionarItem("cbo",campo,"F")%>>Edición</option>
		<option value="M" <%=SeleccionarItem("cbo",campo,"M")%>>Materia</option>
		<option value="R" <%=SeleccionarItem("cbo",campo,"R")%>>Tipo de Material</option>
-->		
		</select></td>
	  <td width="5%"  align="right" style="cursor:hand">
		<img src="../../../../images/menus/buscar_small.gif" onClick="BuscarBibliografia(frmbuscarLibros)"> </td>
	  <td width="5%"  align="left" style="cursor:hand"><span onClick="BuscarBibliografia(frmbuscarLibros)"> Buscar</span></td>
	</tr>
</table>
</form>
<span id="mensaje" class="rojo"></span>
<%if criterio<>"" and HayReg=true then%>
<input type="hidden" name="txtCodigo_cco" value="<%=request.querystring("codigo_cco")%>">

<table width="100%" class="contornotabla" bgcolor="white" height="83%" align="center">
	<tr class="usatCeldaMenu" height="95%">
	  <td width="100%" valign="top"><table width="100%" border="0" cellpadding="2" cellspacing="0" class="contornotabla">
        <tr class="usatCeldaTitulo">
          <td width="5%" align="center">N&ordm;</td>
          <td width="30%">T&iacute;tulo</td>
          <td width="25%">Autor</td>
          <td width="10%">Editorial</td>
          <td width="10%" align="center">Ejemplares</td>
          <td width="10%" align="center">Precio</td>
          <td width="10%" align="center">Solicitar</td>
        </tr>
		<%
		i=0
		for i=0 to rsLibros.RecordCount-1
		%>
        <tr class="bordeinf">
          <td class="bordeinf" align="center"><%Response.Write(i+1)%></td>
          <td class="bordeinf"><%Response.Write(rsLibros("titulo"))%></td>
          <td class="bordeinf"><%Response.Write(rsLibros("nombreautor"))%></td>
          <td class="bordeinf"><%Response.Write(rsLibros("nombreeditorial"))%></td>
          <td class="bordeinf" align="center">
		  <%if isnull(rsLibros("ejemplares")) = true then
			Response.Write("-")
		  else
			Response.Write(rsLibros("ejemplares"))
			end if
		  %>
		  </td>
          <td class="bordeinf" align="center">
		  <%if isnull(rsLibros("precio")) = true then
			Response.Write("-")
		  else
			Response.Write(rsLibros("precio"))
			end if
		  %>
		  </td>
          <td class="bordeinf" align="center"><table width="100%" border="0" cellspacing="0" cellpadding="2">
            <tr>
              <td align="right"><img style="cursor:hand" src="../../../../images/carrito.gif" width="18" height="15" onClick="AbrirDetallePedidoLst('A','<%=rslibros("idlibro")%>','<%=codigo_cco%>','<%=rsLibros("preciounit")%>','<%=rsLibros("moneda")%>')" ></td>
              <td><span style="cursor:hand" onClick="AbrirDetallePedidoLst('A','<%=rslibros("idlibro")%>','<%=codigo_cco%>','<%=rsLibros("preciounit")%>','<%=rsLibros("moneda")%>')">Solictar</span></td>
            </tr>
          </table></td>
        </tr>
		<%
		rsLibros.MoveNext
		next
		%>
      </table></td>
	</tr>
	<tr height="5%" class="azul">
		<td class="bordesup" align="right" bgcolor="#FFF8DC">Resultados de búsqueda: <%=total%> coincidencias</td></tr>
</table>	
<%elseif criterio<>"" then%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp; No se han encontrado coincidencias según sus criterios de búsqueda</h5>
<%end if%>
</body>
</html>
