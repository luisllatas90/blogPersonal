<!--#include file="../../../../../funciones.asp"-->


<html>
<head>
<title>Registro de Instituci&oacute;n</title>

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
.Estilo7 {color: #395ACC; font-weight: bold; }
-->
</style></head>
<script language="JavaScript">
function RegistrarInstitucion(){
	var institucion=frminstitucion.txtinstitucion.value
	var abreviatura=frminstitucion.txtabreviatura.value
	var tipo=frminstitucion.cbotipo.value
	var direccion=frminstitucion.txtdireccion.value
	var telefax=frminstitucion.txttelefono.value
	var ciudad=frminstitucion.txtciudad.value
	var pais=frminstitucion.cbopais.value
	var web=frminstitucion.txtweb.value
	var email=frminstitucion.txtemail.value
	var contacto=frminstitucion.txtcontacto.value
	var emailcontacto=frminstitucion.txtemailcontacto.value
	var cadena = "Ingrese los datos: "	
	if (institucion==""){
	cadena = cadena + " Institución |"
	}
	if (tipo<0){
	cadena = cadena + " Tipo |"
	}
	if (ciudad==""){
	cadena = cadena + " Ciudad |"
	}
	if (pais<0){
	cadena = cadena + " País |"
	}
	if (cadena != "Ingrese los datos: "){
	alert(cadena)
}
else{
	//location.href="procesar.asp?institucion=" + institucion + "&abreviatura=" + abreviatura + "&tipo=" + tipo + "&direccion=" + direccion + "&telefax=" + telefax + "&pais=" + pais + "&web=" + web  + "&ciudad=" + ciudad + "&email=" + email + "&contacto=" + contacto + "&emailcontacto=" + emailcontacto + "&accion=institucion"
	popUp("procesar.asp?institucion=" + institucion + "&abreviatura=" + abreviatura + "&tipo=" + tipo + "&direccion=" + direccion + "&telefax=" + telefax + "&pais=" + pais + "&web=" + web  + "&ciudad=" + ciudad + "&email=" + email + "&contacto=" + contacto + "&emailcontacto=" + emailcontacto + "&accion=institucion")
	//if (window.opener.closed==false){
		//window.opener.location.reload()
//}
window.close()
}
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
      <td><table width="85%" border="0" align="center" cellpadding="0" cellspacing="0">
        
        
        <tr>
          <td width="26%">&nbsp;</td>
          <td width="74%">&nbsp;</td>
        </tr>
        <tr>
          <td colspan="2"><span class="Estilo4">Datos de la Instituci&oacute;n </span></td>
          </tr>
        <tr>
          <td><span class="Estilo5">Instituci&oacute;n</span></td>
          <td><span class="Estilo5">
            <input name="txtinstitucion" type="text" id="txtinstitucion" size="50" maxlength="90">
          </span></td>
        </tr>
        <tr>
          <td><span class="Estilo5">Abreviatura</span></td>
          <td><span class="Estilo5">
            <input name="txtabreviatura" type="text" id="txtabreviatura" size="20" maxlength="20">
          </span></td>
        </tr>
        <tr>
          <td><span class="Estilo5">Tipo</span></td>
          <td><span class="Estilo5">
            <% 
		 	Set objCC1=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC1.AbrirConexion
			set rstipoinst=objCC1.Consultar("ConsultarTipoInstitucion","FO","TO",0)
	    	ObjCC1.CerrarConexion
			set objCC1=nothing
		
		 	call llenarlista("cbotipo","",rstipoinst,"codigo_tis","descripcion_tis",tipo,"Seleccionar Tipo Institución","","")
			set rsAmbito = nothing
		 %>
          </span></td>
        </tr>
        <tr>
          <td><span class="Estilo5">Direcci&oacute;n</span></td>
          <td><span class="Estilo5">
            <input name="txtdireccion" type="text" id="txtdireccion" size="50" maxlength="90">
          </span></td>
        </tr>
        <tr>
          <td><span class="Estilo5">Tel&eacute;fono / Fax </span></td>
          <td><span class="Estilo5">
            <input name="txttelefono" type="text" id="txttelefono" size="20" maxlength="20">
          </span></td>
        </tr>
        <tr>
          <td><span class="Estilo5">Ciudad</span></td>
          <td><span class="Estilo5">
            <input name="txtciudad" type="text" id="txtciudad" size="20" maxlength="20">
          </span></td>
        </tr>
        <tr>
          <td><span class="Estilo5">Pais</span></td>
          <td><span class="Estilo5">
            <% 
		 	Set objCC1=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	ObjCC1.AbrirConexiontrans
			set rspais=objCC1.Consultar("ConsultarLugares","FO","1",0,0)
	    	ObjCC1.CerrarConexiontrans
			set objCC1=nothing
		
		 	call llenarlista("cbopais","",rspais,"codigo_pai","nombre_pai",Pais,"Seleccionar Pa&iacute;s","","")
			set rsAmbito = nothing
		 %>
          </span></td>
        </tr>
        <tr>
          <td><span class="Estilo5">P&aacute;gina Web </span></td>
          <td><span class="Estilo5">
            <input name="txtweb" type="text" id="txtweb" size="50" maxlength="50">
          </span></td>
        </tr>
        <tr>
          <td><span class="Estilo5">email</span></td>
          <td><span class="Estilo5">
            <input name="txtemail" type="text" id="txtemail" size="50" maxlength="50">
          </span></td>
        </tr>
        <tr>
          <td><span class="Estilo5"></span></td>
          <td><span class="Estilo5"></span></td>
        </tr>
        <tr>
          <td colspan="2"><span class="Estilo4">Datos del Contacto </span><span class="Estilo5"></span></td>
          </tr>
        <tr>
          <td><span class="Estilo5"></span></td>
          <td><span class="Estilo5"></span></td>
        </tr>
        <tr>
          <td><span class="Estilo5">Nombre</span></td>
          <td><span class="Estilo5">
            <input name="txtcontacto" type="text" id="txtcontacto" size="50" maxlength="50">
          </span></td>
        </tr>
        <tr>
          <td><span class="Estilo5">email</span></td>
          <td><span class="Estilo5">
            <input name="txtemailcontacto" type="text" id="txtemailcontacto" size="50" maxlength="50">
          </span></td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        
      </table></td>
    </tr>
  </table>
  </form>
</body>
</html>