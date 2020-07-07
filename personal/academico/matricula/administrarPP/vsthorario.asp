<!--#include file="../../../../funciones.asp"-->
<%
codigo_alu=session("codigo_alu")
codigo_pes=session("codigo_pes")
codigo_cac=request.querystring("codigo_cac")

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCursos=Obj.Consultar("ConsultarCursoProgramadoPorAsesor","FO","2",codigo_alu,codigo_cac,codigo_pes,0)
	obj.CerrarConexion
Set Obj=nothing
%>
<HTML>
	<HEAD>
	<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
	<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
	<title>Horario de Estudiante</title>
	</HEAD>
	<body oncontextmenu="return event.ctrlKey" bgcolor="#EEEEEE">
	<%if Not(rscursos.BOF and rsCursos.EOF) then%>
	<table id="tblmatriculados" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse; border: 1px solid #808080" bordercolor="#111111" width="100%" bgcolor="white">
			<tr class="etabla">
				<td width="4%" height="14" align="center">#</td>
				<td width="41%" height="14">Nombre del Curso</td>
				<td width="7%" height="14" align="center">GH</td>
				<td width="7%" height="14" align="center">Día</td>
				<td width="4%" height="14" align="center">Inicio</td>
				<td width="3%" height="14" align="center">Fin</td>
				<td width="3%" height="14" align="center">Ambiente</td>
				<td height="14" align="center" style="width: 14%">Docente</td>
			</tr>
			<%
			nombre_cur=""
			codigoAnterior_cup=0
			grupohor_cup=""
			
			Do while not rsCursos.eof			
			%>
			  <tr id="fila<%=i%>">
			  	<!--Se debe combinar siempre y cuando se repita el curso-->
			  	<%
			  	if (codigoAnterior_cup)<>rsCursos("codigo_cup") then
			  		num=true
			  		clase="class=""bordesup"""
					nombre_cur=rsCursos("nombre_cur") & "<br><i>Inicio: " & rsCursos("fechainicio_Cup") & " Fin: " & rsCursos("fechafin_Cup")
					codigoAnterior_cup=rsCursos("codigo_cup") 
					grupohor_cup=rsCursos("grupohor_cup")
					i=i+1
		  		else
					num=false
		  			clase=""
		  			nombre_cur=""
		  			grupohor_cup=""
		  		end if
		  		%>
				<td align="center" <%=clase%>>
				<%
				
				if num=true then
					response.write(i)
				end if
				%>
				</td>
				<td width="30%" <%=clase%>><%=nombre_cur%></td>
				<td width="5%" align="center" <%=clase%>><%=grupohor_cup%></td>
				
				<td width="5%" <%=clase%>><%=UCASE(ConvDia(rsCursos("dia_Lho")))%></td>
				<td width="4%" align="center" <%=clase%>><%=rsCursos("nombre_hor")%></td>
				<td width="3%" align="center" <%=clase%>><%=rsCursos("horaFin_Lho")%></td>
				<td width="3%" align="center" <%=clase%>><%=rsCursos("ambiente")%></td>
				<td width="20%" <%=clase%>><%=rsCursos("docente")%>&nbsp;</td>
			</tr>
				<%		
				rsCursos.movenext
			Loop
			
			Set rsCursos=nothing
			%>
		</table>
	<%else%>
		<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado Horarios registrados 
		en el ciclo académico seleccionado</h5>
	<%end if%>
	</body>
</HTML>
