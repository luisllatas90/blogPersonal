<!--#include file="../../../../funciones.asp"-->


<html>
<head>
<title>Registro de Acuerdos</title>

<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
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
<script language="JavaScript" src="../../../../private/calendario.js"></script>
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
          <td width="87%">&nbsp;</td>
          <td colspan="2">&nbsp;</td>
        </tr>
        <tr>
          <td align="center"><span class="Estilo4">Escriba el acuerdo referente a la propuesta y haga clilc en &quot;Agregar&quot; </span></td>
          <td width="5%" align="right"><img src="../../../../images/menus/edit_add.gif" width="16" height="15" border="0"  onClick="RegistraAcuerdo('<%=codigo_prp%>','<%=codigo_rec%>')" style="cursor:hand"> </td>
          <td width="8%">&nbsp;<span class="Estilo6" onClick="RegistraAcuerdo('<%=codigo_prp%>','<%=codigo_rec%>')" style="cursor:hand"><strong>Agregar&nbsp;&nbsp;</strong></span></td>
        </tr>
        <tr>
          <td colspan="3">&nbsp;</td>
        </tr>
        <tr>
          <td colspan="3"><table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
              <tr>
                <td><textarea style="font-size:14px" name="txtacuerdo" rows="5" class="Cajas2" id="txtacuerdo"></textarea></td>
              </tr>
          </table></td>
        </tr>
        <tr>
          <td colspan="3">&nbsp;</td>
        </tr>
        <tr>
          <td colspan="3"><hr color="#990000"></td>
        </tr>
        <tr bgcolor="#FFFFFF">
          <td colspan="3"><table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td colspan="5"><span class="Estilo4">Acuerdos:</span>
                  <%	    Set objProp=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	objProp.AbrirConexiontrans
			set ACUERDOS=objProp.Consultar("ConsultarAcuerdosPropuesta","FO","PR",codigo_prp)
	    	objProp.CerrarConexiontrans
			set objProP=nothing
	%>              </td>
            </tr>
            <tr>
              <td colspan="5">&nbsp;</td>
            </tr>
            <% do while not ACUERDOS.eof
		  i=i+1
		  %>
            <tr>
              <td width="4%" valign="top"><span class="Estilo6">
                <%response.write(i)%>
                .-</span></td>
              <td width="88%" valign="top">
			<%FILA= LEN(ACUERDOS("descripcion_apr"))/70+2%>
			  <textarea name="TextArea_<%=ACUERDOS("codigo_apr")%>"  id="TextArea_<%=ACUERDOS("codigo_apr")%>" rows="<%=FILA%>" class="Cajas2" readonly="readonly" style="border:none; overflow:hidden" ><%response.write(ACUERDOS("descripcion_apr"))%></textarea>			  </td>
              <td width="3%" align="center" valign="top"><img src="../../../../images/menus/editar.gif" alt="Editar" width="18" height="13" border="0" style="cursor:hand" onClick="modificar('TextArea_<%=ACUERDOS("codigo_apr")%>','guardar_<%=ACUERDOS("codigo_apr")%>')"></td>
              <td width="3%" align="center" valign="top"> <img id="guardar_<%=ACUERDOS("codigo_apr")%>" src="../../../../images/menus/guardar.gif" alt="Guardar" width="16" height="16" border="0" style="cursor:hand; visibility:hidden" onClick="Actualizar('<%=ACUERDOS("codigo_apr")%>','<%=codigo_prp%>')" ></td>
              <td width="3%" align="center" valign="top">
			  <img src="../../../../images/menus/noconforme_small.gif" alt="Eliminar" width="16" height="16"  border="0" style="cursor:hand" onClick="Eliminar('<%=ACUERDOS("codigo_apr")%>','<%=codigo_prp%>')">
			  
			  </td>
            </tr>
            <%
		  ACUERDOS.MoveNext
		  loop%>
            <tr>
              <td colspan="5">&nbsp;</td>
            </tr>
          </table></td>
		  </tr>
      </table></td>
    </tr>
  </table>
</form>
</body>
</html>
