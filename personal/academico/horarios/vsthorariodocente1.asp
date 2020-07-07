<!--#include file="clshorarios.asp"-->
<%
dim titulo

modo=request.querystring("modo")
codigo_per=request.querystring("codigo_per")
codigo_cac=request.querystring("codigo_cac")
titulo=request.querystring("titulo")
descripcion_cac=request.querystring("descripcion_cac")

if codigo_per="" then codigo_per=session("codigo_usu")
if codigo_cac="" then codigo_cac=session("codigo_cac")
if modo="" then modo="D"
if codigo_per="" then codigo_per="-2"
tipo=1

Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsDoc= Obj.Consultar("ConsultarDocente","FO","CL",codigo_cac,0)
		if (codigo_cac<>"" and codigo_per<>"-2") then
			If session("codigo_tfu")="1" or session("codigo_tfu")="15" or session("codigo_tfu")="41" or session("codigo_tfu")="9" then
				if int(codigo_cac)>session("codigo_cac") then
					tipo=0
				else
					tipo=1
				end if
				
				Set rsHorario=obj.Consultar("ConsultarHorariosAmbiente","FO",15,codigo_cac,codigo_per,tipo,0)
			else
				if int(codigo_cac)<=session("codigo_cac") then
			        Set rsHorario=obj.Consultar("ConsultarHorariosAmbiente","FO",15,codigo_cac,codigo_per,tipo,0)
			    else
			        Set rsHorario=obj.Consultar("ConsultarHorariosAmbiente","FO",15,session("codigo_cac"),codigo_per,tipo,0)
			        tipo =2			        
			    end if 
			end if	

		end if
	Obj.CerrarConexion
Set Obj=nothing

function AnchoHora(byVal cad)
	if len(cad)<2 then
		AnchoHora="0" & cad
	else
		anchohora=cad
	end if
end function


if codigo_cac="" then codigo_cac=session("codigo_cac")

%>
<html xmlns="http://www.w3.org/1999/xhtml">
 <head>
  <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
  <link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css" media="print">
  <link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
  <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
  <script type="text/javascript" language="JavaScript" src="private/validarhorarios.js"></script>
  <style type="text/css">
   td {
	font-size   :9px;
	font-family :Tahoma;
	text-align  : center;
   }
  </style>
 </head>
 <body>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
   <tr>
    <td width="100%" colspan="3" class="usatTitulo" height="5%">Consulta de Horarios por Profesor</td>
   </tr>
   <!--<tr>
    <td width="20%" height="5%" class="etiqueta">Ciclo Académico</td>
    <td width="50%" height="5%"><%call ciclosAcademicos("GenerarVistaHorario('HC','" & modo & "')",codigo_cac,"","")%></td>
    <td width="30%" height="5%" rowspan="5" valign="top">
     <%if modo="A" then%>
      <img width="110px" align="FOTO DEL PROFESOR" src="../../imgpersonal/<%=codigo_per%>.jpg" height="120px" class="style1">
     <%end if%>
    </td>
   </tr>
   <tr>
    <td width="20%" class="etiqueta">Profesor</td>
    <td width="50%" style="text-align:left">
     <%
     if modo="A" then
      call llenarlista("cbocodigo_per","GenerarVistaHorario('HD')",rsDoc,"codigo_per","docente",codigo_per,"Seleccione el docente con Carga Académica","","")
	  else
	  response.write session("nombre_usu")
		titulo="HORARIOS: " & 	session("nombre_usu") & "(" & descripcion_cac & ")"
	 end if
	 %>
    </td>
   </tr>-->
   <%if (codigo_cac<>"" and codigo_per<>"-2") then%>
   <tr>
    <td width="70%" style="text-align:right;"  colspan="2"><input onclick="imprimir('N','','')" type="button" value="    Imprimir" name="cmdImprimir" class="usatimprimir"></td>
   </tr>
   <%end if%>
   <tr>
    <td width="70%" style="text-align:right;" colspan="2">&nbsp;</td>
   </tr>
   <tr>
    <td width="70%" style="text-align:right;" colspan="2">&nbsp;</td>
   </tr>
  </table>
  <h3>Horario de asignaturas asignadas</h3>
  <%
  if (codigo_cac<>"" and codigo_per<>"-2") then
  if tipo <2 then
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
	
	for f=1 to 16
		response.write vbtab & "<tr >" & vbcrlf
		
		for c=0 to 6
			if c=0 then		
				'response.write vbtab & "<td width='15%' class='etiquetaTabla'>" & f+6 & ":10 - " & f+1+6 & ":00</td>"  'linea anterior con los 10 min
				
				'============================================================================================================================================
				response.write vbtab & "<td width='15%' class='etiquetaTabla'>" & f+6 & ":00 - " & f+1+6 & ":00</td>"   'linea con horas exactas 08/11/2011
				'============================================================================================================================================
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
							if rsHorario("estadoHorario_lho")="A" then
							    TextoCelda=TextoCelda & rsHorario("nombre_cur") & "<br><font color='blue'>" & rsHorario("ambiente") & "</font><br><br>"
							else
							    TextoCelda=TextoCelda & rsHorario("nombre_cur") & "<br><font color='red'>" & rsHorario("ambiente") & "</font><br><br>"
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
	'if marcas>0 then
	'	response.write "<p class='etiqueta' align='right' id='totalhrs'></p>"
	'end if%>
	<script type="text/javascript" language="JavaScript">
		PintarCeldas()
	</script>
	<%
	else
		response.write "<h3>No se han asignado cursos al Profesor en el semestre académico seleccionado o los cursos no cuentan con horario asignado</h3>"
	end if
   else
   	response.write "<h5 style='color:Red'>Usted no puede visualizar horarios de semestres superiores al actual</h5>"
   end if 
	 
  end if

  if codigo_per<>"" then
  %>
  <!--<h3 >Horario de Asistencia</h3>-->
  <!-- <iframe id="fradetalle" height="900px" width="100%" border="0" frameborder="0" src="../../../librerianet/personal/frmvistadehorario.aspx?id=<%=codigo_per%>"></iframe> -->
  <!--<iframe id="Iframe1" height="900px" width="100%" border="0" frameborder="0" src="../../../librerianet/personal/frmvistatestdehorario.aspx?id=<%=codigo_per%>"></iframe>-->
  <%end if%>
</body>
</html>