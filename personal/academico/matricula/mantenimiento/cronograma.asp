<!--#include file="../../../../funciones.asp"-->
<%
codigo_cac=request.querystring("codigo_cac")
if codigo_cac="" then codigo_cac=session("codigo_cac")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Cronograma de Actividades Académicas</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
</head>

<body>

<p class="usattitulo">Cronograma de Actividades Académicas</p>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="60%">
  <tr>
    <td width="128">Ciclo Académico</td>
    <td width="239">
    <%
			Set objCiclo=Server.CreateObject("PryUSAT.clsDatcicloAcademico")
				Set rsCiclo= objCiclo.ConsultarCicloAcademico("RS","TO","")
         	Set objCiclo=nothing
         	
         	call llenarlista("cbocodigo_cac","actualizarlista('cronograma.asp?codigo_cac=' + this.value)",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")
	%> &nbsp;</td>
  </tr>
  <tr>
  <td colspan="2" width="100%">
  	<%if (codigo_cac<>"") then
		Set objMatricula=Server.CreateObject("PryUSAT.clsDatMatricula")
			Set rsCronograma= objMatricula.ConsultarMatricula("RS","6",codigo_cac,0,0)
		Set objMatricula=nothing
		if Not(rsCronograma.BOF AND rsCronograma.EOF) then
			ArrEncabezados=Array("ID","Actividad","Inicio","Fin","Observaciones")
			ArrCampos=Array("codigo_cro","descripcion_act","fechaini_cro","fechafin_cro","observacion_cro")
			ArrCeldas=Array("5%","30%","20%","20%","15%")
			ArrCamposEnvio=Array("codigo_cro")
			'pagina="detallematricula.asp?codigo_alu=" & session("codigo_alu")
			'otroscript="VerificarEstadoMatricula('" & session("descripcion_cac") & "')"

			call CrearRpteTabla(ArrEncabezados,rsCronograma,"",ArrCampos,ArrCeldas,"N","V",pagina,"N",ArrCamposEnvio,otroscript)
		else
			response.write "No se ha registrado cronograma de actividades para el ciclo seleccionado"
		end if
	end if
	%> &nbsp;</td>
  </tr>
</table>

</body>

</html>