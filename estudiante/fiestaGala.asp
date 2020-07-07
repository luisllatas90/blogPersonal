<!--#include file="../NoCache.asp"-->
<%
codigo_alu= session("codigo_alu")

'Codigo del servicio de semana
codigo_sco = "925"


set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion

set rs = Server.CreateObject("ADODB.RecordSet")
set rs = obj.consultar("consultarExistenciaDeuda", "FO", "E",codigo_alu, codigo_sco,0)
	

	
if rs.recordcount >0 then 'Se ha inscrito en la semana

	'Consultar datos del servicio de almuerzo
	codigo_sco ="926"
	
	
	nroamigos=2 'Nro máximo de acompañantes
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
<link href="../private/estilo.css" rel="stylesheet" type="text/css">
<script type="text/javascript" language="JavaScript">
function validar(form){
	//Valida
	if (form.txtApellidos.value=="" || form.txtNombres.value=="" ) 
	{ 
		if (form.txtApellidos.value==""){
			alert("Por favor, debe de ingresar los apellidos de su acompañante!");
			form.txtApellidos.focus(); 
			return (false);
		}
		else
		{
			alert("Por favor, debe de ingresar los nombres de su acompañante!");
			form.txtNombres.focus(); 
			return (false);
		}	
	}
	
	else
	{
	
		if (confirm("¿Está seguro de registrar a su acompañante?\n" + "Recuerde que deberá cancelar S/. 15.00 por acompañante!")==true)
			{
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
    <td width="60%" class="usattitulo"><b style="color: #800000">REGISTRO DE ACOMPAÑANTES PARA ALMUERZO SEMANA DE INGENIERIA 2008 
    </b></td>
  </tr>
</table>
<br>
<!--#include file="fradatos.asp"-->
<br>

<form  name="frminscripcion" method="post" action="grabarFiestaGala.asp" onSubmit="return validar(this)">
	<table cellpadding="3" cellspacing="0" style="border-collapse: collapse; " bordercolor="#111111" width="100%" class="contornotabla">
		<input type="hidden" name="codigo_alu" value="<%=codigo_alu%>">
		<input type="hidden" name="codigo_sco" value="<%=codigo_sco%>">
		<input type="hidden" name="precio_sco" value="<%=precio_sco%>">
		<input type="hidden" name="moneda_sco" value="<%=moneda_sco%>">
		<input type="hidden" name="generamora_sco" value="<%=generamora_sco%>">
		<input type="hidden" name="fechaVencimiento_sco" value="<%=fechaVencimiento_sco%>">
		<input type="hidden" name="codigo_cco" value="<%=codigo_cco%>">
		<input type="hidden" name="nroamigos" value="<%=nroamigos%>">
   	
 
		<%
			'Consultar lista de acompañantes
			set rs = Server.CreateObject("ADODB.RecordSet")
			set rs = obj.consultar("consultarAcompañanteFiesta", "FO", "E",codigo_alu, codigo_sco)
	
		if rs.recordcount < nroamigos then  'nroamigos = Máximo dos acompañantes=2
		%>
	 
		<tr>
			 <td width="100%" colspan="2" class="etabla" style="text-align: left" height="13"><u>Datos de Inscripci&oacute;n</u></td>
		</tr>
 
		<tr>
			 <td width="100%" colspan="2" class="etabla" style="text-align: left" height="13">Ingrese datos de su acompañante, que servirá para controlar el ingreso 
                 al almuerzo.<br>(Recuerde que máximo podrá llevar dos (2) acompañantes.)</td>
		</tr>
 
	    <tr>
   			<td width="30%">
                <br />
                <br />
                Costo por acompañante:</td>
			<td width="70%"><br />
                <br /><h3><b><%=cstr(simbolo_moneda) + " " + cstr(formatNumber(precio_sco))%> </b></h3> </td>
		</tr>
 
		<tr>
			<td width="30%">Apellidos:</td>
			<td width="70%"><input type="text" name="txtApellidos" maxlength="50" size="50"></td>
		</tr>
 
		<tr>
			<td width="30%">Nombres:</td>
			<td width="70%"><input type="text" name="txtNombres"  maxlength="50" size="50"></td>
		</tr>
		<tr>
			<td colspan=2 align="right"> <input type="submit" value="Registrar"  name="smtGuardar" class="usatguardar"> 
		</tr>  
	
	 <% end if %>

	</table>
	
	<!--<table cellpadding="3" cellspacing="0" style="border-collapse: collapse; " bordercolor="#111111" width="100%" class="contornotabla">-->
	<table bordercolor="#DBEAF5" align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" width="100%">
		<tr>
				<td colspan="2"><b style="color: #0000FF">Lista de acompañantes registrados (máximo dos personas):<br />
                    </b></td>
		</tr>
   
		<tr>
				<td width="80%" class="etabla" align="left">Acompañante</td>
				<td width="20%" class="etabla">Costo(<%=cstr(simbolo_moneda)%>)</td>
		</tr>
   
		<%
		total=0
		do while not rs.eof
				total=total  + cdbl(rs("costo_Afi"))
		%>
				<tr>
					<td><%=rs("Acompañante")%>&nbsp; </td>
					<td align="right"><%=formatNumber(rs("costo_Afi"))%>&nbsp;</td>
				</tr>
		<%
			  rs.movenext
		loop
		
		rs.close
		set rs=nothing
		%>
   
   		<tr>
				<td align="right"><b>Total:</b></td>
				<td align="right"><b><%=formatNumber(total)%></b>&nbsp;</td>
		</tr>
   
		<tr>
				<td colspan=2 align="right"> <input OnClick="location.href='inscripcioneventoSEMANING.asp'" type="button" value="Cerrar" name="cmdCancelar" class="usatsalir">
		</tr>  
	  </table>
		
		

</form>
</BODY>
</HTML>
<%
else 
	rs.close
	set rs=nothing
%>
 <br> 
    <br>
    <br>
    <br>
    
    <table align="center">
    <tr>
    <td><b>Mensaje del sistema:</b></td> 
    </tr>
    <tr>
        <td align="left">Usted no se ha inscrito en la Semana de Ingeniería 2008.<br>Sólo podran registrar acompañantes los inscritos en la semana.</td>
    </tr>
	<tr>
    <td align="center"><b><a href='inscripcioneventoSEMANING.asp'>Volver</a></b></td> 
    </tr>
    </table> 

<% end if


obj.CerrarConexion
set obj = nothing

%>