<!--#include file="../../../../funciones.asp"-->
<%
Dim todos, mensaje


codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
usuario=session("codigo_usu")

if cint(codigo_cpf)=-1 then
	mensaje="(*) Estos resultados no incluyen Programa-Cursos-Especiales"
end if


if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"
if codigo_cpf="" then codigo_cpf="-2"

Set objEscuela=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objEscuela.AbrirConexion
		if session("codigo_tfu")=1 or session("codigo_tfu")=7 or session("codigo_tfu")=16 or session("codigo_tfu")=35 or session("codigo_tfu")=23 then 'tfu Decano
		    if request.QueryString("mod") = "" then
		        Set rsEscuela= objEscuela.Consultar("ConsultarCarreraProfesional","FO","MA",0)
		    elseif request.QueryString("mod") = "10" then
		        Set rsEscuela= objEscuela.Consultar("ConsultarCarreraProfesional","FO","GO",0)
		    else		       
		       Set rsEscuela= objEscuela.Consultar("ConsultarCarreraProfesional","FO","MA",request.QueryString("mod"))
		       
		    end if
		    
		    todos="S"
		else
			'Set rsEscuela= objEscuela.Consultar("consultaracceso","FO","ESC","Silabo",usuario)
			Set rsEscuela= objEscuela.Consultar("consultaracceso","FO","ESC",request.QueryString("mod"),usuario)
		end if

		if codigo_cac<>"-2" and codigo_cpf<>"-2" then
			'Set rsCursoProg= objEscuela.Consultar("ConsultarAlumnosMatriculados","FO",12,codigo_cac,codigo_cpf,0)
			Set rsCursoProg= objEscuela.Consultar("ConsultarAlumnosMatriculados","FO",12,codigo_cac,codigo_cpf,request.QueryString("mod"))
			
			
			if Not(rsCursoProg.BOF and rsCursoProg.EOF) then
				activo=true
			end if
			
		end if
	objEscuela.CerrarConexion
Set objEscuela=nothing
%>
<html>
<head>
<title>Matriculados por Asignatura y Ciclo Académico</title>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/tooltip.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}</style>
</head>
<body>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse; border-color:#111111;" width="100%">
  <tr class="usattitulo">
    <td width="100%" colspan="3" height="5%">Matriculados por Ciclo de Ingreso</td>
  </tr>
  <tr>
    <td width="27%" height="3%">Ciclo Académico</td>
    <td height="3%" colspan="2"><%call ciclosAcademicos("actualizarlista('matriculadosescuelaciclo.asp?codigo_cac='+this.value+'&mod='+mod.value);mensaje.innerHTML='Espere un momento...'",codigo_cac,"","")%></td>
 </tr>
  <tr>
  		<% response.write("<input type='hidden' id='mod' name='mod' value='" + request("mod") + "'  />") %>
    <td width="27%" height="3%">Escuela Profesional</td>
    <td width="60%" height="3%"><%call llenarlista("cbocodigo_cpf","actualizarlista('matriculadosescuelaciclo.asp?codigo_cac='+cbocodigo_cac.value+'&codigo_cpf='+cbocodigo_cpf.value+'&mod='+mod.value);mensaje.innerHTML='Espere un momento...'",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccione la Escuela Profesional",todos,"")%></td>
    <td width="13%" height="3%" align="right" ><%if activo=true then%>
        <input type="button" value="Imprimir" class="imprimir2" onclick="imprimir('N','','')"/> <%end if%>
    </td>
  </tr>
  <%if activo=true then%>
  <tr>
    <td width="100%" colspan="3" align="center">
    <table border="1" cellpadding="1" cellspacing="0" style="border-collapse: collapse; border-color:#666666" width="70%"   id="tblcursoprogramado">
      <tr class="etabla" style="background-color:#e33439;color:White;">
        <td width="40%" height="3%">Ciclo de Ingreso</td>
        <td width="15%" height="3%">Matriculados</td>
        <td width="15%" height="3%">Retirados</td>
        <td width="15%" height="3%">Matriculados - Retirados</td>        
      </tr>
		<%	
			Do while not rsCursoProg.eof
				i=i+1
				
				
				tpre=tpre+rsCursoProg("ret")
				tmat=tmat+rsCursoProg("mat")
				
		%>
			<tr>
				<td width="40%"><%=rsCursoProg("cicloing_alu")%></td>
				<td width="15%" align="center"><%=rsCursoProg("mat") + rsCursoProg("ret")%></td>
				<td width="15%" align="center"><%=rsCursoProg("ret")%></td>
				<td width="15%" align="center"><%=rsCursoProg("mat")%></td>
			</tr>
				<%rsCursoProg.movenext
			loop
		%>
      		<tr class="etabla" style="background-color:#e33439;color:White;">
				<td width="40%" align="right">TOTAL</td>
				<td width="15%" align="center"><%=tmat+tpre%></td>
				<td width="15%" align="center"><%=tpre%></td>
				<td width="15%" align="center"><%=tmat%></td>
			</tr>
		</table>
  </td>
  </tr>
  <%end if%>
</table>

	<p><b><font color="#FF0000"><%=mensaje%></font></b></p>

<span id="mensaje" class="rojo"></span>
</body>
</html>