<!--#include file="../../../../funciones.asp"-->
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Lista de Alumnos Matriculados Por Grupo Horario</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">

</head>
<body>

<table align="center" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
	<tr>
		<td align="center">
			<b><%=request.querystring("cp")%>:</b>
        <font face="Arial" size="4"> <b><%=request.querystring("nc")%> ( <%=request.querystring("gh")%> ) </b></font></td>
		<td align="right" style="cursor:hand" onClick="history.back(-1)">
        <font color="#0000FF"><b>Regresar</b></font></td>
	</tr>
	
</table>


<%
Dim Refcodigo_cup, codigo_cac, total, Acum
Dim obj, rs

'Recuperar Parametros Pasados de Pagina : rpteconsultaMatriculadosGrupoHorarioAgrupados.asp
Refcodigo_cup = request.querystring("codigo_cup")
codigo_cac= request.querystring("codigo_cac")

estado_Dma=request.querystring("estado")
tipo=request.querystring("tipo")
cp=request.querystring("cp")
nc=request.querystring("nc")
gh=request.querystring("gh")

if tipo="" then tipo="1"
IF estado_Dma="M" THEN etiqueta="Matriculados"
IF estado_Dma="P" THEN etiqueta="Pre Matriculados"





'Consultar Cursos Programados Separados
Set obj = Server.CreateObject("pryUSAT.clsDatMatricula")
Set rs = Server.CreateObject("ADODB.RecordSet")
Set rs= obj.ConsultarAlumnosMatriculados("RS","26",RefCodigo_cup,estado_Dma,"")%>

<table align="center" border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td height="16" width="20%" class="usatEncabezadoTabla"><font face="Arial" size="2"><b>Plan de Estudio </b></td>
    <td align="center" height="16" width="15%" class="usatEncabezadoTabla"><b><font face="Arial" size="2">
    Código</font></b></td>
    <td align="left" height="16" width="45%" class="usatEncabezadoTabla"><b><font face="Arial" size="2">&nbsp;&nbsp;Curso</font></b></td>
    <td align="center" height="16" width="5%" class="usatEncabezadoTabla"><b><font face="Arial" size="2">G.H.</font></b></td>
    <td  align="center" height="16" width="5%" class="usatEncabezadoTabla"><b><font face="Arial" size="2">Cred.</font></b></td>
    <td  align="center" height="16" width="5%" class="usatEncabezadoTabla"><b><font face="Arial" size="2">Ciclo</font></b></td>
    <td align="center" height="16" width="8%" class="usatEncabezadoTabla"><b><font face="Arial" size="2">
    Nº Alu</font></b></td>
    
  </tr>
  <%
  total=0
  Do while Not rs.eof
  total = total + cint(rs("NroMatriculados"))
  %>
  <tr>

	<td width="20%" height="16" class="Cajas">&nbsp;&nbsp;<%=rs("abreviatura_pes")%>&nbsp;</td>
	<td width="15%" align="center" height="16" class="Cajas"><%=rs("identificador_Cur")%></td>
	<td width="45%" align="left" height="16" class="Cajas">&nbsp;&nbsp;<%=rs("nombre_Cur")%></td>
	<td width="5%" align="center" height="16" class="Cajas"><%=rs("GrupoHor_Cup")%></td>
	<td width="5%" align="center" height="16" class="Cajas"><%=rs("creditos_Cur")%></td>
	<td width="5%" align="center" height="16" class="Cajas"><%=rs("ciclo_Cur")%></td>
	<td width="8%" align="center" height="16" class="Cajas"><%=rs("NroMatriculados")%></td>
		
 </tr>
	
	<%rs.Movenext
     Loop
	%>
<tr>
<td colsPan="6" align="right"><b>Total:</b></td>
<td class="usatEncabezadoTabla" align="center"><%=total%>&nbsp;</td>
</tr>
  
</table>

<%
'Consultar Listado de Alumnos

if estado_Dma="M" then
	if tipo="1" then
		Set rs= obj.ConsultarMatricula("RS","25A",codigo_cac,Refcodigo_cup,"") 'Incluye Pagos
	else
		Set rs= obj.ConsultarMatricula("RS","25B",codigo_cac,Refcodigo_cup,"") 'Incluye Pagos
	end if
else
	Set rs= obj.ConsultarAlumnosMatriculados("RS","25",codigo_cac,Refcodigo_cup,estado_Dma)
