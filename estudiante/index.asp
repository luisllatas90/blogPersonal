<%
    Session.Contents.RemoveAll
    on error resume next
        '************** Generando el codigo del token **************
        semilla = Session.SessionID
        semillaReversa = StrReverse(semilla)
        dato1 = Left(semillaReversa, 2) 
        dato2 = Mid(semillaReversa, 3, Len(semillaReversa) - 4)
        dato3 = Right(semillaReversa, 2)
        '******* Datos para despistar *******
        max=30
        min=1
        Randomize                
        dato4 = second(Now) 'Segundos
        dato5 = minute(Now) + int((max-min+1)*rnd+min)
        dato6 = hour(Now) + int((max-min+1)*rnd+min)        
        '************************************                                                        
        
        if(len(dato4) = 1)then 
            dato4 = 0 & dato4
        end if
        
        if(len(dato5) = 1)then 
            dato5 = 0 & dato5
        end if
        
        if(len(dato6) = 1)then 
            dato6 = 0 & dato6
        end if
        
				Session("lbltoken") = generateCodeString(16) 'genera el nombre del token
				Session("valtoken") = generateCodeString(64) 'genera el valor del token
				
        session("tkn") = dato3 & dato6 & dato2 & dato5 & dato1 & dato4
        'response.Write (session("tkn") & "</BR>")
       ' response.Write ("Sesion:" & Session.SessionID)
    if(Err.number <> 0) then
        response.Write "Error al cargar página"
    end if
		
		
		
		Function generateCodeString(strLength)
			Dim sDefaultChars
			Dim iCounter
			Dim sMyPassword
			Dim iPickedChar
			Dim iDefaultCharactersLength
			Dim iPasswordLength
			sDefaultChars="L(A>&qTTyq|$r1p)I6,3!VIu[.B(|Q*d;ML]C^S.xF20k[{b~m1XM-)5mc6f57:S"
			iPasswordLength=strLength
			iDefaultCharactersLength = Len(sDefaultChars)
			Randomize
			For iCounter = 1 To iPasswordLength
				iPickedChar = Int((iDefaultCharactersLength * Rnd) + 1)
				sMyPassword = sMyPassword & Mid(sDefaultChars,iPickedChar,1)
			Next
			generateCodeString = sMyPassword
		End Function
%>
<html>
<head>
<title>Campus Virtual</title>
<META HTTP-EQUIV="Cache-Control" CONTENT ="no-cache">
<link href="../private/estilo.css" rel="stylesheet" type="text/css">
<script language="JavaScript" type="text/JavaScript">

var ancho = 400 // especifica la anchura a mostrar
var alto = 120 // especifica la altura a mostrar (alto de las imágenes)
var velo = 10 // velocidad 
var dis = 2 //cantidad de pixels que desplaza por movimiento

var imagenes = new Array()
imagenes[0] = new Image()
imagenes[0].src = "../images/biblioteca2.jpg" // ruta o nombre de imagen 
imagenes[0].a = "" // link de la imagen
imagenes[0].target = "_blank" //target del link
imagenes[1] = new Image()
imagenes[1].src = "../images/biblioteca31.gif"
imagenes[1].a = ""
imagenes[1].target = "_self"
imagenes[2] = new Image()
imagenes[2].src = "../images/biblioteca1.jpg"
imagenes[2].a = ""
imagenes[2].target = "_blank"
imagenes[3] = new Image()
imagenes[3].src = "../images/biblioteca40.jpg"
imagenes[3].a = ""
imagenes[3].target = "_blank"
imagenes[4] = new Image()
imagenes[4].src = "../images/biblioteca41.jpg"
imagenes[4].a = ""
imagenes[4].target = "_blank"
imagenes[5] = new Image()
imagenes[5].src = "../images/biblioteca42.jpg"
imagenes[5].a = ""
imagenes[5].target = "_blank"
imagenes[6] = new Image()
imagenes[6].src = "../images/biblioteca43.jpg"
imagenes[6].a = ""
imagenes[6].target = "_blank"

