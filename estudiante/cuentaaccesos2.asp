<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%
on error resume next
 Dim codigo_usu
 codigo_usu=session("usu_biblioteca")'cargamos nueva sesi�n
 tipo_usu=session("tusu_biblioteca")'cargamos nueva sesi�n
 
 'codigo_usu=session("codigo_usu")
 'response.Write "SESION: " & session("usu_biblioteca")
 
 if codigo_usu = "" then codigo_usu="0"
   'tipo_usu=session("tipo_usu")
 
 if tipo_usu="" then tipo_usu="P"

 session("tusu_biblioteca")= tipo_usu

%>


<%
'response.Write (codigo_usu)
   origen=Request.ServerVariables("HTTP_REFERER")
   
   '----------------------------------------------------------------------
   'Fecha: 29.10.2012
   'Usuario: yperez
   'Motivo: Cambio de URL del servidor de la WebUSAT [www.usat.edu.pe->intranet.edu.pe]
   '----------------------------------------------------------------------
   
   pagprev=https://intranet.usat.edu.pe/CAMPUSVIRTUAL/estudiante/bibilotecas.asp
   referer=left(origen,68) 'intranet.usat.edu.pe
   
   'pagprev=http://server-test/CAMPUSVIRTUAL/estudiante/bibilotecas.asp
   'referer=left(origen,59) 'server-test
   
   If InStr(referer,pagprev)= 0 then
    Response.write("") 'ACCESO NO PERMITIDO
   Else
    Response.write("UNIVERSIDAD CAT�LICA SANTO TORIBIO DE MOGROVEJO")
    'Response.Write(referer)
   ' Response.Write(pagprev) 
%>

