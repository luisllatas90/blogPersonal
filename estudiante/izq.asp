<!--#include file="../NoCache.asp"-->
<!--#include file="../funciones.asp"-->
<%
'on error resume next

'----------------------------------------------------------------------
'Fecha: 29.10.2012
'Usuario: yperez
'Motivo: Cambio de URL del servidor de la WebUSAT [www.usat.edu.pe->intranet.edu.pe]
'----------------------------------------------------------------------


call Enviarfin(session("codigo_alu"),"../")

ct= cstr(replace( replace(replace( replace(NOW,"/",""),":","") ," ",""),".",""))
ct= StrReverse(ct)

dim codigo_alu 
codigo_alu = session("codigo_alu")
'response.Write(session("codigo_alu"))   '--<<<para pruebas dante!
'response.Write(session("codigo_apl"))    '--<<<para pruebas dante!
 
Sub CrearMenu(ByVal codigo_apl,ByVal codigo_tfu,ByVal codigoRaiz_Men,ByVal j)
		dim i,x
		dim ImagenMenu,EventoMenu,TextoMenu,idPadre,EstadoMenu,Clase
		dim rsMenu
		x=0
		'Set objEnc=server.CreateObject("EncriptaCodigos.clsEncripta")
	    'enc=objEnc.Codifica("069" & session("codigouniver_alu"))
	    
	    'response.Write	"ConsultarAplicacionUsuario 11," & codigo_apl & ", " & codigo_tfu  & ", " & codigoRaiz_men
	    Set rsMenu=Obj.Consultar("ConsultarAplicacionUsuario","FO","11",codigo_apl,codigo_tfu,codigoRaiz_men)

        for i=1 to rsMenu.recordcount
			cadena=""
			
			'Genera espacios para jerarqu�a
			for x=1 to j
				'cadena=cadena & "..." ' incluir imagen en blanco
				cadena=cadena & "<img src='../images/blanco.gif'>"
			next
					
			'====================================================================
			clase=""
			EstadoMenu=""
			ImagenMenu=""
			EventoMenu=""
			ImagenMenu=rsMenu("icono_men")
			TextoMenu=rsMenu("descripcion_men")
			idPadre=rsMenu("codigoRaiz_men")
			
			'====================================================================
			'Verificar im�gen del men�, caso contrario colocar una por defecto
			'====================================================================
			if rsMenu("total_men")>0 then
				ImagenMenu="menos2.GIF"
			elseif (rsMenu("icono_men")="" or IsNull(rsMenu("icono_men"))=true) then
				ImagenMenu="../images/menus/menu.gif"
			end if
		
			'====================================================================
			'Verificar enlace del men� y determinar si es padre o Hijo
			'====================================================================
			if rsMenu("total_men")>0 then
				EventoMenu="onclick=""ExpandirNodo(Mnu" & rsMenu("codigo_men") & ")"""
				if pID="" and pPag="" then
					pNodo=rsMenu("codigo_men")
				end if
				
				'**Ocultar menu padre: Procesos en linea para postgrado: YPEREZ 02/11/11 y educaci�n continua : yperez 20.02.12
				if (session("codigo_test")= 5 or session("codigo_test")= 6) and rsMenu("codigo_men")= 432 then 
				   EventoMenu = " style='display:none;'"
                end if
                '**                                               
				
			elseif rsMenu("enlace_men")<>"" or IsNull(rsMenu("enlace_men"))=false  then
			    if rsMenu("codigo_men") = 1097  then
                    EventoMenu=" onClick=""AbrirMenu('" & rsMenu("enlace_men") & "?cod=" & session("codigoUniverCifradoNet") & "')""" 
				    'Elseif rsMenu("codigo_men") = 1459 then 'colocar id segun tb menu aplicaciones
				Elseif rsMenu("codigo_men") = 1604 then 'colocar id segun tb menu aplicaciones
					    EventoMenu=" onClick=""javascript:MarcarMenu();top.parent.frames[2].location.href='" &  rsMenu("enlace_men") & "?al=" & session("AlumnoCifradoNet")  & "&cod=" & session("codigoUniverCifradoNet") & "'""" 
					    
				'========================================================================
				'No eliminar ni comentar este bloque, que se usa para los cursos de nivelacion de profesionalizacion
				'xDguevara 03.07.2013.
				Elseif rsMenu("codigo_men") = 1667 then '-->> Nivelacion
				    EventoMenu=" onClick=""AbrirMenu('" & rsMenu("enlace_men") & "?y=" & session("codigo_alu") & "')""" 
				'====================================================================================================================
				
				Elseif rsMenu("codigo_men") = 146 then '-->> ESTADO DE CUENTA ALUMNO
				    EventoMenu=" onClick=""AbrirMenu('" & rsMenu("enlace_men") & "?y=" & session("codigo_alu") & "')""" 
				'
				Elseif rsMenu("codigo_men") = 1794 then '-->> yperez eventos aulavirtual
                    EventoMenu=" onClick=""AbrirMenu('" & rsMenu("enlace_men") & "?y=" & session("codigo_alu")  & "')"""

				Elseif rsMenu("codigo_men") = 1824 then '-->> yperez postular beca
				    EventoMenu=" onClick=""AbrirMenu('" & rsMenu("enlace_men") & "?y=" & session("codigo_alu")  & "')""" 								

				Elseif rsMenu("codigo_men") = 1902 then  '-->> yperez horarios prof.
					   EventoMenu=" onClick=""AbrirMenu('" & rsMenu("enlace_men") & "?y=" & session("codigo_alu")  & "')"""
						
				else
			        EventoMenu=" onClick=""AbrirMenu('" & rsMenu("enlace_men") & "?modo=" & ct & "')""" 
			    end if 	
			    
				if pID="" and pPag="" then
					pID=rsMenu("codigo_men")
					pPag=rsMenu("enlace_men")
				end if
				
				'**Ocultar algunos menus hijo para postgrado YPEREZ 02/11/11 y educaci�n continua : yperez 20.02.12
				if (session("codigo_test")= 5 or session("codigo_test")= 6 )then
                   if(rsMenu("codigo_men") = 435 or rsMenu("codigo_men") = 1097 or rsMenu("codigoRaiz_men")=432 or rsMenu("codigo_men") = 939 or rsMenu("codigo_men") = 770) then
                        EventoMenu = " style='display:none;'"
                    end if			
                '===============================================================================
                'xDguevara 02.12.2012 -> Menu Nivelacion solo para profesionalizacion
                elseif session("codigo_test")<> 3 then 
                    if rsMenu("codigo_men") = 1667  then    '##  Modificar segun el codigo que se le asigne al Menu Nivelacion ##
                        EventoMenu = " style='display:none;'"
                    end if
                end if
                '===============================================================================
				'**	
				if (session("codigo_test")<>3) then
					if rsMenu("codigo_men") = 1902 then
					   EventoMenu = " style='display:none;'"
					end if 
				end if
				
			end if
			
			
			'====================================================================
			'Ocultar men�s hijo
			'====================================================================
			if (codigoRaiz_men=rsMenu("codigoRaiz_men") and codigoRaiz_men>0) then
				'EstadoMenu="style=""display:none"""
			else
				clase="class='bordeinf' style='font-weight:bold'"
			end if	
			
		'====================================================================

		'Imprimir Fila del men�

		'====================================================================

          
		if(session("codigo_cpf") = 24) then
                	'if(rsMenu("codigo_men") <> 1459) then
                	if(rsMenu("codigo_men") <> 1604) then
                    	response.write  "<tr class='Sel' Typ='Sel' " & EstadoMenu & " id='Mnu" & idPadre & "' onMouseOver=""Resaltar(1,this,'S','#DEE0C5')"" onMouseOut=""Resaltar(0,this,'S','#DEE0C5')"">"  & vbcrlf
                      	response.write "<td " & clase & EventoMenu & ">"
                      	response.write  cadena & "<img name='arrImgCarpetas' id='imgCarpeta" & rsMenu("codigo_men") & "' src='../images/" & ImagenMenu & "'>&nbsp;" & TextoMenu & vbcrlf
                      	response.write "</td>"                                             
                      	response.write  "</tr>" 
                	end if            
        else
                if(rsMenu("codigo_men") = 1604) then
                    
                end if
                response.write  "<tr class='Sel' Typ='Sel' " & EstadoMenu & " id='Mnu" & idPadre & "' onMouseOver=""Resaltar(1,this,'S','#DEE0C5')"" onMouseOut=""Resaltar(0,this,'S','#DEE0C5')"">"  & vbcrlf
                response.write "<td " & clase & EventoMenu & ">"
                response.write  cadena & "<img name='arrImgCarpetas' id='imgCarpeta" & rsMenu("codigo_men") & "' src='../images/" & ImagenMenu & "'>&nbsp;" & TextoMenu & vbcrlf
                response.write "</td>"                                             
                response.write  "</tr>" 	
        end if  
         	

			x=x+1
													
			CrearMenu codigo_apl,codigo_tfu,rsMenu("codigo_men"),x

            'MOSTRAR MENU FERIA LABORAL YPEREZ 07/11/11
            mostrarMenuFeria = 1
			if rsMenu("codigo_men")= 123 then                                       		                       
                  ' set rsFeria = Obj.Consultar("VerificarAccesoLaborum","FO",codigo_alu)             
                  ' if rsFeria.recordcount then
                  '    mostrarMenuFeria = 1                
                  ' end if
    
                   if mostrarMenuFeria = 1 then
                        response.Write("<tr onMouseOver='Resaltar(1,this,'S','#f8f8f8')' onMouseOut='Resaltar(0,this,'S','#f8f8f8')'>")
                        response.Write("<td width='100%' class='bordeinf' height='22'><img src='../images/menus/menu.gif' /> ")
                        response.Write("<a style='text-decoration:none;' href='pagina.htm' target = 'contenido' onclick='ocultar();'><b>Feria Laboral � Alumni USAT</b></a></td></tr>")                                     
                   end if            
            end if            
           'MOSTRAR MENU FERIA LABORAL YPEREZ 07/11/11
			rsMenu.movenext									
		next
