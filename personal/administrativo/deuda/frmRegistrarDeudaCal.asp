<html>
<head>
<title>Documento sin t&iacute;tulo</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<style type="text/css">

#calendario{
	font-family: Tahoma, Arial, Helvetica, sans-serif;
	font-size: 10px;
	text-align: center;
	font-weight: bold;
	margin-left: auto;
	margin-right: auto;
}

/*#mes para configurar aspectos de la caja que muestra el mes y el año*/
#mes{
	font-weight: bold;
	text-align: center;
	color: #CC6633;
	background-color: #E4CAAF;


}
/*.diaS para configurar aspectos de la caja que muestra los días de la semana*/
.diaS{
	color: #ffffff;
	background-color: #666666;

}
/*.celda para configurar aspectos de la caja que muestra los días del mes*/
.celda {
	background-color: #FFFFFF;
	color: #000000;
	font-weight : normal;
}
/*.Hoy para configurar aspectos de la caja que muestra el día actual*/
.Hoy{
	color: #ffffff;
	background-color: #666666;
	font-weight: normal;
}
#miCalendario{
	text-align: center;
}
/*.selectores para configurar aspectos de los campos para el mes y el año*/
.selectores{
	font-family: verdana;
	font-size: 10px;
	color: #990000;
	margin-bottom: 2px;
}
</style>

<script type="text/javascript">
/**************************************************************
Calendario con selector de día (fecha). Script creado por Tunait! (8/8/2004) Actualizado el 21-Dic.-04
Puedes usar este script en tu sitio mientras permanezcan intactas estas líneas, osea, los créditos.
No autorizo a distribuír el código en sitios de script sin previa autorización
Si quieres publicarlo, por favor, contacta conmigo.
Ver condiciones de uso en http://javascript.tunait.com/
tunait@yahoo.com 
****************************************************************/
var idContenedor = "miCalendario" //id del contenedor donde se insertará el calendario
var idCampofecha = "fechaCalendario" //id para el campo donde se mostrará la fecha
var fSalidaNombreMes = false //true escribe el mes por su nombre; false por su número
var fMesAbreviado = true // abrevia el nombre del mes a sus 3 primeras letras
var separadorFecha = "/" //separador para la fecha de salida
var celda = 16 //anchura en pixels para cada cuadro del calendario
var borde = 1 //anchura en pixels para los bordes 
var colorBorde = "#666666" //color de los bordes

/*No tocar nada a partir de aquí */
var hoy = new Date()
var mes = hoy.getMonth()
var dia = 1
var anio = hoy.getFullYear()
var diasSemana = new Array ('L','M','M','J','V','S','D')
var meses = new Array('Enero','Febrero','Marzo','Abril','Mayo','Junio','Julio','Agosto','Septiembre','Octubre','Noviembre','Diciembre')
var tunIex=navigator.appName=="Microsoft Internet Explorer"?true:false;
if(tunIex && navigator.userAgent.indexOf('Opera')>=0){tunIex = false}
tunOp = navigator.userAgent.indexOf('Opera')>=0 ? true: false;
var tunSel = false
function tunCalendario(){
dia2 = dia
var anCa = celda * 7
anCa += borde * 6 
if(tunIex || tunOp){anCa +=2}
tab = document.createElement('div')
tab.id = 'calendario'
tab.style.width = anCa + "px"
tab.style.padding = "1px"
tab.style.backgroundColor = colorBorde
document.getElementById(idContenedor).appendChild(tab)
fCalendario = document.createElement('input')
fCalendario.type = "text"
fCalendario.className = "selectores"
fCalendario.id = idCampofecha
fCalendario.name = idCampofecha
fCalendario.onfocus = function(){this.blur()}

document.getElementById(idContenedor).appendChild(fCalendario)

fi2 = document.createElement('div')
fi2.id = 'mes'
fi2.style.clear = "both"
fi2.style.height = celda + "px"
fi2.style.marginBottom = borde + "px"
fi2.appendChild(document.createTextNode(meses[mes] + "  -  " + anio))
fi = document.createElement('div')
fi.appendChild(fi2)
fi.className = 'fila'
fi.style.clear = "both"
tab.appendChild(fi)
fi.style.height = celda + "px"
fi.style.marginBottom = borde + "px"
for(m=0;m<7;m++){
	ce = document.createElement('div')
	ce.style.width = celda + "px"
	ce.style.height = celda + "px"
	ce.style.marginRight = borde + "px"
	ce.className = "diaS"
	tunIex ? ce.style.styleFloat = "left" : ce.style.cssFloat ="left"
	ce.appendChild(document.createTextNode(diasSemana[m]))
	fi.appendChild(ce)
	if(m == 6){ce.style.marginRight = 0}
	}
	var escribe = false
	var escribe2 = true
fecha = new Date(anio,mes,dia)
var d = fecha.getDay()-1 
if(d<0){d = 6}
while(escribe2){
fi = document.createElement('div')
fi.className = 'fila'
fi.style.clear = "both"
fi.style.marginBottom = borde + "px"
fi.style.height = celda + "px"
co = 0
	for(t=0;t<7;t++){
		ce = document.createElement('div')
		ce.style.width = celda + "px"
		ce.style.height = celda + "px"
		ce.style.marginRight = borde + "px"
		ce.style.position = "relative"
		if(escribe && escribe2){
		fecha2 = new Date(anio,mes,dia)
			if(fecha2.getMonth() != mes){escribe2 = false;}
			else{
			ce.appendChild(document.createTextNode(dia));
			dia++;
			co++;
			ce.onclick = marcaCalendario
			}
		}
		if(d == t && !escribe){
		ce.appendChild(document.createTextNode(dia))
		dia++;co++
		escribe = true
		ce.onclick = marcaCalendario
		}
		fi.appendChild(ce)
		if(hoy.getDate()+1 == dia && mes == hoy.getMonth() && anio == hoy.getFullYear()){ce.className = "Hoy"}
		else{ce.className = 'celda'}
		tunIex ? ce.style.styleFloat = "left" : ce.style.cssFloat ="left"
		if(t == 6){ce.style.marginRight = 0}
		}
		
	if(co>0){tab.appendChild(fi)}
	
	}
dia = dia2
}
function marcaCalendario(){
salidaMes = mes +1
if(fSalidaNombreMes){
	salidaMes = meses[mes] 
	if(fMesAbreviado){
		salidaMes = salidaMes.substring(0,3)
	}
}
<!--Modificaciones Hechas por Hector (a.- Poner comentario a esta Limea, b.-Agregar Codigo para mostrar fecha en caja de texto del frm)-->
<!--document.getElementById(idCampofecha).value = this.firstChild.nodeValue + separadorFecha + salidaMes + separadorFecha + anio -->
document.frmRegistrarDeuda.txtFecha.value=  this.firstChild.nodeValue + separadorFecha + salidaMes + separadorFecha + anio

ceSe = document.createElement('div')
ceSe.id = "tunSeleccionado"
with(ceSe.style){
	borderWidth = "1px"
	borderStyle = "solid"
	borderColor = "#ff0000"
	width = celda -2 + "px"
	height = celda -2 + "px"
	position = "absolute"
	left = 0
	top = 0
	zIndex = "200"
	}
	if(tunSel){
		tunSel.removeChild(tunSel.firstChild.nextSibling)
	}
tunSel = this
this.appendChild(ceSe)
}

