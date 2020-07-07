<!--#INCLUDE file= "../../funciones.asp"-->
<!--#INCLUDE file= "../../tab/clstab.asp"-->
<%
codigo_apl=request.querystring("codigo_apl")
if codigo_apl="" then codigo_apl=0
Set Obj= Server.CreateObject("PryUSAT.clsDatAplicacion")
	ArrAplicacion=Obj.ConsultarAplicacionUsuario("AR","1","","","")
Set Obj=nothing
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>aplicacion</title>
<link rel="stylesheet" type="text/css" href="../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
	function AbrirTabAplicacion(tab,pagina){
		fradetalle.location.href=pagina + "?codigo_apl=<%=codigo_apl%>"
		ResaltarPestana2(tab,'','')
}
function mypopup() {
    mywindow = window.open("frmclonarusuario.asp", "Ventana", "status=1,scrollbars=1,  width=370,height=180");
    //mywindow.moveTo(0, 0);
}
</script>
</head>
<body>
<p class="usatTitulo">Administración de Sistema Integral USAT</p>
<%If IsEmpty(ArrAplicacion)=false then%>
	<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
	<tr>
		<td width="20%" class="etiqueta">MÓDULOS REALIZADOS:</td>
		<td width="80%">
		<SELECT name="codigo_apl" style="width:80%" onChange="actualizarlista('index.asp?codigo_apl='+ this.value)">
			<OPTION VALUE="0">>>Seleccione el Módulo<<</OPTION>
			<%for i=lbound(ArrAplicacion,2) to Ubound(ArrAplicacion,2)%>
			<OPTION value="<%=ArrAplicacion(0,I)%>" <%=SeleccionarItem("cbo",codigo_apl,ArrAplicacion(0,I))%>><%=ArrAplicacion(1,I)%></OPTION>
			<%next%>
		</SELECT>
		<input type="button" value="Clonar Accesos" class="usatnuevo" onclick="javascript: mypopup()" />
		</td>
	</tr>
	</table>
	<br>
	<%if codigo_apl>0 then%>
	<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="85%">
	<tr>
		<td class="pestanaresaltada" id="tab" align="center" width="20%" height="5%" onClick="AbrirTabAplicacion(0,'listamenus.asp')">
		Lista de Menús</td>
		<td width="1%" height="5%" class="bordeinf">&nbsp;</td>
		<td class="pestanabloqueada" id="tab" align="center" width="20%" height="5%" onClick="AbrirTabAplicacion(1,'listafunciones.asp')">Lista de funciones</td>
		<td width="1%" height="5%" class="bordeinf">&nbsp;</td>
		<td class="pestanabloqueada" id="tab" align="center" width="20%" height="5%" onClick="AbrirTabAplicacion(2,'listausuarios.asp')">Lista de usuarios</td>
		<td width="1%" height="5%" class="bordeinf">&nbsp;</td>
		<td class="pestanabloqueada" id="tab" align="center" width="20%" height="5%" onClick="AbrirTabAplicacion(3,'frmpermisorecursos.asp')">Permisos por Escuela</td>
		<td width="1%" height="5%" class="bordeinf">&nbsp;</td>
<td class="pestanabloqueada" id="tab" align="center" width="16%" height="5%" onClick="AbrirTabAplicacion(4,'../../librerianet/academico/frmpermisoacciones.aspx')">Permisos x acciones</td>
	</tr>
	<tr  height="90%" valign="top">
		<td colspan="9" width="100%"  class="pestanarevez">
		<iframe name="fradetalle" height="100%" width="100%" border="0" frameborder="0" src="listamenus.asp?codigo_apl=<%=codigo_apl%>" target="_self">El 
		explorador no admite los marcos flotantes o no está configurado actualmente 
		para mostrarlos.
		</iframe>
		</td>
	</tr>
	<%end if
	else
		response.write "<h3>No se han registrado Módulos del sistema</h3>"
	end if%>
	</table>
</body>
</html>