<!--#include file="../../../../../funciones.asp"-->


<html>
<head>
<title>Registro de Acuerdos</title>

<link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--

.Estilo4 {
	color: #000000;
	font-weight: bold;
}
.Estilo5 {
	font-size: 10pt;
	font-weight: bold;
	color: #000000;
}
.Estilo6 {color: #000000}
.Estilo7 {
	color: #990000;
	font-size: 16px;
}
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
cadena = cadena + " Resoluci�n |"
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
location.href="procesar.asp?resolucion=" + resolucion + "&tipo=" + tipo + "&fecha=" + fecha  + "&accion=resolucion"
window.opener.location.reload()
window.close()
}
}

function RegistraAcuerdo(codigo_prp,codigo_rec){
var acuerdo=frminstitucion.txtacuerdo.value
	if (acuerdo==""){
	alert ("Debe escribir el contenido del acuerdo antes de guardarlos")
	}
	else{
//	alert (codigo_prp)
//	alert (codigo_rec)	
	location.href="procesar.asp?codigo_prp=" + codigo_prp + "&codigo_rec=" + codigo_rec + "&acuerdo=" + acuerdo  + "&accion=registraAcuerdo"	
	}
}
function Actualizar(codigo_apr,codigo_prp){
//	alert (codigo_apr)
	var texto=eval ("frminstitucion.TextArea_" + codigo_apr + ".value")
//	alert (texto)
	location.href="registraAcuerdo.asp?codigo_apr="+codigo_apr+"&texto="+texto+"&guardar=si&codigo_prp="+codigo_prp
	eval ("frminstitucion.guardar_" + codigo_apr + ".style.visibility='hidden'")
}

function Eliminar(codigo_apr,codigo_prp){
//	alert (codigo_apr)
//	alert (codigo_prp)	
	location.href="registraAcuerdo.asp?codigo_apr="+codigo_apr+"&eliminar=si&codigo_prp="+codigo_prp
}
function modificar(objeto,boton){
//alert (boton)
eval ("frminstitucion." + boton + ".style.visibility='visible'")
eval ("frminstitucion." + objeto + ".style.border='inset'")
eval ("frminstitucion." + objeto + ".readOnly=false")
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
	  
<body topmargin="0" rightmargin="0" leftmargin="0">
<%
codigo_prp=Request.QueryString("codigo_prp")
codigo_rec=Request.QueryString("codigo_rec")
guardar=Request.QueryString("guardar")
eliminar=Request.QueryString("eliminar")

%>

<%
'' actualiza un acuerdo de la propuesta
if guardar="si" then
	codigo_apr=Request.QueryString("codigo_apr")
	texto=Request.QueryString("texto")	
    Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
   	objProp.AbrirConexion
	objProp.Ejecutar "ActualizarAcuerdoPropuesta",false,codigo_apr,texto
   	objProp.CerrarConexion
	set objProP=nothing
end if
%>

<%
'' elimina un acuerdo de la propuesta
if eliminar="si" then
	codigo_apr=Request.QueryString("codigo_apr")
    Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
   	objProp.AbrirConexion
	objProp.Ejecutar "EliminarAcuerdoPropuesta",false,codigo_apr
   	objProp.CerrarConexion
	set objProP=nothing
end if
%>
<form action="procesar.asp?accion=" method="post" name="frminstitucion" id="frminstitucion">
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td class="bordeinf"><table  width="95%" height="100%" border="0" align="center" cellpadding="0" cellspacing="5">
        <tr>
	<%	    Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	objProp.AbrirConexiontrans
			set propuesta=objProp.Consultar("ConsultarPropuestas","FO","CP","","",codigo_prp,"","")
	    	objProp.CerrarConexiontrans
			set objProP=nothing
	%>
          <td valign="top"><span class="Estilo5"><span class="Estilo7">Acuerdos</span> referentes a la Propuesta:</span><font size="2"color="#990000"><strong> <%=propuesta("nombre_prp")%> </strong></font></td>
          </tr>
      </table></td>
    </tr>
    <tr>
      <td valign="top"><table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td width="100%">&nbsp;</td>
        </tr>

        <tr bgcolor="#FFFFFF">
          <td><table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td colspan="2"><%	    Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	objProp.AbrirConexiontrans
			set ACUERDOS=objProp.Consultar("ConsultarAcuerdosPropuesta","FO","PR",codigo_prp)
	    	objProp.CerrarConexiontrans
			set objProP=nothing
	%>              </td>
            </tr>
            <tr>
              <td colspan="2">&nbsp;</td>
            </tr>
            <% do while not ACUERDOS.eof
		  i=i+1
		  %>
            <tr>
              <td width="4%" valign="top"><span class="Estilo6">
                <%response.write(i)%>
                .-</span></td>
              <td valign="top">
			<%FILA= LEN(ACUERDOS("descripcion_apr"))/70+2%>
			  <textarea name="TextArea_<%=ACUERDOS("codigo_apr")%>"  id="TextArea_<%=ACUERDOS("codigo_apr")%>" rows="<%=FILA%>" class="Cajas2" readonly="readonly" style="border:none; overflow:hidden" ><%response.write(ACUERDOS("descripcion_apr"))%></textarea>			  </td>
              </tr>
            <%
		  ACUERDOS.MoveNext
		  loop%>
            <tr>
              <td colspan="2">&nbsp;</td>
            </tr>
          </table></td>
		  </tr>
      </table></td>
    </tr>
  </table>
</form>
</body>
</html>
