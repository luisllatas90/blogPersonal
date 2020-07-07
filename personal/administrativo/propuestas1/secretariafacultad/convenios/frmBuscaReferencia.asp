<!--#include file="../../../../../funciones.asp"-->


<html>
<head>
<title>Buscar Convenio de Referencia</title>

<link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--
body {
	background-color: #f0f0f0;
}
.Estilo3 {
	font-size: 8px;
	font-family: Arial, Helvetica, sans-serif;
	color: #000000;
}
.Estilo4 {color: #F0F0F0}
.Estilo5 {	color: #395ACC;
	font-weight: bold;
}
-->
</style>
<script type="text/JavaScript">
<!--
function MM_jumpMenu(targ,selObj,restore){ //v3.0
  eval(targ+".location='"+selObj.options[selObj.selectedIndex].value+"'");
  if (restore) selObj.selectedIndex=0;
}
//-->
</script>

<script language="JavaScript" src="../private/validarpropuestas.js"></script>
<script language="JavaScript" src="../../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../../private/calendario.js"></script>
<script>

function buscar(){
var descripcion= frmreferencia.txtreferucion.value
//alert (descripcion)
	if (descripcion==undefined){
			alert ("Ingrese la descripción de una referución")
		}
	else{
		location.href="frmBuscaReferencia.asp?refer=" + descripcion
		}
}
function EnviaReferencia(){
	
	window.opener.document.frmconvenio.txtReferencia.value=frmreferencia.cboreferencias.value
	window.opener.document.frmconvenio.txtNameReferencia.value=frmreferencia.cboreferencias.options[frmreferencia.cboreferencias.selectedIndex].text	
	window.close()
}

	function popUp(URL) {
	day = new Date();
	id = day.getTime();
	var izq = 300//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=400,height=350,left = "+ izq +",top = "+ arriba +"');");
	}
</script>
<script LANGUAGE="JavaScript">
<!-- Begin
function textCounter(field, countfield, maxlimit) {
if (field.value.length > maxlimit) // if too long...trim it!
field.value = field.value.substring(0, maxlimit);
else 
countfield.value = maxlimit - field.value.length;
}
// End -->
</script>

</head>
	  
<body topmargin="0" rightmargin="0" leftmargin="0">
	<form method="post" " name="frmreferencia" id="frmreferencia">
<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td  height="10%" colspan="3" valign="top" class="bordeinf">
	<table  width="97%" height="100%" border="0" align="center" cellpadding="0" cellspacing="5">
      <tr>
        <td valign="top">
		<input  name="cmdborrador" type="button" class="conforme1" id="cmdborrador" value="         Aceptar"  onClick="EnviaReferencia()">
          &nbsp;&nbsp;&nbsp;</td>
        <td align="right" valign="top"><input onClick="window.close()"  name="cmdborrador2" type="button" class="noconforme1" id="cmdborrador2" value="         Cerrar" ></td>
      </tr>
    </table>
	</td>
  </tr>
  <%
		 	refer=Request.QueryString("refer")
			
			if refer="" then
				refer="-1"
			else
				refer=Request.QueryString("refer")
			end if
			//Response.Write(refer)
			Set prop=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	prop.AbrirConexion
			
			set rsreferencias=prop.Consultar("ConsultarConvenios","FO","LI",0,0,refer)
	    	prop.CerrarConexion
			set prop=nothing

  
  %>
  <tr >
    <td height="10%" width="16%" valign="middle" class="bordeinf">Referencia</td>
    <td height="10%" width="69%" valign="middle" class="bordeinf"><input name="txtreferucion" type="text" class="Cajas2" id="txtreferucion"></td>
    <td height="10%" width="15%" valign="middle" class="bordeinf"><input name="cmdbuscar" type="button" class="buscar_prp_small" id="cmdbuscar" onClick="buscar()" value="   Buscar"></td>
  </tr>
  <tr>
    <td height="10%" valign="middle" class="bordeinf">&nbsp;</td>
    <td height="10%" align="center" valign="middle" class="bordeinf">Seleccione la Referencia y de clic en Aceptar </td>
    <td height="10%" valign="middle" class="bordeinf">&nbsp;</td>
  </tr>
  <tr >
    <td height="70%" colspan="3" valign="top">
	<%
	if Not(rsreferencias.BOF and rsreferencias.EOF) then	
	call llenarlista("cboreferencias","",rsreferencias,"codigo_cni","convenio",codigo_cni,"","","multiple")%>
		<script> document.all.cboreferencias.style.height="100%" </script>
	<%end if
	%>

	
	</td>
  </tr>
</table>

	</form>
</body>
</html>