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
                alert('Esta opción se encuentra deshabilitada');
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
     <span class="Estilo3"><strong class="azul">Aceprensa,</strong> es una agencia periodística especializada 
      en el análisis de tendencias básicas de la sociedad, corrientes de pensamiento y estilos de vida.
      Selecciona temas que influyen decisivamente en la marcha de la sociedad. Aceprensa se dirige a personas 
      que están interesadas en contar con información para intervenir activamente en los debates de la opinión 
      pública. Nuestro deseo es proporcionar a los suscriptores una fuente de datos e ideas que puedan 
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
        <span class="Estilo3"><strong class="azul">ProQuest®</strong> es un servicio global de 
           publicaciones electrónicas. Se trata de uno de los mayores depósitos de contenidos 
           en línea de todo el mundo, y facilita una única plataforma integrada con acceso al 
           texto completo de miles de publicaciones, periódicos, diarios y revistas, además de 
           incluir resúmenes e índices detallados de otros tantos miles de publicaciones adicionales.<br><br>-->
        <!--<a class="usatenlace" href="http://www.etechwebsite.com/proquest1/Tutorial/pq_userguide_spanish.pdf" target="_blank">-->
        <!--<a class="usatenlace" href="https://intranet.usat.edu.pe/CAMPUSVIRTUAL/Biblioteca/userguide_np_en_es (2).pdf" target="_blank">
           <b class="rojo">&nbsp;&gt;&gt; Descargar Manual de Usuario</b></a></span></p>
       </td>
      </tr>-->
        <!--
	  <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=50" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/peruquiosco.png" alt="Perú Kiosko" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Perú Quiosco,</strong> acceso a la 
            versión digital de los diarios, por ejemplo: El Comercio, Gestión, Perú 21, 
            entre otros
            <br />
                      
        
            <br />
                              
        </span>
        <a class="usatenlace" href="../images/biblioteca/QuioscoPeru.pdf" 
        target="_blank">&nbsp;&gt;&gt; Descargar Tutorial<br />  
            <span class="Estilo3"><b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
        <b><a class="rojo">Contraseña&nbsp;:</a></b>  123456<br />             
            </span>
            <br />
           </td>
      </tr>
	  -->
        <tr class="rojo">
            <td colspan="2">
                <h4>
                    <br />
                    Administración de Empresas</h4>
            </td>
        </tr>
        <tr valign="top">
            <td align="center" class="style3">
                <a href="cuentaaccesos.asp?bib=67" target="_blank" class="Estilo2">
                    <img src="../images/biblioteca/ganamas-logo.jpg" alt="Ganamás" width="280" height="95"
                        border="1" /></a>
            </td>
            <td align="left" valign="top" class="Estilo5">
                <p align="justify" class="Estilo2">
                    <span class="Estilo3"><strong class="azul">Ganamás</strong> es una publicación mensual
                        de información, análisis y opinión para apoyar el crecimiento de los negocios y
                        motivar nuevos emprendimientos en el país.</span>
                    <br />
                    <br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
                    <b><a class="rojo">Contraseña&nbsp;:</a></b> biblioteca</p>
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
                        una de las revistas más influyentes en el mundo sobre, entre otros, temas relacionados
                        con el management, la dirección de empresas, la innovación y la globalización. Pensada
                        para servir de puente entre el mundo académico y el empresarial, HBR reúne en sus
                        páginas a reconocidos pensadores así como responsables empresariales. </span>
                    <br />
                    <br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
                    <b><a class="rojo">Contraseña&nbsp;:</a></b> UMogrovejo1!<br /></p>
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
        <img src="../images/biblioteca/tectonica.jpg" alt="Revista Tectónica Online" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Tectónica online</strong> Permite un análisis de la arquitectura, la tecnología y la construcción a través de nuevos artículos, nuevos análisis de proyectos y una constante búsqueda y selección de materiales, productos y sistemas constructivos.
        <br><br />
        <a class="usatenlace" href="../images/biblioteca/Revista-de Tectonica-online.pdf" 
        target="_blank"><b class="rojo">&nbsp;&gt;&gt; Descargar Guía de Usuario</b></a><br />    
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
                        de arquitectura, que presenta en números monográficos las obras y proyectos más
                        significativos de los arquitectos contemporáneos de mayor interés.
                        <br />
                        Para Consultas fuera del campus universitario:</span><br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> USAT<br />
                    <b><a class="rojo">Contraseña&nbsp;:</a></b> USAT                  
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
                        y diseño dirigida hacia el profesionalismo y la tecnología de construcción y las
                        prácticas del diseño que permiten la realización eficiente de un número cada vez
                        mayor de formas y sistemas constructivos, adaptados o adaptables a gran variedad
                        de programas y circunstancias</span>.
                    <br />
                    <br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
                    <b><a class="rojo">Contraseña&nbsp;:</a></b>&nbsp;12345678<br />
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
                        cobertura diversa e incisiva, que abarca todas las partes del mundo. Con una historia de 120 años como la publicación de arquitectura favorita del mundo, es líder
                        de debates y una herramienta imprescindible para las comunidades de arquitectura y diseño de todo el mundo.</span>.
                    <br />
                    <br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
                    <b><a class="rojo">Contraseña&nbsp;:</a></b>&nbsp;bibliotecausat<br />
                    <a class="usatenlace" href="../../campusvirtual/Biblioteca/Manual Revista_RbDigital_ArchitecturalReview.pdf" target="_blank"> <!-- andy.diaz 25/06/2020 -->                        
                        <br />
                        <b class="rojo">&nbsp;&gt;&gt;Descargar Manual de Usuario</b></a>
                </p>
            </td>
        </tr>
        <!--   
   <tr class="rojo">
    <td colspan="2"><h5><br />Ingeniería</h5></td>
   </tr>
   <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=55" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/VirtualPROlogo.png" alt="Virtual PRO" width="199px" height="70px" border="1"  style="cursor:hand" /></a>
		 <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Virtual Pro</strong> Presenta diferentes temas de interés en el área de ingeniería agronómica, ingeniería ambiental, ingeniería industrial, química, administración de empresas, entre otras. Se especializa en procesos industriales.
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
        del Perú y del mundo.<br />Permite acceder de forma ilimitada a todos los contenidos de Derecho a texto completo:<br>
        <br />
        <u>
            <li>Contratos</li>
            <li>Jurisprudencia</li>
            <li>Legislación</li>
            <li>Diarios</li>
        </u>
        <br /><br />
        <a class="usatenlace" href="http://www.youtube.com/embed/Ti6drooBpME" 
        target="_blank"><b class="rojo">&nbsp;&gt;&gt; Ver Video Tutorial</b></a><br /><br />
        <a class="usatenlace" href="remote_auth.asp" target="_blank"><b class="rojo">&nbsp;&gt;&gt; Búsqueda Avanzada</b></a>
        </span></p>
       </td>
      </tr> -->
        <tr class="rojo">
            <td colspan="2">
                <h4>
                    <br />
                    Economía</h4>
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
                    <span class="Estilo3"><strong class="azul">América Economía Internacional y América
                        Economía Perú</strong> es una revista líder en noticias y análisis sobre negocios
                        en América Latina, se ha consolidado como la fuente de información preferida por
                        los influyentes de esta parte del mundo. Su equipo periodístico está dedicado a
                        descubrir las tendencias en los negocios y a investigar los distintos aspectos que
                        conforman el ambiente comercial en Latinoamérica.
                        <br />
                        Para Consultas fuera del campus universitario:</span><br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
                    <b><a class="rojo">Contraseña&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
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
                        se caracteriza por ofrecer un espacio abierto para la discusión de temas actuales
                        de carácter internacional, de gran interés para el mundo y, en particular, para
                        América Latina. Foreign Affairs Latinoamérica se ha posicionado como un influyente
                        foro que refleja el pensamiento iberoamericano sobre el mundo, así como la visión
                        mundial sobre Latinoamérica, privilegiando la diversidad de enfoques y la crítica
                        del más alto nivel.</span>
                    <br />
                    <br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> unicato42<br />
                    <b><a class="rojo">Contraseña&nbsp;:</a></b> dacabio03</p>
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
        <span class="Estilo3"><strong style="color:azul">The Economist,</strong> es una revista que estudia, analiza y comenta los acontecimientos relacionados con la política mundial, el mundo de la economía, los negocios y las finanzas.<br />                             
        </span>
            <br />
            <span class="Estilo3"><b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
        <b><a class="rojo">Contraseña&nbsp;:</a></b> biblioteca<br />             
            </span>
            <br />
           </td>
      </tr>-->
        <!-- <tr class="rojo">
    <td colspan="2"><h5><br />Enfermería</h5></td>
   </tr>
    <tr valign="top">
    <td align="center" class="style3">
      <img src="../images/CIBERINDEX.jpg" alt="ciberindex" width="280" border="1" style="cursor:hand;" />     
    </td>
    <td align="left" valign="top" class="Estilo5"><p align="justify">
     <span class="Estilo3"><strong class="azul">CIBERINDEX ,</strong> es una plataforma especializada 
     en la gestión del conocimiento en cuidados de salud que tiene como misión proporcionar a 
     profesionales e instituciones de cualquier ámbito soluciones prácticas e innovadoras para la ayuda 
     en la toma de decisiones fundamentadas en el conocimiento científico.<br />
