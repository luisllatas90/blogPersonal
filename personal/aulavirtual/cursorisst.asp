﻿<%
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
		

%>
	<html>
	<head>
	</head>
	
	
	<style type="text/css">
       #wrapper
       {
            background-image: url('img/fondoseg.jpg');
            background-repeat: no-repeat;
            background-size: cover;
            height:100vh;
            padding-top: 36px;
        }
	</style>
	
	<script type="text/javascript">
	
	     function loginMoodleCurso() {
	         document.getElementById('curso').value = 'RISST';
          document.formulario.submit()
      } 
      
</script>

	<body>
	<center>	
	
	<form id="formulario" action="//intranet.usat.edu.pe/aulavirtual/login/index.php" method="POST" target="_blank"> <!--REAL-->		    	
		
    <div id="wrapper"> 	
	<div style="padding-top:240px">
	<table border=0 style="text-align:center;">
	<tr>
	<td>	    
	    <input type="image" src="img/btn3.png" onclick="loginMoodleCurso();" style="width:250px; height:60px;">
	</td>	
	</tr>
	<tr>
	    <td colspan="2"><br /><h3>¿Consultas sobre Aula Virtual?</h3></td>
	</tr>
	<tr>
	    <td colspan="2">
	    <center>
	    <table>
	        <tr><td><img alt="mail" src="img/mail.png" style="height: 36px; width: 36px" /></td><td>serviciosti@usat.edu.pe</td></tr>
	        <tr><td><img alt="fono" src="img/fono.png" style="height: 36px; width: 36px" /></td><td> Anexo N° 4050</td></tr>
	    </table>
	    </center>	    	    
        </td>
	</tr>
	<tr>
	<td colspan="2">
	    <br /><!--
	   <a href="https://www.youtube.com/embed/videoseries?list=PLz0j26I3QIKI5KlC3St1qZE_B1hTH3vLd&amp;showinfo=0" target="_blank">
            <img alt="video" src="img/video.png" />
        </a>
	   -->
	</td>
	</tr>
	</table>
	</div>
	
	
	<INPUT Type="Hidden" name= "avm1" Value="<% Response.write(psoCifrado)   	'Codigo_pso
												%>"/>
	<INPUT Type="Hidden" name= "avm2" Value="<% Response.write(valor2) 			'valor disuasivo 
												%>"/>
	<INPUT Type="Hidden" name= "avm3" Value="<% Response.write(clavecifrada) 	'Clave Cifrada
												%>"/>
	<INPUT Type="Hidden" name= "avm4" Value="<% Response.write (valor3) 		'Valor disuasivo
												%>"/>
	<INPUT Type="Hidden" name ="avm5" Value="<% Response.write (PassPerCif) 	'Valor de clave de Persona
												%>"/>
	<INPUT Type="Hidden" name ="avm6" Value="<% Response.write (valor4) 		'cifrado de siuasivo2 y disuasivo3
												%>"/>
												
	<INPUT Type="Hidden" name ="avm7" Value="<% Response.write (pso) 		'cifrado de siuasivo2 y disuasivo3
												%>"/>
	<INPUT Type="Hidden" name ="avm8" Value="<% Response.write (PassPer) 	'cifrado de siuasivo2 y disuasivo3
												%>"/>
												
    <INPUT Type="Hidden" id="curso" name ="curso" value="no"/>
    
												
																								
    </div>
 											
	</FORM>		
	
    </center>
	</body>
	</html>

<%
 else
	 response.write("Acceso Denegado")
 end if
 

%>