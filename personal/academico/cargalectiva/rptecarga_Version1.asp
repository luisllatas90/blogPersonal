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
	  	<td bgcolor="#FFFFCC" width="76"><b>Docente:</b></td>
   		<td colspan="7" width="100%"><%=rsDocente("Docente")%>&nbsp;</td>
      </tr>
	  <%
  		Set rsCarga = Obj.ConsultarCargaAcademica("RS", "9",codigo_cac,rsDocente("codigo_per"))
  	
	  	if Not(rsCarga.BOF AND rsCarga.EOF) then
  	  %>
	  <tr class="etabla"> 
    	<th width="76">Código</th>
	    <th width="233">Nombre del Curso</th>
    	<th width="52">Grupo horario</th>
	    <th width="104">Escuela Profesional</th>
    	<th>Ciclo</th>
    	<th>Hrs. Aula</th>
    	<th>Hrs. Asesoría</th>
	    <th>Horas</th>
	  </tr>
  		<%
  		totalhoras=0
  		Do while not rsCarga.EOF
  			if IsNull(rsCarga("totalcarga"))=true then
  				TC=0
  			else
  				TC=rsCarga("totalcarga")
  			end if
  			totalhoras=totalhoras + TC
	  	%>
	  <tr> 
    	<td width="76"><%=rsCarga("identificador_Cur")%>&nbsp;</td>
	    <td width="233"><%=rsCarga("nombre_Cur")%>&nbsp;</td>
    	<td width="52" align="center"><%=rsCarga("grupoHor_Cup")%>&nbsp;</td>
	    <td width="104"><%=rsCarga("abreviatura_Cpf")%>&nbsp;</td>
    	<td width="33" align="center"><%=rsCarga("ciclo_Cur")%>&nbsp;</td>
    	<td width="4" align="center"><%=rsCarga("totalhorasaula")%>&nbsp;</td>
    	<td width="4" align="center"><%=rsCarga("totalhorasasesoria")%>&nbsp;</td>
	    <td width="39" align="center"><%=rsCarga("totalcarga")%>&nbsp;</td>
	  </tr>
  			<%rsCarga.MoveNext
	 	Loop
    	Set rsCarga=nothing%>
	  <tr> 
	    <td colspan="7" align="right">TOTAL</td>
    	<td align="center" width="39"><%=totalHoras%>&nbsp;</td>
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