<html xmlns="http://www.w3.org/1999/xhtml">
 <head>
  <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
  <script src="js/jquery-1.4.2.min.js" type="text/javascript"></script>
  <title>cuenta accesos</title>
  <script type="text/javascript">
   function AccesoHinari(){
    //    document.getElementById("form1").submit()
       document.location.href = "https://intranet.usat.edu.pe/campusvirtual/estudiante/hinari/hinari1.html";
       //jQuery.post("https://intranet.usat.edu.pe/campusvirtual/estudiante/hinari/hinari.html");
       //setTimeout(document.location.href = "https://intranet.usat.edu.pe/campusvirtual/estudiante/hinari/hinari2.html", 1000);
     
   }
   function direccionar(caso){
    if (caso==1){
	 document.getElementById("form1").action= 'http://search.ebscohost.com/login.aspx?authtype=url'//EBSCO
	 document.getElementById("form1").submit()
	 
    }
	
	if (caso==2){
     document.getElementById("form1").action='http://search.proquest.com/pqcentral'
	 //document.getElementById("form1").action='http://search.proquest.com/refurl'//'http://proquest.umi.com/login/refurl'
     document.getElementById("form1").submit()
    }
    if (caso==3) {
        //document.getElementById("form1").action = 'http://hinari-gw.who.int/whalecomextranet.who.int/whalecom0/hinari/en/journals.php'
        //document.getElementById("form1").action = 'https://intranet.usat.edu.pe/campusvirtual/estudiante/hinari/hinari.html'
		document.getElementById("form1").action = 'http://hinarilogin.research4life.org'
        document.getElementById("form1").submit()
    }
    //if (caso==6){
     //document.getElementById("form1").action='http://www.sciencedirect.com'
     //document.getElementById("form1").submit()
   // }
    if (caso==7){                               
       document.getElementById("form1").action='http://www.bibliotechnia.com/bibliotechnia20/index.php'
       document.getElementById("form1").submit()
    }                                            //acceso no habilitado
    if (caso==8) {
        document.getElementById("form1").action = 'http://trials.uk.proquest.com/ptc?userid=936511'
        document.getElementById("form1").submit()
    }                                           //acceso no habilitado
    if (caso==9) {
       document.getElementById("form1").action = 'http://www.palgraveconnect.com/index.html' 
	   //http://www.palgraveconnect.com/pc/nams/svc/tlogin/988C8641F6B1D9DF4BD5D7D382BE45B2
       document.getElementById("form1").submit() // NO HABILITADO DESDE 26/MAYO 2011
    }
    if (caso==10) {
        document.getElementById("form1").action = 'http://www.statesmansyearbook.com/' 
	    document.getElementById("form1").submit()
    }
    if (caso==11) {
        document.getElementById("form1").action = 'http://www.dictionaryofeconomics.com/dictionary' 
	    document.getElementById("form1").submit()
	}
	if (caso == 12) {
	    document.getElementById("form1").action = 'http://www.whed-online.com/'
	    document.getElementById("form1").submit()
	}
	if (caso == 13) {
	    document.getElementById("form1").action = 'http://nxt.legis.com.co'
	    document.getElementById("form1").submit()
	}
	if (caso == 14) {
	    document.getElementById("form1").action = 'http://www.usat.eblib.com/'
	    document.getElementById("form1").submit()
	}
	if (caso == 15) {
	    document.getElementById("form1").action = 'http://usat.etailer.dpsl.net/'
	    document.getElementById("form1").submit()
	}
	if (caso == 16) {
	    document.getElementById("form1").action = 'http://site.ebrary.com/lib/usatsp/home.action'
	    document.getElementById("form1").submit()
	}
	if (caso == 17) {
	   // document.getElementById("form1").action = 'http://elprofesionaldelainformacion.metapress.com/login.asp'
	   document.getElementById("form1").action = 'http://recyt.fecyt.es/index.php/EPI'
	    document.getElementById("form1").submit()
	    // http://elprofesionaldelainformacion.metapress.com/login.asp
	    // http://elprofesionaldelainformacion.metapress.com?xqv7-jtn3-qpj2
	    // Username: quiroztequen
	    // Password: usat2011
	}
	if (caso == 18) {
	    document.getElementById("form1").action = 'http://www.aceprensa.com'//ACEPRENSA
	    document.getElementById("form1").submit()
	}
	if (caso == 19) {
	    document.getElementById("form1").action = 'http://www.revistadyna.com'//DYNA
	    document.getElementById("form1").submit()
	}
	if (caso == 20) {
	    document.getElementById("form1").action = 'http://www.psicodoc.org/clientesurl.htm'//'http://www.psicodoc.org'//psicodoc
	    document.getElementById("form1").submit()
	}
	if (caso == 21) {
	    document.getElementById("form1").action = 'http://www.uptodate.com/contents/search'//uptodate
	    document.getElementById("form1").submit()
	}
	if (caso == 22) {
	    document.getElementById("form1").action = '../images/357_educacion.pdf'//revista de educaci�n
	    document.getElementById("form1").submit()
	}
	if (caso == 23) {
	    document.getElementById("form1").action = 'http://www.index-f.com/b_cantarida/formulario.php'//HEMEROTECA CANT�RIDA 
	    document.getElementById("form1").submit()
	}
	if (caso == 24) {
	    document.getElementById("form1").action = 'http://www.index-f.com/bdevidencia/formulario.php'//CUIDEN EVIDENCIA 
	    document.getElementById("form1").submit()
	}
	if (caso == 25) {
	    document.getElementById("form1").action = 'http://www.index-f.com/new/cuiden/'//CUIDEN PLUS 
	    document.getElementById("form1").submit()
	}
	if (caso == 26) {
	    document.getElementById("form1").action = 'http://www.index-f.com/cuidenplus/cantaridaesmas.php'//SUMMA CUIDEN
	    document.getElementById("form1").submit()
	}
	if (caso == 27) {
	    document.getElementById("form1").action = 'http://www.emeraldinsight.com'//emeral
	    document.getElementById("form1").submit()
	}
	if (caso == 28) {
	    document.getElementById("form1").action = 'http://www.harrisonmedicina.com'//Harrison online
	    document.getElementById("form1").submit()
	}
	if (caso == 29) {
	    //document.getElementById("form1").action = ''//Revista de educacion N� 357 Ene/Abr 2012
	    document.location = 'https://intranet.usat.edu.pe/CAMPUSVIRTUAL/Biblioteca/357_educacion.pdf'
	    //document.getElementById("form1").submit()
	}
	if (caso == 30) {
	    document.location = 'https://intranet.usat.edu.pe/CAMPUSVIRTUAL/Biblioteca/358_educacion.pdf'
	}
	if (caso == 31) {
	    document.location = 'https://intranet.usat.edu.pe/CAMPUSVIRTUAL/Biblioteca/359_educacion.pdf'
	}
	if (caso == 32) {
	    document.location = 'https://intranet.usat.edu.pe/CAMPUSVIRTUAL/Biblioteca/360_educacion.pdf'
	}
	if (caso == 33) {
		document.getElementById("form1").action = 'remote_auth.asp'//velex
	    document.getElementById("form1").submit()
	//'document.location =remote_auth.asp'
	    //document.location = redirect_to_remote_auth('', '', 18916854, xJmWym22AeBRQ1dWzhkxO5di4qL46bnbzvwawnLNHsnHtAI5)
	    //'http://www.vlex.com/account/login_ip'
	}
	if (caso == 34) {
	    document.location = 'http://www.vlex.com/search/form'
	}
	if (caso == 35) {
		document.getElementById("form1").action = 'http://iopscience.iop.org'//IOPscience
		//document.getElementById("form1").action = 'http://latinoamerica.iop.org/cws/home'//IOPscience 
	    document.getElementById("form1").submit()
			}
	if (caso == 36) {
		//document.getElementById("form1").action = 'http://link.springer.com/WEB-INF/'//SPRINGER
		//document.getElementById("form1").submit()
		self.location.href ='http://link.springer.com'
	}

	//if (caso == 37) {
	  //  self.location.href ='http://dialnet.unirioja.es' //Dialnet
	//}

	if (caso == 37) 
	{
	    //document.location = 'http://www.uptodate.com/online'
	    //document.location = 'https://intranet.usat.edu.pe/campusvirtual/estudiante/uptodatelink2.asp'
		//document.location ='http://www.uptodate.com/online/content/search?unid=' + document.getElementById("codigo_per").value + '&srcsys=EZPX362375&search=%' 
		//  document.location ='http://www.uptodate.com/contents/search?unid=' + document.getElementById("codigo_per").value  + '&srcsys=HMGR362375'
		
		//test
		document.location ='https://intranet.usat.edu.pe/campusvirtual/goto_UpToDate.asp/contents/search?unid=' + document.getElementById("codigo_per").value + '&srcsys=HMGR362375' 
		
	}
	if (caso == 38) {
	    document.location = 'http://www.sectormaritimo.com/revis/archivo.asp?apt=101'
	}
	if (caso == 39) {
	    document.location = 'http://www.tectonica-online.com/'
	}
	if (caso == 40) {
	    document.location = 'http://agoralogin.research4life.org/'
	}
	if (caso == 41) {
	    document.location = 'http://oarelogin.research4life.org/'
	}
	if (caso == 42) {
	    document.location = 'http://ardilogin.research4life.org/'
	}
	if (caso == 44) {
	    document.location = 'http://www.tecnoaqua.es/kiosco-login'
	}
	if (caso == 45) {
	    document.location = 'http://www.index-f.com/new/cuiden/test.php'
	}
	if (caso == 46) {
	    document.location = 'https://www.dropbox.com/sh/9amjwyzls52flxw/AABi5q64UfA7BlTDawTGQj1ca?dl=0'
	}
	if (caso == 47) {
	    document.location = 'http://www.proyectosquimicos.com/?page_id=336782'
	}

	if (caso == 48) {
	    document.location = 'http://www.economist.com'
	}

	if (caso == 49) {
	    document.location = 'http://www.industriambiente.com/kiosco-login'
	}

	if (caso == 50) {
	    document.location = 'http://peruquiosco.pe/'
	}

	if (caso == 51) {
	    document.location = 'http://catalogo.institutoterramar.com/'
	}

	if (caso == 52) {
	    document.location = 'http://www.retema.es/'
	}
	if (caso == 54) {
	    document.location = 'https://www.unav.edu/publicaciones/revistas/index.php/anuario-filosofico'
	}
   }
  </script>
 </head>
 <body>