end if


%>

<table align="center" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber2">
	<tr>
		<td align="Center">
			<%if estado_Dma="M" then%>
				<p style="line-height: 200%"><font face="Arial" size="2"><font color="#0000FF"><b>MATRICULADOS</b></font>
			<%else%>	
				<p style="line-height: 200%"><font face="Arial" size="2"><font color="#0000FF"><b>PRE MATRICULADOS</b></font>

			<%end if%>
            </font>		
		</td>
	</tr>
	<tr>
		<td>Para ordenar Por fecha de pago  haga click en <b>fecha de Pago</b> 		| Para ordenar Por alumno haga click en <b>Alumno<b></b></td>
	</tr>
	<tr><td><%call botonExportar("../../../","xls","alumnosmatriculados","S","B")%></td></tr>
</table>

<br>
<table align="center" border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber4">
  <tr class="Cab">
    <td height="16" width="3%" class="usatEncabezadoTabla"><font face="Arial" size="2"><b>
    Ítem</b></td>
    <td align="center" height="16" width="10%" class="usatEncabezadoTabla"><b><font face="Arial" size="2">
    Código Univer.</font></b></td>
    <td align="left" height="16" width="35%" class="usatEncabezadoTabla"><b><font face="Arial" size="2">&nbsp;<a href="listaAlumnosMatAgrupados.asp?codigo_Cup=<%=refcodigo_cup%>&codigo_cac=<%=codigo_cac%>&estado=<%=estado_dma%>&cp=<%=cp%>&nc=<%=nc%>&gh=<%=gh%>&tipo=1">Alumno</a></font></b></td>
    <td  align="center" height="16" width="5%" class="usatEncabezadoTabla"><b><font face="Arial" size="2">Ciclo Ingreso</font></b></td>
    <td  align="center" height="16" width="30%" class="usatEncabezadoTabla" style="text-align: left"><b><font face="Arial" size="2">&nbsp;Escuela</font></b></td>
    <td height="16" width="8%" align="center" class="usatEncabezadoTabla"><font size="2"></font><b><font face="Arial" size="2"><a href="listaAlumnosMatAgrupados.asp?codigo_Cup=<%=refcodigo_cup%>&codigo_cac=<%=codigo_cac%>&estado=<%=estado_dma%>&cp=<%=cp%>&nc=<%=nc%>&gh=<%=gh%>&tipo=2">Fecha 
    Pago</font></b></td>
    <td  align="center" height="16" width="10%" class="usatEncabezadoTabla"><b><font face="Arial" size="2">Plan Est.</font></b></td>
  </tr>
  <%
  
  	Acum=0

	Do while Not rs.eof
		Acum = Acum + 1 %>
	<tr class="Con">
		<td width="3%" height="16" class="Cajas">&nbsp;&nbsp;<%=Acum%>&nbsp;</td>
		<td width="10%" align="center" height="16" class="Cajas"><%=rs("codigoUniver_alu")%></td>
		<td width="35%" align="left" height="16" class="Cajas">&nbsp;<%=ucase(rs("Alumno"))%></td>
		<td width="5%" align="center" height="16" class="Cajas"><%=rs("cicloIng_Alu")%></td>
		<td width="30%" align="left" height="16" class="Cajas">&nbsp;<%=rs("nombre_Cpf")%></td>
	        <td width="8%" height="16" align="center" class="Cajas"><%if estado_Dma="M" then response.write(rs("fecha_Cin")) end if%></td>
		<td width="10%" align="left" height="16" class="Cajas">&nbsp;<%=rs("abreviatura_Pes")%></td>
	</tr>
	
	<%rs.Movenext
   Loop
   
	if rs.recordcount >0 then
		rs.movefirst
	end if
	
	ArrEncabezados=Array("Código Universitario","Apellidos y Nombres","Ciclo de Ingreso","Escuela Profesional")
	ArrCampos=Array("codigouniver_alu","alumno","cicloIng_alu","nombre_cpf")
	ArrCeldas=Array("15%","55%","10%","25%")
	
	titulorpte="Lista de Alumnos Matriculados" & nc

	call ValoresExportacion(titulorpte,ArrEncabezados,rs,Arrcampos,ArrCeldas)	
	%>
  
  </table>

<%
set rs=nothing
set obj = nothing
%>

</body>

</html>