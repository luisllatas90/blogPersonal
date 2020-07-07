<!--#include file="clscurso.asp"-->
<%
codigo_per=request.querystring("codigo_per")
codigo_cup=request.querystring("codigo_cur")
if codigo_per="" then codigo_per=0
if codigo_cup="" then codigo_cup=0

'Set rsDoc= objDoc.ConsultarDocente("RS","TD","","")
'Set rsCiclo= objCiclo.ConsultarCicloAcademico ("RS","TO","")
'Set rsCarga= objCarga.ConsultarCargaAcademica ("RS","CA",intCicloAcademico,intDocente)

%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Matrícula de cursos virtuales</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
</head>
<body>
<form name="frmLista" method="POST" action="procesar.asp?codigo_cup=<%=codigo_cup%>&titulocurso=<%=nombre_cur%>&idusuario=<%=codigo_per%>">
</form>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr>
    <td width="19%" class="etiqueta">Profesor</td>
    <td width="81%"><%call escribirlista("cbodocente","","OnChange=""actualizarlista('matriculacursovirtual.asp?codigo_per=' + this.value)""",codigo_per,"clscurso","5",codigo_per,"","")%></td>
  </tr>
  <tr>
    <td width="19%" class="etiqueta">Curso Programado</td>
    <td width="81%">.</td>
  </tr>
  <tr>
    <td width="19%" class="etiqueta">&nbsp;</td>
    <td width="81%">
    <input type="button" value="Buscar" name="cmdBuscar" class="buscar"> 
    <input onClick="window.close()" type="button" value="Cancelar" name="cmdCancelar" class="salir">
    <input type="button" value="Matricular" name="cmdMatricular" class="guardar"></td>
  </tr>
</table>
<%if IsEmpty(Arrdatos) then%>
	<p class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; No se han matriculado alumnos en el curso programado seleccionado</p>
<%else%>
<br>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="70%">
  <tr class="etabla">
    <td width="5%" height="5%">
    <input type="checkbox" name="chkSeleccionar" onclick="MarcarTodoCheck(frmLista)" value="1"></td>
    <td width="18%" height="5%">Código</td>
    <td width="38%" height="5%">Apellidos y Nombres</td>
    <td width="73%" height="5%">e-mail</td>
  </tr>
  <tr>
    <td width="100%" colspan="4" height="95%">
     <DIV id="listadiv" style="height:100%;">
    <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
    <%for i=lbound(Arrdatos,2) to Ubound(Arrdatos,2)%>
  	<tr>
    <td width="5%"><input type="checkbox" name="chk" value="<%=Arrdatos(0,I)%>" onclick=pintafilamarcada(this)></td>
    <td width="18%"><%=Arrdatos(1,i)%>&nbsp;</td>
    <td width="38%"><%=Arrdatos(2,i)%>&nbsp;</td>
    <td width="73%"><%=Arrdatos(3,i)%>&nbsp;</td>
  	</tr>
  	<%next%>
</table>
    </td>
  </tr>
</table>
<%end if%>
</form>
</body>
</html>