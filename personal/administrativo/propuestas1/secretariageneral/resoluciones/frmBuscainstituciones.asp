<!--#include file="../../../../../funciones.asp"-->


<html>
<head>
<title>Registrar instituciones participantes</title>

<link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--
body {
	background-color: #f0f0f0;
}
.Estilo6 {
	color: #000000;
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

<script language="JavaScript" src="../../../../../private/funciones.js"></script>
<script language="JavaScript" src="../../../../../private/calendario.js"></script>
<script>
function RegistrarInstitucion(){
var codigo_ins=frminstituciones.txtcodigo_ins.value
var codigo_cni=frminstituciones.txtcodigo_cni.value
var firmante=frminstituciones.txtfirmante.value
var cargo=frminstituciones.txtcargo.value
var gestor=frminstituciones.chkgestor.checked
	if (gestor==true){
		gestor=1
	}
	else{
		gestor=0
	}
var cadena = "Ingrese los datos: "	
if (codigo_ins==""){
cadena = cadena + " Institución |"
}

if (firmante==""){
cadena = cadena + " Firmante |"
}

if (cargo==""){
cadena = cadena + " Cargo |"
}
if (cadena != "Ingrese los datos: "){
alert(cadena)
}
else{
if (confirm("¿Desea guardar la institución participante?")==true){
//alert ("GUARDAR")
location.href="procesar.asp?codigo_ins=" + codigo_ins + "&codigo_cni=" + codigo_cni + "&firmante=" + firmante + "&cargo=" + cargo + "&gestor=" + gestor + "&accion=participante"
//window.opener.location.reload()
//window.close()

}
else{
//alert ("no GUARDAR")

}
}

}


function Enviarresponucion(){
	
	window.opener.document.frmconvenio.txtResponsable.value=frmresponsables.cboresponsables.value
	window.opener.document.frmconvenio.txtNameResponsable.value=frmresponsables.cboresponsables.options[frmresponsables.cboresponsables.selectedIndex].text	
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
	function pupUpModal(URL){
	showModalDialog(URL,window,"dialogWidth:450px;dialogHeight:400px;status:no;help:no;center:yes;scroll:no");	
	window.location.reload()
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

function CapturarDatosInstitucion(){
	
	frminstituciones.txtcodigo_ins.value=frminstituciones.cboinstitucion.value
	frminstituciones.txtnombre_ins.value=frminstituciones.cboinstitucion.options[frminstituciones.cboinstitucion.selectedIndex].text	
	//window.close()
}
function buscar(){
var descripcion= frminstituciones.txtnombre.value
var codigo_cni=frminstituciones.txtcodigo_cni.value
//alert (descripcion)
	//if (descripcion==""){
		//	alert ("Ingrese la denominación de una Institución")
//		}
//	else{
		location.href="frmBuscainstituciones.asp?respon=" + descripcion + "&codigo_cni=" + codigo_cni
//		}
}
// End -->
</script>

</head>
	  
<body topmargin="0" rightmargin="0" leftmargin="0">
<%codigo_cni=Request.QueryString("codigo_cni")%>
	<form method="post" " name="frminstituciones" id="frminstituciones">
<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td  height="10%" valign="top" class="bordeinf">
	<table  width="97%" height="100%" border="0" align="center" cellpadding="0" cellspacing="5">
      <tr>
        <td valign="top">
		<input  name="cmdborrador" type="button" class="guardar_prp" id="cmdborrador" value="          Guardar"  onClick="RegistrarInstitucion()">
          &nbsp;&nbsp;&nbsp;
          <input  name="cmdborrador3" type="button" class="nuevo1" id="cmdborrador3" value="         Nuevo"  onClick="pupUpModal('frmRegistraInstitucion.asp')"></td>
        <td align="right" valign="top"><input onClick="window.close()"  name="cmdborrador2" type="button" class="noconforme1" id="cmdborrador2" value="         Cerrar" ></td>
      </tr>
    </table>	</td>
  </tr>
  <%
		 	respon=Request.QueryString("respon")
			
			if respon="" then
				respon="-1"
			else
				respon=Request.QueryString("respon")
			end if
			//Response.Write(respon)
			Set prop=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	prop.AbrirConexion
			
			set rsResponsables=prop.Consultar("ConsultarInstitucion","FO","LI",respon,0)
	    	prop.CerrarConexion
			set prop=nothing
			if respon="-1"then
				respon=""
			end if
  
  %>
  <tr >
    <td align="center" valign="top"><table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td height="5%" align="center"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="43%" align="center"><span class="Estilo6">Seleccione o Busque una Instituci&oacute;n </span></td>
            <td width="46%">
            <input name="txtnombre" type="text" class="Cajas2" id="txtnombre" value="<%=respon%>" size="20"></td>
            <td width="11%"><div align="right">
              <input name="cmdbuscar" type="button" class="buscar_prp_small" id="cmdbuscar" value="     Buscar"  onclick="buscar()">
            </div></td>
          </tr>
        </table></td>
      </tr>
      <tr>
        <td height="95%" class="bordeinf"><%
	if Not(rsResponsables.BOF and rsResponsables.EOF) then	
	call llenarlista("cboinstitucion","CapturarDatosInstitucion()",rsResponsables,"codigo_Ins","institucion",codigo_Ins,"","","multiple")%>
          <script> document.all.cboinstitucion.style.height="100%" </script>
          <%end if
	%></td>
      </tr>
      <tr>
        <td height="95%"><table width="100%" height="100%" border="0" cellpadding="4" cellspacing="4">

          <tr>
            <td width="24%"><span class="Estilo6">Instituci&oacute;n</span>
              <input name="txtcodigo_ins" type="hidden" id="txtcodigo_ins" size="5"></td>
            <td width="76%">
            <input disabled="disabled" name="txtnombre_ins" type="text" class="Cajas2" id="txtnombre_ins" size="20"></td>
          </tr>
          <tr>
            <td><span class="Estilo6">Firmante</span></td>
            <td><input name="txtfirmante" type="text" class="Cajas2" id="txtfirmante" size="50" maxlength="100"></td>
          </tr>
          <tr>
            <td><span class="Estilo6">Cargo</span></td>
            <td><input name="txtcargo" type="text" class="Cajas2" id="txtcargo" size="100" maxlength="100"></td>
          </tr>
          <tr>
            <td><span class="Estilo6">Gestor
              <input name="txtcodigo_cni" type="hidden" id="txtcodigo_cni" value="<%=codigo_cni%>" size="20">
            </span></td>
            <td><input name="chkgestor" type="checkbox" id="chkgestor" value="checkbox"></td>
          </tr>

        </table></td>
      </tr>
    </table></td>
    </tr>
</table>

	</form>
</body>
</html>