<%
on error resume next
codigo_usu=session("codigo_usu")
tipo_usu=session("tipo_usu")

if codigo_usu="" then codigo_usu=request.querystring("id")

    session("usu_biblioteca") = codigo_usu 'agregado el 05/06/2012
    session("Tusu_biblioteca")= tipo_usu

'response.Write codigo_usu & " - "
'response.Write tipo_usu

if codigo_usu<>"" then
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Bibliotecas</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />

    <script>
        function click() {
            if (event.button == 2) {
                alert('Esta opci�n se encuentra deshabilitada');
            }
        } 
    </script>

    <style type="text/css">
        .Estilo1
        {
            font-family: Verdana,Arial,Helvetica,sans-serif;
            font-weight: bold;
            color: #990000;
            font-size: 12px;
        }
        .Estilo2
        {
            font-family: Verdana, Arial, Helvetica, sans-serif;
        }
        .Estilo3
        {
            font-size: 12px;
        }
        .Estilo4
        {
            width: 472px;
        }
        .Estilo5
        {
            width: 71%;
            height: 109px;
        }
        .style3
        {
            width: 247px;
        }
        .style4
        {
            width: 311px;
        }
    </style>
</head>
<body>
    <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr class="azul">
            <td align="center">
                <h3>
                    Acceso a Revistas Digitales</h3>
            </td>
        </tr>
    </table>
    <table width="99%" border="1" align="center" cellpadding="5" cellspacing="0">
        <!--<tr>
    <td colspan="2" bgcolor="#FFCC00"><span class="Estilo1">BASES DE DATOS</span></td>
   </tr>-->
        <!-- ACEPRENSA 18 
   <tr valign="top">
    <td align="center" class="Estilo4">
     <a href="cuentaaccesos.asp?bib=18" target="_blank" class="Estilo2">
     <img src="../images/aceprensa.jpg" alt="aceprensa" width="280" border="1"  style="cursor:hand;" /></a>
     <br /><a class="rojo"><b>Modo de acceso :</b></a><br />Dentro del Campus Universitario
    </td>
    <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
     <span class="Estilo3"><strong class="azul">Aceprensa,</strong> es una agencia period�stica especializada 
      en el an�lisis de tendencias b�sicas de la sociedad, corrientes de pensamiento y estilos de vida.
      Selecciona temas que influyen decisivamente en la marcha de la sociedad. Aceprensa se dirige a personas 
      que est�n interesadas en contar con informaci�n para intervenir activamente en los debates de la opini�n 
      p�blica. Nuestro deseo es proporcionar a los suscriptores una fuente de datos e ideas que puedan 
      aprovechar en sus proyectos y contactos</span></p>
    </td>
   </tr>-->
        <!-- CIBERINDEX 23,24,25,26 -->
        <tr class="azul">
            <td colspan="2">
                <h3>
                    <br />
                    Multidisciplinaria</h3>
            </td>
        </tr>
        <!-- ProQuest 2 -->
        <!--  <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=2" target="_blank" class="Estilo2">
        <img src="../images/proquest_logo-186.jpg" alt="ProQuest" width="280" height="95" border="1" /></a></td>
       <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong class="azul">ProQuest�</strong> es un servicio global de 
           publicaciones electr�nicas. Se trata de uno de los mayores dep�sitos de contenidos 
           en l�nea de todo el mundo, y facilita una �nica plataforma integrada con acceso al 
           texto completo de miles de publicaciones, peri�dicos, diarios y revistas, adem�s de 
           incluir res�menes e �ndices detallados de otros tantos miles de publicaciones adicionales.<br><br>-->
        <!--<a class="usatenlace" href="http://www.etechwebsite.com/proquest1/Tutorial/pq_userguide_spanish.pdf" target="_blank">-->
        <!--<a class="usatenlace" href="https://intranet.usat.edu.pe/CAMPUSVIRTUAL/Biblioteca/userguide_np_en_es (2).pdf" target="_blank">
           <b class="rojo">&nbsp;&gt;&gt; Descargar Manual de Usuario</b></a></span></p>
       </td>
      </tr>-->
        <!--
	  <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=50" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/peruquiosco.png" alt="Per� Kiosko" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Per� Quiosco,</strong> acceso a la 
            versi�n digital de los diarios, por ejemplo: El Comercio, Gesti�n, Per� 21, 
            entre otros
            <br />
                      
        
            <br />
                              
        </span>
        <a class="usatenlace" href="../images/biblioteca/QuioscoPeru.pdf" 
        target="_blank">&nbsp;&gt;&gt; Descargar Tutorial<br />  
            <span class="Estilo3"><b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
        <b><a class="rojo">Contrase�a&nbsp;:</a></b>  123456<br />             
            </span>
            <br />
           </td>
      </tr>
	  -->
        <tr class="rojo">
            <td colspan="2">
                <h4>
                    <br />
                    Administraci�n de Empresas</h4>
            </td>
        </tr>
        <tr valign="top">
            <td align="center" class="style3">
                <a href="cuentaaccesos.asp?bib=67" target="_blank" class="Estilo2">
                    <img src="../images/biblioteca/ganamas-logo.jpg" alt="Ganam�s" width="280" height="95"
                        border="1" /></a>
            </td>
            <td align="left" valign="top" class="Estilo5">
                <p align="justify" class="Estilo2">
                    <span class="Estilo3"><strong class="azul">Ganam�s</strong> es una publicaci�n mensual
                        de informaci�n, an�lisis y opini�n para apoyar el crecimiento de los negocios y
                        motivar nuevos emprendimientos en el pa�s.</span>
                    <br />
                    <br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
                    <b><a class="rojo">Contrase�a&nbsp;:</a></b> biblioteca</p>
                    <a class="usatenlace" href="../../campusvirtual/Biblioteca/Manual Revista_RbDigital_GanaMas.pdf" target="_blank"> <!-- andy.diaz 23/06/2020 -->
                        <b class="rojo">&nbsp;&gt;&gt;Descargar Manual de Usuario</b>
                    </a>
            </td>
        </tr>
        <tr valign="top">
            <td align="center" class="style3">
                <a href="cuentaaccesos.asp?bib=68" target="_blank" class="Estilo2">
                    <img src="../images/biblioteca/HBR-logo.png" alt="Harvard" width="280" height="95"
                        border="1" /></a>
            </td>
            <td align="left" valign="top" class="Estilo5">
                <p align="justify" class="Estilo2">
                    <span class="Estilo3"><strong class="azul">Harvard Business Review (HBR)</strong> es
                        una de las revistas m�s influyentes en el mundo sobre, entre otros, temas relacionados
                        con el management, la direcci�n de empresas, la innovaci�n y la globalizaci�n. Pensada
                        para servir de puente entre el mundo acad�mico y el empresarial, HBR re�ne en sus
                        p�ginas a reconocidos pensadores as� como responsables empresariales. </span>
                    <br />
                    <br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
                    <b><a class="rojo">Contrase�a&nbsp;:</a></b> UMogrovejo1!<br /></p>
                    <a class="usatenlace" href="../../campusvirtual/Biblioteca/Manual Revista_RbDigital_HarvardBusinessReview.pdf" target="_blank"> <!-- andy.diaz 23/06/2020 -->
                        <b class="rojo">&nbsp;&gt;&gt;Descargar Manual de Usuario</b>
                    </a>
            </td>
        </tr>
        <tr class="rojo">
            <td colspan="2">
                <h4>
                    <br a />
                    Arquitectura</h4>
            </td>
        </tr>
        <!-- <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=39" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/tectonica.jpg" alt="Revista Tect�nica Online" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Tect�nica online</strong> Permite un an�lisis de la arquitectura, la tecnolog�a y la construcci�n a trav�s de nuevos art�culos, nuevos an�lisis de proyectos y una constante b�squeda y selecci�n de materiales, productos y sistemas constructivos.
        <br><br />
        <a class="usatenlace" href="../images/biblioteca/Revista-de Tectonica-online.pdf" 
        target="_blank"><b class="rojo">&nbsp;&gt;&gt; Descargar Gu�a de Usuario</b></a><br />    
       </td>
   </tr>  -->
        <!-- RBdigital 2 -->
        <tr valign="top">
            <td align="center" class="style3">
                <a href="cuentaaccesos.asp?bib=57" target="_blank" class="Estilo2">
                    <img src="../images/biblioteca/elcroquis.jpg" alt="El Croquis" width="280" height="95"
                        border="1" /></a>
            </td>
            <td align="left" valign="top" class="Estilo5">
                <p align="justify" class="Estilo2">
                    <span class="Estilo3"><strong class="azul">El Croquis</strong> es una revista trimestral
                        de arquitectura, que presenta en n�meros monogr�ficos las obras y proyectos m�s
                        significativos de los arquitectos contempor�neos de mayor inter�s.
                        <br />
                        Para Consultas fuera del campus universitario:</span><br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> USAT<br />
                    <b><a class="rojo">Contrase�a&nbsp;:</a></b> USAT                  
                    <a class="usatenlace" href="../../campusvirtual/Biblioteca/Manual Revista_RbDigital_El_Croquis.pdf"
                        target="_blank">                        
                        <br />
                        <br />
                        <b class="rojo">&nbsp;&gt;&gt;Descargar Manual de Usuario</b></a></p>
            </td>
        </tr>
        <tr valign="top">
            <td align="center" class="style3">
                <a href="cuentaaccesos.asp?bib=65" target="_blank" class="Estilo2">
                    <img src="../images/biblioteca/summa-revista.jpg" alt="Summa" width="280" height="95"
                        border="1" /></a>
            </td>
            <td align="left" valign="top" class="Estilo5">
                <p align="justify" class="Estilo2">
                    <span class="Estilo3"><strong class="azul">Summa+</strong> es una revista de arquitectura
                        y dise�o dirigida hacia el profesionalismo y la tecnolog�a de construcci�n y las
                        pr�cticas del dise�o que permiten la realizaci�n eficiente de un n�mero cada vez
                        mayor de formas y sistemas constructivos, adaptados o adaptables a gran variedad
                        de programas y circunstancias</span>.
                    <br />
                    <br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
                    <b><a class="rojo">Contrase�a&nbsp;:</a></b>&nbsp;12345678<br />
                    <a class="usatenlace" href="../../campusvirtual/Biblioteca/Manual_Revista_RbDigital_Summa.pdf"
                        target="_blank">                        
                        <br />
                        <b class="rojo">&nbsp;&gt;&gt;Descargar Manual de Usuario</b></a>
                </p>
            </td>
        </tr>
        <tr valign="top">
            <td align="center" class="style3">
                <a href="cuentaaccesos.asp?bib=71" target="_blank" class="Estilo2">
                    <img src="../images/biblioteca/AR-logo.jpg" alt="Architectural Review" width="280" height="120" border="1" /></a>
            </td>
            <td align="left" valign="top" class="Estilo5">
                <p align="justify" class="Estilo2">
                    <span class="Estilo3"><strong class="azul">Architectural Review</strong> es la revista de arquitectura verdaderamente global, que ofrece una
                        cobertura diversa e incisiva, que abarca todas las partes del mundo. Con una historia de 120 a�os como la publicaci�n de arquitectura favorita del mundo, es l�der
                        de debates y una herramienta imprescindible para las comunidades de arquitectura y dise�o de todo el mundo.</span>.
                    <br />
                    <br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
                    <b><a class="rojo">Contrase�a&nbsp;:</a></b>&nbsp;bibliotecausat<br />
                    <a class="usatenlace" href="../../campusvirtual/Biblioteca/Manual Revista_RbDigital_ArchitecturalReview.pdf" target="_blank"> <!-- andy.diaz 25/06/2020 -->                        
                        <br />
                        <b class="rojo">&nbsp;&gt;&gt;Descargar Manual de Usuario</b></a>
                </p>
            </td>
        </tr>
        <!--   
   <tr class="rojo">
    <td colspan="2"><h5><br />Ingenier�a</h5></td>
   </tr>
   <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=55" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/VirtualPROlogo.png" alt="Virtual PRO" width="199px" height="70px" border="1"  style="cursor:hand" /></a>
		 <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Virtual Pro</strong> Presenta diferentes temas de inter�s en el �rea de ingenier�a agron�mica, ingenier�a ambiental, ingenier�a industrial, qu�mica, administraci�n de empresas, entre otras. Se especializa en procesos industriales.
        <br><br />
         
       </td>
   </tr>-->
        <!-- <tr class="rojo">
    <td colspan="2"><h5><br />Derecho</h5></td>
   </tr>
      <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=33" target="_blank" class="Estilo2">
        <img src="../images/vlexlogo.png" alt="vLex" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <br /><a class="rojo"><!--<b>Modo de acceso :</b></a><br />Dentro del Campus Universitario</td>
       <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong class="azul">vLex,</strong> es una interfaz  que brinda sobre la jurisprudencia 
        del Per� y del mundo.<br />Permite acceder de forma ilimitada a todos los contenidos de Derecho a texto completo:<br>
        <br />
        <u>
            <li>Contratos</li>
            <li>Jurisprudencia</li>
            <li>Legislaci�n</li>
            <li>Diarios</li>
        </u>
        <br /><br />
        <a class="usatenlace" href="http://www.youtube.com/embed/Ti6drooBpME" 
        target="_blank"><b class="rojo">&nbsp;&gt;&gt; Ver Video Tutorial</b></a><br /><br />
        <a class="usatenlace" href="remote_auth.asp" target="_blank"><b class="rojo">&nbsp;&gt;&gt; B�squeda Avanzada</b></a>
        </span></p>
       </td>
      </tr> -->
        <tr class="rojo">
            <td colspan="2">
                <h4>
                    <br />
                    Econom�a</h4>
            </td>
        </tr>
        <tr valign="top">
            <td align="center" class="style3">
                <a href="cuentaaccesos.asp?bib=66" target="_blank" class="Estilo2">
                    <img src="../images/biblioteca/americaeco-logo.png" alt="America Economia" width="280"
                        height="95" border="1" /></a>
            </td>
            <td align="left" valign="top" class="Estilo5">
                <p align="justify" class="Estilo2">
                    <span class="Estilo3"><strong class="azul">Am�rica Econom�a Internacional y Am�rica
                        Econom�a Per�</strong> es una revista l�der en noticias y an�lisis sobre negocios
                        en Am�rica Latina, se ha consolidado como la fuente de informaci�n preferida por
                        los influyentes de esta parte del mundo. Su equipo period�stico est� dedicado a
                        descubrir las tendencias en los negocios y a investigar los distintos aspectos que
                        conforman el ambiente comercial en Latinoam�rica.
                        <br />
                        Para Consultas fuera del campus universitario:</span><br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
                    <b><a class="rojo">Contrase�a&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
                    <a class="usatenlace" href="../../campusvirtual/Biblioteca/Manual_Revista_RbDigital_AmericaEconomia.pdf"
                        target="_blank">                        
                        <br />
                        <b class="rojo">&nbsp;&gt;&gt;Descargar Manual de Usuario</b></a>
            </td>
        </tr>
        <tr valign="top">
            <td align="center" class="style4">
                <a href="cuentaaccesos.asp?bib=69" target="_blank" class="Estilo2">
                    <img src="../images/biblioteca/naffairs-logo.jpg" alt="Foreign Affairs" width="280"
                        height="95" border="1" /></a>
            </td>
            <td align="left" valign="top" class="Estilo5">
                <p align="justify" class="Estilo2">
                    <span class="Estilo3"><strong class="azul">Foreign Affairs</strong> es una revista que
                        se caracteriza por ofrecer un espacio abierto para la discusi�n de temas actuales
                        de car�cter internacional, de gran inter�s para el mundo y, en particular, para
                        Am�rica Latina. Foreign Affairs Latinoam�rica se ha posicionado como un influyente
                        foro que refleja el pensamiento iberoamericano sobre el mundo, as� como la visi�n
                        mundial sobre Latinoam�rica, privilegiando la diversidad de enfoques y la cr�tica
                        del m�s alto nivel.</span>
                    <br />
                    <br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> unicato42<br />
                    <b><a class="rojo">Contrase�a&nbsp;:</a></b> dacabio03</p>
                    <a class="usatenlace" href="../../campusvirtual/Biblioteca/Manual Revista_RbDigital_ForeignAffairs.pdf" target="_blank"> <!-- andy.diaz 23/06/2020 -->
                        <b class="rojo">&nbsp;&gt;&gt;Descargar Manual de Usuario</b>
                    </a>
            </td>
        </tr>
        <!--<tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=48" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/The_economist.png" alt="El Economista" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">The Economist,</strong> es una revista que estudia, analiza y comenta los acontecimientos relacionados con la pol�tica mundial, el mundo de la econom�a, los negocios y las finanzas.<br />                             
        </span>
            <br />
            <span class="Estilo3"><b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
        <b><a class="rojo">Contrase�a&nbsp;:</a></b> biblioteca<br />             
            </span>
            <br />
           </td>
      </tr>-->
        <!-- <tr class="rojo">
    <td colspan="2"><h5><br />Enfermer�a</h5></td>
   </tr>
    <tr valign="top">
    <td align="center" class="style3">
      <img src="../images/CIBERINDEX.jpg" alt="ciberindex" width="280" border="1" style="cursor:hand;" />     
    </td>
    <td align="left" valign="top" class="Estilo5"><p align="justify">
     <span class="Estilo3"><strong class="azul">CIBERINDEX ,</strong> es una plataforma especializada 
     en la gesti�n del conocimiento en cuidados de salud que tiene como misi�n proporcionar a 
     profesionales e instituciones de cualquier �mbito soluciones pr�cticas e innovadoras para la ayuda 
     en la toma de decisiones fundamentadas en el conocimiento cient�fico.<br />