var vel = velo
pasos = 4
var tot = 0
var tam =0;
var pos,pos2,tam2 =0;
function escribe(){
document.write ('<div id ="fuera" style="position:relative; width:' + ancho + 'px; height:' + alto + 'px;overflow:hidden">');
document.write ('<span id="imas" style="position:absolute; width:' + tam + 'px;height:' + alto + 'px; left = -' + tam + 'px;"  onmouseover="if(detienee == 0){detienee = 1}" onmouseout="clearTimeout(tiempo);detienee=0;atras = false;vel=velo;mueve()">');
for (m=0;m<imagenes.length;m++){
	if(imagenes[m].a != ""){
		document.write('<a href="' + imagenes[m].a + '" target="' + imagenes[m].target + '">')
		}
	document.write ('<img border="0"  src ="' + imagenes[m].src + '" id="ima' + m + '" name="ima' + m + '"  onload="tot++;">');
	if(imagenes[m].a != ""){document.write ('</a>')}
	}
document.write ('</span>');
document.write ('<span id="imas2" style="position:absolute; width:' + tam + 'px;height:' + alto + ';left=0;"  onmouseover="if(detienee == 0){detienee = 1}" onmouseout="clearTimeout(tiempo);detienee=0;atras = false;vel=velo;mueve()">');
for (m=0;m<imagenes.length;m++){
	if(imagenes[m].a != ""){
		document.write('<a href="' + imagenes[m].a + '" target="' + imagenes[m].target + '">')
		}
	document.write ('<img border="0" src ="' + imagenes[m].src + '" id="imaa' + m + '" name="imaa' + m + '" onload="tot++;">');
	if(imagenes[m].a != ""){document.write ('</a>')}
	}
document.write ('</span>');
document.write ('</div>');
}
var detienee = 0,posb,pos2b;
function mueve(){
pos = document.getElementById('imas').style.left;
pos2 = document.getElementById('imas2').style.left;
pos = pos.replace(/px/,"");
pos = pos.replace(/pt/,"");
pos = new Number(pos);
pos2 = pos2.replace(/px/,"");
pos2 = pos2.replace(/pt/,"");
pos2 = new Number(pos2);
if(detienee == 1){
	posb = pos;
	pos2b = pos2;
	}
if(atras == true){
pos-=dis;
pos2 -=dis;
}
else{
pos += dis;
pos2 += dis;
}
if(pos2 > (ancho + dis)){
	if(detienee == 0){
		document.getElementById('imas2').style.left = pos  - (tam + dis);
		pos2 = document.getElementById('imas2').style.left;
		}
	else{
		document.getElementById('imas').style.left = pos 
		}
	}
else{
	document.getElementById('imas').style.left = pos 
	}

if(pos > (ancho + dis)){
	if(detienee == 0){
		document.getElementById('imas').style.left = pos2 - (tam + dis);
		pos = document.getElementById('imas').style.left;
		}
	else{
		document.getElementById('imas2').style.left = pos2;
		}
	}
else{
	document.getElementById('imas2').style.left = pos2
	}
if(detienee > 0){
	if(detienee == pasos){
		vel = velo;
		atras = true;
		detienee--;
		tiempo = setTimeout('mueve()',vel);
		}
	else{
		if(atras == true){
			if(detienee>(pasos/2))
				{detienee--}
			else{
			vel = velo;
			clearTimeout(tiempo)
			}
			}
	else{
		detienee++
		}
	if(detienee > (pasos/2) && atras == false){vel +=10}
		if(detienee < (pasos/2)){vel +=10}
		tiempo = setTimeout('mueve()',vel)
		}
	}
else{
tiempo = setTimeout('mueve()',vel)
	}
if(atras == true){
		if (pos == posb){
			clearTimeout(tiempo);
			atras = false;
			}
		}
}
var tiempo;
var atras = false, ini;
function inicio(){
if(tot == (imagenes.length * 2)){clearTimeout(ini);reDimCapas();mueve()}
else{ini=setTimeout('inicio()',500)}
}
function reDimCapas(){
for(m=0;m<imagenes.length;m++){
	tam +=document.getElementById('ima'+m).width
	document.getElementById('imas').style.left = (-tam +10);
	document.getElementById('imas').style.width = tam ;
	document.getElementById('imas2').style.width = tam;
	}
}

function AbrirPopUp(pagina,alto,ancho,ajustable,bestado,barras,variable)
{
   var izq = (screen.width-ancho)/2
   var arriba= (screen.height-alto)/2
   if (variable=="" || variable==undefined)
	{variable="popup"}
   var ventana=window.open(pagina,variable,"height="+alto+",width="+ancho+",statusbar="+bestado+",scrollbars="+barras+",top=" + arriba + ",left=" + izq + ",resizable="+ajustable+",toolbar=no,menubar=no");
   ventana.location.href=pagina
   ventana=null
}
</script>

