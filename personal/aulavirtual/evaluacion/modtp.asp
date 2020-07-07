<!--#include file="clsevaluacion.asp"-->
<%
idevaluacion=request.querystring("idevaluacion")
accion=request.querystring("accion")

if idevaluacion="" then idevaluacion=session("idEvaluacion")
if accion="" then accion="GuardarEncuesta"

'---------------------------------------------------------
'Verificar si el usuario ya ha ingresado a la evaluación
'---------------------------------------------------------
Set evaluacion=new clsevaluacion
	with evaluacion
			ArrDatos=.Consultar("3",idevaluacion,"","")
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Ficha de evaluación</title>
<link rel="stylesheet" type="text/css" href="../../../private/estiloaulavirtual.css">
<script language="JavaScript" src="private/validarpregunta.js"></script>
<base target="preguntasEvaluacion">
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
</head>
<body>
<%If IsEmpty(Arrdatos) then%>
	<h5>No se han registrado Preguntas para esta Evaluación</h5>
<%Else%>
<form name="frmPreguntas" method="post" onSubmit="return validarTodaPregunta(this)" action="procesarencuesta.asp?accion=<%=accion%>">
<table class="contornotabla" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" height="95%">
      <tr class="encabezadopregunta">
        <td width="40%" height="10%" class="e1">Por favor responda a las siguientes preguntas:</td>
        <td width="10%" align="right" class="e2" height="10%">
        <input type="submit" value="   Guardar Todo" name="cmdGuardar" class="guardar" style="width: 110"></td>
      </tr>
<tr>
<td width="100%" height="90%" colspan="2">
<div id="listadiv" style="height:100%" class="NoImprimir">
<%For I=Lbound(Arrdatos,2) to Ubound(Arrdatos,2)%>
<table border="0" cellpadding="5" cellspacing="0" style="border-collapse: collapse" width="100%">
  <tr class="variable">
    <td width="8%" valign="top"><b>Pregunta <%=i+1%> :</b></td>
    <td width="92%" valign="top"><b><%=Arrdatos(5,I)%></b>&nbsp;</td>
  </tr>
  <tr>
    <td width="100%" colspan="2">
    <%Select case Arrdatos(2,I)
    	case 1%><input type="text" name="descripcionrpta<%=arrdatos(1,i)%>" size="80" value="<%=descripcionrpta%>" class="Cajas" idPre="Pregunta<%=Arrdatos(1,I)%>" >
    	<%case 2: .CargarAlternativas2 Arrdatos(1,I),Arrdatos(2,I)
    	case 3:.CargarAlternativas2 Arrdatos(1,I),Arrdatos(2,I)%>
    	<%'case 4%>
    	<%case 5%><textarea rows="4" name="descripcionrpta<%=arrdatos(1,i)%>" cols="80" class="Cajas" idPre="Pregunta<%=Arrdatos(1,I)%>" ><%=descripcionrpta%></textarea>
    	<%case 6:.CargarAlternativas2 Arrdatos(1,I),Arrdatos(2,I)
    End select%> &nbsp;
    <input type="hidden" name="cIdTipoPregunta" value="<%=Arrdatos(2,I)%>">
    <input type="hidden" name="cIdPregunta" value="<%=Arrdatos(1,I)%>">
    </td>
  </tr>
  <%if Arrdatos(7,I)<>"" OR Arrdatos(8,I)<>0 then%>
  <tr>
    <td width="100%" colspan="2">
    	<b>Referencias:</b><br>
        <%if Arrdatos(7,I)<>"" then%><br><img border="0" src="../../images/<%=Arrdatos(7,i)%>"><%end if%>
    	<%if Arrdatos(8,I)<>"" then%><br><br><b>Página web:</b> <a target="_blank" href="<%=Arrdatos(8,I)%>"><%=Arrdatos(8,I)%></a><%end if%>
    &nbsp;</td>
  </tr>
  <%end if%>
</table>
<%Next
End if%>
</div>
</td>
</tr>
</table>
</form>
</body>
</html>
<%
	end with
Set evaluacion=nothing	
%>