</span>
        <p align="justify">
            Consultas dentro del campus universitario:<span class="Estilo3"><u><li><a href="cuentaaccesos.asp?bib=23" target=_blank>HEMEROTECA CANT�RIDA (Dentro del Campus Universitario)</a></li>
     <li><a href="cuentaaccesos.asp?bib=24" target=_blank>CUIDEN EVIDENCIA (Dentro del Campus Universitario)</a></li>
     <li><a href="cuentaaccesos.asp?bib=25" target=_blank>CUIDEN PLUS (Dentro y fuera del Campus Universitario)</a></li>
     <li><a href="cuentaaccesos.asp?bib=26" target=_blank>SUMMA CUIDEN (Dentro del Campus Universitario)</a></li>
     </u>
</span></p>
    </td>
   </tr>-->
        <tr class="rojo">
            <td colspan="2">
                <h4>
                    <br />
                    Filosof�a</h4>
            </td>
        </tr>
        <tr valign="top">
            <td align="center" class="style3">
                <a href="cuentaaccesos.asp?bib=54" target="_blank" class="Estilo2">
                    <img src="../images/biblioteca/anuariofi.png" alt="AnuarioFilosofico" width="280"
                        height="95" border="1" style="cursor: hand" /></a>
            </td>
            <td align="left" valign="top" class="Estilo5">
                <p align="justify" class="Estilo2">
                    <span class="Estilo3"><strong class="azul">Anuario Filos�fico</strong> es una revista
                        del Departamento de Filosof�a de la Universidad de Navarra, dirigido a especialistas
                        en la investigaci�n filos�fica, profesores y estudiantes de filosof�a y humanidades.
                        Incluye art�culos de las diversas �reas de la filosof�a y rese�as de libros de actualidad.
                        <br />
                        Para Consultas fuera del campus universitario:</span><br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> mogrovejo<br />
                    <b><a class="rojo">Contrase�a&nbsp;:</a></b> biblioteca <a class="usatenlace" href="../../campusvirtual/Biblioteca/ANUARIO FILOSOFICO.pdf"
                        target="_blank">
                        <br />
                        <br />
                        <b class="rojo">&nbsp;&gt;&gt;Descargar Manual de Usuario</b></a></p>
            </td>
        </tr>
        <tr valign="top">
            <td align="center" class="style3">
                <a href="cuentaaccesos.asp?bib=62" target="_blank" class="Estilo2">
                    <img src="../images/biblioteca/Scripta.jpg" alt="Scripta Theologica" width="280"
                        height="95" border="1" style="cursor: hand" /></a>
            </td>
            <td align="left" valign="top" class="Estilo5">
                <p align="justify" class="Estilo2">
                    <span class="Estilo3"><strong class="azul">Scripta Theologica</strong> es una revista
                        de teolog�a de la Universidad de Navarra, editada por la Facultad de Teolog�a, publica
                        art�culos y revisiones bibliogr�ficas de teolog�a b�blica y sistem�tica, de patrolog�a
                        y de liturgia. Promueve un enfoque interdisciplinar y presta atenci�n a los temas
                        teol�gicos de actualidad.
                        <br />
                        Para Consultas fuera del campus universitario:</span><br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> mogrovejo<br />
                    <b><a class="rojo">Contrase�a&nbsp;:</a></b> biblioteca <a class="usatenlace" href="../../campusvirtual/Biblioteca/Manual SCRIPTA THEOLOGICA.pdf"
                        target="_blank">
                        <br />
                        <br />
                        <b class="rojo">&nbsp;&gt;&gt;Descargar Manual de Usuario</b></a></p>
            </td>
        </tr>
        <!--<tr class="rojo">
    <td colspan="2"><h5><br />Derecho</h5></td>
   </tr>   
     <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=56" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/personaderecho.jpg" alt="PersonaDerecho" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Persona y Derecho,</strong> revista que ofrece estudios sobre pensamiento jur�dico, pol�tico y social, con particular atenci�n a los derechos humanos. De utilidad para juristas e interesados en problemas jur�dicos, se dirige especialmente a investigadores en Filosof�a del Derecho, Derechos Humanos y Filosof�a.</span>       
        <br>Para Consultas fuera del campus universitario:</br>
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> mogrovejo<br />
        <b><a class="rojo">Contrase�a&nbsp;:</a></b> biblioteca
		<br>
		<br>
		<a class="usatenlace" href="https://intranet.usat.edu.pe/campusvirtual/Biblioteca/MANUAL PERSONA Y DERECHO.pdf" target="_blank">
           <b class="rojo">&nbsp;&gt;&gt;Descargar Manual de Usuario</b></a></span></p>
       </td>
      </tr>-->
        <!--
   <tr class="rojo">
    <td colspan="2"><h5><br />Ingenier�a Civil Ambiental</h5></td>
   </tr>
     <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=51" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/TERRAMAR.png" alt="Instituto Terramar" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
            <span class="Estilo3"><strong style="color:azul">Instituto Terramar,</strong> el catalogo normativo del Instituto Terramar compendia 
            las Normas T�cnicas y administrativas necesarias para dise�ar y construir proyectos de habilitaci�n urbana y/o edificaci�n, 
            as� como tambi�n, la oferta disponible de materiales y acabados para la construcci�n de los mismos.
            <br />                             
            <br />                              
        </span>
        <a class="usatenlace" href="../images/biblioteca/TERRAMAR.pdf" 
        target="_blank">&nbsp;&gt;&gt; Descargar Tutorial</a><br />
            <span class="Estilo3"><b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> usat<br />
        <b><a class="rojo">Contrase�a&nbsp;:</a></b>  hlima759@<br />             
            </span>
            <br />
           </td>
      </tr>-->
        <!--<tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=46" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/elEcologista.png" alt="El Ecologista" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Revista El Ecologista,</strong> La revista El Ecologista es una publicaci�n de informaci�n ambiental. Trata los 
               temas del clima, la biodiversidad, la ingenier�a gen�tica, los bosques, la 
               energ�a, la contaminaci�n, los residuos, el transporte, los recursos naturales y 
               la globalizaci�n. 
            <br />
        </span>
            <br />
            
            <b class="rojo">
            
        <a class="usatenlace" href="../images/biblioteca/Tutorial_Ecologista.pdf" 
        target="_blank">&nbsp;&gt;&gt; Descargar Tutorial</a></b><br />  
            <br />      
       </td>
      </tr>-->
        <!-- <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=49" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/industriambiente.png" alt="Revista IndustriAmbiente" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Revista IndustriAmbiente, </strong>
            Es una publicaci�n cient�fico-t�cnica que trata temas relacionados con el medio 
            ambiente, gesti�n de residuos, energ�as renovables, reciclaje, aguas residuales,&nbsp; 
            tecnolog�as limpias,&nbsp; residuos l�quidos, gaseosos, t�xicos y peligrosos. <br />
            <br />
                            
        </span>
        
        <a class="usatenlace" href="../images/biblioteca/Revista_IndustriAmbiente.pdf" 
        target="_blank">&nbsp;&gt;&gt; Descargar Tutorial</a></b><br />  
            <span class="Estilo3"><b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
        <b><a class="rojo">Contrase�a&nbsp;:</a> </b>os10713us</span><br />
           </td>
      </tr>-->
        <!--
     <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=47" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/Revista_PQ.png" alt="PQ Revista" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="style2"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Proyectos Qu�micos,</strong> es una revista que aborda los temas de ingenier�as, industria, aguas y suelos, s�lidos y pulverulentos.</span><br />
            <br />
            
         <a class="usatenlace" href="../images/biblioteca/Tutorial_Revista_Quimica.pdf" 
        target="_blank">&nbsp;&gt;&gt; Descargar Tutorial</a></b><br />  
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> 
            <a href="https://intranet.usat.edu.pe/OWA/redir.aspx?URL=mailto%3abiblioservicios%40usat.edu.pe" 
                style="font-family: Arial, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 19.2000007629395px; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px;">
            biblioservicios@usat.edu.pe</a> <br />
            <span style="color: rgb(0, 0, 0); font-family: Arial, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 19.2000007629395px; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;">
            <b><a class="rojo">Contrase�a&nbsp;&nbsp; :</a> </b>kgxvya52</span></td>
            
  
      </tr>-->
        <tr class="rojo">
            <td colspan="2">
                <h4>
                    <br />
                    Ingenier�a Industrial</h4>
            </td>
        </tr>
        <!--DYNA 19 -->
        <tr valign="top">
            <td align="center" class="style3">
                <a href="cuentaaccesos.asp?bib=19" target="_blank" class="Estilo2">
                    <img src="../images/DYNA.jpg" alt="Revista DYNA" width="280" border="1" style="cursor: hand;
                        height: 128px;" /></a>
                <!--<a class="rojo"><b>Modo de acceso :</b></a><br />Dentro del Campus Universitario-->
            </td>
            <td align="left" valign="top" class="Estilo5">
                <p align="justify" class="Estilo2">
                    <span class="Estilo3"><strong class="azul">DYNA</strong> es la revista t�cnica industrial
                        pionera de Espa�a y es el �rgano oficial de Ciencia y Tecnolog�a de la Federaci�n
                        de Asociaciones de Ingenieros Industriales de Espa�a (FAIIE). Considerada como revista
                        de divulgaci�n t�cnico-cient�fica Indexada en Science Citation Index Expanded. Contiene
                        gran variedad de temas que van desde los puramente t�cnicos, hasta aspectos de la
                        actualidad, como organizaci�n industrial, desarrollo sostenible, responsabilidad
                        corporativa, enfermedad profesional, innovaci�n, etc.
                        <br />
                        Para Consultas fuera del campus universitario:</span>
                    <br />
                    <a style="font-weight: bold">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a>&nbsp;facha200
                    <br />
                    <a style="font-weight: bold">Contrase�a :</a> 325527100
                </p>
                <a class="usatenlace" href="../../campusvirtual/Biblioteca/MAnual Revista DYNA.pdf"
                    target="_blank"><b class="rojo">&nbsp;&gt;&gt;Descargar Manual de Usuario</b></a>
            </td>
        </tr>
        <tr valign="top">
            <td align="center" class="style3">
                <a href="cuentaaccesos.asp?bib=70" target="_blank" class="Estilo2">
                    <img src="../images/biblioteca/agronoticias-logo.png" alt="Agronoticias" width="280"
                        border="1" style="cursor: hand; height: 128px;" /></a>
                <!--<a class="rojo"><b>Modo de acceso :</b></a><br />Dentro del Campus Universitario-->
            </td>
            <td align="left" valign="top" class="Estilo5">
                <p align="justify" class="Estilo2">
                    <span class="Estilo3"><strong class="azul">Agronoticias</strong> es la primera revista
                        peruana especializada en agricultura, ganader�a, forestaci�n, medio ambiente y desarrollo
                        rural.
                        <br />
                        <br />
                        Descargar las revistas de marzo, abril y mayo:</span>
                    <br />
                </p>
                <a class="usatenlace" href="https://we.tl/t-tMkDc9RsYP" target="_blank"><b class="azul">
                    &nbsp;&gt;&gt;https://we.tl/t-tMkDc9RsYP</b></a>
            </td>
        </tr>
        <!--<tr class="rojo">
    <td colspan="2"><h5><br />Medicina</h5></td>
   </tr>
      <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=37" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/UPTODATE.png" alt="Uptodate" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">UpToDate,</strong> es un recurso de informaci�n m�dica sintetizada basada en evidencia.</span>        
        <br>
        <br />             
       </td>
      </tr>-->
        <!-- <tr class="rojo">
             <td colspan="2"><h5><br />Psicolog�a</h5></td>
   </tr>
         PSICODOC 20 
      <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=20" target="_blank" class="Estilo2">
        <img src="../images/psicodoc.jpg" alt="PSICODOC" width="280" height="95" border="1" /></a>
       <!--<a href="http://www.psicodoc.org/clientesurl.htm" target="_blank">Cliente Psicodoc</a>-
       </td>
       <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong class="azul">PSICODOC</strong> es una interfaz que facilita 
        la b�squeda bibliogr�fica y el acceso al texto completo de las publicaciones cient�ficas 
        sobre Psicolog�a y otras disciplinas afines.<br />
           <b><br />
           Descargar : <a href= "../Biblioteca/PSICODOC PRESENTACION.pdf" target=_blank>Presentaci�n de servicios
            </a><br />
           Descargar : <a href="../Biblioteca/PSICODOC_BUSQUEDA.PDF.pdf" target=_blank>Tutorial de b�squedas</a>
           </b>
           <br />___________________________________<br /><br />
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> unisatom <br />
        <b><a class="rojo">Contrase�a&nbsp;:</a></b> 730500
           </span></p>
       </td>
      </tr>-->
        <tr class="rojo">
            <td colspan="2">
                <h4>
                    <br />
                    Tecnolog�as de la Informaci�n</h4>
            </td>
        </tr>
        <tr valign="top">
            <td align="center" class="style3">
                <a href="cuentaaccesos.asp?bib=17" target="_blank" class="Estilo2">
                    <img src="../images/logproinf.jpg" alt="El Profesional de la Informaci�n" width="280"
                        border="1" style="cursor: hand;" /></a>
            </td>
            <td align="left" valign="top" class="Estilo5">
                <p align="justify" class="Estilo2">
                    <span class="Estilo3"><strong class="azul">El Profesional de la Informaci�n</strong>
                        es una revista electr�nica internacional sobre Documentaci�n, Bibliotecolog�a, Comunicaci�n
                        y Nuevas Tecnolog�as de la informaci�n. Indexada por ISI Social Science Citation
                        Index, Scopus y otras bases de datos.<br />
                        <br />
                        <!--<b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> quiroztequen<br />
       <b><a class="rojo">Contrase�a&nbsp;:</a></b> usat2011-->
                        <a class="usatenlace" href="../../campusvirtual/Biblioteca/MANUAL EL PROFESIONAL DE LA INFORMACION.pdf"
                            target="_blank"><b class="rojo">&nbsp;&gt;&gt;Descargar Manual de Usuario</b></a></span></p>                
            </td>
        </tr>
        <!--E-LIBRO 16 -->
        <!--
   <tr valign="top">
    <td align="center" class="Estilo4">
     <a href="cuentaaccesos.asp?bib=16" target="_blank" class="Estilo2">
     <img src="../images/e-libro-300x120.jpg" alt="e-libro" width="280" border="1"  style="cursor:hand" /></a>
     <p style="text-align:left"><b><a class="rojo">Importante</a></b><br /><br />Antes de 
      comenzar a usar la plataforma e-libro, verificar si tiene instalado Java en su computador
      <br /><br />verificar en: <b>&nbsp;<a href="http://www.java.com/es/" target="_blank"><u>www.java.com</u></a></b>
      <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; opcion: <b>&nbsp;'�Tengo Java?'</b></p>
    </td>
    <td align="left" valign="top" class="style6"><p align="justify" class="Estilo2">
     <span class="Estilo3"><strong class="azul">
      E-LIBRO,</strong> es una bases de libros electr�nicos que contine m�s de 38,000 
      t�tulos en espa�ol; 3700 t�tulos en portugu�s y 1050 mapas. Con actualizaci�n de 
      contenidos semanal y un crecimiento del 20% mensual. En e-libro tambi�n 
      encontrar� un amplio n�mero de documentos en espa�ol de las universidades.<br /><br />
      <b>COLECCIONES ACAD�MICAS</b>
      <UL>
       <li>Bellas artes, artes visuales y ciencias semi�ticas.
       <li>Ciencias sociales.
       <li>Arquitectura, urbanismo y dise�o.
       <li>Ciencias econ�micas y administrativas.
       <li>Ciencias de la salud.
       <li>Psicolog�a.
       <li>Ciencias exactas y naturales.
       <li>Ciencias biol�gicas, veterinarias y silvoagropecuarias.
       <li>Ingenier�as y tecnolog�a.
       <li>Inform�tica, computaci�n y telecomunicaciones.
       <li>Ciencias de la informaci�n y de la comunicaci�n.
       <li>Inter�s general: Autoayuda y espiritualidad, biograf�as, actualidad, deportes, entretenimiento, etc.
      </UL></p>
     </td>
    </tr>
    -->
        <!-- EL PROFESIONAL DE LA INFORMACI�N 17 -->
        <!-- ESMERALD 27 -->
        <!--
    <tr valign="top">
     <td align="center" class="Estilo4">
      <a href="cuentaaccesos.asp?bib=27" target="_blank" class="Estilo2">
      <img src="../images/emerald.png" alt="El Profesional de la Informaci�n" width="280" border="1"  style="cursor:hand;" /></a>
     </td>
     <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
      <span class="Estilo3"><strong class="azul">EMERALD,</strong> 
       es una base de datos que brinda acceso a art�culos cient�ficos, libros, etc. 
       Incluye las bases de datos: <BR />Emerald Management eJournals (�reas tem�ticas de gesti�n, 
       como Ciencias Empresariales, Comercio Internacional, Contabilidad y Finanzas, Derecho y 
       �tica Empresarial, Econom�a, Empresa e Innovaci�n, Marketing) <br /><br />
       Emerald Engineering eJournal Collection (�reas tem�ticas de Ingenier�a, Ciencia de los Materiales y
        Tecnolog�a).<br /><br />
       <b><a class="usatenlace" href="http://www.emeraldinsight.com/help/librarian/demos/spanish/Beta_Robo_Demo_Spanish.htm
