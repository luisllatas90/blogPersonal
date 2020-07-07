<%
codigo_usu=session("codigo_usu")
tipo_usu=session("tipo_usu")

if codigo_usu="" then codigo_usu=request.querystring("id")

    session("usu_biblioteca") = codigo_usu 'agregado el 05/06/2012
    session("Tusu_biblioteca")= "G" 'tipo_usu - solo para egresados

'response.Write codigo_usu
'response.Write tipo_usu

if codigo_usu<>"" then
%>
<html xmlns="http://www.w3.org/1999/xhtml">
 <head>
  <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
  <title>Bibliotecas</title>
  <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <Script>
        function click() { 
         if (event.button== 2 ) { 
         alert( 'Esta opción se encuentra deshabilitada' ); 
         } 
        } 
    </Script>
    <style type="text/css">
        .Estilo1 {font-family: Verdana,Arial,Helvetica,sans-serif;font-weight:bold;color:#990000;font-size:12px;}
        .Estilo2 {font-family: Verdana, Arial, Helvetica, sans-serif}
        .Estilo3 {font-size: 12px}
        .Estilo4 {width: 472px;}
        .Estilo5 {width: 71%;   height : 109px;}
    </style>
 </head>
 <body>
  <table width="99%" border="0" align="center" cellpadding="0" cellspacing="0">
   <tr>
    <td>&nbsp;</td>
   </tr>
   <tr class="azul">
    <td align="center"><h3>.:: BIBLIOTECAS VIRTUALES ::.</h3></td>
   </tr>
  </table>
  <table width="99%" border="1" align="center" cellpadding="5" cellspacing="0">
   <tr>
    <td height="36" colspan="2" bgcolor="#FFCC00"><span class="Estilo1">BASES DE DATOS</span></td>
   </tr>
   <!-- ACEPRENSA 18 -->
   <!--<tr valign="top">
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
   <!--<tr valign="top">
    <td align="center" class="Estilo4">
      <img src="../images/CIBERINDEX.jpg" alt="ciberindex" width="280" border="1" style="cursor:hand;" />
     <br /><a class="rojo"><b>Modo de acceso :</b></a><br />Dentro del Campus Universitario
    </td>
    <td align="left" valign="top" class="Estilo5"><p align="justify">
     <span class="Estilo3"><strong class="azul">CIBERINDEX ,</strong> es una plataforma especializada 
     en la gestión del conocimiento en cuidados de salud que tiene como misión proporcionar a 
     profesionales e instituciones de cualquier ámbito soluciones prácticas e innovadoras para la ayuda 
     en la toma de decisiones fundamentadas en el conocimiento científico.<br /><br />
     Incluye las bases de datos: <br />
     <u>
     <li><a href="cuentaaccesos.asp?bib=23" target=_blank>HEMEROTECA CANTÁRIDA</a></li>
     <li><a href="cuentaaccesos.asp?bib=24" target=_blank>CUIDEN EVIDENCIA</a></li>
     <li><a href="cuentaaccesos.asp?bib=25" target=_blank>CUIDEN PLUS</a></li>
     <li><a href="cuentaaccesos.asp?bib=26" target=_blank>SUMMA CUIDEN</a></li>
     </u>
</span></p>
    </td>
   </tr>-->
   <!--DYNA 19 -->
   <!--<tr valign="top">
    <td align="center" class="Estilo4">
     <a href="cuentaaccesos.asp?bib=19" target="_blank" class="Estilo2">
     <img src="../images/DYNA.jpg" alt="Revista DYNA" width="280" border="1" style="cursor:hand; height: 128px;" /></a>
     <br /><a class="rojo"><b>Modo de acceso :</b></a><br />Dentro del Campus Universitario
    </td>
    <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
     <span class="Estilo3"><strong class="azul">DYNA,</strong> es la revista técnica 
      industrial pionera de España y es el órgano oficial de Ciencia y Tecnología de 
      la Federación de Asociaciones de Ingenieros Industriales de España (FAIIE). 
      Considerada como revista de divulgación técnico-científica Indexada en Science 
      Citation Index Expanded. Contiene gran variedad de temas que van desde los 
      puramente técnicos, hasta aspectos de la actualidad, como organización 
      industrial, desarrollo sostenible, responsabilidad corporativa, enfermedad 
      profesional, innovación, etc.</span></p>
    </td>
   </tr>-->
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
    <tr valign="top">
     <td align="center" class="Estilo4">
      <a href="cuentaaccesos.asp?bib=17" target="_blank" class="Estilo2">
      <img src="../images/logproinf.jpg" alt="El Profesional de la Información" width="280" border="1"  style="cursor:hand;" /></a>
     </td>
     <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
      <span class="Estilo3"><strong class="azul">El Profesional de la Información,</strong> 
       es una Revista Electrónica Internacional sobre Documentación, Bibliotecología, 
       Comunicación y Nuevas Tecnologías de la información. Indexada por ISI Social 
       Science Citation Index, Scopus y otras bases de datos.<br /><br />
       <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> quiroztequen<br />
       <b><a class="rojo">Contraseña&nbsp;:</a></b> usat2011
      </span></p>
     </td>
    </tr>
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
    <tr valign="top">
     <td align="center" class="Estilo4">
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
     </td>
    </tr>
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
      <!-- ProQuest 2 -->
      <tr valign="top">
       <td align="center" class="Estilo4">
       <a href="cuentaaccesos.asp?bib=2" target="_blank" class="Estilo2">
        <img src="../images/proquest_logo-186.jpg" alt="ProQuest" width="280" height="95" border="1" /></a></td>
       <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong class="azul">ProQuest®</strong> es un servicio global de 
           publicaciones electrónicas. Se trata de uno de los mayores depósitos de contenidos 
           en línea de todo el mundo, y facilita una única plataforma integrada con acceso al 
           texto completo de miles de publicaciones, periódicos, diarios y revistas, además de 
           incluir resúmenes e índices detallados de otros tantos miles de publicaciones adicionales.<br><br>
           <!--<a class="usatenlace" href="http://www.etechwebsite.com/proquest1/Tutorial/pq_userguide_spanish.pdf" target="_blank">-->
           <a class="usatenlace" href="https://intranet.usat.edu.pe/CAMPUSVIRTUAL/Biblioteca/userguide_np_en_es (2).pdf" target="_blank">
           <b class="rojo">&nbsp;&gt;&gt; Descargar Manual de Usuario</b></a></span></p>
       </td>
      </tr>
      <!-- PSICODOC 20 -->
      <tr valign="top">
       <td align="center" class="Estilo4">
       <a href="cuentaaccesos.asp?bib=20" target="_blank" class="Estilo2">
        <img src="../images/psicodoc.jpg" alt="PSICODOC" width="280" height="95" border="1" /></a>
       <!--<a href="http://www.psicodoc.org/clientesurl.htm" target="_blank">Cliente Psicodoc</a>-->
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
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> usatbico<br />
        <b><a class="rojo">Contraseña&nbsp;:</a></b> 730500
           </span></p>
       </td>
      </tr>
      <!-- REVISTA DE EDUCACIÓN usat -->
      <tr valign="top">
       <td align="center" class="Estilo4">
       <a class="Estilo2">
        <img src="../images/revista_educacion.png" alt="Revista de Educación" width="280" height="95" border="1" /></a></td>
       <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong class="azul">Revista de Educación</strong> es una publicación 
        científica del Ministerio de Educación, Cultura y Deporte español.
        <br />___________________________________<br /><br />
        <b><a class="rojo">Números</a></b><br /><br />
            <b>Nº 357</b>&nbsp;&nbsp;Ene<b>/</b>Abr&nbsp;2012&nbsp;&nbsp;&nbsp;&nbsp;-->&nbsp;&nbsp;
            <a href="cuentaaccesos.asp?bib=29" class="azul" target="_blank"><b>PDF</b></a>
            <br />
            <b>Nº 358</b>&nbsp;&nbsp;May<b>/</b>Ago&nbsp;2012&nbsp;&nbsp;&nbsp;-->&nbsp;&nbsp;
            <a href="cuentaaccesos.asp?bib=30" class="azul" target="_blank"><b>PDF</b></a>
            <br />
            <b>Nº 359</b>&nbsp;&nbsp;Set<b>/</b>Dic&nbsp;2012&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; -->&nbsp;&nbsp;
            <a href="cuentaaccesos.asp?bib=31" class="azul" target="_blank"><b>PDF</b></a>
            <br />
            <b>Nº 360</b>&nbsp;&nbsp;Ene<b>/</b>Abr&nbsp;2013&nbsp;&nbsp;&nbsp;&nbsp;-->&nbsp;&nbsp;
            <a href="cuentaaccesos.asp?bib=32" class="azul" target="_blank"><b>PDF</b></a>
            <!--<a href="../images/357_educacion.pdf" class="azul" target="_blank"><b>PDF</b></a>-->
        </span></p>
       </td>
      </tr>
      <!--SCIENCE DIRECT 6 -->
      
     <!-- <tr valign="top">
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
           
	<!--   Acceso fuera del Campus Universitario
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
      <!--vLex -->
      
      <tr valign="top">
       <td align="center" class="Estilo4">
       <a href="cuentaaccesos.asp?bib=33" target="_blank" class="Estilo2">
        <img src="../images/vlexlogo.png" alt="vLex" width="280" height="95" border="1"  style="cursor:hand" /></a>
        <!--<br /><a class="rojo"><b>Modo de acceso :</b></a><br />Dentro del Campus Universitario</td>-->
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
      </tr>
      <!-- fin -->
	  
	    <!-- SpringerLink  -->
    <!--<tr valign="top">
     <td align="center" class="Estilo4">
      <a href="cuentaaccesos.asp?bib=36" target="_blank" class="Estilo2">
      <img src="../images/Springer.jpg" alt="Springer.jpg" width="280" border="1"  style="cursor:hand;" /></a>
     </td>
     <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
      <span class="Estilo3"><strong class="azul">SpringerLink</strong> es una plataforma que 
	  proporciona el texto completo de revistas y libros publicados por 
	  Springer-Verlag y otros editores.<br/><br />
       <b><a class="rojo">Habilitado hasta&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b>30 de Noviembre 2013<br />
      </span></p>
	  </td>
    </tr>-->
	  
	  <!-- EBSCO -->
      <tr valign="top">
       <td align="center" class="Estilo4">
       <a href="cuentaaccesos.asp?bib=1" target="_blank" class="Estilo2">
        <img src="../images/logo_ebsco.jpg" alt="Ebsco" width="280" height="95" border="1" /></a></td>
       <td align="left" valign="top" class="Estilo5"><p align="justify" class="Estilo2">
        <span class="Estilo3"><strong class="azul">EBSCO</strong> es una  base de datos 
		bibliográfica que recoge información científica en forma de abstract y texto completo 
		de artículos de revistas y otras publicaciones
		<br /><br /><br />
        <b><a class="rojo">Usuario&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;:</a></b> usat.edu.pe<br />
        <b><a class="rojo">Contraseña&nbsp;:</a></b> usat
           </span></p>
		<br><br>
           <!--<a class="usatenlace" href="http://www.etechwebsite.com/proquest1/Tutorial/pq_userguide_spanish.pdf" target="_blank">-->
          <!--<a class="usatenlace" href="https://intranet.usat.edu.pe/CAMPUSVIRTUAL/Biblioteca/userguide_np_en_es (2).pdf" target="_blank">
           <b class="rojo">&nbsp;&gt;&gt; Descargar Manual de Usuario</b></a></span></p>>-->
       </td>
      </tr>
  </table>
 </body>
</html>
<%end if %>