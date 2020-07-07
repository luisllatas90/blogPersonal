<html>

<head>
	<meta http-equiv="Content-Language" content="es">
	<Title>Resumen de Matriculados y PreMatriculados 2006-II	</title>
	<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
    
</head>

<body>
<p class="usatTitulo">Matriculados por ciclo de ingreso</p>
<p class="etiqueta">Informe Hasta: <%=now%></p>
<%
Dim obj
Dim rs
Dim codigo_Cac
Dim codigo_cpf

codigo_Cac="25"

'codigo_Cac=request.Form("cbocicloacademico")
codigo_cpf=request.form("cboCarreraProfesional")



Dim objclicloacadema
Dim rsclicloacadema
Set objclicloacadema=Server.CreateObject("PryUSAT.clsDatCicloAcademico")
Set rsclicloacadema=Server.CreateObject("ADODB.Recordset")
Set rsclicloacadema= objclicloacadema.ConsultarCicloAcademico("RS","TO","")

Dim objCarrera
Dim rsCarrera
Set objCarrera=Server.CreateObject("PryUSAT.clsDatCarreraProfesional")
Set rsCarrera=Server.CreateObject("ADODB.Recordset")
Set rsCarrera= objCarrera.ConsultarCarreraProfesional("RS","TC","")
%>

<form name="frmParametro" method="post" action="matriculadosescuelaciclo.asp">
    <table width="66%"  border="0" style="border-collapse: collapse" bordercolor="#111111">
      <tr>
  	<td width="20%" class="etiqueta">Carrera Profesional:</td>
      	<td width="40%"><select name="cboCarreraProfesional" style="width: 100%"> 
	  	<option value="0">---Seleccione Carrera---</option>
			   <% do while not rsCarrera.eof
       				seleccionar="" 
					if(cint(codigo_cpf)=rsCarrera(0)) then 
						seleccionar="SELECTED"
						Carrera= rsCarrera("nombre_Cpf")
					end if
					%>
        			<option value="<%=rsCarrera(0)%>" <%=seleccionar%>>
					<%=rsCarrera("nombre_Cpf")%>
					</option>
					<% rsCarrera.movenext
				loop%>
        </select>
		</td>
  </tr>
	  <tr>
        <td width="20%" class="etiqueta">Ciclo Académico</td>
      <td width="40%"><select name="cbocicloacademico" disabled style="width: 100%">
	  <option value="0" >---Seleccione ciclo Academico---</option>
				   <% do while not rsclicloacadema.eof 
					seleccionar="" 
					if (cint(codigo_cac)=rsclicloacadema(0)) then seleccionar="SELECTED"
						
						
					%>
       				 <option value="<%=rsclicloacadema(0) %>" <%=seleccionar%>>
					<%=rsclicloacadema("descripcion_Cac")%>
					</option>
					<% rsclicloacadema.movenext
					loop
					%>
        </select>
		</td>
    </tr>
    <tr>
		<td colsPan=2>
        <Input type="Submit" value="Consultar" class="usatbuscar" style="float: right"></td> 
	</tr>

  </table>
</form>

<%

Set obj = Server.CreateObject("pryUSAT.clsDatMatricula")
Set rs = Server.CreateObject("ADODB.RecordSet")
Set rs= obj.ConsultarMatricula("RS","19",codigo_cpf,codigo_cac,"")



%>

<table align="center" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
	<tr>
		<td><b>Resultado de la Consulta:</b> <%=Carrera%></td>
	</tr>
</table>

<table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr class="etabla">
    <td width="20%" height="16">&nbsp;&nbsp;Semestre 
    de Ingreso</td>
    <td width="15%" align="center" height="16">Pre Mat.</td>
    <td width="15%" align="center" height="16">Mat.</td>
    <td width="15%" align="center" height="16">Total</td>

  </tr>
  <%
  	Dim Acum, TP, TM
  	Acum=0
  	TP=0
  	TM=0

	Do while Not rs.eof%>
	<tr>
		<td height="16" >&nbsp;&nbsp;<%=rs("cicloIng_Alu")%>&nbsp;</td>
		<td  align="center" height="16"><%=rs("TotalPre")%></td>
		<td  align="center" height="16"><%=rs("TotalMat")%></td>
		<%
			TP=TP+cdbl(rs("TotalPre"))
			TM=TM + cdbl(rs("TotalMat"))
			total=cdbl(rs("TotalMat")) + cdbl(rs("TotalPre"))
			Acum = Acum + total
		%>
		
		<td align="center" height="16"><%=total%></td>

	</tr>
	
	<%rs.Movenext
   Loop
	%>

  <tr>
   	<td  height="17">
	    <p align="right"><b><font face="Arial" size="2">&nbsp;Total:</font></b></td>
	<td height="17" align="center" class="etabla"><%=TP%></td>
   	<td height="17" align="center" class="etabla"><%=TM%></td>

  	<td align="center" height="17" class="etabla"><%=Acum%></td>
  </tr>
  </table>
<%
rs.Close
Set rs = Nothing
set obj =Nothing
%>
</body>

</html>