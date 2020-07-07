<!--#include file="../../../funciones.asp"-->
<%
dim obj
codigo_cac=request.querystring("codigo_cac")
codigo_dac=request.querystring("codigo_dac")
if codigo_cac="" then codigo_cac=session("codigo_cac")

Set obj=Server.CreateObject("PryUSAT.clsDatCicloAcademico")
	Set rsCac= obj.ConsultarCicloAcademico ("RS","TO","")
Set obj=nothing

Set obj=Server.CreateObject("PryUSAT.clsDatDepartamentoAcademico")
	Set rsDpto= obj.ConsultarDepartamentoAcademico ("RS","TO","")
Set obj=nothing
%>
<html>
<head>
<title>Consultar Carga Academica</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="private/validarcargaacademica.js"></script>
<style>
<!--
.pre         { color: #0000FF }
.esp         { color: #008080 }
.com         { color: #FF0000 }
-->
</style>
</head>
<body>
<table width="100%" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse">
  <tr>
    <td height="5%" class="usattitulo" width="529">Reporte de Carga Académica</td>
    <td height="5%" width="100">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" height="10%" valign="top" width="635">
	<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
      <tr>
        <td width="18%">Ciclo</td>
        <td width="82%"><%call llenarlista("cboCiclo","ActualizarListaCarga('rptecarga.asp')",rsCac,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>
		</td>
      </tr>
      <tr>
        <td width="18%">Departamento</td>
        <td width="82%"><%call llenarlista("cboDpto","ActualizarListaCarga('rptecarga.asp')",rsDpto,"codigo_dac","nombre_dac",codigo_dac,"Seleccionar el Dpto Académico","S","")%></td>
      </tr>
    </table></td>
  </tr>
</table>
<br>
<%
if codigo_dac<>"-2" and codigo_dac<>"" then
	Set Obj=CreateObject("PryUSAT.clsDatDocente")
		Set rsDocente=Obj.ConsultarCargaAcademica("RS", "DCA",codigo_cac,codigo_dac)
		i=0
	If Not (rsDocente.BOF AND rsDocente.EOF) then
		Do While Not rsDocente.EOF
 			i=i+1
 %>
	<table width="100%" border="1" bordercolor="#C0C0C0" style="border-collapse: collapse" cellpadding="3" cellspacing="0">
	  <tr bgcolor="#FFFFFF">
	  	<td bgcolor="#FFFFCC" width="10%"><b>Docente:</b></td>
   		<td colspan="7" width="90%"><%=rsDocente("Docente")%>&nbsp;</td>
      </tr>
	  <%
  		Set rsCarga = Obj.ConsultarCargaAcademica("RS", "9",codigo_cac,rsDocente("codigo_per"))
  	
	  	if Not(rsCarga.BOF AND rsCarga.EOF) then
  	  %>
	  <tr class="etabla"> 
    	<th width="10%">Código</th>
	    <th width="40%">Nombre del Curso</th>
    	<th width="10%">Grupo horario</th>
	    <th width="20%">Escuela Profesional</th>
    	<th width="5%">Ciclo</th>
    	<th width="5%">Hrs. Clase</th>
    	<th width="5%">Hrs. Asesoría</th>
	    <th width="5%">Total Hrs.</th>
	  </tr>
  		<%
  		totalhoras=0
  		totalComp=0
  		totalEsp=0
  		clase=""
  		  		
  		Do while not rsCarga.EOF
  			clase=""
  			if IsNull(rsCarga("totalcarga"))=true then
  				TC=0
  			else
  				TC=rsCarga("totalcarga")
  			end if
  			
			if (rsCarga("codigo_cpf") <> 19 And rsCarga("codigo_cpf") <> 25) then
		  			totalhoras=totalhoras + TC
		  			clase="class='pre'"
		  	else
		  		if rsCarga("codigo_cpf") = 19 then		  		
			  		totalComp= totalComp + TC
		  			clase="class='com'"			  		
				else
					if rsCarga("codigo_cpf") = 25 then
						totalEsp= totalEsp + TC
						clase="class='esp'"
					end if
				end if
			end if		
	  	%>
	  <tr <%=clase%>> 
    	<td width="10%"><%=rsCarga("identificador_Cur")%>&nbsp;</td>
	    <td width="40%"><%=rsCarga("nombre_Cur")%>&nbsp;</td>
    	<td width="10%" align="center"><%=rsCarga("grupoHor_Cup")%>&nbsp;</td>
	    <td width="20%"><%=rsCarga("abreviatura_Cpf")%>&nbsp;</td>
    	<td width="5%" align="center"><%=rsCarga("ciclo_Cur")%>&nbsp;</td>
    	<td width="5%" align="center"><%=rsCarga("totalhorasaula")%>&nbsp;</td>
    	<td width="5%" align="center"><%=rsCarga("totalhorasasesoria")%>&nbsp;</td>
	    <td width="5%" align="center"><%=rsCarga("totalcarga")%>&nbsp;</td>
	  </tr>
  			<%rsCarga.MoveNext
	 	Loop
    	Set rsCarga=nothing%>

	  <tr bgcolor="#FFFFCC"> 
  	    <td colspan="6" align="LEFT" width="90%"><b><span class='pre'>HORAS PREGRADO:<%=totalHoras%></span>&nbsp;&nbsp;&nbsp;&nbsp;<span class='com'>HORAS COMPL.:<%=totalComp%></span>&nbsp;&nbsp;&nbsp;&nbsp;<span class='esp'>HORAS PRG.ESPEC.:<%=totalEsp%></span>&nbsp;&nbsp;</b></td>
    	<td align="center" width="5%"><b>HORAS TOTALES</b></td>
    	<td align="center" width="5%"><b><%=totalHoras + totalComp + totalEsp%>&nbsp;</b></td>
	  </tr>


  	  <%end If%>
	</table><br>            
	    <%rsDocente.MoveNext
	 	Loop
	else
		response.write "<p class=""usatsugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado profesores con Carga Académica</p>"
	end if
Set rsdocente=nothing
Set Obj=nothing
Set rsCac=nothing
Set rsDpto=nothing
end if
%>
</body>
</html>