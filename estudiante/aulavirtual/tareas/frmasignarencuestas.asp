<!--#include file="../../../NoCache.asp"-->
<!--#include file="../evaluacion/clsevaluacion.asp"-->
<%
idtarea=request.querystring("idtarea")
idtipopublicacion=request.querystring("idtipopublicacion")
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Seleccionar los evaluacions que se integrar�n a la tarea</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="../../../private/funcionesaulavirtual.js"></script>
<script language="JavaScript" src="../../../personal/aulavirtual/documentos/private/validardocumento.js"></script>
<style>
<!--
li	{cursor:hand;list-style-image: url('../../../images/cerrado.gif'); margin-left:-15}
-->
</style>
</head>
<body class="colorbarra" leftmargin="0" topmargin="0">
<form name="frmasignarevaluaciontarea" method="POST" action="procesar.asp?accion=asignarrecurso&idtarea=<%=idtarea%>&idtipopublicacion=<%=idtipopublicacion%>&nombretabla=evaluacion">
<input type="submit" value="Guardar" name="cmdGuardar" id="cmdGuardar" class="guardar"> <input OnClick="window.close()" type="button" value="Cancelar" name="cmdCancelar" id="cmdCancelar" class="salir">
<DIV id="listadiv" style="height:80%" class="contornotabla">
<table border="0" cellpadding="2" cellspacing="0" width="100%" height="80%" bgcolor="#FFFFFF" style="border-collapse: collapse" bordercolor="#111111">
   	<tr><td valign="top" height="100%">
		<ul style="margin-top: 3">
			<%Dim evaluacion
			Set evaluacion=new clsevaluacion
				with evaluacion
					.restringir=session("idcursovirtual")
					call .EncuestasDelUsuario(session("codigo_usu"),session("idcursovirtual"),idtarea)
				end with
			Set evaluacion=nothing%>
		</ul>
   		</td></tr>
</table>
</div>
<b>&nbsp;Encuestas creados por:&nbsp;<%=session("nombre_usu")%></b>
</form>
</body>
</html>