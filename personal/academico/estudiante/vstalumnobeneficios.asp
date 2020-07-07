<!--#include file="../../../funciones.asp"-->
<%
if session("codigo_tfu") = "" then
    Response.Redirect("../../../sinacceso.html")
end if

codigo_cac=request.querystring("codigo_cac")

if codigo_Cac="" then codigo_cac=session("codigo_cac")

Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsCiclo= obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
	
		if codigo_cac<>"" then
			Set rsbecados=Obj.Consultar("ConsultarBeneficioEstudiante","FO",1,codigo_cac,0,0)
		end if
	Obj.CerrarConexion
	Set Obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link href="../../../private/estilo.css" rel="stylesheet" type="text/css">
<title>Beneficios académicos</title>
</head>
<body>

<p class="usatTitulo">Beneficios de beca por semestre académico</p>
<table style="width: 100%">
	<tr>
            <td width="30%">Semestre académico</td>
      		<td width="70%">
          	<%        	
         	call llenarlista("cbocodigo_cac","location.href='vstalumnobeneficios.asp?codigo_cac=' + this.value",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")
			%>
        	</td>
        </tr>
	<tr>
            <td width="30%">&nbsp;</td>
      		<td width="70%">
          	&nbsp;</td>
        </tr>
</table>
<%if (rsBecados.BOF and rsBecados.EOF) then%>
<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp; No se han encontrado estudiantes con beneficio</h5>
<%else%>
<BR>
<table style="border-collapse: collapse; " cellpadding="3" cellspacing="0" bordercolor="gray" border="1" width="100%">
	<tr class="etabla">
		<td>Carrera Profesional</td>
		<td>Tipo Beneficio</td>
		<td>Nro. de estudiantes</td>
	</tr>
	<%
	Do while Not rsBecados.EOF
		t=t+rsBecados("total")
	%>
	<tr>
		<td><%=rsBecados("nombre_cpf")%>&nbsp;</td>		
		<td><%=rsBecados("descripcion_tben")%>&nbsp;</td>
		<td><%=rsBecados("total")%></td>
	</tr>
	<%
		rsBecados.movenext
		Loop
	Set rsBecados=nothing
	%>
	<tr class="etiqueta">
		<td colspan="2" align="right">TOTAL</td>
		<td><%=t%></td>
	</tr>
</table>
<%end if%>
</body>
</html>
