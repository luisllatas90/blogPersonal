<%
dim rsMenu

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.Abrirconexion
	Set rsMenu=Obj.Consultar("ConsultarAplicacionUsuario","FO",11,session("codigo_apl"),session("codigo_tfu"),0)
	Obj.CerrarConexion
Set Obj=nothing

if (rsMenu.BOF and rsMenu.EOF) then
	response.write "<h3>No se han definido Menús para su tipo de función</h3>"
else
%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title>Menú</title>
<link href="../private/estilo.css" rel="stylesheet" type="text/css">
<script language="JavaScript" src="../private/funciones.js"></script>
<script>
function ResaltarItem(MnuActual,codigo_men)
{
	if(filas.length==undefined){
		filas.className="contornotabla_azul"
		filas.style.backgroundColor="#E3E1BA"
	}
	else{
		for (var c=0;c<filas.length; c++){
			var MnuCelda=filas[c]	
			if(MnuActual.codigoMenu==MnuCelda.codigoMenu){//Cambiar a ID :OJO
				MnuCelda.className="contornotabla_azul"
				MnuCelda.style.backgroundColor="#E3E1BA"
				}
			else{
				MnuCelda.className="contornotabla"
				MnuCelda.style.backgroundColor=""
			}
		}
	}
	fradetalle.location.href="carpetas.asp?tipoImagen=icono_men&codigo_men=" + codigo_men
}

function OcultarFrame()
{
	var NombreFrame=top.parent.document.getElementById('fraGrupo')
	var TamanioActual=NombreFrame.cols

	if (TamanioActual=="3%,*"){
		NombreFrame.cols="24%,*"
		document.all.tblMnuPrincipal.style.display=""
		document.all.tblTituloMnu.style.display="none"
	}
	else{
		document.all.tblMnuPrincipal.style.display="none"
		document.all.tblTituloMnu.style.display=""
		NombreFrame.cols="3%,*"
	}
}
</script>
<style type="text/css">
<!--
body 
{
	background-color: #F0F0F0}
.Menu {color: #000000; font-weight: bold; font-size: 7pt;}
-->
</style></head>
<body topmargin="0" leftmargin="0" rightmargin="0">
<table width="100%" height="100%" border="0" cellpadding="4" cellspacing="0" id="tblMnuPrincipal">
  <tr>
    <td width="97%" class="bordeinf" valign="middle" height="8%" valign="top">
    <b> <font size="3" color="#800000" face="Arial">Opciones de Menú</font></b>
	</td>
	<td width="3%"align="right" height="8%" style="cursor:hand" class="bordeinf" onclick="OcultarFrame()">
	<img src="../images/menus/contraer.gif">
	</td>
	</tr>
  <tr>
  	<%
	total=rsMenu.recordcount
    PrimeroReg=rsMenu("codigo_men")
    
    if total=1 then
    	ancho="33%"
    	alto1="15%"
    	alto2="85%"
    else
    	ancho="100%"
    	alto1="32%"
    	alto2="60%"
    end if	
  	%>
  
    <td valign="top" align="left" colspan="2" height="<%=alto1%>" width="100%">
    <%    
    response.write "<table width='" & ancho & "'><tr>" & vbcrlf & vbtab
    Do while Not rsMenu.EOF
		fila=fila+1
	
		if fila=4 or fila=6 then
			response.write "<tr>" & vbcrlf & vbtab
		end if
		
		response.write "<td width=""33%"" valign=""top"">"& vbcrlf & vbtab
		%>
		<table width="100%" border="0" align="center" cellpadding="0" cellspacing="2" >
		  <tr>
			<td id="filas" codigoMenu="<%=rsMenu("codigo_men")%>" onMouseOver="Resaltar_azul(1,this,'S','3399CC')" onMouseOut="Resaltar(0,this,'S','F0F0F0')" onClick="ResaltarItem(this,'<%=rsMenu("codigo_men")%>')" height="60" align="center" bgcolor="#F0F0F0" class="contornotabla">
				<img src="../images/menus/<%=rsMenu("icono_men")%>">
			</td>
		  </tr>
		  <tr>
			<td align="center" class="Menu"><%=rsMenu("descripcion_men")%></td>
		  </tr>
		</table>
		<%
		response.write "</td>" & vbcrlf & vbtab
		if (fila=total) then
			for x=0 to total-3
				response.write "<td width=""33%"" valign=""top"">&nbsp;</td>"& vbcrlf & vbtab
			next
		end if
	
		if fila=4 or fila=6 then
			response.write "</tr>" & vbcrlf & vbtab
		end if
		rsMenu.movenext
	Loop
	response.write "</table>"
	%>
    </td>
	</tr>
  <tr>
    <td colspan="2" height="<%=alto2%>" width="100%" valign="top" align="center">
		<iframe name="fradetalle" width="100%" height="100%" scrolling="auto" frameborder="0" class="contornotabla_azul">

		</iframe>
	</td>
   </tr>
</table>
<script language="javascript">
	ResaltarItem(filas[0],'<%=PrimeroReg%>')
</script>
<table style="width: 100%;display:none;height:100%;cursor:hand" id="tblTituloMnu" bgcolor="#CCCCCC">
	<tr><td align="left" valign="top">
		<img src="../images/menus/barramenu.gif" onClick="OcultarFrame()"></td>
	</tr>
</table>

</body>
</html>
<%end if%>