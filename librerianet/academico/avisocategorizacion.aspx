<%@ Page Language="VB" AutoEventWireup="false" CodeFile="avisocategorizacion.aspx.vb" Inherits="librerianet_academico_avisocategorizacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    p.MsoNormal
	{margin-top:0cm;
	margin-right:0cm;
	margin-bottom:10.0pt;
	margin-left:0cm;
	line-height:115%;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	}
	body
	{	font-family:"Calibri","sans-serif";padding:100px´; padding-top:0px;}
	
	
        .style2
        {
            text-decoration: underline;
        }
	
	
    a:link
	{color:blue;
	text-decoration:underline;
	text-underline:single;
        }
	
	
        .style3
        {
            font-weight: bold;
        }
	.btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3; 
            font-family:Tahoma; 
            font-size:small; 
            font-weight:bold;  padding:3px; 
       }
	
        .style4
        {
            background-color: #E33439;
        }
	
        .style5
        {
            font-weight: bold;
            color: #0000CC;
            font-size:11.0pt;
        }
	
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <div style=" border:1px solid #E33439; width:50%; padding:35px; padding-top:0px;">
        <table class="style1">
            <tr>
                <td  style="text-align:left">
                    <asp:Image ID="Image1" runat="server" 
                        ImageUrl="http://www.usat.edu.pe/templates/dg_usat_1.0/images/logousat2e.gif" 
                        style="text-align:left" />
                </td>
                <td valign="bottom" align="right">
                    <p class="style3">
                        Chiclayo, diciembre de 2014.</p>
                </td>
            </tr>
            <tr>
                <td colspan="2" style=" text-align:left">
                        <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;">
                    Famila</span><br />
                    <asp:Label ID="lblFamilia" runat="server" Text="TxtFamilia" 
                        style="font-weight: 700"></asp:Label>
                    <br />
                        <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;">
                        <span class="style2">Ciudad</span>.-</span><br />
                    <br />
                    <p class="MsoNormal" style="text-align: justify">
                        <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp;&nbsp;&nbsp;
                        Es grato dirigirme a ustedes a fin de hacerles llegar el saludo cordial del 
                        Consejo Universitario de la Universidad Católica Santo Toribio de Mogrovejo y el 
                        mío propio, a pocos días de culminar el presente año académico.<o:p></o:p></span></p>
                    <p class="MsoNormal" style="text-align: justify">
                        <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp;&nbsp;&nbsp;
                        Durante 2014 importantes acontecimientos institucionales nos han llenado de 
                        orgullo, entre los que destaca la acreditación por el SINEACE de nuestras 
                        carreras de Enfermería, Educación Primaria y Educación Secundaria en la 
                        especialidad de Filosofía y Teología. Esto nos ha convertido en la primera 
                        universidad en el país en acreditar dicha especialidad y ser la primera 
                        universidad en la Región Lambayeque con tres carreras acreditadas, lo que 
                        demuestra, sin duda, que los estándares de calidad que venimos manejando en la 
                        USAT para beneficio de sus hijos son los mejores.<o:p></o:p></span></p>
                    <p class="MsoNormal" style="text-align: justify">
                        <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp;&nbsp;&nbsp;
                        Este proceso de calidad educativa no termina, este logro ha sido el punto de 
                        partida para alcanzar la ansiada acreditación institucional para lo cual los 
                        diversos equipos internos de trabajo vienen implementando las estrategias 
                        adecuadas para, en el más breve plazo, decir nuevamente: misión cumplida.<o:p></o:p></span></p>
                    <p class="MsoNormal" style="text-align: justify">
                        <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp;&nbsp;&nbsp;
                        El prestigio ganado por la USAT es producto del trabajo conjunto de toda la 
                        comunidad universitaria y de importantes decisiones económicas que han permitido 
                        en los últimos 4 años invertir poco más de 6 millones de soles en la movilidad 
                        de profesores nacionales y extranjeros, la implementación de laboratorios y 
                        bibliotecas especializadas, la construcción del edifico de idiomas, así como el 
                        mejoramiento de servicios no académicos que benefician la formación de su menor 
                        hijo(a).<o:p></o:p></span></p>
                        
                   <div id="Parrafo1General" runat="server">
                    <p class="MsoNormal" style="text-align: justify">
                        <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp;&nbsp;&nbsp; 
                        Pero queremos ir más allá y para ello se ha estructurado un plan de crecimiento 
                        constante en infraestructura y servicios universitarios que nos permitirá contar 
                        próximamente con mejores laboratorios, tecnología de punta y otros, que 
                        garantizarán continuar en la senda de la mejor formación universitaria en la 
                        región.<o:p></o:p></span></p>
                   </div>   
                   
                    <div id="Parrafo1EnfermeriaInternadoMedicinaOdontologia" runat="server">
                    <p class="MsoNormal" style="text-align: justify">
                        <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp;&nbsp;&nbsp; 
                        Pero queremos ir más allá, y para ello se ha estructurado un plan de crecimiento 
                        constante en infraestructura y de servicios universitarios que nos permitirá contar 
                        próximamente con los mejores laboratorios, tecnología de punta y otros, que
                        garantizarán y nos seguirán permitiendo continuar en la senda de la mejor formación universitaria en la región.<o:p></o:p></span></p>
                   </div>       
                    
                   <div id="Parrafo2General" runat="server"> 
                   <p class="MsoNormal" style="text-align: justify">
                    <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp;&nbsp;&nbsp; 
                   En ese sentido, desde el semestre 2015- I una nueva recategorización de pensiones entrará en vigencia, por lo que 
                       <span class="style2">el costo de crédito por ciclo académico</span> de su menor hijo(a) ascenderá a 
                       <asp:Label ID="txtCosto" runat="server" style="color: #FF0000; font-weight: 700" 
                           Text="0.00"></asp:Label>
