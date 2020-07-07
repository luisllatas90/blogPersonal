<!--#include file="clsrecepcion.asp"-->
<%
if session("codigo_usu")="" then response.Redirect "../../../tiempofinalizado.asp"
	Dim recepcion
	
	set recepcion=new clsrecepcion
		ArrDatos=recepcion.ConsultarParametrosArchivo("1")
	set recepcion=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<title>Sistema de Recepción de documentos</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="Javascript">
	function abrirSistema()
	{
		var item=cbxanio.options[cbxanio.selectedIndex].text

		if (cbxanio.selectedIndex < 0){
			alert("Por favor seleccione el Año para mostrar los documentos.")
			cbxanio.focus()
			return (false)
		}
		else{
			location.href="abrirrecdo.asp?idanio=" + cbxanio.value + "&nombreanio=" + item
		}
	}
</script>  
</head>
<body topmargin="0" leftmargin="0">
<h1>&nbsp;</h1>
<center>
<fieldset style="padding: 2; width:30%">
<legend class="e1">Seleccione el año de consulta</legend>
<table width="95%" cellspacing="0" cellpadding="3" border="0" style="border-collapse: collapse" bordercolor="#111111">
  <tr><td align="center" width="20%">
    <select id="cbxanio" style="width: 115; height:134" multiple>
    <%If IsEmpty(ArrDatos)=false then
		for i=lbound(ArrDatos,2) to Ubound(ArrDatos,2)%>
	   	<option value="<%=ArrDatos(0,I)%>" <%=seleccionar(year(date),ArrDatos(1,I))%>><%=ArrDatos(1,I)%></option>
		<%next
	end if%>
    </select></td>
    <td valign="top">
    <input type="button" class="buscar" value="Aceptar" OnClick="abrirSistema()" NAME="cmdAceptar"><br><br>
    <input type="button" value="Salir" class="salir" onclick="Javascript:top.window.close()" NAME="cmdCancelar"></td>
  </tr>
  </table>
</fieldset>
<center>
<p></p>
</body>
</html>