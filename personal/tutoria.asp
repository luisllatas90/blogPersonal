<%
misesion = Request.QueryString("id")
'miruta1  = "https://intranet.usat.edu.pe/tutoria/index.php"
' treyes20.08.14 para accesos al módulo de tutoría v2
miruta2  = "https://intranet.usat.edu.pe/tutoria/application"

' Lista de Cordinadores y Tutores Registrados
if (Request.QueryString("modo") = 1) then 
	voy_a = miruta1
end if 
' Alta de tutores
if (Request.QueryString("modo") = 2) then 
	voy_a = miruta1 & "/listaprofes"
end if 
' Baja de tutores
if (Request.QueryString("modo") = 3) then 
	voy_a = miruta1 & "/listaprofes/baja"
end if 
' Lista de tutores y sus alumnos (matching)
if (Request.QueryString("modo") = 4) then 
	voy_a =  miruta1 & "/gestionaalumnos/listatutoresalumnos"
end if 
' Lista de tutores registrados
if (Request.QueryString("modo") = 5) then 
	voy_a =  miruta1 & "/gestionatutors/listatutores"
end if 
'Alta de tutores
if (Request.QueryString("modo") = 6) then 
	voy_a =  miruta1 & "/gestionatutors"
end if 
'Asignar estudiantes a tutores
if (Request.QueryString("modo") = 7) then 
	voy_a =  miruta1 & "/gestionaalumnos"
end if 
'Registrar tutorias
if (Request.QueryString("modo") = 8) then 
	voy_a =  miruta1 & "/gestionatutorias" 
end if 
' Listado de asesorias de un profesor
if (Request.QueryString("modo") = 9) then 
	voy_a =  miruta1 & "/gestionatutorias/listadoasesorias"
end if 
' Listado de aseorias de un estudiante
if (Request.QueryString("modo") = 10) then 
	voy_a =  miruta1 & "/gestionatutorias/listadoasesoriasestudiante"
end if 

'ver todas la tutorias

if (Request.QueryString("modo") = 11) then 
 voy_a =  miruta1 & "/gestionatutorias/listatodotutorias"
end if 

'dar de baja a una sola tutoria de un estudiante

if (Request.QueryString("modo") = 12) then 
 voy_a =  miruta1 & "/gestionatutorias/bajaunatutoria"
end if 

'dar de baja a todas la tutorias de un profesor

if (Request.QueryString("modo") = 13) then 
 voy_a =  miruta1 & "/gestionatutorias/bajatutoriasdeprofe"
end if 

'ver tutorias de una escuela 

if (Request.QueryString("modo") = 14) then 

 voy_a =  miruta1 & "/gestionatutorias/tutxescuela"

end if 

'Asignar cualquier alumno a cualquier tutor (sin importar escuela)

if (Request.QueryString("modo") = 15) then 

 voy_a =  miruta1 & "/gestionatutorias/asignacionespecial"

end if
' treyes20.08.14 para accesos al módulo de tutor+ia v2

if (Request.QueryString("modo") = 16) then 
 voy_a =  miruta2 & "/tutoradoMotivo"
end if

if (Request.QueryString("modo") = 17) then 
 voy_a =  miruta2 & "/tutoriaMotivo"
end if

if (Request.QueryString("modo") = 18) then 
 voy_a =  miruta2 & "/tutoriaProgreso"
end if

if (Request.QueryString("modo") = 19) then 
 voy_a =  miruta2 & "/tutoriaReferencia"
end if

if (Request.QueryString("modo") = 20) then 
 voy_a =  miruta2 & "/coordinador"
end if

if (Request.QueryString("modo") = 21) then 
 voy_a =  miruta2 & "/tutor"
end if

if (Request.QueryString("modo") = 22) then 
 voy_a =  miruta2 & "/tutorado"
end if

if (Request.QueryString("modo") = 23) then 
 voy_a =  miruta2 & "/tutoria"
end if

if (Request.QueryString("modo") = 24) then 
 voy_a =  miruta2 & "/tutorEspecialista"
end if

if (Request.QueryString("modo") = 25) then 
 voy_a =  miruta2 & "/derivacion"
end if

if (Request.QueryString("modo") = 26) then 
 voy_a =  miruta2 & "/tutoriaEspecialista"
end if

%> 
<body onload='document.formulario.submit()'> 
<form action='<% response.write(voy_a) %>' method='post' name='formulario'> 
<input type='hidden' name='ajg_xjrskw1pw5jgi' value='<% response.write(misesion) %>'> 
</body> 