function borra(){
document.getElementById(idContenedor).removeChild(document.getElementById('calendario'))
document.getElementById(idContenedor).removeChild(document.getElementById(idCampofecha))
<!-- Hector: Agregue La linea de Abajo -->
document.frmPrueba.txtFecha.value=""
}
function establecerFecha(){
tunFe = new Date()
document.getElementById('tunMes').options[tunFe.getMonth()].selected = true
document.getElementById('tunAnio').value = tunFe.getFullYear()
}
</script>

</head>

<body onload="tunCalendario();establecerFecha()" >
<p> 
  <select name="select" class="selectores" id="select3" onchange="mes=this.selectedIndex;borra();tunCalendario()">
    <option value="0">Enero</option>
    <option value="1">Febrero</option>
    <option value="2">Marzo</option>
    <option value="3">Abril</option>
    <option value="4">Mayo</option>
    <option value="5">Junio</option>
    <option value="6">Julio</option>
    <option value="7">Agosto</option>
    <option value="8">Septiembre</option>
    <option value="9">Octubre</option>
    <option value="10">Noviembre</option>
    <option value="11">Diciembre</option>
  </select>
  <input name="text" type="text" class="selectores" id="text3" onblur="if(!isNaN(this.value)){anio=this.value;borra();tunCalendario()}" size="4" maxlength="4" />
<table width="13%" border="1">
  <tr>
    <!--<td id="miCalendario">&nbsp;</td> -->
	<td id="miCalendario">&nbsp;</td>
  </tr>
</table>

<p> </p>
<p>&nbsp; </p>

<p> 
  <% 
Dim codResp
Dim tipoResp
Dim nomResp
codResp=Request.QueryString("id") 
tipoResp=Request.QueryString("tr") 
'Localizar Datos del Responsable
if tipoResp="A" then
        tipoResponsable=" ALUMNO "
		Set objRes= Server.CreateObject("PryUSAT.clsDatAlumno")
		Set rsRes= Server.CreateObject("ADODB.RecordSet")
		Set rsRes= objRes.ConsultarAlumno("RS","CO",codResp)
		if rsRes.recordcount >0 then
			nomResp=rsRes("Alumno")
		end if
end if

if tipoResp="T" then
        tipoResponsable=" TRABAJADOR "
		'FALTA
end if

if tipoResp="O" then
	        tipoResponsable=" OTROS "
			'FALTA
end if





