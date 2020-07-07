<!--#include file="../../../NoCache.asp"-->
<%
codigo_alu= session("codigo_alu")
'Estos Datos hay que actualizar
    codigo_sco = "72"
   'Llenar datos del combo (se quito combo y se puso hidden)
    codigo_cac="33" '2009-I


'---------------------------------------------------------
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
	
		if (confirm("¿Está seguro de registrar su inscripción?\n" + "Recuerde que esto le generará un cargo en Caja-USAT.")==true){
				return(true);
			}
			else{
				return(false);
			}
	}

}
</script>
    <style type="text/css">

 p.MsoNormal
	{margin-bottom:.0001pt;
	font-size:12.0pt;
	font-family:"Times New Roman";
	        margin-left: 0cm;
            margin-right: 0cm;
            margin-top: 0cm;
        }
        .tituloSemana
        {
            background-color: #6699FF;
            color: #FFFFFF;
            font-weight: bold;
            height: 30px
        }
    </style>
</HEAD>
<BODY>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="100%" class="tituloSemana">&nbsp; REGISTRO DE INSCRIPCIÓN: <%=descripcion_sco%></td>
  </tr>
</table>
<br>
<!--#include file="../../fradatos.asp"-->
<br>

<form  name="frminscripcion" method="post" action="grabarSemanaEnfermeria.asp" onSubmit="return validar(this)">
<table cellpadding="3" cellspacing="0" style="border-collapse: collapse; " bordercolor="#111111" width="100%" class="contornotabla">
<input type="hidden" name="codigo_alu" value="<%=codigo_alu%>">
<input type="hidden" name="codigo_sco" value="<%=codigo_sco%>">
<input type="hidden" name="codigo_cac" value="<%=codigo_cac%>">
<input type="hidden" name="precio_sco" value="<%=precio_sco%>">
<input type="hidden" name="moneda_sco" value="<%=moneda_sco%>">
<input type="hidden" name="generamora_sco" value="<%=generamora_sco%>">
<input type="hidden" name="fechaVencimiento_sco" value="<%=fechaVencimiento_sco%>">
<input type="hidden" name="codigo_cco" value="<%=codigo_cco%>">
   	
 <tr>
     <td width="100%" colspan="2" class="etabla" style="text-align: left" height="13">Datos de Inscripci&oacute;n</td>
 </tr>
 
 <tr>
   	<td width="36%">Servicio:</td>
   	<td width="69%"><%=descripcion_sco%></td>
 </tr>
 <tr>
   	<td width="36%">Precio del servicio:</td>
      	<td width="69%"><b><%=cstr(simbolo_moneda) + " " + cstr(formatNumber(precio_sco))%> </b> </td>
    	</tr>
        <tr>
      	<td width="36%">Numero de Cuotas:</td>
      	<td width="69%">2 partes: (1era cuota vence: Mayo, 2da cuota vence: Junio)<input type=hidden name = cbopartes value="2">
		&nbsp;</td>
    	</tr>
      
      <td width="36%" style="color: #000080">&nbsp;</td>
      <td width="69%" class="rojo">
     
        
        <%if inscrito=0 then
             'response.write "<h3>Estado Actual: No Inscrito.</h3>"
             response.write " "
      %> 
	  <input type="submit" value="Acepto"  name="smtGuardar" class="usatguardar"> 
      <input OnClick="location.href='about:blank'" type="button" value="Cancelar" name="cmdCancelar" class="usatsalir">
	  <%else
	  		response.write "<h3>Usted ya se encuentra inscrito.</h3>"
	  		
  		%>
			<input OnClick="location.href='about:blank'" type="button" value="Cerrar" name="cmdCancelar" class="usatsalir">
	  <%end if%>
	  
      </td>
    </tr>
	
  </table>
<br />
<br />
<p class="boton">
    Con esto tienes derecho a :<br /><br />
    - 1 Polo de la USAT.<br />
    - 1 Certificado de la Jornada de Investigación de Estudiantes.<br />
    - 1 Cena de Gala.</p>
</form>
</BODY>
</HTML>