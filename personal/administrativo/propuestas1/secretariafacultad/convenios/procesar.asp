<!--#include file="../../../../../funciones.asp"-->
<Head>
<script>
		function popUp(URL) {
		day = new Date();
		id = day.getTime();
		var izq = 300//(screen.width-ancho)/2
		//alert (izq)
		var arriba= 200//(screen.height-alto)/2
		eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=400,height=350,left = "+ izq +",top = "+ arriba +"');");
		}	
</script>
<%
on error resume next

accion=Request.querystring("accion")
denominacion=Request.querystring("denominacion")
Ambito =Request.querystring("Ambito")
modalidad =Request.querystring("modalidad")
descripcion =Request.querystring("descripcion")
Duracion =Request.querystring("Duracion")
Periodo =Request.querystring("Periodo")
FechaInicio =Request.querystring("FechaInicio")
Renovacion =Request.querystring("Renovacion")
NumCopias =Request.querystring("NumCopias")
Observacion =Request.querystring("Observacion")
Responsable =Request.querystring("Responsable")
Referencia =Request.querystring("Referencia")
remLen =Request.querystring("remLen")
resolucion=Request.querystring("resolucion")
NameResolucion=Request.QueryString("NameResolucion")
NameReferencia=Request.QueryString("NameReferencia")
NameResponsable=Request.QueryString("NameResponsable")		
codigo_cni=Request.QueryString("codigo_cni")
modifica=Request.QueryString("modifica")


if Duracion="" then
	Duracion=null
	Periodo=null
end if
if NumCopias="" then
	NumCopias=0
end if
if Observacion="" then
	Observacion=null
end if
if Responsable=""then 
	Responsable=null
end if
if Referencia="" then
	Referencia=null
end if
if resolucion="" then
	resolucion=null
end if

if accion="guardar" then

	Set objConvenio=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objConvenio.AbrirConexionTrans
		codigo_cni = objConvenio.ejecutar("RegistraConvenioInstitucional",true,denominacion,Ambito,modalidad,descripcion,Duracion,Periodo,FechaInicio,Renovacion,Observacion,NumCopias,null,Referencia,resolucion,0)
		objConvenio.ejecutar "RegistraBitacoraConvenios",false,codigo_cni,session("Usuario_bit"),"I","Registro de Convenio: " & codigo_cni & " Denominación: " & denominacion
''//	( [denominacion_Cni], [codigo_Amc], [codigo_Mdc], [objetivos_Cni], [duracion_Cni],[periodoDuracion_Cni], [fechaInicio_Cni], [renovacion_Cni], [observacion_Cni], [numCopias_Cni], [archivoPdf_Cni], [referencia_Cni], [resolucion_Cni], [fechaRegistro], [usuarioRegistro], [fechaModificacion], [usuarioModificacion]) 																		
	if Responsable<>"" then
		objConvenio.ejecutar "RegistraResponsableConvenio",false,"NU",codigo_cni,Responsable
		objConvenio.ejecutar "RegistraBitacoraConvenios",false,codigo_cni,session("Usuario_bit"),"I","Registro Nuevo Responsable: " & Responsable		
	end if
	objConvenio.CerrarConexionTrans	
	Set objConvenio=nothing
	response.redirect "registra_convenio.asp?codigo_cni=" & codigo_cni & "&denominacion=" & denominacion & "&Ambito=" & Ambito & "&modalidad=" & modalidad & "&descripcion=" & descripcion & "&Duracion=" & Duracion & "&Periodo=" & Periodo & "&FechaInicio=" & FechaInicio & "&Renovacion=" & Renovacion & "&NumCopias=" & NumCopias & "&accion=guardar" & "&Observacion=" & Observacion & "&Responsable=" & Responsable & "&Referencia=" & Referencia & "&remLen=" & remLen & "&resolucion=" & resolucion & "&NameResponsable=" & NameResponsable & "&NameReferencia=" & NameReferencia & "&NameResolucion="  & NameResolucion  & "&modifica=" + modifica
	
