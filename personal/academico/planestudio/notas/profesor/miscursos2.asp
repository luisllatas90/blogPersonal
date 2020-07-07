<!--#include file="../../../../funciones.asp"-->
<%
nivel=0
codigo_per=session("codigo_Usu")
nombre_per=session("nombre_usu")
codigo_cac = Request.querystring("codigo_cac")
if codigo_per="" then codigo_per=0
if codigo_cac="" then codigo_cac = 42

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCiclo=obj.Consultar("ConsultarCicloAcademico","FO","TOM","")
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
<meta name="tipo_contenido" content="text/html;" http-equiv="content-type" charset="utf-8">
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../jq/jquery.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script>
function mostrarManual(){
   
}

</script>
</head>
<body>
<p class="usattitulo">Registro de Asistencia y Notas Parciales</p>
<table border="0" cellpadding="3" cellspacing="0" width="100%">
  <tr>
    <td width="15%">Ciclo Académico</td>
    <td width="75%">
    <%call llenarlista("cbocodigo_cac","actualizarlista('miscursos2.asp?codigo_cac=' + this.value)",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>
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
 <% else
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
	  </tr>
	  <%
	  Do while not rsCarga.EOF
	  	i=i+1
	  %>
	  <tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
	   <td width="5%" align="center"><%=i%></td>
	    <td width="30%"><a href="../../expediente/medicina/sylabus.aspx?codigo_cac=<%=codigo_cac%>&codigo_cup=<%=rsCarga("codigo_cup")%>&nivel=<%=rsCarga("codigo_aut")%>&codigo_per=<%=codigo_per%>&nombre_per=<%=nombre_per%>&nombre_cur=<%=rsCarga("nombre_cur")%>"><u><%=rsCarga("nombre_cur")%></u></a>
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
	  </tr>
	  	<%
	  	rsCarga.movenext
	  Loop
	  %>
	</table>
	<p style="color:Red;font-size:13px;">Estimado Docente USAT, a partir del Ciclo <b>2012-I</b>, las <b>asistencias y notas parciales</b>, serán registradas desde el aula virtual.
	</p>
	<p style="Color:red;font-size:13px;font-weight:bold;">
	<a href="#" onclick="$('#manual').toggle(200);"><i>MANUAL DE ASISTENCIA, lea las instrucciones.</i></a>
	</p>
	
	<div id="manual" style="display:none;">
	   <object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" style="width:800px;height:500px" id="4e82ec11-ed09-9a1f-f6a9-4f50ddc650b9" ><param name="movie" value="http://static.issuu.com/webembed/viewers/style1/v2/IssuuReader.swf?mode=mini&amp;viewMode=singlePage&amp;titleBarEnabled=true&amp;shareMenuEnabled=false&amp;backgroundColor=%23222222&amp;documentId=120319162258-8456e06723444f76a55ae0198c5f990a" /><param name="allowfullscreen" value="true"/><param name="menu" value="false"/><param name="wmode" value="transparent"/><embed src="http://static.issuu.com/webembed/viewers/style1/v2/IssuuReader.swf" type="application/x-shockwave-flash" allowfullscreen="true" menu="false" wmode="transparent" style="width:800px;height:500px" flashvars="mode=mini&amp;viewMode=singlePage&amp;titleBarEnabled=true&amp;shareMenuEnabled=false&amp;backgroundColor=%23222222&amp;documentId=120319162258-8456e06723444f76a55ae0198c5f990a" /></object>
	</div>
	
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
   ' response.write "<h5 style='color:Red'>Usted no puede visualizar horarios de semestres superiores al actual</h5>"
  end if
  %>
<%end if%>
</body>
</html>

