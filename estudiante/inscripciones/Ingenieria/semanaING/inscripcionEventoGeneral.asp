<!--#include file="../../../../NoCache.asp"-->

<%

if session("codigo_alu") <>"" then

codigo_alu= session("codigo_alu")

'Estos Datos hay que actualizar

    codigo_cac="36" '2010-I



cantidad = ""
msg = request.querystring("msg")




%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<script type="text/javascript" language="JavaScript">


function validarCantidad (){

	if (document.frminscripcion.codigo_sco.value == 0 || document.frminscripcion.codigo_sco.value == 518 || document.frminscripcion.codigo_sco.value == 519 )
	{
		document.frminscripcion.cantidad.value ="";
		//document.frminscripcion.cantidad.disabled=false;
		document.frminscripcion.cantidad.readOnly=false;
			

		
	}
	else
	{

		if (document.frminscripcion.codigo_sco.value == "540")
		{
			document.frminscripcion.cantidad.value ="1";
			//document.frminscripcion.cantidad.disabled=true;
			document.frminscripcion.cantidad.readOnly=true;
		
		}
	
	}	
}




function validar(form)
{
	//Valida
	    if (form.codigo_sco.value=="0") { 
	            alert("Por favor, seleccione el servicio que comprará.");
    		    form.codigo_sco.focus();
    		    return(false);
	    }
	    else 
	    {
	        if (form.cantidad.value == "0" || form.cantidad.value == "" || form.cantidad.value < 0 || form.cantidad.value == "00") { 
		        alert("Por favor, indique la cantidad que comprará.");
		        form.cantidad.focus(); 
		        return(false);
	        }
	        
	        else
	        {
	        	    if (confirm("¿Está seguro de registrar esta operación?\n" + "Recuerde que esto le generará una deuda en Caja-USAT.")==true){
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
			{event.returnValue = false;}
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
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="60%" class="usattitulo"><b>SEMANA DE INGENIERÍA 2010</b></td>
  </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="75%"><br>
      <!--#include file="../../../fradatos.asp"-->
      <br>
      <form  name="frminscripcion" method="post" action="grabarInscripcionEventoGeneral.asp" onSubmit="return validar(this)">
        <table cellpadding="3" cellspacing="0" style="border-collapse: collapse; " bordercolor="#111111" width="100%" class="contornotabla">
          <input type="hidden" name="codigo_alu" value="<%=codigo_alu%>">
          <input type="hidden" name="codigo_cac" value="<%=codigo_cac%>">
          <input type="hidden" name="cboPartes" value="1">
          <tr>
            <td width="100%" colspan="2" class="etabla" style="text-align: left" height="13">Datos de Inscripci&oacute;n</td>
          </tr>
          <tr>
            <td width="36%">Servicio:</td>
            <td width="69%" class="usatTitulousat"><select name="codigo_sco" id="codigo_sco" onChange="validarCantidad()">
                <option value="0">---Seleccione el servicio que desee--- </option>
                <option value="630"> INSCRIPCION (S/. 25.00  : Pagar junto con pensión de Julio)</option>
              
              </Select>
                <br />
                        </td>
          </tr>
          <tr>
            <td width="36%" class="azul">Cantidad que desesa:</td>
            <td width="69%"><input type=text name="cantidad" value="" 
                    onKeyPress="validarSoloNumero()" maxlength=2 style="width: 31px">
                (Cantidad de pases. Esto se multiplicará por el precio)</td>
          </tr>
          <td width="36%">&nbsp;</td>
        <td width="69%" class="rojo"><%=msg%> <br />
            <br />
            <input type="submit" value="Registrar"  name="smtGuardar" class="usatguardar">
            <input OnClick="location.href='about:blank'" type="button" value="Cancelar" name="cmdCancelar" class="usatsalir">
        </td>
          </tr>
        </table>
      </form>
      <%

set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion

set rs = Server.CreateObject("ADODB.RecordSet")
set rs = obj.consultar("consultarDeudaEvento", "FO", "ING", "E",codigo_alu, codigo_cac)
obj.CerrarConexion
set obj = nothing
%>
      <table border="1" cellpadding="0" cellspacing="0" style="border"   bordercolor="#111111" width="100%" align="center">
        <tr>
          <td colspan=8 align="center">Detalle de compra realizada:</td>
        </tr>
        <tr>
          <td width="15%" class="etabla" align="center" height="20">Fecha Registro</td>
          <td width="25%" class="etabla" align="left" height="20">Servicio</td>
          <td width="10%" class="etabla" align="center" height="20">Cantidad</td>
          <td width="10%" class="etabla" align="center" height="20">Precio (S/.)</td>
          <td width="10%" class="etabla" align="center" height="20">Cargo (S/.)</td>
          <td width="10%" class="etabla" align="center" height="20">Abono (S/.)</td>
          <td width="10%" class="etabla" align="center" height="20">Saldo (S/.)</td>
        </tr>
        <% Do while not rs.eof%>
        <tr>
          <td align="center" height="20"><%=rs("fecha")%></td>
          <td align="left" height="20"><%=rs("descripcion_sco")%></td>
          <td align="center" height="20"><%=cdbl(rs("montoTotal_Deu"))/25.0%></td>
          <td align="center" height="20">25.00</td>
          <td align="center" height="20"><%=rs("montoTotal_Deu")%></td>
          <td align="center" height="20"><%=cdbl(rs("montoTotal_Deu")) - cdbl(rs("saldo_deu"))%></td>
          <td align="center" height="20"><%=rs("saldo_Deu")%></td>
        </tr>
        <%
    rs.movenext

loop



%>
 </table>
 </td>


    <td width="5%">&nbsp;</td>
    <td width="20%" align="center" valign="middle"><table width="98%" border="0" cellpadding="2" cellspacing="2" class="contornotabla_azul">
      <tr>
        <td><p><strong><em>Nota:</em></strong> 
	- Puede inscribirse hasta el mismo Viernes 11
	- Para acudir deberás llevar tu carnét
	
	</p>
      </tr>
    </table></td>
</tr>


</table>

<!--
<br><br>
<table width="100%">

<tr>
<td class="usatAmarillo" width="100%" colspan 5>Afiche:</td>
</tr>
<tr><td><a href=afiche.jpg target="_blank">Ver afiche publicitario</a></td></tr>



</table>
-->

</BODY>
</HTML>

<% 
ELSE

response.write ("Debe iniciar una sesión!.")

End IF%>
