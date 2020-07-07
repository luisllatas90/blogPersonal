<!--#include file="../../../funciones.asp"-->
<%
on error resume next
codigouniver_alu=request.querystring("codigouniver_alu")
alumno=request.querystring("alumno")
nombre_cpf=request.querystring("nombre_cpf")
codigo_mat=request.querystring("codigo_mat")
'codigo_mat=request.Form("cbocodigo_mat")
codigo_alu=request.querystring("codigo_alu")

    Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsMatriculas=Obj.Consultar("ConsultarMatricula","FO","29",codigo_alu,0,0)
		if not(rsMatriculas.BOF and rsMatriculas.EOF) then
		if codigo_mat <>"" then
		    codigo_mat=codigo_mat
		    
		    HayReg=true
		else
			codigo_mat=rsMatriculas("codigo_mat")
			HayReg=true
			end if 
		end if

		if codigo_mat<>"" then
			Set rsHorario=obj.Consultar("ConsultarHorariosAmbiente","FO",16,codigo_alu,codigo_mat,0,0)			
			HayHorario=true
		end if
	obj.CerrarConexion
    Set Obj=nothing
    
%>
<html xmlns="http://www.w3.org/1999/xhtml">
	<HEAD>
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
	<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">	
	<script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
	<title>Horario de Estudiante</title>
	<script type="text/javascript" language="javascript">
		function ConsultarHorario()
		{
			location.href="horario.asp?codigo_mat=" + cbocodigo_mat.value + "&codigo_alu=<%=codigo_alu%>&codigouniver_alu=<%=codigouniver_alu%>&alumno=<%=alumno%>&nombre_cpf=<%=nombre_cpf%>"
		}
	
	</script>
	</HEAD>
	<body bgcolor="#EEEEEE">
	<%if HayReg=true then%>	
	<!-- <form id="frmHorario" action="horario.asp" method="post" >  -->
	<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla" bgcolor="#FFFFFF">
      <tr>
	    <td width="18%">Código Universitario&nbsp;</td>
	    <td class="usatsubtitulousuario" width="71%">: <%=codigouniver_alu%></td>
	          </tr>
	          <tr>
	    <td width="18%">Apellidos y Nombres</td>
	    <td class="usatsubtitulousuario" width="71%">: <%=alumno%></td>
	          </tr>
	          <tr>
	    	<td width="18%">Escuela Profesional&nbsp;</td>
	    	<td class="usatsubtitulousuario" width="46%">: <%=nombre_cpf%>&nbsp;</td>
	    </tr>
	    <tr class="usattablainfo">
		<td width="18%">Ciclo Académico&nbsp;</td>
	    <td class="usatsubtitulousuario" width="46%">
		<%call llenarlista("cbocodigo_mat","ConsultarHorario()",rsMatriculas,"codigo_mat","descripcion_cac",codigo_mat,"","","")%>
		</td>
	    </tr>
	</table>
	<!-- </form> -->
	<br>
	<%
	

	if codigo_mat<>"" and HayHorario=true then

	if Not(rsHorario.BOF and rsHorario.EOF) then
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
	
	for f=1 to 14
		response.write vbtab & "<tr >" & vbcrlf
		
		for c=0 to 6
			if c=0 then		
				'response.write vbtab & "<td width='15%' class='etiquetaTabla'>" & f+6 & ":00 - " & f+6 & ":50</td>"    'LINEA ANTERIOR
				
				'=====================================================================================================================
				response.write vbtab & "<td width='15%' class='etiquetaTabla'>" & f+6 & ":00 - " & f+1+6 & ":00</td>" 'HORAS EXACTAS
				'=====================================================================================================================
				
			else
				
				if c=1 then dia="LU"
				if c=2 then dia="MA"
				if c=3 then dia="MI"
				if c=4 then dia="JU"
				if c=5 then dia="VI"
				if c=6 then dia="SA"
				
				hora=AnchoHora(f+6)
			
				'TextoCelda=vbtab & "<td width='15%' id='" & dia & "' hora='" & hora & "'>" & vbcrlf
				TextoCelda=vbtab & "<td>" & vbcrlf
				
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
							TextoCelda=TextoCelda & rsHorario("nombre_cur") & "<br>" & rsHorario("docente") & "<br>" & rsHorario("ambiente") & "<br><br>"
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
		response.write "<p class='etiqueta' align='right'>" & marcas & " horas ocupadas</p>"
	end if%>
	<script type="text/javascript" language="JavaScript">
		PintarCeldas('S')
	</script>
	<%else
		response.write "<h3>No se han horarios registrados en la matrícula seleccionada</h3>"
	end if
  end if
else%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado Matrículas registradas para el Estudiante</h5>
<%end if%>
	</body>
</html>
<%
If Err.Number <> 0 Then  
    Response.Write ("Error: " & Err.Description& "<br><br>")         
End If
%>