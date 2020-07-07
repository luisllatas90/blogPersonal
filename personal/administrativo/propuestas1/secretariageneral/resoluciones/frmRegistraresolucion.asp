<!--#include file="../../../../../funciones.asp"-->


<html>
<head>
<title>Registro deResoluci&oacute;n</title>

<link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--
body {
	background-color: #f0f0f0;
}
.Estilo4 {
	color: #000000;
	font-weight: bold;
}
.Estilo5 {color: #000000}
-->
</style></head>
<script language="JavaScript" src="../../../../../private/calendario.js"></script>
<script language="JavaScript">
function RegistrarInstitucion(){
var resolucion=frminstitucion.txtresolucion.value
var tipo=frminstitucion.cbotipo.value
var fecha=frminstitucion.txtFechaInicio.value

var cadena = "Ingrese los datos: "	

if (resolucion==""){
cadena = cadena + " Resolución |"
}
if (tipo<0){
cadena = cadena + " Tipo |"
}
if (fecha==""){
cadena = cadena + " Fecha |"
}

if (cadena != "Ingrese los datos: "){
alert(cadena)
}
else{
popUp ("procesar.asp?resolucion=" + resolucion + "&tipo=" + tipo + "&fecha=" + fecha  + "&accion=resolucion")
//window.opener.location.reload()
}
window.close()
}

	function popUp(URL) {
	day = new Date();
	id = day.getTime();
	var izq = 300//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=1px,height=1px,left = "+ izq +",top = "+ arriba +"');");
	}

</script>
	  
<body topmargin="0" rightmargin="0" leftmargin="0">
<form action="procesar.asp?accion=" method="post" name="frminstitucion" id="frminstitucion">
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td class="bordeinf"><table  width="95%" height="100%" border="0" align="center" cellpadding="0" cellspacing="5">
        <tr>
          <td valign="top"><input  name="cmdborrador" type="button" class="guardar_prp" id="cmdborrador" value="          Guardar"  onClick="RegistrarInstitucion()">
            &nbsp;&nbsp;&nbsp;</td>
          <td align="right" valign="top"><input onClick="window.close()"  name="cmdborrador2" type="button" class="noconforme1" id="cmdborrador2" value="         Cerrar" ></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td valign="top"><table width="85%" border="0" align="center" cellpadding="0" cellspacing="0">
        
        
        <tr>
          <td width="26%">&nbsp;</td>
          <td width="74%">&nbsp;</td>
        </tr>
        <tr>
          <td colspan="2"><span class="Estilo4">Datos de la Resoluci&oacute;n </span></td>
          </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td><span class="Estilo5">Tipo </span></td>
          <td><span class="Estilo5">
            <% 
		 	Set objCC1=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC1.AbrirConexion
			set rstiporesol=objCC1.Consultar("ConsultarTipoResolucion","FO","TO",0)
	    	ObjCC1.CerrarConexion
			set objCC1=nothing
		
		 	call llenarlista("cbotipo","",rstiporesol,"codigo_tru","descripcion_tru",codigo_tru,"Seleccionar Tipo de Resoluci&oacute;n","","")
			set rsAmbito = nothing
		 %>
          </span></td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td><span class="Estilo5">Fecha</span></td>
          <td><span class="Estilo5">
            <input disabled="disabled" name="txtFechaInicio" type="text" id="txtFechaInicio" size="10" maxlength="10">
            <input name="Submit" type="button" class="cunia" value="  " onClick="MostrarCalendario('txtFechaInicio')" >
          </span></td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td><span class="Estilo5">Resoluci&oacute;n</span></td>
          <td><span class="Estilo5">
            <input name="txtresolucion" type="text" id="txtresolucion" size="50" maxlength="50">
          </span></td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        
        <tr>
          <td valign="top"><span class="Estilo5">Observaci&oacute;n</span></td>
          <td><span class="Estilo5">
            <textarea name="txtresolucion2" cols="50" rows="4" id="txtresolucion2" style="overflow:hidden"></textarea>
          </span></td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
      </table></td>
    </tr>
  </table>
  <p>&nbsp;</p>
</form>
</body>
</html>
