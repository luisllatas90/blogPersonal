<!--#include file="../../../../funciones.asp"-->
<%
on error resume next

dim rsHorario
codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
ciclo_cur=request.querystring("ciclo_cur")

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
<meta http-equiv="Content-Language" content="es" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>horario</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" />
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/tooltip.js"></script>
<style type="text/css">
td {
	font-size: xx-small;
	text-align: center;
}
.CU {
	background-color: #FFCC00;
	cursor:hand
}

.etiquetaTabla {
	background-color: #EAEAEA;
	color: #0000FF;
}
</style>
<script type="text/javascript" language="Javascript">
var contador=0
function pintaHora(celda)
{
	if (celda.className=="CU"){
		AbrirPopUp('lstcursosambiente.asp?dia=' + celda.id + "&codigo_cac=<%=codigo_cac%>&codigo_amb=" + celda.codigo_amb,'400','700','yes','yes','yes')
	}
	/*No se separá ambientes por el momento
	else{
			if (celda.className=="Selected"){
				celda.className="SelOff"
				//tdMarcas.innerHTML=eval(tdMarcas.innerText)-1
			}
			else{
				celda.className="Selected"
				//tdMarcas.innerHTML=eval(tdMarcas.innerText)+1
			}
			
			//if (eval(tdMarcas.innerText)>0){
			//	cmdGuardar.disabled=false
			//}
	}*/
}

</script>
</head>

<body style="background-color: #DCDCDC;">
<%

Server.ScriptTimeout=1000

dim dia,hora
dim diaBD,inicioBD,finBD
dim TextoCelda
dim marcas

marcas=0
hora=0


	'******************************************************
	'Imprimir cabezeras de días
	'******************************************************
	response.write "<table id='tblHorario' cellpadding=2 style='border-collapse: collapse;' width='100%' border='1' bgcolor='white' bordercolor='#CCCCCC'>" & vbcrlf
	'response.write vbtab & "<thead class='fixedHeaderTable'>" 
	response.write vbtab & "<tr class='etiquetaTabla'>" & vbcrlf
	response.write vbtab & "<th rowspan='2' width='18%'>AMBIENTE</th>" & vbcrlf
	response.write vbtab & "<th width='10%' colspan='15'>LUNES</th>" & vbcrlf
	response.write vbtab & "<th width='10%' colspan='15'>MARTES</th>" & vbcrlf
	response.write vbtab & "<th width='10%' colspan='15'>MIÉRCOLES</th>" & vbcrlf
	response.write vbtab & "<th width='10%' colspan='15'>JUEVES</th>" & vbcrlf
	response.write vbtab & "<th width='10%' colspan='15'>VIERNES</th>" & vbcrlf
	response.write vbtab & "<th width='10%' colspan='15'>SÁBADO</th>" & vbcrlf
	response.write vbtab & "</tr>" & vbcrlf
	'response.write vbtab & "</thead>"
	'******************************************************
	'Imprimir cabezeras de horas
	'******************************************************
	response.write vbtab & "<tr class='etiquetaTabla'>" & vbcrlf
		
		for c=0 to 89
			'clase=""
			if (hora mod 21)= 0 then
				hora=7
			else
				hora=hora+1
			end if
			
			hora=AnchoHora(hora)
			
			'if int(hora)=21 then clase=" class='bordedia'"
	
			response.write vbtab & "<td" & clase & " height='10px'>" & hora & "</td>" & vbcrlf
		next
	response.write vbtab & "</tr>" & vbcrlf
	
Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion
	
Set rsAmbiente=obj.Consultar("ConsultarHorariosAmbiente","FO",10,codigo_cac,codigo_cpf,ciclo_cur,0)
	
if Not(rsAmbiente.BOF and rsAmbiente.EOF) then
	
	Do while not rsAmbiente.EOF
		i=i+1
			
		if rsAmbiente("codigo_amb")<>32 then
				'******************************************************
				'Buscar el horario del ambiente e imprimir Fila
				'******************************************************
				Set rsHorario=obj.Consultar("ConsultarHorariosAmbiente","FO",11,codigo_cac,rsAmbiente("codigo_amb"),codigo_cpf,ciclo_cur)
				
				if Not(rsHorario.BOF and rsHorario.EOF) then
					HayReg=true 
				else
					HayReg=false
				end if
				
				response.write vbtab & "<tr>" & vbcrlf
				hora=0
				for c=0 to 90		
					
					if c=0 then
						response.write vbtab & "<td width='18%' class='etiquetaTabla' style='text-align:left'>" & rsAmbiente("ambiente") & "<br> [" & rsAmbiente("ambienteReal") & "]<br>[Cap: " & rsAmbiente("capacidad_Amb")  & "]</td>" & vbcrlf
					else
									        
						if c=1 then dia="LU"
						if c=16 then dia="MA"
						if c=31 then dia="MI"
						if c=46 then dia="JU"
						if c=61 then dia="VI"
						if c=76 then dia="SA"
						
						'clase=""
						if (hora mod 21)= 0 then
							hora=7
						else
							hora=hora+1
						end if
						
						hora=AnchoHora(hora)
													
						TextoCelda=vbtab & "<td onClick='pintaHora(this)' codigo_amb='" & rsAmbiente("codigo_amb") & "' id='" & dia & "' hora='" & hora & "' "
						
						'******************************************************
						'Buscar el horario según el ambiente día y hora
						'******************************************************
						if HayReg=true then
							rsHorario.movefirst
							Do while not rsHorario.EOF
								diaBD=mid(rsHorario("dia_lho"),1,2)
								inicioBD=mid(rsHorario("nombre_hor"),1,2)
								finBD=mid(rsHorario("horafin_lho"),1,2)	    
								'*******************************************************************************************************
								'Pintar la celda si el día es el mismo y la horaactual es menor que horafin y mayor que la horainicio
								'*******************************************************************************************************
							    if trim(dia)=trim(diaBD) AND int(hora)>=int(inicioBD) AND int(hora)<int(finBD) then
								    marcas=marcas+1
								    TextoCelda=TextoCelda & rsHorario("color_hor") '" style=""background-color: #FFFF66;""" '
								    '******************************************************
								    'Salir del Bucle ya que encontró un curso coincidente
								    '******************************************************
								    Exit Do
							    end if							
								
								rsHorario.movenext
							Loop
							
						end if
						
						TextoCelda=TextoCelda & " tooltip='" &  diaBD & "|" & inicioBD & "|" &  finBD & "'> </td>" & vbcrlf
						
						response.write TextoCelda
					end if
					
				next
				
				response.write vbtab & "</tr>" & vbcrlf
				
				rsHorario.close
				Set rsHorario=nothing
		end if
		rsAmbiente.movenext
		
	Loop
else
	response.write "<tr height='30px'><td colspan='91' style='text-align:left' class='usattitulousat'>No se han encontrado horarios registrados en la base de datos</td></tr>"
end if
	
obj.CerrarConexion
Set Obj=nothing
	
rsAmbiente.close
Set rsAmbiente=nothing

response.write "</table>"
%>
</body>
</html>
<% 
if Err.number <> 0 then  
    Response.Write "Error: " & Err.Description
end if
%>