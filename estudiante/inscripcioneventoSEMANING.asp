<!--#include file="../NoCache.asp"-->
<%

'----------------------------------------------------------------------
'Fecha: 29.10.2012
'Usuario: yperez
'Motivo: Cambio de URL del servidor de la WebUSAT [www.usat.edu.pe->intranet.edu.pe]
'----------------------------------------------------------------------


codigo_alu= session("codigo_alu")
codigo_sco = "925"


set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion

set rs = Server.CreateObject("ADODB.RecordSet")
set rs = obj.consultar("consultarExistenciaDeuda", "FO", "E",codigo_alu, codigo_sco,0)
	
inscrito=0   
	
if rs.recordcount >0 then
	inscrito= 1 
	nroPartes= rs("nroPartes_deu")
end if

rs.close


set rs = obj.consultar("ConsultarServicioConcepto", "FO", "CO",codigo_sco)

descripcion_sco= rs("descripcion_sco")
precio_sco= rs("precio_sco")
simbolo_Moneda = rs("simbolo_Moneda")
moneda_sco= rs("moneda_sco")
generamora_sco= rs("generamora_sco")
fechaVencimiento_sco = rs("fechaVencimiento_sco")
codigo_cco = rs("codigo_cco")

rs.close
set rs=nothing

obj.CerrarConexion
set obj = nothing

%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link href="../private/estilo.css" rel="stylesheet" type="text/css">
<script type="text/javascript" language="JavaScript">
function validar(form){
	//Valida
	if (form.cbopartes.value=="0") { 
		alert("Por favor, indique en cuantas partes pagará esta inscripción");
		form.cbopartes.focus(); 
		return (false);
	}
	
	else
	{
	
		if (confirm("¿Está seguro de registrar su inscripción en el evento?\n" + "Recuerde que esto le generará un cargo en Caja-USAT")==true){
				return(true);
			}
			else{
				return(false);
			}
	}

}
</script>
</HEAD>
<BODY>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="60%" class="usattitulo"><b class="azul">1.- REGISTRA TU INSCRIPCIÓN EN&nbsp;LA 
        SEMANA DE INGENIERÍA 2008</b></td>
  </tr>
</table>
<!--#include file="fradatos.asp"-->
<br>

<form  name="frminscripcion" method="post" action="grabarinscripcioneventoSEMANING.asp" onSubmit="return validar(this)">
<table cellpadding="3" cellspacing="0" style="border-collapse: collapse; " bordercolor="#111111" width="100%" class="contornotabla">
<input type="hidden" name="codigo_alu" value="<%=codigo_alu%>">
<input type="hidden" name="codigo_sco" value="<%=codigo_sco%>">
<input type="hidden" name="precio_sco" value="<%=precio_sco%>">
<input type="hidden" name="moneda_sco" value="<%=moneda_sco%>">
<input type="hidden" name="generamora_sco" value="<%=generamora_sco%>">
<input type="hidden" name="fechaVencimiento_sco" value="<%=fechaVencimiento_sco%>">
<input type="hidden" name="codigo_cco" value="<%=codigo_cco%>">
   	
 <tr>
     <td width="100%" colspan="2" class="etabla" style="text-align: left" height="13">Datos de Inscripci&oacute;n</td>
 </tr>
 
 <tr>
 <td >Evento:</td>
 <td ><b>Semana de Ingeniería 2008<br>(02-06 Junio)</b></td>
 </tr>
 <tr>
   	<td width="36%">Servicio:</td>
   	<td width="69%"><%=descripcion_sco%></td>
 </tr>
 <tr>
   	<td width="36%">Costo:</td>
      	<td width="69%"><b><%=cstr(simbolo_moneda) + " " + cstr(formatNumber(precio_sco))%> </b></td>
    	</tr>
        <tr>
      	<td width="36%">Numero de Cuotas:</td>
      	<td width="69%">
		<%if inscrito=0 then 
			habilitar=""
			
		else
			habilitar="disabled='disabled'"
		end if
		
				
		%>
		
		    1 Cuota - Con la pensión de junio<input type =hidden name ="cbopartes" value="1"></td>
    	</tr>
      
      <td width="36%">&nbsp;</td>
      <td width="69%" align="right" class="rojo">
      <%if inscrito=0 then
      response.write "<h3><br>No inscrito.</h3>"
      response.write "Clic en Grabar para inscribirse."
      %>
	  <input type="submit" value="Grabar"  name="smtGuardar" class="usatguardar"> 
      <input OnClick="location.href='avisos/ingenieria/index.asp'" type="button" value="Cancelar" name="cmdCancelar" class="usatsalir">
	  <%else
	  		response.write "<h3>Gracias por inscribirse.<br>Usted ya se encuentra inscrito.</h3>"
  		%>
			<input OnClick="location.href='avisos/ingenieria/index.asp'" type="button" value="Cerrar" name="cmdCancelar" class="usatsalir">
	  <%end if%>
	  
      </td>
    </tr>
	<!--
	<tr>
	<td class="azul"><a href="https://intranet.usat.edu.pe/avances/semanaingenieria/" target="_blank"><b>Visite la página del SINSYC</b></a>
	</td>
	<td></td>
	</tr>-->
	</table>
  <br>
	<br>
	<br>
  <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="60%" class="usattitulo"><b class="azul" style="color: #800000">2. REGISTRA TUS ACOMPAÑANTES AL ALMUERZO DE LA SEMANA DE INGENIERIA 2008:&nbsp; 
        </b><a href="fiestaGala.asp" > Click Aquí </a></td>
  </tr>
  <tr>
  <td ><br><br><a href ="fiestaGala.asp"><img src="avisos/ingenieria/almuerzo.jpg"/></a></td>
  </tr>
</table>
  
</form>
</BODY>
</HTML>