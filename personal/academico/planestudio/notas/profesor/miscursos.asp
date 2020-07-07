<!--#include file="../../../../funciones.asp"-->
<%
nivel=0
codigo_per=session("codigo_Usu")
nombre_per=session("nombre_usu")
codigo_cac=Request.querystring("codigo_cac")
if codigo_per="" then codigo_per=0
if codigo_cac="" then codigo_cac=session("codigo_cac")	

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCiclo=obj.Consultar("ConsultarCicloAcademico","FO","TO","")
		Set rsCarga=Obj.Consultar("NOT_ConsultarRegistroNotas","FO",0,codigo_cac,codigo_per)

		if Not(rsCarga.BOF and rsCarga.EOF)=true then
			HayReg=true
			Set rsHorario=obj.Consultar("ConsultarHorariosAmbiente","FO",15,codigo_cac,codigo_per,1,0)
		end if
	obj.CerrarConexion
Set obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../jq/jquery.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="avisos/jquery-1.7.2.min.js"></script>
<link rel="stylesheet" type="text/css" href="avisos/styles.css">
</head>
<body>
<p class="usattitulo">Registro de Notas Finales</p>
<table border="0" cellpadding="3" cellspacing="0" width="100%">
  <tr>
    <td width="15%">Ciclo Académico</td>
    <td width="75%">
    <%call llenarlista("cbocodigo_cac","actualizarlista('miscursos.asp?codigo_cac=' + this.value)",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>
	</td>
  </tr>
  <tr>
    <td width="15%">Docente</td>
    <td width="75%"><%=session("Nombre_Usu")%>&nbsp;</td>
  </tr>
</table>
		
<%
  	if HayReg="" then%>
  		<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ud. no tiene asignaturas asignadas en el ciclo académico seleccionado. Consulte con su Departamento Académico al cual está adscrito.</h5>
  	<%else
  	    if (codigo_cac<>"" and codigo_per<>"-2" and cint(codigo_cac)<=cint(session("codigo_cac"))) then
%>
<br>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="gray" width="100%">
	  <tr class="etabla">
	    <th width="5%">#</th>
	    <th width="10%">Asignatura</th>
	    <th width="5%">Ciclo</th>
	    <th width="20%">Escuela Profesional</th>
		<th width="10%">Grupo horario</th>
	    <th width="5%">Matric.</th>
	    <th width="5%">Retirados</th>
	    <th width="10%">Estado Reg.Notas</th>
		<th width="10%">Hrs. Clase</th>
		<th width="10%">Hrs. Asesoría</th>
		<th width="10%">Total Hrs.</th>
		<th width="10%">Investigaciones Finales</th>
	  </tr>
	  <%
	  Do while not rsCarga.EOF
	  	i=i+1
	  %>
	  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
	   <td width="5%" align="center"><%=i%></td>
	    <td width="30%"><a href="../administrarconsultar/lstalumnosmatriculados.asp?codigo_cac=<%=codigo_cac%>&codigo_cup=<%=rsCarga("codigo_cup")%>&nivel=<%=rsCarga("codigo_aut")%>"><u><%=rsCarga("nombre_cur")%></u></a>
		<br /><i><%=rsCarga("identificador_Cur")%></i></td>
	    <td width="5%"><%=ConvRomano(rsCarga("ciclo_cur"))%></td>
		<td width="20%"><%=rsCarga("nombre_cpf")%></td>
		<td width="10%" align="center"><%=rsCarga("grupoHor_Cup")%></td>
		<td width="5%" align="center"><%=rsCarga("matriculados")%></td>
	    <td width="5%" align="center"><%=rsCarga("retirados")%></td>
	    <td width="10%" class="etiqueta">
		<%if rsCarga("codigo_aut")=0 and rsCarga("estadonota_cup")<>"P" then%>
			Realizado
		<%elseif rsCarga("codigo_aut")>0 and rsCarga("estadonota_cup")<>"P" then%>
			Pendiente con autorización
			<%else%>
			Pendiente
		<%end if%>
	    </td>
		<td width="5%" align="center"><%=rsCarga("totalhorasaula")%></td>
		<td width="5%" align="center"><%=rsCarga("totalhorasasesoria")%></td>
		<td width="5%" align="center"><%=rsCarga("totalhoras_car")%></td>
		<td width="5%" align="center"><a href="../../expediente/subirarchivos.asp?codigo_cac=<%=codigo_cac%>&codigo_per=<%=codigo_per%>&nombre_per=<%=nombre_per%>&codigo_cup=<%=rsCarga("codigo_cup")%>&nivel=<%=rsCarga("codigo_aut")%>&nombre_cur=<%=rsCarga("nombre_cur")%>"><u>Publicar</u></a></td>
	  </tr>
	  	<%
	  	rsCarga.movenext
	  Loop
	  %>
	</table>
	<%
	    else
  		response.Write "<h5 class='usatsugerencia'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ud. no tiene asignaturas asignadas en el ciclo académico seleccionado. Consulte con su Departamento Académico al cual está adscrito.</h5>"
	    end if 
	end if
Set rsCarga=nothing
%>
<p align="right" id="mensaje" class="rojo">&nbsp;</p>
<%if HayReg=true then
  if (codigo_cac<>"" and codigo_per<>"-2" and cint(codigo_cac)<=cint(session("codigo_cac"))) then
%>
<p class="usatTitulo">Horario de asignaturas</p>
<%
	if Not(rsHorario.BOF and rsHorario.EOF) then
		dim dia,hora
	dim diaBD,inicioBD,finBD
	dim TextoCelda
	dim marcas
	
	marcas=0
	response.write "<table id='tblHorario' style='border-collapse: collapse;' width='90%'  border='1' bgcolor='white' bordercolor='#CCCCCC'>" & vbcrlf
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
				response.write vbtab & "<td width='15%' class='etiquetaTabla'>" & f+6 & ":00 - " & f+6 & ":50</td>"
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
							TextoCelda=TextoCelda & rsHorario("nombre_cur") & "<br>" & rsHorario("ambiente") & "<br><br>"
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
	%>
	<script type="text/javascript" language="JavaScript">
		PintarCeldas()
	</script>
	<%else
		response.write "<h3>No se han asignado cursos al Profesor en el semestre académico seleccionado</h3>"
	end if
  'else  
    'response.write "<h5 style='color:Red'>Usted no puede visualizar horarios de semestres superiores al actual</h5>"
  end if
  %>
<%end if%>


</body>
</html>