end Sub
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>

<STYLE type="text/css">
<!--
	img {
	border:0px none;
	vertical-align: middle;
	}
	
.bloqueMenu {
	border-style: solid none solid none;
	border-width: 1px;
	border-color: #808080;
}
-->
</STYLE>
<script type= "text/javascript" language="Javascript">
    
    function AbrirMenu(pagina) {
        if (pagina != 'about:blank' && pagina != "") {
            /*if (pagina.indexOf('?')==-1){//Si no encuentra una referencia
            pagina=pagina + "?id=<%=session("codigo_usu")%>"
            }
            else{
            pagina=pagina + "&id=<%=session("codigo_usu")%>"
            }*/

            MarcarMenu()
            top.parent.frames[2].location.href = "cargando.asp?pagina=" + pagina
        }
    }


    function ExpandirNodo(item) {
        var idMenu = 0;
        var img = ""
        if (item.length == undefined) {
            idMenu = item.id
            img = document.getElementById("imgCarpeta" + idMenu.substring(3, idMenu.length));
            MostrarMenu(item, img)
        }
        else {
            idMenu = item[0].id
            img = document.getElementById("imgCarpeta" + idMenu.substring(3, idMenu.length));

            for (i = 0; i < item.length; i++) {
                MostrarMenu(item[i], img)
            }
        }
    }

    function MostrarMenu(Hijo, img) {
        if (Hijo.style.display != "none") {
            Hijo.style.display = "none"
            img.src = "../images/mas2.gif"
        }
        else {
            Hijo.style.display = ""
            img.src = "../images/menos2.gif"
        }
    }

    function MarcarMenu() {

        oRow = $(this).parent; //window.event.srcElement.parentElement;

        if (oRow.tagName == "TR") {
            AnteriorFila.Typ = "Sel";
            AnteriorFila.className = ""
            AnteriorFila = oRow;
        }
        if (oRow.Typ == "Sel") {
            oRow.Typ = "Selected";
            oRow.className = oRow.Typ;
        }
    }


