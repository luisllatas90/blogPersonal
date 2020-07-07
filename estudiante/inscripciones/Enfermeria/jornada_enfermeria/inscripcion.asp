<html>
<head>
<title>Programa de Especialiaci&oacute;n en Finanzas -  Instituto Mayorga</title>
<META HTTP-EQUIV="Cache-Control" CONTENT ="no-cache"> 
<!--Fireworks MX 2004 Dreamweaver MX 2004 target.  Created Wed Apr 05 06:11:09 GMT-0500 2006-->
<script language="JavaScript">

</script>
<style type="text/css">
<!--
body {
	background-image: url('files/fondo.gif');
	font-family:Verdana, Arial, Helvetica, sans-serif;
}
a:link {
	color: #990000;
	text-decoration: none;
}
a:visited {
	text-decoration: none;
	color: #990000;
}
a:hover {
	text-decoration: underline;
	color: #990000;
}
a:active {
	text-decoration: none;
	color: #000000;
}
body, td, th {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 12px;
}
.Estilo1 {
	color:#000000;
	font-weight: bold;
}
.Estilo6 {color: #990000}
.Estilo8 {font-size: 10px; color: #FFFFFF;}
.Estilo19 {font-size: 18}
-->

</style>
<script>
function validar(){
var cadena =""
if (document.all.txtnombres.value==""){
	if (cadena==""){
		cadena=cadena+"Nombres"
	}
	else{
		cadena=cadena + ", Nombres"
	}
}

if (document.all.txtapellidos.value==""){
	if (cadena==""){
		cadena=cadena+"Apellidos"
	}
	else{
		cadena=cadena + ", Apellidos"
	}
}

if (document.all.txtdireccion.value==""){
	if (cadena==""){
		cadena=cadena+"Dirección"
	}
	else{
		cadena=cadena + ", Dirección"
	}
}

if (document.all.txtdistritodir.value==""){
	if (cadena==""){
		cadena=cadena+"Distrito"
	}
	else{
		cadena=cadena + ", Distrito"
	}
}

if (document.all.txtemail.value==""){
	if (cadena==""){
		cadena=cadena+"E-mail"
	}
	else{
		cadena=cadena + ", E-mail"
	}
}

if (validarEmail(document.all.txtemail.value)==false){

	
	if (cadena==""){
		cadena=cadena+"E-mail"
	}
	else{
		cadena=cadena + ", E-mail"
	}
}

if (document.all.txtdocumento.value==""){
	if (cadena==""){
		cadena=cadena+"Doc. de identidad"
	}
	else{
		cadena=cadena + ", Doc. de identidad"
	}
}

if (document.all.cbodia.value==0){
	if (cadena==""){
		cadena=cadena+"Día"
	}
	else{
		cadena=cadena + ", Día"
	}
}


if (document.all.cbomes.value==0){
	if (cadena==""){
		cadena=cadena+"Mes"
	}
	else{
		cadena=cadena + ", Mes"
	}
}

if (document.all.cboanio.value==0){
	if (cadena==""){
		cadena=cadena+"Año"
	}
	else{
		cadena=cadena + ", Año"
	}
}

if (document.all.optsexo1.checked==false && document.all.optsexo2.checked==false){
	if (cadena==""){
		cadena=cadena+"Sexo"
	}
	else{
		cadena=cadena + ", Sexo"
	}
}

if ((document.all.txtestudios1.value=="") || (document.all.txtinstitucion1.value=="") || (document.all.txtdesde1.value=="")){
	if (cadena==""){
		cadena=cadena+"Estudios Superiores"
	}
	else{
		cadena=cadena + ", Estudios Superiores"
	}
}
/*---Ultimas validaciones---*/
/*if (document.all.optfactura.checked == true ){
	if (document.all.txtfacturara.value=="" ||  document.all.txtruc.value==""  ||  document.all.txtdireccionfact.value=="") 
	{
		if (cadena==""){
			cadena=cadena + "Razon, Ruc, Dirección de la empresa de Facturación"
		}
		else{
			cadena=cadena + ", Razon, Ruc, Dirección de la empresa de Facturación"
		}
	}
}

if (document.all.credito.checked==true)
{	if (document.all.txtnrocuotas.value=="")
	{	if (cadena=="")
		{	cadena=cadena + "Nro Cuotas"
		}else
		{	cadena=cadena + ", Nro Cuotas"
		}
	}
}

if ((document.all.carta.checked ==false) && (document.all.diario.checked==false) &&
(document.all.web.checked==false) && (document.all.email.checked==false) && (document.all.amigos.checked==false))
{	if (cadena==""){
		cadena=cadena + "Referencias"
	}
	else{
		cadena=cadena + ", Referencias"
	}
}
*/
if (cadena==""){
//	alert ("OK")
	form1.submit()

//	return true
}
else{
	alert ("Debe ingresar los campos obligatorios: "+ cadena + " adecuadamente.")
//	return false
}
}

function validarnumero()
{
	if (event.keyCode < 45 || event.keyCode > 57) 
		{event.returnValue = false}
}

function validarLetra()
{
	if (event.keyCode < 45 || event.keyCode > 57) 
		{event.returnValue = true}
	else
			{event.returnValue = false}
}

 function validarEmail(valor) {
  if (/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(valor)){
 //  alert("La dirección de email " + valor    + " es correcta.") 
   return (true)
  } else {
 //  alert("La dirección de email es incorrecta.");
   return (false);
  }
 }
 
</script>

</head>

<body bgcolor="#ffffff"  topmargin="2">
<form name="form1" method="post" action="registra.asp">
  <table border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
   <td align="center" bgcolor="#FFFBF0"><!--<img src="../Evento III Jornada de Enfermeria/jpg/Principal_1.jpg" width="916" height="390" >--></td>
    </tr>
    <tr bgcolor="#999999">
      <td height="25" valign="middle" align="center"><h3 >Ficha de Inscripci&oacute;n </h3></td>
    </tr>
    <tr>
      <td bgcolor="#EFEFEF"><table width="95%" border="0" align="center" cellpadding="1" cellspacing="0" id="AutoNumber1" style="BORDER-COLLAPSE: collapse">
        <tr>
          <td colspan="8" align="center" height="0%" bgcolor="#FFFFFF"></td>
        </tr>
        
        <tr>
          <td height="1%" colspan="8" align="left">&nbsp;</td>
        </tr>
        <tr>
          <td  colspan="8" align="left"><p class="Estilo1"> Infomaci&oacute;n personal</p></td>
        </tr>
        <tr>
          <td align="left"><span class="Estilo6">*</span> Nombres y Apellidos</td>
          <td >:</td>
          <td  align="left" valign="top"><p>
              <input name="txtnombres" id="txtnombres" size="20" onKeyPress="validarLetra()">
              <font size="1">Nombres</font> </p></td>
          <td colspan="5" align="left" valign="top"><input name="txtapellidos" id="txtapellidos" size="40" onKeyPress="validarLetra()"  >
              <br>
              <font size="1">Apellidos</font></td>
        </tr>
        <tr>
          <td align="left"><span class="Estilo6">*</span> Direcci&oacute;n </td>
          <td align="left">:</td>
          <td colspan="3" align="left"><input name="txtdireccion" id="txtdireccion" size="35" ></td>
          <td align="left"><span class="Estilo6">*</span>Distrito</td>
          <td align="left">:</td>
          <td  align="left"><input name="txtdistritodir" id="txtdistritodir" size="35" onKeyPress="validarLetra()"></td>
        </tr>
        <tr>
          <td align="left"><p>Tel&eacute;fono de Domicilio </p></td>
          <td align="left">:</td>
          <td align="left"><input name="txttelefono" id="txttelefono" size="15" onKeyPress="validarnumero()" ></td>
          <td width="83" align="left">Tel&eacute;fono Celular: </td>
          <td align="left"><input name="txtcelular" id="txtcelular" size="15" onKeyPress="validarnumero()" ></td>
          <td align="left"><span class="Estilo6">*</span> E- mail</td>
          <td align="left">:</td>
          <td align="left"><input name="txtemail" id="txtemail" size="35" onlostfocus="validarEmail()" ></td>
        </tr>
        <tr>
          <td align="left"><span class="Estilo6">*</span> Fecha de nacimiento </td>
          <td align="left">:</td>
          <td align="left" ><label>
            <select name="cbodia" id="cbodia">
				<option value="0">Dia</option>
				<%for i=1 to 30
					if (i<10) then%>
						<option value=<%=i%>><%response.Write("0"& i)%></option>	
				<%	else	%>
						<option value=<%=i%>><%=i%></option>
				<%  end if
				  next %>
            </select>
            </label>
              <label>
              <select name="cbomes" id="cbomes">
  				<option value="0">Mes</option>
			  	<%for i=1 to 12
					if (i<10) then%>
						<option value=<%=i%>><%response.Write("0"& i)%></option>	
				<%	else	%>
						<option value=<%=i%>><%=i%></option>
				<%  end if
				  next %>
              </select>
              </label>
              <label>
              <select name="cboanio" id="cboanio">
  				<option value="0">Año</option>
			  	<%for i=year(date) to 1900 step -1%>
				<option value=<%=i%>><%=i%></option>
				<%next %>
              </select>
            </label></td>
          <td align="left"><span class="Estilo6">*</span> Sexo : </td>
          <td align="left"><label>
            <input name="optsexo" id="optsexo1" type="radio"  value="F">
            F</label>
              <label>
              <input name="optsexo" id="optsexo2" type="radio" value="M">
                M</label></td>
          <td width="110" align="left"><span class="Estilo6">*</span> Doc. de Identidad</td>
          <td align="left">:</td>
          <td align="left"><label>
            <input name="txtdocumento" type="text" id="txtdocumento" width="100px"  onKeyPress="validarnumero()">
          </label></td>
        </tr>
        <tr>
          <td height="1%" colspan="8" align="left">&nbsp;</td>
        </tr>
        <tr>
          <td height="1%" colspan="8" align="left"><span class="Estilo1">Formaci&oacute;n Acad&eacute;mica - Profesional </span></td>
        </tr>
        <tr >
          <td height="4%" colspan="8" align="left"><table width="100%" border="1" cellpadding="0" cellspacing="0">
              <tr>
                <td bgcolor="#CCCCCC">Estudios Superiores / T&eacute;cnicos </td>
                <td bgcolor="#CCCCCC">Instituci&oacute;n</td>
                <td bgcolor="#CCCCCC">Desde - Hasta </td>
                <td bgcolor="#CCCCCC">T&iacute;tulo o Grado Obtenido </td>
              </tr>
              <tr>
                <td align="center"><label>
                  <span class="Estilo6">*</span>
                  <input name="txtestudios1" type="text" id="txtestudios1" size="37">
                  </label></td>
                <td><div align="center">
                  <span class="Estilo6">*</span>
                  <input name="txtinstitucion1" type="text" id="txtinstitucion1" size="30">
                </div></td>
                <td><div align="center">
                  <span class="Estilo6">*</span>
                  <input name="txtdesde1" type="text" id="txtdesde1" size="18">
                </div></td>
                <td><div align="center">
                  <input name="txttitulo1" type="text" id="txttitulo1" size="35">
                </div></td>
              </tr>
              <tr>
                <td><div align="center">
                   &nbsp;&nbsp;<input name="txtestudios2" type="text" id="txtestudios2" size="37">
                </div></td>
                <td><div align="center">
                  &nbsp;&nbsp;<input name="txtinstitucion2" type="text" id="txtinstitucion2" size="30">
                </div></td>
                <td><div align="center">
                  &nbsp;&nbsp;<input name="txtdesde2" type="text" id="txtdesde2" size="18">
                </div></td>
                <td><div align="center">
                  <input name="txttitulo2" type="text" id="txttitulo2" size="35">
                </div></td>
              </tr>
          </table></td>
        </tr>
        <tr >
          <td height="1%" colspan="8" align="left">&nbsp;</td>
        </tr>
        <tr >
          <td height="1%" colspan="8" align="left"><span class="Estilo1"> Informaci&oacute;n Laboral </span></td>
        </tr>
        <tr>
          <td height="4%" width="150" align="left">Nombre de la Instituci&oacute;n </td>
          <td align="left">:</td>
          <td colspan="3" align="left"><input name="txtcentrolaboral" size="35" onKeyPress="validarLetra()" ></td>
          <td align="left">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left"></td>
        </tr>
        <tr>
          <td height="2%" align="left">Cargo</td>
          <td align="left">:</td>
          <td colspan="3" align="left"><label>
            <input name="txtcargo" type="text" id="txtcargo" size="35" onKeyPress="validarLetra()">
          </label></td>
          <td align="left">Tel&eacute;fono</td>
          <td align="left">:</td>
          <td align="left"><input name="txttelefonotrab" id="txttelefonotrab" width="80px" onKeyPress="validarnumero()"></td>
        </tr>
        <tr>
          <td height="2%" align="left">Direcci&oacute;n</td>
          <td align="left">:</td>
          <td colspan="3" align="left"><input name="txtDireccionCentroLab" id="txtDireccionCentroLab" size="35" ></td>
          <td align="left">E-mail</td>
          <td align="left">:</td>
          <td align="left"><input name="txtmailreporta" id="txtmailreporta" size="32"  onlostfocus="validarEmail()"></td>
        </tr>
       
        
        <tr>
          <td height="1%" colspan="5" align="left">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left">&nbsp;</td>
          <td align="left">&nbsp;</td>
        </tr>

        <tr>
          <td height="2%" colspan="8" align="left" class="Estilo1">Forma de pago </td>
          </tr>
        <tr>
          <td height="2%" align="left"><span class="Estilo6">*</span> Forma de Pago </td>
          <td align="left">:</td>
          <td colspan="6" align="left"><label>
            <input name="optpago" type="radio" id="optpago" value="contado" checked>
            Contado
            <input name="optpago" type="radio" id="optpago" value="credito">
            Cr&eacute;dito
            </label></td>
        </tr>
        <tr>
          <td height="2%" align="left"><span class="Estilo6">*</span> Opciones </td>
          <td align="left">:</td>
          <td colspan="6" align="left"><p>
              <label>
              <input name="PreJornada" type="checkbox" value="1">
              PreJornada</label>
              <label>
              <input name="Jornada" type="checkbox" value="2">
              Jornada</label>
          </p></td>
        </tr>
        <tr>
          <td height="1%" colspan="8" align="left"><div align="right">Fecha de Inscripci&oacute;n <%=date()%></div>
           </td>
        </tr>
        <tr>
          <td height="2%" colspan="8" align="center"><input type="button" value="Pre Inscribir" name="B1"  onClick="validar()">
            &nbsp;&nbsp;
            <input type="reset" value="Restablecer" name="B2"></td>
        </tr>
        <tr>
          <td  colspan="8" align="left">&nbsp;</td>
        </tr>
        
      </table></td>
    </tr>
    <tr>
      <td bgcolor="#98000F"><span class="Estilo8"><strong>Informes e Inscripciones</strong><br>
- Universidad Cat&oacute;lica Santo Toribio de Mogrovejo. Facultad de Ciencias de la Salud<br>
Av. Panamericana Norte 855 <br>
      </span></td>
    </tr>
  </table>
  <p>&nbsp;</p>
</form> 
</body>
</html>