<%
codigo_alu=request.querystring("codigo_alu")
tipo=request.querystring("tipo")
Encontrado=false
HayReg=false

if tipo="T" then tipoconsulta="TG":tipo=" pertenecen al Tercio Estudiantil." : modo="Tercio estudiantil"
if tipo="Q" then tipoconsulta="TQ":tipo=" pertenecen al Quinto Estudiantil." : modo="Quinto estudiantil"

if codigo_alu<>"" then
	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsAlumnos=Obj.Consultar("ConsultarPonderadoAlumno_Tercio","FO",tipoconsulta,codigo_alu)
		Obj.CerrarConexion
		
		if Not(rsAlumnos.BOF and rsAlumnos.EOF) then
			HayReg=true
		End if
	Set Obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Cuadro de méritos por estudiante</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
</head>
<body>
<p class="usatTitulo">Estudiante que pertenecen al Cuadro de méritos (<%=modo%>)</p>
<%if HayReg=false then%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se ha encontrado estudiantes en el Cuadro de Méritos</h5>
<%else%>
<table id="tblmeritos" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#C0C0C0" width="100%" id="AutoNumber1">
  <tr class="usatceldatitulo">
    <td width="5%" align="center">Orden</td>
    <td width="15%" align="center">Código Universitario</td>
    <td width="60%" align="center">Alumno</td>
    <td width="5%" align="center">Créditos Matriculados</td>
    <td width="5%" align="center">Créditos Aprobados</td>
    <td width="5%" align="center">Créditos Desaprobadas</td>
    <td width="5%" align="center">Asignaturas Desaprobadas</td>
    <td width="5%" align="center">Promedio Ponderado</td>
    <td width="5%">Estado</td>
  </tr>
  <%
	i=0
	total=rsAlumnos.recordcount
	limite=total
		
	Do While Not rsAlumnos.EOF
		estado="No Apto"
		clase=""
  		NroCredDesaprobados=rsAlumnos("NroCredMatriculados")-rsAlumnos("NroCredAprobados")
		
		i=i+1
	
		if cdbl(rsAlumnos("codigo_alu"))=cdbl(codigo_alu) then 
			clase="class=selected"
			Encontrado=true
		end if
  %>
  <tr <%=clase%>>
    <td width="5%" id="<%=rsAlumnos("codigo_alu")%>"><%=rsAlumnos("pto")%>&nbsp;</td>
    <td width="15%"><%=rsAlumnos("codigouniver_alu")%>&nbsp;</td>
    <td width="60%"><%=rsAlumnos("alumno")%>&nbsp;</td>
    <td width="5%"><%=rsAlumnos("NroCredMatriculados")%>&nbsp;</td>
    <td width="5%"><%=rsAlumnos("NroCredAprobados")%>&nbsp;</td>
    <td width="5%"><%=NroCredDesaprobados%>&nbsp;</td>
    <td width="5%"><%=rsAlumnos("NroAsigDesaprobadas")%>&nbsp;</td>    
    <td width="5%"><%=FormatNumber(rsAlumnos("Ponderado"),4)%>&nbsp;</td>
    <td width="5%"><%'=estado%>&nbsp;</td>
  </tr>
  <%  	rsAlumnos.movenext
  	Loop
  	Set rsAlumnos=nothing
  %>
  <tr class="usatTablaInfo" >
    <td colspan="9">&nbsp;<%=limite%> estudiantes <%=tipo%>
    </td>
  </tr>
  </table>
	<%if i=0 then
		response.write "No se ha encontrado estudiantes en el Cuadro de Méritos especificado"
	end if
end if

if Encontrado=false and HayReg=true then
%>
<h5 class="azul">El estudiante no está en el CUADRO DE MÉRITOS (<%=modo%>)</h5>
<script type="text/javascript" language="javascript">
	tblmeritos.style.display="none"
</script>
<%end if%>
</body>
</html>
<%end if%>