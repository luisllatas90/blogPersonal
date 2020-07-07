<!--#include file="../../funciones.asp"-->    
<%      


Response.ContentType = "application/msword"
Response.AddHeader "Content-Disposition","attachment;filename=carta.doc"
		
%>

<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<style>
<!--
.etiqueta    { font-weight: bold }
.normal    { font-weight: normal }
.cabezera    { font-weight: bold; text-align: center; background-color: #C0C0C0; border-left-width:1; border-right-width:1; border-top-style:solid; border-top-width:1; border-bottom-style:solid; border-bottom-width:1}
body         { font-family: Belwe Lt BT; font-size: 12pt }
td           { font-size: 12pt }
p.primeralinea {
	text-indent: 15px;
        width: 565px;
    text-align:justify;
    }
    .style1
    {
        height: 24px;
    }
.colorazul { color:Blue; text-decoration: underline}
-->
</style>
</head>
<body>
<%
On error Resume next        
    alumnosArray =  split(request.QueryString("alumnosArray"), ",")                    
    
    fechaactual = FormatDateTime(Date(), 1)
    fechaactual = mid(fechaactual, instr(fechaactual, ",") + 2, len(fechaactual)-instr(fechaactual, ",") + 2)
    
    For i = 0 to ubound(alumnosArray)
    
    codigo_alu =  mid(alumnosArray(i),3,Len(alumnosArray(i)-2))
        
    Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsHistorial=Obj.Consultar("EPRE_ListarPostulantes","FO","%",0,0,"%","%","%","%",1,codigo_alu)
	Obj.CerrarConexion
	Set Obj=nothing
	
	If (rsHistorial.BOF and rsHistorial.EOF) then
	
	else
		
        apellidos_alu = rsHistorial("apellidos")
	    nombres_alu= rsHistorial("nombres")
	    codigouniver_alu = rsHistorial("CodUniversitario")
	    nombre_cpf = rsHistorial("carrera")
	    credito_alu = replace(rsHistorial("Categorizacion"), ",", ".")	    
	    password_alu = rsHistorial("password_Alu")
	    
	    Response.Write "<table cellpadding='2' cellspacing='0' style='border-collapse: collapse; padding-top:15px' width='100%'>" 
	    Response.Write "<THEAD>" 
	    Response.Write "<tr><td>"
	    Response.Write "<table border='0' cellpadding='0' cellspacing='0' style='border-collapse: collapse' bordercolor='#111111' width='100%' height='130'>"
	    Response.Write "<tr>"
	    Response.Write "<th style='font-size: 12pt; font-weight:bold' width='100%' height='50' align='right' valign='top'>"
	    Response.Write "Chiclayo, " & fechaactual
	    Response.Write "</th>"
	    Response.Write "</tr>"	    
	    Response.Write "<tr>"
	    
	    Response.Write "<td height='19' class='normal'>"
	    Response.Write "<br /><br /><br /><br />"
	    Response.Write "Familia</td>"
	    Response.Write "</tr>"
	    Response.Write "<tr>"
	    Response.Write "<td height='19' class='etiqueta'>&nbsp;" & apellidos_alu & "&nbsp;</td>"
	    Response.Write "</tr>"
	    Response.Write "<tr>"
	    Response.Write "<td height='19' class='normal'>Ciudad.-</td>"
	    Response.Write "</tr>"
	    Response.Write "<tr>"
	    Response.Write "<td height='50'</td>"
	    Response.Write "</tr>"
	    Response.Write "<tr>"
	    Response.Write "<td class='normal'>"
	    Response.Write "<p class='primeralinea'>"
	    Response.Write "En nombre  de la Universidad Católica Santo Toribio de Mogrovejo, les expreso mi cordial saludo y felicitación por el ingreso de su hijo (a) <b>" & nombres_alu & "</b> a la Escuela Profesional de <b>" & nombre_cpf & "</b>."
	    Response.Write "</p>"	    
	    Response.Write "<p class='primeralinea'>"
	    Response.Write "En la evaluación de su Expediente Socio-económico la Comisión de Pensiones ha determinado asignarle un <b> costo de crédito por ciclo académico de S/. " & credito_alu & " Nuevos Soles. </b> La pensión académica está en función del costo por crédito asignado y de la carga académica; la misma que podrá ser cancelada en 4 ó 5 cuotas los 30 de cada mes. Esta categorización será supervisada periódicamente y podrá suspenderse o extinguirse de conformidad con el ítem IV (j) del  Reglamento de  Pensiones 2012-I."
	    Response.Write "</p>"	    
	    Response.Write "<p class='primeralinea'>"
	    Response.Write "Su hijo (a) podrá realizar su matrícula a través de nuestra página web: <span class='colorazul'>www.usat.edu.pe/campusvirtual</span>, ingresando su código universitario <b>" & codigouniver_alu & "</b>, cuya clave es <b>" & password_alu & "</b>. Asimismo, adjunto encontrará el Reglamento de  Pensiones 2012-I para su  atenta lectura. "
	    Response.Write "</p>"	    
	    Response.Write "<p class='primeralinea'>"
	    Response.Write "Reciban nuestro agradecimiento por la confianza depositada en nuestra Universidad."
	    Response.Write "</p>"	    
	    Response.Write "<p class='primeralinea'>"
	    Response.Write "Sin otro particular, quedo de ustedes."
	    Response.Write "</p>"	    	    
	    Response.Write "<p class='primeralinea'>"
	    Response.Write "Atentamente,"
	    Response.Write "</p>"
	    Response.Write "<br />"
	    Response.Write "<br />"
	    Response.Write "<br />"
	    Response.Write "</td>"
	    Response.Write "</tr>"
	    Response.Write "<tr>"
	    Response.Write "<td style='text-align:center'>"
	    Response.Write "<img src='firma.JPG' />"
	    Response.Write "</td>"
	    Response.Write "</tr>"
	    Response.Write "<tr>"
	    Response.Write "<td style='text-align:center'>"
	    Response.Write "<b>Mgtr. Carlos Campana Marroquín</b>"
	    Response.Write "</td>"
	    Response.Write "</tr>"
	    Response.Write "<tr>"
	    Response.Write "<td style='text-align:center'>"
	    Response.Write "<b>Administrador General</b>"
	    Response.Write "</td>"
	    Response.Write "</tr>"
	    Response.Write "</table>"
	    Response.Write "</td></tr>"
	    Response.Write "</THEAD>"
	    Response.Write "</table>"
	    Response.Write "<br/>"	 
	  end if
	       
    next    	
	
%>
</body>
</html>
	