<!--#include file="../../../../funciones.asp"-->
<!--#include file="../../../../funciones.asp"-->
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Reporte de Ponderado acumulado</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../private/validarcursomatriculado.js"></script>
</head>

<body>

<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" >
  <tr>
    <td>
     <p align="center"><b><font face="Arial" size="4" color="#800000">Créditos 
     Aprobados Por Semestre de Ingreso</font></b><Br align="center" class="etiqueta">
</td>
  </tr>
</table>

<%

Dim obj
Dim rs
Dim codigo_Cac
Dim codigo_cpf

codigo_Cac=request.Form("cbocicloacademico")
codigo_cpf=request.form("cboCarreraProfesional")

Dim objclicloacadema
Dim rsclicloacadema
Set objclicloacadema=Server.CreateObject("PryUSAT.clsDatCicloAcademico")
Set rsclicloacadema=Server.CreateObject("ADODB.Recordset")
Set rsclicloacadema= objclicloacadema.ConsultarCicloAcademico("RS","TO","")

Dim objCarrera
Dim rsCarrera
dim id_per
Set objCarrera=Server.CreateObject("PryUSAT.clsDatCarreraProfesional")
Set rsCarrera=Server.CreateObject("ADODB.Recordset")
Set rsCarrera= objCarrera.ConsultarCarreraProfesional("RS","TC","")
'Set rsCarrera= objCarrera.ConsultarCarreraProfesional("RS","PG",request.QueryString("id"))
%>

<form name="frmParametro" method="post" action="creditoalumnociclo.asp?modo=R&id=<% response.write request.QueryString("id") %>">
<div align="center">
    <table width="66%"  border="0" style="border-collapse: collapse" bordercolor="#111111">
      <tr>
  	<td width="20%" class="etiqueta">Carrera Profesional:</td>
      	<td width="40%"><select name="cboCarreraProfesional" style="width: 100%"> 
	  	<option value="0">---Seleccione Carrera---</option>
			   <% do while not rsCarrera.eof
       				seleccionar="" 
					if (Cint(codigo_Cpf)=rsCarrera("codigo_Cpf")) then seleccionar="SELECTED"
					
					<!--Carrera= rsCarrera("nombre_Cpf")-->
				    
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
        <td width="20%" class="etiqueta">Semestre Ingreso:</td>
      <td width="40%"><select name="cbocicloacademico" style="width: 100%">
	  <option value="0" >---Seleccione Semestre Academico---</option>
				   <% do while not rsclicloacadema.eof 
					seleccionar="" 
					if ((codigo_cac)=rsclicloacadema("descripcion_Cac")) then seleccionar="SELECTED"
												
					%>
       				 <option value="<%=rsclicloacadema("descripcion_Cac")%>" <%=seleccionar%>>
					<%=rsclicloacadema("descripcion_Cac")%>
					</option>
					<% rsclicloacadema.movenext
					loop
					%>
        </select>
		</td>
    </tr>
    <tr>
		<td colsPan=2 align="right">
        <Input type="Submit" value="Consultar" class="usatbuscar">
        <%call botonExportar("../../../","xls","CreditosAprobado","S","B")%>
        </td> 
	</tr>
    </table>

   <%if request.querystring("modo")="R" then%>
    <table width="100%" height="80%">
    <tr><td>
<%

Set obj = Server.CreateObject("pryUSAT.clsDatMatricula")
Set rs = Server.CreateObject("ADODB.RecordSet")
Set rs= obj.consultarNroAlumnosMatriculados("RS","SI",codigo_cpf,codigo_cac)

'---------------------

Dim ArrCampos,ArrEncabezados,ArrCeldas

	ArrEncabezados=Array("Codigo Universitario","Apellidos y Nombres","Plan Estudio","Cred Aprob.")
	ArrCampos=Array("codigoUniver_alu","Alumno","abreviatura_Pes","credAprobados")
	ArrCeldas=Array("15%","70%","10%","20%")
	pagina="MostrarHistorial(this)"
	
	titulorpte="Creditos Aprobados"

	call CrearRpteTabla(ArrEncabezados,rs,"",ArrCampos,ArrCeldas,"S","V",pagina,"S","","")
	call ValoresExportacion(titulorpte,ArrEncabezados,rs,Arrcampos,ArrCeldas)

'--------------------

'rs.Close
'Set rs = Nothing
set obj =Nothing
%>
</td></tr>
  </table>
 <%end if%>

  </div>
</form>
</body>

</html>