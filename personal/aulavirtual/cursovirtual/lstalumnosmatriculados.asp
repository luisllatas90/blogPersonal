<!--#include file="clscargaacademica.asp"-->
<%
Dim activarnotas
codigo_cac=request.querystring("codigo_cac")
codigo_cup=request.querystring("codigo_cup")
pagina=request.querystring("pagina")
if codigo_cac="" then codigo_cac=0

identificador_cur=request.querystring("identificador_cur")
nombre_cur=request.querystring("nombre_cur")
grupohor_cur=request.querystring("grupohor_cur")
ciclo_cur=request.querystring("ciclo_cur")
codigo_per=request.querystring("codigo_per")
nombre_per=request.querystring("nombre_per")
codigo_prof=request.querystring("codigo_prof")

Set notas=new clscargaacademica
	with notas
		Set rsAlumnos=.ConsultarRegistroNotas(codigo_cup)
		Set rsCicloAcademico=.ConsultarCicloAcademico("CO",codigo_cac)
		if rsCicloAcademico.recordcount>0 then
			fechainicio=rsCicloAcademico("fechaini_cac")
			fechafin=rsCicloAcademico("fechafin_cac")
		end if
		set rsCicloAcademico=nothing
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>PASO 2: Elegir los Alumnos que se matricularán en el curso</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarcurso.js"></script>
</head>
<body topmargin="0" leftmargin="0">
<form name="frmLista">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="barraherramientas">
    <tr>
      <td width="30%">
      	<input type="button" onclick="MatricularCursoVirtual('<%=codigo_prof%>','<%=fechainicio%>','<%=fechafin%>','<%=codigo_cup%>')" value="     Matricular" class="guardar3" name="cmdGuardar">
    	<input onclick="location.href='frmcargaacademica.asp?codigo_cac=<%=codigo_cac%>&codigo_per=<%=codigo_per%>'" type="button" value="    Regresar" class="cerrar3" name="cmdCancelar"></td>
      <td width="70%"><span id="mensaje" style="color:#FF0000"></span></td>
    </tr>
  </table>
<br>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="97%">
  <tr>
    <td width="12%" class="etiqueta">Docente</td>
    <td width="88%" colspan="3" id="nombre_per"><%=nombre_per%></td>
  </tr>
  <tr>
    <td width="12%" class="etiqueta">Asignatura</td>
    <td width="88%" colspan="3" id="nombre_cur"><%=nombre_cur%></td>
  </tr>
  <tr>
    <td width="12%" class="etiqueta">Código</td>
    <td width="44%" class="etiqueta" id="identificador_cur"><%=identificador_cur%></td>
    <td width="22%" class="etiqueta" id="grupo_cur">Grupo <%=grupohor_cur%></td>
    <td width="22%" class="etiqueta" id="ciclo_cur">Ciclo <%=ciclo_cur%></td>
  </tr>
</table>
<br>
<table width="97%" border="0" cellpadding="0" cellspacing="0" bordercolor="#808080" style="border-collapse: collapse" height="73%">
	<tr class="etabla">
	<td align="center" height="5%" class="contornotabla" width="3%">
    <input type="checkbox" name="chkSeleccionar" onclick="MarcarTodoCheck(frmLista)" value="1"></td>
	<td align="center" height="5%" class="contornotabla" width="5%">Nº</td>
	<td align="center" height="5%" class="contornotabla" width="12%">Código</td>
	<td align="center" height="5%" class="contornotabla" width="45%">Apellidos y Nombres</td>
	<td align="center" height="5%" class="contornotabla" width="30%">email</td>
	</tr>
	<tr><td colspan="5" valign="top">
	<div id="listadiv" style="height:100%" class="contornotabla">
	<table width="100%" border="0" cellpadding="2" cellspacing="0" bordercolor="#DCDCDC" style="border-collapse: collapse">
	<%Do while not rsAlumnos.eof
		i=i+1%>
		<tr onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)" id="fila<%=rsAlumnos("codigouniver_alu")%>">
		<td width="5%" align="center"><input type="checkbox" name="chk" value="<%=rsAlumnos("codigouniver_alu")%>" onclick=pintafilamarcada(this) id="fila<%=i%>" nombre_alu="<%=rsAlumnos("alumno")%>" email_alu="<%=rsAlumnos("email_alu")%>"></td>
		<td width="5%" align="center"><%=i%>&nbsp;</td>
		<td align="right" width="12%"><%=rsAlumnos("codigoUniver_Alu") %>&nbsp;</td>
		<td align="left" width="47%"><%=rsAlumnos("alumno")%>&nbsp;</td>
		<td align="center" width="30%">
        <input  maxLength="100" size="82" name="email" class="cajas" value="<%=rsAlumnos("email_alu")%>" id="email" onChange="actualizarcorreo(this,fila<%=i%>)"></td>
		</tr>
		<%rsAlumnos.movenext
	Loop
	rsAlumnos.close
	set rsAlumnos=Nothing
	%>
	</table>
	<div>
	</td></tr>
</table>
</body>
</html>
	<%end with
Set notas=nothing
%>