</span>
        <p align="justify">
            Consultas dentro del campus universitario:<span class="Estilo3"><u><li><a href="cuentaaccesos.asp?bib=23" target=_blank>HEMEROTECA CANTÁRIDA (Dentro del Campus Universitario)</a></li>
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
                    Filosofía</h4>
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
                    <span class="Estilo3"><strong class="azul">Anuario Filosófico</strong> es una revista
                        del Departamento de Filosofía de la Universidad de Navarra, dirigido a especialistas
                        en la investigación filosófica, profesores y estudiantes de filosofía y humanidades.
                        Incluye artículos de las diversas áreas de la filosofía y reseñas de libros de actualidad.
                        <br />
                        Para Consultas fuera del campus universitario:</span><br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> mogrovejo<br />
                    <b><a class="rojo">Contraseña&nbsp;:</a></b> biblioteca <a class="usatenlace" href="../../campusvirtual/Biblioteca/ANUARIO FILOSOFICO.pdf"
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
                        de teología de la Universidad de Navarra, editada por la Facultad de Teología, publica
                        artículos y revisiones bibliográficas de teología bíblica y sistemática, de patrología
                        y de liturgia. Promueve un enfoque interdisciplinar y presta atención a los temas
                        teológicos de actualidad.
                        <br />
                        Para Consultas fuera del campus universitario:</span><br />
                    <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> mogrovejo<br />
                    <b><a class="rojo">Contraseña&nbsp;:</a></b> biblioteca <a class="usatenlace" href="../../campusvirtual/Biblioteca/Manual SCRIPTA THEOLOGICA.pdf"
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
        <span class="Estilo3"><strong style="color:azul">Persona y Derecho,</strong> revista que ofrece estudios sobre pensamiento jurídico, político y social, con particular atención a los derechos humanos. De utilidad para juristas e interesados en problemas jurídicos, se dirige especialmente a investigadores en Filosofía del Derecho, Derechos Humanos y Filosofía.</span>       
        <br>Para Consultas fuera del campus universitario:</br>
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> mogrovejo<br />
        <b><a class="rojo">Contraseña&nbsp;:</a></b> biblioteca
		<br>
		<br>
		<a class="usatenlace" href="https://intranet.usat.edu.pe/campusvirtual/Biblioteca/MANUAL PERSONA Y DERECHO.pdf" target="_blank">
           <b class="rojo">&nbsp;&gt;&gt;Descargar Manual de Usuario</b></a></span></p>
       </td>
      </tr>-->
        <!--
   <tr class="rojo">
    <td colspan="2"><h5><br />Ingeniería Civil Ambiental</h5></td>
   </tr>
     <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=51" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/TERRAMAR.png" alt="Instituto Terramar" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
            <span class="Estilo3"><strong style="color:azul">Instituto Terramar,</strong> el catalogo normativo del Instituto Terramar compendia 
            las Normas Técnicas y administrativas necesarias para diseñar y construir proyectos de habilitación urbana y/o edificación, 
            así como también, la oferta disponible de materiales y acabados para la construcción de los mismos.
            <br />                             
            <br />                              
        </span>
        <a class="usatenlace" href="../images/biblioteca/TERRAMAR.pdf" 
        target="_blank">&nbsp;&gt;&gt; Descargar Tutorial</a><br />
            <span class="Estilo3"><b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> usat<br />
        <b><a class="rojo">Contraseña&nbsp;:</a></b>  hlima759@<br />             
            </span>
            <br />
           </td>
      </tr>-->
        <!--<tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=46" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/elEcologista.png" alt="El Ecologista" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Revista El Ecologista,</strong> La revista El Ecologista es una publicación de información ambiental. Trata los 
               temas del clima, la biodiversidad, la ingeniería genética, los bosques, la 
               energía, la contaminación, los residuos, el transporte, los recursos naturales y 
               la globalización. 
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
            Es una publicación científico-técnica que trata temas relacionados con el medio 
            ambiente, gestión de residuos, energías renovables, reciclaje, aguas residuales,&nbsp; 
            tecnologías limpias,&nbsp; residuos líquidos, gaseosos, tóxicos y peligrosos. <br />
            <br />
                            
        </span>
        
        <a class="usatenlace" href="../images/biblioteca/Revista_IndustriAmbiente.pdf" 
        target="_blank">&nbsp;&gt;&gt; Descargar Tutorial</a></b><br />  
            <span class="Estilo3"><b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
        <b><a class="rojo">Contraseña&nbsp;:</a> </b>os10713us</span><br />
           </td>
      </tr>-->
        <!--
     <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=47" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/Revista_PQ.png" alt="PQ Revista" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="style2"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Proyectos Químicos,</strong> es una revista que aborda los temas de ingenierías, industria, aguas y suelos, sólidos y pulverulentos.</span><br />
            <br />
            
         <a class="usatenlace" href="../images/biblioteca/Tutorial_Revista_Quimica.pdf" 
        target="_blank">&nbsp;&gt;&gt; Descargar Tutorial</a></b><br />  
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> 
            <a href="https://intranet.usat.edu.pe/OWA/redir.aspx?URL=mailto%3abiblioservicios%40usat.edu.pe" 
                style="font-family: Arial, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 19.2000007629395px; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px;">
            biblioservicios@usat.edu.pe</a> <br />
            <span style="color: rgb(0, 0, 0); font-family: Arial, sans-serif; font-size: 12px; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: 19.2000007629395px; orphans: auto; text-align: start; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; display: inline !important; float: none;">
            <b><a class="rojo">Contraseña&nbsp;&nbsp; :</a> </b>kgxvya52</span></td>
            
  
      </tr>-->
        <tr class="rojo">
            <td colspan="2">
                <h4>
                    <br />
                    Ingeniería Industrial</h4>
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
                    <span class="Estilo3"><strong class="azul">DYNA</strong> es la revista técnica industrial
                        pionera de España y es el órgano oficial de Ciencia y Tecnología de la Federación
                        de Asociaciones de Ingenieros Industriales de España (FAIIE). Considerada como revista
                        de divulgación técnico-científica Indexada en Science Citation Index Expanded. Contiene
                        gran variedad de temas que van desde los puramente técnicos, hasta aspectos de la
                        actualidad, como organización industrial, desarrollo sostenible, responsabilidad
                        corporativa, enfermedad profesional, innovación, etc.
                        <br />
                        Para Consultas fuera del campus universitario:</span>
                    <br />
                    <a style="font-weight: bold">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a>&nbsp;facha200
                    <br />
                    <a style="font-weight: bold">Contraseña :</a> 325527100
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
                        peruana especializada en agricultura, ganadería, forestación, medio ambiente y desarrollo
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
        <span class="Estilo3"><strong style="color:azul">UpToDate,</strong> es un recurso de información médica sintetizada basada en evidencia.</span>        
        <br>
        <br />             
       </td>
      </tr>-->
        <!-- <tr class="rojo">
             <td colspan="2"><h5><br />Psicología</h5></td>
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
        la búsqueda bibliográfica y el acceso al texto completo de las publicaciones científicas 
        sobre Psicología y otras disciplinas afines.<br />
           <b><br />
           Descargar : <a href= "../Biblioteca/PSICODOC PRESENTACION.pdf" target=_blank>Presentación de servicios
            </a><br />
           Descargar : <a href="../Biblioteca/PSICODOC_BUSQUEDA.PDF.pdf" target=_blank>Tutorial de búsquedas</a>
           </b>
           <br />___________________________________<br /><br />
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> unisatom <br />
        <b><a class="rojo">Contraseña&nbsp;:</a></b> 730500
           </span></p>
       </td>
      </tr>-->
        <tr class="rojo">
            <td colspan="2">
                <h4>
                    <br />
                    Tecnologías de la Información</h4>
            </td>
        </tr>
        <tr valign="top">
            <td align="center" class="style3">
                <a href="cuentaaccesos.asp?bib=17" target="_blank" class="Estilo2">
                    <img src="../images/logproinf.jpg" alt="El Profesional de la Información" width="280"
                        border="1" style="cursor: hand;" /></a>
            </td>
            <td align="left" valign="top" class="Estilo5">
                <p align="justify" class="Estilo2">
                    <span class="Estilo3"><strong class="azul">El Profesional de la Información</strong>
                        es una revista electrónica internacional sobre Documentación, Bibliotecología, Comunicación
                        y Nuevas Tecnologías de la información. Indexada por ISI Social Science Citation
                        Index, Scopus y otras bases de datos.<br />
                        <br />
                        <!--<b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> quiroztequen<br />
       <b><a class="rojo">Contraseña&nbsp;:</a></b> usat2011-->
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
      <br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; opcion: <b>&nbsp;'¿Tengo Java?'</b></p>
    </td>
    <td align="left" valign="top" class="style6"><p align="justify" class="Estilo2">
     <span class="Estilo3"><strong class="azul">
      E-LIBRO,</strong> es una bases de libros electrónicos que contine más de 38,000 
      títulos en español; 3700 títulos en portugués y 1050 mapas. Con actualización de 
      contenidos semanal y un crecimiento del 20% mensual. En e-libro también 
      encontrará un amplio número de documentos en español de las universidades.<br /><br />
      <b>COLECCIONES ACADÉMICAS</b>
      <UL>
       <li>Bellas artes, artes visuales y ciencias semióticas.
       <li>Ciencias sociales.
       <li>Arquitectura, urbanismo y diseño.
       <li>Ciencias económicas y administrativas.
       <li>Ciencias de la salud.
       <li>Psicología.
       <li>Ciencias exactas y naturales.
       <li>Ciencias biológicas, veterinarias y silvoagropecuarias.
       <li>Ingenierías y tecnología.
       <li>Informática, computación y telecomunicaciones.
       <li>Ciencias de la información y de la comunicación.
       <li>Interés general: Autoayuda y espiritualidad, biografías, actualidad, deportes, entretenimiento, etc.
      </UL></p>
     </td>
    </tr>
    -->
        <!-- EL PROFESIONAL DE LA INFORMACIÓN 17 -->
        <!-- ESMERALD 27 -->
        <!--
    <tr valign="top">
     <td align="center" class="Estilo4">
      <a href="cuentaaccesos.asp?bib=27" target="_blank" class="Estilo2">
      <img src="../images/emerald.png" alt="El Profesional de la Información" width="280" border="1"  style="cursor:hand;" /></a>
     </td>
     <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
      <span class="Estilo3"><strong class="azul">EMERALD,</strong> 
       es una base de datos que brinda acceso a artículos científicos, libros, etc. 
       Incluye las bases de datos: <BR />Emerald Management eJournals (áreas temáticas de gestión, 
       como Ciencias Empresariales, Comercio Internacional, Contabilidad y Finanzas, Derecho y 
       Ética Empresarial, Economía, Empresa e Innovación, Marketing) <br /><br />
       Emerald Engineering eJournal Collection (áreas temáticas de Ingeniería, Ciencia de los Materiales y
        Tecnología).<br /><br />
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
        <span class="Estilo3"><strong class="azul">Harrison online</strong> es un  texto básico 
        de la Medicina ahora en línea y con actualizaciones diarias.<br /><br />
        <b>Incluye:</b>
      <UL>
       <li>Autoevaluación: Con mas de 800 preguntas
       <li>Imágenes de Medicina de Urgencias: auxiliares en la realización de diagnósticos visuales.
       <li>Actualizaciones: Que incorpora un historial con las actualizaciones que se han hecho a 
           partir de noviembre de 2004 del Harrison. 
       <li>Grand rounds reúne conferencias multimedia: sobre aspectos novedosos de enfermedades que
           día a día atacan con más fuerza a la población mundial.
      </UL>
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> univcstdm<br />
       <b><a class="rojo">Contraseña&nbsp;:</a></b> medicine<br /><br />
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
       HINARI,</strong> tiene como objeto ofrecer acceso al mayor número de revistas de 
       biomedicina y otros temas en el campo de las ciencias sociales. HINARI ofrece al 
       usuario una interfaz simple que sirve como portal de acceso al texto completo de 
       artículos de revistas de las Editoriales Asociadas. Los usuarios de HINARI 
       pueden buscar y tener acceso a artículos a texto completo disponibles 
       directamente desde la base de datos PubMed (Medline).</span></p>
	   
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> PER098<br />
        <b><a class="rojo">Contraseña&nbsp;:</a></b> 62701</a><br />             
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
       El contenido ha sido organizado mediante más de 6000 materias clasificadas utilizando PACS y MSC, especializadas en físicas, astronomía y matemáticas.
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
        <span class="Estilo3"><strong class="azul">Multilegis.</strong> Legis nació en 1952 
           como respuesta a la necesidad de compilar y hacer comprensible toda la 
           legislación económica oficial del país. <br />
           Para cumplir con este propósito, introdujo su revolucionario sistema que gracias 
           a su permanente tecnológica y al convenio con Universidad Católica Santo Toribio 
           de Mogrovejo, presenta un completo y actualizado sistema de información jurídica 
           en línea llamado Multilegis.<br />
           Multilegis cuenta con prácticos buscadores de tecnología avanzada y con 
           herramientas que proporcionan un manejo sencillo y eficaz de texto.<br /></span></p>
       </td>
      </tr>
      -->
        <!-- REVISTA DE EDUCACIÓN usat -->
        <!-- <tr valign="top">
       <td align="center" class="Estilo4">
       <a class="Estilo2">
        <img src="../images/revista_educacion.png" alt="Revista de Educación" width="280" height="95" border="1" /></a></td>
       <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong class="azul">Revista de Educación</strong> es una publicación 
        científica del Ministerio de Educación, Cultura y Deporte español.
        <br />___________________________________<br /><br />
        <b><a class="rojo">Números</a></b><br /><br />
            <b>Nº 357</b>&nbsp;&nbsp;Ene<b>/</b>Abr&nbsp;2012&nbsp;&nbsp;&nbsp;&nbsp;>>>&nbsp;&nbsp;
            <a href="cuentaaccesos.asp?bib=29" class="azul" target="_blank"><b>PDF</b></a>
            <br />
            <b>Nº 358</b>&nbsp;&nbsp;May<b>/</b>Ago&nbsp;2012&nbsp;&nbsp;&nbsp;>>>&nbsp;&nbsp;
            <a href="cuentaaccesos.asp?bib=30" class="azul" target="_blank"><b>PDF</b></a>
            <br />
            <b>Nº 359</b>&nbsp;&nbsp;Set<b>/</b>Dic&nbsp;2012&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;>>>&nbsp;&nbsp;
            <a href="cuentaaccesos.asp?bib=31" class="azul" target="_blank"><b>PDF</b></a>
            <br />
            <b>Nº 360</b>&nbsp;&nbsp;Ene<b>/</b>Abr&nbsp;2013&nbsp;&nbsp;&nbsp;&nbsp;>>>&nbsp;&nbsp;
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
        <span class="Estilo3"><strong class="azul">Science Direct,</strong> es una edición biomédica y el más importante proveedor de información
         científico-médica.<br><br>
           <b><a class="rojo">Habilitado hasta&nbsp;:&nbsp;</a></b>30 de Agosto de 2013
           <br><br>
	   -->
        <!--<a class="usatenlace" href="http://www.biblioteca.udep.edu.pe/wp-content/uploads/2011/11/ScienceDirect-User-Guide_ESP.pdf
