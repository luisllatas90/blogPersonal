<!--#include file="../../../../funciones.asp"-->


<html>
<head>
<title>Registro Actividades de la Propuesta</title>

<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
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
.Estilo6 {color: #990000}
-->
</style></head>
<script language="JavaScript" src="../../../../private/calendario.js"></script>
<script language="JavaScript">
function RegistrarInstitucion(){
var descripcion=frmActividad.txtDescripcion.value
var fechainicio = frmActividad.txtFechaInicio.value
var fechafin = frmActividad.txtFechaFin.value

var cadena = "Ingrese los datos: "	

if (descripcion==""){
cadena = cadena + " Descripción |"
}

if (fechainicio==""){
cadena = cadena + " Fecha Inicio |"
}

if (fechafin==""){
cadena = cadena + " Fecha Fin |"
}

if (cadena != "Ingrese los datos: "){
alert(cadena)
}
else{
//popUp ("procesar.asp?resolucion=" + resolucion + "&tipo=" + tipo + "&fecha=" + fecha  + "&accion=resolucion")
frmActividad.submit()
//window.opener.location.reload()
}
//window.close()
}

	function popUp(URL) {
	day = new Date();
	id = day.getTime();
	var izq = 300//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=1px,height=1px,left = "+ izq +",top = "+ arriba +"');");
	}
function validarnumeros()
{
   if (event.keyCode < 45 || event.keyCode > 57)
	{event.returnValue = false}
}
</script>
	  
<body topmargin="0" rightmargin="0" leftmargin="0">
<%codigo_dap=Request.QueryString("codigo_dap")%>
<form action="RegistraActividad_prop.asp?accion=guardar&codigo_dap=<%=codigo_dap%>" method="post" name="frmActividad" id="frmActividad">
<%
accion= Request.QueryString("accion")
if accion="guardar" then
codigo_dap=Request.QueryString("codigo_dap")

					Set ObjAct=Server.CreateObject("PryUSAT.clsAccesoDatos")
					ObjAct.AbrirConexion
					ObjAct.Ejecutar "RegistraActividadesPropuesta",false,codigo_dap,Request.Form("txtDescripcion"),Request.Form("txtFechaInicio"),Request.Form("txtFechaFin"),iif(Request.Form("txtobservacion")="",null,Request.Form("txtobservacion")),iif(Request.Form("txtCostoAprox")="",null,Request.Form("txtCostoAprox"))
					ObjAct.CerrarConexion
					set ObjAct=nothing%>
					<script>
					window.opener.parent.document.listaAct.location.href="actividadesPropuesta.asp?codigo_dap=<%=codigo_dap%>"
					if (confirm("Los datos se guardaron correctamente. ¿Desea Registrar otra actividad?")==false){
					window.close()
					}

					</script>
<%end if

%>
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
      <td valign="top"><table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
        
        
        <tr>
          <td width="22%">&nbsp;</td>
          <td width="78%">&nbsp;</td>
        </tr>
        <tr>
          <td colspan="2"><span class="Estilo4">Datos de la Actividad </span></td>
          </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td><span class="Estilo5"><span class="Estilo6">*</span> Descripci&oacute;n</span></td>
          <td><span class="Estilo5">
            <input name="txtDescripcion" type="text" class="Cajas2" id="txtDescripcion" maxlength="50">
          </span></td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td><span class="Estilo5"><span class="Estilo6">*</span> Fecha Inicio </span></td>
          <td><span class="Estilo5">
            <input readonly="readonly" name="txtFechaInicio" type="text" id="txtFechaInicio" size="8" maxlength="10">
            <input name="Submit" type="button" class="cunia" value="  " onClick="MostrarCalendario('txtFechaInicio')" >
          </span></td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td><span class="Estilo5"><span class="Estilo6">*</span> Fecha Fin </span></td>
          <td><span class="Estilo5">
            <input  readonly="readonly" name="txtFechaFin" type="text" id="txtFechaFin" size="8" maxlength="10">
            <input name="Submit2" type="button"  class="cunia" value="  " onClick="MostrarCalendario('txtFechaFin')" >
          </span></td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td><span class="Estilo5">Observaci&oacute;n</span></td>
          <td><span class="Estilo5">
            <input name="txtobservacion" type="text" class="Cajas2" id="txtobservacion" maxlength="50">
          </span></td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td><span class="Estilo5">Costo Aproximado</span> </td>
          <td>
            <input name="txtCostoAprox" onKeyPress="validarnumeros()" type="text" id="txtCostoAprox" value="0" size="15" maxlength="15">
          <span id="moneda" name="moneda" class="Estilo5"></span>
		  <script>
		  if (window.opener.frmpropuesta.chkdolar.checked==true){
		  	//alert(window.opener.frmpropuesta.chkdolar.checked)
			document.all.moneda.innerText=' Dólares'	
		  }else{
		  document.all.moneda.innerText=' Nuevos Soles'	
		  }
		  </script>
		  </td>
        </tr>
        
        <tr>
          <td>&nbsp;</td>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
          <td align="right"><span class="Estilo6">* Son campos obligatorios </span></td>
        </tr>
      </table></td>
    </tr>
  </table>
</form>
</body>
</html>
