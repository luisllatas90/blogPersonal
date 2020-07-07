<!--#include file="../../funciones.asp"-->
<%
Dim Modalidad
Dim codigo_apl
Dim tipo_uap,codigo_uap,codigo_tfu

Modalidad=Request("modalidad")
codigo_apl=request.querystring("codigo_apl")
tipo_uap=Request("tipo_uap")
codigo_uap=Request("codigo_uap")
codigo_tfu=Request("codigo_tfu")

response.Buffer=true

	If Len(Request.form("cmdGuardar"))>0 then
		if modalidad="Modificar" then
			set Obj=server.CreateObject ("PryUSAT.clsDatAplicacion")
				Call Obj.modificarusuarioaplicacion(tipo_uap,codigo_uap,codigo_apl,codigo_tfu,0,0)
			set Obj=nothing
		end if
		modalidad=""
	Else
		If modalidad="Eliminar" then
			set Obj=server.CreateObject ("PryUSAT.clsDatAplicacion")
				'response.write request.querystring
				Call Obj.eliminarusuarioaplicacion(tipo_uap,codigo_uap,codigo_apl,codigo_tfu)
			set Obj=nothing
		End if
	End if
	
	Set Obj= Server.CreateObject("PryUSAT.clsDatAplicacion")
		ArrPermiso=Obj.ConsultarAplicacionUsuario("AR","3",codigo_apl,"","")
		if modalidad="Modificar" then
			ArrFuncion=Obj.ConsultarAplicacionUsuario("AR","4","","","")
		end if
	Set Obj=nothing
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Menú de la aplicación</title>
<link rel="stylesheet" type="text/css" href="../../private/estilo.css">
<script language="JavaScript" src="../../private/funciones.js"></script>
</head>

<body bgcolor="#EEEEEE">
<form name="frmusuario" method="post" ACTION="listausuarios.asp?modalidad=<%=modalidad%>&amp;codigo_apl=<%=codigo_apl%>">
<br />
<table width="100%" border="0" cellpadding="4" cellspacing="0" style="border:1px solid #808080; border-collapse: collapse" bordercolor="#111111" bgcolor="white">
  <tr class="etabla">
    <td width="3%">&nbsp;</td>
    <td style="text-align: left" width="45%">Nombre de Usuario</td>
    <td style="text-align: left" width="22%">Tipo de acceso</td>
    <td width="20%" style="text-align: right;cursor:hand" onclick="AbrirPopUp('frmagregarusuario.asp?codigo_apl=<%=codigo_apl%>','300','550')">
    <img border="0" src="../../images/anadir.gif"> Agregar</td>
  </tr>
<%if IsEmpty(ArrPermiso)=false then
	num=0								  
	for i=Lbound(ArrPermiso,2) to Ubound(ArrPermiso,2)
		num=num+1%>
  <tr>
    <td width="3%"><%=num%>.</td>
    <td width="45%">
    <%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then%>
    	<%=ArrPermiso(3,I)%>
    	<input type="hidden" name="tipo_uap" id="tipo_uap" value="<%=ArrPermiso(0,I)%>">
    	<input type="hidden" name="codigo_uap" id="codigo_uap" value="<%=ArrPermiso(1,I)%>">
    <%else%>
    	<%=ArrPermiso(3,I)%>
    <%end if%></td>
    <td nowrap="nowrap" width="22%">
    <%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then%>
    <select name="codigo_tfu" style="width:100%">
	    <%If IsEmpty(ArrFuncion)=false then
    		for j=lbound(ArrFuncion,2) to Ubound(ArrFuncion,2)%>
	    	<option value="<%=Arrfuncion(0,j)%>" <%=SeleccionarItem("cbo",ArrPermiso(2,i),ArrFuncion(0,j))%>><%=ArrFuncion(1,j)%></option>
	    	<%next
    	end if%></select>
    <%else%>
    	<%=ArrPermiso(4,I)%>
    <%end if%></td>
    <td align="right" width="20%">
    <%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then%>
    <input type="submit" value="    " name="cmdGuardar" class="imgGuardar">
    <img border="0" src="../../images/salir.gif" onclick="MM_goToURL('self','listausuarios.asp?codigo_apl=<%=codigo_apl%>&tipo_uap=<%=ArrPermiso(0,I)%>&codigo_uap=<%=replace(ArrPermiso(1,I),"\","/")%>&codigo_tfu=<%=ArrPermiso(2,I)%>');return document.MM_returnValue" class="imagen">
    <%else%>
    <img border="0" src="../../images/editar.gif" onclick="MM_goToURL('self','listausuarios.asp?codigo_apl=<%=codigo_apl%>&modalidad=Modificar&recordid=<%=num%>');return document.MM_returnValue" width="18" height="13" class="imagen">
    <img border="0" src="../../images/eliminar.gif" onclick="ConfirmarEliminar('Está seguro que desea Eliminar este USUARIO del Sistema.\n Recuerde que esta acción no podrá deshacerla','listausuarios.asp?codigo_apl=<%=codigo_apl%>&tipo_uap=<%=ArrPermiso(0,I)%>&codigo_uap=<%=replace(ArrPermiso(1,I),"\","/")%>&codigo_tfu=<%=ArrPermiso(2,I)%>');" class="imagen">
    <%end if%></td>
  </tr>
  <%Next
 end if%>
</table>
</form>
</body>
</html>