<form id="form1" name="form1" method=post action="https://intranet.usat.edu.pe/campusvirtual/estudiante/hinari/hinari1.html">
   <input type="hidden" name="whoUsername" value="PER098" />
   <input type="hidden" name="whoPassword" value="62701" />
   
  
<%
'Registra la visita

response.write("<input type='hidden' id='codigo_per' name='codigo_per' value='" & codigo_usu & "'/>")
Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion			
	obj.Ejecutar "BIB_RegistrarVisita",false,tipo_usu,codigo_usu,cint(request.QueryString("bib"))
	'obj.Ejecutar "BIB_RegistrarVisita",false,session("tipo_usu"),session("codigo_usu"),cint(request.QueryString("bib"))
	'obj.Ejecutar "BIB_RegistrarVisita",false,session("tipo_usu"),session("codigo_usu"),cint(request.QueryString("bib"))
	obj.CerrarConexion
Set obj=nothing

'Direcciona a las p�ginas respectivas

select case cint(request.QueryString("bib"))
 case 1:response.Write("<script>direccionar(1)</script>")'EBSCO

 case 2:response.Write("<script>direccionar(2)</script>")    'Proquest                  
 case 3:response.Write("<script>direccionar(3)</script>")    'Hinari                    

'case 4:response.Write("<script>direccionar(4)</script>")    'euromonitor               
'case 5:response.Write("<script>direccionar(5)</script>")    'lexi-comp                 

 'case 6:response.Write("<script>direccionar(6)</script>")    'sciencedirect             
 case 7:response.Write("<script>direccionar(7)</script>")    'bibliotechnia             

