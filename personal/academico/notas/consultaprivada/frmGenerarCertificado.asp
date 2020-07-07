<!--#include file="../../../../funciones.asp"-->
<%
Enviarfin session("codigo_usu"),"../../../../"

codigo_alu=request.querystring("codigo_alu")
modo=request.querystring("modo")
%>
<HTML>
  <HEAD>
<title>Matricula</title>
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../private/validarnotas.js"></script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
	function DescargarCertificados()
	{
		var tipo=1
		var pagina="doccertificado.asp"
		var tipoOrden="descripcion_cac"
		
		// olluen 19/05/2020 no imprimia en chrome
		
	    //var optTipo = 0                                      //<-- EPENA 25/05/2020
		var optTipo = document.getElementsByName('optTipo');   //<-- EPENA 25/05/2020

		for (var i=0;i<optTipo.length;i++){
			if (optTipo[i].checked==true){
				tipo=optTipo[i].value
			}
		}
		



		if (tipo==3 || tipo==4){
			pagina="doccertificadocomplementario.asp"
		}
		if (tipo==13){
			pagina="doccertificadose.asp"
		}
		
		if (tipo==5){
			tipoOrden="ciclo_cur"
		}

	    if (tipo==17){
			tipoOrden="ciclo_cur"
		}
	    
		location.href=pagina + "?tipo=" + tipo + "&codigo_alu=<%=codigo_alu%>&codigo_pes=<%=session("codigo_pes")%>&tipoOrden=" + tipoOrden	
	}
</script>
      <style type="text/css">
          .style1
          {
              width: 5%;
          }
      </style>
</HEAD>
<body onLoad="document.all.txtcodigouniver_alu.focus()">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="60%" class="usattitulo">Generar certificado de estudios</td>
	<%if codigo_alu="" then%>
    <td width="40%"><%call buscaralumno("notas/consultaprivada/frmGenerarCertificado.asp","../../",-1)%></td>
    <%end if%>
  </tr>
  <tr>
    <td width="100%" colspan="2">&nbsp;</td>
  </tr>
</table>
<%if codigo_alu<>"" then%>
<!--#include file="../../fradatos.asp"-->
<br>
<table class="contornotabla" style="width: 100%" cellspacing="0" cellpadding="3">
	<tr>
		<td colspan="2" class="usatCeldaTabActivo">Opciones para generar certificado de 
            estudios</td>
	</tr>
	<!--<tr>
		<td style="width: 5%">
		<input name="optTipo" type="radio" checked="checked" style="height: 20px" value="11"></td>
		<td width="95%">Todas las asignaturas del Plan de Estudios acorde al Cuadro de 
            Méritos</td>
	</tr>-->
	<tr>
		<td style="width: 5%">
		<input name="optTipo" type="radio" checked="checked" style="height: 20px" value="1"></td>
		<td width="95%">Todas las asignaturas del Plan de Estudios omitiendo a Idiomas y 
            Computación ordenados por semestre académico</td><!--antes por ciclo académico-->
	</tr>
	<!--<tr>
		<td style="width: 5%"><input name="optTipo" type="radio" value="2"></td>
		<td width="95%">Todas las asignaturas complementarias con creditaje igual a 0 
            ordenados por ciclo académico</td>
	</tr>-->
	<tr>
		<td style="width: 5%"><input name="optTipo" type="radio" value="3"></td>
		<td width="95%">Todas las asignaturas complementarias de Computación ordenados por 
            ciclo académico</td>
	</tr>
	<tr>
		<td style="width: 5%">
		<input name="optTipo" type="radio" value="4"></td>
		<td width="95%">Todas las asignaturas complementarias de Idiomas ordenados por ciclo 
            académico</td>
	</tr>
<!--	
	<tr>
		<td style="width: 5%">
		<input name="optTipo" type="radio" style="height: 20px" value="5"></td>
		<td width="95%">Todas las asignaturas del Plan de Estudios omitiendo a Idiomas y 
            Computación ordenados por ciclo de estudios</td>
	</tr>
-->	 
	<tr>
		<td class="style1">
		<input name="optTipo" type="radio" style="height: 20px" value="6"></td>
		<td width="95%">Todas las asignaturas registradas por convalidación ordenados por 
            ciclo académico</td>
	</tr>	
	<tr>
		<td style="width: 5%">
		<input name="optTipo" type="radio" style="height: 20px" value="7"></td>
		<td width="95%">Todas las asignaturas registradas por suficiencia ordenados por 
            ciclo académico</td>
	</tr>
	<tr>
		<td class="style1">
		<input name="optTipo" type="radio" style="height: 20px" value="8"></td>
		<td width="95%">Todas las asignaturas registradas por Exámen de ubicación ordenados 
            por ciclo académico</td>
	</tr>
	<!--
	<tr>
		<td style="width: 5%">
		<input name="optTipo" type="radio" style="height: 20px" value="9"></td>
		<td width="95%">Todas las asignaturas del Plan de Estudios omitiendo a Idiomas y 
            Computación ordenados por ciclo académico mayores que 10 y menores que 14</td>
	</tr>-->
	<!--
	<tr>
		<td style="width: 5%">
		<input name="optTipo" type="radio" style="height: 20px" value="12" checked="true"></td>
		<td width="95%">Todas las asignaturas del Plan de Estudios omitiendo a Idiomas y 
            Computación ordenados por ciclo académico mayores que 10 y menores que 14 y sólo 
            de la <b>ESCUELA PROFESIONAL ACTUAL</b></td>
	</tr>-->
	<tr>
		<td style="width: 5%">
		<input name="optTipo" type="radio" style="height: 20px" value="13" ></td>
		<td width="95%">Programas de Segunda Especialidad en Educación. Facultad de 
            Humanidades</td>
	</tr>
