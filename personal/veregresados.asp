<%@LANGUAGE="VBSCRIPT" CODEPAGE="1252"%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Consulta de disponibilidad de ambientes</title>
<link rel="stylesheet" type="text/css" href="../private/estilo.css">

<script language="javascript1.1">
	function recarga ()
	{
		document.location="veregresados.asp?codigo_Cpf=" + select1.value;	
	}
</script>
</head>

<body>
<p>
  <%
	codigo_cpf	=request.QueryString("codigo_cpf")
	if codigo_cpf="" then
		codigo_cpf=-1
	end if 
	dim rs 	
	dim rs_listaegresados
	DIM rs_carreraprofesional
	set cn=server.createobject("pryusat.clsaccesodatos")
	
	cn.abrirconexion	
	set rs_carreraprofesional= cn.consultar ("dbo.sp_vercarreraprofesional","FO",	-1)
	
	response.write ("<select name='select1'>")
	while  rs_carreraprofesional.eof=false  and rs_carreraprofesional.bof=false
		if rs_carreraprofesional.fields("codigo_cpf")=codigo_cpf then
			seleccion ="Selected"
		end if 
		
	    response.write ("<option value='"& rs_carreraprofesional.fields("codigo_cpf")  & "' "  & seleccion & ">"  & ucase ( rs_Carreraprofesional.fields("nombre_cpf")) &"</option>")
		rs_carreraprofesional.movenext		
	wend 
   response.write ("</select>")	

	set rs_listaegresados=cn.consultar("dbo.sp_consultaralumnosegresados","FO","E",codigo_cpf)


	cn.cerrarconexion

%>
</p>
<p>
<input type='button' name='Submit' value='Enviar' onClick="recarga()">   
</p>

<table width="70%" border="1">
  <tr>
    <td>Código</td>
    <td>Alumno</td>
    <td>Plan Estudio</td>
    <td>Cilco Ing.</td>
  </tr>
<% 
	while  rs_listaegresados.eof =false and rs_listaegresados.Bof =false
%>
  <tr>  
    <td><%=rs_listaegresados.fields("codigouniver_Alu")%></td>
    <td><%=rs_listaegresados.fields("alumno")%></td>
    <td><%=rs_listaegresados.fields("descripcion_pes")%></td>
    <td><%=rs_listaegresados.fields("cicloing_alu")%></td>
  </tr>
  <%
  	rs_listaegresados.movenext
	wend	
  %>
  
</table>
<p>&nbsp;</p>
</body>

</html>