</script>
<script type="text/javascript" src="js/prototype.js"></script>
<script type="text/javascript" src="js/scriptaculous.js?load=effects,builder"></script>
<!--<script type="text/javascript" src="js/lightbox.js"></script>-->
<link rel="stylesheet" href="css/lightbox.css" type="text/css" media="screen" />
<base target="_self">
</HEAD>
<%  if(session("cicloIng_alu") = "2012-I") then  %>
	<script>
		<!-- window.open("images/examen.medico.jpg?x=1", "mywindow", "location=1,status=0,scrollbars=1,menubar=0,  width=780,height=780"); -->
	</script>
<%	end if  %>

<%'if session("codigo_test")= 2 then %>
<%'end if%>

<% if session("foto_alu")=0 then %>
    <script>    
        <!-- alert('OBLIGATORIO: TOMA DE FOTOGRAF�A PARA CARNET DEL 01 AL 15 DE MARZO EN AULA 104 DE 10:00 AM A 01:00 PM');'-->
    </script>	    

<%end if %>

<body topmargin="0" leftmargin="0" bgcolor="#F8F8F8" style="border-right-style: solid; border-right-width: 1px;border-color:#767C4A" >
<table cellpadding=3 cellspacing=0 border=0 width="167%" heigth="100%" height="202">

