<!--#include file="../../../NoCache.asp"-->
<%

codigo_alu= session("codigo_alu")

'Estos Datos hay que actualizar
   'Llenar datos del combo (se quito combo y se puso hidden)
    codigo_cac="31" '2008-II
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
	            alert("Por favor, seleccione el servicio que comprará.");
    		    form.codigo_sco.focus();
    		    return(false);
	    }
	    else 
	    {
	        if (form.cantidad.value=="0" || form.cantidad.value=="" || form.cantidad.value<0 ) { 
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
    .Estilo1 {	color: #FF6600;
	font-weight: bold;
	font-size: 14pt;
}
    .Estilo2 {
	color: #FF6600;
	font-weight: bold;
}
    .Estilo4 {	color: #FF6600;
}
.Estilo3 {color: #000000}
.Estilo4 {	color: #FF6600;
	font-weight: bold;
	font-size: 11px;
}
    .Estilo6 {color: #000000; font-weight: bold; }
    </style>
</HEAD>
<BODY>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="60%" class="usattitulo"><span class="Estilo1">SEMANA NARANJA</span></td>
  </tr>
</table>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td width="75%" valign="top"><br>
      <!--#include file="../../fradatos.asp"-->
      <br>
      <form  name="frminscripcion" method="post" action="grabarInscripcionEventoGeneral_v2.asp" onSubmit="return validar(this)">
        <table cellpadding="3" cellspacing="0" style="border-collapse: collapse; " bordercolor="#111111" width="100%" class="contornotabla">
          <input type="hidden" name="codigo_alu" value="<%=codigo_alu%>">
          <input type="hidden" name="codigo_cac" value="<%=codigo_cac%>">
          <input type="hidden" name="cboPartes" value="1">
          <tr bgcolor="#FFFF99" class="bordeinf">
            <td width="100%" height="13" colspan="2" class="etabla"  style="text-align: left">Datos de Inscripci&oacute;n</td>
          </tr>
          <tr>
            <td width="36%">Servicio:</td>
            <td width="69%" class="usatTitulousat"><select name="codigo_sco" id="codigo_sco" >
                <option value="0">---Seleccione el servicio que desee--- </option>
                <option value="981"> 6 CONFERENCIAS Y FIESTA DE GALA (S/. 40.00)</option>
                <option value="983"> FIESTA NARANJA Y CENA (S/. 30.00) </option>
                <option value="982"> FIESTA NARANJA (S/. 15.00) </option>
              </Select>
            </td>
          </tr>
          <tr>
            <td width="36%" class="azul">Cantidad de pases que desesa:---&gt;</td>
            <td width="69%"><input type=text name="cantidad" value="" 
                    onKeyPress="validarSoloNumero()" maxlength=2 style="width: 36px">
                <span class="Estilo2">(cantidad de pases. Esto se multiplicará por el precio)</span></td>
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
set rs = obj.consultar("consultarDeudaEvento", "FO", "ADM", "E",codigo_alu, codigo_cac)
obj.CerrarConexion
set obj = nothing
%>
      <table border="1" cellpadding="0" cellspacing="0" style="border"   bordercolor="#111111" width="100%" align="center">
        <tr>
          <td colspan=7 align="center">Detalle de compra realizada:</td>
        </tr>
        <tr class="etabla">
          <td width="15%" height="20" align="center" >Fecha Registro</td>
          <td width="25%" height="20" align="center" >Servicio</td>
          <td width="10%" height="20" align="center" >Cantidad</td>
          <td width="10%" height="20" align="center" >Precio (S/.)</td>
          <td width="10%" height="20" align="center" >Cargo (S/.)</td>
          <td width="10%" height="20" align="center" >Abono (S/.)</td>
          <td width="10%" height="20" align="center" >Saldo (S/.)</td>
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
    <td width="20%" align="center" valign="top"><table width="100%" border="0" cellpadding="2" cellspacing="2" class="contornotabla_azul">
        <tr>
          <td><p align="justify" class="Estilo4"><strong>Conferencias Emrpesariales y Fiesta Naranja </strong><br>
                  <span class="Estilo3">Del 10 al 14 de Noviembre: <br>
                    06 conferencias, Concurso de investigaci&oacute;n, Ma&ntilde;ana deportiva, Fiesta de confraternidad, Gymkana.<br>
                    Costo: S/. 40.00 <a href="conferencias.jpg" target="_blank">Ver m&aacute;s...</a> <br>
                </span></p>
            <p><span class="Estilo4"><strong>Fiesta de Confraternidad </strong><br>
                    <span class="Estilo3">Viernes 14 de Noviembre<br>
                      En el Jockey Club de Cliclayo. 09.00pm<br>
                      Concursos, orquesta,regalos, sorteos
                      .<br>
                            <strong>Fiesta: S/. 15.00</strong></span></span><br>
            <span class="Estilo4"><span class="Estilo3"><strong>Fiesta y Cena: S/. 30.00</strong></span></span><span class="Estilo6"> <a href="fiesta naranja.jpg" target="_blank">Ver m&aacute;s...</a></span></p>
            <p align="center"><span class="Estilo4">Acercarse luego al  MODULO NARAJA en cafeter&iacute;a a recabar sus entradas y pases a las conferencias </span></p></td>
        </tr>
      </table>      <p>&nbsp;</p></td>
  </tr>
</table>
</BODY>
</HTML>