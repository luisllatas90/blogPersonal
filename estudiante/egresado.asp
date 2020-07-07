<%    
      
   
    Session.Contents.RemoveAll    
    on error resume next
    
    'response.Redirect("https://intranet.usat.edu.pe/campusestudiante/Default.aspx?Tipo=Mg==")
    
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
        
        session("tkn") = dato3 & dato6 & dato2 & dato5 & dato1 & dato4
        'response.Write (session("tkn") & "</BR>")
        'response.Write ("Sesion:" & Session.SessionID)
    if(Err.number <> 0) then
        response.Write "Error al cargar página"
    end if
%>
<html>
<head>
<title>Campus Virtual</title>
<META HTTP-EQUIV="Cache-Control" CONTENT ="no-cache">
<link href="../private/estilo.css" rel="stylesheet" type="text/css">

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
    .style1
    {
        color: #FF0000;
        font-weight: bold;
        font-size: 9pt;
    }
    .style2
    {
        color: #FF0000;
    }
-->
</style>
<script language="JavaScript" src="../private/validaracceso.js"></script>
<script language="JavaScript" src="../private/tooltip.js"></script>

</head>
<!--<body bgcolor="#ffffff" onLoad="MM_preloadImages('images/CAMPUS3_r2_c2_f2.jpg','images/CAMPUS3_r2_c4_f2.jpg','images/CAMPUS3_r4_c2_f2.jpg','images/CAMPUS3_r4_c4_f2.jpg')">
-->
<body topmargin="0" leftmargin="0" bgcolor="#ffffff" >
<table width="758" align="center" cellpadding="0" cellspacing="0" border="0" style="border:1px solid #642C2F;">
<!-- fwtable fwsrc="CAMPUS3.png" fwbase="CAMPUS3.jpg" fwstyle="Dreamweaver" fwdocid = "1726926981" fwnested="0" -->
  <tr>
   <td width="303"><img src="../images/spacer.gif" width="303" height="1" border="0" alt=""></td>
   <td width="130"><img src="../images/spacer.gif" width="130" height="1" border="0" alt=""></td>
   <td width="20"><img src="../images/spacer.gif" width="20" height="1" border="0" alt=""></td>
   <td width="197"><img src="../images/spacer.gif" width="197" height="1" border="0" alt=""></td>
   <td width="376"><img src="../images/spacer.gif" width="41" height="1" border="0" alt=""></td>
   <td width="1"><img src="../images/spacer.gif" width="1" height="1" border="0" alt=""></td>
  </tr>

  <tr>
   <td colspan="5">
        <table width="65%" height="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="5"><img src="../images/alumni1.png?X=3" ></td>
            </tr>
        </table>
   </td></tr>
   <tr>   
   <td colspan="2" align="center" >
        <img src="../images/logoalumni_.png?y=1" style="float: center; text-align: center">
   </td>
   
   <td colspan="4">
     <br />
       <form name="frmAcceso" method="post" action="accederEgresado.asp">
         <table class="" width="70%" border="0" cellpadding="3" cellspacing="0" style="padding-right:5px;">
           <tr align="center" bgcolor="#e33439" >
             <td colspan="4" bgcolor="#e33439" class="usatSubTitulo"><b>Ingrese a Bolsa de trabajo  alumniUSAT</b></td>
                        </tr>
          
           <tbody id="TRestudiante">
             <tr bgcolor="">
               <td  width="40%" align="right" id="TRtipo" class="Estilo4">DNI:</td>
               <td width="60%" colspan="3" ><input type="text" size="20" name="Login" id="Login" class="Cajas2" size="15" /></td>
               
             </tr>
             <tr bgcolor="">
               <td width="40%" align="right" class="Estilo4">Contrase&ntilde;a</td>
               <td width="60%" colspan="3"><input type="password" size="20" name="Clave" id="Clave" class="Cajas2" size="15" onkeyup="if(event.keyCode==13){ValidarAcceso(frmAcceso)}" maxlength="20" tooltip="Ingrese su contrase&ntilde;a hasta <b>20 caracteres</b>" /></td>
               
             </tr>
           </tbody>
           <tr  bgcolor="">
             <td WIDTH="100%" colspan="4" align="right">
                <input type="submit" class="usatbuscar" value="Ingresar" name="cmdBuscar" style="width: 100" />  
                <input name="button" type="button" class="usatsalir" style="width: 100" onclick="top.window.close()" value="Salir" /></td>

           </tr>
           <tr align="center" bgcolor="#808000" id="TRclave" height="30" >
             <td colspan="4" bgcolor="#e33439"  id=mensaje>
             <a href="../mailnet/recoverypassEgresado.aspx">
             <font size='8'><b></font><strong>SOLICITAR CONTRASEÑA</strong>
             </a>&nbsp;&nbsp;&nbsp;&nbsp;
         <!--    <a href="../librerianet/egresado/manualdeusuario.pdf" target="_blank" title="Manual de Usuario"> -->
         <!--    <img src="../images/user.png" style="border-style: none; border-color: inherit; border-width: 0px; width:16px;" /></a>-->
             </td>
             
           </tr>
         </table>
       </form>
   </td></tr>
   
  
  
   <tr >   
   <td colspan="2" align="center">                       
           
             <!--<a href="../librerianet/egresado/alumni_recuperar_password.gif" target="_blank" title="¿Cómo solicitar tu contraseña?">
             <img src="../images/pdf.png" 
                 style="border-style: none; border-color: inherit; border-width: 0px; width:16px;" 
                 class="style2" /></a>-->
				
			 <a href="../librerianet/egresado/alumni_recuperar_password.gif" target="_blank" title="¿Cómo solicitar tu contraseña?">
				<font color="Black">¿Cómo solicitar tu contraseña?</font>
			 </a>
			 
           
			
</td>
   
   <td colspan="4">
       &nbsp;</td></tr>
   
  
  
   <tr >   
   <td colspan="2">&nbsp;</td>
   
   <td colspan="4">
       &nbsp;</td></tr>
   
  
  
</table>

</body>
</html>