" target="_blank">-&gt; Video Tutorial</a></b><br /><br />
       <b><a class="rojo">Modo de prueba&nbsp;:</a></b>(Desde el 14 de mayo hasta el 14 de junio)
      </span></p>
     </td>
    </tr>
    -->
        <!--Harrison online 28 -->
        <!--
      <tr valign="top">
       <td align="center" class="style9">
        <a href="cuentaaccesos.asp?bib=28" target="_blank" class="Estilo2">
         <img src="../images/HARRISON_ONLINE.png" alt="Harrison online" width="280" border="1"  style="cursor:hand;" /></a>
        
       </td>
       <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong class="azul">Harrison online</strong> es un  texto b�sico 
        de la Medicina ahora en l�nea y con actualizaciones diarias.<br /><br />
        <b>Incluye:</b>
      <UL>
       <li>Autoevaluaci�n: Con mas de 800 preguntas
       <li>Im�genes de Medicina de Urgencias: auxiliares en la realizaci�n de diagn�sticos visuales.
       <li>Actualizaciones: Que incorpora un historial con las actualizaciones que se han hecho a 
           partir de noviembre de 2004 del Harrison. 
       <li>Grand rounds re�ne conferencias multimedia: sobre aspectos novedosos de enfermedades que
           d�a a d�a atacan con m�s fuerza a la poblaci�n mundial.
      </UL>
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> univcstdm<br />
       <b><a class="rojo">Contrase�a&nbsp;:</a></b> medicine<br /><br />
        <b><a class="rojo">Modo de prueba&nbsp;:</a></b>(Desde el 18 de mayo hasta el 10 de junio)
       </td>
      </tr>
    -->
        <!-- HINARI 3 -->
        <!--   <tr valign="top">
     <td align="center" class="style3">
      <a href="cuentaaccesos.asp?bib=3" target="_blank" class="Estilo2">
       <img src="../images/logohinari.jpg" alt="Hinari" width="280" height="95" border="1"  style="cursor:hand" /></a></td>
     <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
      <span class="Estilo3"><strong class="azul">
       HINARI,</strong> tiene como objeto ofrecer acceso al mayor n�mero de revistas de 
       biomedicina y otros temas en el campo de las ciencias sociales. HINARI ofrece al 
       usuario una interfaz simple que sirve como portal de acceso al texto completo de 
       art�culos de revistas de las Editoriales Asociadas. Los usuarios de HINARI 
       pueden buscar y tener acceso a art�culos a texto completo disponibles 
       directamente desde la base de datos PubMed (Medline).</span></p>
	   
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> PER098<br />
        <b><a class="rojo">Contrase�a&nbsp;:</a></b> 62701</a><br />             
     </td>
    </tr>-->
        <!-- IOPscience  35 -->
        <!--<tr valign="top">
     <td align="center" class="Estilo4">
      <a href="cuentaaccesos.asp?bib=35" target="_blank" class="Estilo2">
       <img src="../images/IOPSCIENCE.jpg" alt="IOPScience" width="280" height="95" border="1"  style="cursor:hand" /></a></td>
     <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
      <span class="Estilo3"><strong class="azul">
       IOPscience,</strong> es un servicio online para poder acceder al contenido de las revistas publicadas por el IOP.<br />
       El contenido ha sido organizado mediante m�s de 6000 materias clasificadas utilizando PACS y MSC, especializadas en f�sicas, astronom�a y matem�ticas.
       <br /><br />
       <a class="usatenlace" href=" http://iopscience.iop.org/onlinetour?tab=tour3&locale=es" target="_blank"><b class="rojo">&nbsp;&gt;&gt; Video Tutorial</b></a>
       <br /><br />
       <b>Modo de Prueba:</b> Hasta el 01 de Julio
