<html>
<head>
<title>III Jornada Internacional de Enfermeria</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<!--Fireworks MX 2004 Dreamweaver MX 2004 target.  Created Wed Apr 05 06:11:09 GMT-0500 2006-->
<script language="JavaScript">
<!--

function MM_preloadImages() { //v3.0
 var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
   var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
   if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

//-->
</script>
<style type="text/css">
<!--
body {
	background-image: url('files/fondo.gif');
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
	color: #990000;
}
body, td, th {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 12px;
}
.Estilo2 {	color: #FFFFFF;
	font-weight: bold;
}
.Estilo3 {
	font-size: 12px;
	color: #003366;
}
-->
</style></head>
<body bgcolor="#ffffff">
<table width="700" border="0" align="center" cellpadding="0" cellspacing="0">
<!-- fwtable fwsrc="prueba34.png" fwbase="agrocadenas.gif" fwstyle="Dreamweaver" fwdocid = "83761823" fwnested="0" -->
  <tr>
   <td width="28"><img src="files/spacer.gif" width="26" height="1" border="0" alt=""></td>
   <td width="95"><img src="files/spacer.gif" width="89" height="1" border="0" alt=""></td>
   <td width="11"><img src="files/spacer.gif" width="10" height="1" border="0" alt=""></td>
   <td width="67"><img src="files/spacer.gif" width="63" height="1" border="0" alt=""></td>
   <td width="11"><img src="files/spacer.gif" width="10" height="1" border="0" alt=""></td>
   <td width="69"><img src="files/spacer.gif" width="65" height="1" border="0" alt=""></td>
   <td width="13"><img src="files/spacer.gif" width="12" height="1" border="0" alt=""></td>
   <td width="71"><img src="files/spacer.gif" width="67" height="1" border="0" alt=""></td>
   <td width="13"><img src="files/spacer.gif" width="12" height="1" border="0" alt=""></td>
   <td width="69"><img src="files/spacer.gif" width="65" height="1" border="0" alt=""></td>
   <td width="12"><img src="files/spacer.gif" width="11" height="1" border="0" alt=""></td>
   <td width="115"><img src="files/spacer.gif" width="76" height="1" border="0" alt=""></td>
   <td width="105"><img src="files/spacer.gif" width="105" height="1" border="0" alt=""></td>
   <td width="21"><img src="files/spacer.gif" width="21" height="1" border="0" alt=""></td>
  </tr>
  <tr>
    <td colspan="14" bgcolor="#FFFFFF"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="94%" align="center" bgcolor="#003366"><span class="Estilo2">CONSULTA DE PRE - INSCRITOS </span></td>
        </tr>
      <tr>
        <td>&nbsp;</td>
        </tr>
      <tr>
        <td align="center"><strong>III Jornada Internacional de Enfermeria</strong></td>
        </tr>
      <tr>
        <td>&nbsp;</td>
        </tr>
      <tr>
        <td align="center"><%
Set conn = Server.CreateObject("ADODB.Connection")
Conn.open "DRIVER={Microsoft Access Driver (*.mdb)}; DBQ=" & Server.MapPath("base.mdb")
set rs=Server.CreateObject("ADODB.recordset")
set rs1=Server.CreateObject("ADODB.recordset")

sql  = "SELECT * FROM interesados"
rs.Open sql, conn

if rs.EOF =true or rs.BOF = true then
	Response.Write("No se encontraron registros")
else
Arraykike = rs.GetRows()
numreg = UBound(Arraykike,2) - LBound(Arraykike,2)
dim consulta(1000)
response.write("<table border=1 cellpadding='0' cellspacing='0' style='border-collapse: collapse'>")
response.write("<tr bgcolor='#0066CC' align='center'>")

response.write("<td>")
response.write("<b>Nombres</b>")
response.write("</td>")

response.write("<td>")
response.write("<b>Apellidos</b>")
response.write("</td>")

response.write("<td>")
response.write("<b>Dirección</b>")
response.write("</td>")

response.write("<td>")
response.write("<b>Correo Electrónico</b>")
response.write("</td>")

response.write("<td>")
response.write("<b>Teléfono</b>")
response.write("</td>")

response.write("<td>")
response.write("<b>Celular</b>")
response.write("</td>")

response.write("<td>")
response.write("<b>Profesión</b>")
response.write("</td>")

response.write("<td>")
response.write("<b>Est. Concluidos</b>")
response.write("</td>")

response.write("<td>")
response.write("<b>Institución</b>")
response.write("</td>")

response.write("<td>")
response.write("<b>Centro Laboral</b>")
response.write("</td>")

response.write("<td>")
response.write("<b>Cargo</b>")
response.write("</td>")

response.write("<td>")
response.write("<b>Dir. Centro Lab.</b>")
response.write("</td>")

response.write("<td>")
response.write("<b>Sector</b>")
response.write("</td>")

response.write("<td>")
response.write("<b>Registrado</b>")
response.write("</td>")

response.write("</tr>")

For x = 0 to numreg
	response.write("<tr>")
	response.write("<td>")
	response.write(Arraykike(0,x))
	response.write("</td>")
	response.write("<td>")
	response.write(trim(Arraykike(1,x))) 
	response.write("</td>")
	response.write("<td>")
	response.write(Arraykike(2,x))
	response.write("</td>")
	response.write("<td>")
	response.write(Arraykike(3,x))
	response.write("</td>")
	response.write("<td>")
	response.write(Arraykike(4,x))
	response.write("</td>")
	response.write("<td>")
	response.write(Arraykike(5,x))
	response.write("</td>")
	response.write("<td>")
	response.write(Arraykike(6,x))
	response.write("</td>")
	response.write("<td>")
	response.write(Arraykike(7,x))
	response.write("</td>")
	response.write("<td>")
	response.write(Arraykike(8,x))
	response.write("</td>")
	response.write("<td>")
	response.write(Arraykike(9,x))
	response.write("</td>")	
	response.write("<td>")
	response.write(Arraykike(10,x))
	response.write("</td>")	
	response.write("<td>")
	response.write(Arraykike(11,x))
	response.write("</td>")	
	response.write("<td>")
	response.write(Arraykike(12,x))
	response.write("</td>")			
	response.write("<td>")
	response.write(Arraykike(13,x))
	response.write("</td>")	

	response.write("</tr>")
Next 
response.write("</table>")
%>
          <br>
          <b><span class="Estilo3">Total de participantes Inscritos:</span></b><font color="#800000"><b><font size="3">
          <%response.write(numreg+1)%>
          </font></b></font></td>
        </tr>
      <tr>
        <td>&nbsp;</td>
        </tr>
    </table></td>
  </tr>
</table>
<% end if

rs.close
conn.Close %>
</body>
</html>