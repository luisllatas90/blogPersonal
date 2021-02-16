<!--#include file="../../../NoCache.asp"-->
<%

if session("codigo_Usu") <>"" then

codigo_per= session("codigo_Usu")


'Estos Datos hay que actualizar
   
codigo_cac="33" '2009-I
    
'Configurar los SP: consultarExistenciaDeuda, consultarDeudaEvento
    
'---------------------------------------------------------


cantidad = ""
msg = request.querystring("msg")




%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link href="../../../private/estilo.css" rel="stylesheet" type="text/css">
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
    <td width="60%" class="usattitulo"><b>SEMANA DE FACULTAD DE INGENIERÍA: DEL 04 AL 08 
        DE JUNIO</b></td>
  </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="75%"><br>
           <br>
      <form  name="frminscripcion" method="post" action="grabarInscripcionEventoGeneral_v2.asp" onSubmit="return validar(this)">
        <table cellpadding="3" cellspacing="0" style="border-collapse: collapse; " bordercolor="#111111" width="100%" class="contornotabla">
          <input type="hidden" name="codigo_per" value="<%=codigo_per%>">
          <input type="hidden" name="codigo_cac" value="<%=codigo_cac%>">
          <input type="hidden" name="cboPartes" value="1">
          <tr>
            <td width="100%" colspan="2" class="etabla" style="text-align: left" height="13">Datos de Inscripci&oacute;n</td>
          </tr>
          <tr>
            <td width="36%">Servicio:</td>
            <td width="69%" class="usatTitulousat"><select name="codigo_sco" id="codigo_sco" >
                <option value="0">---Seleccione el servicio que desee--- </option>
                <option value="416"> INSCRIPCION SEMANA (S/. 25.00)</option>
                <option value="418"> ACOMPAÑANTE PARA SEMANA (S/. 25.00) </option>-->
                <!--<option value="966"> DERECHO POLO SEMANA (S/. 20.00)</option>-->
              </Select>
                <br />
                        </td>
          </tr>
          <tr>
            <td width="36%" class="azul">Cantidad de pases que deseas:</td>
            <td width="69%"><input type=text name="cantidad" value="1" 
                    onKeyPress="validarSoloNumero()" maxlength=1 style="width: 31px">
                (Cantidad de pases. Esto se multiplicará por el precio)</td>
          </tr>
          <td width="36%">&nbsp;</td>
        <td width="69%" class="rojo"><%=msg%> <br />
            <br />
            <input type="submit" value="Registrar"  name="smtGuardar" class="usatguardar">
            <!--<input onClick="top.window.close()" type="button" value="Cancelar" name="cmdCancelar" class="usatsalir">-->
            <input onClick="location.href='../../listaaplicaciones.asp'" type="button" value="Cerrar" name="cmdCancelar" class="usatsalir">
            
            
        </td>
          </tr>
        </table>
      </form>
<%

set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion

set rs = Server.CreateObject("ADODB.RecordSet")
set rs = obj.consultar("consultarDeudaEvento", "FO", "ING", "P",codigo_per, codigo_cac)
obj.CerrarConexion
set obj = nothing
%>
      <table border="1" cellpadding="0" cellspacing="0" style="border"   bordercolor="#111111" width="100%" align="center">
        <tr>
          <td colspan=7 align="center">Detalle de compra realizada:</td>
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
        <% Do while not rs.eof%>
        <tr>
          <td align="center" height="20"><%=rs("fecha")%></td>
          <td align="center" height="20"><%=rs("descripcion_sco")%></td>
          <td align="center" height="20"><%=cdbl(rs("montoTotal_Deu"))/cdbl(rs("precio_Sco"))%></td>
          <td align="center" height="20"><%=rs("precio_sco")%></td>
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
    <td width="20%" align="center" valign="middle"><table width="98%" border="0" cellpadding="2" cellspacing="2" class="contornotabla_azul">
      <tr>
        <td class="azul"><p>Podrás acceder a:</p>
                            <p>- Conferencias</p>
                            <p>- Concursos</p>
                            <p>- Exposiciones</p>
                            <p>- Almuerzo de confraternidad</p>
                            <p><em>&nbsp;</em></p></td>
      </tr>
    </table></td>
  </tr>
</table>
<p class="azul">
        Ver Programación: <a href="deportes.htm" style="font-size: 12px; font-weight: bold;" target="_blank">Clic aquí</a></p>
    <p style="height: 16px; font-weight: bold; font-size: 12px;">
        &nbsp;
    
</BODY>
</HTML>
<%
    else
        response.write("Debe iniciar sesión por el campus virtual!")
    
    end if

 %>