&nbsp;nuevos soles.  Cabe destacar que el último ajuste de nuestras pensiones se llevó a cabo en el semestre 2010 – I y tienen nuestro compromiso que en adelante no registrarán variaciones, a menos que surja un imponderable económico debidamente justificado que afecte el costo de nuestro servicio. Para hacer el cálculo de su pensión le sugerimos leer el reglamento adjunto.<o:p></o:p></span></p>
                    </div>  
                    
                    <div id="Parrafo2Enfermeria" runat="server">  
                       <p class="MsoNormal" style="text-align: justify">
                           <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;;mso-bidi-font-family:Calibri;mso-fareast-language:ES-PE">&nbsp;&nbsp;&nbsp;&nbsp; Por ello, hemos considerado oportuno que para el 
                           semestre 2015 - I, la inversión de la pensión de su menor hijo (a) seguirá 
                           siendo la misma. Asimismo, aprovecho la oportunidad para comunicarle que a 
                           partir del semestre 2015 – II, una nueva recategorización de pensiones entrará 
                           en vigencia, de esta manera, el costo de crédito por ciclo académico de su menor 
                           hijo(a) ascenderá a 
                    <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;"> 
                       <asp:Label ID="txtCostoEnfermeria" runat="server" style="color: #FF0000; font-weight: 700" 
                           Text="0.00"></asp:Label>