</span></p>
     </td>
    </tr>-->
        <!--Multilegis 13 -->
        <!--
     <tr valign="top">
      <td align="center" class="Estilo4">
       <a href="cuentaaccesos.asp?bib=13" target="_blank" class="Estilo2">
         <img src="../images/multilegis.jpg" alt="multilegis" width="280" border="1"  style="cursor:hand;" /></a>
        <br /><a class="rojo"><b>Modo de acceso :</b></a><br />Dentro del Campus Universitario
       </td>
       <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong class="azul">Multilegis.</strong> Legis naci� en 1952 
           como respuesta a la necesidad de compilar y hacer comprensible toda la 
           legislaci�n econ�mica oficial del pa�s. <br />
           Para cumplir con este prop�sito, introdujo su revolucionario sistema que gracias 
           a su permanente tecnol�gica y al convenio con Universidad Cat�lica Santo Toribio 
           de Mogrovejo, presenta un completo y actualizado sistema de informaci�n jur�dica 
           en l�nea llamado Multilegis.<br />
           Multilegis cuenta con pr�cticos buscadores de tecnolog�a avanzada y con 
           herramientas que proporcionan un manejo sencillo y eficaz de texto.<br /></span></p>
       </td>
      </tr>
      -->
        <!-- REVISTA DE EDUCACI�N usat -->
        <!-- <tr valign="top">
       <td align="center" class="Estilo4">
       <a class="Estilo2">
        <img src="../images/revista_educacion.png" alt="Revista de Educaci�n" width="280" height="95" border="1" /></a></td>
       <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong class="azul">Revista de Educaci�n</strong> es una publicaci�n 
        cient�fica del Ministerio de Educaci�n, Cultura y Deporte espa�ol.
        <br />___________________________________<br /><br />
        <b><a class="rojo">N�meros</a></b><br /><br />
            <b>N� 357</b>&nbsp;&nbsp;Ene<b>/</b>Abr&nbsp;2012&nbsp;&nbsp;&nbsp;&nbsp;>>>&nbsp;&nbsp;
            <a href="cuentaaccesos.asp?bib=29" class="azul" target="_blank"><b>PDF</b></a>
            <br />
            <b>N� 358</b>&nbsp;&nbsp;May<b>/</b>Ago&nbsp;2012&nbsp;&nbsp;&nbsp;>>>&nbsp;&nbsp;
            <a href="cuentaaccesos.asp?bib=30" class="azul" target="_blank"><b>PDF</b></a>
            <br />
            <b>N� 359</b>&nbsp;&nbsp;Set<b>/</b>Dic&nbsp;2012&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;>>>&nbsp;&nbsp;
            <a href="cuentaaccesos.asp?bib=31" class="azul" target="_blank"><b>PDF</b></a>
            <br />
            <b>N� 360</b>&nbsp;&nbsp;Ene<b>/</b>Abr&nbsp;2013&nbsp;&nbsp;&nbsp;&nbsp;>>>&nbsp;&nbsp;
            <a href="cuentaaccesos.asp?bib=32" class="azul" target="_blank"><b>PDF</b></a> -->
        <!--<a href="../images/357_educacion.pdf" class="azul" target="_blank"><b>PDF</b></a>-->
        <!-- </span></p>
       </td>
      </tr> -->
        <!--SCIENCE DIRECT 6 -->
        <!--
      <tr valign="top">
       <td align="center" class="Estilo4">
       <a href="cuentaaccesos.asp?bib=6" target="_blank" class="Estilo2">
        <img src="../images/logoElsevier.jpg" alt="Science Direct" width="280" height="95" border="1"  style="cursor:hand" /></a></td>
       <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong class="azul">Science Direct,</strong> es una edici�n biom�dica y el m�s importante proveedor de informaci�n
         cient�fico-m�dica.<br><br>
           <b><a class="rojo">Habilitado hasta&nbsp;:&nbsp;</a></b>30 de Agosto de 2013
           <br><br>
	   -->
        <!--<a class="usatenlace" href="http://www.biblioteca.udep.edu.pe/wp-content/uploads/2011/11/ScienceDirect-User-Guide_ESP.pdf
