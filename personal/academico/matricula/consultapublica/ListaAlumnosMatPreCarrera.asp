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


<%
Dim Refcodigo_cup, codigo_cac, total, Acum
Dim obj, rs

'Recuperar Parametros: 

codigo_cac= request.querystring("ca")
codigo_cpf= request.querystring("cp")
tipo= request.querystring("tip")
escuela = request.querystring("esc")


'Consultar Listado de Alumnos
Set obj = Server.CreateObject("pryUSAT.clsDatMatricula")
Set rs = Server.CreateObject("ADODB.RecordSet")

if tipo="M" then
	Set rs= obj.ConsultarAlumnosMatriculados("RS","1",codigo_cac,codigo_cpf,"")
end if
if tipo="P" then
	Set rs= obj.ConsultarAlumnosMatriculados("RS","2",codigo_cac,codigo_cpf,"")
end if


%>

<table align="center" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="80%" id="AutoNumber2">
	<tr>
		<td align="left">
			<%if tipo="M" then%>
				<p style="line-height: 200%"><font face="Arial" size="2">
                <font color="#800000"><b>MATRICULADOS : <%=escuela%></b>
			<%else%></font><p style="line-height: 200%"><font color="#800000"><b>PRE MATRICULADOS : <%=escuela%></b>

			<%end if%> </font></td>
		<td align="right">
	        <input type="button" value="Regresar" onclick="history.back(-1)" class="salir">
	        <%call botonExportar("../../../../","xls","alumnosmatriculados","S","B")%>
        </td>

		
	</tr>
</table>


<table align="center" border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="80%" id="AutoNumber4">
  <tr class="Cab">
    <td height="16" width="3%" class="usatEncabezadoTabla"><font face="Arial" size="2"><b>
    Ítem</b></td>
    <td align="center" height="16" width="5%" class="usatEncabezadoTabla"><b><font face="Arial" size="2">
    Código Univer.</font></b></td>
    <td align="left" height="16" width="25%" class="usatEncabezadoTabla">
    <p style="text-align: left"><b><font face="Arial" size="2">&nbsp;Alumno</font></b></td>
    <td  align="center" height="16" width="5%" class="usatEncabezadoTabla"><b><font face="Arial" size="2">Ciclo Ingreso</font></b></td>
  </tr>
  <%
  	Acum=0

	Do while Not rs.eof
		Acum = Acum + 1 %>
	<tr>
		<td width="3%" height="16" class="Cajas" align="center"><%=Acum%></td>
		<td width="5%" align="center" height="16" class="Cajas"><%=rs("codigoUniver_alu")%></td>
		<td width="25%" align="left" height="16" class="Cajas">&nbsp;<%=ucase(rs("Alumno"))%></td>
		<td width="5%" align="center" height="16" class="Cajas"><%=rs("cicloIng_Alu")%></td>
	</tr>
	
	<%rs.Movenext
   Loop

   rs.movefirst
	ArrEncabezados=Array("Código Universitario","Apellidos y Nombres","Ciclo de Ingreso")
	ArrCampos=Array("codigouniver_alu","alumno","cicloIng_alu")
	ArrCeldas=Array("15%","55%","10%")
	
	titulorpte="Lista de Alumnos Matriculados en la Escuela Profesional de " & escuela

	call ValoresExportacion(titulorpte,ArrEncabezados,rs,Arrcampos,ArrCeldas)	
   
	%>
  
  </table>

<%
set rs=nothing
set obj = nothing
%>

</body>

</html>