<%
codigo_men=0
codigo_apl=8
codigo_tfu=3

Set obj=server.CreateObject("PryUSAT.clsAccesodatos")
	obj.Abrirconexion%>
<%IF session("codigo_usu")<>13379 then%>
	<%
		response.write  "<table cellpadding=3 cellspacing=0 border=0 width='100%' heigth='100%'>" & vbcrlf
		if session("codigouniver_alu") <> "" then
			Set RsRandom = Obj.Consultar("ConsultarAleatorio","FO")
			RsRandom.movefirst
			
			'***DESCOMENTAR PARA SUBIR AL REAL, comentar para ejecutar en server-test***
			'posible motivo: ClscifradoNet da�ada en server-test.
			'####(YPEREZ)###
			   'Dim objNEnc
			   'Set objNEnc = Server.CreateObject("PryCifradoNet.ClscifradoNet")
			   'codigocifrado = mid(RsRandom("aleatorio"),1,16) & objNEnc.Cifrado("1X9B3M" & cstr(session("codigouniver_alu")),mid(RsRandom("aleatorio"),1,16))
			   'alumnocifrado = mid(RsRandom("aleatorio"),17,32) & objNEnc.Cifrado(String(16-(Len(session("codigo_alu"))),"0") & session("codigo_alu"),mid(RsRandom("aleatorio"),17,32))
			   'session("codigoUniverCifradoNet") = lcase(codigocifrado)
			   'session("AlumnoCifradoNet") = lcase(alumnocifrado)
		    '#######
		end if
		
						
			if session("EgresadoAlumni")= 1 then 
			  codigo_tfu = 133
			end if
			CrearMenu codigo_apl,codigo_tfu,codigo_men,0
			%>

			<!--  Para mostrar avisos (sistemas)-->
			<%if session("codigo_cpf")=3 then %>
				<tr onMouseOver="Resaltar(1,this,'S','#f8f8f8')" onMouseOut="Resaltar(0,this,'S','#f8f8f8')">
				<td width="100%" class="bordeinf" height="25">
				<img alt="Microsoft" border="0" src="../images/msdn.jpg"> 
				<a href="http://msdn34.e-academy.com/ucstm_cins" target="contenido">Descarga Microsoft</a></td>
				</tr>
					  
			  <tr onMouseOver="Resaltar(1,this,'S','#f8f8f8')" onMouseOut="Resaltar(0,this,'S','#f8f8f8')">
				<td width="100%" class="bordeinf" height="25" align="right">
				<!--<img alt="Microsoft" border="0" src="../images/msdn.jpg"> -->
				<b>
				<a href="https://intranet.usat.edu.pe/campusvirtual/sinsyc/index.html" target="contenido">
                <font color="#FF6600">SINSYC: Ponencias y Fotos</font></a></b></td>
				</tr>
  		    <%end if%>
<%end if%>
			<%'IF session("codigo_usu")=13379 then%>
<!--			<tr onMouseOver="Resaltar(1,this,'S','#f8f8f8')" onMouseOut="Resaltar(0,this,'S','#f8f8f8')" >-->
<!--			<td width="100%" class="bordeinf" height="22"> -->
<!--			<img alt="Bibliotecas Virtuales" border="0" src="../images/libroabierto.gif"> -->
<!--			<a href="bibliotecas.asp?bib=2" target="contenido"><b>Bibliotecas Virtuales</b></a></td> -->
<!--			</tr> -->
			
			<%'end if%>