<!--<tr>
		<td style="width: 5%">
		<input name="optTipo" type="radio" style="height: 20px" value="14" ></td>
		<td width="95%">Todas las asignaturas del Plan de Estudios omitiendo a Idiomas y 
            Computación ordenados por ciclo académico mayores que 10 y menores que 14, sólo 
            de la <b>ESCUELA DE ENFERMERÍA omitiendo el curso INTERNADO y cursos del X ciclo</b></td>
	</tr>
--> 	
	<tr>
		<td style="width: 5%">
		<input name="optTipo" type="radio" style="height: 20px" value="19" ></td>
		<td width="95%">Todas las asignaturas del Plan de Estudios omitiendo a Idiomas y 
            Computación ordenados por ciclo académico mayores que 11 y menores que 14, sólo 
            de la <b>ESCUELA DE ENFERMERÍA omitiendo el curso INTERNADO</b></td>
	</tr>
 	
 	<tr>
		<td class="style1">
		<input name="optTipo" type="radio" style="height: 20px" value="15" ></td>
		<td width="95%">Todas las asignaturas del plan aprobados y desaprobados omitiendo 
            Idiomas y Computación ordenados por <b>ciclo</b> académico y sólo de la <b>ESCUELA 
            PROFESIONAL Y PLAN ACTUAL</b></td>
	</tr> 
	<tr>
		<td class="style1">
		<input name="optTipo" type="radio" style="height: 20px" value="151" ></td>
		<td width="95%">Todas las asignaturas del plan aprobados y desaprobados omitiendo 
            Idiomas y Computación ordenados por <b>semestre</b> académico y sólo de la <b>ESCUELA 
            PROFESIONAL Y PLAN ACTUAL</b></td>
	</tr> 
 	<tr>
		<td class="style1">
		<input name="optTipo" type="radio" style="height: 20px" value="16"  ></td>
		<td width="95%">Todas las asignaturas del plan aprobados y desaprobados omitiendo 
            Idiomas y Computación ordenados por ciclo académico y sólo de la <b>ESCUELA 
            PROFESIONAL (Todos los planes cursados)</b></td>
	</tr> 
	
	<tr>
		<td class="style1">
		<input name="optTipo" type="radio" style="height: 20px" value="17"  ></td>
		<td width="95%">Todas las asignaturas del Plan de Estudios omitiendo a Idiomas y Computación 
		ordenados por ciclo académico.</td>
	</tr> 
		
	<tr>
		<td class="style1">
		<input name="optTipo" type="radio" style="height: 20px" value="18"  ></td>
		<td width="95%">Todas las asignaturas del Plan de Estudios incluyendo cursos complementarios</td>
	</tr> 
	<tr>
		<td style="width: 5%">
		<input name="optTipo" type="radio" style="height: 20px" value="20"></td>
		<td width="95%">Todas las asignaturas del Plan de Estudios incluyendo cursos complementarios <b>omitiendo los ciclos XIII y XIV (Medicina)</b></td>
	</tr>
	<tr>
		<td style="width: 5%">
		<input name="optTipo" type="radio" style="height: 20px" value="24"></td>
		<td width="95%">Todas las asignaturas del Plan de Estudios incluyendo cursos complementarios <b>hasta VII Ciclo (Psicología)</b></td>
	</tr>
	<tr>
		<td class="style1">
		<input name="optTipo" type="radio" style="height: 20px" value="21" ></td>
		<td width="95%">Todas las asignaturas del plan aprobados y desaprobados omitiendo 
            Idiomas y Computación ordenados por ciclo académico, incluye las asignaturas complementarias del plan de estudios actual.
	</tr> 

	<tr>
		<td class="style1">
		<input name="optTipo" type="radio" style="height: 20px" value="22"/></td>
		<td width="95%">Todas las asignaturas del plan <b>aprobados</b> omitiendo 
            Idiomas y Computación ordenados por <b>ciclo</b> académico y sólo de la <b>ESCUELA 
            PROFESIONAL Y PLAN ACTUAL </b>
	</tr> 

	<tr>
		<td class="style1">
		<input name="optTipo" type="radio" style="height: 20px" value="23" /></td>
		<td width="95%">Todas las asignaturas del plan <b>aprobados</b> omitiendo 
            Idiomas y Computación ordenados por <b>semestre</b> académico y sólo de la <b>ESCUELA 
            PROFESIONAL Y PLAN ACTUAL</b></tr> 

	<tr>
		<td style="width: 5%">&nbsp;</td>
		<td width="95%">
		<input name="cmdDescargar" type="button" value="Generar" onClick="DescargarCertificados()" class="word">
		</td>
	</tr>
	</table>
<%end if%>
</body>
</HTML>