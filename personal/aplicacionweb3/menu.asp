<%
Set Obj= Server.CreateObject("PryUSAT.clsDatAplicacion")
	ArrDatos=Obj.ConsultarAplicacionUsuario("AR","11",session("codigo_apl"),session("codigo_tfu"),0)
Set Obj=nothing

if IsEmpty(ArrDatos) then
	response.write "<h3>No se han definido Menús para su tipo de función</h3>"
else
%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title>Documento sin t&iacute;tulo</title>
<link href="../../private/estilo.css" rel="stylesheet" type="text/css">
<script language="JavaScript" src="../../private/funciones.js"></script>
<script>
function ResaltarItem(MnuActual,codigo_men)
{
	if(filas.length==undefined){
		MnuActual.className="contornotabla_azul"
		MnuActual.style.backgroundColor="#E3E1BA"
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
	fradetalle.location.href="carpetas.asp?tipoImagen=5&codigo_men=" + codigo_men
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
.Estilo2 {
	color: #000000;
	font-weight: bold;
	font-size: 10pt;
}
.Estilo4 {color: #000000; font-weight: bold; font-size: 7pt; }
.style1 {
	margin-left: 0px;
}
-->
</style></head>
<body topmargin="0" leftmargin="0" rightmargin="0">
<table width="100%" height="100%" border="0" cellpadding="4" cellspacing="0" id="tblMnuPrincipal">
  <tr>
    <td class="bordeinf" valign="middle" height="8%" colspan="2" valign="top">
    <b> <font size="3" color="#800000" face="Arial">Barra de Menús</font></b>
	</td>
	<td align="right" style="cursor:hand" class="bordeinf" onclick="OcultarFrame()">
	<img src="../../images/menus/contraer.gif">
	</td>
	</tr>
  <tr>
    <td height="28%" colspan="2" valign="top"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td align="left">
		<table height="104" align="left" cellpadding="0" cellspacing="2" id="tblMenu">	
		<tr>
		<%for c=Lbound(Arrdatos,2) to Ubound(Arrdatos,2)
			fila=c+1
			
			if (fila=4 or fila=6) then%>
			<tr></tr>
			<%end if%>
			<td valign="top">
				<table width="60" border="0" align="center" cellpadding="0" cellspacing="2" >
					  <tr>
						<td width="60" id="filas" codigoMenu="<%=arrdatos(1,c)%>" onMouseOver="Resaltar_azul(1,this,'S','3399CC')" onMouseOut="Resaltar(0,this,'S','F0F0F0')" onClick="ResaltarItem(this,'<%=arrdatos(1,c)%>')" height="60" align="center" bgcolor="#F0F0F0" class="contornotabla"><img src="../../images/menus/<%=arrdatos(5,c)%>"></td>
					  </tr>
					  <tr>
						<td align="center"><span class="Estilo4"><%=arrdatos(3,c)%> </span></td>
					  </tr>
				</table>
			</td>
	  	<%next%>
	  	<input type="hidden" value="<%=Arrdatos(0,0)%>" name="txtPrimerMnu">
		</tr>
		</table>
		</td>
      </tr>

    </table></td>
  </tr>
  <tr>
    <td height="60%" colspan="2" valign="top" align="center">
	<table width="100%" height="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="contornotablaazul">
      <tr>
        <td class="contornotabla_azul" align="center" >
		<iframe name="fradetalle" width="100%" height="100%" scrolling="no" frameborder="0">
		
		</iframe>
		</td>
      </tr>
    </table></td>
  </tr>
</table>
<script language="javascript">
	ResaltarItem(filas[0],'<%=arrdatos(1,0)%>')
</script>
<table style="width: 100%;display:none;height:100%;cursor:hand" id="tblTituloMnu" bgcolor="#CCCCCC">
	<tr><td align="left" valign="top">
		<img src="../../images/menus/barramenu.gif" onClick="OcultarFrame()" class="style1"></td>
	</tr>
</table>

</body>
</html>
<%end if%>