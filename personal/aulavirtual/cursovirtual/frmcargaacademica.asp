<!--#include file="../../../funcionesaulavirtual.asp"-->
<!--#include file="clscargaacademica.asp"-->
<%
codigo_per=Request.querystring("codigo_per")
codigo_cac=Request.querystring("codigo_cac")
nombre_usu=Request.querystring("nombre_usu")
if codigo_per="" then codigo_per=0

Dim notas

Set notas=new clscargaacademica
	with notas
		Set rsDoc=.ConsultarDocente("TD","","")
		Set rsCac=.ConsultarCicloAcademico("TO","")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Expires" content="0"> 
<meta http-equiv="Last-Modified" content="0"> 
<meta http-equiv="Cache-Control" content="no-cache, mustrevalidate"> 
<meta http-equiv="Pragma" content="no-cache">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="private/validarcurso.js"></script>
<title>Crear Cursos Virtuales según Carga Lectiva del Profesor</title>
</head>
<body topmargin="0" leftmargin="0" bgcolor="#E9F3FC">
<table cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" id="tblbotones" class="barraherramientas">
  	<tr><td>
	<input type="button" value="Crear Cursos" class="guardar3" name="cmdGuardar" name="cmdGuardar" disabled=true onClick="ConfirmarCurso()" style="width: 120">
	<input onClick="top.window.close()" type="button" value="   Cancelar" name="cmdCancelar" class="cerrar3">
	<span id="mensaje" style="color:#FF0000"></span>
	</td></tr>
</table>

  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="5%" class="etiqueta">Ciclo</td>
    <td width="10%">
    <select id="cbocodigo_cac" onChange="actualizarlista('frmcargaacademica.asp?codigo_cac=' + this.value + '&codigo_per=' + cbocodigo_per.value + '&nombre_usu=' + cbocodigo_per.options[cbocodigo_per.selectedIndex].text)">
	<%do while not rsCac.eof%>
        <option value="<%=rsCac(0)%>" <%=SeleccionarItem("cbo",codigo_cac,rsCac(0))%>><%=rsCac("Descripcion_Cac")%></option>
		<%rsCac.movenext
	loop
	rsCac.Close
	Set rsCac=Nothing
	%>
      </select></td>
    <td width="10%" align="right">Docente</td>
    <td width="70%">
        <select id="cbocodigo_per" onChange="actualizarlista('frmcargaacademica.asp?codigo_cac=' + cbocodigo_cac.value + '&codigo_per=' + this.value + '&nombre_usu=' + this.options[this.selectedIndex].text)" class="cajas">
          <option value="0">---Seleccione Docente---</option>
			<%Do while not rsDoc.eof%>
				<option value="<%=rsDoc("Codigo_Per")%>" <%=SeleccionarItem("cbo",codigo_per,rsDoc(0))%>><%=rsDoc("Docente")%>
			</option>
			<%rsDoc.movenext
			loop%>
		</select></td>
  </tr>
  </table>
<%
if codigo_per>0 then
	Set rsCarga=.ConsultarCargaAcademica(codigo_cac,codigo_per)

  	if (rsCarga.BOF and rsCarga.EOF) then
  		response.write "<p class=""sugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;El profesor seleccionado no tiene carga académica para el ciclo académico</p>"
  	else
%>
<form name="frmcurso" method="POST" Action="procesar.asp?accion=agregarcursovirtual&codigo_cac=<%=codigo_cac%>&codigo_per=<%=codigo_per%>&login_per=<%=rsCarga("login_per")%>">
<div align="center">
  <center>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="95%">
  <tr>
    <td width="54%" colspan="5" bgcolor="#C0C0C0" style="border-left-width: 1; border-right: 1px solid #C0C0C0; border-top-width: 1; border-bottom-width: 1">
    <b><font size="3" color="#FFFCBF">Asignaturas del Profesor</font></b></td>
    <td width="46%" colspan="4" bgcolor="#C0C0C0" align="right" class="rojo">
    <input type="checkbox" name="chkagrupar" value="A">Agrupar 
    en un sólo curso los check marcados</td>
  </tr>
  <tr class="etabla">
    <td width="3%">&nbsp;</td>
    <td width="5%">Nº</td>
    <td width="12%">Código</td>
    <td width="30%">Descripción de Asignatura</td>
    <td width="10%" colspan="2">GH</td>
    <td width="5%">Ciclo</td>
    <td width="30%">Escuela Profesional</td>
    <td width="5%">Matriculados</td>
  </tr>
  <%
  do while not rsCarga.EOF 	
 	i=i+1%>
  <tr id="fila<%=i%>" >
    <td width="3%" align="center" bgcolor="#FFFFFF"><input type="checkbox" onClick="validarcargaacademica()" name="chkcursoshabiles" value="<%=rsCarga("codigo_cup")%>"></td>
    <td width="5%" align="center" bgcolor="#FFFFFF"><%=i%>&nbsp;</td>
    <td width="12%" bgcolor="#FFFFFF"><%=rsCarga("identificador_Cur")%>&nbsp;</td>
    <td width="30%" bgcolor="#FFFFFF"><%=rsCarga("nombre_Cur")%>&nbsp;</td>
    <td width="10%" align="center" bgcolor="#FFFFFF" colspan="2"><%=rsCarga("grupoHor_Cup")%>&nbsp;</td>
    <td width="5%" align="center" bgcolor="#FFFFFF"><%=ConvRomano(rsCarga("ciclo_cur"))%>&nbsp;</td>
    <td width="30%" bgcolor="#FFFFFF"><%=rsCarga("nombre_cpf")%><span style="font-size: 7pt"> (<%=rsCarga("descripcion_pes")%>)</span>&nbsp;</td>
    <td width="10%" align="center" bgcolor="#FFFFFF"><%=rsCarga("matriculados")%>&nbsp;</td>
  </tr>
  	<%
  	rsCarga.movenext
  loop
  
  Set rsCarga=nothing%>
</table>
  </center>
</div>
</form>
	<%end if
end if%>
</body>
</html>
<%
	end with
Set notas=nothing
%>