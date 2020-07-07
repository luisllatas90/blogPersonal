<!--#include file="clsdocumento.asp"-->
<%
Dim Modalidad
Dim IdDocumento
Dim IdLink
Dim tituloLink
Dim tipoLink
Dim URL

Modalidad=Request("modalidad")
IdDocumento=request.querystring("IdDocumento")
IdLink=Request("IdLink")
tituloLink=Request("tituloLink")
tipoLink=Request("tipoLink")
URL=Request("URL")

'response.Buffer=true
	
set documento=new clsdocumento
	with documento
		.restringir=session("idcursovirtual")
		If Len(Request.form("cmdGuardar"))>0 then
			Select case Modalidad
				case "AgregarNuevo"
					call .AgregarLink(IdDocumento,titulolink,tipolink,URL) 
					Modalidad=""
				case "Modificar"
					call .ModificarLink(IdLink,titulolink,tipolink,URL)
					Modalidad=""
			end Select
		Else
			If Modalidad="Eliminar" then Call .EliminarLink(IdLink)
		End if
		ArrDatos=.consultar("4",IdDocumento,"","","")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registrar enlaces web del documento</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validardocumento.js"></script>
</head>
<body>
<form name="frmListaLinks" method="post" onSubmit="return validarlink(this)" ACTION="link.asp?Modalidad=<%=Modalidad%>&IdDocumento=<%=IdDocumento%>">
<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Recuerde que no debe cerrar la ventana sin ingresar los links del documento</p>
<table width="100%" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" class="contornotabla">
  <tr class="etabla"> 
      <td width="3%">&nbsp;</td>
      <td style="text-align: left" width="40%">Título</td>
      <td style="text-align: left" width="45%">Dirección URL</td>
      <td width="12%">
<input name="Button" type="button" class="agregar" onclick="MM_goToURL('self','link.asp?idDocumento=<%=idDocumento%>&Modalidad=AgregarNuevo');return document.MM_returnValue" value="   Agregar"></td>
	</tr>
	<%if IsEmpty(ArrDatos)=false then
		num=0								  
		for i=Lbound(Arrdatos,2) to Ubound(Arrdatos,2)
			num=num+1%>			  
    <tr>
    <td width="3%"><%=num%>.</td>
    <td width="40%">
    <%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then%>
		<input maxLength="100" name="tituloLink" id="tituloLink" class="Cajas" size="20" value="<%=Arrdatos(1,I)%>" style="width: 100%">
		<input name="idLink" type="hidden" class="cajas" id="IdLink" value="<%=Arrdatos(0,I)%>">
	<%else%>
        <%=Arrdatos(1,I)%>
    <%end if%></td>
	<td nowrap="nowrap" width="45%">
		<%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then%>
		    <select name="tipoLink" id="tipoLink">
  				<option value="http://" <%=seleccionar("http://",Arrdatos(2,I))%>>http://</option>
  				<option value="https://" <%=seleccionar("https://",Arrdatos(2,I))%>>https://</option>
  				<option value="ftp://" <%=seleccionar("ftp://",Arrdatos(2,I))%>>ftp://</option>
		  	    <option value="mailto" <%=seleccionar("mailto",Arrdatos(2,I))%>>email</option>
		  	</select>
  		    <input  maxLength="100" name="URL" class="Cajas" id="URL" size="20" value="<%=Arrdatos(3,I)%>" style="width: 150">
  		<%else
  			response.write Arrdatos(2,I) & Arrdatos(3,I)
		end if%>
	</td>
	<td align="right" width="12%">
		<%if trim(modalidad)="Modificar" and num=cint(request("recordid")) then%>
  			<input type="submit" value="    " name="cmdGuardar" class="imgGuardar">
  			<img border="0" src="../../../images/salir.gif" onclick="MM_goToURL('self','link.asp?idDocumento=<%=idDocumento%>&IdLink=<%=Arrdatos(0,I)%>');return document.MM_returnValue" class="imagen">
		<%else%>
  			<img border="0" src="../../../images/editar.gif" onclick="MM_goToURL('self','link.asp?idDocumento=<%=idDocumento%>&IdLink=<%=Arrdatos(0,I)%>&modalidad=Modificar&recordid=<%=num%>');return document.MM_returnValue" width="18" height="13" class="imagen">
  			<img border="0" src="../../../images/eliminar.gif" onclick="EliminarLink('<%=idDocumento%>','<%=Arrdatos(0,I)%>');" class="imagen">
		<%end if%>
	</td>
	</tr>
	<%Next
 end if
 if trim(modalidad)="AgregarNuevo" then%>
  	<tr>
       <td width="3%"><%=num+1%>.</td>
       <td width="40%">
       <input maxLength="100" name="tituloLink" id="tituloLink" class="Cajas" size="20" style="width: 100%"></td>
       <td width="45%">
			<select name="tipolink" id="tipolink">
				<option value="http://">http://</option>
				<option value="https://">https://</option>
				<option value="ftp://">ftp://</option>
			    <option value="mailto">email</option>
			</select>
			<input maxLength="100" name="URL" class="Cajas" id="URL" size="20" style="width: 150">
		</td>
		<td align="right" width="12%">
		<input type="submit" value="    " name="cmdGuardar" class="imgGuardar">
        <img border="0" src="../../../images/salir.gif" onclick="MM_goToURL('self','link.asp?idDocumento=<%=idDocumento%>');return document.MM_returnValue" class="imagen"></td>
      </tr>
  <%end if%>
  </td>
  </tr>
	</td>
	</tr>
</td>
  </tr>
</td>
  </tr>
  </table>
</form>
</body>
</html>
<%
	end with
set documento=nothing
%>