end if

if accion="modificar" then

	Set objConvenio=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objConvenio.AbrirConexionTrans
		''response.write codigo_cni
		objConvenio.ejecutar "ActualizaConvenioInstitucional",true,codigo_cni,denominacion,Ambito,modalidad,descripcion,Duracion,Periodo,FechaInicio,Renovacion,Observacion,NumCopias,Referencia,resolucion,0
		objConvenio.ejecutar "RegistraBitacoraConvenios",false,codigo_cni,session("Usuario_bit"),"A","Actualiza convenio: " & denominacion							
''//	( [denominacion_Cni], [codigo_Amc], [codigo_Mdc], [objetivos_Cni], [duracion_Cni],[periodoDuracion_Cni], [fechaInicio_Cni], [renovacion_Cni], [observacion_Cni], [numCopias_Cni], [archivoPdf_Cni], [referencia_Cni], [resolucion_Cni], [fechaRegistro], [usuarioRegistro], [fechaModificacion], [usuarioModificacion]) 																		
	if Responsable<>"" then
		if Request.QueryString("modifica")="1" then
			objConvenio.ejecutar "RegistraResponsableConvenio",false,"MO",codigo_cni,Responsable		
		objConvenio.ejecutar "RegistraBitacoraConvenios",false,codigo_cni,session("Usuario_bit"),"M","Modifica Responsable: " & Responsable					
		else
			objConvenio.ejecutar "RegistraResponsableConvenio",false,"NU",codigo_cni,Responsable
		objConvenio.ejecutar "RegistraBitacoraConvenios",false,codigo_cni,session("Usuario_bit"),"M","Registro Nuevo Responsable: " & Responsable					
		end if
	end if
	objConvenio.CerrarConexionTrans	
	Set objConvenio=nothing
	response.redirect "registra_convenio.asp?codigo_cni=" & codigo_cni & "&denominacion=" & denominacion & "&Ambito=" & Ambito & "&modalidad=" & modalidad & "&descripcion=" & descripcion & "&Duracion=" & Duracion & "&Periodo=" & Periodo & "&FechaInicio=" & FechaInicio & "&Renovacion=" & Renovacion & "&NumCopias=" & NumCopias & "&accion=guardar" & "&Observacion=" & Observacion & "&Responsable=" & Responsable & "&Referencia=" & Referencia & "&remLen=" & remLen & "&resolucion=" & resolucion & "&NameResponsable=" & NameResponsable & "&NameReferencia=" & NameReferencia & "&NameResolucion="  & NameResolucion & "&modifica=" + modifica	
end if

if accion="participante" then
	codigo_cni = Request.QueryString("codigo_cni")
	codigo_ins = Request.QueryString("codigo_ins")	
	firmante = Request.QueryString("firmante")
	cargo = Request.QueryString("cargo")
	gestor = Request.QueryString("gestor")

	'RESPONSE.WRITE codigo_ins
'		RESPONSE.WRITE codigo_cni
'			RESPONSE.WRITE firmante 
'				RESPONSE.WRITE cargo 
'				RESPONSE.WRITE gestor 
'				response.write session("Usuario_bit")
	Set objConvenio=Server.CreateObject("PryUSAT.clsAccesoDatos")

	objConvenio.AbrirConexionTRANS
		objConvenio.ejecutar "RegistraConvenioInstitucion",false,codigo_cni,codigo_ins,firmante,cargo,gestor
		objConvenio.ejecutar "RegistraBitacoraConvenios",false,codigo_cni,session("Usuario_bit"),"I","Registro de Institución: " & codigo_ins & " Firmante " & firmante & "-" & cargo			
	objConvenio.CerrarConexiontrans
	Set objConvenio=nothing
'	response.write "grabo"
%>
	<script>
	window.opener.location.reload()
	window.close()
	</script>
<%end if

