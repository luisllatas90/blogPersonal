<!--#include file="../../../funciones.asp"-->
<% 
Dim param
Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio

tipo=request.querystring("tipo")
param=request.querystring("param")
resultado=request.querystring("resultado")

if (trim(param)<>"") then
	Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
		
		If tipo="T" then
			Set rsDirectorio=obj.Consultar("ConsultarPersonal","FO","DI",ReemplazarTildes(param))
			ArrEncabezados=Array("Apellidos y Nombres","Dirección","Casa","Celular","Trabajo","email")
			ArrCampos=Array("nombres","direccion","telcasa","celular","teltrab","mail1")
			ArrCeldas=Array("35%","20%","10%","10%","10%","15%")
		else
			Set rsDirectorio=obj.Consultar("ConsultarProveedor","FO",ReemplazarTildes(param))
			ArrEncabezados=Array("Proveedor","Dirección","Teléfono","Fax","E-mail","RUC")
			ArrCampos=Array("nombrepro","direccionpro","telefonopro","faxpro","emailpro","rucpro")
			ArrCeldas=Array("30%","20%","10%","10%","15%","15%")
		end if
		obj.CerrarConexion
	set obj= Nothing	
 
	if Not(rsDirectorio.BOF and rsDirectorio.EOF) then
		alto="height=""98%"""
		resultado="S"
	else
		resultado="N"
	end if
end if
%>
<html>
<head>
<title>Buscar Responsable de Deuda</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript">
	function BuscarDirectorio()
	{
		if (document.all.txtParam.value.length<3){
			alert("Especifique el término de búsqueda")
			document.all.txtParam.focus()
			return(false)
		}
		location.href="vstdirectorio.asp?tipo=" + cboTipoBus.value + "&param=" + txtParam.value
	}
	
</script>
</head>

<body onload="document.all.txtParam.focus()">
<table width="100%" <%=alto%> border="0" cellspacing="0" cellpadding="3" style="border-collapse: collapse" bordercolor="#111111">
    <tr> 
      <td height="5%" colspan="4" valign="top" class="usatTitulo" width="100%">
		Búsqueda de Directorio</td>
    </tr>
    <tr> 
      <td  width="15%" height="5%">Buscar por:</td>
      <td valign="top" width="20%" height="5%"><select name="cboTipoBus" id="cboTipoBus" class="cajas2">
          <option value="T" <%=SeleccionarItem("cbo",tipo,"T")%>>Personal USAT</option>
			<option value="P" <%=SeleccionarItem("cbo",tipo,"P")%>>Proveedores</option>
        </select></td>
      <td valign="top" width="28%" height="5%">
      <input name="txtParam" type="text" id="txtParam" size="30" class="cajas2" onkeyup="if(event.keyCode==13){BuscarDirectorio()}"></td>
      <td valign="top" width="30%" height="5%">
      <input name="cmdBuscar" type="button" id="cmdBuscar" value="Buscar" class="usatbuscar" onClick="BuscarDirectorio()"> </td>
    </tr>
    <%if resultado="S" then%>
    <tr id="trLista">
  		<td width="100%" height="95%" colspan="4">
  		<%
  		call CrearRpteTabla(ArrEncabezados,rsDirectorio,"",ArrCampos,ArrCeldas,"S","N",pagina,"S",ArrCamposEnvio,"")
  		%>
  		</td>
	</tr>
	<%else%>
	<tr><td colspan="4" class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp; <b>No se han encontrado coincidencias con el término especificado</b></td>
	</tr>
	<%end if%>
  </table>
</body>
</html>