<!--			<tr onMouseOver="Resaltar(1,this,'S','#f8f8f8')" onMouseOut="Resaltar(0,this,'S','#f8f8f8')" > -->
<!--			<td width="100%" class="bordeinf" height="22"> -->
<!--			<img alt="Biblioteca ALEPH" border="0" src="../images/librocerrado.gif"> -->
<!--			<a href="http://intranet.usat.edu.pe:8991/F" target="_blank"><b>Biblioteca ALEPH</b></a></td> -->
<!--			</tr>-->
<!--			<tr onMouseOver="Resaltar(1,this,'S','#f8f8f8')" onMouseOut="Resaltar(0,this,'S','#f8f8f8')" > -->
<!--			<td width="100%" class="bordeinf" height="22"> -->
<!--			<img alt="Biblioteca ALEPH" border="0" src="../images/librocerrado.gif">  -->
<!--			<a href="http://intranet.usat.edu.pe:8991/F" target="_blank"><b>Biblioteca ALEPH</b></a></td> -->
<!--			</tr> -->

           
            
            
			<tr onMouseOver="Resaltar(1,this,'S','#f8f8f8')" onMouseOut="Resaltar(0,this,'S','#f8f8f8')" onClick="cerrarSistema('../cerrar.asp?Decision=Si')">

			<td width="100%" class="bordeinf" height="22">
			<img alt="Cerrar sistema" border="0" src="../images/cerrar.gif"> 
			Cerrar sistema</td>
			
			</tr>
			


<%	

	if session("codigo_cpf")=25 then 'Programas Especiales	
		'recuperar el codigo del servicio del programa para mostrar mensajes
		Set rs=Obj.Consultar("consultarServicioProgramaEspecial","FO",session("codigo_Alu"))
	
		if rs.recordcount >0 then
		
			if rs("codigo_Sco")=869 OR rs("codigo_Sco")=467 OR rs("codigo_Sco")=468  then 'Prof. Sistemas
			
			
				if date <= cdate("31/12/2009") and rs("edicionProgramaEspecial_alu")="1" then
				%>
					<script>
					    AbrirPopUp('I_7toCiclo.htm', '550', '750', 'no', 'no', 'yes')
					</script>

				<%else
					if date <= cdate("09/01/2010") and rs("edicionProgramaEspecial_alu")="2" then%>
				
					<script>
					    AbrirPopUp('2Ivciclo.htm', '550', '750', 'no', 'no', 'yes')
					</script>


					<% end if

				end if%>

			 
			 <tr onMouseOver="Resaltar(1,this,'S','#f8f8f8')" onMouseOut="Resaltar(0,this,'S','#f8f8f8')">
				<td width="100%" class="bordeinf" height="25">
				<img alt="Microsoft" border="0" src="../images/msdn.jpg"> 
				<a href="http://msdn34.e-academy.com/ucstm_cins" target="contenido">Descarga Microsoft</a></td>
				</tr>
			 
		<%  end if
		
		
			if rs("codigo_Sco")=469 OR rs("codigo_Sco")=470 OR rs("codigo_Sco")=471 then 'Prof. Industrial
			
			
				if rs("edicionProgramaEspecial_alu")="1" then 'date <= cdate("31/12/2009") and rs("edicionProgramaEspecial_alu")="1" then
				%>
					<script>
					    AbrirPopUp('HorariosIND/Programacion2010_ IProg.htm', '550', '850', 'no', 'no', 'yes')
					</script>

				<%else
					if rs("edicionProgramaEspecial_alu")="2" then  'date <= cdate("09/01/2010") and rs("edicionProgramaEspecial_alu")="2" then
				%>
					<script>
					    AbrirPopUp('HorariosIND/Programacion2010_IIProg.htm', '550', '850', 'no', 'no', 'yes')
					</script>

				        <%else
						if rs("edicionProgramaEspecial_alu")="3" then  
					%>
					<script>
					    AbrirPopUp('HorariosIND/Programacion_Anual2010_III_Programa.htm', '550', '850', 'no', 'no', 'yes')
					</script>


						<% end if

				   	end if

				end if%>

			 
			
			 
		<%  end if
		%>
	


		<% end if%>
	
		<%end if%>
		
			<%'votacion SISTEMAS EN CASO QUIERAN QUE ESTE EN EL MEN�
