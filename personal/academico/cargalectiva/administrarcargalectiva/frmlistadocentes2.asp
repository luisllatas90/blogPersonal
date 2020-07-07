<!--#include file="../../../../funciones.asp"-->
<%
accion=request.querystring("accion")
codigo_cup=request.querystring("codigo_cup")
codigo_cac=request.querystring("codigo_cac")
codigo_dac=request.querystring("codigo_dac")

if accion="" then accion="agregarcargaacademica"

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsDocente=obj.Consultar("ConsultarCentroCosto","FO","PD",codigo_dac)
	obj.CerrarConexion
Set obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Seleccione que usuarios tendrán acceso</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>

</head>
<body topmargin="0" leftmargin="0">
<form name="frmListaCorreos" onSubmit="return Validarlistaagregada(this)" method="post" ACTION="procesar.asp?pagina=detallecarga2.asp&accion=<%=accion%>&codigo_cup=<%=codigo_cup%>&codigo_cac=<%=codigo_cac%>&codigo_dac=<%=codigo_dac%>">
<table cellpadding="2" cellspacing="0" border="0" width="100%" style="border-color:#C0C0C0; border-collapse: collapse" bordercolor="#111111" height="100%">
	<tr style="background-color: #C0C0C0">
	<td  height="5%">
	<input type="submit" class="guardar2" value="Guardar" NAME="cmdGrabar">
    <input type="button" class="regresar2" onClick="history.back(-1)" value="Regresar" NAME="cmdCancelar">

	</td>
	<td height="5%">&nbsp;</td>
	<td height="5%">&nbsp;</td>
    </tr>
	<tr align="center">          
      <td  width="40%"  height="5%">&nbsp; </td>
          <td width="10%" height="5%">&nbsp;</td>
          <td width="40%" height="5%"><font color="#800000"><b>Profesores 
          seleccionados</b></font></td></tr>
		<tr align="center">
          <td  width="40%" valign="top" height="90%">
          <%call llenarlista("ListaDe","",rsDocente,"codigo_per","docente","","","","multiple")%>
          <script type="text/javascript" language="javascript">frmListaCorreos.ListaDe.style.height="100%"</script>
		  </td>
			<td  width="10%" valign="top" height="90%">
			  <input type="button" value="Agregar-&gt;" style="width: 80" onClick="AgregarItem(this.form.ListaDe)" class="cajas">
			  <br>
		      <input type="button" value="&lt;-Quitar" style="width: 80" onClick="QuitarItem(this.form.ListaPara)" class="cajas"></td>
			<td  width="40%" valign="top" height="90%">
				<select multiple name="ListaPara" size="10" style="width: 100%; height:100%">
		</select></tr>
		</table>
</form>
</body>
</html>