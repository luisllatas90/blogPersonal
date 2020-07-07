<%
on error resume next

codigo_cpf=request.querystring("codigo_cpf")
cicloingreso=request.querystring("cicloingreso")
codigo_cacini=request.querystring("codigo_cacini")
codigo_cacfin=request.querystring("codigo_cacfin")
incluir=request.querystring("incluir")
tipo=request.querystring("tipo")

if tipo="A" then divisor=0:tipo=" que pertenecen al ciclo de Ingreso " & cicloingreso
if tipo="T" then divisor=3:tipo=" pertenecen al Tercio Estudiantil de un total de "
if tipo="Q" then divisor=5:tipo=" pertenecen al Quinto Estudiantil de un total de "

if codigo_cpf<>"" then
	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsAlumnos=Obj.Consultar("ConsultarPonderado_Tercio","FO","TO",codigo_cpf,cicloIngreso,codigo_cacini,codigo_cacfin)
		Obj.CerrarConexion
	Set Obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Pagina nueva 1</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../private/validarreportes.js"></script>
</head>
<body topmargin="0" leftmargin="0">
<%if rsAlumnos.BOF and rsAlumnos.EOF then%>
	<p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se ha encontrado estudiantes en el Cuadro de Méritos</p>
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
		
	pagina="lstcuadromeritos.asp?tipo=A&codigo_cpf=" & codigo_cpf & "&cicloingreso=" & cicloingreso & "&codigo_cacini=" & codigo_cacini & "&codigo_cacfin=" & codigo_cacfin & "&incluir=" & incluir
	'Extraer hasta qué orden se mostrará los estudiantes
	if divisor>0 then
		limite=cint(total/divisor)
		tipo=tipo & " " & total & " estudiantes, según el ciclo de ingreso especificado."
		tipo=tipo & "<br>&nbsp;Si desea ver a todos los estudiantes de la Promoción <span class='rojo'><a href='" & pagina & "'>Haga click aquí</a></span>"
	else
		limite=total
	end if
	  
  	'Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	'Obj.AbrirConexion

	
	Do While Not rsAlumnos.EOF
		estado="No Apto"
		clase=""
  		NroCredDesaprobados=rsAlumnos("NroCredMatriculados")-rsAlumnos("NroCredAprobados")

		'Set rsMerito=Obj.Consultar("DeterminarInclusionEnTercio","FO",rsAlumnos("codigo_alu"),cicloIngreso,codigo_cacfin)
		'if int(rsMerito(0))=1 then
		'	estado="Apto"
		'	clase="class=azul"
		'end if
		
		'response.write "codigo_alu=" & rsAlumnos("codigo_alu") & "=" & int(rsMerito(0)) & "<br>"
		i=i+1
		
		if (i<=limite) then
  %>
  <tr <%=clase%> onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onclick="AbrirHistorial('<%=rsAlumnos("codigouniver_alu")%>')">
    <td width="5%" id="<%=rsAlumnos("codigo_alu")%>"><%=i%>&nbsp;</td>
    <td width="15%"><%=rsAlumnos("codigouniver_alu")%>&nbsp;</td>
    <td width="60%"><%=rsAlumnos("alumno")%>&nbsp;</td>
    <td width="5%"><%=rsAlumnos("NroCredMatriculados")%>&nbsp;</td>
    <td width="5%"><%=rsAlumnos("NroCredAprobados")%>&nbsp;</td>
    <td width="5%"><%=NroCredDesaprobados%>&nbsp;</td>
    <td width="5%"><%=rsAlumnos("NroAsigDesaprobadas")%>&nbsp;</td>    
    <td width="5%"><%=FormatNumber(rsAlumnos("Ponderado"),4)%>&nbsp;</td>
    <td width="5%"><%'=estado%>&nbsp;</td>
  </tr>
  <%  	end if
	  rsAlumnos.movenext
  	Loop
	'Obj.CerrarConexion
	'Set Obj=nothing
	'Set rsMerito=nothing
  	Set rsAlumnos=nothing
  %>
  <tr class="usatTablaInfo" >
    <td colspan="9">&nbsp;<%=limite%> estudiantes <%=tipo%>
    </td>
  </tr>
  </table>
	<%if i=0 then%>
		<p class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se ha encontrado estudiantes en el Cuadro de Méritos especificado</p>
	<%end if
end if%>
</body>
</html>
<%end if%>
<script>parent.document.all.mensaje.innerHTML=""</script>
<%if err.number>0 then
	Obj.CerrarConexion
	Set Obj=nothing

	response.write "Ha ocurrido el Error " & Err.number & "<br>" & Err.description
end if
%>