<!--#include file="../../../NoCache.asp"-->
<%
codigo_per= session("codigo_Usu")
codigo_sco = "925"


set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion

set rs = Server.CreateObject("ADODB.RecordSet")
set rs = obj.consultar("consultarExistenciaDeuda", "FO", "P",codigo_per, codigo_sco,0)
	
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
<title>Semana de Ingeniería</title> 
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link href="../../../private/estilo.css" rel="stylesheet" type="text/css">
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
	
		if (confirm("¿Está seguro de registrar su inscripción en el evento?\n" + "Recuerde que esto le generará un cargo en Caja-USAT.")==true){
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
    <p>
        &nbsp;</p>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="60%" class="usattitulo"><b class="azul">1.- REGISTRA TU INSCRIPCIÓN EN LA 
        SEMANA DE INGENIERÍA 2008</b></td>
  </tr>
</table>
<br>
<form  name="frminscripcion" method="post" action="grabarinscripcioneventoSEMANING.asp" onSubmit="return validar(this)">
		<input type =hidden name ="cbopartes" value="1">
		
<table cellpadding="3" cellspacing="0" style="border-collapse: collapse; " bordercolor="#111111" width="100%" class="contornotabla">
<input type="hidden" name="codigo_per" value="<%=codigo_per%>">
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
 <td >Participante:</td>
  <td ><b><% =session("Nombre_Usu") %></b></td>
 </tr>
 
 <tr>
   	<td width="36%">Servicio:</td>
   	 <td width="69%" class="usatTitulousat"><select name="codigo_sco" id="codigo_sco" >
                <option value="0">---Seleccione el servicio que desee--- </option>
                <option value="416"> INSC. SEMANA   (S/. 25.00)</option>
                <!--<option value="926"> ACOMPAÑANTE (S/. 25.00) </option>-->
                <!--<option value="966"> DERECHO POLO SEMANA (S/. 20.00)</option>-->
              </Select>
                <br />
                        </td>
 </tr>
 <tr>
   	<td width="36%">&nbsp;</td>
      	<td width="69%">&nbsp;</td>
    	</tr>
     
      
      <td width="36%">&nbsp;</td>
      <td width="69%" align="right" class="rojo">
      <%if inscrito=0 then
      
      response.write "<h3><br>No inscrito.</h3>"
      response.write "Clic en Grabar para inscribirse."
      %>
	  <input type="submit" value="Grabar"  name="smtGuardar" class="usatguardar"> 
	  
      <input OnClick="location.href='../../listaaplicaciones.asp'" type="button" value="Cancelar" name="cmdCancelar" class="usatsalir">
	  <%else
	  		response.write "<h3>Gracias por inscribirse.<br>Usted ya se encuentra inscrito.</h3>"
  		%>
			<input OnClick="location.href='../../listaaplicaciones.asp'" type="button" value="Cerrar" name="cmdCancelar" class="usatsalir">
	  <%end if%>
	  
      </td>
    </tr>
	<!--
	<tr>
	<td class="azul"><a href="http://www.usat.edu.pe/avances/semanaingenieria/" target="_blank"><b>Visite la página del SINSYC</b></a>
	</td>
	<td></td>
	</tr>-->
	</table>
  <br>
	  
</form>
</BODY>
</HTML>