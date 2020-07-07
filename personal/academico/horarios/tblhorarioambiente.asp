<!--#include file="../../../funciones.asp"-->
<%
dim rsHorario
on error resume next
modo=request.querystring("modo")
codigo_cac=request.querystring("codigo_cac")
codigo_amb=request.querystring("codigo_amb")

if (codigo_cac<>"" or codigo_amb<>"") then

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsHorario=obj.Consultar("ConsultarHorariosAmbiente", "FO", 18, codigo_cac, codigo_amb, 0, 0)
	obj.CerrarConexion
Set Obj=nothing

function AnchoHora(byVal cad)
	if len(cad)<2 then
		AnchoHora="0" & cad
	else
		anchohora=cad
	end if
end function
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
<style type="text/css">
td {
	font-size:9px;
	font-family:Tahoma;
	text-align: center;
}
.etiquetaTabla {
	font-size: 10px;
	background-color: #EAEAEA;
	color: #0000FF;
}
.CU {
	background-color: #FFCC00;
}
</style>
</head>
<body bgcolor="#F0F0F0">
<%

if Not(rsHorario.BOF and rsHorario.EOF) then
  if ((cint(codigo_cac) <= cint(session("codigo_cac"))) or (session("codigo_tfu")="1" or session("codigo_tfu")="15" or session("codigo_tfu")="41"  or session("codigo_tfu")="18" or session("codigo_tfu")="25" or session("codigo_tfu")="72" or session("codigo_tfu")="9")or session("codigo_tfu")="16") then
	dim dia,hora
	dim diaBD,inicioBD,finBD
	dim TextoCelda
	dim marcas
	
	marcas=0
	response.write "<table id='tblHorario' style='border-collapse: collapse;' width='100%'  border='1' bgcolor='white' bordercolor='#CCCCCC'>" & vbcrlf
	response.write vbtab & "<tr class='etiquetaTabla' height='25px'>" & vbcrlf
	response.write vbtab & "<th width='15%'>Horas</th>" & vbcrlf
	response.write vbtab & "<th width='15%'>Lunes</th>" & vbcrlf
	response.write vbtab & "<th width='15%'>Martes</th>" & vbcrlf
	response.write vbtab & "<th width='15%'>Miércoles</th>" & vbcrlf
	response.write vbtab & "<th width='15%'>Jueves</th>" & vbcrlf
	response.write vbtab & "<th width='15%'>Viernes</th>" & vbcrlf
	response.write vbtab & "<th width='15%'>Sábado</th>" & vbcrlf
	response.write vbtab & "</tr>" & vbcrlf
	
	for f=1 to 16
		response.write vbtab & "<tr >" & vbcrlf
		
		for c=0 to 6
			if c=0 then		
				'response.write vbtab & "<td width='15%' class='etiquetaTabla'>" & f+6 & ":10 - " & f+1+6 & ":00</td>"    'hora con 10 min
				
				'=================================================================================================================================
				response.write vbtab & "<td width='15%' class='etiquetaTabla'>" & f+6 & ":00 - " & f+1+6 & ":00</td>"  'linea para horas exactas
				'=================================================================================================================================
								
			else
				
				if c=1 then dia="LU"
				if c=2 then dia="MA"
				if c=3 then dia="MI"
				if c=4 then dia="JU"
				if c=5 then dia="VI"
				if c=6 then dia="SA"
				
				hora=AnchoHora(f+6)
			
				TextoCelda=vbtab & "<td width='15%' id='" & dia & "' hora='" & hora & "'>" & vbcrlf
				
				'Si hay horario
				if Not(rsHorario.BOF and rsHorario.EOF) then
					rsHorario.movefirst
				
					Do while not rsHorario.EOF
						diaBD=mid(rsHorario("dia_lho"),1,2)
						inicioBD=mid(rsHorario("nombre_hor"),1,2)
						finBD=mid(rsHorario("horafin_lho"),1,2)
			
						'si el día es el mismo y la horaactual es menor que horafin y mayor que la horainicio
						if trim(dia)=trim(diaBD) AND int(hora)>=int(inicioBD) AND int(hora)<int(finBD) then
							marcas=marcas+1
							if rsHorario("estadoHorario_lho") = "A" then
							    TextoCelda=TextoCelda & rsHorario("nombre_cur") & "<br>" & rsHorario("docente")  & "<br>" & rsHorario("tipoHoraCur_Lho") & "<br><font size='1pt' color='blue'>[Asignado]</font><br><br>"
							elseif rsHorario("estadoHorario_lho") = "R" then
							    TextoCelda=TextoCelda & rsHorario("nombre_cur") & "<br>" & rsHorario("docente")  & "<br>" & rsHorario("tipoHoraCur_Lho") & "<br><font size='1pt' color='green'>[Por Reasignar]</font><br><br>"
							else
							    TextoCelda=TextoCelda & rsHorario("nombre_cur") & "<br>" & rsHorario("docente")  & "<br>" & rsHorario("tipoHoraCur_Lho") & "<br><font size='1pt' color='red'>[Pendiente]</font><br><br>" 
							end if 
						end if
						rsHorario.movenext
					Loop
				end if
				TextoCelda=TextoCelda & "</td>" & vbcrlf
				
				response.write TextoCelda
			end if
		next
		response.write vbtab & "</tr>" & vbcrlf
	next
	
	response.write "</table>"
	if marcas>0 then
		response.write "<p class='etiqueta' align='right' id='totalhrs'></p>"
	end if
%>
	<script type="text/javascript" language="JavaScript">
		PintarCeldas()
	</script>
<%
    else
    response.write "<h5 class='usatsugerencia'>&nbsp;&nbsp;&nbsp;&nbsp;Usted no puede visualizar horarios de semestres superiores al actual</h5>"
    end if 
else%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp; No se han registrado horarios según los datos seleccionados</h5>
<%end if%>
</body>
</html>

<%end if

if err.number<>0 then
    response.Write  err.Description
end if

%>

