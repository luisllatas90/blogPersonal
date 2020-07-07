<!--#include file="../../../funciones.asp"-->
<%
if session("codigo_usu") = "" then
    Response.Redirect("../../../sinacceso.html")
end if

codigo_amb=request.querystring("codigo_amb")
codigo_cac=request.querystring("codigo_cac")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_amb="" then codigo_amb="-2"

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsCursos=Obj.Consultar("ConsultarHorarios","FO",21,codigo_amb,codigo_cac,0)
		if Not(rsCursos.BOF and rsCursos.EOF) then
			HayReg=true
		end if
	obj.CerrarConexion
Set Obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>HORARIOS POR AMBIENTE</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css" media="print">
<script language="JavaScript" src="../../../private/funciones.js"></script>
</head>
<body>
<%if HayReg=true then%>
	<table id="tblmatriculados" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse; border: 1px solid #808080" bordercolor="#111111" width="100%" bgcolor="white">
			<tr class="usatceldatitulo">
				<td width="4%" height="14" align="center">#</td>
				<td width="30%" height="14">Nombre del Curso</td>
				<td width="7%" height="14" align="center">GH</td>
				<td width="7%" height="14" align="center">D�a</td>
				<td width="4%" height="14" align="center">Inicio</td>
				<td width="3%" height="14" align="center">Fin</td>
				<td height="20" align="center" style="width: 14%">Docente</td>
				<td height="20" align="center" style="width: 3%">Matriculados</td>
				<td height="20" align="center" style="width: 2%">Vacantes</td>
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
					'nombre_cur=rsCursos("nombre_cur")  & "<br>" & rsCursos("abreviatura_cpf") & "(" & rsCursos("abreviatura_pes") & ")" & "<br><i>Inicio: " & rsCursos("fechainicio_Cup") & " - Fin: " & rscursos("fechaFin_cup")
					nombre_cur=rsCursos("nombre_cur")  & "<br>" & rsCursos("abreviatura_cpf") & "(" & rsCursos("abreviatura_pes") & ")" & "<br><i>Inicio: " & rsCursos("fechainicio_Cup") & " - Fin: " & rscursos("fechaFin_cup") & "<br>Fecha de registro: " & rscursos("fechareg_lho")
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
				
				<td width="5%" <%=clase%>><%=ConvDia(rsCursos("dia_Lho"))%></td>
				<td width="4%" align="center" <%=clase%>><%=rsCursos("nombre_hor")%></td>
				<td width="3%" align="center" <%=clase%>><%=rsCursos("horaFin_Lho")%></td>
				<td width="20%" <%=clase%>><%=rsCursos("docente")%>&nbsp;</td>
				<td width="3%" <%=clase%> align="center"><%=rsCursos("matriculados")%>&nbsp;</td>
				<td width="2%" <%=clase%> align="center"><%=rsCursos("vacantes_Cup")%>&nbsp;</td>
			</tr>
				<%		
				rsCursos.movenext
			Loop
			%>
		</table>
<%elseif codigo_amb<>"-2" then%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado Registros</h5>
<%end if

Set rsCursos=nothing
%>
</body>
</html>