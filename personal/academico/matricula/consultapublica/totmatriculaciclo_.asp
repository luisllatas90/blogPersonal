<html>

<head>
	<meta http-equiv="Content-Language" content="es">
	<Title>Resumen de Matriculados y PreMatriculados 2006-II</title>
	<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
</head>

<body>

<%
Dim obj
Dim rs
Dim codigo_Cac
Dim codigo_cpf

codigo_Cac="25"
codigo_cpf="1"


Set obj = Server.CreateObject("pryUSAT.clsDatMatricula")

Set rs = Server.CreateObject("ADODB.RecordSet")
Set rs= obj.ConsultarMatricula("RS","14",codigo_cac,"","")
TotalMat = rs("contador") 'Total de MAtriculados (han pagado cpto matricula)

Set rs= obj.ConsultarMatricula("RS","15",codigo_cac,"","")
TotalPre = rs("contador") 'Total de Prematriculados (no han pafago cpto de matricula)
%>
<p class="usattitulo">Matriculados y Pre Matriculados 2006-II</p>
<p class="etiqueta">Fecha de actualización: <%=now%></p>
<p><b>Resumen de Matricula:</b></p>

<table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
  <tr>
    <td width="85%">&nbsp;Nro. de Alumnos <b>
    Pre Matriculados </b>
    (No Han Pagado el concepto de Matricula)
    <td width="15%" align="center"><%=TotalPre%>&nbsp;</td>
  </tr>

  <tr>
    <td width="85%">&nbsp;Nro. de Alumnos<b>
   Matriculados </b>
   (Han Pagado el concepto de Matricula)</td>
    <td width="15%" align="center"><%=TotalMat%>&nbsp;</td>
  </tr>
  <tr>
    <td width="85%" align="right">Total:</td>
    <td width="15%" align="center" class="etabla"><% =totalMat + TotalPre %>&nbsp;</td>
  </tr>
</table>

<p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Haga clic sobre la celda de la columna Pre Mat. o Mat. para ver la lista de 
estudiantes</p>

<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber4">
  <tr class="etabla">
    <td width="55%" height="16"><font face="Arial" size="2"><b>&nbsp;Escuela Profesional</b></td>
    <td width="15%" align="center"><b><font face="Arial" size="2">Pre Mat.</font></b></td>
    <td width="15%" align="center"><b><font face="Arial" size="2">Mat.</font></b></td>
    <td width="15%" align="center"><b><font face="Arial" size="2">Total</font></b></td>

  </tr>
  <%
  	Dim Acum, TP, TM
  	Acum=0
  	TP=0
  	TM=0
	Set rs= obj.ConsultarMatricula("RS","16",codigo_cac,"","")

	Do while Not rs.eof%>
	<tr>
    	
 		<td width="55%" height="16">&nbsp;&nbsp;<%=rs("nombre_Cpf")%>&nbsp;</td>
		<td width="15%" align="center" <%if rs("Pre")>0 then%> style="cursor:hand" onclick="location.href='ListaAlumnosMatPreCarrera.asp?ca=<%=codigo_cac%>&cp=<%=rs("codigo_cpf")%>&esc=<%=rs("nombre_Cpf")%>&tip=P'" <%end if%>><%=rs("Pre")%>&nbsp;</td>
		<td width="15%" align="center" <%if rs("Mat")>0 then%> style="cursor:hand" onclick="location.href='ListaAlumnosMatPreCarrera.asp?ca=<%=codigo_cac%>&cp=<%=rs("codigo_cpf")%>&esc=<%=rs("nombre_Cpf")%>&tip=M'" <%end if%>><%=rs("Mat")%>&nbsp;</td>
		<%
			TP=TP+cdbl(rs("Pre"))
			TM=TM + cdbl(rs("Mat"))
			total=cdbl(rs("Mat")) + cdbl(rs("Pre"))
			Acum = Acum + total
		%>
		
		<td width="15%" align="center"><%=total%>&nbsp;</td>

	</tr>
	
	<%rs.Movenext
   Loop
	%>

  <tr>
   	<td width="55%" height="17">
	    <p align="right"><b><font face="Arial" size="2">&nbsp;Total:</font></b></td>
	<td width="15%" align="center" class="etabla"><%=TP%>&nbsp;</td>
   	<td width="15%" align="center" class="etabla"><%=TM%>&nbsp;</td>

  	<td width="15%" align="center" class="etabla"><%=Acum%>&nbsp;</td>
  </tr>
  
  
  </table>


<%
rs.Close
Set rs = Nothing
set obj =Nothing
%>
</body>

</html>