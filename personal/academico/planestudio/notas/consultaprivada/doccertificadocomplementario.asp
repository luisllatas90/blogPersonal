<!--#include file="../../../../funciones.asp"-->
<%
	tipo=request.querystring("tipo")
	codigo_alu=request.querystring("codigo_alu")
	codigo_pes=request.querystring("codigo_pes")

	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsHistorial=Obj.Consultar("GenerarCertificadoEstudios","FO",tipo,codigo_alu,codigo_pes)
	Obj.CerrarConexion
	Set Obj=nothing
	
	If (rsHistorial.BOF and rsHistorial.EOF) then%>
			<script language="Javascript">
				alert("No se han registrado Historial Académico para el estudiante seleccionado")
				history.back(-1)
			</script>
	<%else
		
		alumno=rsHistorial("alumno")
		nombre_esp=rsHistorial("especialidad")
		nombre_cpf=rsHistorial("escuela")
		nombre_fac=rsHistorial("facultad")
		codigouniver_alu=rsHistorial("codigouniver_alu")
		sexo_alu=rsHistorial("sexo_alu")
		tipoalumno=rsHistorial("tipoalumno")
		if tipoalumno >0  then
		    tipoalumno = iif(sexo_alu="F","egresada","egresado")
		else
		    tipoalumno =  "estudiante"
		end if		  		
		Response.ContentType = "application/msword"
		Response.AddHeader "Content-Disposition","attachment;filename=" & codigouniver_alu & ".doc"
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<style>
<!--
.etiqueta    { font-weight: bold;font-size:12px }
.cabezera    { font-weight: bold; text-align: center; background-color: #C0C0C0; border-left-width:1; border-right-width:1; border-top-style:solid; border-top-width:1; border-bottom-style:solid; border-bottom-width:1}
body         { font-family: Belwe Lt BT; font-size: 12pt }
td           { font-size: 12pt }
.style1 {
	font-family: "Monotype Corsiva";
	font-size: large;
}
.contornotabla {
	border: 1px solid #808080;
	background-color: #FFFFFF;
}
-->
</style>
</head>
<body style="margin-top:3.2cm">
<table cellpadding="2" cellspacing="0" style="border-collapse: collapse" width="100%">
  <THEAD>
  <tr>
  <td colspan="3">
	  <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="130">
	  <tr>
	    <th width="100%" colspan="2" height="80" align="center" valign="top">
	    <em> <font style="font-size: 14pt">El Secretario General  de la<br>
		Universidad Católica Santo Toribio de Mogrovejo</font></em></th>
	  </tr>
	  <tr>
	    <th width="100%" colspan="2" height="60" align="center" valign="top">
	    <font style="font-size: 16pt">
	    CERTIFICA
	    </font>
	    </th>
	  </tr>
	  <tr class="etiqueta">
	    <td width="24%" height="19"  colspan="2" style="width: 100%;text-align:justify">
	    Que <%=iif(sexo_alu="F","la señorita","el señor")%>&nbsp;<%=alumno%>, con código de matrícula Nº <%=codigouniver_alu%>,
	    <%=tipoalumno%> de la Escuela de <%=nombre_cpf%>,
	    <%if nombre_esp<>"" then%> en la Especialidad de <%=nombre_esp%>,<%end if%>
	    Facultad de <%=nombre_fac%> de nuestra Universidad,
	    ha aprobado los Cursos Complementarios según la siguiente descripción:
	    </td>
	  </tr>
	  </table>
  </td></tr>
  <tr><td colspan="3">&nbsp;</td></tr>
  <tr>
    <td height="14" class="contornotabla" align="center" width="15%">SEMESTRE ACADÉMICO</td>
    <td height="14" class="contornotabla" align="center" width="75%">ASIGNATURA</td>
    <td height="14" class="contornotabla" align="center" width="10%">PROMEDIO</td>
  </tr>
  </THEAD>
  <%
  totalcursos=0:NumConvalidaciones=0
  
  Do while not rsHistorial.eof
	  	  	
		totalcursos=totalcursos+1
  		
  		if rsHistorial("tipoMatricula_dma")="C" then
  			NumConvalidaciones=NumConvalidaciones+1
  		end if
  %>
  <tr>
    <td class="contornotabla" height="14" width="15%"><%=rsHistorial("descripcion_cac")%></td>
    <td class="contornotabla" height="14" width="75%"><%=rsHistorial("nombre_cur")%></td>
    <td class="contornotabla" align="center" height="14" width="10%"><%=rsHistorial("notafinal_dma")%></td>
  </tr>
  	<%rsHistorial.movenext
  loop%>
  </table>
  <%if NumConvalidaciones>0 then%>
  	<p><!--<b>(*)Asignaturas convalidadas</b>--></p>
  <%end if%>
  <p>&nbsp;</p>
<%
fechaimpresion=QuitarDia(formatdatetime(date,1))

if left(date,1)=0 then
	fechaimpresion=mid(fechaimpresion,2,len(fechaimpresion))
end if
%>
<p align="right"><b>Chiclayo, <%=lcase(fechaimpresion)%></b></p>
<p>&nbsp;</p>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="30%" align="right" class="etiqueta">
  <tr>
    <td width="100%" align="center">Mgtr. Jorge Pérez Uriarte</td>
  </tr>
  <tr>
    <td width="100%" align="center" class="style1">Secretario General</td>
  </tr>
</table>
</body>
</html>
	<%end if
Set rsHistorial=nothing
%>