" target="_blank"><b class="rojo">&nbsp;&gt;&gt; Ver Tutorial Interactivo</b></a>-->
        <!--          
		  Acceso fuera del Campus Universitario
           -&gt;&nbsp;
           <a class="usatenlace" href="http://ezproxy.concytec.gob.pe:2048/login?url=http://www.sciencedirect.com/" target="_blank">CLIC AQU�</a>
           <br /><br />
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> usat.edu.pe<br />
        <b><a class="rojo">Contrase�a&nbsp;:</a></b> usat
         </span></p>
       </td>
      </tr>
-->
        <!--UpToDate 21 -->
        <!--
      <tr valign="top">
       <td align="center" class="Estilo4">
       <a href="cuentaaccesos.asp?bib=21" target="_blank" class="Estilo2">
        <img src="../images/Logo_Up_to_date.jpg" alt="Science Direct" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <br /><a class="rojo"><b>Modo de acceso :</b></a><br />Dentro del Campus Universitario
       </td>
       <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
       <span class="Estilo3"><strong class="azul">UpToDate ,</strong> es una base de datos
         que incluye evidencia cl�nica de 19 especialidades m�dicas. Cuenta con m�s de 23.000 
         gr�ficos, enlaces a res�menes de Medline, m�s de 328.000 referencias.<br /><b>(Modo de Prueba)</b> </span></p>
       </td>
      </tr>
 -->
        <!--vLex-->
        <!--UPTODATE 05.05.2014
       <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=37" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/UPTODATE.jpg" alt="Uptodate" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Uptodate,</strong> es una base de datos basada en evidencia  que permite a los  profesionales de la salud  encontrar respuesta a las consultas cl�nicas que se presentan en la pr�ctica diaria. La informaci�n se encuentra en constante revisi�n y actualizaci�n por el grupo de editores de UpToDate, m�dicos expertos en las distintas �reas de las ciencias de la salud        
        <br><br />
        <a class="usatenlace" href="../images/biblioteca/UPTODATE_2014.pdf" 
        target="_blank"><b class="rojo">&nbsp;&gt;&gt; Ver Video Tutorial</b></a><br />             
       </td>
      </tr>-->
        <!--revista naval 15.09.2014
       <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=38" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/revistanaval.jpg" alt="Revista de Ing. Naval" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Ingenier�a Naval,</strong> es una Revista Electr�nica Internacional sobre el sector mar�timo, editada desde 1929 por la Asociaci�n de Ingenieros Navales y Oce�nicos de Espa�a.       
        <br><br />
        <a class="usatenlace" href="../images/biblioteca/UPTODATE_2014.pdf" 
        target="_blank"> <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> 30732<br />
        <b><a class="rojo">Contrase�a&nbsp;:</a></b> PLP69F</a><br />             
       </td>
      </tr>
      fin -->
        <!--revista Tect�nica ? online 03.11.14-->
        <!--AGORA 18.03.15-->
        <!--     <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=40" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/agora.gif" alt="AGORA" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">AGORA,</strong> desarrollado por la Organizaci�n de las Naciones Unidas para la Agricultura y la Alimentaci�n (FAO) junto con importantes editoriales, ofrece acceso a colecciones bibliogr�ficas digitales excepcionales en el �mbito de la alimentaci�n, la agricultura, las ciencias medioambientales y ciencias sociales conexas.       
        <br><br />
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> ag-per038<br />
        <b><a class="rojo">Contrase�a&nbsp;:</a></b> d56mWgGj</a><br />             
       </td>
      </tr>-->
        <!-- fin -->
        <!--OARE 18.03.15-->
        <!--     <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=41" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/OARE.gif" alt="OARE" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="style2"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">OARE,</strong> desarrollado por el Programa de las Naciones Unidas para el Medio Ambiente (PNUMA), Yale University y destacadas editoriales cient�ficas y tecnol�gicas, ofrece acceso a una de las colecciones m�s vastas de literatura en las ciencias ambientales del mundo.       
        <br><br />
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> PER624<br />
        <b><a class="rojo">Contrase�a&nbsp;:</a></b> 46870</a><br />             
       </td>
      </tr>-->
        <!-- fin -->
        <!-- ARDI 18.03.15-->
        <!--       <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=42" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/ARDI.gif" alt="OARE" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">ARDI,</strong> es coordinado por la Organizaci�n Mundial de la Propiedad Intelectual. Brinda acceso a la literatura acad�mica de diversos campos de la ciencia y tecnolog�a 
        <br><br />
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> ardi-pe018<br />
        <b><a class="rojo">Contrase�a&nbsp;:</a></b> c5myevry</a><br />             
       </td>
      </tr>-->
        <!-- fin -->
        <!-- Tecnoaqua 11.06.15
       <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=44" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/TECNOAQUA.png" alt="TECNOAQUA" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Revista Tecnoaqua,</strong> permite obtener informaci�n precisa de todos los temas relacionados con el agua, especialmente en el �mbito de las aguas potables, aguas residuales (tanto industriales como urbanas), instrumentaci�n e informes sobre sectores espec�ficos. 
        <br><br />
        <a class="usatenlace" href="../images/biblioteca/TUTORIAL_ TECNOAQUA.pdf" 
        target="_blank"><b class="rojo">&nbsp;&gt;&gt; Descargar Tutorial</b></a><br />  
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
        <b><a class="rojo">Contrase�a&nbsp;:</a></b> os10713us </a><br />             
       </td>
      </tr>-->
        <!-- fin -->
        <!-- SpringerLink
    <tr valign="top">
     <td align="center" class="Estilo4">
      <a href="cuentaaccesos.asp?bib=36" target="_blank" class="Estilo2">
      <img src="../images/Springer.jpg" alt="Springer.jpg" width="280" border="1"  style="cursor:hand;" /></a>
	  <br /><a class="rojo"><b>Modo de acceso :</b></a><br />Dentro del Campus Universitario
     </td>
     <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
      <span class="Estilo3"><strong class="azul">SpringerLink</strong> es una plataforma que 
	  proporciona el texto completo de revistas y libros publicados por 
	  Springer-Verlag y otros editores.<br /><br />
       <b><a class="rojo">Habilitado hasta&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b>30 de Noviembre 2013<br/>
	  </span></p>
	  </td>
    </tr>-->
        <!-- Dialnet
    <tr valign="top">
     <td align="center" class="Estilo4">
      <a href="cuentaaccesos.asp?bib=37" target="_blank" class="Estilo2">
      <img src="../images/DIALNET.jpg" alt="DIALNET" width="280" border="1"  style="cursor:hand;" /></a>
	 </td>
     <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
      <span class="Estilo3"><strong class="azul">Dialnet</strong> es una base de datos de prducci�n cient�fica hispana, creada 
      por la Universidad de la Rioja, que integra m�ltiples recursos (revistas, libros, tesis,...)<br /><br />
       <b><a class="rojo">Habilitado hasta&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b>30 de Noviembre 2013<br/>
       <a class="usatenlace" href="../Biblioteca/tutorial_dialnet.pdf" target="_blank">
       <!--<a href="../Biblioteca/tutorial_dialnet.pdf" target=_blank></a>
        <b class="rojo">&nbsp;&gt;&gt;Tutorial</b></a></span></p>
	  </span></p>
	  </td>
    </tr>-->
        <!-- EBSCO inactivado el d�a 24/04/17  reactivado el 26.04.18-->
        <!-- <tr class="rojo">
		<td colspan="2"><h5><br />Odontolog�a</h5></td>
	</tr>
   
      <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=1" target="_blank" class="Estilo2">
        <img src="../images/logo_ebsco.jpg" alt="Ebsco host" width="280"  border="1" /></a></td>
       <td align="left" valign="top" class="Estilo5">
			<p align="justify" class="Estilo2">
        <span class="Estilo3"><strong class="azul">EBSCO Dentistry & Oral Sciences Source</strong> Es una base de datos especializada en temas de odontolog�as: endodoncia, patolog�a oral y m�xilofacial, radiolog�a, cirug�a, ortodoncia, periodoncia, odontolog�a pedi�trica y m�s. Cuenta con m�s de 250 revistas en texto completo.
			<br/><br/>
		  
		 <b><a class="rojo">&nbsp;&nbsp;Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> usat20 <br />
        <b><a class="rojo">&nbsp;&nbsp;Contrase�a&nbsp;:</a></b> ebsco18
		<br /><br />               
       </td>
      </tr>-->
        <!-- El Ecologista 11.09.15-->
        <!-- fin -->
        <!-- Proyectos Qu�micos 11.09.15-->
        <!-- fin -->
        <!-- El Economista 11.09.15-->
        <!-- fin -->
        <!-- IndustriAmbiente 20.10.15-->
    </table>
    <table width="99%" border="1" align="center" cellpadding="5" cellspacing="0">
        <!-- Peru Kiosko 17.11.15 -->
        <!-- fin -->
        <!-- Terramar 17.11.15 -->
        <!-- fin -->
        <!-- Retema 17.11.15 
       <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=52" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/RETEMA.png" alt="Revista Retema" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
            <span class="Estilo3"><strong style="color:azul">Revista RETEMA,</strong> es una publicaci�n que trata temas relacionados con el medio ambiente, 
            tratamiento, gesti�n, y valorizaci�n de residuos, residuos l�quidos, gaseosos, t�xicos y peligrosos.
            <br />
                      
        
            <br />
                              
        </span>
        <a class="usatenlace" href="../images/biblioteca/Revista_Retema.pdf" 
        target="_blank">&nbsp;&gt;&gt; Descargar Tutorial</a><br />
            <span class="Estilo3"><b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
        <b><a class="rojo">Contrase�a&nbsp;:</a></b>  vIcFJr3Y<br />             
            </span>
            <br />
           </td>
      </tr>-->
        <!-- fin -->
        <!--UPTODATE 04.05.2017-->
        <!--ANUARIO FILOSOFICO 15.05.2017-->
    </table>
</body>
</html>
<%end if 
if Err.number <> 0 then
    response.Write Err.Description
end if
%>