if accion="institucion" then
 institucion=Request.QueryString("institucion")
 abreviatura=Request.QueryString("abreviatura")
 if abreviatura="" then
	abreviatura=null
 end if
 tipo=Request.QueryString("tipo")
 direccion=Request.QueryString("direccion")
  if direccion="" then
	direccion=null
 end if
 telefax=Request.QueryString("telefax")
  if telefax="" then
	telefax=null
 end if
 ciudad=Request.QueryString("ciudad")
 pais=Request.QueryString("pais")
 web=Request.QueryString("web")
 if web="" then
	web=null
 end if
 email=Request.QueryString("email")
 if email="" then
	email=null
 end if
contacto=Request.QueryString("contacto")
 if contacto="" then
	contacto=null
 end if
emailcontacto=Request.QueryString("emailcontacto")
 if emailcontacto="" then
	emailcontacto=null
 end if
 
	Set objConvenio=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objConvenio.AbrirConexiontrans
		objConvenio.ejecutar "RegistraInstitucion",false,trim(institucion),Trim(abreviatura),tipo,pais,Trim(web),Trim(ciudad),Trim(email),Trim(direccion),Trim(telefax),Trim(contacto),Trim(emailcontacto)
//		objConvenio.ejecutar "RegistraBitacoraConvenios",false,codigo_cni,session("Usuario_bit"),"I","Registro de Instituciones Nuevas: " & institucion
	objConvenio.CerrarConexiontrans
	Set objConvenio=nothing%>
	<script>
	window.close()
	//window.opener.location.reload()</script>
<%end if
%>

<%
if accion="eliminaParticipante" then
codigo_cin=Request.QueryString("codigo_cin")
	Set objConvenio=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objConvenio.AbrirConexionTrans
		''response.write codigo_cni
		objConvenio.ejecutar "EliminarConvenioInstitucion",true,codigo_cin
		objConvenio.ejecutar "RegistraBitacoraConvenios",false,codigo_cni,session("Usuario_bit"),"I","Elimina Institución: " & codigo_ins
	objConvenio.CerrarConexionTrans	
	Set objConvenio=nothing
	response.redirect "registra_convenio.asp?codigo_cni=" & codigo_cni & "&denominacion=" & denominacion & "&Ambito=" & Ambito & "&modalidad=" & modalidad & "&descripcion=" & descripcion & "&Duracion=" & Duracion & "&Periodo=" & Periodo & "&FechaInicio=" & FechaInicio & "&Renovacion=" & Renovacion & "&NumCopias=" & NumCopias & "&accion=guardar" & "&Observacion=" & Observacion & "&Responsable=" & Responsable & "&Referencia=" & Referencia & "&remLen=" & remLen & "&resolucion=" & resolucion & "&NameResponsable=" & NameResponsable & "&NameReferencia=" & NameReferencia & "&NameResolucion="  & NameResolucion & "&modifica=" + modifica	
end if

if accion="resolucion" then
resolucion=Request.QueryString("resolucion")
tipo=Request.QueryString("tipo")
fecha=Request.QueryString("fecha")

	Set objConvenio=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objConvenio.AbrirConexionTrans
		objConvenio.ejecutar "RegistraResolucion",true,resolucion,tipo,fecha
//		objConvenio.ejecutar "RegistraBitacoraConvenios",false,codigo_cni,session("Usuario_bit"),"I","Elimina Institución: " & codigo_ins
	objConvenio.CerrarConexionTrans	
	Set objConvenio=nothing
%>
	<script>
	window.close()
	//window.opener.location.reload()
	</script>
<%end if

%>
<title>Procesando</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" /><style type="text/css">
<!--
body {
	background-color: #FFFFCC;
}
-->
</style>
<link href="../../../../../private/estilo.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
.Estilo1 {
	font-family: Arial, Helvetica, sans-serif;
	font-size: 10pt;
	color: #0066CC;
	font-weight: bold;
}
-->
</style>
</head>
<body>
<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td align="center"><span class="Estilo1">Procesando...</span></td>
  </tr>
  <tr>
    <td align="center"><img src="../../../../../images/cargando.gif" width="165" height="16" /></td>
  </tr>
</table>
</body>