'case 8:response.Write("<script>direccionar(8)</script>")    'Proquest Central          
'case 9:response.Write("<script>direccionar(9)</script>")    'Palgrave Connect          

 case 10:response.Write("<script>direccionar(10)</script>")  'statesmansyearbook        
 case 11:response.Write("<script>direccionar(11)</script>")  'dictionaryofeconomics     
 case 12:response.Write("<script>direccionar(12)</script>")  'whed-online               
 case 13:response.Write("<script>direccionar(13)</script>")  'multilegis                
 case 14:response.Write("<script>direccionar(14)</script>")  'Ebook Library             
 case 15:response.Write("<script>direccionar(15)</script>")  'Taylor & Francis          
 case 16:response.Write("<script>direccionar(16)</script>")  'ebrary                    
 case 17:response.Write("<script>direccionar(17)</script>")  'elprofesionaldelainformacion
 
'case 17:response.Write("<script>direccionar(17)</script>")  'elprofesionaldelainformacion -http://elprofesionaldelainformacion.metapress.com?xqv7-jtn3-qpj2http://elprofesionaldelainformacion.metapress.com?xqv7-jtn3-qpj2
'case 17:response.Write("<script>Accesoprofinfo()</script>") 'elprofesionaldelainformacion.metapress.com?xqv7-jtn3-qpj2

 case 18:response.Write("<script>direccionar(18)</script>")  'aceprensa
 case 19:response.Write("<script>direccionar(19)</script>")  'Dyna
 case 20:response.Write("<script>direccionar(20)</script>")  'psicodoc
 case 21:response.Write("<script>direccionar(21)</script>")  'UPTODATE
 case 22:response.Write("<script>direccionar(22)</script>")  'Revista de Educaci�n
 case 23:response.Write("<script>direccionar(23)</script>")  'CIBERINDEX  - HEMEROTECA CANT�RIDA 
 case 24:response.Write("<script>direccionar(24)</script>")  'CIBERINDEX  - CUIDEN EVIDENCIA
 case 25:response.Write("<script>direccionar(25)</script>")  'CIBERINDEX  - CUIDEN PLUS 
 case 26:response.Write("<script>direccionar(26)</script>")  'CIBERINDEX  - SUMMA CUIDEN
 case 27:response.Write("<script>direccionar(27)</script>")  'EMERALD
 case 28:response.Write("<script>direccionar(28)</script>")  'Harrison online
 case 29:response.Write("<script>direccionar(29)</script>")  'Revista de educacion N� 357 Ene/Abr 2012
 case 30:response.Write("<script>direccionar(30)</script>")  'Revista de educacion N� 358 May/Ago 2012
 case 31:response.Write("<script>direccionar(31)</script>")  'Revista de educacion N� 359 Set/Dic 2012
 case 32:response.Write("<script>direccionar(32)</script>")  'Revista de educacion N� 360 Ene/Abr 2013
 case 33:response.Write("<script>direccionar(33)</script>")  'VLEX
 case 34:response.Write("<script>direccionar(34)</script>")  'VLEX busqueda avanzada
 case 35:response.Write("<script>direccionar(35)</script>")  'IOPscience
 'case 36:response.Write("<script>direccionar(36)</script>")  'SPRINGER
 'case 37:response.Write("<script>direccionar(37)</script>")  'SPRINGER
 case 37:response.Write("<script>direccionar(37)</script>")  'update
 case 38:response.Write("<script>direccionar(38)</script>")  'ing. naval
 case 39:response.Write("<script>direccionar(39)</script>")  'tectonica
 case 40:response.Write("<script>direccionar(40)</script>")  'AGORA
 case 41:response.Write("<script>direccionar(41)</script>")  'OARE
 case 42:response.Write("<script>direccionar(42)</script>")  'ARDI 
 case 44:response.Write("<script>direccionar(44)</script>")  'TECNOAQUA 
 case 45:response.Write("<script>direccionar(45)</script>")  'CIBERINDEX 
 case 46:response.Write("<script>direccionar(46)</script>")  'El Ecologista 
 case 47:response.Write("<script>direccionar(47)</script>")  'Proyectos Qu�micos
 case 48:response.Write("<script>direccionar(48)</script>")  'The Economist
 case 49:response.Write("<script>direccionar(49)</script>")  'Revista IndustriAmbiente
 case 50:response.Write("<script>direccionar(50)</script>")  'Per� Quiosko
 case 51:response.Write("<script>direccionar(51)</script>")  'Instituto Terramar
 case 52:response.Write("<script>direccionar(52)</script>")  'Instituto Retema
  case 54:response.Write("<script>direccionar(54)</script>")  'Anuario filosofico 
end select


End if
%>
 <br /><br /><br /><hr /><br />
 <table align=center>
  <tr><td align=center><h3>.:: UNIVERSIDAD CAT�LICA SANTO TORIBIO DE MOGROVEJO ::.</h3></td></tr>
  <tr><td><br /><br />&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 
 Estimado Usuario,</td></tr><tr><td align=center>
  El Acceso a las Bibliotecas Virtuales deber� realizarse ingresando a: <br /><br />
  - <a href=https://intranet.usat.edu.pe/campusvirtual/>Campus Virtual USAT</a> -</td></tr>
 </table><br /><hr />
 
 <!--
  <p>Revistas:</p>
  <p><a href="http://pubs.ama-assn.org" target="_blank">http://pubs.ama-assn.org</a><br>
   <a href="http://www.jco.ascopubs.org" target="_blank">http://www.jco.ascopubs.org</a><br>
   <a href="http://www.jop.ascopubs.org" target="_blank">http://www.jop.ascopubs.org</a></p>
  <p>Acceso: <a href="acceso.pdf" target="_blank">Descargar Gu�a</a> </p>
 </form>
 -->
  </form>
 </body>
</html>