<style type="text/css">
<!--
.Estilo4 {color: #000000; font-size:8pt}
.mensaje     { border:1px solid #808080; color: #FFFFFF; font-weight: bold; background-color: #EDBB78 }
a:link       {
	color: #FFFFFF;
	text-decoration: none;
}
a:visited {
	text-decoration: none;
	color: #FFFFFF;
}
a:hover {
	text-decoration: underline;
}
a:active {
	text-decoration: none;
}
body, td, th {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 10px;
	color: #000000;
}
.Estilo5 {
	color: #000000;
	font-size: 12px;
}
.Estilo6 {
	color: #990000;
	font-size: 14px;
}
-->
</style>
<script language="JavaScript" src="../private/validaracceso.js"></script>
<script language="JavaScript" src="../private/tooltip.js"></script>
<script language="JavaScript">
<!--
function MM_preloadImages() { //v3.0
 var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
   var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
   if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_findObj(n, d) { //v4.01
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && d.getElementById) x=d.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}

function MM_openBrWindow(theURL,winName,features) { //v2.0
  window.open(theURL,winName,features);
}
//-->
</script>

</head>
<!--<body bgcolor="#ffffff" onLoad="MM_preloadImages('images/CAMPUS3_r2_c2_f2.jpg','images/CAMPUS3_r2_c4_f2.jpg','images/CAMPUS3_r4_c2_f2.jpg','images/CAMPUS3_r4_c4_f2.jpg')">
-->
<body topmargin="0" leftmargin="0" bgcolor="#ffffff" >
<table width="758" border="0" align="center" cellpadding="0" cellspacing="0">
<!-- fwtable fwsrc="CAMPUS3.png" fwbase="CAMPUS3.jpg" fwstyle="Dreamweaver" fwdocid = "1726926981" fwnested="0" -->
  <tr>
   <td width="303"><img src="../images/spacer.gif" width="303" height="1" border="0" alt=""></td>
   <td width="197"><img src="../images/spacer.gif" width="197" height="1" border="0" alt=""></td>
   <td width="20"><img src="../images/spacer.gif" width="20" height="1" border="0" alt=""></td>
   <td width="197"><img src="../images/spacer.gif" width="197" height="1" border="0" alt=""></td>
   <td width="376"><img src="../images/spacer.gif" width="41" height="1" border="0" alt=""></td>
   <td width="1"><img src="../images/spacer.gif" width="1" height="1" border="0" alt=""></td>
  </tr>

  <tr>
   <td colspan="5"><table width="73%" height="100%" border="0" cellpadding="0" cellspacing="0">
     <tr>
       <td colspan="5"><img src="../images/cabeceraCampus.jpg" width="758" height="200"></td>
     </tr>
     <tr>
       <td colspan="3"><img src="../images/campus_der1.gif" width="380" height="21"></td>
       <td width="11%" rowspan="3" align="left" valign="bottom"><img src="../images/campus_inf_1.gif" width="100" height="50" border="0"></td>
       <td width="38%" rowspan="3" align="left"><img src="../images/campus_izq.gif" width="278" height="50"></td>
     </tr>
     <tr>
       <td width="2%"><img src="../images/campus_der3.gif" width="19" height="25"></td>
       <td width="16%"><a href="../images/matricula15-2.jpg" target="_blank"><img src="../images/tapa_preguntas.gif" alt="" width="148" height="25" border="0"></a></td>
       <td width="33%"><img src="../images/campus_der14.gif" width="213" height="25"></td>
     </tr>
     <tr>
       <td colspan="3"  height="4" ><img src="../images/campus_der2.gif" width="380" height="4"></td>
     </tr>
     <tr>
       <td colspan="5"><table width="100%" border="0" cellspacing="0" cellpadding="0">
         <tr>
           <td><img src="../images/campusinf03_.gif" width="758" height="28"></td>
         </tr>
         <tr>
           <td><table width="100%" border="0" cellspacing="0" cellpadding="0">
             <tr>
               <td><img src="../images/campusinf4.gif"></td>
                <td width="409" align="center" valign="top">
<!--  
			<MARQUEE direction="up" height="120" onmouseout=this.start() onmouseover=this.stop() scrollAmount=4 scrollDelay=0 width=409>				
			-->
			
				</td>
                <td><img src="../images/campusinf5.gif" height="138" alt="" /></td>
             </tr>
           </table></td>
         </tr>
         <tr>
           <td><img src="../images/campus_inf2.gif" width="758" height="57"></td>
         </tr>
       </table></td>
     </tr>
   </table>
     <div style="z-index: 1; position:absolute; left:57%; top:45%; width:30% ">
       <form name="frmAcceso" method="post" action="frmAcceso.asp">
         <table class="contornotabla" width="100%" border="0" cellpadding="3" cellspacing="0">
           <tr align="center" bgcolor="#990000" >
             <td colspan="4" bgcolor="#661C1D" class="usatSubTitulo"><b>Ingrese al Campus Virtual</b></td>
           </tr>
           <tr bgcolor="#DECE9C">
             <td width="40%" align="right" class="Estilo4">Tipo de Usuario</td>
             <td width="60%" colspan="3"><select size="1" id="cbxtipo" name="cbxtipo" id="cbxtipo" onchange="cambiartipo()" style="width: 100%">
                 <option value="A">Estudiante</option>
                 <option value="P">Personal USAT</option>
                 <!-- <option value="T">Particular</option> -->

                 </select></td>
           </tr>
		   
           <tbody id="TRestudiante">
             <tr bgcolor="#DECE9C">
               <td  width="40%" align="right" id="TRtipo" class="Estilo4">C&oacute;digo Universitario</td>
               <td width="60%" colspan="3" ><input type="text" size="20" name="Login" id="Login" class="Cajas2" size="15" /></td>
             </tr>
             <tr bgcolor="#DECE9C">
               <td width="40%" align="right" class="Estilo4">Contrase&ntilde;a</td>
               <td width="60%" colspan="3"><input type="password" size="20" name="Clave" id="Clave" class="Cajas2" size="15"  maxlength="20" tooltip="Ingrese su contrase&ntilde;a hasta <b>20 caracteres</b>" />
							 <input id="lbltoken" type="hidden" name="<%=Session("lbltoken")%>" value="<%=Session("valtoken")%>" />
							 </td>
             </tr>
           </tbody>
		   
           <tr bgcolor="#DECE9C">
             <td WIDTH="100%" colspan="4" align="right">
                <input type="submit" class="usatbuscar" value="Ingresar" name="cmdBuscar" style="width: 100" />  
                <input name="button" type="button" class="usatsalir" style="width: 100" onclick="top.window.close()" value="Salir" /></td>
           </tr>
           <!--<tr align="center" bgcolor="#808000" id="TRclave">
             <td colspan="4" bgcolor="#676735"  id=mensaje><a href=""><font size='4'><b></font><strong>Recuperar Contrase&ntilde;a</strong> </a></td>
           </tr>-->
         </table>
       </form>
    </div></td>
   <td><img src="../images/spacer.gif" width="1" height="473" border="0" alt=""></td>
  </tr>
  <tr>
   <td rowspan="4"><img name="CAMPUS3_r2_c1" src="../images/CAMPUS3_r2_c01_.jpg" width="303" height="87" border="0" alt=""></td>
   <td><a href="#" onmouseOut="MM_swapImgRestore()" onmouseOver="MM_swapImage('Image23','','images/CAMPUS3_r2_c2_f2.jpg',1)"><img src="../images/CAMPUS3_r2_c2.jpg" name="Image23" width="197" height="14" border="0" ></a></td>
   <td rowspan="4"><img name="CAMPUS3_r2_c3" src="../images/CAMPUS3_r2_c3.jpg" width="20" height="87" border="0" alt=""></td>
   <td><a href="#" onmouseOut="MM_swapImgRestore()" onmouseOver="MM_swapImage('Image24','','images/CAMPUS3_r2_c4_f2.jpg',1)"><img src="../images/CAMPUS3_r2_c4.jpg" name="Image24" width="197" height="14" border="0" ></a></td>
   <td rowspan="4"><a href="#" ><img name="CAMPUS3_r2_c5" src="../images/CAMPUS3_r2_c5.jpg" width="41" height="87" border="0" alt=""></a></td>
   <td><img src="../images/spacer.gif" width="1" height="14" border="0" alt=""/></td>
  </tr>
  <tr>
   <td><img name="CAMPUS3_r3_c2" src="../images/CAMPUS3_r3_c2.jpg" width="197" height="15" border="0" alt=""/></td>
   <td><img name="CAMPUS3_r3_c4" src="../images/CAMPUS3_r3_c4.jpg" width="197" height="15" border="0" alt=""/></td>
   <td><img src="../images/spacer.gif" width="1" height="15" border="0" alt=""/></td>
  </tr>
  <tr>
   <td><img src="../images/tapapreguntasfrecuentes.jpg" name="Image25" width="197" height="14" border="0" alt="" /></td>
   <td><img src="../images/tapapreguntasfrecuentes.jpg" name="Image26" width="197" height="14" border="0" alt="" /></td>
   <td><img src="../images/spacer.gif" width="1" height="14" border="0" alt=""></td>
  </tr>
  <tr>
   <td><img name="CAMPUS3_r5_c2" src="../images/CAMPUS3_r5_c2.jpg" width="197" height="44" border="0" alt=""></td>
   <td><a href="#" ><img name="CAMPUS3_r5_c4" src="../images/CAMPUS3_r5_c4.jpg" width="197" height="44" border="0" alt=""></a></td>
   <td><img src="../images/spacer.gif" width="1" height="44" border="0" alt=""></td>
  </tr>
</table>

</body>
</html>