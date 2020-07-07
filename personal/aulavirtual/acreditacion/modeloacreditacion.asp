<!--#include file="clsAcreditacion.asp"-->
<%
Dim scriptTabla

'---------------------------------------------------------------------------------		
'Variable querystring para abrir tablas evaluadas y registradas con tareas recientemente
'---------------------------------------------------------------------------------
idindicadorE=request.querystring("idindicadorE")
idvariableE=request.querystring("idvariableE")
idseccionE=request.querystring("idseccionE")
'---------------------------------------------------------------------------------
if idindicadorE<>"" then
	scriptTabla="onLoad=""MostrarTabla(tblseccion" & idseccionE & ",'../images/',imgseccion" & idseccionE & ");MostrarTabla(tblvariable" & idvariableE & ",'../images/',imgVariable" & idvariableE & ")"""
end if
%>
<HTML>
	<HEAD>
		<title>listasecciones</title>
		<meta name="vs_showGrid" content="True">
		<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie3-2nav3-0">
		<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
		<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
	</HEAD>
	<body <%=scripttabla%>>
		<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="colorbarra">
          <tr>
            <td class="e1">Asignación de tareas para la evaluación del Modelo de Acreditación</td>
            <td style="cursor:hand" onClick="AbrirTodasTablas('../images/',AbrirTodo,'')" align="right">
            <IMG id="AbrirTodo" SRC="../../../images/desplegar.gif">&nbsp;Mostrar todo&nbsp;</td>
          </tr>
        </table>
        	<span class="sugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Despliegue las secciones y variables y asigne las tareas según el indicador que desea evaluar</span>
        <br>
		<%
		dim acreditacion
		Set acreditacion=new clsacreditacion
			Call acreditacion.MostrarSeccionModelo(session("idmodelo"))
		Set acreditacion=nothing
		%>
	</body>
</html>