&nbsp;</span>nuevos soles. <o:p></o:p></span>
                       </p>
                       <p class="MsoNormal" style="text-align: justify">
                           <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;;mso-bidi-font-family:Calibri;mso-fareast-language:ES-PE">&nbsp;&nbsp;&nbsp;&nbsp; Cabe destacar que el último ajuste de nuestras 
                           pensiones se llevó a cabo en el semestre 2010 – I y tienen nuestro compromiso 
                           que en adelante no registrarán variaciones, a menos que surja un imponderable 
                           económico debidamente justificado que afecte el costo de nuestro servicio. Para 
                           hacer el cálculo de su pensión a partir del semestre indicado le sugerimos leer 
                           el reglamento adjunto. <o:p></o:p></span>
                       </p>
                    </div>
                       
                     <div id="Parrafo2Internado" runat="server">   
                       <p class="MsoNormal" style="text-align: justify">
                            <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp;&nbsp;&nbsp; 
                            En ese sentido, desde el semestre 2015 - I una nueva recategorización de 
                            pensiones entrará en vigencia, sin embargo, la universidad ha considerado 
                            oportuno, en el caso de los estudiantes que se encuentran realizando su período 
                            de internado, que este grupo continúe con la pensión actual, como es el caso de 
                            su menor hijo(a).<o:p></o:p></span></p>                       
                    </div>
                   
                  
                    <div id="Parrafo2MedicinaOdontologia" runat="server">   
                       <p class="MsoNormal" style="text-align: justify">
                            <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp;&nbsp;&nbsp; 
                            En ese sentido, desde el semestre 2015 – I, la inversión en la pensión de su menor hijo(a) será de<span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:&quot;Times New Roman&quot;;mso-bidi-font-family:Calibri;mso-fareast-language:ES-PE"><span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;"> 
                       <asp:Label ID="txtCostoMedicinaOdontologia" runat="server" style="color: #FF0000; font-weight: 700" 
                           Text="0.00"></asp:Label>
&nbsp;</span></span>nuevos soles. Cabe destacar que el último ajuste de nuestras pensiones se llevó a cabo en el semestre 2010 – I y tienen nuestro compromiso que en adelante no registrarán variaciones, a menos que surja un imponderable económico debidamente justificado que afecte el costo de nuestro servicio.<o:p></o:p></span></p>
                       
                      
                    </div>
                    <div id="ParrafoInfo" runat="server">                    
                     <p class="MsoNormal" style="text-align: justify">
                            <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp;&nbsp;&nbsp; 
                            Para mayor información o dudas al respecto, pueden acercarse a la oficina de 
                            Caja y Pensiones en nuestro Campus Universitario, o a través del correo 
                            eletrónico
                            <a href="mailto:informacion@usat.edu.pe">
                            <span style="font-size:11.0pt;font-family:&quot;Calibri&quot;,&quot;sans-serif&quot;;mso-ascii-theme-font:minor-latin;mso-hansi-theme-font:minor-latin;text-decoration:none;text-underline:none" 
                                class="style5">informacion@usat.edu.pe</span></a> </span></p>
                        
                    </div>
                    
                    <p class="MsoNormal">
                            <span style="font-size:10.0pt;font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;">&nbsp;&nbsp;&nbsp;&nbsp; 
                            Al agradecer su atención a la presente, así como su confianza depositada en 
                            nosotros para la formación de sus hijos, hago propicia la oportunidad para 
                            expresarle las muestras de mi especial consideración.<o:p></o:p></span></p>
                        
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">                
                    <img alt="Firma" src="../images/firmacarta.png" 
                        style="width: 390px; height: 168px; " /></td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
           
     
                    <br />
           
     
        <asp:Button ID="btnAceptar" runat="server" Text="He leído y entendido el contenido del documento"  CssClass="btn" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button1" runat="server" Text="Cancelar" CssClass="btn" Visible="False" />
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
           
     
                    <br />
                    <span style="font-size:7.5pt;line-height:115%;
font-family:&quot;Humnst777 Lt BT&quot;,&quot;sans-serif&quot;;mso-fareast-font-family:Calibri;
mso-fareast-theme-font:minor-latin;mso-bidi-font-family:&quot;Times New Roman&quot;;
mso-bidi-theme-font:minor-bidi;color:white;mso-themecolor:background1;
mso-ansi-language:ES-PE;mso-fareast-language:EN-US;mso-bidi-language:AR-SA"><span class="style4">Av. 
                    San Josemaría Escrivá 855. Chiclayo - Perú. T: (074) 606200 - 606203</span><span 
                        class="style4" style="mso-spacerun:yes">&nbsp;&nbsp; </span><span class="style4">
                    www.usat.edu.pe</span><span class="style4" style="mso-spacerun:yes">&nbsp;&nbsp; </span>
                    </span></td>
            </tr>
        </table>
    </div>
    </center>
    </form>
</body>
</html>
