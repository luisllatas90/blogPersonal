<%
Dim codigo1, codigo2
codigo1 = session("codigo_usu2") 
codigo2 = request.querystring("id")

function vige(texto,clave,ty)
  dim alfabeto 
	alfabeto =	"ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"	
	texto = UCase(texto)
  clave = UCase(clave)		
	
  dim cla 
	cla = clave		
  Do while Len(clave)<Len(texto) 
		 clave = clave & cla
	loop
		
	dim result 
	result = ""
	
	dim i 
	
	response.write ("<div style=""display:none;""> Len(texto) = " & Len(texto) & "</div>")
	i = 1
	 Do while  i < Len(texto) 		
		  if mid(texto,i,1)= " " then		 		
				   result = result &  mid(texto,i,1)		  
	      End If
	
	 dim idx	
	
	 idx = Instr(alfabeto, mid(texto,i,1))
	 if idx < 0 then
	
			result = result & mid(texto,i,1)
		else 
	
			 k = Instr(alfabeto, mid(clave,i,1))
			 if ty = true then
			  idx = idx + k
			 else
				idx = idx + (Len(alfabeto)- k)
			 end if
			 dim pos 
			 pos = idx mod Len(alfabeto)
			 result = result &  mid(alfabeto,pos,1)
		end if
		  i = i + 1
	 Loop			
	vige = result
end function


 function cifrado(texto,clave)
  cifrado = vige(texto,clave,true) 
 end function
 


if (cint(codigo1) = cint(codigo2)) then 
	 Dim clave 
	 Dim Hora
	 Dim pso
	 Dim psoCifrado
	 Dim PassPer
	 Dim PassPerCif

	Dim Obj , ObjBD
	Dim RS
	'Set Obj = Server.CreateObject("PryCifradoNet.ClscifradoNet")
	Set ObjBD = Server.CreateObject("PryUSAT.clsAccesoDatos")

		 ObjBD.AbrirConexion
		 set Rs = objbd.Consultar("MOODLE_ConsultarCodigoAcceso","FO","PE",cint(codigo1))
		 ObjBD.CerrarConexion
	
		if Not (rs.eof and rs.bof) then
			Rs.Movefirst
			pso = Rs("codigo_pso")
			PassPer = Rs("ClaveInterna_Pso")
		end if
		
	' 'pso = "123456789" & trim(cstr(pso))
	' hora = cstr(replace( replace(replace( replace(NOW,"/",""),":","") ," ",""),".",""))

	' hora = StrReverse(hora)


	  ' Randomize
	  ' clavecifrada = cifrado(cstr(hora),cstr(Int(((1000) * Rnd) + 1)))
		
		' 'response.write "v1=" & clavecifrada
	   ' Randomize
	 ' valor2 = cifrado(cstr(hora),cstr(Int(((1000) * Rnd) + 1)))
	  ' Randomize
	  ' valor3 = cifrado(cstr(hora),cstr(Int(((1000) * Rnd) + 1)))
	
	 ' valor4 = cifrado(valor2,valor3)
		
	' psoCifrado = cifrado(trim(cstr(pso)), cstr(clavecifrada))
	' PassPerCif = cifrado(trim(cstr(PassPer)),cstr(clavecifrada) )
	
	
	
 


%>
	<html>
	<head>
		<script type="text/javascript" src="javascript/pdfobject.js"></script> 
		<style type="text/css">
		<!--

		#pdf1, #pdf2 {
			width: 800px;
			height: 500px;
			margin: 2em auto;
			border: 2px solid #6699FF;
		}

		#pdf1 p, #pdf2 p {
			 padding: 1em;
		}

		#pdf1 object, #pdf2 object {
			 display: block;
			 border: solid 1px #666;
		}

		-->
		</style>
	</head>
	<body>
	<center>
	<FORM action="https://intranet.usat.edu.pe/aulavirtual/login/index.php" method="POST" target="_blank"> <!--REAL-->
	<!--<FORM action="http://10.10.14.28/aulavirtual/login/index.php" method="POST" target="_blank">-->	<!--PRUEBAS-->
	 </br>
	<INPUT Type="submit" name= "Enviar" Value="Clic para Ingresar" disabled =disabled />
	<INPUT Type="Hidden" name= "avm1" Value="<% Response.write(psoCifrado)   	'Codigo_pso
												%>"/>
	<INPUT Type="Hidden" name= "avm2" Value="<% Response.write(valor2) 			'valor disuasivo 
												%>"/>
	<INPUT Type="Hidden" name= "avm3" Value="<% Response.write(clavecifrada) 	'Clave Cifrada
												%>"/>
	<INPUT Type="Hidden" name= "avm4" Value="<% Response.write (valor3) 		'Valor disuasivo
												%>"/>
	<INPUT Type="Hidden" name ="avm5" Value="<% Response.write (PassPerCif) 		'Valor de clave de Persona
												%>"/>
	<INPUT Type="Hidden" name ="avm6" Value="<% Response.write (valor4) 		'cifrado de siuasivo2 y disuasivo3
												%>"/>
												
												<INPUT Type="Hidden" name ="avm7" Value="<% Response.write (pso) 		'cifrado de siuasivo2 y disuasivo3
												%>"/>
												<INPUT Type="Hidden" name ="avm8" Value="<% Response.write (PassPer) 		'cifrado de siuasivo2 y disuasivo3
												%>"/>
	</FORM>
	<br /><br />
	
	

<div id="pdf1">Tu navegador necesita soporte para PDF<br/> <a href="moodle/manual.asistencias.pdf">Registrar Asistencias en el Aula Virtual</a></div>

<div id="pdf2">Tu navegador necesita soporte para PDF<br/> <a href="moodle/actividad.no.en.linea.pdf">Registrar actividades no en línea ( notas parciales )</a></div>
	
	
	
	</center>
	<script type="text/javascript"> 
	/*<![CDATA[*/
	
  function embedPDF(){

    var asistencias = new PDFObject({ 
      url: '../../personal/moodle/actividades.asistencia.pdf'
    }).embed('pdf1'); 
    
		var parciales = new PDFObject({ 
      url: '../../personal/moodle/tareas.no.en.linea.pdf'
			}).embed('pdf2'); 

  }

  window.onload = embedPDF; 
	
	
	/*
	window.onload = function (){
		var pdf = new PDFObject({ url: "http://www.usat.edu.pe/campusvirtual/personal/moodle/actividades.asistencia.pdf" });
		pdf.embed("pdf1");
		
		var pdf2 = new PDFObject({ url: "moodle/tareas.no.en.linea.pdf" });
		pdf2.embed("pdf2");
	}
	
	*/
	
	/*]]>*/
</script> 
	
	</body></html>

<%
 else
	 response.write("Acceso Denegado")
 end if
 

%>