" target="_blank"><b class="rojo">&nbsp;&gt;&gt; Ver Tutorial Interactivo</b></a>-->
        <!--          
		  Acceso fuera del Campus Universitario
           -&gt;&nbsp;
           <a class="usatenlace" href="http://ezproxy.concytec.gob.pe:2048/login?url=http://www.sciencedirect.com/" target="_blank">CLIC AQUÍ</a>
           <br /><br />
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> usat.edu.pe<br />
        <b><a class="rojo">Contraseña&nbsp;:</a></b> usat
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
         que incluye evidencia clínica de 19 especialidades médicas. Cuenta con más de 23.000 
         gráficos, enlaces a resúmenes de Medline, más de 328.000 referencias.<br /><b>(Modo de Prueba)</b> </span></p>
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
        <span class="Estilo3"><strong style="color:azul">Uptodate,</strong> es una base de datos basada en evidencia  que permite a los  profesionales de la salud  encontrar respuesta a las consultas clínicas que se presentan en la práctica diaria. La información se encuentra en constante revisión y actualización por el grupo de editores de UpToDate, médicos expertos en las distintas áreas de las ciencias de la salud        
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
        <span class="Estilo3"><strong style="color:azul">Ingeniería Naval,</strong> es una Revista Electrónica Internacional sobre el sector marítimo, editada desde 1929 por la Asociación de Ingenieros Navales y Oceánicos de España.       
        <br><br />
        <a class="usatenlace" href="../images/biblioteca/UPTODATE_2014.pdf" 
        target="_blank"> <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> 30732<br />
        <b><a class="rojo">Contraseña&nbsp;:</a></b> PLP69F</a><br />             
       </td>
      </tr>
      fin -->
        <!--revista Tectónica ? online 03.11.14-->
        <!--AGORA 18.03.15-->
        <!--     <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=40" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/agora.gif" alt="AGORA" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">AGORA,</strong> desarrollado por la Organización de las Naciones Unidas para la Agricultura y la Alimentación (FAO) junto con importantes editoriales, ofrece acceso a colecciones bibliográficas digitales excepcionales en el ámbito de la alimentación, la agricultura, las ciencias medioambientales y ciencias sociales conexas.       
        <br><br />
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> ag-per038<br />
        <b><a class="rojo">Contraseña&nbsp;:</a></b> d56mWgGj</a><br />             
       </td>
      </tr>-->
        <!-- fin -->
        <!--OARE 18.03.15-->
        <!--     <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=41" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/OARE.gif" alt="OARE" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="style2"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">OARE,</strong> desarrollado por el Programa de las Naciones Unidas para el Medio Ambiente (PNUMA), Yale University y destacadas editoriales científicas y tecnológicas, ofrece acceso a una de las colecciones más vastas de literatura en las ciencias ambientales del mundo.       
        <br><br />
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> PER624<br />
        <b><a class="rojo">Contraseña&nbsp;:</a></b> 46870</a><br />             
       </td>
      </tr>-->
        <!-- fin -->
        <!-- ARDI 18.03.15-->
        <!--       <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=42" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/ARDI.gif" alt="OARE" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">ARDI,</strong> es coordinado por la Organización Mundial de la Propiedad Intelectual. Brinda acceso a la literatura académica de diversos campos de la ciencia y tecnología 
        <br><br />
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> ardi-pe018<br />
        <b><a class="rojo">Contraseña&nbsp;:</a></b> c5myevry</a><br />             
       </td>
      </tr>-->
        <!-- fin -->
        <!-- Tecnoaqua 11.06.15
       <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=44" target="_blank" class="Estilo2">
        <img src="../images/biblioteca/TECNOAQUA.png" alt="TECNOAQUA" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong style="color:azul">Revista Tecnoaqua,</strong> permite obtener información precisa de todos los temas relacionados con el agua, especialmente en el ámbito de las aguas potables, aguas residuales (tanto industriales como urbanas), instrumentación e informes sobre sectores específicos. 
        <br><br />
        <a class="usatenlace" href="../images/biblioteca/TUTORIAL_ TECNOAQUA.pdf" 
        target="_blank"><b class="rojo">&nbsp;&gt;&gt; Descargar Tutorial</b></a><br />  
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
        <b><a class="rojo">Contraseña&nbsp;:</a></b> os10713us </a><br />             
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
      <span class="Estilo3"><strong class="azul">Dialnet</strong> es una base de datos de prducción científica hispana, creada 
      por la Universidad de la Rioja, que integra múltiples recursos (revistas, libros, tesis,...)<br /><br />
       <b><a class="rojo">Habilitado hasta&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b>30 de Noviembre 2013<br/>
       <a class="usatenlace" href="../Biblioteca/tutorial_dialnet.pdf" target="_blank">
       <!--<a href="../Biblioteca/tutorial_dialnet.pdf" target=_blank></a>
        <b class="rojo">&nbsp;&gt;&gt;Tutorial</b></a></span></p>
	  </span></p>
	  </td>
    </tr>-->
        <!-- EBSCO inactivado el día 24/04/17  reactivado el 26.04.18-->
        <!-- <tr class="rojo">
		<td colspan="2"><h5><br />Odontología</h5></td>
	</tr>
   
      <tr valign="top">
       <td align="center" class="style3">
       <a href="cuentaaccesos.asp?bib=1" target="_blank" class="Estilo2">
        <img src="../images/logo_ebsco.jpg" alt="Ebsco host" width="280"  border="1" /></a></td>
       <td align="left" valign="top" class="Estilo5">
			<p align="justify" class="Estilo2">
        <span class="Estilo3"><strong class="azul">EBSCO Dentistry & Oral Sciences Source</strong> Es una base de datos especializada en temas de odontologías: endodoncia, patología oral y máxilofacial, radiología, cirugía, ortodoncia, periodoncia, odontología pediátrica y más. Cuenta con más de 250 revistas en texto completo.
			<br/><br/>
		  
		 <b><a class="rojo">&nbsp;&nbsp;Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> usat20 <br />
        <b><a class="rojo">&nbsp;&nbsp;Contraseña&nbsp;:</a></b> ebsco18
		<br /><br />               
       </td>
      </tr>-->
        <!-- El Ecologista 11.09.15-->
        <!-- fin -->
        <!-- Proyectos Químicos 11.09.15-->
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
            <span class="Estilo3"><strong style="color:azul">Revista RETEMA,</strong> es una publicación que trata temas relacionados con el medio ambiente, 
            tratamiento, gestión, y valorización de residuos, residuos líquidos, gaseosos, tóxicos y peligrosos.
            <br />
                      
        
            <br />
                              
        </span>
        <a class="usatenlace" href="../images/biblioteca/Revista_Retema.pdf" 
        target="_blank">&nbsp;&gt;&gt; Descargar Tutorial</a><br />
            <span class="Estilo3"><b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> biblioservicios@usat.edu.pe<br />
        <b><a class="rojo">Contraseña&nbsp;:</a></b>  vIcFJr3Y<br />             
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