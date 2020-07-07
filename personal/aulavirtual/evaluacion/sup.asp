<!--#include file="clsevaluacion.asp"-->
<%
dim Mensaje,mostrarScript
	
	function MostrarTiempo()
		If session("minutos")>0 then
			MostrarTiempo="Onload=""Empezar(" & int(session("minutos")) & ")"" "
		end if
	end function
	'oncontextmenu="return false"
	'OnUnload=AbrirEvaluacion("T")
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Título de Evaluación</title>
<base target="preguntasEvaluacion">
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="private/validarevaluacion.js"></script>
<script language="JavaScript" src="private/validarpregunta.js"></script>
<style>
<!--
.tiempo      {font-family: Verdana; font-size: 8pt; color: #800000; font-weight: bold;  }
-->
</style>
</head>
<body <%=mostrarTiempo%> topmargin="0" leftmargin="0" class="menusuperior">
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr>
    <td width="20%" rowspan="3"><img src="../../../images/logo.jpg"></td>
    <td width="100%" class="e1" colspan="2"><%=session("tipoeval")%>: <%=session("tituloeval")%>&nbsp;</td>
  </tr>
  <tr>
    <td width="60%"><%=session("descripcioneval")%></td>
    <%if session("minutos")>0 then%>
    <td width="20%" class="tiempo"><b>Duración: <%=iif(session("minutos")=0,"",session("minutos"))%> min.</b></td>
    <%end if%>
  </tr>
  <tr>
    <td width="60%"><%=session("instrucciones")%>&nbsp;</td>
    <%if session("minutos")>0 then%>
    <td width="20%" ><b>Transcurrido:</b>&nbsp;<span class="tiempo" id="txtTiempo" style="margin-top: 0">0</span>
    </td>
    <%end if%>
  </tr>
</table>
</body>
</html>