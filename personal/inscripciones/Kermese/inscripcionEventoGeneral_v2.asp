<!--#include file="../../../NoCache.asp"-->
<%
codigo_per= session("codigo_Usu")

'Estos Datos hay que actualizar
    codigo_sco = "952"
   'Llenar datos del combo (se quito combo y se puso hidden)
    codigo_cac="31" '2008-II


'---------------------------------------------------------
set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion

set rs = Server.CreateObject("ADODB.RecordSet")
set rs = obj.consultar("ContarCantidadEntradas", "FO", codigo_sco,codigo_cac)
if rs("limite")="S" then

	
inscrito=0   
cantidad = 0
bloqueo=""	




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



%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link href="../../../private/estilo.css" rel="stylesheet" type="text/css">
<script type="text/javascript" language="JavaScript">
function validar(form){
	//Valida
	if (form.cbopartes.value=="0") { 
		alert("Por favor, indique en cuantas partes pagar� esta inscripci�n");
		form.cbopartes.focus(); 
		return (false);
	}
	
	else
	{
	
	   if (form.cantidad.value=="0" || form.cantidad.value=="" || form.cantidad.value<0 ) { 
		alert("Por favor, indique la cantidad de entradas que comprar�.");
		form.cantidad.focus(); 
		return (false);
	    }
	    else 
	    {
	    
	
		if (confirm("�Est� seguro de registrar esta operaci�n?\n" + "Recuerde que esto le generar� una deuda en Caja-USAT.")==true){
				return(true);
			}
			else{
				return(false);
			}
			
		}	
	}

}


function validarSoloNumero()
{
	if (event.keyCode!=8 )
	{	 
		if (event.keyCode < 48 || event.keyCode > 57 || event.keyCode==46 )
			{event.returnValue = false}
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
    </style>
</HEAD>
<BODY>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="80%" align="center" align="center">
  <tr>
    <td width="60%" class="usattitulo"><b>COMPRA DE ENTRADAS: <%=descripcion_sco%></b></td>
  </tr>
</table>
<br>

<br>

<form  name="frminscripcion" method="post" action="grabarInscripcionEventoGeneral_v2.asp" onSubmit="return validar(this)">
<table cellpadding="3" cellspacing="0" style="border-collapse: collapse; " bordercolor="#111111" width="80%" class="contornotabla" align="center">
<input type="hidden" name="codigo_per" value="<%=codigo_per%>">
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
   	<td width="36%">Precio Unitario:</td>
      	<td width="69%"><b><%=cstr(simbolo_moneda) + " " + cstr(formatNumber(precio_sco))%> </b> </td>
    	</tr>
    	
 <tr>
   	<td width="36%">Cantidad de entradas:</td>
      	<td width="69%"><input type=text name="cantidad" value="<%=cantidad%>" onKeyPress="validarSoloNumero()" maxlength=2 <%=bloqueo%>> 
      	<input type=hidden name = cbopartes value="1">
      	
      	</td>
    	</tr>
        
      <td width="36%">&nbsp;</td>
      <td width="69%" class="rojo">
     
        
	      <br />
          <br />
        <%if inscrito=0 then
        response.write ""
      %> 
	  <input type="submit" value="Registrar"  name="smtGuardar" class="usatguardar"> 
      <input OnClick="location.href='../../listaaplicaciones.asp'" type="button" value="Cancelar" name="cmdCancelar" class="usatsalir">
      
	  <%else
	  		response.write "Operaci�n Registrada correctamente. Usted ha comprado " +  cstr(cantidad) + " entradas."
  		%>
			<input OnClick="location.href='../../listaaplicaciones.asp'" type="button" value="Cerrar" name="cmdCancelar" class="usatsalir">
			
	  <%end if%>
	  
      </td>
    </tr>
	
  </table>
</form>




<%else
	  		response.write "Ya no hay entradas disponibles."
  		%>
			<input OnClick="location.href='../../listaaplicaciones.asp'" type="button" value="Cerrar" name="cmdCancelar" class="usatsalir">
			
	  <%end if%>


<%
set rs = Server.CreateObject("ADODB.RecordSet")
set rs = obj.consultar("consultarExistenciaDeuda", "FO", "P",codigo_per, codigo_sco,0)
obj.CerrarConexion
set obj = nothing
%>



<table border="1" cellpadding="0" cellspacing="0" style="border"   bordercolor="#111111" width="80%" align="center">
<tr><td colspan=5 align="center">Detalle de compra:</td></tr>
<tr>
    <td width="15%" class="etabla" align="center" height="20">Fecha Registro</td>
    <td width="15%" class="etabla" align="center" height="20">Cant.Entradas</td>
    <td width="15%" class="etabla" align="center" height="20">Cargo (S/.)</td>
    <td width="15%" class="etabla" align="center" height="20">Abono (S/.)</td>
    <td width="15%" class="etabla" align="center" height="20">Saldo (S/.)</td>
</tr>
<% Do while not rs.eof%>

    <tr>
    <td align="center" height="20"><%=rs("fecha")%></td>
    <td align="center" height="20"><%=cdbl(rs("montoTotal_Deu"))/20.00 %></td>
    <td align="center" height="20"><%=rs("montoTotal_Deu")%></td>
    <td align="center" height="20"><%=cdbl(rs("montoTotal_Deu")) - cdbl(rs("saldo_deu"))%></td>
    <td align="center" height="20"><%=rs("saldo_Deu")%></td>
    </tr>
    
    <%
    rs.movenext

loop%>


</table>
<p class ="azul" align=center>�D�nde recojo las entradas?<br>
        Si ya te registraste, recogerlas del unes 15 al viernes 19 de setiembre personal administrativo y de mantenimiento en Direcci�n de Personal; los profesores en Direcci�n de Promoci�n y Desarrollo.
        
        </p>
</BODY>
</HTML>