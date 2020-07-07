<%
codigo_cup=request.querystring("codigo_cup")

set obj=Server.createObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsAlumnos=Obj.Consultar("ConsultarAlumnosMatriculados","FO",5,codigo_cup,0,0)
	Obj.CerrarConexion
Set obj=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Registro de notas</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<style>
<!--
.letrapequeña {
	font-size: xx-small;
}
.mensajeAviso {
	font-size: 14;
}
.Aprobado {
	color: #0000FF;
}
.Desaprobado{
	color: #FF0000;
}
.Retirado {
	color: #008080;
}
.Inhabilitado {
	color: orange;
}
-->
</style>
</head>
<body>
<table height="100%" width="99%" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse;" class="contornotabla">
<tr class="etabla">
	<td height="5%" width="5%">Nº</td>
	<td height="5%" width="15%">Escuela Profesional</td>
	<td height="5%" width="10%">Código</td>
	<td width="43%">Apellidos y Nombres</td>
	<td height="5%" width="8%">Nota final</td>
	<td height="5%" width="10%">Condición</td>
</tr>
<%
	aprobados=0:desaprobados=0:retirados=0:inhabilitados=0
	codigoAlu_Actual=""
	estadodma_Actual=""
	
	while rsAlumnos.eof=false
			i=i+1
			'codigoAlu_Actual=rsAlumnos("codigo_alu")
			if rsAlumnos("estado_dma")="R" then 
				retirados=retirados+1
			end if 

			if rsAlumnos("condicion_dma")="Aprobado" and rsAlumnos("estado_dma")="M" then					
					aprobados=aprobados+1
			end if 
			if rsAlumnos("condicion_dma")="Desaprobado" and rsAlumnos("estado_dma")="M" then					
					desaprobados=desaprobados+1
			end if
			if rsAlumnos("condicion_dma")="Inhabilitado" and rsAlumnos("estado_dma")="M" then					
					inhabilitados=inhabilitados+1
			end if 
	       
%>
<tr onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)" class="letrapequeña">
	<td width="5%" align="center"><%=i%>&nbsp;</td>
	<td width="15%"><%=rsAlumnos("nombre_cpf")%>&nbsp;</td>
	<td align="right" width="12%"><%=rsAlumnos("codigoUniver_Alu")%>&nbsp;</td>
	<td align="left" width="45%"><%=rsAlumnos("alumno")%>&nbsp;</td>
	<td align="center" width="8%"><%=rsAlumnos("notafinal_dma")%>&nbsp;</td>
	<td class="<%=rsAlumnos("condicion_dma")%>"><%=rsAlumnos("condicion_dma")%>&nbsp;</td>
</tr>
		<%
		rsAlumnos.movenext
	wend
	rsAlumnos.close
	set rsAlumnos=Nothing
%>
<tr>
	<td width="100%" height="5%" colspan="6" class="usattablainfo">
	&nbsp;<span class="azul">Aprobados:<%=aprobados%></span>  | 
	<span class="rojo">Desaprobados: <%=desaprobados%> | </span> 
	<span class ="cursos">Retirados: <%=retirados%>| </span> 
	<span style="color:orange;">Inhabilitados: <%=inhabilitados%>&nbsp;</span>
	</td>
</tr>
</table>
</body>
</html>