Dim objCon
Dim rsCon
Set objCon=Server.CreateObject("PryUSAT.clsDatServicio")
Set rsCon=Server.CreateObject("ADODB.Recordset")
Set rsCon= objCon.ConsultarServicioConcepto ("RS","TO","")
%>
</p>
<form action="registrarDeuda.asp" method="post" name="frmRegistrarDeuda">
  <table width="75%" border="0" align="center">
    <tr> 
      <td colspan="4"><div align="center"><font size="4" face="Arial, Helvetica, sans-serif"><strong>Registrar 
          Deuda</strong></font></div></td>
    </tr>
    <tr> 
      <td width="24%" bgcolor="#fff5d5"> <div align="right"><font size="2" face="Arial, Helvetica, sans-serif">Tipo 
          de Responsable:</font></div></td>
      <td colspan="3"><font size="2" face="Arial, Helvetica, sans-serif"><%=tipoResponsable%></font></td>
    </tr>
    <tr> 
      <td bgcolor="#fff5d5"> <div align="right"><font size="2" face="Arial, Helvetica, sans-serif">Responsable<strong>:</strong></font></div></td>
      <td colspan="3"><font size="2" face="sans-serif"><b><%=nomResp%></b></font></td>
    </tr>
    <tr> 
      <td><font size="2" face="Arial, Helvetica, sans-serif">&nbsp; 
        <input name="txtTipoResp" type="hidden" id="txtTipoResp" value="<%=tipoResp%>">
        </font></td>
      <td colspan="3"><font size="2" face="Arial, Helvetica, sans-serif">&nbsp; 
        <input name="txtCodResp" type="hidden" id="txtCodResp" value="<% =codResp%>">
        </font></td>
    </tr>
    <tr> 
      <td rowspan="7" bgcolor="#fff5d5">&nbsp; </td>
      <td colspan="3">&nbsp;</td>
    </tr>
    <tr> 
      <td colspan="3">&nbsp;</td>
    </tr>
    <tr> 
      <td colspan="3">&nbsp;</td>
    </tr>
    <tr> 
      <td colspan="3">&nbsp;</td>
    </tr>
    <tr> 
      <td colspan="3">&nbsp;</td>
    </tr>
    <tr> 
      <td colspan="3">&nbsp;</td>
    </tr>
    <tr> 
      <td colspan="3">&nbsp;</td>
    </tr>
    <tr> 
      <td bgcolor="#fff5d5"> <div align="right"><font size="2" face="Arial, Helvetica, sans-serif">Fecha:</font></div></td>
      <td colspan="3"><font size="2" face="Arial, Helvetica, sans-serif"> 
        <input name="txtFecha" type="text" id="txtFecha" size="20">
        </font></td>
    </tr>
    <tr> 
      <td bgcolor="#fff5d5"> <div align="right"><font size="2" face="Arial, Helvetica, sans-serif">Concepto 
          de Deuda:</font></div></td>
      <td colspan="3"><font size="2" face="Arial, Helvetica, sans-serif"> 
        <select name="cboConcep_Deu" id="cboConcep_Deu">
          <% do while not rsCon.eof %>
          <option value="<%=rsCon(0)%>"><%=rsCon("descripcion_Sco")%></option>
          <% rsCon.movenext
		   loop
		   rsCon.close
		   set rsCon=Nothing
		   set objCon= Nothing
	    %>
        </select>
        </font></td>
    </tr>
    <tr> 
      <td bgcolor="#fff5d5"> <div align="right"><font size="2" face="Arial, Helvetica, sans-serif">Monto 
          de la Deuda:</font></div></td>
      <td width="19%"><font size="2" face="Arial, Helvetica, sans-serif"> 
        <input name="txtMon_Deu" type="text" id="txtMon_Deu3" size="20">
        </font></td>
      <td width="12%" bgcolor="#fff5d5"> <div align="right"><font size="2" face="Arial, Helvetica, sans-serif">Moneda:</font></div></td>
      <td width="45%"><font size="2" face="Arial, Helvetica, sans-serif"> 
        <select name="cboMoneda" id="select2">
          <option value="S">Soles</option>
          <option value="D">Dolares</option>
          <option value="E">Euros</option>
        </select>
        Como Decimal Reconoce a la coma (,)</font></td>
    </tr>
    <tr> 
      <td bgcolor="#fff5d5"> <div align="right"><font size="2" face="Arial, Helvetica, sans-serif">Observaci&oacute;n:</font></div></td>
      <td colspan="3"><font size="2" face="Arial, Helvetica, sans-serif"> 
        <input name="txtObserv" type="text" id="txtObserv" size="80">
        </font></td>
    </tr>
    <tr> 
      <td>&nbsp;</td>
      <td colspan="2"><div align="right"> 
          <input name="cmdGrabar" type="submit" id="cmdGrabar" value="Registrar Deuda">
        </div></td>
      <td><input name="cmdCancelar" type="reset" id="cmdCancelar" value="Cancelar Registro"></td>
    </tr>
  </table>
  </form>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>

<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<p>&nbsp;</p>

</body>
</html>