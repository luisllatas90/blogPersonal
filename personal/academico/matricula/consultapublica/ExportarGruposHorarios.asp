<%
Response.ContentType = "application/vnd.ms-excel"
Response.AddHeader "Content-Disposition","attachment;filename=" & "Consulta" & ".xls"
%>
<html>
	<head>
		<title>Reporte Matriculados Grupo Horario</title>
	    <style>
			<!--
			.usatCabeceraCelda {
					    COLOR: #2F4F4F;
					    FONT-FAMILY: Verdana, Arial, Helvetica, Sans-serif;
					    background-color: #FFF8DC;
					    font-size: 12;
					    text-align: center;
			}
			.etiqueta    { font-weight: bold }
			.etabla { color: Black; font-weight: bold; background-color: #000080; text-align:center}
			.text  	{ font-family: Verdana; font-size: 8.5pt}
}
			-->
		</style>
	</head>

<body>

<%
codigo_cpf= request.querystring("cp")
codigo_cac=request.querystring("ca")
ciclo=request.querystring("cc")

IF codigo_cpf="0" THEN codigo_cpf="%%"
IF ciclo="0" THEN ciclo="%%"

Set obj = Server.CreateObject("pryUSAT.clsDatMatricula")
Set rs = Server.CreateObject("ADODB.RecordSet")
Set rs= obj.ConsultarAlumnosMatriculados("RS","27",codigo_cpf,codigo_cac,ciclo)

%>
<table align="center" border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" >
  <tr  height="20" >
    
    <td class="usatCabeceraCelda" width="35%" align="left">&nbsp;ASIGNATURA</td>
    <td class="usatCabeceraCelda" align="center"  width="3%">G.H.</td>
    <td class="usatCabeceraCelda" align="center"  width="4%">CICLO</td>
    <td class="usatCabeceraCelda" align="left"  width="30%">&nbsp;CARRERA</td>
    <td class="usatCabeceraCelda" align="center" width="5%">MATRIC</td>
    <td class="usatCabeceraCelda" align="center" width="5%">PRE MAT</td>
    <td class="usatCabeceraCelda" align="center" width="5%">TOTAL</td>

    
  </tr>
  <%Do while Not rs.eof	%>
    <tr height="20" class="text">
		<td width="35%" align="left"><%=rs("nombre_Cur")%></td>
		<td width="3%" align="center" ><%=rs("GrupoHor_Cup")%></td>
		<td width="4%" align="center" ><%=cint(trim(rs("ciclo_Cur")))%></td>
		<td width="30%" align="left" ><%=rs("nombre_Cpf")%></td>
		<td width="5%" align="center" ><%=cint(trim(rs("NroMatriculados")))%></td>
		<td width="5%" align="center" ><%=cint(trim(rs("NroPreMatriculados")))%></td>
		<td width="5%" align="center" ><%=cint(trim(rs("NroMatriculados")))+ cint(trim(rs("NroPreMatriculados")))%></td>

	</tr>
	
	<%
	rs.Movenext
   Loop
	%>
  
  </table>

<%
rs.Close
Set rs = Nothing
set obj =Nothing
%>

</body>
</html>