<!--#include file="../../../../NoCache.asp"-->
<%

codigo_alu= session("codigo_alu")

'Estos Datos hay que actualizar
   
    codigo_cac="34" '2009-II
    
    'Configurar los SP: consultarExistenciaDeuda, consultarDeudaEvento
    
'---------------------------------------------------------


cantidad = ""
msg = request.querystring("msg")




%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
<script type="text/javascript" language="JavaScript">

function validar(form)
{
	//Valida
	    if (form.codigo_sco.value=="0") { 
	            alert("Por favor, seleccione el servicio que adquirirá.");
    		    form.codigo_sco.focus();
    		    return(false);
	    }
	    else 
	    {
	        if (form.cantidad.value=="0" || form.cantidad.value=="" || form.cantidad.value<0 ) { 
		        alert("Por favor, indique la cantidad que adquirirá.");
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
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="60%" class="usattitulo"><b>PREJORNADA Y III JORNADA INTERNACIONAL DE INVESTIGACION EN CIENCIAS DE ENFERMERIA</b></td>
  </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="75%"><br>
      <!--#include file="../../../fradatos.asp"-->
      <br>
      <form  name="frminscripcion" method="post" action="grabarInscripcionEventoGeneral_v2.asp" onSubmit="return validar(this)">
        <table cellpadding="3" cellspacing="0" style="border-collapse: collapse; " bordercolor="#111111" width="100%" class="contornotabla">
          <input type="hidden" name="codigo_alu" value="<%=codigo_alu%>">
          <input type="hidden" name="codigo_cac" value="<%=codigo_cac%>">
          <input type="hidden" name="cboPartes" value="2">
          <input type=hidden name="cantidad" value="1" 
                
          <tr>
            <td width="100%" colspan="2" class="etabla" style="text-align: left" height="13">Datos de Inscripci&oacute;n</td>
          </tr>
          <tr>
            <td width="36%">Servicio:</td>
            <td width="69%" class="usatTitulousat"><select name="codigo_sco" id="codigo_sco" >
                <option value="0">---Seleccione el servicio que desee--- </option>
                <option value="71"> PRE JORNADA INTERN ENFERMERIA 2009 (S/. 120.00)</option>
                <option value="70"> III JORNADA INTERN ENFERMERIA 2009 (S/.  80.00) </option>
                <!--<option value="966"> DERECHO POLO SEMANA (S/. 20.00)</option>-->
              </Select>
                <br />
           </td>
          </tr>
          
          <td width="36%">&nbsp;</td>
        <td width="69%" class="rojo"><%=msg%> 
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
set rs = obj.consultar("consultarDeudaEvento", "FO", "JOR", "E",codigo_alu, codigo_cac)
obj.CerrarConexion
set obj = nothing
%>
      <table border="1" cellpadding="0" cellspacing="0" style="border"   bordercolor="#111111" width="100%" align="center">
        <tr>
          <td colspan=7 align="center">Detalle de Servicio registrado:</td>
        </tr>
        <tr>
          <td width="15%" class="etabla" align="center" height="20">Fecha Registro</td>
          <td width="25%" class="etabla" align="center" height="20">Servicio</td>
          <td width="10%" class="etabla" align="center" height="20">Cantidad</td>
          <td width="10%" class="etabla" align="center" height="20">Precio (S/.)</td>
          <td width="10%" class="etabla" align="center" height="20">Cargo (S/.)</td>
          <td width="10%" class="etabla" align="center" height="20">Abono (S/.)</td>
          <td width="10%" class="etabla" align="center" height="20">Saldo (S/.)</td>
        </tr>
        <% Do while not rs.eof
          
        
        %>
        <tr>
          <td align="center" height="20"><%=rs("fecha")%></td>
          <td align="center" height="20"><%=rs("descripcion_sco")%></td>
          <td align="center" height="20">1</td>
          <td align="center" height="20"><%=rs("montoTotal_Deu")%></td>
          <td align="center" height="20"><%=rs("montoTotal_Deu")%></td>
          <td align="center" height="20"><%=cdbl(rs("montoTotal_Deu")) - cdbl(rs("saldo_deu"))%></td>
          <td align="center" height="20"><%=rs("saldo_Deu")%></td>
        </tr>
        <%
    rs.movenext

loop



%>
      </table></td>
    <td width="5%">&nbsp;</td>
    <td width="20%" align="center" valign="middle">&nbsp;</td>
  </tr>
</table>
</BODY>
</HTML>