if session("codigo_cpf")=103 then %>

			  <tr onMouseOver="Resaltar(1,this,'S','#f8f8f8')" onMouseOut="Resaltar(0,this,'S','#f8f8f8')">
				<td width="100%" class="bordeinf" height="30" align="center">
				<!--<img alt="Microsoft" border="0" src="../images/msdn.jpg"> -->
				<b>
				<a href="https://intranet.usat.edu.pe/campusvirtual/librerianet/academico/votacionalumno.aspx?codigo_alu=<%=session("codigo_usu")%>" target="contenido">
                <font color="#FF6600">VOTACI�N PARA REPRESENTANTE</font></a></b></td>
				</tr>

<%end if%>


<%if session("codigo_cpf")=5 or session("codigo_cpf")=6 or session("codigo_cpf")=7 or session("codigo_cpf")=11 or session("codigo_cpf")=14 or session("codigo_cpf")=16 or session("codigo_cpf")=17 or session("codigo_cpf")=26 or session("codigo_cpf")=38 or session("codigo_cpf")=124  or session("codigo_cpf")=174 then %>

<script>
    AbrirPopUp('https://intranet.usat.edu.pe/campusvirtual/estudiante/avisoAcredita.asp', '850', '850', 'no', 'no', 'yes')
</script>

<%end if%>
		
		<% response.write  "</table>"
		obj.CerrarConexion
		set obj=nothing
%>

<% ' la votaci�n de la escuela de SISTEMAS - PARA POP UP

'IF  (session("codigo_cpf")="3" )THEN
'pagina ="../librerianet/academico/votacionalumno.aspx?codigo_alu=" & session("codigo_usu") 
%>
	    <script type="text/javascript">
	        //window.open("<%=pagina%>", "Votaci�n")
	        //var izq = (screen.width - ancho) / 2
	        //var arriba = (screen.height - alto) / 2
	        //window.open("<%=pagina%>", "Votaci�n", "height=700,width=700,statusbar='no',scrollbars='no',top=" + arriba + ",left=" + izq + ",resizable='no',toolbar=no,menubar=no");
	        
	    </script>
<%'end if%>

<%' Para encuestas 
'IF  session("codigo_cpf")="32" THEN
	'Set obj=server.CreateObject("PryUSAT.clsAccesodatos")
	'obj.Abrirconexion
	'Set rsAlu=obj.Consultar("ENC_ConsultarCursosVeranoArq", "ST", session("codigo_Alu"))
	'obj.cerrarconexion
	'If not (rsAlu.BOF = false and rsAlu.EOF = false) then 
		'Set objEnc=server.CreateObject("EncriptaCodigos.clsEncripta")
        'pagina ="../librerianet/encuesta/escuela/CursosElectivosArquitectura.aspx?x=" & objEnc.Codifica("069" & session("codigo_alu"))
%>
	    <script type="text/javascript">
	        //var izq = (screen.width - ancho) / 2
	        //var arriba = (screen.height - alto) / 2
	        //window.open("<%'=pagina%>", "Encuesta", "height=580,width=700,statusbar='no',scrollbars='no',top=" + arriba + ",left=" + izq + ",resizable='no',toolbar=no,menubar=no");
	        //AbrirPopUp2("<%'=pagina%>", '300', '650', 'no', 'no', 'yes', "1")
	    </script>
<%	'end if 
    'set rsalu = nothing
    'set obj = nothing
    'set objEnc = nothing
'END IF 	
%>

<%
'Para aviso de Direcci�n de Pensiones CARNET
Set obj1=server.CreateObject("PryUSAT.clsAccesodatos")
	obj1.AbrirConexion
	set rsCarnet = obj1.Consultar("AVI_VerificaCanet20092","FO",session("codigo_Alu"))
	obj1.CerrarConexion
	if rsCarnet.RecordCount>0 then %>
 <script>
     //AbrirPopUp2('avisos/carnet/avisocarnet.html','500','500','yes','yes','yes','Pensiones')
 </script>


<%	end if

'Para aviso de escuela PRE
'Set obj1=server.CreateObject("PryUSAT.clsAccesodatos")
'	obj1.AbrirConexion
'	avisoPre= obj1.Ejecutar("AVI_ConsultarParaAviso",true,"am",session("codigo_Alu"),"EXP_PRE",0,"")
'	obj1.CerrarConexion
'	if avisoPre="SI" then %>
 <script>

     //AbrirPopUp2('avisos/pre/EXPEDIENTE/mensaje.html','400','650','yes','yes','yes','exp_pre')
   //  top.location.href = 'avisos/pre/EXPEDIENTE/mensaje.html'	 
 </script>
