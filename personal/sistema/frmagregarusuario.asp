<!--#include file="../../funciones.asp"-->
<%
codigo_tpe=request.querystring("codigo_tpe")
codigo_apl=request.querystring("codigo_apl")
if codigo_tpe="" then codigo_tpe=0

	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
			Set rsTipoPersonal=Obj.Consultar("ConsultarTipoPersonal","FO","TO",0)
			Set rsPersonal=Obj.Consultar("ConsultarPersonal","FO","TP",codigo_tpe)
			Set rsTipofuncion=Obj.Consultar("ConsultarAplicacionUsuario","FO","5",codigo_apl,0,0)
		obj.CerrarConexion
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Seleccione que usuarios tendrán acceso</title>
<link rel="stylesheet" type="text/css" href="../../private/estilo.css">
<script language="JavaScript" src="../../private/funciones.js"></script>
</head>
<body topmargin="0" leftmargin="0">
<form name="frmListaCorreos" onsubmit="return Validarlistaagregada(this)" method="post" ACTION="procesar.asp?accion=agregarpermisos&codigo_apl=<%=codigo_apl%>">
<table style="border-collapse: collapse;" bordercolor="#111111" cellpadding="4" cellspacing="0" width="100%">
  <tr>
   	<td height="19" width="100%" colspan="6" class="barraherramientas">
    <input type="submit" class="usatguardar" value="Guardar" NAME="cmdGrabar">
    <input type="button" class="usatsalir" onclick="window.close()" value="Cancelar" NAME="cmdCancelar"></td>
  </tr>
</table>  
<table cellpadding="2" cellspacing="0" border="0" width="100%" style="border-color:#C0C0C0; border-collapse: collapse" bordercolor="#111111">
		<tr align="center">
          <td  width="40%"  height="21">
          <%call llenarlista("cbocodigo_tpe","actualizarlista('frmagregarusuario.asp?codigo_tpe='+ this.value + '&codigo_apl=" & codigo_apl & "')",rsTipoPersonal,"codigo_tpe","descripcion_tpe",codigo_tpe,"Seleccione el tipo de Personal","","")%>
          </td>
          <td width="10%" height="21">&nbsp;</td>
          <td width="40%" height="21"><font color="#800000"><b>Usuarios 
          seleccionados</b></font></td></tr>
		<tr align="center">
          <td  width="40%">
		  <select multiple name="ListaDe" size="10" style="width: 100%; height:200">
			<%If not(rsPersonal.BOF and rsPersonal.EOF) then
				Do While Not rsPersonal.EOF%>
				<option value="<%=rsPersonal("codigo_per")%>"><%=rsPersonal("personal")%></option>
				<%
				rsPersonal.movenext
				Loop
			end if%>
		  </select></td>
			<td  width="10%" valign="top">
			  <input type="button" value="Agregar-&gt;" style="width: 80" onclick="AgregarItem(this.form.ListaDe)" class="cajas">
			  <br>
		      <input type="button" value="&lt;-Quitar" style="width: 80" onclick="QuitarItem(this.form.ListaPara)" class="cajas"></td>
			<td  width="40%">
				<select multiple name="ListaPara" size="10" style="width: 100%; height:200">
		</select></tr>
		<%if codigo_tpe>0 then%>
		<tr class="rojo">
          	<td width="40%" align="right">Agregar  usuarios con función:&nbsp;</td>
			<td width="50%" valign="top" colspan="2"> 
			<%call llenarlista("cbocodigo_tfu","",rsTipofuncion,"codigo_tfu","descripcion_tfu",codigo_tfu,"","","")%>
			</td>
		<%end if%>
		</table>
</form>
</body>
</html>