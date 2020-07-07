<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Pagina nueva 1</title>
<link href="private/lytebox.css" rel="stylesheet" type="text/css" />

</head>

<body>
<%


esc=session("codigo_cpf")


if esc=25 then 'Programas Especiales	
		'consultar el plan de estudios
		Set obj=server.CreateObject("PryUSAT.clsAccesodatos")
	    obj.Abrirconexion
		Set rs=Obj.Consultar("ConsultarPlanEstudio","FO","AL", session("codigo_Alu"),"")
		'alumnoProgramaISYC=obj.Ejecutar("PRO_ConsultarAlumnosPorPrograma", true, 3, session("codigo_alu"), 129, 0)
	    obj.cerrarconexion
		if rs.recordcount >0 then
		
			if rs("codigo_pes")=129 or rs("codigo_pes")=134 then  '129: PP. Sistemas, 134: PP.Industrial
			     esc = 3 ' le pongo 3 para que forme parte de la facultad de ingenieria 			    
			end if
		end if
		
		rs.close
		set rs=nothing
		set obj= nothing
		
end if		

'IF alumnoprogramaISYC = true THEN
'	response.redirect ("avisoprograma/avisoProgramaISYC.html")
'ELSE
	
	'----------------------------------------------------------------------
    'Fecha: 29.10.2012
    'Usuario: yperez
    'Motivo: Cambio de URL del servidor de la WebUSAT [www.usat.edu.pe->intranet.edu.pe]
    '----------------------------------------------------------------------

select case esc
	'------------------------------------------------
	' Facultad de Ciencias Empresariales
	' Escuela de Administracióm
	case 1 : response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/empresariales/escAdministracion/index.html")	
	' Escuela de Contabilidad
	case 4 : response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/empresariales/escContabilidad/index.html")	
	'Escuela de Economía
	case 21: response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/empresariales/escEconomia/index.html")
	'Escuela de Adm. Hotelera
	case 34: response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/empresariales/escHotelera/index.html")
	'------------------------------------------------
	
	'Escuela y Facultad de derecho
	case 2 : response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/derecho/escDerecho/index.html")
	'Escuela de Ingeniería de sistemas y computaci{on
	'------------------------------------------------

	'Facultad de Humanidades
	case 3 : response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/ingenieria/escSistemas/index.html")
	' Escuela de Educación todas sus especialidades
	case 5,6,7,812,14,15,16,17,26,38: response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/humanidades/escEducacion/index.html")
	'Escuela de Ciencias de la Comunicación
	case 20: response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/humanidades/escComunicacion/index.html")	
	'------------------------------------------------

	'Facultad de Ingeniería
	'Escuela de Ingeniería Industrial	
	case 22: response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/ingenieria/escIndustrial/index.html")
	'Escuela de Ingeniería Naval
	case 23: response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/ingenieria/escNaval/index.html")	
	'Escuela de Ingeniería Civil y Ambiental
	case 28: response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/ingenieria/escCivil/index.html")	
	'Escuela de Ingeniería Energética
	case 30: response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/ingenieria/escEnergetica/index.html")		
	'Escuela de Arquitectura
	case 32: response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/ingenieria/escArquitectura/index.html")	
	'Escuela de Mecánica Eléctrica
	case 33: response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/ingenieria/escMecanica/index.html")
	'------------------------------------------------
	
	'Facultad de Medicina
	'Escuela de Medicina
	case 24: response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/medicina/escMedicina/index.html")	
	'Escuela de Psicolgía
	case 27: response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/medicina/escPsicologia/index.html")
	' Escuela de Enfermería
	case 11: response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/medicina/escEnfermeria/index.html")
	'Escuela de Odontología
	case 31: response.redirect ("https://intranet.usat.edu.pe/usat/avisoestudiante/medicina/escOdontologia/index.html")
		
	case else
		response.write ("<br><br><center><h2>Bienvenido a tu Campus Virtual!!</h2></center>")
end select

'end if   
%>
</body>

</html>