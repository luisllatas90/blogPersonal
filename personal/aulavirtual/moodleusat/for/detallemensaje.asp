<%
if session("idcursovirtual")="" then response.write "<script>top.location.href='../../../tiempofinalizado.asp'</script>"

idforomensaje=request.querystring("idforomensaje")
idforo=request.querystring("idforo")

function CargarFoto(byval idtipo,byval usuario,Byval modo)
	Dim alto,ancho
	alto=" height=""143"" "
	ancho=" width=""108"" "
	
	if modo="P" then
		alto=" height=""65"" "
		ancho=" width=""50"" "
	end if
	if trim(idtipo)="2" then
		'usuario=obEnc.CodificaWeb("069" & usuario)
		'---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '---------------------------------------------------------------------------------------------------------------
        
		CargarFoto="<img border=""0"" src=""//intranet.usat.edu.pe/imgestudiantes/" & usuario & """ " & alto & ancho & ">"
	else
		CargarFoto="<img border=""0"" src=""../../imgpersonal/" & usuario & """ " & alto & ancho & ">"
	end if
end function

'Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Mensajes del Tema de discusi�n</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript">
function AbrirMensaje(modo,id)
{
 	switch(modo)
	{
		case "A": //Agregar
			AbrirPopUp("frmmensaje.asp?accion=agregarforomensaje&idforo=<%=idforomensaje%>","450","650")
			break

		case "M": //Modificar
			AbrirPopUp("frmmensaje.asp?accion=modificarforomensaje&idforomensaje=<%=idforomensaje%>" + id,"450","650")
			break

		case "R": //Responder a mensaje
			AbrirPopUp("frmmensaje.asp?accion=agregarforomensaje&idforo=<%=idforo%>&refidforomensaje=" + id,"450","650")
			break

	}
}
</script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
</head>
<body>
<table cellspacing="0" width="100%" cellpadding="3" bgcolor="#C7E0CE" class="contornotabla" height="5%" style="border-collapse: collapse" bordercolor="#111111">
  <tr class="etiqueta">
    <td width="10%" valign="top" height="5%">Tema:</td>
    <td width="85%" valign="top" height="5%"><%=titulomensaje%>&nbsp;</td>
    <td width="5%" valign="top" height="5%">
    <input type="button" value=" Regresar" name="cmdregresar" class="salir" onclick="history.back(-1)"></td>
  </tr>
</table>
<br>
<%
'****************************************************
'Obtener los contenidos del curso virtual
'****************************************************
Sub CrearArbolRptas(ByVal idforo,ByVal codigo_usu,ByVal idforomensaje,ByVal j)
	dim i,x
	dim ImagenMenu,TextoMenu,idPadre
	dim rsContenido
	dim num
		
	x=0
	
	Set rsRptas=obj.Consultar("ConsultarForo","FO",10,idforo,idforomensaje,codigo_usu)
		
	for i=1 to rsRptas.recordcount
		cadena=""
			
		'Genera espacios para jerarqu�a
		for x=1 to j
			'cadena=cadena & "..." ' incluir imagen en blanco
			cadena=cadena & "<img border=""0"" src='../../../../images/blanco.gif'>"
		next
		response.write cadena
%>								
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla" height="40%">
  <tr>
    <td valign="top" width="5%" bgcolor="#C7E0CE" rowspan="3">
    <%=CargarFoto(rsRptas("idtipousuario"),rsRptas("foto"),"P")%>
    </td>
    <td valign="top" width="67%" bgcolor="#B9DBF7" height="10%">
    <b><%=rsRptas("titulomensaje")%></b><br>
    <%=rsRptas("nombreusuario")%> - Fecha de Registro: <%=rsRptas("fechareg")%>
   </td>
   	<%if session("idestadocursovirtual")=1 then%>
    <td valign="top" width="11%" bgcolor="#B9DBF7" height="10%">
    <input type="button" value="  Responder" name="cmdresponder" class="boton4" onclick="AbrirMensaje('R','<%=rsRptas("idforomensaje")%>')">
    </td>
    <%end if%>
  </tr>
  <tr>
    <td width="85%" valign="top" height="85%" colspan="2">
     <div id="listadiv" style="height:100%">
    <%=replace(rsRptas("descripcionmensaje"),chr(13),"<br>")%>
    </div>
    </td>
  </tr>
  <tr>
   <td valign="top" width="85%" align="right" height="5%" colspan="2" class="bordesup">
	<%=rsRptas("accionmensaje")%>
    </td>
  </tr>  
  </table>
  <br>
<%
		x=x+1
		CrearArbolRptas idforo,codigo_usu,rsRptas("idforomensaje"),x
		rsRptas.movenext			
	next
end Sub

Set Obj= Server.CreateObject("AulaVirtual.clsAccesoDatos")
obj.AbrirConexion

Set rsMensaje=obj.Consultar("ConsultarForo","FO",9,idforo,idforomensaje,session("codigo_usu"))
%>								
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla" height="40%">
  <tr>
    <td valign="top" width="5%" bgcolor="#C7E0CE" rowspan="3">
    <%=CargarFoto(rsMensaje("idtipousuario"),rsMensaje("foto"),"P")%>
    </td>
    <td valign="top" width="67%" bgcolor="#B9DBF7" height="10%">
    <b><%=rsMensaje("titulomensaje")%></b><br>
    <%=rsMensaje("nombreusuario")%> - Fecha de Registro: <%=rsMensaje("fechareg")%>
   </td>
   	<%if session("idestadocursovirtual")=1 then%>
    <td valign="top" width="11%" bgcolor="#B9DBF7" height="10%">
    <input type="button" value="  Responder" name="cmdresponder" class="boton4" onclick="AbrirMensaje('R','<%=rsMensaje("idforomensaje")%>')">
    </td>
    <%end if%>
  </tr>
  <tr>
    <td width="85%" valign="top" height="85%" colspan="2">
     <div id="listadiv" style="height:100%">
    <%=replace(rsMensaje("descripcionmensaje"),chr(13),"<br>")%>
    </div>
    </td>
  </tr>
  <tr>
   <td valign="top" width="85%" align="right" height="5%" colspan="2" class="bordesup">
	<%=rsMensaje("accionmensaje")%>
    </td>
  </tr>  
  </table>
  <br>
<%
Set rsMensaje=nothing

	CrearArbolRptas idforo,session("codigo_usu"),idforomensaje,0
obj.CerrarConexion
Set Obj=nothing
%>
</body>
</html>
	<%
'Set obEnc=nothing
%>