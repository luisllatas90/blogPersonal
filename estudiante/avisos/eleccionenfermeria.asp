<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Votaci&oacute;n Enfermer&iacute;a</title>
<style type="text/css">
<!--
body,td,th {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 12px;
}
.Estilo3 {color: #003366; font-weight: bold; font-size: 15px; }
.Estilo4 {color: #003366}
.Estilo5 {color: #993300}
.Estilo6 {color: #993300; font-weight: bold; font-size: 15px; }
.Estilo7 {
	font-size: 16px;
	font-weight: bold;
	color: #009900;
}
-->
.Estilo8{
color:#FF0000; font-size:24px}
</style></head>

<body>

<% 


if (Request.QueryString("guardar")="SI") then

if Request.form("radiobutton")="" then
		Response.redirect("eleccionenfermeria.asp?votar=SI")
else
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			Obj.AbrirConexion
				votacion=obj.Ejecutar("VOT_VotacionEnfermeria",true,"VO",session("codigo_alu"),Request.form("radiobutton"),"")
			Obj.CerrarConexion
		Set obj=nothing
		
		response.redirect "../principal.asp?pagina=" & session("enlace_apl")
end if		
else
%> 
<form id="form1" name="form1" method="post" action="eleccionenfermeria.asp?guardar=SI">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td align="center"><span class="Estilo7">ELECCIONES ENFERMERIA </span></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td><p align="justify"><strong>El  C&iacute;rculo de Estudiantes de Enfermer&iacute;a de la Universidad Cat&oacute;lica Santo Toribio  de Mogrovejo (CEENF-USAT), es una organizaci&oacute;n sin&nbsp; fines de lucro, de car&aacute;cter eminentemente  acad&eacute;mico, social, y cultural,&nbsp; de bien  com&uacute;n, ajeno a toda actividad pol&iacute;tica partidaria. Se constituye legalmente con la aprobaci&oacute;n dispuesta por el Concejo de  Facultad en el a&ntilde;o 2007 y ha sido creado con la finalidad de promover el  desarrollo integral de los estudiantes de enfermer&iacute;a: personal, acad&eacute;mico,  profesional y espiritual.</strong><br />
          <strong>El  CEENF-USAT est&aacute; constituido de la siguiente manera: </strong></p>
      <p align="justify"><strong>1) &Oacute;rgano de gobierno:  <br />
        a.-Asamblea general, b.-Consejo directivo. <br />
        2) Comit&eacute;s:<br /> 
      a.-Comit&eacute; Cient&iacute;fico,  b.-Comit&eacute; de &Eacute;tica y Desarrollo, c.- Comit&eacute; Proyecci&oacute;n Social, d.- Comit&eacute; de  Prensa y Propaganda.</strong></p>
      <p align="justify"><strong>        EL  CONSEJO DIRECTIVO, CEENF-USAT: depende directamente de la oficina de Asuntos  Estudiantiles de la Escuela de Enfermer&iacute;a, siendo asesor del CEENF-USAT el  profesor designado por Direcci&oacute;n de Escuela.</strong><br />
        <strong>Actualmente  el CONSEJO DIRECTIVO, esta terminando sus 2 a&ntilde;os de trabajo, por esta raz&oacute;n y  cumpliendo con el art&iacute;culo 5 del Estatuto del CEENF-USAT, es que se convoca a  elecciones.</strong><br />
        </p></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td><table width="80%" border="0" align="center" cellpadding="6" cellspacing="0">
      <tr>
        <td width="50%" bgcolor="#FFFFCC"><p class="Estilo4"><strong>LISTA N&deg; 1</strong><br />
              <strong>INTEGRANTES: </strong></p>
          <ul class="Estilo4">
            <li><strong>Mariana  D&iacute;az Valera&nbsp; (Presidenta)(7mo ciclo)</strong></li>
            <li><strong>Karen  Quispe (vicepresidenta)(6to ciclo)</strong></li>
            <li><strong>Jes&uacute;s&nbsp; Villacrez D&aacute;vila (secretario)(1er ciclo)</strong></li>
            <li><strong>Ver&oacute;nica  Ripalda P&eacute;rez (Tesorera)(7mo ciclo)</strong></li>
            <li><strong>Nery&nbsp; Elisa  Vilchez Mu&ntilde;oz (Vocal)(1er ciclo)</strong></li>
            <li><strong>Doris  Alarc&oacute;n Bautista (Vocal)(3er ciclo)</strong></li>
          </ul>          </td>
        <td width="50%" valign="top" bgcolor="#FFFFCC"><p class="Estilo5"><strong>LISTA N&ordm; 2: N&Uacute;CLEOS PARA EL DESARROLLO  DE ENFERMER&Iacute;A</strong><br />
            <strong>INTEGRANTES:</strong></p>
          <ul class="Estilo5">
            <li><strong>PRESIDENTA: Pinglo Neyra Gloria Mar&iacute;a&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (VII Ciclo) </strong></li>
            <li><strong>VICEPRESIDENTA: Gil Acedo Katerin&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (VII Ciclo) </strong></li>
            <li><strong>TESORERA: Gonzales Villegas  Diana&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (V Ciclo) </strong></li>
            <li><strong>SECRETARIA: Renter&iacute;a Velazco  Melissa&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (II Ciclo) </strong></li>
            <li><strong>VOCAL 1: Salda&ntilde;a Oca&ntilde;a Iris&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (IV Ciclo)</strong> </li>
            <li><strong>VOCAL 2: Zapata Guerrero  Claudia&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; (I ciclo)</strong> </li>
          </ul></td>
      </tr>
      <tr>
        <td bgcolor="#FFFFCC">&nbsp;</td>
        <td valign="top" bgcolor="#FFFFCC"><span class="Estilo5"></span></td>
      </tr>
      <tr>
        <td align="center" bgcolor="#FFFFCC"><span class="Estilo3">
          <input name="radiobutton" type="radio" value="1" />
          Votar por lista 01 </span></td>
        <td align="center" valign="top" bgcolor="#FFFFCC"><span class="Estilo6">
          <input name="radiobutton" type="radio" value="2" />
          Votar por lista 02 </span></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
        <td valign="top">&nbsp;</td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td align="center"><input name="Submit" type="submit" value="VOTAR" /></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
</table>
<%end if%>
<%
if (Request.QueryString("votar")="SI") then
	response.write("<font class='Estilo8' ><center>SELECCIONE UNA LISTA Y EFECTÚE SU VOTACIÓN</center></font>")
end if
%>
</form>
</body>
</html>
