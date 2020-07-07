<!--#include file="../../funcionesaulavirtual.asp"-->
<%
Pagina=Request.querystring("Pagina")
idusuario=session("codigo_usu")
idtabla=request.querystring("idtabla")
nombretabla=request.querystring("nombretabla")
IdGrupo=Request.querystring("IdGrupo")
if Pagina="" then Pagina=1

If idGrupo="" then IdGrupo=3000
If session("idcursovirtual")="" and nombreTabla="cursovirtual" then
	session("idcursovirtual")=idTabla
end if

Select case IdGrupo
	case 2000 'Todos
		cadSQL= "SELECT idusuario,NombreUsuario FROM usuario "
	case 3000 'Usuarios del cursovirtual actual
		cadSQL= "SELECT idusuario,NombreUsuario FROM Permisos "
		cadSQL= cadSQL & " WHERE nombretabla='cursovirtual' AND idTabla=" & session("idcursovirtual")
	case else
		cadSQL=cadSQL & " SELECT idusuario,NombreUsuario FROM listadistribucion"
		cadSQL=cadSQL & " WHERE IdGrupo=" & IdGrupo
End Select
	cadSQL= cadSQL & " ORDER BY NombreUsuario"

if Pagina=2 then
	Set Obj= Server.CreateObject("AulaVirtual.clsDocumento")
		ArrGrupo=Obj.CargarTablas("SELECT * FROM ListaGrupos where (idusuario='" & IdUsuario & "' AND Idcursovirtual=" & session("idcursovirtual") & ") OR Idcursovirtual=0")
	Set Obj=nothing
	Set Obj= Server.CreateObject("AulaVirtual.clsDocumento")
		ArrDatos=Obj.CargarTablas(cadSQL)
	Set Obj=nothing
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de correos</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
</head>
<body topmargin="0" leftmargin="0">
<%if pagina=2 then%>
<form name="frmListaCorreos" onsubmit="return validate(this)" method="post" ACTION="procesar.asp?accion=agregarpermisos&idtabla=<%=idtabla%>&nombretabla=<%=nombretabla%>">
<%=(BotonesAccion("S"))%>
<table cellpadding="2" cellspacing="0" border="0" width="100%" style="border-collapse: collapse" bordercolor="#111111">
	<tr align="center">
      <td  width="10%"  height="30">Mostrar&nbsp; </td>
      <td  width="30%"  height="30" align="left">
	<select  name="idGrupo" style="width: 100%" onChange="actualizarlista('listacorreos.asp?pagina=<%=pagina%>&idGrupo='+ frmListaCorreos.idGrupo.value + '&IdTabla=<%=IdTabla%>&NombreTabla=<%=NombreTabla%>')">
	<option value="2000" <%=Seleccionar(idGrupo,2000)%>>--Todos--</option>
	<option value="3000" <%=Seleccionar(idGrupo,3000)%>>Usuarios de esta actividad</option>
	<%If IsEmpty(ArrGrupo)=false then
		for i=lbound(ArrGrupo,2) to Ubound(ArrGrupo,2)%>
			<option value="<%=ArrGrupo(0,I)%>" <%=Seleccionar(idgrupo,ArrGrupo(0,i))%>><%=ArrGrupo(1,I)%></option>
		<%next
	end if%>
	      </td>
          <td width="15%"  height="30">&nbsp;</td>
          <td width="42%" height="30"><font color="#800000"><b>Usuarios con 
          acceso</b></font></td></tr>
		<tr align="center">
          <td  width="43%" height="163" colspan="2">
		  <select multiple name="ListaDe" size="10" style="width: 100%; height:100%">
			<%If IsEmpty(ArrDatos)=False then
				FOR I=Lbound(ArrDatos,2) to Ubound(ArrDatos,2)%>
				<option value="<%=ArrDatos(0,I)%>"><%=ArrDatos(1,I)%></option>
				<%NEXT
			end if%>
		  </select></td>
			<td  width="15%" height="163" valign="top">
			  <input type="button" value="Agregar-&gt;" style="width: 80" onclick="AgregarItem(this.form.ListaDe)" class="cajas">
			  <br>
		      <input type="button" value="&lt;-Quitar" style="width: 80" onclick="QuitarItem(this.form.ListaPara)" class="cajas"></td>
			<td  width="42%" height="163">
				<select multiple name="ListaPara" size="10" style="width: 100%; height:100%">
		</select></tr>
		<tr>
          <td  width="43%" height="10" colspan="2" valign="top">
          <%if nombretabla="cursovirtual" and session("autorizacion")=1 then%>
          <input type="checkbox" name="chkHeredar" value="S">Desea heredar  permisos a los recursos
          <%end if%>
          </td>
			<td  width="15%" height="10" valign="top"></td>
			<td  width="42%" height="10" valign="top" class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Para seleccionar o quitar 
			los usuarios que tendrán acceso al Recurso, utilice los botones &quot; Agregar y/o Quitar&quot;
			</td></tr>
		</table>
<%end if%>
</form>
<%if Pagina=1 then
	if nombretabla<>"cursovirtual" then%>
	<h4>&nbsp;</h4>
<h4 align="center">¿ Desea que todos los usuarios de la Actividad Académica<br>
tengan acceso al recurso seleccionado?</h4>
<p align="center">Si elije <b>No</b> Ud. debe seleccionar uno por uno lo 
usuarios que tendrán acceso al recurso.<br>
		  <input onClick="location.href='procesar.asp?accion=heredarpermisos&idtabla=<%=idtabla%>&nombretabla=<%=nombretabla%>'" type="button" value="Sí" class="boton" name="cmdSi" style="width: 50">
		  <input onClick="location.href='listacorreos.asp?Pagina=2&idtabla=<%=idtabla%>&nombretabla=<%=nombretabla%>'" type="button" value="No" name="cmdNo" class="boton" style="width: 50"><br>
&nbsp;</p>
	<%end if
end if%>
</body>
</html>