<!--#include file="../NoCache.asp"-->
<%
Dim mostrarScript

function AnchoHora(byVal cad)
	if len(cad)<2 then
		AnchoHora="0" & cad
	else
		anchohora=cad
	end if
end function

estadomatricula=request.QueryString("estadomatricula")

if estadomatricula="previo" then
	mostrarScript="MostrarHorarioCursosElegido()"
	titulo="HORARIO DE CURSOS ELEGIDOS (" & session("descripcion_Cac") & ")"
else
	mostrarScript="MostrarHorarioCursosMatriculados()"
	titulo="HORARIO DE CURSOS MATRICULADOS (" & session("descripcion_Cac") & ")"
	end if

dim cn
set cn=server.createobject("PryUSAT.clsAccesoDatos")
cn.AbrirConexion
    dim FechaInicioCrog
	FechaInicioCrog =cn.ejecutar("DevuelveCronogramaInicioAmbiente",true,session("codigo_cac"),"")	
cn.CerrarConexion
	
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >
		<title><%=titulo%></title>
		<link rel="stylesheet" type="text/css" href="../private/estilo.css">
		<link rel="stylesheet" type="text/css" href="../private/estiloimpresion.css" media="print">
		<script type="text/javascript" language="JavaScript" src="private/validarhorario.js"></script>
	    <style type="text/css">
        <!--
        td { border: 1px solid #EEEEEE; font-size:8pt }
        .etiquetaTabla {
	        background-color: #EAEAEA;
	        color: #0000FF;
        }
        .CeldaMarcada {
	        border-left-width: 0;
	        border-right-width: 0;
	        border-top-width: 0;
	        border-bottom: 0px solid #FF9933;
	        background-color:#FF9933;
        }
        -->
        </style>
	</HEAD>
	<body style="background-color: #DCDCDC;">
	<p><b>AMBIENTES POR DEFINIR: Los Ambientes serán mostrados a partir del <%response.write(FechaInicioCrog)%></b></p>
<%
dim dia,hora
dim diaBD,inicioBD,finBD
dim TextoCelda
dim marcas

marcas=0
response.write "<table id='tblHorario' style='border-collapse: collapse;' width='100%' cellpadding='3' border='1' bgcolor='white' bordercolor='#CCCCCC'>" & vbcrlf
response.write vbtab & "<tr class='etiquetaTabla' height='8%'>" & vbcrlf
response.write vbtab & "<th width='20%'>Horas</th>" & vbcrlf
response.write vbtab & "<th width='10%'>Lunes</th>" & vbcrlf
response.write vbtab & "<th width='10%'>Martes</th>" & vbcrlf
response.write vbtab & "<th width='10%'>Miércoles</th>" & vbcrlf
response.write vbtab & "<th width='10%'>Jueves</th>" & vbcrlf
response.write vbtab & "<th width='10%'>Viernes</th>" & vbcrlf
response.write vbtab & "<th width='10%'>Sábado</th>" & vbcrlf
response.write vbtab & "</tr>" & vbcrlf

for f=1 to 16
	response.write vbtab & "<tr>" & vbcrlf
	
	for c=0 to 6
		if c=0 then		
			'response.write vbtab & "<td width='20%' class='etiquetaTabla'>&nbsp;" & f+6 & ":00 - " & f+6 & ":50</td>" & vbcrlf     'linea anterior
			
			'========================================================================================================================================
			response.write vbtab & "<td width='20%' class='etiquetaTabla'>&nbsp;" & f+6 & ":00 - " & f+1+6 & ":00</td>" & vbcrlf  'hotas exactas
			'========================================================================================================================================
		else
			
			if c=1 then dia="LU"
			if c=2 then dia="MA"
			if c=3 then dia="MI"
			if c=4 then dia="JU"
			if c=5 then dia="VI"
			if c=6 then dia="SA"
			
			hora=AnchoHora(f+6)
		
			response.Write vbtab & "<td valign='center' align='center' width='10%' id='" & dia & hora & "'>&nbsp;</td>" & vbcrlf

		end if
	next
	response.write vbtab & "</tr>" & vbcrlf
next

response.write "</table>"
%>
<script type="text/javascript" language=javascript>
    <%=mostrarScript%>
</script>
<p align=right class="NoImprimir">
<input onclick="window.print()" type="button" value="    Imprimir" name="cmdImprimir" class="usatimprimir">
</p>
	</body>
</HTML>