<%'	end if




If Err.Number<>0 then
    session("pagerror")="estudiante/izq.asp"
    session("nroerror")=err.number
    session("descripcionerror")=err.description    
	'response.write("<script>top.location.href='../error.asp'</script>")
End If

%>


<% 'IF session("codigo_usu")=11268 then%>
	<!--<tr onMouseOver="Resaltar(1,this,'S','#f8f8f8')" onMouseOut="Resaltar(0,this,'S','#f8f8f8')" 
		<td width="100%" class="bordeinf" height="22">
		<a href="inscripciones/ingenieria/semanaING/inscripcionEventoGeneral.asp" target="contenido"><b>Exp.Derecho</b></a></td>
			</tr>-->
<% 'End if%>


<tr onMouseOver="Resaltar(1,this,'S','#DEE0C5')" onMouseOut="Resaltar(0,this,'S','#DEE0C5')" onClick="cerrarSistema('../cerrar.asp?Decision=Si')">
<td width="100%" class="bordeinf" height="14">
</td>
</tr>


</table>
<center>
<!--<a id="first" href="images/lightboxindex.jpg" rel="lightbox[roadtrip]"><img src="images/lightboxindex.jpg" title="Recomendaciones"  width="100" height="120"/></a>-->
</center>
<%  'Mostrar Alerta de Pagos
    'Set obj=server.CreateObject("PryUSAT.clsAccesodatos")
	'obj.AbrirConexion
	'Set rsCuenta=Obj.Consultar("consultarAlertaPagoBanco","FO",session("codigo_Alu"))
    'if trim(rsCuenta("debe"))="1" then
    %>
    <script>
       // AbrirPopUp('estadocuenta.asp?modo=TE', '600', '800', 'no', 'no', 'yes')
	</script>
    <%
    'end if
    'obj.CerrarConexion
    'set obj=nothing
    %>    
    <%
    '	MOSTRAR ALERTA DE INFRAESTRUCTURA
    
	Set objInf=server.CreateObject("PryUSAT.clsAccesodatos")
	objInf.Abrirconexion
	Set	DebeInfra = objInf.Consultar("CAJ_ConsultarDeudaInfraestructura", "FO", session("codigo_Alu"))
	objInf.cerrarconexion
	IF DebeInfra("INF") ="SI"  THEN
    %>
    <script>
        AbrirPopUp('mensajeInfraestructura.asp', '400', '300', 'no', 'no', 'yes')
	</script>
    <%	END IF
    
	
     'MOSTRAR AVISO DE EXAMEN MEDICO MEDICINA 20.02.15 YPEREZ
    
    IF session("codigo_cpf") = 24 then %>
    <script>
        //AbrirPopUp('examenmedicomedicina.asp', '250', '600', 'no', 'no', 'yes')
        //AbrirPopUp('escuelamedicinaessalud.asp', '550', '500', 'no', 'no', 'yes')
        //AbrirPopUp('escuelamedicinauptodate.asp', '350', '500', 'no', 'no', 'yes')
	</script>
    <%
    end if 
    
    %>
		<script>
			AbrirPopUp('http://www.usat.edu.pe/campusvirtual/estudiante/avisoegresado.htm?aq=2', '400', '800', 'no', 'no', 'yes')
		</script>

	<%   
    %>
	
	
	
	<% 
	'[ MUESTRA VENTANA PARA ACTUALIZACI�N DE DATOS - ALUMNI - 2016-06-23 MVILCHEZ]
	Set objActDatos=server.CreateObject("PryUSAT.clsAccesodatos")
	objActDatos.Abrirconexion
	Set	li_Dato = objActDatos.Consultar("ALUMNI_ConsultarActualizacionDatos", "FO", session("codigo_Alu"))
	objActDatos.cerrarconexion
	IF li_Dato("dato") = 0  THEN
	%>		
	<script>
		'AbrirPopUp('../librerianet/Egresado/campus/frmActualizarDatos.aspx', '400', '600', 'no', 'no', 'yes')
	</script>
	<%	end if %>
	

 <script>

  
 </script>
</body>
</html>